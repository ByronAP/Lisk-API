//https://lisk.io/documentation?i=lisk-docs/APIReference
//Author: Allen Byron Penner
// BIP39 can be found at https://github.com/ByronAP/BIP39.NET-Portable

using System;
using System.Collections.Generic;
using System.Globalization;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using BigMath;
using Bitcoin.BIP39;
using Lisk.API.Responses;
using Newtonsoft.Json;

namespace Lisk.API
{
    public class LiskAPI
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="serverBaseUrl">The url of the server to connect to such as https://www.liskwallet.info (default) do not include any parameters or slashes, a port is acceptable.</param>
        /// <param name="userAgent">The user agent to send when making requests to the server. Default is the Google Chrome 41 user agent string.</param>
        public LiskAPI(string serverBaseUrl = "https://www.liskwallet.info", string userAgent = "Mozilla/5.0 (Windows NT 6.1) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/41.0.2228.0 Safari/537.36")
        {
            User_Agent = userAgent;
            Server_Url = serverBaseUrl;
        }

        public string User_Agent { get; set; }
        public string Server_Url { get; set; }

        /// <summary>
        /// Get information about an account.
        /// </summary>
        /// <param name="secret">secret key of account</param>
        /// <returns></returns>
        public async Task<accounts_open_response> Accounts_Open(string secret)
        {
            const string url = "/api/accounts/open";
            var postdata = new List<KeyValuePair<string, string>> {new KeyValuePair<string, string>("secret", secret)};
            var pr = await HttpPostRequestAsync(url, User_Agent, postdata);
            return pr.StartsWith("ERROR") ? new accounts_open_response {error = pr, success = false} : JsonConvert.DeserializeObject<accounts_open_response>(pr);
        }

        /// <summary>
        /// Get the balance of an account.
        /// </summary>
        /// <param name="address">Address of the account</param>
        /// <returns></returns>
        public async Task<accounts_getBalance_response> Accounts_GetBalance(string address)
        {
            var url = "/api/accounts/getBalance?address=" + WebUtility.UrlEncode(address);
            var gr = await HttpGetRequestAsync(url, User_Agent);
            return gr.StartsWith("ERROR") ? new accounts_getBalance_response {error = gr, success = false} : JsonConvert.DeserializeObject<accounts_getBalance_response>(gr);
        }

        /// <summary>
        /// Get the public key of an account. If the account does not exist the API call will return an error.
        /// </summary>
        /// <param name="address"> Address of account</param>
        /// <returns></returns>
        public async Task<accounts_getPublicKey_response> Accounts_GetPublicKey(string address)
        {
            var url = "/api/accounts/getPublicKey?address=" + WebUtility.UrlEncode(address);
            var gr = await HttpGetRequestAsync(url, User_Agent);
            return gr.StartsWith("ERROR") ? new accounts_getPublicKey_response {error = gr, success = false} : JsonConvert.DeserializeObject<accounts_getPublicKey_response>(gr);
        }

        /// <summary>
        /// Returns the public key of the provided secret key.
        /// </summary>
        /// <param name="secret">secret key of account</param>
        /// <returns></returns>
        public async Task<accounts_generatePublicKey_response> Accounts_GeneratePublicKey(string secret)
        {
            const string url = "/api/accounts/generatePublicKey";
            var postdata = new List<KeyValuePair<string, string>> {new KeyValuePair<string, string>("secret", secret)};
            var pr = await HttpPostRequestAsync(url, User_Agent, postdata);
            return pr.StartsWith("ERROR") ? new accounts_generatePublicKey_response {error = pr, success = false} : JsonConvert.DeserializeObject<accounts_generatePublicKey_response>(pr);
        }

        /// <summary>
        /// Return account information of an address.
        /// </summary>
        /// <param name="address">Address of account</param>
        /// <returns></returns>
        public async Task<accounts_getAccount_response> Accounts_GetAccount(string address)
        {
            var url = "/api/accounts?address=" + WebUtility.UrlEncode(address);
            var gr = await HttpGetRequestAsync(url, User_Agent);
            return gr.StartsWith("ERROR") ? new accounts_getAccount_response {error = gr, success = false} : JsonConvert.DeserializeObject<accounts_getAccount_response>(gr);
        }

        /// <summary>
        /// Returns delegate accounts by address.
        /// </summary>
        /// <param name="address">Address of account</param>
        /// <returns></returns>
        public async Task<accounts_getDelegates_response> Accounts_GetDelegates(string address)
        {
            var url = "/api/accounts/delegates?address=" + WebUtility.UrlEncode(address);
            var gr = await HttpGetRequestAsync(url, User_Agent);
            return gr.StartsWith("ERROR") ? new accounts_getDelegates_response {error = gr, success = false} : JsonConvert.DeserializeObject<accounts_getDelegates_response>(gr);
        }

        /// <summary>
        /// Vote for the selected delegates. Maximum of 33 delegates at once.
        /// </summary>
        /// <param name="secret">Secret key of account</param>
        /// <param name="delegates">Array of string in the following format: ["+DelegatePublicKey"] OR ["-DelegatePublicKey"]. Use + to UPvote, - to DOWNvote</param>
        /// <param name="publicKey">Public key of sender account, to verify secret passphrase in wallet. Optional, only for UI</param>
        /// <param name="secondSecret">Secret key from second transaction, required if user uses second signature</param>
        /// <returns></returns>
        public async Task<accounts_putDelegates_response> Accounts_PutDelegates(string secret, string delegates, string publicKey = "", string secondSecret = "")
        {
            const string url = "/api/accounts/delegates";
            var sb = new StringBuilder();
            sb.Append("{\"secret\":\"" + secret + "\",\"delegates\":\"" + delegates + "\"");
            if (!string.IsNullOrEmpty(publicKey))
                sb.Append(",\"publicKey\":\"" + publicKey + "\"");
            if (!string.IsNullOrEmpty(secondSecret))
                sb.Append(",\"secondSecret\":\"" + secondSecret + "\"");
            sb.Append("}");

            var pr = await HttpPutRequestAsync(url, User_Agent, sb.ToString());
            return pr.StartsWith("ERROR") ? new accounts_putDelegates_response {error = pr, success = false} : JsonConvert.DeserializeObject<accounts_putDelegates_response>(pr);
        }

