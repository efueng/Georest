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
    public class InstructorLabsController : Controller
    {
        private IHttpClientFactory ClientFactory { get; }
        private IMapper Mapper { get; }
        public InstructorLabsController(IHttpClientFactory clientFactory, IMapper mapper)
        {
            ClientFactory = clientFactory;
            Mapper = mapper;
        }
        public async Task<IActionResult> Index()
        {
            using (var httpClient = ClientFactory.CreateClient("GeorestApi"))
            {
                var georestClient = new GeorestClient(httpClient.BaseAddress.ToString(), httpClient);
                ViewBag.InstructorLabs = await georestClient.GetAllLabsAsync();
            }
            return View();
        }

        [HttpGet]
        public IActionResult Add()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Add(InstructorLabInputViewModel viewModel)
        {
            IActionResult result = View();

            if (ModelState.IsValid)
            {
                using (var httpClient = ClientFactory.CreateClient("GeorestApi"))
                {
                    try
                    {
                        var georestClient = new GeorestClient(httpClient.BaseAddress.ToString(), httpClient);
                        await georestClient.AddLabAsync(viewModel);

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
            InstructorLabViewModel fetchedInstructorLab = null;

            using (var httpClient = ClientFactory.CreateClient("GeorestApi"))
            {
                try
                {
                    var georestClient = new GeorestClient(httpClient.BaseAddress.ToString(), httpClient);
                    fetchedInstructorLab = await georestClient.GetLabByIdAsync(id);
                }
                catch (SwaggerException se)
                {
                    ModelState.AddModelError("", se.Message);
                }
            }
            return View(fetchedInstructorLab);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(InstructorLabViewModel viewModel)
        {
            IActionResult result = View();

            if (ModelState.IsValid)
            {
                using (var httpClient = ClientFactory.CreateClient("GeorestApi"))
                {
                    try
                    {
                        var georestClient = new GeorestClient(httpClient.BaseAddress.ToString(), httpClient);
                        await georestClient.UpdateLabAsync(viewModel.Id, Mapper.Map<InstructorLabInputViewModel>(viewModel));

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
                    await georestClient.DeleteInstructorLabAsync(id);

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
