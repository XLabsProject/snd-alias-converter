using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace SoundAliasConverter
{
    public class XSSWriter
    {
        public static byte[] Write(snd_alias_list_t aliases)
        {
            using (var ms = new MemoryStream())
            {
                using (var bw = new BinaryWriter(ms))
                {
                    bw.Write(new byte[17]);

                    WriteUnique(bw, aliases.aliasName);

                    var cAliases = new Marshaling.snd_alias_t[aliases.head.Length];

                    for (int i = 0; i < aliases.head.Length; i++)
                    {
                        cAliases[i] = Marshaling.snd_alias_t.From(aliases.head[i]);
                    }

                    WriteUniqueArray(bw, cAliases);

                    for (int i = 0; i < aliases.head.Length; i++)
                    {
                        var alias = aliases.head[i];
                        var cAlias = cAliases[i];

                        uint soundType = cAlias.flags & (uint)IW4SoundFlags.TYPE;

                        if (cAlias.aliasName != 0)
                        {
                            WriteUnique(bw, alias.aliasName);
                        }

                        if (cAlias.subtitle != 0)
                        {
                            WriteUnique(bw, alias.subtitle);
                        }

                        if (cAlias.secondaryAliasName != 0)
                        {
                            WriteUnique(bw, alias.secondaryAliasName);
                        }

                        if (cAlias.chainAliasName != 0)
                        {
                            WriteUnique(bw, alias.chainAliasName);
                        }

                        if (cAlias.mixerGroup != 0)
                        {
                            WriteUnique(bw, alias.mixerGroup);
                        }

                        if (cAlias.soundFile != 0)
                        {
                            bw.Write((byte)XSSFormat.EDumpType.DUMP_TYPE_ARRAY);
                            bw.Write((uint)1);
                            bw.Write((byte)soundType);
                            bw.Write(new byte[2]);
                            bw.Write(true); // sound exists
                            bw.Write(ulong.MaxValue); // 8 bytes of hot fuming garbage

                            if (soundType == 0x1) // SAT_LOADED
                            {
                                WriteUnique(bw, alias.soundFile, XSSFormat.EDumpType.DUMP_TYPE_ASSET);

                            }
                            else
                            {
                                var dir = Path.GetDirectoryName(alias.soundFile);
                                var file = Path.GetFileName(alias.soundFile);
                                WriteUnique(bw, dir, XSSFormat.EDumpType.DUMP_TYPE_STRING);
                                WriteUnique(bw, file, XSSFormat.EDumpType.DUMP_TYPE_STRING);
                            }
                        }

                        if (cAlias.volumeFalloffCurve != 0)
                        {
                            WriteUnique(bw, alias.volumeFalloffCurve);
                        }

                        if (cAlias.speakerMap != 0)
                        {
                            WriteUniqueArray(bw, new Marshaling.SpeakerMap[] { Marshaling.SpeakerMap.From(alias.speakerMap) });
                            WriteUnique(bw, alias.speakerMap.name);
                        }
                    }

                }


                return ms.ToArray();
            }
        }

        private static void WriteUnique(BinaryWriter bw, string str, XSSFormat.EDumpType dumpType = XSSFormat.EDumpType.DUMP_TYPE_STRING)
        {
            // "Unicity" is a big word here. We're actually never going to use the object table, and we're gonna dump the data always and the offset never.
            // This is a hit on optimization, although not a big one. Zonetool wise it should probably still work the same

            if (str == null)
            {
                // Do nothing
                bw.Write(new byte[] { (byte)dumpType , XSSFormat.DUMP_NON_EXISTING});
            }
            else
            {
                bw.Write(new byte[] { (byte)dumpType, XSSFormat.DUMP_EXISTING });

                foreach (var chr in str)
                {
                    bw.Write(chr);
                }
            }

            bw.Write('\0'); // Null termination for strings
        }

        private static void WriteUniqueArray<T>(BinaryWriter bw, T[] arr)
        {
            bw.Write((byte)XSSFormat.EDumpType.DUMP_TYPE_ARRAY);

            if (arr == null || arr.Length == 0)
            {
                bw.Write((uint)0);

            }
            else
            {
                bw.Write((uint)arr.Length);

                for (int i = 0; i < arr.Length; i++)
                {
                    var size = Marshal.SizeOf(arr[i]);

                    IntPtr ptr = Marshal.AllocHGlobal(size);
                    Marshal.StructureToPtr(arr[i], ptr, true);

                    byte[] buff = new byte[size];
                    Marshal.Copy(ptr, buff, 0, size);

                    Marshal.FreeHGlobal(ptr);

                    bw.Write(buff);
                }
            }
        }
    }
}
