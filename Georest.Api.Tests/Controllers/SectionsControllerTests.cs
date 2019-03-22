using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using Georest.Api.Controllers;
using Georest.Api.ViewModels;
using Georest.Domain.Models;
using Georest.Domain.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections.ObjectModel;
using Georest.Api.Models;

namespace Georest.Api.Tests.Controllers
{
    [TestClass]
    public class SectionsControllerTests
    {
        private CustomWebApplicationFactory<Startup> Factory { get; set; }

        [TestMethod]
        public async Task GetAllSections_ReturnsSections()
        {
            var client = Factory.CreateClient();

            var section1 = new Section
            {
                SectionString = "Section 1",
                Instructor = new Instructor { FirstName = "Tom", LastName = "Capaul"}
            };
            var section2 = new Section
            {
                SectionString = "Section 2",
                InstructorId = 1
            };

            var service = new Mock<ISectionService>();
            //service.Setup(x => x.GetAllSections())
            //    .Returns(Task.FromResult(new Collection<Section> { section1, section2 }))
            //    .Verifiable();


            var controller = new SectionsController(service.Object, Mapper.Instance);
            await controller.AddSection(Mapper.Map<SectionInputViewModel>(section1));
            await controller.AddSection(Mapper.Map<SectionInputViewModel>(section2));

            var result = (await controller.GetAllSections()).Result as OkObjectResult;

            List<SectionViewModel> sections = ((IEnumerable<SectionViewModel>)result.Value).ToList();

            Assert.AreEqual(2, sections.Count);
            AssertAreEqual(sections[0], section1);
            AssertAreEqual(sections[1], section2);
            service.VerifyAll();
        }

        [TestMethod]
        public async Task CreateSection_RequiresSection()
        {
            var service = new Mock<ISectionService>();
            var controller = new SectionsController(service.Object, Mapper.Instance);

            var result = (await controller.AddSection(null)).Result as BadRequestResult;

            Assert.IsNotNull(result);
        }

        [TestMethod]
        public async Task CreateSection_ReturnsCreatedSection()
        {
            var section = new Section
            {
                SectionString = "Section 1",
                Instructor = new Instructor { FirstName = "Tom", LastName = "Capaul" }
            };

            var service = new Mock<ISectionService>();
            service.Setup(x => x.AddSection(It.Is<Section>(g => g.SectionString == section.SectionString)))
                .Returns(Task.FromResult(new Section
                {
                    SectionString = section.SectionString
                }))
                .Verifiable();

            var controller = new SectionsController(service.Object, Mapper.Instance);

            var result = (await controller.AddSection(Mapper.Map<SectionInputViewModel>(section))).Result as OkObjectResult;
            Section resultValue = (Section)result.Value;

            Assert.IsNotNull(resultValue);
            Assert.AreEqual("Section 1", resultValue.SectionString);
            Assert.AreEqual("Tom", resultValue.Instructor.FirstName);
            Assert.AreEqual("Capaul", resultValue.Instructor.LastName);
            service.VerifyAll();
        }

        //[TestMethod]
        //public async Task UpdateSection_RequiresSection()
        //{
        //    var service = new Mock<ISectionService>(MockBehavior.Strict);
        //    var controller = new SectionsController(service.Object, Mapper.Instance);


        //    BadRequestResult result = await controller.UpdateSection(1, null);

        //    Assert.IsNotNull(result);
        //}

        [TestMethod]
        public async Task UpdateSection_ReturnsUpdatedSection()
        {
            var section = new Section
            {
                SectionString = "Section 1",
                Instructor = new Instructor { FirstName = "Tom", LastName = "Capaul" }
            };
            var service = new Mock<ISectionService>();
            service.Setup(x => x.AddSection(It.Is<Section>(g => g.SectionString == section.SectionString)))
                .Returns(Task.FromResult(new Section
                {
                    SectionString = section.SectionString
                }))
                .Verifiable();

            var controller = new SectionsController(service.Object, Mapper.Instance);

            var result = (await controller.UpdateSection(1, Mapper.Map<SectionInputViewModel>(section)));

            Assert.IsNotNull(result);
            service.VerifyAll();
        }

        [TestMethod]
        [DataRow(-1)]
        [DataRow(0)]
        public async Task DeleteSection_RequiresPositiveId(int sectionId)
        {
            var service = new Mock<ISectionService>(MockBehavior.Strict);
            var controller = new SectionsController(service.Object, Mapper.Instance);

            var result = await controller.DeleteSection(sectionId);

            Assert.IsTrue(result is BadRequestObjectResult);
        }

        [TestMethod]
        public async Task DeleteSection_ReturnsNotFoundWhenTheSectionFailsToDelete()
        {
            var service = new Mock<ISectionService>();
            service.Setup(x => x.DeleteSection(2))
                .Returns(Task.FromResult(false))
                .Verifiable();
            var controller = new SectionsController(service.Object, Mapper.Instance);

            var result = await controller.DeleteSection(2);

            Assert.IsTrue(result is NotFoundResult);
            service.VerifyAll();
        }

        [TestMethod]
        public async Task DeleteSection_ReturnsOkWhenSectionIsDeleted()
        {
            var service = new Mock<ISectionService>();
            service.Setup(x => x.DeleteSection(2))
                .Returns(Task.FromResult(true))
                .Verifiable();
            var controller = new SectionsController(service.Object, Mapper.Instance);

            var result = await controller.DeleteSection(2);

            Assert.IsTrue(result is OkResult);
            service.VerifyAll();
        }

        private static void AssertAreEqual(SectionViewModel expected, Section actual)
        {
            if (expected == null && actual == null) return;
            if (expected == null || actual == null) Assert.Fail();

            Assert.AreEqual(expected.Id, actual.Id);
            Assert.AreEqual(expected.SectionString, actual.SectionString);
        }
    }
}