using System;

namespace BookStoreApplication
{
    class Program
    {
        static void Main(string[] args)
        {
            // TODO:
            // 1. Read initial input
            // Format: BookID Title Price Stock
            string[] data = Console.ReadLine().Split(' ');
            
            Book book = new Book()
            {
                Id = data[0],
                Title = data[1],
                Price = int.Parse(data[2]),
                Stock = int.Parse(data[3])
            };

            BookUtility utility = new BookUtility(book);

            while (true)
            {
                // TODO:
                // Display menu:
                // 1 -> Display book details
                Console.WriteLine("1 -> Display book details");
                // 2 -> Update book price
                Console.WriteLine("2 -> Update book price");
                // 3 -> Update book stock
                Console.WriteLine("3 -> Update book stock");
                // 4 -> Exit
                Console.WriteLine("4 -> Exit");

                int choice = 0; // TODO: Read user choice
                choice = int.Parse(Console.ReadLine());
                switch (choice)
                {
                    case 1:
                        utility.GetBookDetails();
                        break;

                    case 2:
                        // TODO:
                        // Read new price
                        int newPrice;
                        newPrice = int.Parse(Console.ReadLine());
                        // Call UpdateBookPrice()
                        utility.UpdateBookPrice(newPrice);
                        break;

                    case 3:
                        // TODO:
                        // Read new stock
                        int newStock;
                        newStock = int.Parse(Console.ReadLine());
                        // Call UpdateBookStock()
                        utility.UpdateBookStock(newStock);
                        break;

                    case 4:
                        Console.WriteLine("Thank You");
                        return;

                    default:
                        // TODO: Handle invalid choice
                        Console.WriteLine("Invalid Choice!");
                        break;
                }
            }
        }
    }
}