        /// <summary>
        /// Get the loading status of the client.
        /// </summary>
        /// <returns></returns>
        public async Task<loader_status_response> Loader_Status()
        {
            const string url = "/api/loader/status";
            var gr = await HttpGetRequestAsync(url, User_Agent);
            return gr.StartsWith("ERROR") ? new loader_status_response {error = gr, success = false} : JsonConvert.DeserializeObject<loader_status_response>(gr);
        }

        /// <summary>
        /// Get the synchronisation status of the client.
        /// </summary>
        /// <returns></returns>
        public async Task<loader_status_sync_response> Loader_SyncStatus()
        {
            const string url = "/api/loader/status/sync";
            var gr = await HttpGetRequestAsync(url, User_Agent);
            return gr.StartsWith("ERROR") ? new loader_status_sync_response {error = gr, success = false} : JsonConvert.DeserializeObject<loader_status_sync_response>(gr);
        }

        /// <summary>
        /// Transactions list matched by provided parameters.
        /// </summary>
        /// <param name="blockId"> Block id of transaction</param>
        /// <param name="senderId">Sender address of transaction</param>
        /// <param name="recipientId">Recipient of transaction</param>
        /// <param name="limit">Limit of transaction to send in response. Default is 20</param>
        /// <param name="offset">Offset to load. Default is 0</param>
        /// <param name="orderBy">Name of column to order. After column name must go "desc" or "acs" to choose order type, prefix for column name is t_. Example: orderBy=t_timestamp:desc</param>
        /// <returns></returns>
        public async Task<transactions_getList_response> Transactions_GetList(string blockId = "", string senderId = "",
            string recipientId = "", int limit = 20, int offset = 0, string orderBy = "")
        {
            var url = "/api/transactions?";
            if (!string.IsNullOrEmpty(blockId))
                url += "blockId=" + WebUtility.UrlEncode(blockId);
            if (!string.IsNullOrEmpty(senderId))
            {
                url += url.EndsWith("?") || url.EndsWith("&")
                    ? "senderId=" + WebUtility.UrlEncode(senderId)
                    : "&senderId=" + WebUtility.UrlEncode(senderId);
            }
            if (!string.IsNullOrEmpty(recipientId))
            {
                url += url.EndsWith("?") || url.EndsWith("&")
                    ? "recipientId=" + WebUtility.UrlEncode(recipientId)
                    : "&recipientId=" + WebUtility.UrlEncode(recipientId);
            }
            url += url.EndsWith("?") || url.EndsWith("&") ? "limit=" + limit : "&limit=" + limit;
            url += url.EndsWith("?") || url.EndsWith("&") ? "offset=" + offset : "&offset=" + offset;
            if (!string.IsNullOrEmpty(orderBy))
            {
                url += url.EndsWith("?") || url.EndsWith("&")
                    ? "orderBy=" + WebUtility.UrlEncode(orderBy)
                    : "&orderBy=" + WebUtility.UrlEncode(orderBy);
            }
            var gr = await HttpGetRequestAsync(url, User_Agent);
            return gr.StartsWith("ERROR") ? new transactions_getList_response {error = gr, success = false} : JsonConvert.DeserializeObject<transactions_getList_response>(gr);
        }

        /// <summary>
        /// Send transaction to broadcast network.
        /// </summary>
        /// <param name="secret">Secret key of account</param>
        /// <param name="amount">Amount of transaction * 10^8. Example: to send 1.1234 LISK, use 112340000 as amount. It is recomended that you use the static converts provided in this class</param>
        /// <param name="recipientId">Recipient of transaction. Address or username.</param>
        /// <param name="publicKey">Public key of sender account, to verify secret passphrase in wallet. Optional, only for UI</param>
        /// <param name="secondSecret">Secret key from second transaction, optional, only required if account uses second signature</param>
        /// <returns></returns>
        public async Task<transactions_send_response> Transactions_Send(string secret, long amount, string recipientId,
            string publicKey = "", string secondSecret = "")
        {
            const string url = "/api/transactions";
            var sb = new StringBuilder();
            sb.Append("{\"secret\":\"" + secret + "\",\"amount\":" + amount + ",\"recipientId\":\"" + recipientId + "\"");
            if (!string.IsNullOrEmpty(publicKey))
                sb.Append(",\"publicKey\":\"" + publicKey + "\"");
            if (!string.IsNullOrEmpty(secondSecret))
                sb.Append(",\"secondSecret\":\"" + secondSecret + "\"");
            sb.Append("}");
            var pr = await HttpPutRequestAsync(url, User_Agent, sb.ToString());
            return pr.StartsWith("ERROR") ? new transactions_send_response {error = pr, success = false} : JsonConvert.DeserializeObject<transactions_send_response>(pr);
        }

        /// <summary>
        /// Gets a transaction matched by id.
        /// </summary>
        /// <param name="id">Id string of transaction</param>
        /// <returns></returns>
        public async Task<transactions_get_response> Transactions_Get(string id)
        {
            var url = "/api/transactions/get?id=" + WebUtility.UrlEncode(id);
            var gr = await HttpGetRequestAsync(url, User_Agent);
            return gr.StartsWith("ERROR") ? new transactions_get_response {error = gr, success = false} : JsonConvert.DeserializeObject<transactions_get_response>(gr);
        }

        /// <summary>
        /// Gets an unconfirmed transaction by id.
        /// </summary>
        /// <param name="id">Id string of transaction</param>
        /// <returns></returns>
        public async Task<transactions_getUnconfirmed_response> Transactions_GetUnconfirmed(string id)
        {
            var url = "/api/transactions/unconfirmed?id=" + WebUtility.UrlEncode(id);
            var gr = await HttpGetRequestAsync(url, User_Agent);
            return gr.StartsWith("ERROR") ? new transactions_getUnconfirmed_response {error = gr, success = false} : JsonConvert.DeserializeObject<transactions_getUnconfirmed_response>(gr);
        }

