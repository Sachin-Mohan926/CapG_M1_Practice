// using System;
// using System.Collections.Generic;
// using System.Linq;

// public interface IProduct
// {
//     int Id { get; }
//     string Name { get; }
//     decimal Price { get; set; }
//     Category Category { get; }
// }

// public enum Category { Electronics, Clothing, Books, Groceries }

// public class ProductRepository<T> where T : class, IProduct
// {
//     private readonly List<T> _products = new List<T>();

//     public void AddProduct(T product)
//     {
//         if (product == null)
//             throw new ArgumentNullException(nameof(product));

//         if (product.Id <= 0)
//             throw new ArgumentException("Product ID must be positive.");

//         if (_products.Any(p => p.Id == product.Id))
//             throw new InvalidOperationException("Duplicate product ID is not allowed.");

//         if (string.IsNullOrWhiteSpace(product.Name))
//             throw new ArgumentException("Product name cannot be null or empty.");

//         if (product.Price <= 0)
//             throw new ArgumentException("Product price must be positive.");

//         _products.Add(product);
//     }

//     public IEnumerable<T> FindProducts(Func<T, bool> predicate)
//     {
//         if (predicate == null)
//             throw new ArgumentNullException(nameof(predicate));

//         return _products.Where(predicate);
//     }

//     public decimal CalculateTotalValue()
//     {
//         return _products.Sum(p => p.Price);
//     }

//     public IReadOnlyList<T> GetAll() => _products.AsReadOnly();
// }

// public class ElectronicProduct : IProduct
// {
//     public int Id { get; set; }
//     public string Name { get; set; }
//     public decimal Price { get; set; }
//     public Category Category => Category.Electronics;

//     public int WarrantyMonths { get; set; }
//     public string Brand { get; set; }
// }

// public class ClothingProduct : IProduct
// {
//     public int Id { get; set; }
//     public string Name { get; set; }
//     public decimal Price { get; set; }
//     public Category Category => Category.Clothing;

//     public string Size { get; set; }
//     public string Material { get; set; }
// }

// public class BookProduct : IProduct
// {
//     public int Id { get; set; }
//     public string Name { get; set; }
//     public decimal Price { get; set; }
//     public Category Category => Category.Books;

//     public string Author { get; set; }
// }

// public class GroceryProduct : IProduct
// {
//     public int Id { get; set; }
//     public string Name { get; set; }
//     public decimal Price { get; set; }
//     public Category Category => Category.Groceries;

//     public DateTime ExpiryDate { get; set; }
// }


// public class DiscountedProduct<T> where T : IProduct
// {
//     private readonly T _product;
//     private readonly decimal _discountPercentage;

//     public DiscountedProduct(T product, decimal discountPercentage)
//     {
//         _product = product ?? throw new ArgumentNullException(nameof(product));

//         if (discountPercentage < 0 || discountPercentage > 100)
//             throw new ArgumentOutOfRangeException(nameof(discountPercentage), "Discount must be between 0 and 100.");

//         _discountPercentage = discountPercentage;
//     }

//     public decimal DiscountedPrice => Math.Round(_product.Price * (1 - _discountPercentage / 100), 2);

//     public override string ToString()
//     {
//         return $"{_product.Name} | Original: {_product.Price:C} | Discount: {_discountPercentage}% | Final: {DiscountedPrice:C}";
//     }
// }


// public class InventoryManager
// {
//     public void ProcessProducts<T>(IEnumerable<T> products) where T : IProduct
//     {
//         if (products == null) throw new ArgumentNullException(nameof(products));

//         Console.WriteLine("\n=== Products ===");
//         foreach (var p in products)
//             Console.WriteLine($"{p.Name} - {p.Price:C} - {p.Category}");

//         var mostExpensive = products.OrderByDescending(p => p.Price).FirstOrDefault();
//         Console.WriteLine($"\nMost Expensive: {mostExpensive?.Name} - {mostExpensive?.Price:C}");

//         Console.WriteLine("\n=== Grouped By Category ===");
//         var grouped = products.GroupBy(p => p.Category);
//         foreach (var group in grouped)
//         {
//             Console.WriteLine(group.Key);
//             foreach (var p in group)
//                 Console.WriteLine($"   {p.Name} - {p.Price:C}");
//         }

//         Console.WriteLine("\n=== 10% Discount on Electronics > $500 ===");
//         foreach (var p in products.Where(p => p.Category == Category.Electronics && p.Price > 500))
//         {
//             var discounted = new DiscountedProduct<T>(p, 10);
//             Console.WriteLine(discounted);
//         }
//     }

//     public void UpdatePrices<T>(List<T> products, Func<T, decimal> priceAdjuster)
//         where T : IProduct
//     {
//         if (products == null) throw new ArgumentNullException(nameof(products));
//         if (priceAdjuster == null) throw new ArgumentNullException(nameof(priceAdjuster));

//         foreach (var p in products)
//         {
//             try
//             {
//                 decimal newPrice = priceAdjuster(p);
//                 if (newPrice <= 0)
//                     throw new InvalidOperationException("Adjusted price must be positive.");

//                 p.Price = newPrice;
//             }
//             catch (Exception ex)
//             {
//                 Console.WriteLine($"Price update failed for {p.Name}: {ex.Message}");
//             }
//         }
//     }
// }


// class Program
// {
//     public static void Main()
//     {
//         var repo = new ProductRepository<IProduct>();

//         var products = new List<IProduct>
//         {
//             new ElectronicProduct { Id = 1, Name = "Laptop", Price = 1200, Brand = "Dell", WarrantyMonths = 24 },
//             new ElectronicProduct { Id = 2, Name = "Smartphone", Price = 800, Brand = "Samsung", WarrantyMonths = 12 },
//             new ClothingProduct { Id = 3, Name = "T-Shirt", Price = 25, Size = "L", Material = "Cotton" },
//             new BookProduct { Id = 4, Name = "Clean Code", Price = 45, Author = "Robert C. Martin" },
//             new GroceryProduct { Id = 5, Name = "Milk", Price = 3, ExpiryDate = DateTime.Today.AddDays(5) }
//         };

//         foreach (var p in products)
//             repo.AddProduct(p);

//         Console.WriteLine($"Total Inventory Value: {repo.CalculateTotalValue():C}");

//         var electronicsByBrand = repo.FindProducts(p =>
//             p is ElectronicProduct e && e.Brand.Contains("Sam", StringComparison.OrdinalIgnoreCase));

//         Console.WriteLine("\nElectronics by brand 'Sam':");
//         foreach (var e in electronicsByBrand)
//             Console.WriteLine(e.Name);

//         var manager = new InventoryManager();
//         manager.ProcessProducts(products);

//         Console.WriteLine("\n=== Bulk Price Update (+5%) ===");
//         manager.UpdatePrices(products, p => p.Price * 1.05m);

//         Console.WriteLine($"Total Inventory Value After Update: {repo.CalculateTotalValue():C}");
//     }
// }
