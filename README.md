# Product List Manager

A simple C# console application for managing a list of products.

## Features

* Add products
* View all products
* Search for products
* Delete products
* Display product statistics
* Save products to a text file
* Validate product input
* Prevent duplicate products

## Product Format

Products must follow this format:

LETTERS-NUMBER

Examples:

CE-400
XX-480
LABAN-231

The product number must be between 200 and 500.

## Menu

1. Add Product
2. View Products
3. Search Product
4. Delete Product
5. Statistics
6. Save to File
7. Exit

The application also accepts `exit` to close the program.

## Project Structure

ProductListManager/
│
├── Program.cs
├── Product.cs
├── ProductService.cs
└── products.txt

### Product

The `Product` class represents a product using a name and product number.

### ProductService

`ProductService` contains the main business logic for adding, searching, deleting, sorting, validating and saving products.

## Saving Data

Products can be saved to `products.txt`. Before saving, they are sorted alphabetically by product name and then by product number.

## Technologies

* C#
* .NET
* Console Application
* Regular Expressions
* LINQ
* File I/O

## How to Run

Open the project in Visual Studio and run the application.

```bash
dotnet run
```

## Author

**Luisiano Denovan Calill**