        /// <summary>
        /// Gets a list of unconfirmed transactions.
        /// </summary>
        /// <returns></returns>
        public async Task<transactions_getListUnconfirmed_response> Transactions_GetListUnconfirmed()
        {
            const string url = "/api/transactions/unconfirmed";
            var gr = await HttpGetRequestAsync(url, User_Agent);
            return gr.StartsWith("ERROR") ? new transactions_getListUnconfirmed_response {error = gr, success = false} : JsonConvert.DeserializeObject<transactions_getListUnconfirmed_response>(gr);
        }

        /// <summary>
        /// Get peers list by parameters.
        /// </summary>
        /// <param name="state">State of peer. 1 - disconnected. 2 - connected. 0 - banned.</param>
        /// <param name="os">OS of peer.</param>
        /// <param name="shared">Is peer shared?</param>
        /// <param name="version">Version of peer.</param>
        /// <param name="limit">Limit to show. Max limit is 100. Default is 20</param>
        /// <param name="offset">Offset to load.</param>
        /// <param name="orderBy">Name of column to order. After column name must go "desc" or "acs" to choose order type.</param>
        /// <returns></returns>
        public async Task<peers_getList_response> Peers_GetList(int? state = null, string os = "", bool? shared = null,
            string version = "", int limit = 20, int? offset = null, string orderBy = "")
        {
            //TODO: Impliment OR for params
            if (limit > 100)
                limit = 100;
            var url = "/api/peers?";
            if (state != null && state >= 0 && state <= 3)
                url += "state=" + state;
            if (!string.IsNullOrEmpty(os))
            {
                url += url.EndsWith("?") || url.EndsWith("&")
                    ? "os=" + WebUtility.UrlEncode(os)
                    : "&os=" + WebUtility.UrlEncode(os);
            }
            if (shared != null)
            {
                url += url.EndsWith("?") || url.EndsWith("&")
                    ? "shared=" + shared.ToString().ToLower()
                    : "&shared=" + shared.ToString().ToLower();
            }
            if (!string.IsNullOrEmpty(version))
            {
                url += url.EndsWith("?") || url.EndsWith("&")
                    ? "version=" + WebUtility.UrlEncode(version)
                    : "&version=" + WebUtility.UrlEncode(version);
            }
            url += url.EndsWith("?") || url.EndsWith("&") ? "limit=" + limit : "&limit=" + limit;
            if (offset != null && offset > 0)
            {
                url += url.EndsWith("?") || url.EndsWith("&") ? "offset=" + offset : "&offset=" + offset;
            }
            if (!string.IsNullOrEmpty(orderBy))
            {
                url += url.EndsWith("?") || url.EndsWith("&")
                    ? "orderBy=" + WebUtility.UrlEncode(orderBy)
                    : "&orderBy=" + WebUtility.UrlEncode(orderBy);
            }
            var gr = await HttpGetRequestAsync(url, User_Agent);
            return gr.StartsWith("ERROR") ? new peers_getList_response {error = gr, success = false} : JsonConvert.DeserializeObject<peers_getList_response>(gr);
        }

        /// <summary>
        /// Get peer by ip and port
        /// </summary>
        /// <param name="ip">Ip of peer.</param>
        /// <param name="port">Port of peer.</param>
        /// <returns></returns>
        public async Task<peers_get_response> Peers_Get(string ip, int port)
        {
            var url = "/api/peers/get?ip=" + WebUtility.UrlEncode(ip) + "&port=" + port;
            var gr = await HttpGetRequestAsync(url, User_Agent);
            return gr.StartsWith("ERROR") ? new peers_get_response {error = gr, success = false} : JsonConvert.DeserializeObject<peers_get_response>(gr);
        }

        /// <summary>
        /// Get peer version and build time
        /// </summary>
        /// <returns></returns>
        public async Task<peers_getVersion_response> Peers_GetVersion()
        {
            const string url = "/api/peers/version";
            var gr = await HttpGetRequestAsync(url, User_Agent);
            return gr.StartsWith("ERROR") ? new peers_getVersion_response {error = gr, success = false} : JsonConvert.DeserializeObject<peers_getVersion_response>(gr);
        }

        /// <summary>
        /// Get block by id.
        /// </summary>
        /// <param name="id">Id of block.</param>
        /// <returns></returns>
        public async Task<blocks_get_response> Blocks_Get(string id)
        {
            var url = "/api/blocks/get?id=" + WebUtility.UrlEncode(id);
            var gr = await HttpGetRequestAsync(url, User_Agent);
            return gr.StartsWith("ERROR") ? new blocks_get_response {error = gr, success = false} : JsonConvert.DeserializeObject<blocks_get_response>(gr);
        }

        /// <summary>
        /// Get all blocks.
        /// </summary>
        /// <param name="generatorPublicKey">generator id of block in hex.</param>
        /// <param name="height">height of block.</param>
        /// <param name="previousBlock">previous block of need block</param>
        /// <param name="totalAmount">total amount of block.</param>
        /// <param name="totalFee">total fee of block.</param>
        /// <param name="limit">limit of blocks to add to response. Default to 20</param>
        /// <param name="offset">offset to load blocks. Default 0</param>
        /// <param name="orderBy">field name to order by. Format: fieldname:orderType. Example: height:desc, timestamp:asc </param>
        /// <returns></returns>
        public async Task<blocks_getList_response> Blocks_GetList(string generatorPublicKey = "", long? height = null,
            long? previousBlock = null, long? totalAmount = null, long? totalFee = null,
            int limit = 20, int? offset = null, string orderBy = "")
        {
            //TODO: Impliment OR for params
            var url = "/api/blocks?";
            if (!string.IsNullOrEmpty(generatorPublicKey))
                url += "generatorPublicKey=" + WebUtility.UrlEncode(generatorPublicKey);
            if (height != null && height >= 0)
            {
                url += url.EndsWith("?") || url.EndsWith("&") ? "height=" + height : "&height=" + height;
            }
            if (previousBlock != null && previousBlock >= 0)
            {
                url += url.EndsWith("?") || url.EndsWith("&")
                    ? "previousBlock=" + previousBlock
                    : "&previousBlock=" + previousBlock;
            }
            if (totalAmount != null && totalAmount >= 0)
            {
                url += url.EndsWith("?") || url.EndsWith("&")
                    ? "totalAmount=" + totalAmount
                    : "&totalAmount=" + totalAmount;
            }
            if (totalFee != null && totalFee >= 0)
            {
                url += url.EndsWith("?") || url.EndsWith("&") ? "totalFee=" + totalFee : "&totalFee=" + totalFee;
            }
            if (limit > 0)
            {
                url += url.EndsWith("?") || url.EndsWith("&") ? "limit=" + limit : "&limit=" + limit;
            }
            if (offset != null && offset > 0)
            {
                url += url.EndsWith("?") || url.EndsWith("&") ? "offset=" + offset : "&offset=" + offset;
            }
            if (!string.IsNullOrEmpty(orderBy))
            {
                url += url.EndsWith("?") || url.EndsWith("&")
                    ? "orderBy=" + WebUtility.UrlEncode(orderBy)
                    : "&orderBy=" + WebUtility.UrlEncode(orderBy);
            }
            var gr = await HttpGetRequestAsync(url, User_Agent);
            return gr.StartsWith("ERROR") ? new blocks_getList_response {error = gr, success = false} : JsonConvert.DeserializeObject<blocks_getList_response>(gr);
        }

