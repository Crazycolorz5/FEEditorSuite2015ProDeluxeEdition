using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace SpaceManager
{
    public class Ranges
    {
        List<Tuple<int, int>> allocated;

        public Ranges()
        {
            allocated = new List<Tuple<int, int>>();
        }

        public void addPoint(int offset)
        {
            int i = 0;
            while (i < allocated.Count && offset > allocated[i].Item2) //insert it in its sorted position.
            {   //short circuit && prevents it from accessing anything over the size.
                i++;
            }
            allocated.Insert(i, Tuple.Create(offset, offset + 1));
            cleanup(i);
        }
        public void addSection(int offset, int length)
        {
            int i = 0;
            while (i < allocated.Count && offset > allocated[i].Item2)
            {
                i++;
            }
            allocated.Insert(i, Tuple.Create(offset, offset + length));
            cleanup(i);
        }
        public void addRange(int offset, int end)
        {
            addSection(offset, end-offset);
        }
        public void removeRange(int offset, int end)
        {
            int i=0;
            while(i < allocated.Count && offset > allocated[i].Item2)
            {
                i++;
            }
            if (i == allocated.Count)
            { return; }
            else
            {
                if(allocated[i].Item1 > end)
                {
                    return;
                }
                else if (offset > allocated[i].Item1)
                {
                    if (end >= allocated[i].Item2)
                    {
                        var first = allocated[i].Item1;
                        allocated.RemoveAt(i);
                        allocated.Insert(i, Tuple.Create(first, offset));
                        removeRange(offset, end);
                    }
                    else
                    {
                        var first = allocated[i].Item1;
                        var last = allocated[i].Item2;
                        allocated.RemoveAt(i);
                        allocated.Insert(i, Tuple.Create(end, last));
                        allocated.Insert(i, Tuple.Create(first, offset));
                    }
                }
                else
                {
                    if (end >= allocated[i].Item2)
                    {
                        allocated.RemoveAt(i);
                        removeRange(offset, end);
                    }
                    else
                    {
                        var last = allocated[i].Item2;
                        allocated.RemoveAt(i);
                        allocated.Insert(i, Tuple.Create(end, last));
                        //removeRange(offset, end);
                    }
                }
            }
            
            
            
        }
        public void removeSection(int offset, int length)
        {
            removeRange(offset, offset+length);
        }
        
        

        public bool hasPoint(int offset)
        {
            //TODO
            return intersects(offset, offset+1);
        }
        public bool intersects(int offset, int length)
        {
            int i = 0;
            while(i<allocated.Count && offset>allocated[i].Item2)
            { i++; }
            if(i==allocated.Count)
            { return false; }
            else
            { 
                var temp = allocated[i];
                if (offset+length>temp.Item1)
                { return true; }
                else
                { return false; }
            }
        }
        public ReadOnlyCollection<Tuple<int, int>> getList()
        {
            return new ReadOnlyCollection<Tuple<int, int>>(allocated);
        }

        private void cleanup(int index)
        //merges intersecting ranges and whatnot. index is the index of the just inserted element that might intersect with the next element.
        {
            if (index == allocated.Count - 1)//if index is the last index, nothing to cleanup
                return;
            else
            {
                var temp1 = allocated[index];
                var temp2 = allocated[index + 1];
                if (temp1.Item2 >= temp2.Item1)
                {
                    //The ranges intersect
                    if (temp1.Item1 >= temp2.Item1)
                    {
                        //just use temp2
                        allocated.RemoveAt(index);
                        return;
                    }
                    else
                    {
                        var newVal = Tuple.Create(temp1.Item1, temp2.Item2);
                        allocated.RemoveRange(index, 2);
                        allocated.Insert(index, newVal);
                    }
                }
                else
                {
                    return;
                }
            }
        }

    }
}