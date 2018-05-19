using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace Blockchain
{
        public class SlimUnspentResponseList
        {
            [JsonProperty("outputs")]
            public List<SlimUnspentResponse> SlimUnspentObjects;
        }
    
}
