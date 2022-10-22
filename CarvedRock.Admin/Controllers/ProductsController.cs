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

    public IActionResult Create()
    {
        return View();
    }

    // POST: ProductsData/Create
    // To protect from overposting attacks, enable the specific properties you want to bind to.
    // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create(ProductModel product)
    {
        if (ModelState.IsValid)
        {
            await _logic.AddNewProduct(product);
            return RedirectToAction(nameof(Index));
        }
        return View(product);
    }

    // GET: ProductsData/Edit/5
    public async Task<IActionResult> Edit(int? id)
    {
        if (id == null)
        {
            return NotFound();
        }

        var productModel = await _logic.GetProductById(id.Value);
        if (productModel == null)
        {
            return NotFound();
        }

        return View(productModel);
    }

    // POST: ProductsData/Edit/5
    // To protect from overposting attacks, enable the specific properties you want to bind to.
    // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Edit(int id, ProductModel product)
    {
        if (id != product.Id) return NotFound();

        if (ModelState.IsValid)
        {
            await _logic.UpdateProduct(product);
            return RedirectToAction(nameof(Index));
        }

        return View(product);
    }

    // GET: ProductsData/Delete/5
    public async Task<IActionResult> Delete(int? id)
    {
        if (id == null) return NotFound();

        var productModel = await _logic.GetProductById(id.Value);
        if (productModel == null) return NotFound();

        return View(productModel);
    }

    // POST: ProductsData/Delete/5
    [HttpPost, ActionName("Delete")]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> DeleteConfirmed(int id)
    {
        await _logic.RemoveProduct(id);
        return RedirectToAction(nameof(Index));
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