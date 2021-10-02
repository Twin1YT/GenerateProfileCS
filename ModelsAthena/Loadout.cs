using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GenProfileFN.Models.Loadout
{
    public class Loadout
    {
        [JsonProperty("templateId")]
        public string TemplateId { get; set; }

        [JsonProperty("attributes")]
        public LocketAttribute Attributes = new LocketAttribute();

        public int quantity = 1;

        public Loadout()
        {

        }

        public Loadout(string templateId)
        {
            this.TemplateId = templateId;
        }
    }
}
