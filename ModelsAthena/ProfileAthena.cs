using CUE4Parse.Encryption.Aes;
using CUE4Parse.FileProvider;
using CUE4Parse.UE4.Objects.Core.Misc;
using CUE4Parse.UE4.Versions;
using GetItems;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;

namespace GenProfileFN.Models
{
    class ProfileAthena
    {
        [JsonProperty("_id")]
        public string Id { get; set; }

        [JsonProperty("profileId")]

        public string ProfileId = "athena";

        [JsonProperty("version")]
        public string Version = "neonite_2";

        [JsonProperty("wipeNumber")]
        public int WipeNumber = 1;

        [JsonProperty("accountId")]
        public string AccountId { get; set; }

        [JsonProperty("created")]
        public DateTime Created = DateTime.UtcNow;
        
        [JsonProperty("updated")]
        public DateTime Updated = DateTime.UtcNow;

        [JsonProperty("rvn")]
        public int Rvn = 1;

        [JsonProperty("items")]
        public JObject items = GetItems();

        private static JObject GetItems()
        {
            do
            {
                System.Threading.Thread.Sleep(50);
            } while (Program.GenerationFinished == false);

            return Program.items;
        }

        [JsonProperty("stats")]
        public Stats stats = new Stats();


    }
}
