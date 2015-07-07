using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using FE_Editor_Suite;

namespace Modules
{
    public partial class FreespaceModule : EditorModule
    {
        private bool offsetSort = true;
        public FreespaceModule(Hub owner) : base(owner)
        {
            InitializeComponent();
            this.GotFocus += refreshList_wrapper;
        }

        private void refreshList_wrapper(object sender, EventArgs e)
        {
            refreshList();
        }
        private void refreshList()
        {
            string acc = "";
            ReadOnlyCollection<Tuple<int, int>> ranges = owner.freeRanges;
            List<Tuple<int, int>> ranges_mutable = new List<Tuple<int, int>>(ranges);
            if(offsetSort)
            {
                ranges_mutable.Sort( delegate(Tuple<int, int> first, Tuple<int, int> second) 
                {
                    if (first.Item1 == second.Item1)
                        return 0;
                    else if (first.Item1 < second.Item1)
                        return -1;
                    else
                        return 1;
                } );
            }
            else
            {
                ranges_mutable.Sort(delegate(Tuple<int, int> first, Tuple<int, int> second)
                {
                    int firstLen = first.Item2 - first.Item1;
                    int secondLen = second.Item2 - second.Item1;
                    if (firstLen == secondLen)
                        return 0;
                    else if (firstLen < secondLen)
                        return -1;
                    else
                        return 1;
                });
            }

            foreach(Tuple<int, int> aRange in ranges_mutable)
            {
                acc += "Start: " + HexTools.intToOffset(aRange.Item1) + "  End: " + HexTools.intToOffset(aRange.Item2) + "\r\nLength: " 
                    + HexTools.intToOffset(aRange.Item2-aRange.Item1) + "\r\n\r\n";
            }
            freespaceListing.Text = acc;
        }


        private void useLengthChecked(object sender, System.EventArgs e)
        {
            this.lengthBox.ReadOnly = false;
            this.endBox.ReadOnly = true;
            endBox.ResetText();
        }

        private void useEndChecked(object sender, System.EventArgs e)
        {
            this.lengthBox.ReadOnly = true;
            this.endBox.ReadOnly = false;
            lengthBox.ResetText();
        }

        private void sortByOffset(object sender, EventArgs e)
        {
            offsetSort = true;
            refreshList();
        }

        private void sortByLength(object sender, EventArgs e)
        {
            offsetSort = false;
            refreshList();
        }

        private void markAsFreespace(object sender, EventArgs e)
        {
            if(this.useEnd.Checked)
            {
                int start = HexTools.hexToInt(this.startBox.Text);
                int end = HexTools.hexToInt(this.endBox.Text);
                owner.markFree(start, end - start);
            }
            else
            {
                int start = HexTools.hexToInt(this.startBox.Text);
                int length = HexTools.hexToInt(this.endBox.Text);
                owner.markFree(start, length);
            }
            startBox.ResetText();
            endBox.ResetText();
            lengthBox.ResetText();
            refreshList();
        }

        private void markAsAllocated(object sender, EventArgs e)
        {

            if (this.useEnd.Checked)
            {
                int start = HexTools.hexToInt(this.startBox.Text);
                int end = HexTools.hexToInt(this.endBox.Text);
                owner.markAllocated(start, end - start);
            }
            else
            {
                int start = HexTools.hexToInt(this.startBox.Text);
                int length = HexTools.hexToInt(this.endBox.Text);
                owner.markAllocated(start, length);
            }
            startBox.ResetText();
            endBox.ResetText();
            lengthBox.ResetText();
            refreshList();
        }

        public override string ToString()
        {
            return "FreespaceModule";
        }
        public override void updateData()
        {}
        public override void uponOpen()
        {
            refreshList();
        }
        public override void writePendingChanges()
        {}

    }
}
