namespace SoundAliasConverter.Marshaling
{
    public struct ChannelMap
    {
        public int entryCount; // how many entries are used

        [System.Runtime.InteropServices.MarshalAs(System.Runtime.InteropServices.UnmanagedType.ByValArray, SizeConst = 6, ArraySubType = System.Runtime.InteropServices.UnmanagedType.Struct)]
        public SpeakerLevels[] speakers;

        public static ChannelMap From(SoundAliasConverter.MSSChannelMap map)
        {
            var cMap = new ChannelMap();
            cMap.entryCount = map.entryCount;
            cMap.speakers = new SpeakerLevels[6];
            for (int i = 0; i < cMap.speakers.Length; i++)
            {
                if (i < map.speakers.Count)
                {
                    var speaker = map.speakers[i];

                    var cSpeaker = new SpeakerLevels();
                    cSpeaker.level0 = speaker.levels0;
                    cSpeaker.level1 = speaker.levels1;
                    cSpeaker.numLevels = speaker.numLevels;
                    cSpeaker.speaker = speaker.speaker;

                    cMap.speakers[i] = cSpeaker;
                }
            }

            return cMap;

        }
    }
}
