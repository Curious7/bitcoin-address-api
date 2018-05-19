using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Blockchain
{
    public static class BitcoinConfiguration
    {
        /// <summary>
        /// Declare constants needed throughout the project
        /// </summary>
        public struct Constants
        {
            /// <summary>
            /// Unspent output API URI
            /// </summary>
            public const string UspentApiUri = @"https://blockchain.info/unspent?active=";

            /// <summary>
            /// Error string when address in invalid
            /// </summary>
            public const string InvalidBitcoinAddrError = "Invalid bitcoin address";
            
            /// <summary>
            /// Error string when no address is passed
            /// </summary>
            public const string NoAddressPassedError = "No address is passed";

           /// <summary>
           /// Check if multiple address passing is alllowed
           /// </summary>
            public const bool AllowMultipleUnspentAddress = true;

        }
    }
}