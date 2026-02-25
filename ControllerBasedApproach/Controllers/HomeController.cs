using Microsoft.AspNetCore.Mvc;

namespace ControllerBasedApproach.Controllers;

[Route("")]
public class HomeController : Controller
{
    // GET
    
    public IActionResult Index()
    {
        return Ok(new
        {
            FirstName = "Artur",
            LastName = "Nowak"
        });
    }
}