
using Microsoft.AspNetCore.Mvc;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Georest.Api.ViewModels;
using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Georest.Api;
using SecretSanta.Api.Tests;

namespace Georest.Api.Tests.Controllers
{
    [TestClass]
    public class StudentsControllerTests
    {
        private CustomWebApplicationFactory<Startup> Factory { get; set; }

        [TestInitialize]
        public void CreateWebFactory()
        {
            Factory = new CustomWebApplicationFactory<Startup>();
        }

        [TestMethod]
        public async Task AddStudentViaApi_FailsDueToMissingFirstName()
        {
            var client = Factory.CreateClient();

            var viewModel = new StudentInputViewModel
            {
                FirstName = "",
                LastName = "Montoya"
            };

            var response = await client.PostAsJsonAsync("/api/students", viewModel);

            Assert.AreEqual(HttpStatusCode.BadRequest, response.StatusCode);

            var result = await response.Content.ReadAsStringAsync();

            var problemDetails = JsonConvert.DeserializeObject<ProblemDetails>(result);

            var errors = problemDetails.Extensions["errors"] as JObject;

            var firstError = (JProperty)errors.First;

            var errorMessage = firstError.Value[0];

            Assert.AreEqual("The FirstName field is required.", ((JValue)errorMessage).Value);
        }

        [TestMethod]
        public async Task AddStudentViaApi_CompletesSuccessfully()
        {
            var client = Factory.CreateClient();

            var studentViewModel = new StudentInputViewModel
            {
                FirstName = "Inigo",
                LastName = "Montoya"
            };

            var response = await client.PostAsJsonAsync("/api/students", studentViewModel);

            Assert.AreEqual(HttpStatusCode.Created, response.StatusCode);

            var result = await response.Content.ReadAsStringAsync();

            var resultViewModel = JsonConvert.DeserializeObject<StudentViewModel>(result);

            Assert.AreEqual(studentViewModel.FirstName, resultViewModel.FirstName);
        }
    }
}
