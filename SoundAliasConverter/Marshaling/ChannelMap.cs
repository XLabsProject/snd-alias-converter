namespace SoundAliasConverter.Marshaling
{
    public struct ChannelMap
    {
        public int entryCount; // how many entries are used

        [System.Runtime.InteropServices.MarshalAs(System.Runtime.InteropServices.UnmanagedType.ByValArray, SizeConst = 6, ArraySubType = System.Runtime.InteropServices.UnmanagedType.Struct)]
        public SpeakerLevels[] speakers;
    }
}
