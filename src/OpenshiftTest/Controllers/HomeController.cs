using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using OpenshiftTest.Models;

namespace OpenshiftTest.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

        [HttpGet]
        public ActionResult GetValidateCode()
        {
            try
            {
                CaptchaHelper captcha = new CaptchaHelper();

                string code = captcha.CreateVerifyCode(4);

                byte[] bytes = captcha.CreateImageCode(code);

                return File(bytes, "image/jpeg");
            }
            catch (Exception ex)
            {
                return Content(string.Format("{0}++++++{1}{2}", ex.Message, System.Environment.NewLine, ex.StackTrace));
            }
        }

        [HttpGet]
        public async Task<string> Proxy(string url)
        {
            if (string.IsNullOrWhiteSpace(url))
            {
                return "empty";
            }

            using (var client = new HttpClient())
            {
                var result = await client.GetStringAsync(url);
                return result;
            }
        }
    }
}
