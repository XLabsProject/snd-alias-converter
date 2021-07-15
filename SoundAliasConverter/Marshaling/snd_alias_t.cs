using System.Runtime.InteropServices;

namespace SoundAliasConverter.Marshaling
{
    public struct snd_alias_t
    {
        public int aliasName;
        public int subtitle;
        public int secondaryAliasName;
        public int chainAliasName;
        public int mixerGroup;
        public int soundFile;
        public int sequence;
        public float volMin;
        public float volMax;
        public int volModIndex;
        public float pitchMin;
        public float pitchMax;
        public float distMin;
        public float distMax;
        public float velocityMin;
        public int flags;
        public char masterPriority;
        public float masterPercentage;
        public float slavePercentage;
        public float probability;
        public float lfePercentage;
        public float centerPercentage;
        public int startDelay;
        public int volumeFalloffCurve;
        public float envelopMin;
        public float envelopMax;
        public float envelopPercentage;
        public int speakerMap;
    };
}
