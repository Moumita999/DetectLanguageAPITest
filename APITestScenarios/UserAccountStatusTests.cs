using DetectLanguage;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net.Http.Headers;
using System.Net;
using Newtonsoft.Json.Linq;

namespace APITestScenarios
{
    public class UserAccountStatusTests
    {
        private readonly HttpClient client;

        public UserAccountStatusTests()
        {
            client = new HttpClient();
            client.DefaultRequestHeaders.Add("Accept", "application/json");
            client.DefaultRequestHeaders.Authorization = 
                new AuthenticationHeaderValue("Bearer", "5b4381edaa4a17e516de55dff006688d");
        }

        [Fact]
        public async Task TestGetUserStatusAsync()
        {
           var userStatus = await client.GetAsync("https://ws.detectlanguage.com/0.2/user/status");

           var userStatusContent = await userStatus.Content.ReadAsStringAsync();
           
            dynamic userDetails = JObject.Parse(userStatusContent);


           Assert.Equal(HttpStatusCode.OK, userStatus.StatusCode);
           Assert.NotEmpty(userStatusContent);
           Assert.Equal(Convert.ToString(userDetails.plan),"FREE");
           Assert.NotEmpty(Convert.ToString(userDetails.date));
           Assert.Equal(Convert.ToString(userDetails.status), "ACTIVE");
           Assert.True(Convert.ToInt64(userDetails.daily_requests_limit) >0);
           Assert.True(Convert.ToInt64(userDetails.daily_bytes_limit) >0);
           Assert.True(Convert.ToInt64(userDetails.bytes)>=0);
           Assert.True(Convert.ToInt64(userDetails.requests)>=0);
        }

    }
}
