using Newtonsoft.Json;
using System.Collections.Generic;

namespace GenProfileFN.Models
{

    public class Variant
    {
        [JsonProperty("active")]
        public string Active { get; set; }     
        
        [JsonProperty("owned")]
        public List<string> Owned { get; set; }

        [JsonProperty("channel")]
#nullable enable
        public string? Channel { get; set; }
#nullable disable


        [JsonIgnore]

        public string CID { get; set; }

#nullable enable
        public Variant(List<string> owned, string? channel)
#nullable disable
        {
            this.Owned = owned;
            if(this.Owned.Count == 0)
            {
                this.Active = "";
            } else
            {
                this.Active = this.Owned[0];
            }

            if(channel == null)
            {
                this.Channel = null;
            } else
            {
                this.Channel = channel;
            }
        }
    }
}