        /// <summary>
        /// Get transaction fee for sending "normal" transactions.
        /// </summary>
        /// <returns></returns>
        public async Task<blocks_getFee_response> Blocks_GetFee()
        {
            const string url = "/api/blocks/getFee";
            var gr = await HttpGetRequestAsync(url, User_Agent);
            return gr.StartsWith("ERROR") ? new blocks_getFee_response {error = gr, success = false} : JsonConvert.DeserializeObject<blocks_getFee_response>(gr);
        }

        /// <summary>
        /// Get the fees for all actions.
        /// </summary>
        /// <returns></returns>
        public async Task<blocks_getFees_response> Blocks_GetFees()
        {
            const string url = "/api/blocks/getFees";
            var gr = await HttpGetRequestAsync(url, User_Agent);
            return gr.StartsWith("ERROR") ? new blocks_getFees_response { error = gr, success = false } : JsonConvert.DeserializeObject<blocks_getFees_response>(gr);
        }

        /// <summary>
        /// Get blockchain height.
        /// </summary>
        /// <returns></returns>
        public async Task<blocks_getHeight_response> Blocks_GetHeight()
        {
            const string url = "/api/blocks/getHeight";
            var gr = await HttpGetRequestAsync(url, User_Agent);
            return gr.StartsWith("ERROR") ? new blocks_getHeight_response {error = gr, success = false} : JsonConvert.DeserializeObject<blocks_getHeight_response>(gr);
        }

        /// <summary>
        /// Get current milestone.
        /// </summary>
        /// <returns></returns>
        public async Task<blocks_getMilestone_response> Blocks_GetMilestone()
        {
            const string url = "/api/blocks/getMilestone";
            var gr = await HttpGetRequestAsync(url, User_Agent);
            return gr.StartsWith("ERROR") ? new blocks_getMilestone_response { error = gr, success = false } : JsonConvert.DeserializeObject<blocks_getMilestone_response>(gr);
        }

        /// <summary>
        /// Get current nodes nethash (genesis block id).
        /// </summary>
        /// <returns></returns>
        public async Task<blocks_getNethash_response> Blocks_GetNethash()
        {
            const string url = "/api/blocks/getNethash";
            var gr = await HttpGetRequestAsync(url, User_Agent);
            return gr.StartsWith("ERROR") ? new blocks_getNethash_response { error = gr, success = false } : JsonConvert.DeserializeObject<blocks_getNethash_response>(gr);
        }

        /// <summary>
        /// Get amount forged by account.
        /// </summary>
        /// <param name="generatorPublicKey">generator id of block in hex.</param>
        /// <returns></returns>
        public async Task<delegates_forging_getForgedByAccount_response> Delegates_GetForgedByAccount(
            string generatorPublicKey)
        {
            var url = "/api/delegates/forging/getForgedByAccount?generatorPublicKey=" +
                      WebUtility.UrlEncode(generatorPublicKey);
            var gr = await HttpGetRequestAsync(url, User_Agent);
            return gr.StartsWith("ERROR") ? new delegates_forging_getForgedByAccount_response {error = gr, success = false} : JsonConvert.DeserializeObject<delegates_forging_getForgedByAccount_response>(gr);
        }

        /// <summary>
        /// Get second signature of account.
        /// </summary>
        /// <param name="id">Id of signature.</param>
        /// <returns></returns>
        public async Task<signatures_get_response> Signatures_Get(string id)
        {
            var url = "/api/signatures/get?id=" + WebUtility.UrlEncode(id);
            var gr = await HttpGetRequestAsync(url, User_Agent);
            return gr.StartsWith("ERROR") ? new signatures_get_response {error = gr, success = false} : JsonConvert.DeserializeObject<signatures_get_response>(gr);
        }

        /// <summary>
        /// Add second signature to account.
        /// </summary>
        /// <param name="secret">secret key of account</param>
        /// <param name="secondSecret">second key of account</param>
        /// <param name="publicKey">optional, to verify valid secret key and account</param>
        /// <returns></returns>
        public async Task<signatures_add_response> Signatures_Add(string secret, string secondSecret,
            string publicKey = "")
        {
            const string url = "/api/signatures";
            var sb = new StringBuilder();
            sb.Append("{\"secret\":\"" + secret + "\",\"secondSecret\":\"" + secondSecret + "\"");
            if (!string.IsNullOrEmpty(publicKey))
                sb.Append(",\"publicKey\":\"" + publicKey + "\"");
            sb.Append("}");

            var pr = await HttpPutRequestAsync(url, User_Agent, sb.ToString());
            return pr.StartsWith("ERROR") ? new signatures_add_response {error = pr, success = false} : JsonConvert.DeserializeObject<signatures_add_response>(pr);
        }

        /// <summary>
        /// TODO
        /// </summary>
        /// <returns></returns>
        public async Task<delegates_count_response> Delegates_Count()
        {
            const string url = "/api/delegates/count";
            var gr = await HttpGetRequestAsync(url, User_Agent);
            return gr.StartsWith("ERROR") ? new delegates_count_response {error = gr, success = false} : JsonConvert.DeserializeObject<delegates_count_response>(gr);
        }

