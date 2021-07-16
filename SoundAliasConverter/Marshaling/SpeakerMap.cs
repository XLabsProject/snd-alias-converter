using System.Runtime.InteropServices;

namespace SoundAliasConverter.Marshaling
{
    public struct SpeakerMap
    {
        [System.Runtime.InteropServices.MarshalAs(System.Runtime.InteropServices.UnmanagedType.I1)]
        public bool isDefault;
        public byte _pad1;
        public byte _pad2;
        public byte _pad3;
        public int ptr;
        public ChannelMap channelMap00;
        public ChannelMap channelMap01;
        public ChannelMap channelMap10;
        public ChannelMap channelMap11;

        public static SpeakerMap From(SoundAliasConverter.SpeakerMap map)
        {
            var cMap = new SpeakerMap();
            cMap.isDefault = map.isDefault;
            cMap.channelMap00 = ChannelMap.From(map.channelMaps[0]);
            cMap.channelMap01 = ChannelMap.From(map.channelMaps[1]);
            cMap.channelMap10 = ChannelMap.From(map.channelMaps[2]);
            cMap.channelMap11 = ChannelMap.From(map.channelMaps[3]);

            return cMap;
        }
    }
}
