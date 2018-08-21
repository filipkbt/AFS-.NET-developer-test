using AFS.NET_developer_test.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace AFS.NET_developer_test.Controllers
{
    public class HomeController : Controller
    {
        private const string _baseTranslationsUri = "https://api.funtranslations.com/translate";

        public ActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public PartialViewResult TranslateText()
        {
            return PartialView("_translateForm");
        }

        [HttpPost]
        public async Task<JsonResult> TranslateTextAsync(string text, string translationType)
        {
            var _translationTypeUri = "/" + translationType + ".json";

            using (var client = new HttpClient())
            {
                string BaseAddress = _baseTranslationsUri + _translationTypeUri;
                client.BaseAddress = new Uri(BaseAddress);
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                string jsonText = JsonConvert.SerializeObject(new {text = text});

                var response = await client.PostAsync(BaseAddress, new StringContent(jsonText, Encoding.UTF8, "application/json"));


                if (response.IsSuccessStatusCode)
                {
                    TranslationModel translation = new TranslationModel();
                    //translation.Text = deserializedResponseTranslation.Text;
                    //translation.TranslationType = deserializedResponseTranslation.TranslationType;
                    //translation.Translated = deserializedResponseTranslation.Translated;
                    return Json(new { success = true, responseText = "OK." }, JsonRequestBehavior.AllowGet);
                }
                else
                {
                    return Json(new { success = false, responseText = "NIE OK" }, JsonRequestBehavior.AllowGet);
                }
            }
        }

        //public async Task<TranslationModel> AddTranslationAsync(TranslationModel Translation)
        //{
        //    using (DbContextModel DbContext = new DbContextModel())
        //    {

        //    }
        //}

        //public async Task<List<TranslationModel>> GetTranslationsAsync()
        //{

        //}
    }
}