        /// <summary>
        /// Calls for delegates functional.
        /// </summary>
        /// <param name="secret">Secret key of account</param>
        /// <param name="username">Username of delegate. String from 1 to 20 characters.</param>
        /// <param name="secondSecret">Second secret of account</param>
        /// <returns></returns>
        public async Task<delegates_enable_response> Delegates_Enable(string secret, string username, string secondSecret = "")
        {
            if (username.Trim().Length < 1 || username.Trim().Length > 20)
                return new delegates_enable_response {error = "Invalid username", success = false, transaction = null};
            const string url = "/api/delegates";
            var json = "{\"secret\":\"" + secret + "\",\"username\":\"" + username + "\"";
            if (!string.IsNullOrEmpty(secondSecret))
                json += ",\"secondSecret\":\"" + secondSecret + "\"";
            json += "}";
            var pr = await HttpPutRequestAsync(url, User_Agent, json);
            return pr.StartsWith("ERROR") ? new delegates_enable_response {error = pr, success = false} : JsonConvert.DeserializeObject<delegates_enable_response>(pr);
        }

        /// <summary>
        /// Get delegates list.
        /// </summary>
        /// <param name="limit">Limit to show. Integer. Maximum is 100, Default is 20</param>
        /// <param name="offset">offset to load delegates. Default 0</param>
        /// <param name="orderBy">Order by field</param>
        /// <returns></returns>
        public async Task<delegates_getList_response> Delegates_GetList(int limit = 20, int offset = 0,
            string orderBy = "")
        {
            if (limit > 100)
                limit = 100;
            var url = "/api/delegates?limit=" + limit + "&offset=" + offset;
            if (!string.IsNullOrEmpty(orderBy))
            {
                url += url.EndsWith("?") || url.EndsWith("&")
                    ? "orderBy=" + WebUtility.UrlEncode(orderBy)
                    : "&orderBy=" + WebUtility.UrlEncode(orderBy);
            }
            var gr = await HttpGetRequestAsync(url, User_Agent);
            return gr.StartsWith("ERROR") ? new delegates_getList_response {error = gr, success = false} : JsonConvert.DeserializeObject<delegates_getList_response>(gr);
        }

        /// <summary>
        /// Get delegate by public key.
        /// </summary>
        /// <param name="publicKey">public key of delegated.</param>
        /// <returns></returns>
        public async Task<delegates_get_response> Delegates_Get(string publicKey)
        {
            var url = "/api/delegates/get?publicKey=" + WebUtility.UrlEncode(publicKey);
            var gr = await HttpGetRequestAsync(url, User_Agent);
            return gr.StartsWith("ERROR") ? new delegates_get_response {error = gr, success = false} : JsonConvert.DeserializeObject<delegates_get_response>(gr);
        }

        /// <summary>
        /// Get votes by account address.
        /// </summary>
        /// <param name="address">Address of account.</param>
        /// <returns></returns>
        public async Task<delegates_getVotes_response> Delegates_GetVotes(string address)
        {
            var url = "/api/accounts/delegates/?address=" + WebUtility.UrlEncode(address);
            var gr = await HttpGetRequestAsync(url, User_Agent);
            return gr.StartsWith("ERROR") ? new delegates_getVotes_response {error = gr, success = false} : JsonConvert.DeserializeObject<delegates_getVotes_response>(gr);
        }

        /// <summary>
        /// Get voters of delegate.
        /// </summary>
        /// <param name="publicKey">Public key of delegate.</param>
        /// <returns></returns>
        public async Task<delegates_getVoters_response> Delegates_GetVoters(string publicKey)
        {
            var url = "/api/delegates/voters?publicKey=" + WebUtility.UrlEncode(publicKey);
            var gr = await HttpGetRequestAsync(url, User_Agent);
            return gr.StartsWith("ERROR") ? new delegates_getVoters_response {error = gr, success = false} : JsonConvert.DeserializeObject<delegates_getVoters_response>(gr);
        }

        /// <summary>
        /// Enable forging.
        /// </summary>
        /// <param name="secret">secret key of delegate account</param>
        /// <returns></returns>
        public async Task<delegates_forging_enable_response> Delegates_EnableForging(string secret)
        {
            const string url = "/api/delegates/forging/enable";
            var postdata = new List<KeyValuePair<string, string>> {new KeyValuePair<string, string>("secret", secret)};
            var pr = await HttpPostRequestAsync(url, User_Agent, postdata);
            return pr.StartsWith("ERROR") ? new delegates_forging_enable_response {error = pr, success = false} : JsonConvert.DeserializeObject<delegates_forging_enable_response>(pr);
        }

        /// <summary>
        /// Disable forging.
        /// </summary>
        /// <param name="secret">secret key of delegate account</param>
        /// <returns></returns>
        public async Task<delegates_forging_disable_response> Delegates_DisableForging(string secret)
        {
            const string url = "/api/delegates/forging/disable";
            var postdata = new List<KeyValuePair<string, string>> {new KeyValuePair<string, string>("secret", secret)};
            var pr = await HttpPostRequestAsync(url, User_Agent, postdata);
            return pr.StartsWith("ERROR") ? new delegates_forging_disable_response {error = pr, success = false} : JsonConvert.DeserializeObject<delegates_forging_disable_response>(pr);
        }

