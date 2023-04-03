﻿namespace IWantApp.Domain.Products;

public class Product : Entity
{

    public Guid CategoryId { get; set; }
    public Category Category { get; set; }
    public string Description { get; set; }
    public bool HasStock { get; set; }

}
