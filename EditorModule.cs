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
    //http://stackoverflow.com/questions/1620847/how-can-i-get-visual-studio-2008-windows-forms-designer-to-render-a-form-that-im/17661276#17661276
    [TypeDescriptionProvider(typeof(AbstractDescriptionProvider<EditorModule, Form>))]
    public abstract class EditorModule : Form
    {
        protected Hub owner;

        public EditorModule(Hub owner)
        {
            this.owner = owner;
            this.FormClosed += new FormClosedEventHandler(uponClose);
        }
        public byte[] requestData(int offset, int length)
        {
            return owner.requestData(this, offset, length);
        }
        public Pointer requestPointer(int offset, int hardwareOffset = 0x08000000)
        {
            return new Pointer(owner.requestData(this, offset, 4), hardwareOffset);
        }
        public byte[] requestWrite(int offset, byte[] data) //returns an xor diff
        {
            return owner.requestWrite(offset, data);
        }
        public byte[] writePointer(int offset, Pointer poin) //same
        {
            return owner.requestWrite(offset, poin.underlyingData);
        }
        public void free(byte[] data)
        {
            owner.free(data);
        }
        public void freePointer(Pointer poin)
        {
            owner.free(poin.underlyingData);
        }
        
        public void uponClose(object sender, FormClosedEventArgs e)
        {
            owner.closeModule(this);
            this.Dispose();
        }


        public abstract void uponOpen();
        public abstract void updateData(); //Should update displays based on updated data
        public abstract void writePendingChanges(); //Called when Save and Close is pressed
        public override abstract string ToString(); //Name of the editor for display purposes.
    }


}
