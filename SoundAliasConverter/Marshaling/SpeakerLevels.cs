using System.Runtime.InteropServices;

namespace SoundAliasConverter.Marshaling
{
    [StructLayout(LayoutKind.Sequential)]
    public struct SpeakerLevels
    {
        public int speaker;
        public int numLevels;
        public float level0;
        public float level1;
    }
}
