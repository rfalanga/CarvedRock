namespace CarvedRock.Admin.Data;

public class Product 
{
    //Again, Erik didn't put in a constructor. I'm adding one to
    //to get rid of the warnings on Name and Description properties.
    public Product()
    {
        Name = "";
        Description = "";
    }
    
    public int Id { get; set; }
    public string Name { get; set; }
    public string Description { get; set; }
    public decimal Price { get; set; }
    public bool IsActive { get; set; }
}