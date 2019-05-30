using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using FactotumAPI.Helpers;
using FactotumModels;
using Microsoft.AspNetCore.Http;
using Microsoft.IdentityModel.Tokens;
using Newtonsoft.Json;

namespace FactotumWeb.Repository
{
    public class MessageRepository
    {
        public MessageRepository()
        {
        }

        public static async Task<List<EMessage>> Get(string baseURL)
        {

            using (var client = new HttpClient())
            {
                client.DefaultRequestHeaders.Authorization
                       = new AuthenticationHeaderValue("Bearer", SecurityHelper.Instance.SecurityKey);
                try
                {
                    var url = new Uri(baseURL);
                    client.BaseAddress = url;
                    var res = await client.GetAsync("");
                    var str = await res.Content.ReadAsStringAsync();
                    if ((str != null) && (str != string.Empty))
                    {
                        var msgs = JsonConvert.DeserializeObject<List<EMessage>>(str);
                        return msgs;
                    }
                    else
                    {
                        return null;
                    }
                }
                catch(Exception)
                {
                    return null;
                }
            }
        }

        public static async Task<List<MDashBoardMessage>> GetDashBoardMessages(string baseURL)
        {

            using (var client = new HttpClient())
            {
                //client.DefaultRequestHeaders.Authorization
                //       = new AuthenticationHeaderValue("Bearer", SecurityHelper.Instance.SecurityKey);
                try
                {
                    var url = new Uri(baseURL);
                    client.BaseAddress = url;
                    var res = await client.GetAsync("");
                    var str = await res.Content.ReadAsStringAsync();
                    if ((str != null) && (str != string.Empty))
                    {
                        var msgs = JsonConvert.DeserializeObject<List<MDashBoardMessage>>(str);
                        return msgs;
                    }
                    else
                    {
                        return null;
                    }
                }
                catch (Exception)
                {
                    return null;
                }
            }
        }

    }
}
