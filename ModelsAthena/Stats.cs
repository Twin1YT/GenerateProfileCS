using Newtonsoft.Json;
using System;

namespace GenProfileFN.Models
{
    public class Stats
    {
        [JsonProperty("attributes")]
        public Attributes Attributes = new Attributes();
    }
}