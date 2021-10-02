using Newtonsoft.Json;

namespace GenProfileFN.Models
{
    public class Items
    {
        [JsonProperty("sandbox_loadout")]

        public Loadout.Loadout locker1 = new Loadout.Loadout("CosmeticLocker:cosmeticlocker_athena");

        [JsonProperty("loadout_1")]

        public Loadout.Loadout locker2 = new Loadout.Loadout("CosmeticLocker:cosmeticlocker_athena");


    }
}