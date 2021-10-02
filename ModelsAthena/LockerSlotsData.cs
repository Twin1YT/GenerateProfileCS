using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace GenProfileFN.Models
{
    public class LockerSlotsData
    {
        [JsonProperty("slots")]
        public JObject slots = JObject.Parse("{\"Pickaxe\":{\"items\":[null],\"activeVariants\":[]},\"Dance\":{\"items\":[null,null,null,null,null,null]},\"Glider\":{\"items\":[null]},\"Character\":{\"items\":[\"AthenaCharacter:cid_028_athena_commando_f\"],\"activeVariants\":[]},\"Backpack\":{\"items\":[null],\"activeVariants\":[]},\"ItemWrap\":{\"items\":[null,null,null,null,null,null,null],\"activeVariants\":[null,null,null,null,null,null,null]},\"LoadingScreen\":{\"items\":[null],\"activeVariants\":[null]},\"MusicPack\":{\"items\":[null],\"activeVariants\":[null]},\"SkyDiveContrail\":{\"items\":[null],\"activeVariants\":[null]}}");
        public LockerSlotsData()
        {
        }
    }
}