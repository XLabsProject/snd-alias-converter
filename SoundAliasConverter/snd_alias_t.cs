using System.Collections.Generic;

namespace SoundAliasConverter
{
    [System.Serializable]
    public class snd_alias_t
    {
        public string aliasName;
        public float centerPercentage;
        public string chainAliasName;
        public float distMax;
        public float distMin;
        public float envelopMax;
        public float envelopMin;
        public float envelopPercentage;
        public int flags;
        public float lfePercentage;
        public string mixerGroup;
        public float pitchMax;
        public float pitchMin;
        public float probability;
        public string secondaryAliasName;
        public int sequence;
        public float slavePercentage;
        public string soundFile;
        public SpeakerMap speakerMap;
        public float startDelay;
        public string subtitle;
        public int type;
        public float volMax;
        public float volMin;
        public string volumeFalloffCurve;

        public snd_alias_t()
        {

        }

        public snd_alias_t(Marshaling.snd_alias_t cStruct)
        {
            centerPercentage = cStruct.centerPercentage;
            distMax = cStruct.distMax;
            distMin = cStruct.distMin;
            envelopMax = cStruct.envelopMax;
            envelopMin = cStruct.envelopMin;
            envelopPercentage = cStruct.envelopPercentage;
            flags = cStruct.flags;
            lfePercentage = cStruct.lfePercentage;
            pitchMax = cStruct.pitchMax;
            pitchMin = cStruct.pitchMin;
            probability = cStruct.probability;
            sequence = cStruct.sequence;
            slavePercentage = cStruct.slavePercentage;
            startDelay = cStruct.startDelay;
            volMax = cStruct.volMax;
            volMin = cStruct.volMin;
        }
    }
}