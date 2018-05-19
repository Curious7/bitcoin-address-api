using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace Blockchain
{
    public class SlimUnspentResponse
    {

        [JsonProperty("value")]
        public int Value { get; set; }

        [JsonProperty("tx_hash")]
        public string Tx_hash { get; set; }

        [JsonProperty("output_idx")]
        public int OutputIndex { get; set; }

    }
}