        /// <summary>
        /// Registers a app.
        /// </summary>
        /// <param name="secret">Secret of account.</param>
        /// <param name="name">DApp name.</param>
        /// <param name="link">Link to DApp file. ZIP supported.</param>
        /// <param name="secondSecret">Second secret of account.</param>
        /// <param name="publicKey">Public key to verify sender secret key. Hex.</param>
        /// <param name="category">DApp category.</param>
        /// <param name="description">DApp description.</param>
        /// <param name="tags">DApp tags.</param>
        /// <param name="type">DApp type. (Only type 0 is currently supported)</param>
        /// <param name="icon">Link to icon file. PNG and JPG/JPEG supported.</param>
        /// <returns></returns>
        public async Task<dapps_register_response> Dapps_Register(string secret, string name, string link,
            string secondSecret = "",
            string publicKey = "",
            int category = 0, string description = "", string tags = "", int type = 0, string icon = "")
        {
            const string url = "/api/dapps";

            var sb = new StringBuilder();
            sb.Append("{\"secret\":\"" + secret + "\",\"name\":\"" + name + "\",\"link\":\"" + link + "\",\"category\":" +
                      category + ",\"type\":" + type);
            if (!string.IsNullOrEmpty(secondSecret))
                sb.Append(",\"secondSecret\":\"" + secondSecret + "\"");
            if (!string.IsNullOrEmpty(publicKey))
                sb.Append(",\"publicKey\":\"" + publicKey + "\"");
            if (!string.IsNullOrEmpty(description))
                sb.Append(",\"description\":\"" + description + "\"");
            if (!string.IsNullOrEmpty(tags))
                sb.Append(",\"tags\":\"" + tags + "\"");
            if (!string.IsNullOrEmpty(icon))
                sb.Append(",\"icon\":\"" + icon + "\"");

            var pr = await HttpPutRequestAsync(url, User_Agent, sb.ToString());
            return pr.StartsWith("ERROR") ? new dapps_register_response {error = pr, success = false} : JsonConvert.DeserializeObject<dapps_register_response>(pr);
        }

        /// <summary>
        /// Gets a list of apps registered on the network.
        /// </summary>
        /// <param name="category">DApp category.</param>
        /// <param name="name">DApp name.</param>
        /// <param name="type">DApp type.</param>
        /// <param name="link">DApp link.</param>
        /// <param name="limit">Query limit. Maximum is 100, Default is 20</param>
        /// <param name="offset">Query offset.</param>
        /// <param name="orderBy">Order by field.</param>
        /// <returns></returns>
        public async Task<dapps_getList_response> Dapps_GetList(int? category = null, string name = "", int? type = null,
            string link = "", int limit = 20, int? offset = null, string orderBy = "")
        {
            if (limit > 100)
                limit = 100;
            var url = "/api/delegates?limit=" + limit;
            if (category != null && category >= 0)
            {
                url += url.EndsWith("?") || url.EndsWith("&") ? "category=" + category : "&category=" + category;
            }
            if (!string.IsNullOrEmpty(name))
            {
                url += url.EndsWith("?") || url.EndsWith("&") ? "name=" + name : "&name=" + name;
            }
            if (offset != null && offset > 0)
            {
                url += url.EndsWith("?") || url.EndsWith("&") ? "offset=" + offset : "&offset=" + offset;
            }
            if (type != null)
            {
                url += url.EndsWith("?") || url.EndsWith("&") ? "type=" + type : "&type=" + type;
            }
            if (!string.IsNullOrEmpty(link))
            {
                url += url.EndsWith("?") || url.EndsWith("&") ? "link=" + link : "&link=" + link;
            }
            if (!string.IsNullOrEmpty(orderBy))
            {
                url += url.EndsWith("?") || url.EndsWith("&")
                    ? "orderBy=" + WebUtility.UrlEncode(orderBy)
                    : "&orderBy=" + WebUtility.UrlEncode(orderBy);
            }
            var gr = await HttpGetRequestAsync(url, User_Agent);
            return gr.StartsWith("ERROR") ? new dapps_getList_response {error = gr, success = false} : JsonConvert.DeserializeObject<dapps_getList_response>(gr);
        }

        /// <summary>
        /// Gets a specific app by id.
        /// </summary>
        /// <param name="id">Id of app.</param>
        /// <returns></returns>
        public async Task<dapps_get_response> Dapps_Get(string id)
        {
            var url = "/api/dapps/get?id=" + WebUtility.UrlEncode(id);
            var gr = await HttpGetRequestAsync(url, User_Agent);
            return gr.StartsWith("ERROR") ? new dapps_get_response {error = gr, success = false} : JsonConvert.DeserializeObject<dapps_get_response>(gr);
        }

        /// <summary>
        /// Searches for apps by keyword(s).
        /// </summary>
        /// <param name="query">Search criteria.</param>
        /// <param name="category">Category to search within.</param>
        /// <param name="installed">Search installed apps only. Default false</param>
        /// <returns></returns>
        public async Task<dapps_search_response> Dapps_Search(string query, int category, bool installed = false)
        {
            var url = "/api/dapps/search?q=" + WebUtility.UrlEncode(query) + "&category=" + category + "&installed=" +
                      Convert.ToInt32(installed);
            var gr = await HttpGetRequestAsync(url, User_Agent);
            return gr.StartsWith("ERROR") ? new dapps_search_response {error = gr, success = false} : JsonConvert.DeserializeObject<dapps_search_response>(gr);
        }

        /// <summary>
        /// Installs a app by id on the node.
        /// </summary>
        /// <param name="appId">dapp id to install</param>
        /// <returns></returns>
        public async Task<dapps_install_response> Dapps_Install(string appId)
        {
            const string url = "/api/dapps/install";
            var postdata = new List<KeyValuePair<string, string>> {new KeyValuePair<string, string>("id", appId)};
            var pr = await HttpPostRequestAsync(url, User_Agent, postdata);
            return pr.StartsWith("ERROR") ? new dapps_install_response {error = pr, success = false} : JsonConvert.DeserializeObject<dapps_install_response>(pr);
        }

        /// <summary>
        /// Returns a list of installed apps on the requested node.
        /// </summary>
        /// <returns></returns>
        public async Task<dapps_installed_response> Dapps_Insalled()
        {
            const string url = "/api/dapps/installed";
            var gr = await HttpGetRequestAsync(url, User_Agent);
            return gr.StartsWith("ERROR") ? new dapps_installed_response { error = gr, success = false } : JsonConvert.DeserializeObject<dapps_installed_response>(gr);
        }

        /// <summary>
        /// Returns a list of installed app ids on the requested node.
        /// </summary>
        /// <returns></returns>
        public async Task<dapps_installedIds_response> Dapps_InsalledIds()
        {
            const string url = "/api/dapps/installedIds";
            var gr = await HttpGetRequestAsync(url, User_Agent);
            return gr.StartsWith("ERROR") ? new dapps_installedIds_response {error = gr, success = false} : JsonConvert.DeserializeObject<dapps_installedIds_response>(gr);
        }

