//https://lisk.io/documentation?i=lisk-docs/APIReference
//Author: Allen Byron Penner
// BIP39 can be found at https://github.com/ByronAP/BIP39.NET-Portable

using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading;
using System.Web;
using BigMath;
using Bitcoin.BIP39;
using Lisk.API.Responses;
using Newtonsoft.Json;

namespace Lisk.API
{
    public class LiskAPI
    {
        public LiskAPI()
        {
            User_Agent =
                "Mozilla/5.0 (Windows NT 6.1) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/41.0.2228.0 Safari/537.36";
            Server_Url = "https://www.liskwallet.info";
        }

        public LiskAPI(string serverBaseUrl)
        {
            User_Agent =
                "Mozilla/5.0 (Windows NT 6.1) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/41.0.2228.0 Safari/537.36";
            Server_Url = serverBaseUrl;
        }

        public LiskAPI(string serverBaseUrl, string userAgent)
        {
            User_Agent = userAgent;
            Server_Url = serverBaseUrl;
        }

        public string User_Agent { get; set; }
        public string Server_Url { get; set; }

        public accounts_open_response Accounts_Open(string secret)
        {
            var url = "/api/accounts/open";
            var postdata = new List<KeyValuePair<string, string>> {new KeyValuePair<string, string>("secret", secret)};
            var pr = HttpPostRequest(url, User_Agent, postdata);
            if (pr.StartsWith("ERROR"))
                return new accounts_open_response {error = pr, success = false};
            return JsonConvert.DeserializeObject<accounts_open_response>(pr);
        }

        public accounts_getBalance_response Accounts_GetBalance(string address)
        {
            var url = "/api/accounts/getBalance?address=" + HttpUtility.UrlEncode(address);
            var gr = HttpGetRequest(url, User_Agent);
            if (gr.StartsWith("ERROR"))
                return new accounts_getBalance_response {error = gr, success = false};
            return JsonConvert.DeserializeObject<accounts_getBalance_response>(gr);
        }

        public accounts_getPublicKey_response Accounts_GetPublicKey(string address)
        {
            var url = "/api/accounts/getPublicKey?address=" + HttpUtility.UrlEncode(address);
            var gr = HttpGetRequest(url, User_Agent);
            if (gr.StartsWith("ERROR"))
                return new accounts_getPublicKey_response {error = gr, success = false};
            return JsonConvert.DeserializeObject<accounts_getPublicKey_response>(gr);
        }

        public accounts_generatePublicKey_response Accounts_GeneratePublicKey(string secret)
        {
            var url = "/api/accounts/generatePublicKey";
            var postdata = new List<KeyValuePair<string, string>> {new KeyValuePair<string, string>("secret", secret)};
            var pr = HttpPostRequest(url, User_Agent, postdata);
            if (pr.StartsWith("ERROR"))
                return new accounts_generatePublicKey_response {error = pr, success = false};
            return JsonConvert.DeserializeObject<accounts_generatePublicKey_response>(pr);
        }

        public accounts_getAccount_response Accounts_GetAccount(string address)
        {
            var url = "/api/accounts?address=" + HttpUtility.UrlEncode(address);
            var gr = HttpGetRequest(url, User_Agent);
            if (gr.StartsWith("ERROR"))
                return new accounts_getAccount_response {error = gr, success = false};
            return JsonConvert.DeserializeObject<accounts_getAccount_response>(gr);
        }

        public accounts_getDelegates_response Accounts_GetDelegates(string address)
        {
            var url = "/api/accounts/delegates?address=" + HttpUtility.UrlEncode(address);
            var gr = HttpGetRequest(url, User_Agent);
            if (gr.StartsWith("ERROR"))
                return new accounts_getDelegates_response {error = gr, success = false};
            return JsonConvert.DeserializeObject<accounts_getDelegates_response>(gr);
        }

        public accounts_putDelegates_response Accounts_PutDelegates(string secret, string publicKey, string secondSecret,
            string delegates)
        {
            var url = "/api/accounts/delegates";
            var postdata = new List<KeyValuePair<string, string>>
            {
                new KeyValuePair<string, string>("secret", secret),
                new KeyValuePair<string, string>("publicKey", publicKey),
                new KeyValuePair<string, string>("secondSecret", secondSecret),
                new KeyValuePair<string, string>("delegates", delegates)
            };
            var pr = HttpPostRequest(url, User_Agent, postdata);
            if (pr.StartsWith("ERROR"))
                return new accounts_putDelegates_response {error = pr, success = false};
            return JsonConvert.DeserializeObject<accounts_putDelegates_response>(pr);
        }

