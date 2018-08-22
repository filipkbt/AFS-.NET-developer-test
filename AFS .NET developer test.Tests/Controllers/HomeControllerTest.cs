using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;
using System.Web.Script.Serialization;
using AFS.NET_developer_test;
using AFS.NET_developer_test.Controllers;
using AFS.NET_developer_test.Models;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using NUnit.Framework;

namespace AFS.NET_developer_test.Tests.Controllers
{
    [TestFixture]
    public class HomeControllerTest
    {
        private HomeController _homeController;        

        public HomeControllerTest()
        {
            _homeController = new HomeController();
        }

        [TestCase("test text", "leetspeak")]
        public async Task TestTranslateTextAsyncShouldReturnTrue(string text, string translationType)
        {
            //If tests are red, probably we're sent more than 5 requests in 1 hour
            var jsonResult = await _homeController.TranslateTextAsync(text, translationType) as JsonResult;
            dynamic data = jsonResult.Data;
            var isSuccess = data.GetType().GetProperty("success").GetValue(data, null);
            Assert.AreEqual(isSuccess, true);
        }

        [TestCase("", "")]
        [TestCase("test", "")]
        [TestCase("", "test")]
        [TestCase("test text", "leet_speak")]
        public async Task TestTranslateTextAsyncShouldReturnFalse(string text, string translationType)
        {
            var jsonResult = await _homeController.TranslateTextAsync(text, translationType) as JsonResult;
            dynamic data = jsonResult.Data;
            var isSuccess = data.GetType().GetProperty("success").GetValue(data, null);
            Assert.AreEqual(isSuccess, false);
        }

        [Test]
        public void TestAddTranslationShouldReturnTrue()
        {
            TranslationModel translation = new TranslationModel();
            translation.Contents = new contents();
            translation.Success = new success();
            translation.Error = new error();
            translation.Date = DateTime.Now;
            translation.Contents.text = "test";
            translation.Contents.translation = "test";
            Assert.IsTrue(_homeController.AddTranslation(translation));
        }

        [Test]
        public void TestAddTranslationShouldReturnFalse()
        {
            TranslationModel translation = new TranslationModel();
            Assert.AreEqual(_homeController.AddTranslation(translation),false);
        }

        [TestCase("properlyTranslations")]
        [TestCase("errors")]
        [TestCase(null)]
        [Test]
        public void TestGetTranslations(string filteredResults)
        {
            Assert.NotNull(_homeController.GetTranslations(filteredResults));
        }

    }
}
