using Microsoft.AspNetCore.Mvc;
using CarvedRock.Admin.Models;
using CarvedRock.Admin.Logic;

namespace CarvedRock.Admin.Controllers;

public class ProductsController : Controller
{
    private readonly IProductLogic _logic;

    //public List<ProductModel> Products { get; set; }

    public ProductsController(IProductLogic logic)
    {
        //Products = GetSampleProducts();
        this._logic = logic;
    }

    public async Task<IActionResult> Details(int id)
    {
        var product = await _logic.GetProductById(id);
        return product == null ? NotFound() : View(product);
    }

    // The functionality of this code has been moved to the DbContext class and incorporated 
    // in Program.cs
    // private List<ProductModel> GetSampleProducts()
    // {
    //     return new List<ProductModel>()
    //     {
    //         new ProductModel {Id = 1, Name = "Trailblazer", Price = 69.99M, IsActive = true,
    //             Description = "Great support in this high-top to take you to great heights and trails." },
    //         new ProductModel {Id = 2, Name = "Coastliner", Price = 49.99M, IsActive = true,
    //             Description = "Easy in and out with this lightweight but rugged shoe with great ventilation to get your around shores, beaches, and boats."},
    //         new ProductModel {Id = 3, Name = "Woodsman", Price = 64.99M, IsActive = true,
    //             Description = "All the insulation and support you need when wandering the rugged trails of the woods and backcountry." },
    //         new ProductModel {Id = 4, Name = "Basecamp", Price = 249.99M, IsActive = true,
    //             Description = "Great insulation and plenty of room for 2 in this spacious but highly-portable tent."}
    //     };
    // }

    public async Task<IActionResult> Index()
    {
        var products = await _logic.GetAllProducts();
        return View(products);
    }
}