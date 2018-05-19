using System;
using Base58Check;

namespace Blockchain
{
    /// <summary>
    ///  Class to represent a bitcoin object
    /// </summary>
    public class BitcoinAddress
    {
        /// <summary>
        /// A bitcoin object can either contain xpub address of base58 address
        /// </summary>
        public string Base58Address { get; }
        public string XpubAddress { get; }

        public bool HasBase58Address = false;

        public bool HasXpubAddress = false;

        public BitcoinAddress()
        {

        }

        /// <summary>
        /// Constructor takes in a string
        /// and assigns it to appropriate address field
        /// </summary>
        /// <param name="addressString"></param>
        public BitcoinAddress(string addressString)
        {
            if (ValidBase58(addressString))
            {
                this.Base58Address = addressString;
                HasBase58Address = true;
            }
            else if (ValidXpub(addressString))
            {
                this.XpubAddress = addressString;
                HasXpubAddress = true;

            }
        }

        /// <summary>
        /// Method to check whether an address is vald base58
        /// </summary>
        /// <param name="addressString"></param>
        /// <returns>bool value if or not address is base58</returns>
        private bool ValidBase58(string addressString)
        {
            bool isValidbase58address = false;

            if (String.IsNullOrWhiteSpace(addressString))
                return false;

            try
            {
                Base58CheckEncoding.Decode(addressString);
                isValidbase58address = true;
            }
            catch (Exception ex)
            {
                isValidbase58address = false;
            }

            return isValidbase58address;
        }

        /// <summary>
        /// Method to check whether address is valid xpub
        /// </summary>
        /// <param name="addressString"></param>
        /// <returns>bool value</returns>
        private bool ValidXpub(string addressString)
        {
            // pending implementation
            return false;
        }
    }
}
