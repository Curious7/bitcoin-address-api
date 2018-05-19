using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace Blockchain
{
    public class UnspentApiResponse
    {
        [JsonProperty("tx_hash")]
        public string Tx_hash { get; set; }

        [JsonProperty("tx_hash_big_endian")]
        public string Tx_hash_big_endian { get; set; }

        [JsonProperty("tx_index")]
        public int Tx_index { get; set; }

        [JsonProperty("tx_output_n")]
        public int Tx_output_n { get; set; }

        [JsonProperty("script")]
        public string Script { get; set; }

        [JsonProperty("value")]
        public int Value { get; set; }

        [JsonProperty("value_hex")]
        public string Value_hex { get; set; }

        [JsonProperty("confirmations")]
        public int Confirmations { get; set; }

    }
}
