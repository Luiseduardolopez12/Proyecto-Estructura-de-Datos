using System;
using System.Collections.Generic;

class Product
{
    public int Id { get; set; }
    public string Name { get; set; }
    public int Quantity { get; set; }
    public decimal Price { get; set; }

    // Constructor para inicializar las propiedades
    public Product(int id, string name, int quantity, decimal price)
    {
        Id = id;
        Name = name;
        Quantity = quantity;
        Price = price;
    }
}

class Inventory
{
    private List<Product> products = new List<Product>();
    private int nextId = 1;

    public void AddProduct(string name, int quantity, decimal price)
    {
        var product = new Product(nextId++, name, quantity, price);
        products.Add(product);
        Console.WriteLine($"Producto agregado con ID: {product.Id}");
    }

    public void ShowProducts()
    {
        if (products.Count == 0)
        {
            Console.WriteLine("No hay productos en el inventario.");
            return;
        }

        foreach (var product in products)
        {
            Console.WriteLine($"ID: {product.Id} | Nombre: {product.Name} | Cantidad: {product.Quantity} | Precio: {product.Price:C}");
        }
    }

    public void SearchProduct(int id)
    {
        var product = products.Find(p => p.Id == id);
        if (product != null)
        {
            Console.WriteLine($"ID: {product.Id} | Nombre: {product.Name} | Cantidad: {product.Quantity} | Precio: {product.Price:C}");
        }
        else
        {
            Console.WriteLine($"Producto con ID {id} no encontrado.");
        }
    }

    public void DeleteProduct(int id)
    {
        var product = products.Find(p => p.Id == id);
        if (product != null)
        {
            products.Remove(product);
            Console.WriteLine($"Producto con ID {id} eliminado.");
        }
        else
        {
            Console.WriteLine($"Producto con ID {id} no encontrado.");
        }
    }
}

class Program
{
    static void Main(string[] args)
    {
        Inventory inventory = new Inventory();
        int option;

        do
        {
            Console.WriteLine("\n--- Sistema de Gestión de Inventario ---");
            Console.WriteLine("1. Agregar producto");
            Console.WriteLine("2. Mostrar productos");
            Console.WriteLine("3. Buscar producto por ID");
            Console.WriteLine("4. Eliminar producto");
            Console.WriteLine("5. Salir");
            Console.Write("Selecciona una opción: ");

            if (int.TryParse(Console.ReadLine(), out option))
            {
                switch (option)
                {
                    case 1:
                        Console.Write("Nombre del producto: ");
                        string name = Console.ReadLine() ?? string.Empty; // Evita valores nulos
                        if (string.IsNullOrWhiteSpace(name))
                        {
                            Console.WriteLine("Error: El nombre no puede estar vacío.");
                            break;
                        }

                        Console.Write("Cantidad: ");
                        if (!int.TryParse(Console.ReadLine(), out int quantity))
                        {
                            Console.WriteLine("Error: Cantidad no válida.");
                            break;
                        }

                        Console.Write("Precio: ");
                        if (!decimal.TryParse(Console.ReadLine(), out decimal price))
                        {
                            Console.WriteLine("Error: Precio no válido.");
                            break;
                        }

                        inventory.AddProduct(name, quantity, price);
                        break;

                    case 2:
                        inventory.ShowProducts();
                        break;

                    case 3:
                        Console.Write("Ingresa el ID del producto: ");
                        if (!int.TryParse(Console.ReadLine(), out int searchId))
                        {
                            Console.WriteLine("Error: ID no válido.");
                            break;
                        }
                        inventory.SearchProduct(searchId);
                        break;

                    case 4:
                        Console.Write("Ingresa el ID del producto a eliminar: ");
                        if (!int.TryParse(Console.ReadLine(), out int deleteId))
                        {
                            Console.WriteLine("Error: ID no válido.");
                            break;
                        }
                        inventory.DeleteProduct(deleteId);
                        break;

                    case 5:
                        Console.WriteLine("Saliendo del programa...");
                        break;

                    default:
                        Console.WriteLine("Opción no válida. Intenta de nuevo.");
                        break;
                }
            }
            else
            {
                Console.WriteLine("Error: Ingresa un número válido.");
            }

        } while (option != 5);
    }
}