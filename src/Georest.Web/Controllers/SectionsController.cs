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
    public class SectionsController : Controller
    {
        private IHttpClientFactory ClientFactory { get; }
        private IMapper Mapper { get; }
        public SectionsController(IHttpClientFactory clientFactory, IMapper mapper)
        {
            ClientFactory = clientFactory;
            Mapper = mapper;
        }
        public async Task<IActionResult> Index()
        {
            using (var httpClient = ClientFactory.CreateClient("GeorestApi"))
            {
                var georestClient = new GeorestClient(httpClient.BaseAddress.ToString(), httpClient);
                ViewBag.Sections = await georestClient.GetAllSectionsAsync();
            }
            return View();
        }

        [HttpGet]
        public IActionResult Add()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Add(SectionInputViewModel viewModel)
        {
            IActionResult result = View();

            if (ModelState.IsValid)
            {
                using (var httpClient = ClientFactory.CreateClient("GeorestApi"))
                {
                    try
                    {
                        var georestClient = new GeorestClient(httpClient.BaseAddress.ToString(), httpClient);
                        await georestClient.AddSectionAsync(viewModel);

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
            SectionViewModel fetchedSection = null;

            using (var httpClient = ClientFactory.CreateClient("GeorestApi"))
            {
                try
                {
                    var georestClient = new GeorestClient(httpClient.BaseAddress.ToString(), httpClient);
                    fetchedSection = await georestClient.GetSectionByIdAsync(id);
                }
                catch (SwaggerException se)
                {
                    ModelState.AddModelError("", se.Message);
                }
            }
            return View(fetchedSection);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(SectionViewModel viewModel)
        {
            IActionResult result = View();

            if (ModelState.IsValid)
            {
                using (var httpClient = ClientFactory.CreateClient("GeorestApi"))
                {
                    try
                    {
                        var georestClient = new GeorestClient(httpClient.BaseAddress.ToString(), httpClient);
                        await georestClient.UpdateSectionAsync((int) viewModel.Id, Mapper.Map<SectionInputViewModel>(viewModel));

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
                    await georestClient.DeleteSectionAsync(id);

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
