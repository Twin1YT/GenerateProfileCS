using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GenProfileFN.Models
{
    public class LocketAttribute
    {
        [JsonProperty("favorite")]
        public bool Favorite = false;
        [JsonProperty("item_seen")]
        public bool ItemSeen = true;

        [JsonProperty("banner_color_template")]

        public string BannerColorTemplate = "defaultcolor0";   
        
        [JsonProperty("banner_icon_template")]

        public string BannerIconTemplate = "OtherBanner28";    
        
        
        [JsonProperty("use_count")]

        public int UseCount = 0;  
        
        [JsonProperty("locker_slots_data")]

        public LockerSlotsData slots = new LockerSlotsData();


    }
}
