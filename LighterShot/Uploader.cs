using System;
using System.IO;
using System.Net.Http;
using System.Text;

namespace LighterShot
{
    class Uploader
    {
        public static String Upload(string url, string shotDate, string shotKey, string filePath)
        {
            // Stream fileStream, byte[] fileBytes

            var fileStream = new FileStream(filePath, FileMode.Open, FileAccess.Read);
        
            // examples of converting both Stream and byte [] to HttpContent objects
            // representing input type file
            HttpContent fileStreamContent = new StreamContent(fileStream);
//            HttpContent bytesContent = new ByteArrayContent(fileBytes);

            // Submit the form using HttpClient and 
            // create form data as Multipart (enctype="multipart/form-data")

            using (var client = new HttpClient())
            using (var formData = new MultipartFormDataContent())
            {
                formData.Add(new StringContent("upload"), "action");
                formData.Add(new StringContent(shotDate), "shot_date");
                formData.Add(new StringContent(shotKey), "shot_key");

                // <input type="file" name="file1" />
                formData.Add(fileStreamContent, "shot_file", "shot_file");
                // <input type="file" name="file2" />
//                formData.Add(bytesContent, "file2", "file2");

                // Actually invoke the request to the server

                // equivalent to (action="{url}" method="post")
                var response = client.PostAsync(url, formData).Result;

                // equivalent of pressing the submit button on the form
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
