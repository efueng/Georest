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
    public class InstructorsController : Controller
    {
        private IHttpClientFactory ClientFactory { get; }
        private IMapper Mapper { get; }
        public InstructorsController(IHttpClientFactory clientFactory, IMapper mapper)
        {
            ClientFactory = clientFactory;
            Mapper = mapper;
        }
        public async Task<IActionResult> Index()
        {
            using (var httpClient = ClientFactory.CreateClient("GeorestApi"))
            {
                var georestClient = new GeorestClient(httpClient.BaseAddress.ToString(), httpClient);
                ViewBag.Instructors = await georestClient.GetAllInstructorsAsync();
            }
            return View();
        }

        [HttpGet]
        public IActionResult Add()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Add(InstructorInputViewModel viewModel)
        {
            IActionResult result = View();

            if (ModelState.IsValid)
            {
                using (var httpClient = ClientFactory.CreateClient("GeorestApi"))
                {
                    try
                    {
                        var georestClient = new GeorestClient(httpClient.BaseAddress.ToString(), httpClient);
                        await georestClient.AddInstructorAsync(viewModel);

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
            InstructorViewModel fetchedInstructor = null;

            using (var httpClient = ClientFactory.CreateClient("GeorestApi"))
            {
                try
                {
                    var georestClient = new GeorestClient(httpClient.BaseAddress.ToString(), httpClient);
                    fetchedInstructor = await georestClient.GetInstructorByIdAsync(id);
                }
                catch (SwaggerException se)
                {
                    ModelState.AddModelError("", se.Message);
                }
            }
            return View(fetchedInstructor);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(InstructorViewModel viewModel)
        {
            IActionResult result = View();

            if (ModelState.IsValid)
            {
                using (var httpClient = ClientFactory.CreateClient("GeorestApi"))
                {
                    try
                    {
                        var georestClient = new GeorestClient(httpClient.BaseAddress.ToString(), httpClient);
                        await georestClient.UpdateInstructorAsync(viewModel.Id, Mapper.Map<InstructorInputViewModel>(viewModel));

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
                    await georestClient.DeleteInstructorAsync(id);

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
