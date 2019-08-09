using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Authentication;
using System.Net.Http;
using Newtonsoft.Json.Linq;
using System.Net.Http.Headers;

namespace MvcClient.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        [Authorize]
        public IActionResult Secure()
        {
            ViewData["Message"] = "Secure page.";

            return View();
        }

        public IActionResult Logout()
        {
            return SignOut("Cookies", "oidc");
        }

        public IActionResult Error()
        {
            return View();
        }

        public async Task<IActionResult> CallApi()
        {
            var accessToken = await HttpContext.GetTokenAsync("access_token");

            var client = new HttpClient();
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", accessToken);
            
            //var content = await client.GetStringAsync("http://localhost:5001/identity?scopeId=aaa");

            var response = await client.PostAsJsonAsync("http://localhost:5001/identity", new {ScopeId = "6b60b622-805c-4304-9b24-c87987b45488", Name = "test"})
                                      .ConfigureAwait(false);
            var content = await response.Content.ReadAsStringAsync();

            ViewBag.Json = content;
            return View("json");
        }
    }
}