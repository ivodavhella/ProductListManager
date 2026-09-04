
using System;
using System.Collections.Generic;
using System.Text;

namespace ProductListManager;

using System.Text.RegularExpressions;


// This class manages all product-related operations.
// It allows the user to add, view, search, delete, validate,
// display statistics, and save products to a file.
public class ProductService
{
    // Stores all products in a private list.
    // The list can only be accessed and modified through this class.
    private readonly List<Product> _products = new();


    // Adds a new product to the product list.
    // The method validates the product format and checks
    // if the product already exists.
    //
    // input: The product entered by the user.
    // message: Returns a message describing the result.
    //
    // Returns true if the product was added successfully.
    // Returns false if the product is invalid or already exists.
    public bool AddProduct(string input, out string message)
    {
        // Removes unnecessary spaces before and after the input.
        input = input.Trim();

        // Validates the product input and extracts
        // the product name and product number.
        if (!IsValidProduct(input, out string name, out int number))
        {
            message =
                "Invalid product format. Use LETTERS-NUMBER. Example: CE-400";

            return false;
        }

        // Checks whether a product with the same name
        // and product number already exists in the list.
        bool exists = _products.Any(product =>
            product.Name.Equals(name, StringComparison.OrdinalIgnoreCase)
            && product.Number == number);

        // Prevents duplicate products from being added.
        if (exists)
        {
            message = "WARNING: Product already exists.";
            return false;
        }

        // Creates and adds the new product to the list.
        _products.Add(new Product(name, number));

        // Returns a success message.
        message = "Product added successfully.";

        return true;
    }


    // Returns all products sorted alphabetically by name
    // and then by product number.
    public List<Product> GetProducts()
    {
        return _products
            .OrderBy(product => product.Name)
            .ThenBy(product => product.Number)
            .ToList();
    }


    // Searches for products using a product name
    // or a product number.
    //
    // searchText: The text entered by the user to search for.
    //
    // Returns a list of matching products sorted
    // alphabetically by name and product number.
    public List<Product> SearchProducts(string searchText)
    {
        // Removes unnecessary spaces from the search text.
        searchText = searchText.Trim();

        // Searches for products where the name contains
        // the search text or the product number contains
        // the search text.
        return _products
            .Where(product =>
                product.Name.Contains(
                    searchText,
                    StringComparison.OrdinalIgnoreCase)
                ||
                product.Number.ToString().Contains(searchText))
            .OrderBy(product => product.Name)
            .ThenBy(product => product.Number)
            .ToList();
    }


    // Deletes a product from the product list.
    //
    // input: The product entered by the user.
    //
    // Returns true if the product was found and removed.
    // Returns false if the product does not exist
    // or the input format is invalid.
    public bool DeleteProduct(string input)
    {
        // Validates the input before searching for the product.
        if (!IsValidProduct(
                input.Trim(),
                out string name,
                out int number))
        {
            return false;
        }

        // Searches for the first product with the same
        // name and product number.
        Product? product = _products.FirstOrDefault(product =>
            product.Name.Equals(
                name,
                StringComparison.OrdinalIgnoreCase)
            && product.Number == number);

        // Returns false if the product was not found.
        if (product == null)
        {
            return false;
        }

        // Removes the product from the list.
        _products.Remove(product);

        return true;
    }


    // Displays statistics about all products in the list.
    // The statistics include:
    // - Total number of products
    // - Highest product number
    // - Lowest product number
    // - Average product number
    public void ShowStatistics()
    {
        // Checks whether there are any products available.
        if (_products.Count == 0)
        {
            Console.WriteLine("No products available.");
            return;
        }

        // Displays the statistics heading.
        Console.WriteLine("\n--- Statistics ---");

        // Displays the total number of products.
        Console.WriteLine(
            $"Total products: {_products.Count}");

        // Displays the highest product number.
        Console.WriteLine(
            $"Highest product number: {_products.Max(p => p.Number)}");

        // Displays the lowest product number.
        Console.WriteLine(
            $"Lowest product number: {_products.Min(p => p.Number)}");

        // Calculates and displays the average product number.
        // F2 formats the result with two decimal places.
        Console.WriteLine(
            $"Average product number: {_products.Average(p => p.Number):F2}");
    }


    // Saves all products to a text file named products.txt.
    public void SaveToFile()
    {
        // Defines the name and location of the file.
        string filePath = "products.txt";

        // Sorts the products and converts each product
        // to a string before saving them to the file.
        List<string> lines = _products
            .OrderBy(product => product.Name)
            .ThenBy(product => product.Number)
            .Select(product => product.ToString())
            .ToList();

        // Writes all product lines to the text file.
        File.WriteAllLines(filePath, lines);
    }


    // Validates whether a product follows the required format.
    //
    // Required format:
    // LETTERS-NUMBER
    //
    // Examples of valid products:
    // CE-400
    // XX-480
    // LABAN-231
    //
    // The product number must be between 200 and 500.
    //
    // input: The product entered by the user.
    // name: Returns the product name if the input is valid.
    // number: Returns the product number if the input is valid.
    //
    // Returns true if the product is valid.
    // Returns false if the product format is invalid.
    private bool IsValidProduct(
        string input,
        out string name,
        out int number)
    {
        // Sets default values for the output variables.
        name = string.Empty;
        number = 0;

        // Defines a Regular Expression pattern.
        //
        // ^            Start of the input
        // [A-Za-z]+    One or more letters
        // -            A required hyphen
        // \d+          One or more digits
        // $            End of the input
        string pattern = @"^([A-Za-z]+)-(\d+)$";

        // Checks whether the input matches the required pattern.
        Match match = Regex.Match(input, pattern);

        // Returns false if the format does not match.
        if (!match.Success)
        {
            return false;
        }

        // Extracts the product name from the first group
        // and converts it to uppercase letters.
        name = match.Groups[1].Value.ToUpper();

        // Converts the product number from a string to an integer.
        // TryParse prevents the application from crashing
        // if the number cannot be converted.
        if (!int.TryParse(
                match.Groups[2].Value,
                out number))
        {
            return false;
        }

        // Checks whether the product number is within
        // the allowed range of 200 to 500.
        if (number < 200 || number > 500)
        {
            return false;
        }

        // Returns true when all validation rules are successful.
        return true;
    }
}

