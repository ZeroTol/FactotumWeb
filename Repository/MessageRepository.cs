using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using FactotumModels;
using Microsoft.AspNetCore.Http;
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
                try
                {
                    var url = new Uri(baseURL);
                    client.BaseAddress = url;
                    var res = await client.GetAsync("");
                    var str = await res.Content.ReadAsStringAsync();
                    var msgs = JsonConvert.DeserializeObject<List<MMessage>>(str);
                    return msgs;
                }
                catch(Exception)
                {
                    return null;
                }
            }
        }
    }
}
