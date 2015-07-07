using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Collections.ObjectModel;
using Modules;
using SpaceManager;

namespace FE_Editor_Suite
{
    public partial class Hub : Form
    {
        public const int MAX_SIZE = 32*(1<<20);
        public const int BASE_METADATA_SIZE = 256;
        
        private byte[] ROMData = new byte[MAX_SIZE]; //32MB
        private MetadataManager metadata;
        private string ROMName = null;
        private string ROMPath = null;
        private bool ROMOpen = false;
        private bool ROMChanged = false;
        private int ROMSize = 0;
        private List<EditorModule> myModules = new List<EditorModule>();
        private object myLock = new object();
        private List<Tuple<EditorModule, int, byte[]>> accessedData = new List<Tuple<EditorModule,int, byte[]>>();
        private List<Tuple<int, byte[]>> xorDiffs = new List<Tuple<int,byte[]>>();
        private List<Tuple<int, byte[]>> condensedDiffs = new List<Tuple<int, byte[]>>();
        //private List<Tuple> floatingFreeSpace
        //private Ranges dataAllocation= new Ranges();
        private Ranges freeSpace = new Ranges();

        public string MetadataFolderPath {get; private set;}
        public ReadOnlyCollection<Tuple<int, int>> freeRanges { get { return freeSpace.getList(); } }



        public Hub()
        {
            /*
            myLock = new object();
            myModules = new List<EditorModule>();
            accessedData = new List<Tuple<EditorModule,int, byte[]>>();
            xorDiffs = new List<Tuple<int, byte[]>>();
            dataAllocation = new Ranges(); //TODO read this from metadata */
            InitializeComponent();
        }

        private void openROM(object sender, EventArgs e)
        {
            if (this.promptSaveIfChanged())
            {
                //Copy pasted part for opening file from MSDN
                OpenFileDialog openROMPicker = new OpenFileDialog();

                openROMPicker.InitialDirectory = "c:\\";
                openROMPicker.Filter = "ROMs (*.gba)|*.gba|All files (*.*)|*.*";
                openROMPicker.FilterIndex = 1;
                openROMPicker.RestoreDirectory = true;
                openROMPicker.Multiselect = false;

                //int ROMSize = 0;

                if (openROMPicker.ShowDialog() == DialogResult.OK)
                {
                    //try
                    {
                        // Insert code to read the stream here.
                        this.ROMPath = openROMPicker.FileName;
                        this.ROMName = openROMPicker.SafeFileName;
                        Stream ROMStream = null;
                        if ((ROMStream = openROMPicker.OpenFile())!=null)
                        {
                            this.ROMSize = ROMStream.Read(ROMData, 0, 33554432);
                            this.MetadataFolderPath = Path.Combine(Path.GetDirectoryName(ROMPath), Path.GetFileNameWithoutExtension(this.ROMName) + "_meta\\");
                            if(!Directory.Exists(MetadataFolderPath))
                            { Directory.CreateDirectory(MetadataFolderPath); }
                            processMetadata(ROMData, Path.Combine(MetadataFolderPath, "Hub.meta"));

                            this.ROMOpen = true;
                            Console.Out.WriteLine("ROM Opened succesfully, " + this.ROMSize + " bytes read.");

                            this.SetUp();
                        }
                    }
                    /*catch (Exception ex)
                    {
                        MessageBox.Show("Error: Could not read file from disk. Original error: \n" + ex.Message);
                        try { metadata.closeMetadata(); }
                        catch (Exception except) {;}
                    }*/
                }
            }
        }
        
