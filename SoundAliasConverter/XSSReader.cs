namespace SoundAliasConverter
{
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
    using System.Runtime.InteropServices;
    using static XSSFormat;

    public static class XSSReader
    {

        static List<object> objectTable = new List<object>();

        public static snd_alias_list_t Read(string fileName)
        {
            objectTable.Clear();
            var aliasList = new snd_alias_list_t();

            using (var fs = new FileStream(fileName, FileMode.Open))
            {
                using (var br = new BinaryReader(fs))
                {
                    br.ReadBytes(12); // Skip 12 bytes, which is the size of a sound alias list struct

                    //  read_single <snd_alias_list_t>;
                    // Unique asset ptr, lost in translation
                    br.ReadBytes(5);
                    objectTable.Add(null);

                    aliasList.aliasName = ReadUniqueCString(br);

                    //  read_single <snd_alias_list_t>;
                    // We read the same data again, it's not important
                    br.ReadBytes(1);
                    objectTable.Add(null);

                    uint count = br.ReadUInt32(); // Discard pointer
                    aliasList.head = new snd_alias_t[count];

                    //  read_array <snd_alias_t>;
                    // We read the headers a bit unlike the way ZT does it, which is fine
                    List<Marshaling.snd_alias_t> cStructs = new List<Marshaling.snd_alias_t>();

                    for (int i = 0; i < aliasList.head.Length; i++)
                    {
                        var cStruct = ReadCStruct<Marshaling.snd_alias_t>(br);
                        cStructs.Add(cStruct);
                    }

                    for (int i = 0; i < aliasList.head.Length; i++)
                    {
                        var cAlias = cStructs[i];
                        var alias = new snd_alias_t(cAlias);

                        if (cAlias.aliasName != 0)
                        {
                            alias.aliasName = ReadUniqueCString(br);
                        }

                        if (cAlias.subtitle != 0)
                        {
                            alias.subtitle = ReadUniqueCString(br);
                        }

                        if (cAlias.secondaryAliasName != 0)
                        {
                            alias.secondaryAliasName = ReadUniqueCString(br);
                        }

                        if (cAlias.chainAliasName != 0)
                        {
                            alias.chainAliasName = ReadUniqueCString(br);
                        }

                        if (cAlias.mixerGroup != 0)
                        {
                            alias.mixerGroup = ReadUniqueCString(br);
                        }

                        if (cAlias.soundFile != 0)
                        {
                            EDumpType dataType = (EDumpType)br.ReadByte();

                            if (dataType == EDumpType.DUMP_TYPE_ARRAY)
                            {
                                uint soundFileCount = br.ReadUInt32();
                                // Should always be 1 anyway
                                if (soundFileCount == 1)
                                {
                                    byte soundType = br.ReadByte();
                                    br.ReadBytes(2);
                                    bool exists = br.ReadBoolean();

                                    br.ReadInt32(); // Skip LoadSnd pointer
                                    br.ReadInt32(); // Skip rest of the union (8 bytes total)
                                    objectTable.Add(null); // Add the "asset" to the object table (no one will ever use it)

                                    string soundName = ReadUniqueCString(br);
                                    if (soundType == 1) // SAT LOADED
                                    {
                                        // nothing to do !
                                    }
                                    else
                                    {
                                        soundName = Path.Combine(soundName, ReadUniqueCString(br));
                                    }

                                    alias.type = soundType;
                                    alias.soundFile = soundName;

                                }
                            }

                        }

                        if (cAlias.volumeFalloffCurve != 0)
                        {
                            alias.volumeFalloffCurve = ReadUniqueCString(br);
                        }

                        if (cAlias.speakerMap != 0)
                        {
                            var cMap = ReadUniqueArray<Marshaling.SpeakerMap>(br)[0];

                            string speakerMapName = ReadUniqueCString(br);

                            alias.speakerMap = new SpeakerMap(cMap);
                            alias.speakerMap.name = speakerMapName;
                        }

                        aliasList.head[i] = alias;
                    }
                }
            }

            return aliasList;
        }

        private static T GetUniqueObject<T>(int index)
        {
            if (index < 0 || index >= objectTable.Count)
            {
                // Throw error here
                throw new Exception($"Wrong type of data referenced in offset ${index}, index is out of bounds (max: {objectTable.Count})");
            }

            if (!(objectTable[index] is T loadedData))
            {
                // Throw error here
                throw new Exception($"Wrong type of data referenced in offset ${index}, expected a {typeof(T).Name} and got {objectTable[index]?.GetType()?.Name ?? "NULL"}");
            }

            return loadedData;
        }

        private static T[] ReadUniqueArray<T>(BinaryReader br)
        {
            var dumpType = (EDumpType)br.ReadByte();

            if (dumpType == EDumpType.DUMP_TYPE_ARRAY)
            {
                // Actual read
                int count = br.ReadInt32();
                var arr = new T[count];
                for (int i = 0; i < count; i++)
                {
                    arr[i] = ReadCStruct<T>(br);
                }

                objectTable.Add(arr);
                return arr;
            }
            else if (dumpType == EDumpType.DUMP_TYPE_OFFSET)
            {
                var index = br.ReadInt32();

                var data = GetUniqueObject<T[]>(index);

                // Skip 0 int32
                br.ReadInt32();

                return data;
            }
            else
            {
                // Unknown type throw error or something
                throw new Exception("Unexpected data type " + dumpType);
            }
        }
        private static string ReadUniqueCString(BinaryReader br)
        {
            var dumpType = (EDumpType)br.ReadByte();

            if (dumpType == EDumpType.DUMP_TYPE_STRING || dumpType == EDumpType.DUMP_TYPE_ASSET)
            {
                var stringExistence = br.ReadByte();

                if (stringExistence == DUMP_EXISTING)
                {
                    var sb = new StringBuilder();
                    char c;
                    while ((c = (char)br.ReadByte()) != 0)
                    {
                        sb.Append(c);
                    }
                    var result = sb.ToString();
                    objectTable.Add(result);
                    return result;
                }
                else if (stringExistence != DUMP_NON_EXISTING)
                {
                    // Unknown type throw error or something
                    throw new Exception();
                }

                // NON_EXISTING means nullptr for the game. So i assume use null here? You could also use "" depending on the asset type.
                return null;
            }
            else if (dumpType == EDumpType.DUMP_TYPE_OFFSET)
            {
                var index = br.ReadInt32();

                var data = GetUniqueObject<string>(index);

                // Skip 0 int32
                br.ReadInt32();

                return data;
            }
            else
            {
                // Unknown type throw error or something
                throw new Exception("Unexpected data type " + dumpType);
            }
        }

        private static T ReadCStruct<T>(BinaryReader reader)
        {
            byte[] bytes = reader.ReadBytes(Marshal.SizeOf(typeof(T)));

            GCHandle handle = GCHandle.Alloc(bytes, GCHandleType.Pinned);
            T theStructure = (T)Marshal.PtrToStructure(handle.AddrOfPinnedObject(), typeof(T));
            handle.Free();

            return theStructure;
        }
    }
}
