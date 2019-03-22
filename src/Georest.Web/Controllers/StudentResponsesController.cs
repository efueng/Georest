using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using AutoMapper;
using Georest.Web.ApiViewModels;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Georest.Web.Controllers
{
    public class StudentResponsesController : Controller
    {
        private IHttpClientFactory ClientFactory { get; }
        private IMapper Mapper { get; }
        public StudentResponsesController(IHttpClientFactory clientFactory, IMapper mapper)
        {
            ClientFactory = clientFactory;
            Mapper = mapper;
        }
        public async Task<IActionResult> Index(int studentId)
        {
            using (var httpClient = ClientFactory.CreateClient("GeorestApi"))
            {
                var georestClient = new GeorestClient(httpClient.BaseAddress.ToString(), httpClient);
                ViewBag.StudentResponses = await georestClient.GetAllStudentResponsesAsync().ConfigureAwait(false);
            }
            return View();
        }

        [HttpGet]
        public IActionResult Add()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Add(StudentResponseInputViewModel viewModel)
        {
            IActionResult result = View();

            if (ModelState.IsValid)
            {
                using (var httpClient = ClientFactory.CreateClient("GeorestApi"))
                {
                    try
                    {
                        var georestClient = new GeorestClient(httpClient.BaseAddress.ToString(), httpClient);
                        await georestClient.AddStudentResponseAsync(viewModel);

                        result = RedirectToAction(nameof(Index));
                    }
                    catch (SwaggerException se)
                    {
                        ModelState.AddModelError("", se.Message);
                    }
                }
            }

            return result;
        }

        [HttpGet]
        public async Task<IActionResult> Edit(int id)
        {
            StudentResponseViewModel fetchedStudentResponse = null;

            using (var httpClient = ClientFactory.CreateClient("GeorestApi"))
            {
                try
                {
                    var georestClient = new GeorestClient(httpClient.BaseAddress.ToString(), httpClient);
                    fetchedStudentResponse = await georestClient.GetStudentResponseByIdAsync(id);
                }
                catch (SwaggerException se)
                {
                    ModelState.AddModelError("", se.Message);
                }
            }
            return View(fetchedStudentResponse);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(StudentResponseViewModel viewModel)
        {
            IActionResult result = View();

            if (ModelState.IsValid)
            {
                using (var httpClient = ClientFactory.CreateClient("GeorestApi"))
                {
                    try
                    {
                        var georestClient = new GeorestClient(httpClient.BaseAddress.ToString(), httpClient);
                        await georestClient.UpdateStudentResponseAsync((int) viewModel.Id, Mapper.Map<StudentResponseInputViewModel>(viewModel));
                        
                        result = RedirectToAction(nameof(Index));
                    }
                    catch (SwaggerException se)
                    {
                        ModelState.AddModelError("", se.Message);
                    }
                }
            }

            return result;
        }

        public async Task<IActionResult> Delete(int id)
        {
            IActionResult result = View();
            using (var httpClient = ClientFactory.CreateClient("GeorestApi"))
            {
                try
                {
                    var georestClient = new GeorestClient(httpClient.BaseAddress.ToString(), httpClient);
                    await georestClient.DeleteStudentResponseAsync(id);

                    result = RedirectToAction(nameof(Index));
                }
                catch (SwaggerException se)
                {
                    ModelState.AddModelError("", se.Message);
                }
            }

            return result;
        }
    }
}
