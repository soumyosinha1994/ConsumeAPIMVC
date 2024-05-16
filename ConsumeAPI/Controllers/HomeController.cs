using ConsumeAPI.Model;
using ConsumeAPI.Models;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Diagnostics;
using System.Net.Http.Headers;
using System.Text.Json.Serialization;

namespace ConsumeAPI.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private static HttpClient _httpClient = new HttpClient();
        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public async Task<IActionResult>  Index()
        {
            Employee employee = new()
            {
                Name = "Test",
                Age = 25,
                Email = "test@gmail",
                Password = "Test",
                Phone = "Test",
                Salary = 12
            };




            var token = await CallAPI.GetToken();
            int id = 1;
            if (_httpClient.BaseAddress == null)
            {
                _httpClient.BaseAddress = new Uri("https://localhost:44318/api/");
            }

            _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token.ToString());

            //var resp = _httpClient.PostAsJsonAsync("Employee/AddEmployee", employee).Result;

            //var resp = _httpClient.PutAsJsonAsync($"Employee/UpdateEmployee?id={id}", employee).Result;

            var resp = _httpClient.DeleteAsync($"Employee/DeleteEmployee?id={id}").Result;

            var res = _httpClient.GetAsync("Employee/GetAllEmployee").Result;
            //var res = _httpClient.GetAsync($"Employee/GetEmployeeById?id={id}").Result;
            var emp=JsonConvert.DeserializeObject<List<Employee>>(res.Content.ReadAsStringAsync().Result).ToList();
            //var emp=JsonConvert.DeserializeObject<Employee>(res.Content.ReadAsStringAsync().Result);
            return View(emp);
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
    }
}
