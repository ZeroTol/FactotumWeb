using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
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

        public static async Task<List<MMessage>> Get(string baseURL)
        {

            using (var client = new HttpClient())
            {
                var sharedKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("mysupers3cr3tsharedkey!"));
                client.DefaultRequestHeaders.Authorization
                         = new AuthenticationHeaderValue("Bearer", sharedKey.ToString());
                try
                {
                    var url = new Uri(baseURL);
                    client.BaseAddress = url;
                    var res = await client.GetAsync("");
                    var str = await res.Content.ReadAsStringAsync();
                    var msgs = JsonConvert.DeserializeObject<List<EMessage>>(str);
                    return null;  //msgs;
                }
                catch(Exception)
                {
                    return null;
                }
            }
        }
    }
}
