using DetectLanguage;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace APITestScenarios
{
    public class BatchLangDetection
    {
        private readonly DetectLanguageClient client;

        public BatchLangDetection()
        {
            client = new DetectLanguageClient("5b4381edaa4a17e516de55dff006688d");
        }

        [Fact]
        public async Task Batchlang_LangDetect_Returns_Success()
        {
           string[] texts = { "Hello", "Labas" };
           var results = await client.BatchDetectAsync(texts);

           Assert.True(results[0][0].language=="en");
           Assert.True(results[0][0].reliable);
           Assert.True(results[0][0].confidence>0);
           Assert.True(results[1][0].language=="lt");
           Assert.True(results[1][0].reliable);
           Assert.True(results[1][0].confidence > 0);
        }

        [Fact]
        public async Task Batchlang_junklangdetect_Returns_Success()
        {
            string[] texts = { "Hello", "Bonjour", "KJHgyusY", "1267356&%&", "" };
            var results = await client.BatchDetectAsync(texts);

            Assert.True(results[0][0].language == "en");
            Assert.True(results[0][0].reliable);
            Assert.True(results[0][0].confidence > 0);
            Assert.True(results[1][0].language == "fr");
            Assert.True(results[1][0].reliable);
            Assert.True(results[1][0].confidence > 0);
            Assert.True(results[2][0].language == "en");
            Assert.False(results[2][0].reliable);
            Assert.True(results[2][0].confidence < 1);
            Assert.Empty(results[3]);
            Assert.Empty(results[4]);

        }



    }
}
