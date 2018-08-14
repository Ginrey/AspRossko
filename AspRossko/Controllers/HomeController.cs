using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Text;
using System.Threading.Tasks;
using AspRossko.Helper;
using Microsoft.AspNetCore.Mvc;
using AspRossko.Models;
using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;

namespace AspRossko.Controllers
{
    public class HomeController : Controller
    {
        public static Dictionary<string, RequestModel> CachedData { get; set; } = new Dictionary<string, RequestModel>(); 

        Permutations Permutation = new Permutations();
        public IActionResult Index()
        {
            ViewData["Message"] = "Testing data";
            return View();
        }     

        [HttpPost]
        public async Task<IActionResult> UploadFile(IFormFile file)
        {
            if (file != null)
            {
                InputConvertingData inputData;
                using (var fileStream = new StreamReader(file.OpenReadStream(), Encoding.UTF8))
                {   
                    inputData = JsonConvert.DeserializeObject<InputConvertingData>(await fileStream.ReadToEndAsync());
                }

                var requests = new List<RequestModel>();
                foreach (var text in inputData.Data)
                {
                    Permutation.PermutationText = text;
                    Stopwatch sw = Stopwatch.StartNew();

                    if (CachedData.ContainsKey(text))
                    {      
                        CachedData[text].OutputData.Time = sw.Elapsed;
                        requests.Add(CachedData[text]);
                    }
                    else
                    {
                        RequestModel model = new RequestModel
                        {
                            InputData = text,
                            OutputData = new ResponseModel
                            {
                                OutList = Permutation.GetSortedPermutationsList(),
                                Time = sw.Elapsed
                            }
                        };
                        requests.Add(model);
                        CachedData.Add(text, model);
                    }      
                   
                    sw.Stop();
                }

                var path = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "JsonResult.json");
                string serializeData = JsonConvert.SerializeObject(requests);
                using (var stream = new FileStream(path, FileMode.Create))
                {
                    await stream.WriteAsync(Encoding.UTF8.GetBytes(serializeData));  
                }
                return View("Permutations", requests);
            }

            return View("Index");
        }

        public async Task<IActionResult> Download(string filename)
        {
            if (filename == null)
                return Content("filename not present");

            var path = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", filename);

            var memory = new MemoryStream();
            using (var stream = new FileStream(path, FileMode.Open))
            {
                await stream.CopyToAsync(memory);
            }      
            memory.Position = 0;
            return File(memory, "application/json", Path.GetFileName(path));
        }
    }
}
