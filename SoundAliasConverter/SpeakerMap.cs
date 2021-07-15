using System.Collections.Generic;

namespace SoundAliasConverter
{
    [System.Serializable]
    public class SpeakerMap
    {
        public bool isDefault;
        public string name;
        public List<MSSChannelMap> channelMaps = new List<MSSChannelMap>();

        public SpeakerMap()
        {

        }

        public SpeakerMap(Marshaling.SpeakerMap cStruct)
        {
            this.isDefault = cStruct.isDefault;
            channelMaps.Add(new MSSChannelMap(cStruct.channelMap00));
            channelMaps.Add(new MSSChannelMap(cStruct.channelMap01));
            channelMaps.Add(new MSSChannelMap(cStruct.channelMap10));
            channelMaps.Add(new MSSChannelMap(cStruct.channelMap11));
        }
    }
}