# Lisk-API (.net Portable )
Lisk API is a .net PCL (portable class library) written in c#.

A NuGet package is available https://www.nuget.org/packages/Lisk.API

Uses the BIP39 PCL https://github.com/ByronAP/BIP39.NET-Portable

The Lisk API document can be found here https://lisk.io/documentation?i=lisk-docs/APIReference </br>All method names in this library are based on the url and relevant action as specified in the reference document.

********************************************
### Example Usage
```javascript
class Program
    {
        static void Main(string[] args)
        {
           // this allows us to run async and support cancellation, this extra code is not required but nice to have
           CancellationTokenSource cts = new CancellationTokenSource();

            Console.CancelKeyPress += (s, e) =>
            {
                e.Cancel = true;
                cts.Cancel();
            };

            MainAsync(args, cts.Token).Wait();
        }

        static async Task MainAsync(string[] args, CancellationToken token)
        {
            Console.WriteLine("Press enter to run again or any other key+enter to exit.");
            RERUN:
            //create an instance of the api, we will just use the defaults instead of supplying server etc...
            var api = new Lisk.API.LiskAPI();
            //get the current block height
            var ss = await api.Blocks_GetHeight();
            //show the current blockheight in the console
            Console.WriteLine(ss.height);
            if ( string.IsNullOrEmpty(Console.ReadLine().Trim()))
                goto RERUN;
        }
    }
```
