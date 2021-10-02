using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GenProfileFN
{
    public class ProfileItem
    {
        [JsonProperty("templateId")]
        public string TemplateId { get; set; }

        [JsonProperty("attributes")]
        public AttributeItem attributes = new AttributeItem();

        [JsonProperty("quantity")]
        public int quantity = 1;

        public ProfileItem(string templateId)
        {
            this.TemplateId = templateId;
        }
    }

    

    public class AttributeItem
    {
        [JsonProperty("max_level_bonus")]
        public int MaxLevelBonus = 0;

        [JsonProperty("level")]
        public int Level = 0;

        [JsonProperty("item_seen")]
        public bool ItemSeen = true;

        [JsonProperty("rnd_sel_cnt")]
        public int RndSelCnt = 0;

        [JsonProperty("xp")]
        public int Xp = 0;

        [JsonProperty("variants")]
        public object Variants = Array.Empty<object>();

        [JsonProperty("favorite")]
        public bool Favorite = false;
    }
}