        /// <summary>
        /// Uninstalls a app by id from the requested node.
        /// </summary>
        /// <param name="appId">dapp id to uninstall</param>
        /// <returns></returns>
        public async Task<dapps_uninstall_response> Dapps_Uninstall(string appId)
        {
            const string url = "/api/dapps/uninstall";
            var postdata = new List<KeyValuePair<string, string>> {new KeyValuePair<string, string>("id", appId)};
            var pr = await HttpPostRequestAsync(url, User_Agent, postdata);
            return pr.StartsWith("ERROR") ? new dapps_uninstall_response {error = pr, success = false} : JsonConvert.DeserializeObject<dapps_uninstall_response>(pr);
        }

        /// <summary>
        /// Launches a app by id on the requested node.
        /// </summary>
        /// <param name="appId">dapp id to launch</param>
        /// <param name="params">dapp launch params</param>
        /// <returns></returns>
        public async Task<dapps_launch_response> Dapps_Launch(string appId, string @params = "")
        {
            const string url = "/api/dapps/launch";
            var postdata = new List<KeyValuePair<string, string>>();
            postdata.Add(new KeyValuePair<string, string>("id", appId));
            if (!string.IsNullOrEmpty(@params))
                postdata.Add(new KeyValuePair<string, string>("params", @params));
            var pr = await HttpPostRequestAsync(url, User_Agent, postdata);
            return pr.StartsWith("ERROR") ? new dapps_launch_response {error = pr, success = false} : JsonConvert.DeserializeObject<dapps_launch_response>(pr);
        }

        /// <summary>
        /// Returns a list of app ids currently being installed on the requested node.
        /// </summary>
        /// <returns></returns>
        public async Task<dapps_installing_response> Dapps_Insalling()
        {
            const string url = "/api/dapps/installing";
            var gr = await HttpGetRequestAsync(url, User_Agent);
            return gr.StartsWith("ERROR") ? new dapps_installing_response {error = gr, success = false} : JsonConvert.DeserializeObject<dapps_installing_response>(gr);
        }

        /// <summary>
        /// Returns a list of app ids currently being uninstalled on the requested node.
        /// </summary>
        /// <returns></returns>
        public async Task<dapps_uninstalling_response> Dapps_Uninstalling()
        {
            const string url = "/api/dapps/uninstalling";
            var gr = await HttpGetRequestAsync(url, User_Agent);
            return gr.StartsWith("ERROR") ? new dapps_uninstalling_response {error = gr, success = false} : JsonConvert.DeserializeObject<dapps_uninstalling_response>(gr);
        }

        /// <summary>
        /// Returns a list of app ids which are currently launched on the requested node.
        /// </summary>
        /// <returns></returns>
        public async Task<dapps_launched_response> Dapps_Launched()
        {
            const string url = "/api/dapps/launched";
            var gr = await HttpGetRequestAsync(url, User_Agent);
            return gr.StartsWith("ERROR") ? new dapps_launched_response {error = gr, success = false} : JsonConvert.DeserializeObject<dapps_launched_response>(gr);
        }

        /// <summary>
        /// Returns a full list of app categories.
        /// </summary>
        /// <returns></returns>
        public async Task<dapps_categories_response> Dapps_Categories()
        {
            const string url = "/api/dapps/categories";
            var gr = await HttpGetRequestAsync(url, User_Agent);
            return gr.StartsWith("ERROR") ? new dapps_categories_response {error = gr, success = false} : JsonConvert.DeserializeObject<dapps_categories_response>(gr);
        }

        /// <summary>
        /// Stops a app by id on the requested node.
        /// </summary>
        /// <param name="appId">dapp id to stop</param>
        /// <returns></returns>
        public async Task<dapps_stop_response> Dapps_Stop(string appId)
        {
            const string url = "/api/dapps/stop";
            var postdata = new List<KeyValuePair<string, string>> {new KeyValuePair<string, string>("id", appId)};
            var pr = await HttpPostRequestAsync(url, User_Agent, postdata);
            return pr.StartsWith("ERROR") ? new dapps_stop_response {error = pr, success = false} : JsonConvert.DeserializeObject<dapps_stop_response>(pr);
        }

        /// <summary>
        /// Return multisig transaction that waiting for your signature.
        /// </summary>
        /// <param name="publicKey">Public key of account</param>
        /// <returns></returns>
        public async Task<multisignatures_pending_response> Multisignatures_Pending(string publicKey)
        {
            var url = "/api/multisignatures/pending?publicKey=" + WebUtility.UrlEncode(publicKey);
            var gr = await HttpGetRequestAsync(url, User_Agent);
            return gr.StartsWith("ERROR") ? new multisignatures_pending_response {error = gr, success = false} : JsonConvert.DeserializeObject<multisignatures_pending_response>(gr);
        }

        /// <summary>
        /// Create a multisignature account.
        /// </summary>
        /// <param name="secret">your secret. string.</param>
        /// <param name="keysGroup">[array of public keys strings]. add '+' before publicKey to add an account or '-' to remove.</param>
        /// <param name="lifetime">request lifetime in hours (1-24). Default 1</param>
        /// <param name="min">minimum signatures needed to approve a tx or a change (1-15). Default 1</param>
        /// <returns></returns>
        public async Task<multisignatures_createAccount_response> Multisignatures_Create(string secret, string keysGroup,
            int lifetime = 1, int min = 1)
        {
            if (lifetime < 1)
                lifetime = 1;
            if (lifetime > 24)
                lifetime = 24;
            if (min < 1)
                min = 1;
            if (min > 15)
                min = 15;
            if (string.IsNullOrEmpty(keysGroup))
                return new multisignatures_createAccount_response {error = "ERROR: Invalid keysGroup", success = false};
            const string url = "/api/multisignatures";
            var json = "{\"secret\":\"" + secret + "\",\"lifetime\":" + lifetime + ",\"min\":" + min +
                       ",\"keysgroup\":\"" + keysGroup + "\"";

            var pr = await HttpPutRequestAsync(url, User_Agent, json);
            return pr.StartsWith("ERROR") ? new multisignatures_createAccount_response {error = pr, success = false} : JsonConvert.DeserializeObject<multisignatures_createAccount_response>(pr);
        }