        public loader_status_response Loader_Status()
        {
            var url = "/api/loader/status";
            var gr = HttpGetRequest(url, User_Agent);
            if (gr.StartsWith("ERROR"))
                return new loader_status_response {error = gr, success = false};
            return JsonConvert.DeserializeObject<loader_status_response>(gr);
        }

        public loader_status_sync_response Loader_SyncStatus()
        {
            var url = "/api/loader/status/sync";
            var gr = HttpGetRequest(url, User_Agent);
            if (gr.StartsWith("ERROR"))
                return new loader_status_sync_response {error = gr, success = false};
            return JsonConvert.DeserializeObject<loader_status_sync_response>(gr);
        }

        public transactions_getList_response Transactions_GetList(string blockId = "", string senderId = "",
            string recipientId = "", int limit = 20, int offset = 0, string orderBy = "")
        {
            var url = "/api/transactions?";
            if (!string.IsNullOrEmpty(blockId))
                url += "blockId=" + HttpUtility.UrlEncode(blockId);
            if (!string.IsNullOrEmpty(senderId))
            {
                if (url.EndsWith("?") || url.EndsWith("&"))
                    url += "senderId=" + HttpUtility.UrlEncode(senderId);
                else
                    url += "&senderId=" + HttpUtility.UrlEncode(senderId);
            }
            if (!string.IsNullOrEmpty(recipientId))
            {
                if (url.EndsWith("?") || url.EndsWith("&"))
                    url += "recipientId=" + HttpUtility.UrlEncode(recipientId);
                else
                    url += "&recipientId=" + HttpUtility.UrlEncode(recipientId);
            }
            if (url.EndsWith("?") || url.EndsWith("&"))
                url += "limit=" + limit;
            else
                url += "&limit=" + limit;
            if (url.EndsWith("?") || url.EndsWith("&"))
                url += "offset=" + offset;
            else
                url += "&offset=" + offset;
            if (!string.IsNullOrEmpty(orderBy))
            {
                if (url.EndsWith("?") || url.EndsWith("&"))
                    url += "orderBy=" + HttpUtility.UrlEncode(orderBy);
                else
                    url += "&orderBy=" + HttpUtility.UrlEncode(orderBy);
            }
            var gr = HttpGetRequest(url, User_Agent);
            if (gr.StartsWith("ERROR"))
                return new transactions_getList_response {error = gr, success = false};
            return JsonConvert.DeserializeObject<transactions_getList_response>(gr);
        }

        public transactions_send_response Transactions_Send(string secret, long amount, string recipientId,
            string publicKey, string secondSecret)
        {
            var url = "/api/transactions";
            var postdata = new List<KeyValuePair<string, string>>
            {
                new KeyValuePair<string, string>("secret", secret),
                new KeyValuePair<string, string>("amount", amount.ToString()),
                new KeyValuePair<string, string>("recipientId", recipientId),
                new KeyValuePair<string, string>("publicKey", publicKey),
                new KeyValuePair<string, string>("secondSecret", secondSecret)
            };
            var pr = HttpPostRequest(url, User_Agent, postdata);
            if (pr.StartsWith("ERROR"))
                return new transactions_send_response {error = pr, success = false};
            return JsonConvert.DeserializeObject<transactions_send_response>(pr);
        }

        public transactions_get_response Transactions_Get(string id)
        {
            var url = "/api/transactions/get?id=" + HttpUtility.UrlEncode(id);
            var gr = HttpGetRequest(url, User_Agent);
            if (gr.StartsWith("ERROR"))
                return new transactions_get_response {error = gr, success = false};
            return JsonConvert.DeserializeObject<transactions_get_response>(gr);
        }

        public transactions_getUnconfirmed_response Transactions_GetUnconfirmed(string id)
        {
            var url = "/api/transactions/unconfirmed?id=" + HttpUtility.UrlEncode(id);
            var gr = HttpGetRequest(url, User_Agent);
            if (gr.StartsWith("ERROR"))
                return new transactions_getUnconfirmed_response {error = gr, success = false};
            return JsonConvert.DeserializeObject<transactions_getUnconfirmed_response>(gr);
        }

