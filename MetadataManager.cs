using System;
using System.Text;
using System.IO;
using System.Collections.Generic;
using System.Linq;

namespace FE_Editor_Suite
{
    public class MetadataManager
    {
        public const int IDENTIFIER_LENGTH = 4;
        public const int TABLE_INFO_SIZE = IDENTIFIER_LENGTH + 0xC;

        private FileStream metadataStream;
        private string metadataPath;
        private Metadata mainTable;
        private Dictionary<string, Metadata> tables;
        //private List<Metadata> myData;
        //List<MetadataTable> tables; //maybe in the works for more efficient refactoring
        
        public MetadataManager(string filePath)
        {
            this.metadataPath = filePath;
            if (!File.Exists(filePath))
            {
                metadataStream = File.Open(filePath, FileMode.CreateNew);
                initializeMetadata(metadataStream);
            }
            else
            { metadataStream = File.Open(filePath, FileMode.Open); }

            tables = new Dictionary<string, Metadata>();
            //myData = new List<Metadata>();

            mainTable = readMetadataTable(metadataStream, 0);//new Metadata();
            //mainTable.info = readMetadataTableInfo(metadataStream, 0);

            int numTables = mainTable.info.numEntries;
            int offs = mainTable.info.tableOffset.value;

            //int offs = 4;
            for (int i = 0; i < numTables; i++)
            {
                offs += 4; //ignore length data; constant length
                Pointer tableOffs = new Pointer(metadataStream.ReadData(offs,4)); //Get the pointer to the data table
                Metadata temp = readMetadataTable(metadataStream, tableOffs.value);
                offs += 4;
                string identifier = metadataStream.ReadASCII(tableOffs.value, IDENTIFIER_LENGTH); //read the identifier of the data table
                tables.Add(identifier, temp);
                //myData.Add(temp);
            }
            metadataStream.Close();

        }
        
        private static void initializeMetadata(FileStream metadataStream)
        {
            MetadataTableInfo mainTable = new MetadataTableInfo();
            mainTable.identifier = "MAIN";
            mainTable.totalLength = 0x0;
            mainTable.numEntries = 0x0;
            mainTable.tableOffset = new Pointer(0x10);

            writeMetadataTableInfo(metadataStream, mainTable, 0);
            metadataStream.Flush();

        }
        /*public byte[] getAt(int offset, int length)
        {
            //Returns [offset, offset+length)
            
        }
        public byte[] getDereferenceOf(int offset, int length)
        {
            //Returns [*offset, *offset+length)
            
        }*/
        private static Tuple<int, byte[]> readMetadataEntry(FileStream metadataStream, int offset)
        {
            //Returns [*(offset+4), *(offset+4)+*offset)
            return Tuple.Create(metadataStream.ReadInt(offset),
                metadataStream.ReadData(offset + 4, metadataStream.ReadInt(offset)));
        }
        private static Metadata readMetadataTable(FileStream metadataStream, int offset)
        {
            MetadataTableInfo info = readMetadataTableInfo(metadataStream, offset);
            List<Tuple<int, byte[]>> table = new List<Tuple<int, byte[]>>();
            int offs_acc = info.tableOffset.value;
            for(int i=0; i<info.numEntries; i++)
            {
                table.Add(readMetadataEntry(metadataStream, offs_acc));
                offs_acc += metadataStream.ReadInt(offs_acc) + 4;
                offs_acc = HexTools.align(offs_acc); //word align
            }
            Metadata ret = new Metadata();
            ret.data = table;
            ret.info = info;
            return ret;
        }
        public byte[][] getMetadataTable(string identifier)
        {
            if(tables.ContainsKey(identifier))
            {
                Metadata raw = tables[identifier];
                byte[][] ret = new byte[raw.info.numEntries][];
                for (int i = 0; i < raw.info.numEntries; i++ )
                {
                    ret[i] = raw.data[i].Item2;
                }
                return ret; 
            }
            else
            { return null; }
        }
        public void setMetadataTable(string identifier, byte[][] table)
        {
            Metadata toAdd = new Metadata();
            toAdd.info = new MetadataTableInfo();
            toAdd.data = new List<Tuple<int, byte[]>>();

            toAdd.info.identifier = identifier;
            toAdd.info.numEntries = table.Length;
            toAdd.info.tableOffset = new Pointer(0); //Placeholder; determined upon write

            int len = 0;
            foreach(byte[] entry in table)
            {
                len += 4;
                int entry_len = entry.Length;
                len += entry_len;
                toAdd.data.Add(Tuple.Create(entry_len, entry));

            }

            toAdd.info.totalLength = len;


            if(tables.ContainsKey(identifier))
            {
                tables[identifier] = toAdd;
                quickWrite(identifier);
            }
            else
            {
                tables[identifier] = toAdd;
                writeMetadata();
            }
            //placeholder until metadata is written
            //myData.Add(toAdd);
        }
        public void setMetadataTable(string identifier, Metadata meta)
        {
            if (tables.ContainsKey(identifier))
            {
                tables[identifier] = meta;
                quickWrite(identifier);
            }
            else
            {
                tables[identifier] = meta;
                writeMetadata();
            }
        }
        public bool hasTable(string iden)
        {
            return tables.ContainsKey(iden);
        }
        public void addTableEntry(string identifier, byte[] data)
        {
            if(tables.ContainsKey(identifier))
            {
                tables[identifier].data.Add(Tuple.Create(data.Length, data));
                quickWrite(identifier);
            }
            else
            {
                byte[][] table = {data};
                setMetadataTable(identifier, table);
            }
            
        }        
        public void dropTable(string identifier)
        {
            tables.Remove(identifier);
            writeMetadata(); //TODO -- only rewrite main
        }

