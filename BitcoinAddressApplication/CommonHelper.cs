using System;
using System.Diagnostics;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Blockchain
{
    
    public static class CommonHelper
    {

        /// <summary>
        /// Validates an address string if it's single valid address
        /// or concatenated list of addresses
        /// </summary>
        /// <param name="addresses"></param>
        /// <returns>bool field indicating if string is valid or not</returns>
        public static bool ValidBitCoinAddress(string addresses)
        {
            if (String.IsNullOrWhiteSpace(addresses))
            {
                Trace.TraceError("bitcoin address cannot be an empty string");
                return false;
            }

            if (!BitcoinConfiguration.Constants.AllowMultipleUnspentAddress && addresses.Contains('|'))
            {
                Trace.TraceError("Multiple addresses are not allowed");
                return false;
            }

            bool allAddressValid = true;
            var addressList = addresses.Split('|');

            for (int i = 0; i < addressList.Length && allAddressValid; i++)
            {
                var bitcoinAddress = new BitcoinAddress(addressList[i]);

                if (!bitcoinAddress.HasBase58Address && !bitcoinAddress.HasXpubAddress)
                    allAddressValid = false;
            }

            Trace.TraceInformation($"Address {addresses} are valid.");
            return allAddressValid;
        }
    }
}
