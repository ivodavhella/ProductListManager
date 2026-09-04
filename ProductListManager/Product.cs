using System;
using System.Collections.Generic;
using System.Text;

namespace ProductListManager;


// Represents a product with a name and a product number.
public class Product
{

    // Gets or sets the name of the product.
    public string Name { get; set; }


    // Gets or sets the number of the product.
    public int Number { get; set; }


    // Initializes a new instance of the <see cref="Product"/> class
    // with the specified product name and product number.

    // <param name="name">The name of the product.</param>
    // <param name="number">The product number.</param>
    public Product(string name, int number)
    {
        Name = name;
        Number = number;
    }


    // Returns the product as a formatted string containing
    // the product name and product number.

    // <returns>
    // A string in the format <c>Name-Number</c>.
    // </returns>
    public override string ToString()
    {
        return $"{Name}-{Number}";
    }
}