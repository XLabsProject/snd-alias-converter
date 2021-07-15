namespace SoundAliasConverter.Marshaling
{
    struct _AILSOUNDINFO
    {
        public int format;
        public int data_ptr;
        public uint data_len;
        public uint rate;
        public int bits;
        public int channels;
        public uint samples;
        public uint block_size;
        public int initial_ptr;
    };
}
