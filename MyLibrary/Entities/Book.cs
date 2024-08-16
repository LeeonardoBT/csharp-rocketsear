using System.Reflection;

namespace MyLibrary.Entities;

public class Book
{
    public int Id { get; private set; }
    public string Title { get; private set; } = string.Empty;
    public string Author { get; private set; } = string.Empty;
    public string Gender { get; private set; } = string.Empty;
    public double Price {  get; private set; }
    public int Stock { get; private set; }

    public Book(string title, string author, string gender, double price, int stock)
    {
        Random random = new Random();

        Id = random.Next(1, 100);
        Title = title;
        Author = author;
        Gender = gender;
        Price = price;
        Stock = stock;
    }

    public Book(int id, double price, int stock)
    {
        Id = id;
        Title = "Controle de estoque e preço";
        Author = "Pessoal";
        Gender = "Logística";
        Price = price;
        Stock = stock;
    }
}
