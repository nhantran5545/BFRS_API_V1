using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace BFRS_BankTests.ControllersTesting
{
    [TestClass]
    public class AccountsControllerTests
    {
        private readonly HttpClient _httpClient;

        public AccountsControllerTests()
        {
            var api = new ApiWebApplicationFactory();
            _httpClient = api.CreateClient();
        }

        [TestMethod]
        public async Task Login_ValidCredentials_ReturnsToken()
        {
            // Arrange
            var requestBody = new
            {
                username = "nhantran5545",
                password = "231093nhan"
            };

            var content = new StringContent(JsonConvert.SerializeObject(requestBody), Encoding.UTF8, "application/json");

            // Act
            var response = await _httpClient.PostAsync("/api/Accounts/login", content);

            // Assert
            Assert.AreEqual(HttpStatusCode.OK, response.StatusCode);

            var responseBody = await response.Content.ReadAsStringAsync();
            var responseObject = JsonConvert.DeserializeObject<LoginResponse>(responseBody);

            Assert.IsNotNull(responseObject);
            Assert.IsTrue(!string.IsNullOrEmpty(responseObject.Token));
            Assert.AreEqual("Tran", responseObject.Account.FirstName);
            Assert.AreEqual("Nhan", responseObject.Account.LastName);
            Assert.AreEqual("Admin", responseObject.Account.Role);
            Assert.IsNull(responseObject.Account.FarmId);
        }

        [TestMethod]
        public async Task Login_InvalidCredentials_ReturnsUnauthorized()
        {
            // Arrange
            var invalidCredentials = new
            {
                username = "invalid_username",
                password = "invalid_password"
            };

            var content = new StringContent(JsonConvert.SerializeObject(invalidCredentials), Encoding.UTF8, "application/json");

            // Act
            var response = await _httpClient.PostAsync("/api/Accounts/login", content);

            // Assert
            Assert.AreEqual(HttpStatusCode.Unauthorized, response.StatusCode);
        }

        [TestMethod]
        public async Task Login_InsufficientPermissions_ReturnsForbidden()
        {
            // Arrange
            var requestBody = new
            {
                username = "limited_user",
                password = "password"
            };

            var content = new StringContent(JsonConvert.SerializeObject(requestBody), Encoding.UTF8, "application/json");

            // Act
            var response = await _httpClient.PostAsync("/api/Accounts/login", content);

            // Assert
            Assert.AreEqual(HttpStatusCode.Forbidden, response.StatusCode);
        }

        [TestMethod]
        public async Task Login_IncorrectPassword_ReturnsUnauthorized()
        {
            // Arrange
            var requestBody = new
            {
                username = "nhantran5545",
                password = "incorrect_password"
            };

            var content = new StringContent(JsonConvert.SerializeObject(requestBody), Encoding.UTF8, "application/json");

            // Act
            var response = await _httpClient.PostAsync("/api/Accounts/login", content);

            // Assert
            Assert.AreEqual(HttpStatusCode.Unauthorized, response.StatusCode);
        }


    }

    // Define your response model here
    public class LoginResponse
    {
        public string Token { get; set; }
        public AccountInfo Account { get; set; }
    }

    public class AccountInfo
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Role { get; set; }
        public object FarmId { get; set; }
    }
}
