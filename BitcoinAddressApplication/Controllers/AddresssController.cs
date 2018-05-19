using System;
using System.Diagnostics;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using System.Net;
using System.IO;
using Newtonsoft.Json;


namespace Blockchain
{
    [Route("api/[controller]")]
    public class AddressController : Controller
    {
        public UnspentModelOutput UnpsentOutput;

        // GET: api/<controller>
        // No address is passed
        [HttpGet]
        public string Get()
        {
            return BitcoinConfiguration.Constants.NoAddressPassedError;
        }

        /// <summary>
        /// GET api/address/{*address} 
        /// Get unspent output for a given bitcoin address
        /// </summary>
        /// <param name="address"> Bitcoin address </param>
        /// <returns></returns>
        [HttpGet("{address}")]
        public async Task<string> Get(string address)
        {
            UnpsentOutput = new UnspentModelOutput();

            Trace.TraceInformation("Validating input address"); 

            if (String.IsNullOrWhiteSpace(address))
            {
                UnpsentOutput.OutputString = BitcoinConfiguration.Constants.NoAddressPassedError;
                return UnpsentOutput.OutputString;
            }

            if (!CommonHelper.ValidBitCoinAddress(address))
            {
                UnpsentOutput.OutputString = BitcoinConfiguration.Constants.InvalidBitcoinAddrError;
                return UnpsentOutput.OutputString;
            }

            string requestUrl = BitcoinConfiguration.Constants.UspentApiUri + address;
            var response = await GetAPIResponse(requestUrl);

            var processingError = ProcessAPIResponse(response);

            if (processingError)
            {
                return BitcoinConfiguration.Constants.InvalidBitcoinAddrError;
            }

            Trace.TraceInformation("Unspent output for address {0} is \n {1}", address, UnpsentOutput.OutputString);
            return UnpsentOutput.OutputString;
        }

        /// <summary>
        /// Process API reponse for json data and populates output object
        /// </summary>
        /// <returns>bool to indicate if there was error processing the response </returns>
        private bool ProcessAPIResponse(HttpWebResponse response)
        {
            if (response != null && (response.StatusCode == HttpStatusCode.OK || response.StatusCode == HttpStatusCode.InternalServerError))
            {
                var stream = response.GetResponseStream();
                StreamReader reader = new StreamReader(stream);
                UnpsentOutput.OutputString = reader.ReadToEnd();

                if (response.StatusCode == HttpStatusCode.OK)
                {
                    var apiOutput = new UnspentApiResponseList();

                    try
                    {
                        apiOutput = JsonConvert.DeserializeObject<UnspentApiResponseList>(UnpsentOutput.OutputString);
                    }
                    catch (Exception ex)
                    {
                        string strng = ex.Message;
                        return true;
                    }

                    int count = 0;
                    var OutputObject = new SlimUnspentResponseList
                    {
                        SlimUnspentObjects = new List<SlimUnspentResponse>()
                    };

                    foreach (var obj in apiOutput.UnspentObjects)
                    {
                        Trace.TraceInformation("API ouput returns multiple fields. We'll filter only the necessary fields and create a slim object out of the response");
                        var unspentObject = new SlimUnspentResponse
                        {
                            Value = obj.Value,
                            Tx_hash = obj.Tx_hash,
                            OutputIndex = count++
                        };

                        OutputObject.SlimUnspentObjects.Add(unspentObject);
                    }

                    UnpsentOutput.OutputString = JsonConvert.SerializeObject(OutputObject, Formatting.Indented);
                }

                return false;
            }

            return true;
        }

        /// <summary>
        /// Makes http call to api and gets response
        /// </summary>
        /// <returns>response of the api</returns>
        private Task<HttpWebResponse> GetAPIResponse(string requestUrl)
        {
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(requestUrl);
            HttpWebResponse response = null;

            try
            {
                response = (HttpWebResponse)request.GetResponse();
            }
            catch (WebException webException)
            {
                Trace.TraceError("Encountered a web exception while getting API Response. Details: \n {0}", webException.Message);
                response = (HttpWebResponse)webException.Response;
            }

            return Task.FromResult(response);
        }
    }
}
