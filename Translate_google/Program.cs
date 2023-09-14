using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RestSharp;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace Translate_google
{
    internal class Program
    {
        static void Main(string[] args)
        {
            while (true)
            {
                Console.Write("1 - Detect Language Method\n2 - Translate Text Method ");
                Console.WriteLine();
                Console.Write("Kullanmak istediğiniz metodun numarasını yazın ve enter tusuna basın veya çıkış yapmak için 'q' tuşuna basın: ");
                string secim = Console.ReadLine();
                if (secim == "q")
                    break;
                switch (secim)
                {
                    case "1":
                        DetectedLanguageMethod();
                        break;
                    case "2":
                        TranslateTextMethod();
                        break;
                }
                Console.ReadLine();
            }
        }

        private static void TranslateTextMethod()
        {
            Console.Write("Çevirilecek olan eng yazıyı girin. : ");
            string translateText = Console.ReadLine();
            Console.WriteLine();


            var client = new RestClient("https://google-translate1.p.rapidapi.com/language/translate/v2");
            var request = new RestRequest(Method.POST);
            request.AddHeader("content-type", "application/x-www-form-urlencoded");
            request.AddHeader("Accept-Encoding", "application/gzip");
            request.AddHeader("X-RapidAPI-Key", "6a32f94072msh50d6e3ec8bc5eacp1a8e1ejsnc10d243d092f");
            request.AddHeader("X-RapidAPI-Host", "google-translate1.p.rapidapi.com");
            request.AddParameter("application/x-www-form-urlencoded", "q=Hello%2C%20world!&target=es&source=en", ParameterType.RequestBody);
            request.AddParameter("q", translateText);
            request.AddParameter("target", "tr");

            IRestResponse response = client.Execute(request);
            //Console.WriteLine(response.Content);

            if (response.StatusCode == System.Net.HttpStatusCode.OK)
            {
                Console.WriteLine("İşlem Başarılı");

                // JSON verisini ayrıştırma
                JObject jsonResponse = JObject.Parse(response.Content);
                //{ "data":{ "translations":[{ "translatedText":"This is a test article.","detectedSourceLanguage":"tr"}]} }
                string translatedText = jsonResponse["data"]["translations"][0]["translatedText"].ToString();

                Console.Write("Çeviri sonucu : " + translatedText);
            }
            else
            {
                Console.Write("Hata : " + response.StatusCode);
                Console.Write("Hata mesajı: " + response.ErrorMessage);
            }
        }

        private static void DetectedLanguageMethod()
        {
            Console.Write("Tespit edilecek olan yazıyı girin. : ");
            string detectedLngText = Console.ReadLine();
            Console.WriteLine();

            var client = new RestClient("https://google-translate1.p.rapidapi.com/language/translate/v2/detect");
            var request = new RestRequest(Method.POST);

            request.AddHeader("content-type", "application/x-www-form-urlencoded");
            request.AddHeader("Accept-Encoding", "application/gzip");
            request.AddHeader("X-RapidAPI-Key", "private api key");
            request.AddHeader("X-RapidAPI-Host", "google-translate1.p.rapidapi.com");
            request.AddParameter("application/x-www-form-urlencoded", "q=English%20is%20hard%2C%20but%20detectably%20so", ParameterType.RequestBody);
            request.AddParameter("q", detectedLngText);

            IRestResponse response = client.Execute(request);

            if (response.StatusCode == System.Net.HttpStatusCode.OK)
            {
                Console.WriteLine("İşlem Başarılı");

                // JSON verisini ayrıştırma
                JObject jsonResponse = JObject.Parse(response.Content);
                string detectedLanguage = jsonResponse["data"]["detections"][0][0]["language"].ToString();

                Console.WriteLine("Algılanan Dil : " + detectedLanguage.ToUpper());
            }
            else
            {
                Console.Write("Hata : " + response.StatusCode);
                Console.Write("Hata mesajı: " + response.ErrorMessage);
            }
        }
    }
}
