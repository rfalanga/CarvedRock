using Microsoft.AspNetCore.Mvc;

namespace CarvedRock.Admin.Controllers;

public class ProductsController : Controller
{
    public IActionResult Index()
    {
        return View();
    }
}