        public void processMetadata(byte[] rawROMData, string metadataName)
        {
            metadata = new MetadataManager(metadataName);
            
            
            byte[][] init_table = metadata.getMetadataTable("INIT"); //INIT metadata only has 1 row.
            if(init_table == null) //INIT
            {
                setUpMetadata();
            }
            else
            {
                //In case something happened to the data
                addTableIfDoesNotExist("XORD");
                addTableIfDoesNotExist("FREE");
                
                //XORD
                
                //Read XOR diffs from base
                byte[][] xorDiffs_raw = metadata.getMetadataTable("XORD");
                foreach (byte[] xorDiff in xorDiffs_raw)
                {
                    int offset = BitConverter.ToInt32(xorDiff, 0);
                    byte[] diff = xorDiff.Skip(4).ToArray();
                    //xorDiffs.Add(Tuple.Create(offset, diff)); //Only keep logs from start of current session.
                    condensedDiffs.Add(Tuple.Create(offset, diff));
                    applyXORDiff(offset, diff); //Get ROM to most recent ver
                }
                /*
                //Read XOR diffs LOG
                xorDiffs_raw = metadata.tryToGetTable("XORL");
                foreach (byte[] xorDiff in xorDiffs_raw)
                {
                    int offset = BitConverter.ToInt32(xorDiff, 0);
                    byte[] diff = xorDiff.Skip(4).ToArray();
                    xorDiffsLog.Add(Tuple.Create(offset, diff));
                }*/
                
                
                
                //FREE
                byte[][] freeList = metadata.getMetadataTable("FREE");
                foreach(byte[] free in freeList)
                {
                    int start = BitConverter.ToInt32(free, 0);
                    int end = BitConverter.ToInt32(free, 4);
                    freeSpace.addRange(start, end);
                }

            }
        }
        
        public void addTableIfDoesNotExist(string iden)
        {
            if(!metadata.hasTable(iden))
            {
                byte[][] empty_table = new byte[0][];
                metadata.setMetadataTable(iden, empty_table);
            }
        }

        public void setUpMetadata()
        {
            applyAutopatches();
            byte[][] init = { BitConverter.GetBytes(true) };
            metadata.setMetadataTable("INIT", init);

            addTableIfDoesNotExist("XORD");
            addTableIfDoesNotExist("FREE");
        }

        private void applyXORDiff(int offset, byte[] diff)
        {
            for(int i=0; i<diff.Length; i++)
            {
                ROMData[offset + i] ^= diff[i];
            }
        }
        private List<Tuple<int, byte[]>> condenseXORDiffs()
        {
            //Ranges edited = new Ranges();
            //public void refreshData(int offset, int length)
            var diffs = new List<Tuple<int, byte[]>>();
            byte[] test = new byte[ROMSize];
            Array.Clear(test, 0, test.Length);
            foreach (Tuple<int, byte[]> elem in xorDiffs)
            {
                for (int index = 0; index < elem.Item2.Length; index++)
                {
                    test[elem.Item1 + index] ^= elem.Item2[index];
                }
            }
            foreach (Tuple<int, byte[]> elem in condensedDiffs)
            {
                for (int index = 0; index < elem.Item2.Length; index++)
                {
                    test[elem.Item1 + index] ^= elem.Item2[index];
                }
            }
            int i = 0;
            while (i<ROMSize)
            {
                if(test[i]==0)
                { i++; continue; }
                else
                {
                    int j = i;
                    while (j < ROMSize && test[j] != 0x0)
                    {
                        j++;
                    }
                    byte[] diff = test.Skip(i).Take(j - i).ToArray();
                    diffs.Add(Tuple.Create(i, diff));
                    i = j;
                }
            }
            return diffs;

        }

        public void applyAutopatches()
        {
            //TODO
        }

        public void free(byte[] data)
        {
            for(int i=0; i<accessedData.Count; i++)
            {
                if(accessedData[i].Item3 == data)
                {
                    accessedData.RemoveAt(i);
                    return;
                }
            }
        }
        public void markFree(int offset, int length)
        {
            freeSpace.addSection(offset, length);
        }
        public void markAllocated(int offset, int length)
        {
            freeSpace.removeSection(offset, length);
        }

        public void refreshData(int offset, int length)
        {
            HashSet<EditorModule> needsUpdating = new HashSet<EditorModule>();
            lock (myLock)
            {
                foreach (Tuple<EditorModule, int, byte[]> elem in accessedData)
                {
                    var start = elem.Item2;
                    var end = start + elem.Item3.Length;
                    if (start >= offset && start < offset + length) //break into 3 cases as to only reload the intersection
                    { Array.Copy(ROMData, start, elem.Item3, 0, Math.Min(length, end - start)); }
                    else if (end > offset && end <= offset + length)
                    { Array.Copy(ROMData, offset, elem.Item3, offset-start, end-offset); }
                    else if (start < offset && end > offset + length)
                    { Array.Copy(ROMData, offset, elem.Item3, offset - start, length); }
                    else {continue;}
                    needsUpdating.Add(elem.Item1);
                }
            }
            foreach (EditorModule module in needsUpdating)
            {
                module.updateData();
            }
        }

