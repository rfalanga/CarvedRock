namespace CarvedRock.Admin.Models;

public class ProductModel
{
    public ProductModel()
    {
        // Erik didn't add this constructor
        Name = "";
        Description = "";
    }
    
    public int Id { get; set; }
    public string Name { get; set; }
    public string Description { get; set; }
    public decimal Price { get; set; }
    public bool IsActive { get; set; }
}