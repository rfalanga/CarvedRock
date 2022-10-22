using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using CarvedRock.Admin.Data;

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

    [Required]
    [DisplayName("PRODUCT NAME")]
    public string Name { get; set; }

    [Required]
    public string Description { get; set; }

    [DataType(DataType.Currency)]
    [Range(0.01, 1000.00, ErrorMessage = "Value for {0} must be between " + "{1:C} and {2:C}")]
    public decimal Price { get; set; }
    public bool IsActive { get; set; }

    public static ProductModel FromProduct(Product product)
    {
        return new ProductModel
        {
            Id = product.Id,
            Name = product.Name,
            Description = product.Description,
            Price = product.Price,
            IsActive = product.IsActive
        };
    }

    public Product ToProduct()
    {
        return new Product
        {
            Id = Id,
            Name = Name,
            Description = Description,
            Price = Price,
            IsActive = IsActive
        };

    }
}