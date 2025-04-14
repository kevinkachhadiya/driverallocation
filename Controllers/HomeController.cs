using System.Diagnostics;
using allocation.Models;
using Microsoft.AspNetCore.Mvc;

namespace allocation.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        private readonly AppDbContext _context;
        public HomeController(ILogger<HomeController> logger, AppDbContext context)
        {
            _logger = logger;
            _context = context;
        }

        public IActionResult Index()
        {
            var data = _context.drivers.ToList();


            return View(data);
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [HttpGet]
        public IActionResult Available_drivers()
        {
            var driver = _context.drivers.Where(d => d.IsAllocated == false).Select(s=> new { Value = s.Driverid,Text = s.Drivername,Vehical_No = s.DriverVehicalNumber}).ToList();


            foreach (var i in driver)
            {
                Debug.WriteLine(i.Text+"  "+i.Value);
            }

            return Json(driver, new System.Text.Json.JsonSerializerOptions
            {
                PropertyNamingPolicy = null 
            });
        }

        [HttpGet]
        public IActionResult GetVehicalbyId(int id)
        {
            var vehical = _context.drivers.Find(id);

            return Json(vehical?.DriverVehicalNumber, new System.Text.Json.JsonSerializerOptions
            {
                PropertyNamingPolicy = null
            });
        }

        [HttpPost]
        public IActionResult Update_states(int id)
        {
            var driver = _context.drivers.FirstOrDefault(d=>d.Driverid == id);

            if (driver != null)  // Explicit null check
            {
                driver.IsAllocated = true;
                _context.SaveChanges();
                return Ok(new
                {
                    success = true,
                    message = $"Dri_Id: {driver.Driverid} and Dri_name: {driver.Drivername} Allocated",
                    driverId = driver.Driverid,
                    driverName = driver.Drivername
                });
            }
            else
            {
         
                return NotFound($"Driver with ID {id} not found.");
            }
        }

        [HttpPost]
        public IActionResult AssignDriver(int id)
        {

            var driver = _context.drivers.FirstOrDefault(d => d.Driverid == id);

            if (driver != null)  // Explicit null check
            {
                driver.IsAllocated = true;
                _context.SaveChanges();
                return Ok(new
                {
                    success = true,
                    message = $"Dri_Id: {driver.Driverid} and Dri_name: {driver.Drivername} Allocated",
                    driverId = driver.Driverid,
                    driverName = driver.Drivername
                });
            }
            else
            {

                return NotFound($"Driver with ID {id} not found.");
            }

        }

        [HttpPost]
        public IActionResult UnassignDriver(int id)
        {

            var driver = _context.drivers.FirstOrDefault(d => d.Driverid == id);

            if (driver != null)  // Explicit null check
            {
                driver.IsAllocated = false;
                _context.SaveChanges();
                return Ok(new
                {
                    success = true,
                    message = $"Dri_Id: {driver.Driverid} and Dri_name: {driver.Drivername} Unassigned",
                    driverId = driver.Driverid,
                    driverName = driver.Drivername
                });
            }
            else
            {

                return NotFound($"Driver with ID {id} not found.");
            }

        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