        public void writeMetadata()
        {
            //metadataStream.Seek(0, SeekOrigin.Begin);

            //TODO
            //Load all tables into RAM, erase the file, rewrite all the tables.
            mainTable.info.numEntries = tables.Count;

            metadataStream = File.Open(metadataPath, FileMode.Open);
            
            MetadataTableInfo[] tableInfo = new MetadataTableInfo[tables.Count];
            var myData = new List<Metadata>();
            foreach(var table in tables)
            {
                myData.Add(table.Value);
            }

            for(int i=0; i<tables.Count; i++)
            {
                tableInfo[i] = myData[i].info;
            }
            int [] tableInfoOffsets = new int[myData.Count];

            int offs_acc = TABLE_INFO_SIZE;

            for(int i=0; i<myData.Count; i++)
            {
                tableInfoOffsets[i] = offs_acc;
                MetadataTableInfo curTable;
                curTable = myData[i].info;
                offs_acc += TABLE_INFO_SIZE;
            }

            mainTable.info.tableOffset = new Pointer(offs_acc); //start of main data
            //mainTable.data = new List<Tuple<int, byte[]>>();

            //now write the main table
            for (int i = 0; i < myData.Count; i++)
            {
                metadataStream.WriteInt(4, offs_acc);
                offs_acc += 4;
                metadataStream.WriteInt(tableInfoOffsets[i], offs_acc);
                offs_acc += 4;
                //mainTable.data.Add(Tuple.Create(4, BitConverter.GetBytes(tableInfoOffsets[i])));
            }

            //now write all the other tables' data
            for (int i=0; i<myData.Count; i++)
            {
                tableInfo[i].tableOffset = new Pointer(offs_acc);
                for(int j=0; j<myData[i].data.Count; j++)
                {
                    int len = myData[i].data[j].Item1;
                    metadataStream.WriteInt(len, offs_acc); //length
                    offs_acc += 4;
                    metadataStream.Seek(offs_acc, SeekOrigin.Begin);
                    metadataStream.Write(myData[i].data[j].Item2, 0, len);
                    offs_acc += len;
                    int align_diff = HexTools.align(offs_acc) - offs_acc;
                    if(align_diff!=0)
                    {
                        byte[] alignment = new byte[align_diff];
                        Array.Clear(alignment, 0, align_diff);
                        metadataStream.Write(alignment, 0, align_diff);
                    }
                    offs_acc += align_diff;
                }
            }
            metadataStream.SetLength(offs_acc);

            //now go back to the top and write all the table data
            writeMetadataTableInfo(metadataStream, mainTable.info, 0);
            offs_acc = TABLE_INFO_SIZE;

            for (int i = 0; i < myData.Count; i++)
            {
                writeMetadataTableInfo(metadataStream, tableInfo[i], offs_acc);
                offs_acc += TABLE_INFO_SIZE;
            }

            mainTable = readMetadataTable(metadataStream, 0);

            metadataStream.Flush();
            metadataStream.Close();

            //And we're done.


            //All headers, then all data
            //TableData1, TableData2, TableData3, . . .
            //length, dataaaaaaaaaaaa; length, dataaaaaaaaaaaaa, . . .
        }
        /*private void rewriteMain()
        {
            //Note!! The new main must take the same or less space than the previous.
            //Rewrites from tables;
            for(var table in tables)
            {


            }

        }*/

