using BusinessObjects.ResponseModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Testing;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace BFRS_BankTests.ControllersTesting
{
    [TestClass]
    public class BirdsControllerTest
    {
        private readonly ApiWebApplicationFactory api;
        private readonly HttpClient _httpClient;
        private readonly string? jwtToken;

        public BirdsControllerTest()
        {
            api = new ApiWebApplicationFactory();
            _httpClient = api.CreateClient();
            jwtToken = "eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJBY2NvdW50SWQiOiIyIiwicm9sZSI6Ik1hbmFnZXIiLCJuYmYiOjE3MTMxMDcxMjQsImV4cCI6MTcxMzEyODcyNCwiaWF0IjoxNzEzMTA3MTI0fQ.Qj3xHg6Z0RnKrkQs1bsVCmm-njRaRVWRI8hL1d11faE";
            _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", jwtToken);
        }

        [TestMethod]
        public async Task Get_retrieves_Birds()
        {
            var birds = await _httpClient.GetAsync("/api/Birds");
            //assert
            Assert.AreEqual(HttpStatusCode.OK, birds.StatusCode);
        }
    }
}
