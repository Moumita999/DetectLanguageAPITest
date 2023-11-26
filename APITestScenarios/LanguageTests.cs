using DetectLanguage;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Headers;
using System.Runtime.Intrinsics.X86;
using System.Text;
using System.Threading.Tasks;

namespace APITestScenarios
{
    public class LanguageTests
    {
        private readonly HttpClient client;


        public LanguageTests()
        {
            client = new HttpClient();
            client.DefaultRequestHeaders.Add("Accept", "application/json");
            client.DefaultRequestHeaders.Authorization =
                new AuthenticationHeaderValue("Bearer", "5b4381edaa4a17e516de55dff006688d");
        }

        [Fact]
        public async Task TestGetLanguagesAsync_FirstLang_Return_success()
        {
            var languages = await client.GetAsync("https://ws.detectlanguage.com/0.2/languages");

            var languagesContent = await languages.Content.ReadAsStringAsync();
            JArray resplangarray = JArray.Parse(languagesContent);

            dynamic languageDetails = JObject.Parse(resplangarray[0].ToString());


            Assert.Equal(Convert.ToString(languageDetails.code), "aa");
            Assert.Equal(Convert.ToString(languageDetails.name), "AFAR");



        }

        [Fact]
        public async Task TestGetLanguagesAsync_LastLang_Return_success()
        {
            var languages = await client.GetAsync("https://ws.detectlanguage.com/0.2/languages");

            var languagesContent = await languages.Content.ReadAsStringAsync();
            JArray resplangarray = JArray.Parse(languagesContent);

            dynamic languageDetailslast = JObject.Parse(resplangarray[163].ToString());


            Assert.Equal(Convert.ToString(languageDetailslast.code), "zu");
            Assert.Equal(Convert.ToString(languageDetailslast.name), "ZULU");

        }


        [Fact]
        public async Task TestGetLanguagesAsync_langcount()
        {
            var languages = await client.GetAsync("https://ws.detectlanguage.com/0.2/languages");

            var languagesContent = await languages.Content.ReadAsStringAsync();
            JArray resplangarray = JArray.Parse(languagesContent);

            Assert.Equal(resplangarray.Count, 164);

        }
    }

}    
              