        public transactions_getListUnconfirmed_response Transactions_GetListUnconfirmed()
        {
            var url = "/api/transactions/unconfirmed";
            var gr = HttpGetRequest(url, User_Agent);
            if (gr.StartsWith("ERROR"))
                return new transactions_getListUnconfirmed_response {error = gr, success = false};
            return JsonConvert.DeserializeObject<transactions_getListUnconfirmed_response>(gr);
        }

        public peers_getList_response Peers_GetList(int? state = null, string os = "", bool? shared = null,
            string version = "", int limit = 20, int? offset = null, string orderBy = "")
        {
            if (limit > 100)
                limit = 100;
            var url = "/api/peers?";
            if (state != null && state >= 0 && state <= 3)
                url += "state=" + state;
            if (!string.IsNullOrEmpty(os))
            {
                if (url.EndsWith("?") || url.EndsWith("&"))
                    url += "os=" + HttpUtility.UrlEncode(os);
                else
                    url += "&os=" + HttpUtility.UrlEncode(os);
            }
            if (shared != null)
            {
                if (url.EndsWith("?") || url.EndsWith("&"))
                    url += "shared=" + shared.ToString().ToLower();
                else
                    url += "&shared=" + shared.ToString().ToLower();
            }
            if (!string.IsNullOrEmpty(version))
            {
                if (url.EndsWith("?") || url.EndsWith("&"))
                    url += "version=" + HttpUtility.UrlEncode(version);
                else
                    url += "&version=" + HttpUtility.UrlEncode(version);
            }
            if (url.EndsWith("?") || url.EndsWith("&"))
                url += "limit=" + limit;
            else
                url += "&limit=" + limit;
            if (offset != null && offset > 0)
            {
                if (url.EndsWith("?") || url.EndsWith("&"))
                    url += "offset=" + offset;
                else
                    url += "&offset=" + offset;
            }
            if (!string.IsNullOrEmpty(orderBy))
            {
                if (url.EndsWith("?") || url.EndsWith("&"))
                    url += "orderBy=" + HttpUtility.UrlEncode(orderBy);
                else
                    url += "&orderBy=" + HttpUtility.UrlEncode(orderBy);
            }
            var gr = HttpGetRequest(url, User_Agent);
            if (gr.StartsWith("ERROR"))
                return new peers_getList_response {error = gr, success = false};
            return JsonConvert.DeserializeObject<peers_getList_response>(gr);
        }

        public peers_get_response Peers_Get(string ip, int port)
        {
            var url = "/api/peers/get?ip=" + HttpUtility.UrlEncode(ip) + "&port=" + port;
            var gr = HttpGetRequest(url, User_Agent);
            if (gr.StartsWith("ERROR"))
                return new peers_get_response {error = gr, success = false};
            return JsonConvert.DeserializeObject<peers_get_response>(gr);
        }

        public peers_getVersion_response Peers_GetVersion()
        {
            var url = "/api/peers/version";
            var gr = HttpGetRequest(url, User_Agent);
            if (gr.StartsWith("ERROR"))
                return new peers_getVersion_response {error = gr, success = false};
            return JsonConvert.DeserializeObject<peers_getVersion_response>(gr);
        }

        public blocks_get_response Blocks_Get(string id)
        {
            var url = "/api/blocks/get?id=" + HttpUtility.UrlEncode(id);
            var gr = HttpGetRequest(url, User_Agent);
            if (gr.StartsWith("ERROR"))
                return new blocks_get_response {error = gr, success = false};
            return JsonConvert.DeserializeObject<blocks_get_response>(gr);
        }

