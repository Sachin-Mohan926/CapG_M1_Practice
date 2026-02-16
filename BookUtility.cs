using System;

namespace BookStoreApplication
{
    public class BookUtility
    {
        private Book _book;


        public BookUtility(Book book)
        {
            // TODO: Assign book object
            _book = book;
        }

        public void GetBookDetails()
        {
            // TODO:
            // Print format:
            // Details: <BookId> <Title> <Price> <Stock>
            Console.WriteLine($"Details: {_book.Id} {_book.Title} {_book.Price} {_book.Stock}");
        }

        public void UpdateBookPrice(int newPrice)
        {
            // TODO:
            // Validate new price
            // Update price
            _book.Price = newPrice;
            // Print: Updated Price: <newPrice>
            Console.WriteLine($"Updated Price: {newPrice}");
        }

        public void UpdateBookStock(int newStock)
        {
            // TODO:
            // Validate new stock
            // Update stock
            _book.Stock = newStock;
            // Print: Updated Stock: <newStock>
            Console.WriteLine($"Updated Stock: {newStock}");
        }
    }
}
