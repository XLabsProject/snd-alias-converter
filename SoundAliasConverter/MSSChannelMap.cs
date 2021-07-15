using System.Collections.Generic;

namespace SoundAliasConverter
{
    [System.Serializable]
    public class MSSChannelMap
    {
        public int entryCount
        {
            get
            {
                return speakers.Count;
            }
            set
            {
                speakers = new List<MSSSpeakerLevels>(value);
            }
        }

        public List<MSSSpeakerLevels> speakers = new List<MSSSpeakerLevels>();

        public MSSChannelMap()
        {

        }

        public MSSChannelMap(Marshaling.ChannelMap cStruct)
        {
            entryCount = cStruct.entryCount;
            for (int i = 0; i < cStruct.entryCount; i++)
            {
                var cLevel = cStruct.speakers[i];
                var level = new MSSSpeakerLevels();
                level.levels0 = cLevel.level0;
                level.levels1 = cLevel.level1;
                level.numLevels = cLevel.numLevels;
                level.speaker = cLevel.speaker;

                speakers.Add(level);
            }
        }
    }
}