        public blocks_getList_response Blocks_GetList(string generatorPublicKey = "", long? height = null,
            long? previousBlock = null, long? totalAmount = null, long? totalFee = null,
            int limit = 20, int? offset = null, string orderBy = "")
        {
            var url = "/api/blocks?";
            if (!string.IsNullOrEmpty(generatorPublicKey))
                url += "generatorPublicKey=" + HttpUtility.UrlEncode(generatorPublicKey);
            if (height != null && height >= 0)
            {
                if (url.EndsWith("?") || url.EndsWith("&"))
                    url += "height=" + height;
                else
                    url += "&height=" + height;
            }
            if (previousBlock != null && previousBlock >= 0)
            {
                if (url.EndsWith("?") || url.EndsWith("&"))
                    url += "previousBlock=" + previousBlock;
                else
                    url += "&previousBlock=" + previousBlock;
            }
            if (totalAmount != null && totalAmount >= 0)
            {
                if (url.EndsWith("?") || url.EndsWith("&"))
                    url += "totalAmount=" + totalAmount;
                else
                    url += "&totalAmount=" + totalAmount;
            }
            if (totalFee != null && totalFee >= 0)
            {
                if (url.EndsWith("?") || url.EndsWith("&"))
                    url += "totalFee=" + totalFee;
                else
                    url += "&totalFee=" + totalFee;
            }
            if (limit > 0)
            {
                if (url.EndsWith("?") || url.EndsWith("&"))
                    url += "limit=" + totalFee;
                else
                    url += "&limit=" + totalFee;
            }
            if (offset != null && offset > 0)
            {
                if (url.EndsWith("?") || url.EndsWith("&"))
                    url += "offset=" + offset;
                else
                    url += "&offset=" + offset;
            }
            if (!string.IsNullOrEmpty(orderBy))
            {
                if (url.EndsWith("?") || url.EndsWith("&"))
                    url += "orderBy=" + HttpUtility.UrlEncode(orderBy);
                else
                    url += "&orderBy=" + HttpUtility.UrlEncode(orderBy);
            }
            var gr = HttpGetRequest(url, User_Agent);
            if (gr.StartsWith("ERROR"))
                return new blocks_getList_response {error = gr, success = false};
            return JsonConvert.DeserializeObject<blocks_getList_response>(gr);
        }

        public blocks_getFee_response Blocks_GetFee()
        {
            var url = "/api/blocks/getFee";
            var gr = HttpGetRequest(url, User_Agent);
            if (gr.StartsWith("ERROR"))
                return new blocks_getFee_response {error = gr, success = false};
            return JsonConvert.DeserializeObject<blocks_getFee_response>(gr);
        }

        public blocks_getHeight_response Blocks_GetHeight()
        {
            var url = "/api/blocks/getHeight";
            var gr = HttpGetRequest(url, User_Agent);
            if (gr.StartsWith("ERROR"))
                return new blocks_getHeight_response {error = gr, success = false};
            return JsonConvert.DeserializeObject<blocks_getHeight_response>(gr);
        }

        public delegates_forging_getForgedByAccount_response Delegates_GetForgedByAccount(string generatorPublicKey)
        {
            var url = "/api/delegates/forging/getForgedByAccount?generatorPublicKey=" +
                      HttpUtility.UrlEncode(generatorPublicKey);
            var gr = HttpGetRequest(url, User_Agent);
            if (gr.StartsWith("ERROR"))
                return new delegates_forging_getForgedByAccount_response {error = gr, success = false};
            return JsonConvert.DeserializeObject<delegates_forging_getForgedByAccount_response>(gr);
        }

        public signatures_get_response Signatures_Get(string id)
        {
            var url = "/api/signatures/get?id=" + HttpUtility.UrlEncode(id);
            var gr = HttpGetRequest(url, User_Agent);
            if (gr.StartsWith("ERROR"))
                return new signatures_get_response {error = gr, success = false};
            return JsonConvert.DeserializeObject<signatures_get_response>(gr);
        }

        public signatures_add_response Signatures_Add(string secret, string secondSecret, string publicKey)
        {
            var url = "/api/signatures";
            var postdata = new List<KeyValuePair<string, string>>
            {
                new KeyValuePair<string, string>("secret", secret),
                new KeyValuePair<string, string>("secondsecret", secondSecret),
                new KeyValuePair<string, string>("publicKey", publicKey)
            };
            var pr = HttpPostRequest(url, User_Agent, postdata);
            if (pr.StartsWith("ERROR"))
                return new signatures_add_response {error = pr, success = false};
            return JsonConvert.DeserializeObject<signatures_add_response>(pr);
        }

