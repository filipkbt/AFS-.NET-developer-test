using AFS.NET_developer_test.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;
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

        [HttpPost]
        public async Task<JsonResult> TranslateTextAsync(string text, string translationType)
        {
            var _translationTypeUri = "/" + translationType + ".json";

            if(text == "")
            {
                return Json(new { success = false, message = "Write text you want to translate!" }, JsonRequestBehavior.AllowGet);
            }

            using (var client = new HttpClient())
            {
                string BaseAddress = _baseTranslationsUri + _translationTypeUri;
                client.BaseAddress = new Uri(BaseAddress);
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                string jsonText = JsonConvert.SerializeObject(new { text = text });

                var response = await client.PostAsync(BaseAddress, new StringContent(jsonText, Encoding.UTF8, "application/json"));

                string responseTranslation = response.Content.ReadAsStringAsync().Result;
                var deserializedResponseTranslation = JsonConvert.DeserializeObject<TranslationModel>(responseTranslation);

                TranslationModel translation = new TranslationModel();
                translation.Contents = new contents{ TranslationId = translation };
                translation.Success = new success{ TranslationId = translation };
                
                translation.IsSuccessStatusCode = response.IsSuccessStatusCode;
                translation.Date = DateTime.Now;

                if (response.IsSuccessStatusCode)
                {
                    translation.Contents.text = deserializedResponseTranslation.Contents.text;
                    translation.Contents.translation = deserializedResponseTranslation.Contents.translation;
                    translation.Contents.translated = deserializedResponseTranslation.Contents.translated;
                    translation.StatusCode = response.StatusCode.ToString();
                    translation.Success.total = deserializedResponseTranslation.Success.total;

                    AddTranslation(translation);
                    return Json(new { success = true, translated = translation.Contents.translated }, JsonRequestBehavior.AllowGet);
                }
                else
                {
                    translation.Contents.text = text;
                    translation.Contents.translation = translationType;
                    translation.Contents.translated = null;
                    translation.Error = new error { TranslationId = translation };
                    translation.Error.code = deserializedResponseTranslation.Error.code;
                    translation.Error.message = deserializedResponseTranslation.Error.message;                   

                    AddTranslation(translation);
                    return Json(new { success = false, errorCode = deserializedResponseTranslation.Error.code , errorMessage = deserializedResponseTranslation.Error.message}, JsonRequestBehavior.AllowGet);
                }
            }
        }

        public bool AddTranslation(TranslationModel translation)
        {
            using (DbContextModel DbContext = new DbContextModel())
            {
                try
                {
                    DbContext.Translations.Add(translation);
                    DbContext.SaveChanges();
                    return true;
                }
                catch (Exception ex)
                {
                    return false;
                }
            }
        }

        public ActionResult PreviousTranslations(string filteredResults)
        {
            var resultsList = GetTranslations(filteredResults);
            int countProperlyTranslations = resultsList.Where(x => x.IsSuccessStatusCode == true).Count();
            int countErrors = resultsList.Where(x => x.IsSuccessStatusCode == false).Count();
            ViewBag.CountProperlyTranslations = countProperlyTranslations;
            ViewBag.CountErrors = countErrors;
            double countAllResults = ViewBag.CountProperlyTranslations + ViewBag.CountErrors;
            ViewBag.countAllResults = countAllResults;
            var progressBarProperlyTranslationsWidth = (((double)countProperlyTranslations / (double)countAllResults) * 100).ToString();
            var progressBarErrorsWidth = (((double)countErrors / (double)countAllResults) * 100).ToString();
            ViewBag.progressBarProperlyTranslationsWidth = progressBarProperlyTranslationsWidth.Replace(",", "."); 
            ViewBag.progressBarErrorsWidth = progressBarErrorsWidth.Replace(",", "."); 
            return View(resultsList);
        }

        public IEnumerable<TranslationModel> GetTranslations(string filteredResults)
        {
            using (DbContextModel DbContext = new DbContextModel())
            {
                var translationsList = DbContext.Translations
                                                .Include("contents")
                                                .Include("error")
                                                .Include("success")
                                                .OrderByDescending(x=> x.Date)
                                                .ToList();

                if (filteredResults == "properlyTranslations")
                {
                    var filteredTranslationsList = translationsList.Where(x => x.IsSuccessStatusCode == true).ToList();
                    return filteredTranslationsList;
                }

                else if (filteredResults == "errors")
                {
                    var filteredTranslationsList = translationsList.Where(x => x.IsSuccessStatusCode == false).ToList();
                    return filteredTranslationsList;
                }

                return translationsList;
            }
        }
    }
}