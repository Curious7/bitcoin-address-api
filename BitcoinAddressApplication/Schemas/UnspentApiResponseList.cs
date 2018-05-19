using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace Blockchain
{
    public class UnspentApiResponseList
    {
        [JsonProperty("unspent_outputs")]
        public List<UnspentApiResponse> UnspentObjects;
    }
}
