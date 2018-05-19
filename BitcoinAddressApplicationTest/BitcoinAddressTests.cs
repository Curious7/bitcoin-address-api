using System;
using System.Diagnostics;
using System.Collections.Generic;
using System.Threading.Tasks;

using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace BitcoinAddressApplicationTest
{
    [TestClass]
    public class BitcoinAddressTest
    {
        public static string jsonOutput = "{\r\n  \"outputs\": [\r\n    {\r\n      \"value\": 62993,\r\n      \"tx_hash\": \"8ad3f10a70a341e5b4d061c29649d5db4037580cbdf105ddce613364196fc3d2\",\r\n      \"output_idx\": 0\r\n    },\r\n    {\r\n      \"value\": 50937,\r\n      \"tx_hash\": \"364d3d1a3901943190d06e94d85817230f2ce21812c17d0ae0a71ef849b0ab98\",\r\n      \"output_idx\": 1\r\n    },\r\n    {\r\n      \"value\": 50989,\r\n      \"tx_hash\": \"25d9563ecd6d6e1792cb48226e4ef01c53b5b55caa01b1b46787e881159a5403\",\r\n      \"output_idx\": 2\r\n    },\r\n    {\r\n      \"value\": 23268,\r\n      \"tx_hash\": \"cb37bd3ecd28d7c376c073d3f2ca03dc82858b854e24408dc1ff93239b7d8c72\",\r\n      \"output_idx\": 3\r\n    },\r\n    {\r\n      \"value\": 307964,\r\n      \"tx_hash\": \"7eac6c44308254108493f805a02c76b3e9c5ee9b943fb3a53e0c10b182bfd82f\",\r\n      \"output_idx\": 4\r\n    },\r\n    {\r\n      \"value\": 163345,\r\n      \"tx_hash\": \"e1e357a087c0ee95e8d00722d645cfac251beb9adbec6741196cd51f2bfa5a43\",\r\n      \"output_idx\": 5\r\n    },\r\n    {\r\n      \"value\": 163345,\r\n      \"tx_hash\": \"fa0b798a7e47d1cf8ae0184cc5233ce6b9a5dc0380aa270ae32b0b8a081aaf22\",\r\n      \"output_idx\": 6\r\n    }\r\n  ]\r\n}";
        public const string InvalidBitcoinAddress = "Invalid bitcoin address";
        public const string NoFreeOutputsToSpend = "No free outputs to spend";

        [TestMethod]
        public void GetAddress_EmptyAddress_Failure()
        {
            var controller = new Blockchain.AddressController();

            Trace.TraceInformation("Empty address should return failure");
            var response = controller.Get(string.Empty);
            Assert.IsNotNull(response);
            Assert.AreEqual(TaskStatus.RanToCompletion, response.Status);
            Assert.AreEqual(InvalidBitcoinAddress, response.Result);
        }

        [TestMethod]
        public void GetAddress_ValidAddress_Success()
        {
            var controller = new Blockchain.AddressController();

            Trace.TraceInformation("Valid address should return success");
            var response = controller.Get("1A8JiWcwvpY7tAopUkSnGuEYHmzGYfZPiq");
            Assert.IsNotNull(response);
            Assert.AreEqual(TaskStatus.RanToCompletion, response.Status);
            Assert.AreEqual(jsonOutput, response.Result);
        }

        [TestMethod]
        public void GetAddress_ValidAddress_NoFreeOutput()
        {
            var controller = new Blockchain.AddressController();

            Trace.TraceInformation("Valid base58 address with no unspent outputs should return no free outputs to spend");
            var response = controller.Get("1AJbsFZ64EpEfS5UAjAfcUG8pH8Jn3rn1F");
            Assert.IsNotNull(response);
            Assert.AreEqual(TaskStatus.RanToCompletion, response.Status);
            Assert.AreEqual("No free outputs to spend", response.Result);
        }

        [TestMethod]
        public void GetAddress_InvalidBase58Address_Failure()
        {
            var controller = new Blockchain.AddressController();

            Trace.TraceInformation("Passing an invalid base58 address should return invalid bitcoin address");
            var response = controller.Get("1AJbsFZ64EpEfS5UAjAfcUG8pH8Jn3rn1G");
            Assert.IsNotNull(response);
            Assert.AreEqual(TaskStatus.RanToCompletion, response.Status);
            Assert.AreEqual(InvalidBitcoinAddress, response.Result);
        }

        [TestMethod]
        public void GetAddress_InvalidAddressString_Failure()
        {
            var controller = new Blockchain.AddressController();

            Trace.TraceInformation("Passing an invalid address string should return invalid bitcoin address");
            var response = controller.Get("garbag#$#@");
            Assert.IsNotNull(response);
            Assert.AreEqual(TaskStatus.RanToCompletion, response.Status);
            Assert.AreEqual(InvalidBitcoinAddress, response.Result);
        }
    }
}