        public delegates_count_response Delegates_Count()
        {
            var url = "/api/delegates/count";
            var gr = HttpGetRequest(url, User_Agent);
            if (gr.StartsWith("ERROR"))
                return new delegates_count_response {error = gr, success = false};
            return JsonConvert.DeserializeObject<delegates_count_response>(gr);
        }

        public delegates_enable_response Delegates_Enable(string secret, string secondSecret, string username)
        {
            var url = "/api/delegates";
            var postdata = new List<KeyValuePair<string, string>>
            {
                new KeyValuePair<string, string>("secret", secret),
                new KeyValuePair<string, string>("secondsecret", secondSecret),
                new KeyValuePair<string, string>("username", username)
            };
            var pr = HttpPostRequest(url, User_Agent, postdata);
            if (pr.StartsWith("ERROR"))
                return new delegates_enable_response {error = pr, success = false};
            return JsonConvert.DeserializeObject<delegates_enable_response>(pr);
        }

        public delegates_getList_response Delegates_GetList(int limit = 20, int? offset = null, string orderBy = "")
        {
            if (limit > 100)
                limit = 100;
            var url = "/api/delegates?";
            url += "limit=" + limit;
            if (offset != null && offset > 0)
            {
                if (url.EndsWith("?") || url.EndsWith("&"))
                    url += "offset=" + offset;
                else
                    url += "&offset=" + offset;
            }
            if (!string.IsNullOrEmpty(orderBy))
            {
                if (url.EndsWith("?") || url.EndsWith("&"))
                    url += "orderBy=" + HttpUtility.UrlEncode(orderBy);
                else
                    url += "&orderBy=" + HttpUtility.UrlEncode(orderBy);
            }
            var gr = HttpGetRequest(url, User_Agent);
            if (gr.StartsWith("ERROR"))
                return new delegates_getList_response {error = gr, success = false};
            return JsonConvert.DeserializeObject<delegates_getList_response>(gr);
        }

        public delegates_get_response Delegates_Get(string publicKey)
        {
            var url = "/api/delegates/get?publicKey=" + HttpUtility.UrlEncode(publicKey);
            var gr = HttpGetRequest(url, User_Agent);
            if (gr.StartsWith("ERROR"))
                return new delegates_get_response {error = gr, success = false};
            return JsonConvert.DeserializeObject<delegates_get_response>(gr);
        }

        public delegates_getVotes_response Delegates_GetVotes(string address)
        {
            var url = "/api/accounts/delegates/?address=" + HttpUtility.UrlEncode(address);
            var gr = HttpGetRequest(url, User_Agent);
            if (gr.StartsWith("ERROR"))
                return new delegates_getVotes_response {error = gr, success = false};
            return JsonConvert.DeserializeObject<delegates_getVotes_response>(gr);
        }

        public delegates_getVoters_response Delegates_GetVoters(string publicKey)
        {
            var url = "/api/delegates/voters?publicKey=" + HttpUtility.UrlEncode(publicKey);
            var gr = HttpGetRequest(url, User_Agent);
            if (gr.StartsWith("ERROR"))
                return new delegates_getVoters_response {error = gr, success = false};
            return JsonConvert.DeserializeObject<delegates_getVoters_response>(gr);
        }

        public delegates_forging_enable_response Delegates_EnableForging(string secret)
        {
            var url = "/api/delegates/forging/enable";
            var postdata = new List<KeyValuePair<string, string>> {new KeyValuePair<string, string>("secret", secret)};
            var pr = HttpPostRequest(url, User_Agent, postdata);
            if (pr.StartsWith("ERROR"))
                return new delegates_forging_enable_response {error = pr, success = false};
            return JsonConvert.DeserializeObject<delegates_forging_enable_response>(pr);
        }

        public delegates_forging_disable_response Delegates_DisableForging(string secret)
        {
            var url = "/api/delegates/forging/disable";
            var postdata = new List<KeyValuePair<string, string>> {new KeyValuePair<string, string>("secret", secret)};
            var pr = HttpPostRequest(url, User_Agent, postdata);
            if (pr.StartsWith("ERROR"))
                return new delegates_forging_disable_response {error = pr, success = false};
            return JsonConvert.DeserializeObject<delegates_forging_disable_response>(pr);
        }

