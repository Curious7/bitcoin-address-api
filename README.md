# bitcoin-address-api
The project exposes a web API which accepts bitcoin address and shows unspent output transactions for that address.

## Description

The bitcoin address is captured from an incoming request and is forwarded to blockchain api over an http request.
The blockchain api returns transaction details in form of json response. This response is slimed and is given as output to the request.

The incoming address can be of format base58 or xpub. The code validates the incoming address string before swamping the blockchain API with these requests.
The blockchain API accepts multiple addresses seperated by ('|') delimeter. 
The Bitcoin address api has configurable parameter which when set allows request for mulitple bitcoin addresses.

### Prerequisites

The project is developed usin Web API .net framework in windows.

Visual Studio 2017
ASP.NET and web development workload
.NET Core cross-platform development workload

### Installing and running

Clone the repository into your machine.
Build and run the project. This will launch a localhost server and open the web page link in a browser tab.
Add address parameters to the opened Url to get the api response.

## Running the tests

Unit test cases for the project have been added to the same solution.
The test cases are automated and covers variety of cases for the incoming bitcoin address.

for eg :
is valid base58 address
is invalid base58 address
empty or null address

## Deployment

For testing purposes, this code has been hosted in azure and can be tested using the following url

```
http://bitcoinapplication.azurewebsites.net/api/address/<address>
```
eg : 
http://bitcoinapplication.azurewebsites.net/api/address/1A8JiWcwvpY7tAopUkSnGuEYHmzGYfZPiq

Alternatively, the response can be fetched using the following curl command :

```
$curl http://bitcoinapplication.azurewebsites.net/api/address/<address>
```