        private void quickWrite(string iden)
        {
            //precondition that iden is in tables
            metadataStream = File.Open(metadataPath, FileMode.Open);
            Metadata updated = tables[iden];
            List<Tuple<int, byte[]>> tables_raw = mainTable.data;
            //string current;
            Pointer? ret = getTableOffset(iden);
            if(!ret.HasValue)
            {
                metadataStream.Close();
                writeMetadata();
                return;
            }
            Pointer tableOffset = ret.Value;
            MetadataTableInfo cur_info = readMetadataTableInfo(metadataStream, tableOffset.value);
            if(updated.info.totalLength <= cur_info.totalLength)
            {
                //free to write over
                writeMetadataTableInfo(metadataStream, updated.info, tableOffset.value);
                metadataStream.Seek(updated.info.tableOffset.value, SeekOrigin.Begin);
            }
            else
            {
                //write at the end
                updated.info.tableOffset = new Pointer((int)metadataStream.Length);
                writeMetadataTableInfo(metadataStream, updated.info, tableOffset.value);
                metadataStream.Seek(0, SeekOrigin.End);
            }
            foreach (var entry in updated.data)
            {
                var len = BitConverter.GetBytes((int)entry.Item1);
                metadataStream.Write(len, 0, 4);
                metadataStream.Write(entry.Item2, 0, entry.Item2.Length);
            }
            metadataStream.Close();
        }
        private Pointer? getTableOffset(string iden)
        {
            List<Tuple<int, byte[]>> tables_raw = mainTable.data;
            int i;
            string current;
            for (i = 0; i < tables_raw.Count; i++)
            {
                Pointer tableOffs = new Pointer(tables_raw[i].Item2);
                current = metadataStream.ReadASCII(tableOffs.value, IDENTIFIER_LENGTH);
                if (current == iden)
                    return tableOffs;
            }
            return null;
        }
        private static MetadataTableInfo readMetadataTableInfo(FileStream metadataStream, int offset)
        {
            MetadataTableInfo info = new MetadataTableInfo();

            info.identifier = metadataStream.ReadASCII(offset, IDENTIFIER_LENGTH);
            info.tableOffset = new Pointer(metadataStream.ReadData(offset + IDENTIFIER_LENGTH, 4));
            info.numEntries = metadataStream.ReadInt(offset + 4 + IDENTIFIER_LENGTH);
            info.totalLength = metadataStream.ReadInt(offset + 8 + IDENTIFIER_LENGTH);
            
            return info;
        }
        private static void writeMetadataTableInfo(FileStream metadataStream, MetadataTableInfo info, int offset)
        {
            metadataStream.WriteASCII(info.identifier, offset);
            metadataStream.WriteInt(info.tableOffset.value, offset + IDENTIFIER_LENGTH);
            metadataStream.WriteInt(info.numEntries, offset + 4 + IDENTIFIER_LENGTH);
            metadataStream.WriteInt(info.totalLength, offset + 8 + IDENTIFIER_LENGTH);
        }
    }
    
    public struct MetadataTableInfo
    {
        public string identifier;
        public Pointer tableOffset;
        public int numEntries;
        public int totalLength;
    }
    public struct Metadata
    {
        public MetadataTableInfo info;
        public List<Tuple<int, byte[]>> data;
    }

    public static class ExtensionMethods
    {
        public static int ReadInt(this Stream s_in, int offsetInStream)
        {
            byte[] buff = new byte[4];
            s_in.Seek(offsetInStream, SeekOrigin.Begin);
            s_in.Read(buff, 0, 4);
            return BitConverter.ToInt32(buff, 0);
        }
        public static byte[] ReadData(this Stream s_in, int offsetInStream, int length)
        {
            byte[] buff = new byte[length];
            s_in.Seek(offsetInStream, SeekOrigin.Begin);
            s_in.Read(buff, 0, length);
            return buff;
        }
        public static string ReadASCII(this Stream s_in, int offsetInStream, int length)
        {
            ASCIIEncoding ascii = new ASCIIEncoding();
            return ascii.GetString(s_in.ReadData(offsetInStream, length));
        }
        public static void WriteASCII(this Stream s_in, string toWrite, int offsetInStream)
        {
            ASCIIEncoding ascii = new ASCIIEncoding();
            byte[] buff = ascii.GetBytes(toWrite);
            s_in.Seek(offsetInStream, SeekOrigin.Begin);
            s_in.Write(buff, 0, buff.Length);
        }
        public static void WriteInt(this Stream s_in, int toWrite, int offsetInStream)
        {
            byte[] buff = BitConverter.GetBytes(toWrite); 
            s_in.Seek(offsetInStream, SeekOrigin.Begin);
            s_in.Write(buff, 0, 4);
        }
    }
}

        /*
         4: Num entries
         
         TableData1, Tabledata2, etc.
         
         * 
        */

/*
metadataTable format is:
        
4(IDENTIFIER_LENGTH): 4-byte Identifier
4: Pointer-To-Table
4: Number-of-Entries
4: Total-Length(in bytes)-Of-Table
        
. . .
        
4: 
        
*/