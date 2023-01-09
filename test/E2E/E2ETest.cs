using ServerING.DTO;
using ServerING.Models;
using E2E.Utils;

using Allure.Xunit.Attributes;
using System.Net.Http.Json;
using System.Net;
using System.Web;


namespace E2E {
    [AllureOwner("EqualNine")]
    [AllureSuite("E2E Test")]
    public class E2ETest: IClassFixture<MyWebFactory> {
        private MyWebFactory factory;
        private string baseUrl = "http://localhost:5555/api/v1/";
        private HttpClient client;

        public E2ETest(MyWebFactory _factory) {
            factory = _factory;
            client = factory.CreateClient();
            client.BaseAddress = new Uri(baseUrl);
        }

        [AllureXunit]
        [ResetDatabase]
        public async Task ScenarioE2E() {
            // Registration

            // Arrange
            var registerData = new LoginDto() {Login = "testUser", Password = "testPassword"};
            var registrationJSON = JsonContent.Create(registerData);

            // Act
            var registerResponse = await client.PostAsync(baseUrl + "users/register", registrationJSON);
            var registerResult = await registerResponse.Content.ReadFromJsonAsync<UserIdPasswordDto>();

            // Assert
            Assert.Equal(HttpStatusCode.OK, registerResponse.StatusCode);
            Assert.Equal(registerData.Login, registerResult.Login);


            // Login

            // Arrange
            var loginData = new LoginDto() {Login = "testUser", Password = "testPassword"}; 
            var loginJSON = JsonContent.Create(loginData);

            // Act
            var loginResponse = await client.PostAsync(baseUrl + "users/login", loginJSON);
            var loginResult = await loginResponse.Content.ReadFromJsonAsync<UserDto>();

            // Assert
            Assert.Equal(HttpStatusCode.OK, loginResponse.StatusCode);
            Assert.Equal(loginData.Login, loginResult.Login);
            Assert.Equal("use", loginResult.Role);


            // Get Servers

            // Arrange
            var query = HttpUtility.ParseQueryString(string.Empty);
            query["Name"] = "Server";
            query["sortState"] = "NameDesc";
            query["page"] = "1";
            query["pageSize"] = "1";

            // Act
            var getServersResponse = await client
                .GetAsync(baseUrl + "servers?" + query.ToString());

            var getServersResult = await getServersResponse
                .Content
                .ReadFromJsonAsync<List<ServerDto>>();

            // Assert
            Assert.Equal(HttpStatusCode.OK, getServersResponse.StatusCode);
            
            foreach (var server in getServersResult) {
                Assert.Equal("Server5", server.Name);
            }


            // Add Server To Favorites
            
            // Arrange
            int userId = loginResult.Id;
            int serverId = 1;

            // Act
            var addFavoriteServerResponse = await client
                .PostAsync(baseUrl + string.Format("users/{0}/favorites/{1}", userId, serverId), null);
            var addFavoriteServerResult = await addFavoriteServerResponse.Content.ReadFromJsonAsync<FavoriteServerDto>();

            // Assert
            Assert.Equal(HttpStatusCode.OK, addFavoriteServerResponse.StatusCode);
            Assert.Equal(userId, addFavoriteServerResult.UserID);
            Assert.Equal(serverId, addFavoriteServerResult.ServerID);


            // Get Favorite Servers List

            // Arrange
            userId = loginResult.Id;

            // Act
            var getFavServersResponse = await client
                .GetAsync(baseUrl + string.Format("users/{0}/favorites", userId));

            var getFavServersResult = await getFavServersResponse
                .Content
                .ReadFromJsonAsync<List<ServerDto>>();

            // Assert
            Assert.Equal(HttpStatusCode.OK, getFavServersResponse.StatusCode);
            
            foreach (var server in getFavServersResult) {
                Assert.Equal("Server1", server.Name);
            }
        }
    }
}