        /// <summary>
        /// Sign transaction that wait for your signature.
        /// </summary>
        /// <param name="secret">your secret. string.</param>
        /// <param name="transactionId">id of transaction to sign</param>
        /// <param name="publicKey">public key of your account</param>
        /// <returns></returns>
        public async Task<multisignatures_sign_response> Multisignatures_Sign(string secret,string transactionId, string publicKey = "")
        {
            const string url = "/api/multisignatures/sign";
            var postdata = new List<KeyValuePair<string, string>>
            {
                new KeyValuePair<string, string>("secret", secret),
                new KeyValuePair<string, string>("transactionId", transactionId)
            };
            if(!string.IsNullOrEmpty(publicKey))
                postdata.Add(new KeyValuePair<string, string>("publicKey", publicKey));

            var pr = await HttpPostRequestAsync(url, User_Agent, postdata);
            return pr.StartsWith("ERROR") ? new multisignatures_sign_response {error = pr, success = false} : JsonConvert.DeserializeObject<multisignatures_sign_response>(pr);
        }

        /// <summary>
        /// Get accounts of multisignature.
        /// </summary>
        /// <param name="publicKey">Public key of multi-signature account.</param>
        /// <returns></returns>
        public async Task<multisignatures_accounts_response> Multisignatures_Accounts(string publicKey)
        {
            var url = "/api/multisignatures/accounts?publicKey=" + WebUtility.UrlEncode(publicKey);
            var gr = await HttpGetRequestAsync(url, User_Agent);
            return gr.StartsWith("ERROR") ? new multisignatures_accounts_response {error = gr, success = false} : JsonConvert.DeserializeObject<multisignatures_accounts_response>(gr);
        }

        /// <summary>
        /// Generates a new BIP39 secret. In lisk this is the equivalent of creating a new account.
        /// </summary>
        /// <returns>A BIP39 object with the MnemonicSentence which is the secret</returns>
        public async Task<BIP39> GenerateNewSecret()
        {
            var trys = 0;
            RETRY:
            var ni = new BIP39();
            // verify that the secret is good
            var tres = await Accounts_Open(ni.MnemonicSentence);
            if (tres != null && tres.success)
                return ni;
            // keep trying until we verify the secret is good or we have passed 12 tries
            trys++;
            // sleep for a second in case this is a temp network issue
            Task.Delay(1000).Wait();
            if (trys <= 12)
                goto RETRY;
            throw new Exception("Unable to generate or verify a new BIP39 secret");
        }

        /// <summary>
        /// Converts a Lisk long string value into a decimal which can be used by your program.
        /// </summary>
        /// <param name="value">The lisk long string value</param>
        /// <returns>The value as a decimal</returns>
        public static decimal LSKLongToDecimal(string value)
        {
            var div = new BigInteger("100000000");
            var bih = new BigInteger(value);
            var res = bih.DivideAndRemainder(div);
            var sstr = res[0].ToString();
            var istr = res[1].ToString();
            var cstr = istr.Length > 8 ? sstr + "." + istr.Substring(0, 8) : sstr + "." + istr;
            return decimal.Parse(cstr, CultureInfo.InvariantCulture);
        }

        /// <summary>
        /// Converts a decimal value into a biginteger which can be used by Lisk.
        /// </summary>
        /// <param name="value">The decimal to convert</param>
        /// <returns>The value as a biginteger</returns>
        public static BigInteger LSKDecimalToLong(decimal value)
        {
            return new BigInteger(value.ToString("F8").Replace(".", ""));
        }

        /// <summary>
        /// Converts a Lisk timestamp to a datetime object which can be used by your program.
        /// </summary>
        /// <param name="timestamp">The Lisk timestamp to convert</param>
        /// <returns>The value as datetime object</returns>
        public static DateTime LSKTimestampToDateTime(string timestamp)
        {
            var ts = long.Parse(timestamp);
            return new DateTime(2016, 5, 24, 17, 0, 0, 0).AddSeconds(ts);
        }

        /// <summary>
        /// Performs a basic check on an address to help ensure it is valid.
        /// </summary>
        /// <param name="address">The address to check</param>
        /// <returns></returns>
        public static bool IsAddressValid(string address)
        {
            var len = address.Length;
            return !address.Contains(" ") && (len <= 21 && len >= 20 && address.EndsWith("L"));
        }

        private async Task<string> HttpGetRequestAsync(string url, string ua)
        {
            var client = new HttpClient();
            client.DefaultRequestHeaders.Add("User-Agent", ua);

            var response = await client.GetAsync(Server_Url + url);

            if (!response.IsSuccessStatusCode)
                return "ERROR:" + response.StatusCode + " " + response.ReasonPhrase + " | " + response.RequestMessage;
            var res = await response.Content.ReadAsStringAsync();
            return res;
        }

        private async Task<string> HttpPostRequestAsync(string url, string ua,
            List<KeyValuePair<string, string>> postdata)
        {
            var client = new HttpClient();
            client.DefaultRequestHeaders.Add("User-Agent", ua);

            var content = new FormUrlEncodedContent(postdata);

            var response = await client.PostAsync(Server_Url + url, content);

            return response.IsSuccessStatusCode
                ? await response.Content.ReadAsStringAsync()
                : "ERROR:" + response.StatusCode + " " + response.ReasonPhrase + " | " + response.RequestMessage;
        }

        private async Task<string> HttpPutRequestAsync(string url, string ua, string putdata)
        {
            var client = new HttpClient();
            client.DefaultRequestHeaders.Add("User-Agent", ua);

            var content = new StringContent(putdata, Encoding.UTF8, "application/json");

            var response = await client.PutAsync(Server_Url + url, content);

            return response.IsSuccessStatusCode
                ? await response.Content.ReadAsStringAsync()
                : "ERROR:" + response.StatusCode + " " + response.ReasonPhrase + " | " + response.RequestMessage;
        }
    }
}