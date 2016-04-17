using System;
using System.Text;
using System.IO;
using System.Net.Http;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Photon.Hive.Plugin.PluginTest
{

    public class TestPlugin : PluginBase
    {
        public TestPlugin()
        {
            this.UseStrictMode = true;
        }

        public override void OnSetProperties(ISetPropertiesCallInfo info)
        {
            base.OnSetProperties(info);

            var dict = new Dictionary<string, string>();

            dict["player_name"] = info.Nickname;

            foreach (DictionaryEntry entry in info.Request.Properties)
            {
                dict[entry.Key.ToString()] = entry.Value.ToString();
            }

            MainProc(dict).Wait();
        }

        internal static async Task MainProc(Dictionary<string, string> dict)
        {
            var result = await Post("http://photon-test-app.herokuapp.com/", dict);
        }

        private static async Task<string> Post(string url, Dictionary<string, string> dict)
        {
            string result = "";

            try
            {
                HttpClient httpClient = new HttpClient();
                httpClient.MaxResponseContentBufferSize = int.MaxValue;

                HttpContent content = new FormUrlEncodedContent(dict);
                var resopnse = await httpClient.PostAsync(url, content);
                var text = await resopnse.Content.ReadAsStringAsync();

                result = text;
            }
            catch (Exception e)
            {
                result= e.Message;
            }
            return result;
        }
    }
}