        /*
        public byte[] getROM()
        {
            return ROMData;
        }*/
        public object getLock()
        {
            return myLock;
        }

        public byte[] requestData(EditorModule owner, int offset, int length)
        {
            byte[] myData = new byte[length];
            lock (myLock)
            {
                Array.Copy(ROMData, offset, myData, 0, length);
            }
            accessedData.Add(Tuple.Create(owner, offset, myData));
            return myData;
        }
        public byte[] requestWrite(int offset, byte[] data)
        {
            byte[] diff = new byte[data.Length];
            lock (myLock)
            {
                for (int i = 0; i < data.Length; i++ )
                {
                    diff[i] = (byte)(ROMData[offset + i] ^ data[i]);
                    ROMData[offset + i] = data[i];

                    //xorDiffsCondensed.Add(Tuple.Create<int, byte[]>(offset, diff));
                }
                xorDiffs.Add(Tuple.Create<int, byte[]>(offset, diff));
                //Array.Copy(data, 0, ROMData, offset, data.Length);
                //myAllocation.addRange(offset, data.Length);
                if(autoSaveToolStripMenuItem.Selected)
                {
                    /*FileStream ROMStream = File.OpenWrite(ROMPath);
                    ROMStream.Write(ROMData, offset, data.Length);
                    Console.Out.WriteLine("Saved ROM.");
                    ROMStream.Close();*/
                    saveToMetadata();
                }
                else
                {
                    ROMChanged = true;
                }
                refreshData(offset, data.Length);
            }
            return diff;
        }
        public Pointer requestFreeSpace(int length)
        {
            ReadOnlyCollection<Tuple<int, int>> space = freeSpace.getList();
            List<Tuple<int, int>> space_mutable = new List<Tuple<int, int>>(space);
            space_mutable.Sort(delegate(Tuple<int, int> first, Tuple<int, int> second)
            {
                int firstLen = first.Item2 - first.Item1;
                int secondLen = second.Item2 - second.Item1;
                if (firstLen == secondLen)
                { return 0; }
                else if (firstLen > secondLen)
                { return 1; }
                else
                { return -1; }
            }); //sort smallest to largest by length
            for(int i=0; i<space_mutable.Count; i++)
            {
                int len = space_mutable[i].Item2 - space_mutable[i].Item1;
                if(len > length)
                {
                    Pointer start = new Pointer(space_mutable[i].Item1, 0x08000000);
                    this.markAllocated(space_mutable[i].Item1, length);
                    return start;
                }
            }
            //expand ROM
            Pointer ret = new Pointer(ROMSize);
            ROMSize += length;
            return ret;
        }
        public Pointer requestWriteToFreeSpace(byte[] data)
        {
            Pointer loc = requestFreeSpace(data.Length);
            requestWrite(loc.value, data);
            return loc;
        }
        /*private void reloadData(Tuple<EditorModule, int, byte[]> Data)
        {
            Array.Copy(ROMData, Data.Item2, Data.Item3, 0, Data.Item3.Length); //locked from refreshData
        }*/

        private void openModule(EditorModule opened)
        {
            opened.uponOpen();
            myModules.Add(opened);
            this.openEditorList.Items.Add(opened.ToString()); //add name of the object
            this.openEditorList.ClearSelected();
            opened.Show();
        }
        public void closeModule(EditorModule closed)
        {
            freeAllFrom(closed);
            for (int i = 0; i < myModules.Count; i++ )
            {
                if (myModules[i]==closed)
                {
                    myModules.RemoveAt(i);
                    this.openEditorList.Items.RemoveAt(i);
                    break;
                }
            }
            this.openEditorList.ClearSelected();
        }
        public void closeModule(int index)
        {
            freeAllFrom(myModules[index]);
            myModules.RemoveAt(index);
            this.openEditorList.Items.RemoveAt(index);
            this.openEditorList.ClearSelected();
        }
        public void freeAllFrom(EditorModule target)
        {
            for (int i = 0; i < accessedData.Count; i++)
            {
                if (accessedData[i].Item1 == target)
                {
                    accessedData.RemoveAt(i);
                    i--;
                    continue;
                }
            }
        }

