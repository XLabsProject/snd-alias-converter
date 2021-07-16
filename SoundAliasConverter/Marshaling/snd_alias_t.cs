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
        public uint flags;
        public byte masterPriority;
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

        public static snd_alias_t From(SoundAliasConverter.snd_alias_t jsonAlias)
        {
            int fakePtr = 1;

            var cAlias = new snd_alias_t();

            cAlias.aliasName = (jsonAlias.aliasName?.Length ?? 0) > 0 ? ++fakePtr : 0;
            cAlias.subtitle = (jsonAlias.subtitle?.Length ?? 0) > 0 ? ++fakePtr : 0;
            cAlias.secondaryAliasName = (jsonAlias.secondaryAliasName?.Length ?? 0) > 0 ? ++fakePtr : 0;
            cAlias.chainAliasName = (jsonAlias.chainAliasName?.Length ?? 0) > 0 ? ++fakePtr : 0;
            cAlias.mixerGroup = (jsonAlias.mixerGroup?.Length ?? 0) > 0 ? ++fakePtr : 0;
            cAlias.soundFile = (jsonAlias.soundFile?.Length ?? 0) > 0 ? ++fakePtr : 0;
            cAlias.sequence = jsonAlias.sequence;
            cAlias.volMin = jsonAlias.volMin;
            cAlias.volMax = jsonAlias.volMax;
            cAlias.volModIndex = 0; // Does not exist in iw4 afaik
            cAlias.pitchMin = jsonAlias.pitchMin;
            cAlias.pitchMax = jsonAlias.pitchMax;
            cAlias.distMin = jsonAlias.distMin;
            cAlias.distMax = jsonAlias.distMax;
            cAlias.velocityMin = 0f; // I guess someone forgot this value when they wrote the ZB Json parser ¯\_(ツ)_/¯
            cAlias.flags = jsonAlias.flags;
            cAlias.masterPriority = 0; // Does not exist in iw4
            cAlias.masterPercentage = 0f; // Not part of the JSON spec either, probably should be
            cAlias.slavePercentage = jsonAlias.slavePercentage;
            cAlias.probability = jsonAlias.probability;
            cAlias.lfePercentage = jsonAlias.lfePercentage;
            cAlias.centerPercentage = jsonAlias.centerPercentage;
            cAlias.startDelay = jsonAlias.startDelay;
            cAlias.volumeFalloffCurve = (jsonAlias.aliasName?.Length ?? 0) > 0 ? ++fakePtr : 0;
            cAlias.envelopMin = jsonAlias.envelopMin;
            cAlias.envelopMax = jsonAlias.envelopMax;
            cAlias.envelopPercentage = jsonAlias.envelopPercentage;
            cAlias.speakerMap = (jsonAlias.aliasName?.Length ?? 0) > 0 ? ++fakePtr : 0;

            return cAlias;
        }
    };
}
