using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SoundAliasConverter
{
    enum IW4SoundFlags
    {
        LOOPING = 0b_0000_0000_0000_0000_0000_0000_0000_0001,
        IS_MASTER = 0b_0000_0000_0000_0000_0000_0000_0000_0010,
        IS_SLAVE = 0b_0000_0000_0000_0000_0000_0000_0000_0100,
        FULL_DRY = 0b_0000_0000_0000_0000_0000_0000_0000_1000,
        NO_WET_LEVEL = 0b_0000_0000_0000_0000_0000_0000_0001_0000,
        UNK1 = 0b_0000_0000_0000_0000_0000_0000_0010_0000,
        UNK2 = 0b_0000_0000_0000_0000_0000_0000_0100_0000,
        TYPE = 0b_0000_0000_0000_0000_0000_0001_1000_0000,
        CHANNEL = 0b_0000_0000_0000_0000_0111_1110_0000_0000
    };
}
