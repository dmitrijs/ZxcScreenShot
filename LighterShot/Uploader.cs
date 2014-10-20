using System;
using System.IO;
using System.Net.Http;
using System.Text;
using LighterShot.Properties;

namespace LighterShot
{
    static class Uploader
    {
        public static Tuple<String, String> GetKey()
        {
            using (var client = new HttpClient())
            {
                var response = client.GetAsync(Settings.Default.ShotsServiceBaseUrl + "?action=get_key").Result;
                if (response.IsSuccessStatusCode)
                {
                    using (var reader = new StreamReader(response.Content.ReadAsStreamAsync().Result, Encoding.UTF8))
                    {
                        var strings = reader.ReadToEnd().Split(new[] { ' ' });
                        if (strings.Length == 2)
                        {
                            return new Tuple<string, string>(strings[0], strings[1]);
                        }
                    }
                }
            }
            return null;
        }

        public static string Upload(Tuple<String, String> key, string filePath)
        {
            var fileStream = new FileStream(filePath, FileMode.Open, FileAccess.Read);
        
            HttpContent fileStreamContent = new StreamContent(fileStream);

            using (var client = new HttpClient())
            using (var formData = new MultipartFormDataContent())
            {
                formData.Add(new StringContent("upload"), "action");
                formData.Add(new StringContent(key.Item1), "shot_date");
                formData.Add(new StringContent(key.Item2), "shot_key");

                formData.Add(fileStreamContent, "shot_file", "shot_file");

                var response = client.PostAsync(Settings.Default.ShotsServiceBaseUrl, formData).Result;
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
