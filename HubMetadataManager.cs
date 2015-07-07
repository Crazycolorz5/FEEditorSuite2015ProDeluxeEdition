using System.IO;

namespace FE_Editor_Suite
{
    public class HubMetadataManager : MetadataManager
    {
        FileStream metadataStream;
        public HubMetadataManager(string filePath) : base(filePath)
        {
        }
        
        protected override void initializeMetadata()
        {
            var initializer = new byte[BASE_METADATA_SIZE]();
            for(int i=0; i<BASE_METADATA_SIZE; i++)
            {
                intializer[i] = 0;
            }
            metadata.Write(initializer, 0, BASE_METADATA_SIZE);
        
            byte[] identifier = {0x46, 0x45, 0x49, 0x44, 0x45, 0x46, 0x5F, 0x48, 0x55, 0x42} //"FEIDE_HUB"
            metadata.Write(identifier, 0, 8);
            
            metadata.Flush();
        }
        
        
    }
}
        /*
        [2/9/15, 7:06:10 PM] Crazy Colorz: 8: "FE_IDE__"
        4: 0x00000001 or 0x00 depending on if autopatches have been applied
        [2/9/15, 7:06:25 PM] Crazy Colorz: 4: offset of xor diffs table
        [2/9/15, 7:07:10 PM] Crazy Colorz: 4: offset(in metadata file) of feespace data
        4: number of freespace data entries
        [2/9/15, 7:07:43 PM] Crazy Colorz: xor diffs table looks like:
        4: offset that the diff is for
        4: 0xYYYYYYYY which is the length of the diff
        Y: xor diff
        */