        public dapps_register_response Dapps_Register(string secret, string name, string link, string secondSecret = "",
            string publicKey = "",
            int category = 0, string description = "", string tags = "", int type = 0, string icon = "")
        {
            var url = "/api/delegates/forging/disable";
            var postdata = new List<KeyValuePair<string, string>>
            {
                new KeyValuePair<string, string>("secret", secret),
                new KeyValuePair<string, string>("name", name),
                new KeyValuePair<string, string>("link", link),
                new KeyValuePair<string, string>("category", category.ToString()),
                new KeyValuePair<string, string>("type", type.ToString())
            };
            if (!string.IsNullOrEmpty(secondSecret))
                postdata.Add(new KeyValuePair<string, string>("secondSecret", secondSecret));
            if (!string.IsNullOrEmpty(publicKey))
                postdata.Add(new KeyValuePair<string, string>("publicKey", publicKey));
            if (!string.IsNullOrEmpty(description))
                postdata.Add(new KeyValuePair<string, string>("description", description));
            if (!string.IsNullOrEmpty(tags))
                postdata.Add(new KeyValuePair<string, string>("tags", tags));
            if (!string.IsNullOrEmpty(icon))
                postdata.Add(new KeyValuePair<string, string>("icon", icon));
            var pr = HttpPostRequest(url, User_Agent, postdata);
            if (pr.StartsWith("ERROR"))
                return new dapps_register_response {error = pr, success = false};
            return JsonConvert.DeserializeObject<dapps_register_response>(pr);
        }

        public dapps_getList_response Dapps_GetList(int? category = null, string name = "", int type = 0,
            string link = "", int limit = 20, int? offset = null, string orderBy = "")
        {
            if (limit > 100)
                limit = 100;
            var url = "/api/delegates?";
            url += "limit=" + limit;
            if (category != null && category >= 0)
            {
                if (url.EndsWith("?") || url.EndsWith("&"))
                    url += "category=" + category;
                else
                    url += "&category=" + category;
            }
            if (!string.IsNullOrEmpty(name))
            {
                if (url.EndsWith("?") || url.EndsWith("&"))
                    url += "category=" + category;
                else
                    url += "&category=" + category;
            }
            if (offset != null && offset > 0)
            {
                if (url.EndsWith("?") || url.EndsWith("&"))
                    url += "name=" + name;
                else
                    url += "&name=" + name;
            }
            if (url.EndsWith("?") || url.EndsWith("&"))
                url += "type=" + type;
            else
                url += "&type=" + type;
            if (!string.IsNullOrEmpty(link))
            {
                if (url.EndsWith("?") || url.EndsWith("&"))
                    url += "link=" + link;
                else
                    url += "&link=" + link;
            }
            if (!string.IsNullOrEmpty(orderBy))
            {
                if (url.EndsWith("?") || url.EndsWith("&"))
                    url += "orderBy=" + HttpUtility.UrlEncode(orderBy);
                else
                    url += "&orderBy=" + HttpUtility.UrlEncode(orderBy);
            }
            var gr = HttpGetRequest(url, User_Agent);
            if (gr.StartsWith("ERROR"))
                return new dapps_getList_response {error = gr, success = false};
            return JsonConvert.DeserializeObject<dapps_getList_response>(gr);
        }

        public dapps_get_response Dapps_Get(string id)
        {
            var url = "/api/dapps/get?id=" + HttpUtility.UrlEncode(id);
            var gr = HttpGetRequest(url, User_Agent);
            if (gr.StartsWith("ERROR"))
                return new dapps_get_response {error = gr, success = false};
            return JsonConvert.DeserializeObject<dapps_get_response>(gr);
        }

        public dapps_search_response Dapps_Search(string query, int category, bool installed = false)
        {
            var url = "/api/dapps/search?q=" + HttpUtility.UrlEncode(query) + "&category=" + category + "&installed=" +
                      Convert.ToInt32(installed);
            var gr = HttpGetRequest(url, User_Agent);
            if (gr.StartsWith("ERROR"))
                return new dapps_search_response {error = gr, success = false};
            return JsonConvert.DeserializeObject<dapps_search_response>(gr);
        }

