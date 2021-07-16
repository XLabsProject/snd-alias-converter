using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SoundAliasConverter
{
    public static class XSSFormat
    {
        public enum EDumpType : byte
        {
            DUMP_TYPE_INT = 0,
            DUMP_TYPE_STRING = 1,
            DUMP_TYPE_ASSET = 2,
            DUMP_TYPE_ARRAY = 3,
            DUMP_TYPE_OFFSET = 4,
            DUMP_TYPE_FLOAT = 5,
            DUMP_TYPE_RAW = 6
        }

        public const byte DUMP_NON_EXISTING = 0;
        public const byte DUMP_EXISTING = 1;
    }
}