        private void SetUp() //initialize environment now that a rom is opened
        {
            this.HubTabs.Visible = true;
            this.HubTabs.SelectedIndex = 0;

            this.toolStripStatusLabel.Text = this.ROMName;
            this.toolStripStatusLabel.Visible = true;

            this.saveToolStripMenuItem.Enabled = true;
            this.saveAsToolStripMenuItem.Enabled = true;
            this.closeToolStripMenuItem.Enabled = true;
            this.autoSaveToolStripMenuItem.Enabled = true;

            //this.generateDataTable();
        }

        private void SetDown()
        {
            this.HubTabs.Visible = false;

            this.toolStripStatusLabel.Text = null;
            this.toolStripStatusLabel.Visible = false;

            this.saveToolStripMenuItem.Enabled = false;
            this.saveAsToolStripMenuItem.Enabled = false;
            this.closeToolStripMenuItem.Enabled = false;
            this.autoSaveToolStripMenuItem.Enabled = false;

            this.ROMDataTable.DataSource = null;
        }

        private void generateDataTable()
        {
            this.ROMDataTable = new DataGridView();

            /*
            //http://stackoverflow.com/questions/4111308/2-dimensional-integer-array-to-datagridview/4111344#4111344
            var data = new object[2,2]
            {
                {"ROM Name:", ROMName},
                {"ROM Size:", ROMSize}
            };
            var rowCount = data.GetLength(0);
            var rowLength = data.GetLength(1);

            this.ROMDataTable.ColumnCount = data.GetLength(1);

            for (int rowIndex = 0; rowIndex < rowCount; ++rowIndex)
            {
                var row = new DataGridViewRow();

                for(int columnIndex = 0; columnIndex < rowLength; ++columnIndex)
                {
                    row.Cells.Add(new DataGridViewTextBoxCell()
                        {
                            Value = data[rowIndex, columnIndex]
                        });
                }

                this.ROMDataTable.Rows.Add(row);
            } */

            //http://stackoverflow.com/questions/15166132/how-do-i-show-the-contents-of-this-array-using-datagridview/15166348#15166348
            var data = new object[2][];
            data[0] = new object[2]{"ROM Name:", ROMName};
            data[1] = new object[2] {"ROM Size:", ROMSize };

            this.ROMDataTable.DataSource = (from arr in data select new { Property = arr[0], Value = arr[1]});

            this.ROMDataTable.Refresh();
        }


