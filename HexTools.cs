using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FE_Editor_Suite
{
    public class HexTools {

        public static byte hexToByte(string oneByte) //for use for 2-length strings only
        {
            byte HONibble = fromHexDigit(oneByte[0]);
            byte LONibble = fromHexDigit(oneByte[1]);
            LONibble += (byte)(HONibble << 4);
            return LONibble;
        }
        public static int hexToInt(string number) //for up to 8-length strings(32 bit)
        {
            int value = 0;
            for (int i = 0; i < number.Length; i++ )
            { value = (value << 4) + fromHexDigit(number[i]); }
            return value;
        }
        private static byte fromHexDigit(char oneDigit)
        {
            if(oneDigit>='0'&&oneDigit<='9') {return (byte)(oneDigit - '0');}
            else {return (byte)(oneDigit + 10 - 'A');}
        }
        private static string toHexDigits(byte oneByte)
        {
            string acc = "";
            int HONibble = oneByte >> 4;
            int LONibble = 15 & oneByte;
            //HO goes first
            if (HONibble <= 9)
            { acc += (char)(HONibble + '0'); }
            else
            { acc += (char)(HONibble - 10 + 'A'); }

            if (LONibble <= 9)
            { acc += (char)(LONibble + '0');}
            else
            { acc += (char)(LONibble - 10 + 'A'); }
            return acc;
        }

        public static string dataToSpacedHex(byte[] data)
        {
            string acc = "";
            foreach(byte hex in data)
            {
                acc += toHexDigits(hex) + ' ';
            }
            return acc;
        }
        public static string intToHex(int number)
        {
            string acc="";
            for (int i = 0; i < 4; i++)
            {
                if(number==0&&acc.Length>0)
                { break; }
                acc = toHexDigits((byte)(number)) + acc; 
                number >>= 8;
            }
            return acc;
        }
        public static string intToOffset(int number)
        {
            return "0x" + intToHex(number);
        }
        public static string dataToPrettyString(byte[] data)
        {
            string acc = "";
            for (int i=0; i<data.Length; i++)
            {
                byte hex = data[i];
                acc += toHexDigits(hex) + ' ';
                if(i%16 == 15)
                { acc += "\r\n"; }
            }
            return acc;
        }

        public static byte[] spacedHexToData(string spaced_hex)
        {
            //spaced hex looks like 2E 00 00 EA 24 FF AE 51 69 9A A2 21 3D 84 82 0A
            char[] spaceDelim = { ' ' };
            string[] doop = spaced_hex.Trim().Split(spaceDelim);//nod to cam

            byte[] data = new byte[doop.Length];
            for(int i=0; i<doop.Length; i++)
            {
                data[i] = hexToByte(doop[i]);
            }
            return data;
        }
        public static byte[] hexToData(string hex)
        {
            //hex looks like 2E0000EA24FFAE51699AA2213D84820A
            //If there is an even number of characters, the last character gets ignored.
            string[] splitHex = new string[hex.Length / 2];
            for(int i=0; i<hex.Length; i+=2)
            {
                splitHex[i / 2] = hex.Substring(i, 2);
            }

            char[] spaceDelim = { ' ' };

            byte[] data = new byte[splitHex.Length];
            for(int i=0; i<splitHex.Length; i++)
            {
                data[i] = hexToByte(splitHex[i]);
            }
            return data;
        }
        public static int align(int offset)
        {
            return ((offset + 3) >> 2) << 2;
        }
    }

    public struct Pointer
    {
        public int value
        {
            get
            {
                return (underlyingData[0] + (underlyingData[1] << 8) +
                    (underlyingData[2] << 16) + (underlyingData[3] << 24)) -
                    hardwarePointer;
            }
        }

        public byte[] underlyingData;
        public int hardwarePointer;
        public Pointer(byte[] data, int offs = 0)
        {
            underlyingData = data;
            hardwarePointer = offs;
        }
        public Pointer(int reference, int offs = 0)
        {
            underlyingData = BitConverter.GetBytes(reference);
            hardwarePointer = offs;
        }
    }
}
