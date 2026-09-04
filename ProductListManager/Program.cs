using Microsoft.VisualBasic.FileIO;
using ProductListManager;


// Creates an object of the ProductService class.
// This object is used to manage all product-related operations.
ProductService productService = new();


// Controls whether the application should continue running.
// The application continues running while this value is true.
bool running = true;


// Main application loop.
// The menu is continuously displayed until the user chooses
// to exit the application.
while (running)
{
    // Displays the main menu.
    ShowMenu();

    // Asks the user to choose a menu option.
    Console.Write("Choose an option a number 1 - 7:");

    // Reads the user's menu choice.
    // If Console.ReadLine returns null, an empty string is used instead.
    // Trim removes unnecessary spaces before and after the input.
    string choice = (Console.ReadLine() ?? "").Trim();

    // Creates an empty line for better readability.
    Console.WriteLine();


    // Checks which menu option the user selected.
    switch (choice)
    {
        // Option 1: Add a new product.
        case "1":
            AddProduct(productService);
            break;


        // Option 2: Display all products.
        case "2":
            ViewProducts(productService);
            break;


        // Option 3: Search for a product.
        case "3":
            SearchProduct(productService);
            break;


        // Option 4: Delete a product.
        case "4":
            DeleteProduct(productService);
            break;


        // Option 5: Display product statistics.
        case "5":
            ShowStatistics(productService);
            break;


        // Option 6: Save all products to a text file.
        case "6":
            // Calls the SaveToFile method in ProductService.
            productService.SaveToFile();

            // Displays a success message in green.
            WriteSuccess(
                "Products saved successfully to products.txt");

            break;


        // Option 7: Exit the application.
        // The user can also type "exit".
        case "7":
        case "exit":
            // Stops the main while loop.
            running = false;

            // Displays a goodbye message.
            Console.WriteLine(
                "Application closed. Goodbye!");

            break;


        // Runs when the user enters an invalid menu option.
        default:
            // Displays an error message in red.
            WriteError(
                "Invalid option. Please choose 1-7.");

            break;
    }

    // Creates an empty line after each operation.
    Console.WriteLine();
}


// Displays the main menu of the application.
static void ShowMenu()
{
    // Displays the application title.
    Console.WriteLine("PRODUCT LIST MANAGER");

    // Displays a separator line.
    Console.WriteLine("********************");

    // Displays all available menu options.
    Console.WriteLine("1. Add Product");
    Console.WriteLine("2. View Products");
    Console.WriteLine("3. Search Product");
    Console.WriteLine("4. Delete Product");
    Console.WriteLine("5. Statistics");
    Console.WriteLine("6. Save to File");
    Console.WriteLine("7. Exit");

    // Displays another separator line.
    Console.WriteLine("********************");

    // Creates an empty line before the user enters an option.
    Console.WriteLine();
}


// Adds a new product to the product list.
//
// productService: The ProductService object that manages
// all product-related operations.
static void AddProduct(
    ProductService productService)
{
    // Asks the user to enter a product.
    // The expected format is LETTERS-NUMBER.
    Console.Write(
        "Enter product (example CE-400): ");

    // Reads the product entered by the user.
    //string? input = Console.ReadLine();
    string input = (Console.ReadLine() ?? "").Trim();

    // Checks if the user entered an empty or invalid value.
    if (string.IsNullOrWhiteSpace(input))
    {
        // Displays an error message.
        WriteError(
            "Product cannot be empty.");

        // Stops this method.
        return;
    }


    // Attempts to add the product.
    // The method also returns a message explaining the result.
    bool success =
        productService.AddProduct(
            input,
            out string message);


    // Checks if the product was successfully added.
    if (success)
    {
        // Displays a success message in green.
        WriteSuccess(message);
    }
    else
    {
        // Displays an error message in red.
        WriteError(message);
    }
}


// Displays all products in the product list.
//
// productService: The ProductService object used
// to retrieve the products.
static void ViewProducts(
    ProductService productService)
{
    // Retrieves all products from ProductService.
    List<Product> products =
        productService.GetProducts();


    // Checks if there are any products in the list.
    if (products.Count == 0)
    {
        // Displays an error message if the list is empty.
        WriteError("No products available.");

        // Stops this method.
        return;
    }


    // Displays a heading for the product list.
    Console.WriteLine("--- Product List ---");


    // Loops through every product in the list.
    foreach (Product product in products)
    {
        // Displays the product name and product number.
        Console.WriteLine("- " + product.Name + "-" + product.Number);
    }
}


// Searches for products in the product list.
//
// productService: The ProductService object used
// to search for products.
static void SearchProduct(
    ProductService productService)
{
    // Asks the user to enter text to search for.
    Console.Write("Search: ");

    // Reads the search text entered by the user.
    //string? searchText = Console.ReadLine();
    string searchText = (Console.ReadLine() ?? "").Trim();

    // Checks if the user entered an empty search value.
    if (string.IsNullOrWhiteSpace(searchText))
    {
        // Displays an error message.
        WriteError(
            "Search text cannot be empty.");

        // Stops this method.
        return;
    }


    // Searches for products using the entered search text.
    List<Product> results =
        productService.SearchProducts(searchText);


    // Checks if any matching products were found.
    if (results.Count == 0)
    {
        // Displays an error message.
        WriteError("No products found.");

        // Stops this method.
        return;
    }


    // Displays a heading for the search results.
    Console.WriteLine("\nResults:");


    // Loops through all matching products.
    foreach (Product product in results)
    {
        // Displays each matching product.
        Console.WriteLine(
            $"- {product.Name}-{product.Number}");
    }
}


// Deletes a product from the product list.
//
// productService: The ProductService object used
// to find and delete products.
static void DeleteProduct(
    ProductService productService)
{
    // Asks the user to enter the product to delete.
    Console.Write(
        "Enter product to delete: ");

    // Reads the product entered by the user.
    //string? input = Console.ReadLine();
    string input = (Console.ReadLine() ?? "").Trim();


    // Checks if the input is empty.
    if (string.IsNullOrWhiteSpace(input))
    {
        // Displays an error message.
        WriteError(
            "Product cannot be empty.");

        // Stops this method.
        return;
    }


    // Attempts to delete the product.
    bool deleted =
        productService.DeleteProduct(input);


    // Checks if the product was successfully deleted.
    if (deleted)
    {
        // Displays a success message.
        WriteSuccess(
            "Product removed successfully.");
    }
    else
    {
        // Displays an error message if the product
        // was not found or the format was invalid.
        WriteError(
            "Product not found or invalid format.");
    }
}


// Displays statistics about the products.
//
// productService: The ProductService object used
// to retrieve and calculate product statistics.
static void ShowStatistics(
    ProductService productService)
{
    // Calls the ShowStatistics method in ProductService.
    // The method displays:
    // - Total products
    // - Highest product number
    // - Lowest product number
    // - Average product number
    productService.ShowStatistics();
}


// Displays a success message in green.
//
// message: The message that will be displayed.
static void WriteSuccess(string message)
{
    // Changes the console text color to green.
    Console.ForegroundColor =
        ConsoleColor.Green;

    // Displays the success message.
    Console.WriteLine(message);

    // Resets the console color to the default color.
    Console.ResetColor();
}


// Displays an error message in red.
//
// message: The error message that will be displayed.
static void WriteError(string message)
{
    // Changes the console text color to red.
    Console.ForegroundColor =
        ConsoleColor.Red;

    // Displays the error message.
    Console.WriteLine(message);

    // Resets the console color to the default color.
    Console.ResetColor();
}