        private bool promptSave()
        {
            //Insert code prompting for save first
            //returns true if the user selected yes or no, but false if they selected cancel
            DialogResult dialogResult = MessageBox.Show("Hub", "Save open ROM?", MessageBoxButtons.YesNoCancel);
            if(dialogResult == DialogResult.Yes)
            {
                this.saveToMetadata();
                return true;
            }
            else if(dialogResult == DialogResult.No)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        private bool promptSaveIfChanged()
        {
            if(this.ROMChanged)
            {
                return this.promptSave();
            }
            else
            {
                return true;
            }
        }

        private void saveToROM()
        {
            FileStream ROMStream = File.OpenWrite(ROMPath);
            ROMStream.Write(ROMData, 0, ROMSize);

            //Now the xor diffs must be invalidated
            xorDiffs.RemoveRange(0, xorDiffs.Count);
            metadata.setMetadataTable("XORD", makeXORTable());
            //xorDiffsCondensed.RemoveRange(0, xorDiffsCondensed.Count);

            Console.Out.WriteLine("Saved to ROM file.");
            ROMStream.Close();
        }

        private void saveToMetadata(object sender=null, EventArgs e=null)
        {
            //FileStream ROMStream = File.OpenWrite(ROMPath);
            //ROMStream.Write(ROMData, 0, ROMSize);
            
            //XORD
            metadata.setMetadataTable("XORD", makeXORTable());
            
            //FREE
            metadata.setMetadataTable("FREE", makeFreespaceTable());

            metadata.writeMetadata();
            Console.Out.WriteLine("Saved to metadata.");
        }
        private byte[][] makeXORTable()
        {
            var condensed = condenseXORDiffs();
            byte[][] XORDiffs_format = new byte[condensed.Count][];
            for (int i = 0; i < condensed.Count; i++)
            {
                var diffs = condensed[i];
                XORDiffs_format[i] = new byte[4+diffs.Item2.Length];
                
                byte[] offs = BitConverter.GetBytes(diffs.Item1); //offset
                for(int j=0; j<4; j++)
                {
                    XORDiffs_format[i][j] = offs[j]; //copy over the offset
                }
                for(int j=0; j<diffs.Item2.Length; j++)
                {
                    XORDiffs_format[i][j+4] = diffs.Item2[j];
                }
            }
            return XORDiffs_format;
        }
        private byte[][] makeFreespaceTable()
        {
            ReadOnlyCollection<Tuple<int, int>> freeSpaceList = this.freeSpace.getList();
            byte[][] freespaceList_format = new byte[freeSpaceList.Count][];
            for (int i = 0; i < freeSpaceList.Count; i++)
            {
                freespaceList_format[i] = new byte[12]; //4 for length, 4 for start, 4 for end
                var len = BitConverter.GetBytes((int)4);
                var start = BitConverter.GetBytes(freeSpaceList[i].Item1);
                var end = BitConverter.GetBytes(freeSpaceList[i].Item2);
                Array.Copy(len, 0, freespaceList_format[i], 0, 4);
                Array.Copy(start, 0, freespaceList_format[i], 4, 4);
                Array.Copy(end, 0, freespaceList_format[i], 8, 4);
            }
            return freespaceList_format;
        }

        private void saveToUPS()
        {
            //TODO

        }

        private void closeROM(object sender, EventArgs e)
        {
            //TODO
            //metadata.writeMetadata();
            
            /*myModules.RemoveRange(0, myModules.Count);
            
            accessedData.RemoveRange(0, accessedData.Count);*/
            
            if(this.promptSaveIfChanged())
            {
                //Free all data; close all modules
                foreach(EditorModule e in myModules)
                {
                    e.Close();
                    e.Dispose();
                }
                this.reinitialize();
                this.SetDown();
            }
        }
        
        private void reinitialize()
        {
             byte[] ROMData = new byte[MAX_SIZE]; //32MB
             MetadataManager metadata;
             string ROMName = null;
             string ROMPath = null;
             bool ROMOpen = false;
             bool ROMChanged = false;
             int ROMSize = 0;
             List<EditorModule> myModules = new List<EditorModule>();
             object myLock = new object();
             List<Tuple<EditorModule, int, byte[]>> accessedData = new List<Tuple<EditorModule,int, byte[]>>();
             List<Tuple<int, byte[]>> xorDiffs = new List<Tuple<int,byte[]>>();
             List<Tuple<int, byte[]>> condensedDiffs = new List<Tuple<int, byte[]>>();
             Ranges freeSpace = new Ranges();
        }

        private void exit(object sender, EventArgs e)
        {
            if(this.promptSaveIfChanged())
            {
                this.Close();
                this.Dispose();
            }
        }

        private void promptSaveIfChanged_void(object sender, FormClosingEventArgs e)
        {
            promptSaveIfChanged();
        }








        //Debug functions
        private void Set_ROMChanged(object sender, EventArgs e)
        {
            ROMChanged = true;
        }

        private void openDebugModule(object sender, EventArgs e)
        {
            openModule(new DebugModule(this));
        }

        private void openFreespaceManager(object sender, EventArgs e)
        {
            openModule(new FreespaceModule(this));
        }

        private void saveToMetadata()
        {

        }

    }
}


//Freespace Rules:
/*
 * If after ROMSize, then it is free.
 * If a module marks it as free, then it is free.
 * Assume everything else is taken.
 */
