
using CUE4Parse.Encryption.Aes;
using CUE4Parse.FileProvider;
using CUE4Parse.UE4.Objects.Core.Misc;
using CUE4Parse.UE4.Versions;
using GenProfileFN;
using GenProfileFN.Models;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;

namespace GetItems
{
    public class Program
    {
        public static string PakFiles = "";
        public static bool GenerationFinished = false;
        public static JObject items;
        static void Main(string[] args)
        {
            if (!File.Exists("C:\\ProgramData\\Epic\\UnrealEngineLauncher\\LauncherInstalled.dat"))
            {
                Console.WriteLine("EpicGames installation file not found.");
                Thread.Sleep(-1);
            }



            string filecontent = File.ReadAllText("C:\\ProgramData\\Epic\\UnrealEngineLauncher\\LauncherInstalled.dat");
            Installation installations = JsonConvert.DeserializeObject<Installation>(filecontent);
            var fortniteinstall = installations.InstallationList.Find(install => install.AppName == "Fortnite");
            if (fortniteinstall == null)
            {
                Console.WriteLine("Fortnite installation not found.");
                Thread.Sleep(-1);
            }

            var pakfiles = Path.Combine(fortniteinstall.InstallLocation, "FortniteGame\\Content\\Paks");
            if (!File.Exists(Path.Combine(pakfiles, "pakchunk0-WindowsClient.pak")))
            {
                Console.WriteLine("Fortnite installation not found.");
                Thread.Sleep(-1);
            }

            new Thread(ConsoleQueue.Init).Start();

            new Thread(() => {

                WebClient aesclient = new WebClient();
                string aesjs = aesclient.DownloadString("https://benbot.app/api/v2/aes");
                var aesz = JObject.Parse(aesjs);
                var aes = aesz["mainKey"].ToString();
                //     Console.WriteLine();
                ConsoleQueue.AddToWrite("AES Key found : " + aes);
                JArray dynamic = JArray.Parse(aesz["dynamicKeys"].ToString());

                var provider = new DefaultFileProvider(pakfiles, SearchOption.TopDirectoryOnly, true);
                provider.Initialize();
                foreach (var a in dynamic)
                {
                       ConsoleQueue.AddToWrite($"Found key : {a["guid"]} - {a["key"]}");
                    provider.SubmitKey(new FGuid(a["guid"].ToString()), new FAesKey(a["key"].ToString()));
                }
                provider.SubmitKey(new FGuid(), new FAesKey(aes));
                provider.LoadMappings();
                provider.LoadLocalization(ELanguage.English);

                string[] prefix = { "fortnitegame/content/athena/items/cosmetics/characters", "fortnitegame/content/athena/items/cosmetics/pickaxes", "fortnitegame/content/athena/items/cosmetics/backpacks", "fortnitegame/content/athena/items/cosmetics/contrails", "fortnitegame/content/athena/items/cosmetics/sprays", "fortnitegame/content/athena/items/cosmetics/dances", "fortnitegame/content/athena/items/cosmetics/victoryposes", "fortnitegame/content/athena/items/cosmetics/gliders", "fortnitegame/content/athena/items/cosmetics/pets", "fortnitegame/content/athena/items/cosmetics/loadingscreens", "fortnitegame/content/athena/items/cosmetics/consumableemotes", "fortnitegame/content/athena/items/cosmetics/toys", "fortnitegame/content/athena/items/cosmetics/musicpacks", "fortnitegame/content/athena/items/cosmetics/itemwraps" };
                var files = provider.Files.Where(key => prefix.Any(p => key.Key.StartsWith(p)) && !key.Key.Contains("tandem"));
                var count = files.Count();

                ConsoleQueue.AddToWrite($"Found {count} possible files");

                Items item = new Items();
                JObject obj = JObject.FromObject(item);



                var i = 0;
                foreach (var a in files)
                {

                        if (!a.Key.Contains("athenadanceitemdefinition_adhocsquadsjoin"))
                        {
                            i++;
                            if (i % 10 == 0 && i > 1000)
                            {
                                ConsoleQueue.AddToWrite($"Parsed {i} files, please wait.");
                            }
                            var exports = provider.LoadObjectExports(a.Value.Path);
                            try
                            {
                                string cid = "";
                                var file = JArray.FromObject(exports);
                                List<Variant> variants = new List<Variant>();

                                for (int i2 = 0; i2 < file.Count; i2++)
                                {
                                    var export = file[i2];
                                    if (i2 == 0)
                                    {

                                        var type = export["Type"].ToString();
                                        if (type != "BlueprintGeneratedClass")
                                        {

                                        }
                                        string backendtype;
                                        if (a.Key.ToLower().Contains("wrap_"))
                                        {
                                            backendtype = type.Substring(0, type.IndexOf("Definition"));
                                        }
                                        else
                                        {
                                            backendtype = type.Substring(0, type.IndexOf("ItemDefinition"));
                                        }
                                        var json = JsonConvert.SerializeObject(exports, Formatting.Indented);
                                        var s = a.Value.Path.Split("/");
                                        cid = $"{backendtype}:{s[s.Length - 1]}";
                                    }
                                    else if (export["Type"].ToString() == "FortCosmeticMaterialVariant")
                                    {
                                  
#nullable enable
                                        string? channel;
                                        var cha = export["Properties"]["VariantChannelTag"]["TagName"];
                                        if (cha.Type == JTokenType.Null)
                                        {
                                            channel = null;
                                        }
                                        else
                                        {
                                            var channel2 = cha.ToString().Split(".");
                                            channel = channel2[channel2.Length - 1];
                                        }
#nullable disable




                                    var owned = new JArray(export["Properties"]["MaterialOptions"].SelectTokens("[*].CustomizationVariantTag.TagName")).Values<string>().ToList().Select(str => {
                   
                                        return str.Replace("Cosmetics.Variant.Property.", "");

                                    }).ToList();
                                    variants.Add(new Variant(owned, channel));



                                }
                                else if(export["Type"].ToString() == "FortCosmeticCharacterPartVariant")
                                {
                                
#nullable enable
                                    string? channel;
                                    var cha = export["Properties"]["VariantChannelTag"]["TagName"];
                                    if (cha.Type == JTokenType.Null)
                                    {
                                        channel = null;
                                    }
                                    else
                                    {
                                        var channel2 = cha.ToString().Split(".");
                                        channel = channel2[channel2.Length - 1];
                                    }
#nullable disable

                                    var owned = new JArray(export["Properties"]["PartOptions"].SelectTokens("[*].CustomizationVariantTag.TagName")).Values<string>().ToList().Select(str => {
                            
                                        return str.Replace("Cosmetics.Variant.Property.", "");

                                    }).ToList();
                                    variants.Add(new Variant(owned, channel));


                              
                                } else if(export["Type"].ToString() == "FortCosmeticParticleVariant")
                                {
#nullable enable
                                    string? channel;
                                    var cha = export["Properties"]["VariantChannelTag"]["TagName"];
                                    if (cha.Type == JTokenType.Null)
                                    {
                                        channel = null;
                                    }
                                    else
                                    {
                                        var channel2 = cha.ToString().Split(".");
                                        channel = channel2[channel2.Length - 1];
                                    }
#nullable disable

                                    var owned = new JArray(export["Properties"]["ParticleOptions"].SelectTokens("[*].CustomizationVariantTag.TagName")).Values<string>().ToList().Select(str => {

                                        return str.Replace("Cosmetics.Variant.Property.", "");

                                    }).ToList();
                                    variants.Add(new Variant(owned, channel));
                                } else if(export["Type"].ToString() == "FortCosmeticMeshVariant")
                                {
#nullable enable
                                    string? channel;
                                    var cha = export["Properties"]["VariantChannelTag"]["TagName"];
                                    if (cha.Type == JTokenType.Null)
                                    {
                                        channel = null;
                                    }
                                    else
                                    {
                                        var channel2 = cha.ToString().Split(".");
                                        channel = channel2[channel2.Length - 1];
                                    }
#nullable disable

                                    var owned = new JArray(export["Properties"]["MeshOptions"].SelectTokens("[*].CustomizationVariantTag.TagName")).Values<string>().ToList().Select(str => {

                                        return str.Replace("Cosmetics.Variant.Property.", "");

                                    }).ToList();
                                    variants.Add(new Variant(owned, channel));
                                }
                            }

                                obj[cid.Substring(0, cid.Length - 7)] = JObject.FromObject(new ProfileItem(cid.Substring(0, cid.Length - 7))
                                {
                                    attributes = new AttributeItem()
                                    {
                                        Variants = variants
                                    }
                                });
                            }
                            catch (Exception ex)
                            {
                                ConsoleQueue.AddToWrite(a.Key + "   -    " + ex);
                            }
                        }
              
                 
                }
                GenerationFinished = true;
                Program.items = obj;
            }).Start();

            Console.WriteLine($"Pak folder located to {pakfiles}");
            PakFiles = pakfiles;

            Console.Write("Username: ");
            string usn = Console.ReadLine();
            ConsoleQueue.Write = true;

            var profile = new ProfileAthena() { Id = usn, AccountId = usn };
            JObject pr = JObject.FromObject(profile);
            Console.WriteLine($"Wrote the profile to : \n{Path.Join(Environment.CurrentDirectory, "profile_athena.json")}\n Thanks to support me !");
       
            File.WriteAllText($"{Environment.CurrentDirectory}\\profile_athena.json", pr.ToString());
            Thread.Sleep(3000);
        }
    }

