using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Net6WebApi.Config;
using Net6WebApi.Entity;
using Newtonsoft.Json;

namespace Net6WebApi.Controllers
{
    [ApiController]
    [Route("[controller]/[action]")]
    public class TestController : ControllerBase
    {
        private readonly ILogger<TestController> _logger;
        private readonly SqlServerContext dbContext;

        public TestController(ILogger<TestController> logger, SqlServerContext dbContext)
        {
            _logger = logger;
            this.dbContext = dbContext;
        }
        // GET: Test
        //public ActionResult Index()
        //{
        //    return View();
        //}

        // GET: Test/Details/5
        [HttpGet]
        public Book Details(int id)
        {
            return dbContext.Book.Where(x => x.Id == id).FirstOrDefault();
        }

        //// GET: Test/Create
        //public ActionResult Create()
        //{
        //    return View();
        //}

        // POST: Test/Create
        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public ActionResult Create(IFormCollection collection)
        //{
        //    try
        //    {
        //        return RedirectToAction(nameof(Index));
        //    }
        //    catch
        //    {
        //        return View();
        //    }
        //}

        [HttpPost]
        public JsonContent Create(Alarm alarm)
        {
            try
            {
                return JsonContent.Create(alarm);
                //_logger.LogInformation(alarm);
                //return new JsonContent(res);
            }
            catch
            {
                return null;
            }
        }

        // GET: Test/Edit/5
        //public ActionResult Edit(int id)
        //{
        //    return View();
        //}

        // POST: Test/Edit/5
        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public ActionResult Edit(int id, IFormCollection collection)
        //{
        //    try
        //    {
        //        return RedirectToAction(nameof(Index));
        //    }
        //    catch
        //    {
        //        return View();
        //    }
        //}

        //// GET: Test/Delete/5
        //public ActionResult Delete(int id)
        //{
        //    return View();
        //}

        //// POST: Test/Delete/5
        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public ActionResult Delete(int id, IFormCollection collection)
        //{
        //    try
        //    {
        //        return RedirectToAction(nameof(Index));
        //    }
        //    catch
        //    {
        //        return View();
        //    }
        //}
    }

    public class Alarm
    {
        public string itemCode { get; set; }
        public string description { get; set; }
        public string shortDescription { get; set; }

    }
}
