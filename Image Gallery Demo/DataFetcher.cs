using Newtonsoft.Json;
using System;
using System.Collections.Generic;

using System.IO;
using System.Net.Http;
using System.Reflection;
using System.Threading.Tasks;

namespace Image_Gallery_Demo
{
    class DataFetcher
    {
        async Task<string> GetDatafromService(string searchstring)
        {
            string readText = null;
            try
            {
                var azure =
               @"https://imagefetcher20200529182038.azurewebsites.net";
                string url = azure + @"/api/fetch_images?query=" +
               searchstring + "&max_count=10";
                using (HttpClient c = new HttpClient())
                {
                    readText = await c.GetStringAsync(url);
                }
            }
            catch
            {
                var a = Properties.Resources.sampleData;
                string result = System.Text.Encoding.UTF8.GetString(a);
                readText = result;
            }

            return readText;

        } // method to fetch json data

        public async Task<List<ImageItem>> GetImageData(string search)
        {
            string data = await GetDatafromService(search);
            return JsonConvert.DeserializeObject<List<ImageItem>>(data);
        } //method to convert json data 

    }
}
