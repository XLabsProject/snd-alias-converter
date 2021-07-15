using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SoundAliasConverter
{
    [System.Serializable]
    public class snd_alias_list_t
    {
        public string aliasName;
        public snd_alias_t[] head;
    }
}
