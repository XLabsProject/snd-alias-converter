using System;
using System.Collections.Generic;
using System.IO;

namespace SoundAliasConverter
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.WriteLine("========= XSS<>JSON sound aliases converter =========\nZoneTool optimal compatibility version: 1.0.205\n");

            Console.ForegroundColor = ConsoleColor.White;

            if (args.Length == 0)
            {
                Console.WriteLine("No sounds given! Please drag & drop XSS sounds on me to start converting to JSON.");
            }

            foreach (var arg in args)
            {
                Console.WriteLine($"Working on {arg}");
                if (File.Exists(arg))
                {
                    byte[] dataToWrite;

                    try
                    {
                        var outputFilename = Path.Combine(Path.GetDirectoryName(arg), Path.GetFileNameWithoutExtension(arg));
                        switch (Path.GetExtension(arg).ToUpper())
                        {
                            case ".XSS":
                                dataToWrite = System.Text.Encoding.UTF8.GetBytes(Newtonsoft.Json.JsonConvert.SerializeObject(XSSReader.Read(arg), Newtonsoft.Json.Formatting.Indented));
                                outputFilename = Path.GetFileNameWithoutExtension(arg); // REMOVE
                                outputFilename += ".json";
                                break;

                            case "":
                            case ".JSON":
                                var aliases = Newtonsoft.Json.JsonConvert.DeserializeObject<snd_alias_list_t>(File.ReadAllText(arg));
                                dataToWrite = XSSWriter.Write(aliases);
                                outputFilename += ".xss";
                                break;

                            default:
                                throw new Exception($"Unsupported format/extension: {Path.GetExtension(arg)}");
                        }

                        File.WriteAllBytes(outputFilename, dataToWrite);
                        Console.WriteLine($"Done - wrote {outputFilename}");

                    }
                    catch (System.Exception e)
                    {
                        Console.WriteLine($"An error occured while reading {arg}:\n{e}\nPress ENTER to continue to next file");
                        Console.ReadLine();
                    }
                }
                else
                {
                    Console.WriteLine($"No such file!");
                }
            }

            Console.WriteLine("Press ENTER to exit!");
            Console.ReadLine();
        }
    }
}
