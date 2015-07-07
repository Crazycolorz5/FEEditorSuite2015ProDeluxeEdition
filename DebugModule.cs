using System;
using System.Collections.Generic;
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
    public partial class DebugModule : EditorModule//Form
    {
        private List<Tuple<int,byte[]>> accessedData;

        public DebugModule(Hub owner) : base(owner)
        {
            accessedData = new List<Tuple<int, byte[]>>();
            InitializeComponent();
        }

        public override void updateData()
        { 
            display.Text = makeText();
        }
        private string makeText()
        {
            string acc = "";
            foreach(Tuple<int,byte[]> data in accessedData)
            {
                acc += "offset: " + HexTools.intToOffset(data.Item1) + "\r\ndata: \r\n" + 
                    HexTools.dataToPrettyString(data.Item2) + "\r\n";
            }
            return acc;
        }

        private void getData(object sender, EventArgs e)
        {
            string offset_str = this.dataOffset.Text;
            string length_str = this.dataLength.Text;
            int offset = HexTools.hexToInt(offset_str);
            byte[] data = this.requestData(offset,HexTools.hexToInt(length_str));
            accessedData.Add(Tuple.Create(offset, data));
            display.Text += "offset: " + HexTools.intToHex(offset) + "\r\ndata: \r\n" +
                    HexTools.dataToPrettyString(data) + "\r\n";
        }

        private void writeData(object sender, EventArgs e)
        {
            string offset_str = this.writeOffset.Text;
            string data_str = this.writeHex.Text;
            byte[] data = HexTools.spacedHexToData(data_str);
            this.requestWrite(HexTools.hexToInt(offset_str), data);
        }

        public override void writePendingChanges()
        { }
        public override void uponOpen()
        { }

        public override string ToString()
        {
            return "Debug Module";
        }
    }
}