        public dapps_install_response Dapps_Install(string appId)
        {
            var url = "/api/dapps/install";
            var postdata = new List<KeyValuePair<string, string>> {new KeyValuePair<string, string>("id", appId)};
            var pr = HttpPostRequest(url, User_Agent, postdata);
            if (pr.StartsWith("ERROR"))
                return new dapps_install_response {error = pr, success = false};
            return JsonConvert.DeserializeObject<dapps_install_response>(pr);
        }

        public dapps_installedIds_response Dapps_InsalledIds()
        {
            var url = "/api/dapps/installedIds";
            var gr = HttpGetRequest(url, User_Agent);
            if (gr.StartsWith("ERROR"))
                return new dapps_installedIds_response {error = gr, success = false};
            return JsonConvert.DeserializeObject<dapps_installedIds_response>(gr);
        }

        public dapps_uninstall_response Dapps_Uninstall(string appId)
        {
            var url = "/api/dapps/uninstall";
            var postdata = new List<KeyValuePair<string, string>> {new KeyValuePair<string, string>("id", appId)};
            var pr = HttpPostRequest(url, User_Agent, postdata);
            if (pr.StartsWith("ERROR"))
                return new dapps_uninstall_response {error = pr, success = false};
            return JsonConvert.DeserializeObject<dapps_uninstall_response>(pr);
        }

        public dapps_launch_response Dapps_Launch(string appId, string @params)
        {
            var url = "/api/dapps/launch";
            var postdata = new List<KeyValuePair<string, string>>();
            postdata.Add(new KeyValuePair<string, string>("id", appId));
            if (!string.IsNullOrEmpty(@params))
                postdata.Add(new KeyValuePair<string, string>("params", @params));
            var pr = HttpPostRequest(url, User_Agent, postdata);
            if (pr.StartsWith("ERROR"))
                return new dapps_launch_response {error = pr, success = false};
            return JsonConvert.DeserializeObject<dapps_launch_response>(pr);
        }

        public dapps_installing_response Dapps_Insalling()
        {
            var url = "/api/dapps/installing";
            var gr = HttpGetRequest(url, User_Agent);
            if (gr.StartsWith("ERROR"))
                return new dapps_installing_response {error = gr, success = false};
            return JsonConvert.DeserializeObject<dapps_installing_response>(gr);
        }

        public dapps_uninstalling_response Dapps_Uninsalling()
        {
            var url = "/api/dapps/uninstalling";
            var gr = HttpGetRequest(url, User_Agent);
            if (gr.StartsWith("ERROR"))
                return new dapps_uninstalling_response {error = gr, success = false};
            return JsonConvert.DeserializeObject<dapps_uninstalling_response>(gr);
        }

        public dapps_launched_response Dapps_Launched()
        {
            var url = "/api/dapps/launched";
            var gr = HttpGetRequest(url, User_Agent);
            if (gr.StartsWith("ERROR"))
                return new dapps_launched_response {error = gr, success = false};
            return JsonConvert.DeserializeObject<dapps_launched_response>(gr);
        }

        public dapps_categories_response Dapps_Categories()
        {
            var url = "/api/dapps/categories";
            var gr = HttpGetRequest(url, User_Agent);
            if (gr.StartsWith("ERROR"))
                return new dapps_categories_response {error = gr, success = false};
            return JsonConvert.DeserializeObject<dapps_categories_response>(gr);
        }

        public dapps_stop_response Dapps_Stop(string appId)
        {
            var url = "/api/dapps/stop";
            var postdata = new List<KeyValuePair<string, string>> {new KeyValuePair<string, string>("id", appId)};
            var pr = HttpPostRequest(url, User_Agent, postdata);
            if (pr.StartsWith("ERROR"))
                return new dapps_stop_response {error = pr, success = false};
            return JsonConvert.DeserializeObject<dapps_stop_response>(pr);
        }

        public multisignatures_pending_response Multisignatures_Pending(string publicKey)
        {
            var url = "/api/multisignatures/pending?publicKey=" + HttpUtility.UrlEncode(publicKey);
            var gr = HttpGetRequest(url, User_Agent);
            if (gr.StartsWith("ERROR"))
                return new multisignatures_pending_response {error = gr, success = false};
            return JsonConvert.DeserializeObject<multisignatures_pending_response>(gr);
        }

