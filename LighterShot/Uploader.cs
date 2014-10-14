using System;
using System.IO;
using System.Net.Http;
using System.Text;

namespace LighterShot
{
    static class Uploader
    {
        public static Tuple<String, String> GetKey()
        {
            using (var client = new HttpClient())
            {
                var response = client.GetAsync("http://shots.local/?action=get_key").Result;
                if (response.IsSuccessStatusCode)
                {
                    using (var reader = new StreamReader(response.Content.ReadAsStreamAsync().Result, Encoding.UTF8))
                    {
                        var strings = reader.ReadToEnd().Split(new char[1] { ' ' });
                        if (strings.Length == 2)
                        {
                            return new Tuple<string, string>(strings[0], strings[1]);
                        }
                    }
                }
            }
            return null;
        }

        public static String Upload(string url, string shotDate, string shotKey, string filePath)
        {
            var fileStream = new FileStream(filePath, FileMode.Open, FileAccess.Read);
        
            HttpContent fileStreamContent = new StreamContent(fileStream);

            using (var client = new HttpClient())
            using (var formData = new MultipartFormDataContent())
            {
                formData.Add(new StringContent("upload"), "action");
                formData.Add(new StringContent(shotDate), "shot_date");
                formData.Add(new StringContent(shotKey), "shot_key");

                formData.Add(fileStreamContent, "shot_file", "shot_file");

                var response = client.PostAsync(url, formData).Result;
                if (!response.IsSuccessStatusCode)
                {
                    return null;
                }

                using (var reader = new StreamReader(response.Content.ReadAsStreamAsync().Result, Encoding.UTF8))
                {
                    return reader.ReadToEnd();
                }
            }
        }
    }
}
