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
    public class StudentLabsController : Controller
    {
        private IHttpClientFactory ClientFactory { get; }
        private IMapper Mapper { get; }
        public StudentLabsController(IHttpClientFactory clientFactory, IMapper mapper)
        {
            ClientFactory = clientFactory;
            Mapper = mapper;
        }
        public async Task<IActionResult> Index(int studentId)
        {
            using (var httpClient = ClientFactory.CreateClient("GeorestApi"))
            {
                var georestClient = new GeorestClient(httpClient.BaseAddress.ToString(), httpClient);
                ViewBag.StudentLabs = await georestClient.GetLabsForStudentAsync(studentId);
            }
            return View();
        }

        [HttpGet]
        public IActionResult Add()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Add(StudentLabInputViewModel viewModel)
        {
            IActionResult result = View();

            if (ModelState.IsValid)
            {
                using (var httpClient = ClientFactory.CreateClient("GeorestApi"))
                {
                    try
                    {
                        var georestClient = new GeorestClient(httpClient.BaseAddress.ToString(), httpClient);
                        await georestClient.AddStudentLabAsync(viewModel);

                        result = RedirectToAction(nameof(Index), viewModel.StudentId);
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
            StudentLabViewModel fetchedStudentLab = null;

            using (var httpClient = ClientFactory.CreateClient("GeorestApi"))
            {
                try
                {
                    var georestClient = new GeorestClient(httpClient.BaseAddress.ToString(), httpClient);
                    fetchedStudentLab = await georestClient.GetStudentLabByIdAsync(id);
                }
                catch (SwaggerException se)
                {
                    ModelState.AddModelError("", se.Message);
                }
            }
            return View(fetchedStudentLab);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(StudentLabViewModel viewModel)
        {
            IActionResult result = View();

            if (ModelState.IsValid)
            {
                using (var httpClient = ClientFactory.CreateClient("GeorestApi"))
                {
                    try
                    {
                        var georestClient = new GeorestClient(httpClient.BaseAddress.ToString(), httpClient);
                        await georestClient.UpdatStudentLabAsync(viewModel.Id, Mapper.Map<StudentLabInputViewModel>(viewModel));

                        result = RedirectToAction(nameof(Index), viewModel.StudentId);
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
                    int? studentId = (await georestClient.GetStudentLabByIdAsync(id).ConfigureAwait(false)).StudentId;
                    await georestClient.DeleteStudentLabAsync(id);

                    result = RedirectToAction(nameof(Index), studentId);
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