        public multisignatures_createAccount_response Multisignatures_Create(string secret, string keysGroup,
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
            var url = "/api/multisignatures";
            var postdata = new List<KeyValuePair<string, string>>
            {
                new KeyValuePair<string, string>("secret", secret),
                new KeyValuePair<string, string>("lifetime", lifetime.ToString()),
                new KeyValuePair<string, string>("min", min.ToString()),
                new KeyValuePair<string, string>("keysgroup", keysGroup)
            };
            var pr = HttpPostRequest(url, User_Agent, postdata);
            if (pr.StartsWith("ERROR"))
                return new multisignatures_createAccount_response {error = pr, success = false};
            return JsonConvert.DeserializeObject<multisignatures_createAccount_response>(pr);
        }

        public multisignatures_sign_response Multisignatures_Sign(string secret, string publicKey, string transactionId)
        {
            var url = "/api/multisignatures/sign";
            var postdata = new List<KeyValuePair<string, string>>
            {
                new KeyValuePair<string, string>("secret", secret),
                new KeyValuePair<string, string>("publicKey", publicKey),
                new KeyValuePair<string, string>("transactionId", transactionId)
            };
            var pr = HttpPostRequest(url, User_Agent, postdata);
            if (pr.StartsWith("ERROR"))
                return new multisignatures_sign_response {error = pr, success = false};
            return JsonConvert.DeserializeObject<multisignatures_sign_response>(pr);
        }

        public multisignatures_accounts_response Multisignatures_Accounts(string publicKey)
        {
            var url = "/api/multisignatures/accounts?publicKey=" + HttpUtility.UrlEncode(publicKey);
            var gr = HttpGetRequest(url, User_Agent);
            if (gr.StartsWith("ERROR"))
                return new multisignatures_accounts_response {error = gr, success = false};
            return JsonConvert.DeserializeObject<multisignatures_accounts_response>(gr);
        }

        public BIP39 GenerateNewSecret()
        {
            var trys = 0;
            RETRY:
            var ni = new BIP39();
            // verify that the secret is good
            var tres = Accounts_Open(ni.MnemonicSentence);
            if(tres != null && tres.success)
                return ni;
            // keep trying until we verify the secret is good or we have passed 12 tries
            trys++;
            // sleep for a second in case this is a temp network issue
            Thread.Sleep(1000);
            if(trys <= 12)
                goto RETRY;
            throw new Exception("Unable to generate or verify a new BIP39 secret");
        }

        public decimal LSKLongToDecimal(string value)
        {
            //HACK: but it seems to work
            var div = new BigInteger("100000000");
            var bih = new BigInteger(value);
            var res = bih.DivideAndRemainder(div);
            var sstr = res[0].ToString();
            var istr = res[1].ToString();
            var cstr = "";
            if (istr.Length > 8)
                cstr = sstr + "." + istr.Substring(0, 8);
            else
                cstr = sstr + "." + istr;
            return decimal.Parse(cstr);
        }

        public BigInteger LSKDecimalToLong(decimal value)
        {
            //HACK: but it seems to work
            var trc = Math.Truncate(value);
            var rem = value.ToString().Substring(value.ToString().IndexOf('.') + 1);
            if (rem.Length < 8)
                rem = rem.PadRight(rem.Length + (8 - rem.Length), '0');

            var bint = new BigInteger(trc + rem);
            return bint;
        }

        private string HttpGetRequest(string url, string ua)
        {
            var client = new HttpClient();
            client.DefaultRequestHeaders.Add("User-Agent", ua);

            var response = client.GetAsync(Server_Url + url).Result;

            if (response.IsSuccessStatusCode)
            {
                return response.Content.ReadAsStringAsync().Result;
            }
            return "ERROR:" + response.StatusCode + " " + response.ReasonPhrase + " | " + response.RequestMessage;
        }

        private string HttpPostRequest(string url, string ua, List<KeyValuePair<string, string>> postdata)
        {
            var client = new HttpClient();
            client.DefaultRequestHeaders.Add("User-Agent", ua);

            var content = new FormUrlEncodedContent(postdata);

            var response = client.PostAsync(Server_Url + url, content).Result;

            if (response.IsSuccessStatusCode)
            {
                return response.Content.ReadAsStringAsync().Result;
            }
            return "ERROR:" + response.StatusCode + " " + response.ReasonPhrase + " | " + response.RequestMessage;
        }
    }
}