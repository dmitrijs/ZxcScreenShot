using System.IO;
using System.Net.Http;
using System.Text;
using ZxcScreenShot.Properties;

namespace ZxcScreenShot.output
{
    static class Uploader
    {
        public static string GetToken()
        {
            using (var client = new HttpClient())
            {
                var response = client.GetAsync(Settings.Default.ShotsServiceBaseUrl + "/get_token").Result;
                if (response.IsSuccessStatusCode)
                {
                    using (var reader = new StreamReader(response.Content.ReadAsStreamAsync().Result, Encoding.UTF8))
                    {
                        return reader.ReadToEnd();
                    }
                }
            }
            return null;
        }

        public static string Upload(string token, string filePath)
        {
            var fileStream = new FileStream(filePath, FileMode.Open, FileAccess.Read);
        
            HttpContent fileStreamContent = new StreamContent(fileStream);

            using (var client = new HttpClient())
            using (var formData = new MultipartFormDataContent())
            {
                formData.Add(new StringContent("upload"), "action");
                formData.Add(new StringContent(token), "shot_token");

                formData.Add(fileStreamContent, "shot_file", "shot_file");

                var response = client.PostAsync(Settings.Default.ShotsServiceBaseUrl + "/upload", formData).Result;
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
