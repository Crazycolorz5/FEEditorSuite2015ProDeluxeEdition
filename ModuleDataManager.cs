using System;
using System.Collections.Generic;
using SpaceManager;

namespace FE_Editor_Suite
{
    public class ModuleDataManager
    {
        /*
        private Hub myHub;
        private List<Tuple<int,byte[]>> myData; //Tuple<offset, data>
        //Possibly needs to be ref byte[] ??
        private Ranges myAllocation;

        public ModuleDataManager(Hub owner)
        {
            this.myHub = owner;
        }
        public void requestWrite(int offset, byte[] data)
        {
            lock (myHub.getLock())
            {
                byte[] ROMData = myHub.getROM();
                Array.Copy(data, 0, ROMData, offset, data.Length);
                myHub.refreshData(offset, data.Length);
            }
        }
        public byte[] requestData(int offset, int length)
        {
            byte[] myData = new byte[length];
            lock (myHub.getLock())
            {
                byte[] ROMData = myHub.getROM();
                Array.Copy(ROMData, offset, myData, 0, length);
            }
            this.myData.Add(Tuple.Create(offset, myData));
            return myData;

        }
        public void updateData(int offset, int length)
        {
            if(myAllocation.intersects(offset, length))
            {
                foreach(Tuple<int, byte[]> elem in myData)
                {
                    var start = elem.Item1;
                    var end = start+elem.Item2.Length;
                    if ((start>offset && start < offset + length)||
                        (end>offset && end < offset + length)||
                        (start<offset && end > offset + length))
                    {
                        reloadData(elem);
                    }
                }
            }
        }
        private void reloadData(Tuple<int, byte[]> Data)
        {
            lock (myHub.getLock())
            {
                byte[] ROMData = myHub.getROM();
                Array.Copy(ROMData, Data.Item1, Data.Item2, 0, Data.Item2.Length);
            }
        }
        */
        //Depriciated/unused
    }
}
