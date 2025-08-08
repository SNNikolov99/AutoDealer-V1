using AutoDealer.Models;
using AutoDealer.src.Decorators;
using AutoDealer.src.Decorators.CarDecorators;
using AutoDealer.src.Decorators.MinibusDecorators;
using AutoDealer.src.Decorators.MotorbikeDecorators;
using Microsoft.Win32;
using Spectre;
using Spectre.Console;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Globalization;
using System.Linq;
using System.Reflection;

namespace AutoDealer.ConsoleApp
{
    class Program
    {
        static AutoRegistry registry = new AutoRegistry("C:\\Users\\Simeon\\source\\repos\\SNNikolov99\\AutoDealer\\AutoDealer\\resources\\list1.txt");
        
        static void Main(string[] args)
        {
            //registry.WriteToCSVFile("C:\\Users\\Simeon\\source\\repos\\SNNikolov99\\AutoDealer\\AutoDealer\\resources\\list2.txt");
            ShowMainConsole();

        }

        static void ShowMainConsole()
        {
            bool exit = false;

            while (!exit)
            {
                Console.Clear();
                Console.WriteLine("\n========================= AutoDealer Registry ==========================");
                Console.WriteLine("1. Add Vehicle");
                Console.WriteLine("2. List Vehicles");
                Console.WriteLine("3. Sort Vehicles");
                Console.WriteLine("4. Filter Vehicles");
                Console.WriteLine("5. Remove Vehicle");
                Console.WriteLine("6. Get total sum");
                Console.WriteLine("7. Modify vehicle by ID");
                Console.WriteLine("8. Exit");
                Console.Write("Select an option: ");

                var choice = Console.ReadLine();
                Console.Clear();

                try
                {
                    switch (choice)
                    {
                        case "1":
                            bool addAnother = true;
                            while (addAnother)
                            {
                                AddVehicle(registry);
                                Console.WriteLine("Would you like to add another? (Yes , no)");
                                if (Console.ReadLine().ToLower() == "no")
                                {
                                    addAnother = false;
                                }
                                else
                                {
                                    continue;
                                }
                            }
                            break;
                        case "2":
                            //ListVehicles(registry.Vehicles);
                            RenderVehicles(registry.Vehicles);
                            Console.WriteLine("\nTo return to the main console, press any key ...");
                            Console.ReadKey(true);
                            break;
                        case "3":
                            SortVehicles(registry);
                            Console.WriteLine("\nTo return to the main console, press any key ...");
                            Console.ReadKey(true);
                            break;
                        case "4":
                            FilterVehicles(registry);
                            Console.WriteLine("\nTo return to the main console, press any key ...");
                            Console.ReadKey(true);
                            break;
                        case "5":
                            RemoveVehicle(registry);
                            Console.WriteLine("\nTo return to the main console, press any key ...");
                            Console.ReadKey(true);
                            break;
                        case "6":
                            GetTotalSum(registry);
                            Console.WriteLine("\nTo return to the main console, press any key ...");
                            Console.ReadKey(true);
                            break;
                        case "7":
                            ModifyExistingVehicle(registry);
                            Console.WriteLine("\nTo return to the main console, press any key ...");
                            Console.ReadKey(true);
                            break;
                        case "8":
                            exit = true;
                            break;
                        default:
                            Console.WriteLine("Invalid option. Please try again.");
                            break;
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Error: {ex.Message}");
                    Console.WriteLine("\nTo return to the main console, press any key ...");
                    Console.ReadKey(true);
                }
            }

            Console.WriteLine("Goodbye!");
        }

        static void AddVehicle(AutoRegistry registry)
        {
            Console.WriteLine("\n---------------- Add vehicle ----------------------");
            Console.WriteLine("Select vehicle type: 1. Car  2. MiniBus  3. Motorbike");
            Console.Write("Choice: ");
            var typeChoice = Console.ReadLine();

            Console.Write("Brand: ");
            var brand = Console.ReadLine();
            Console.Write("Model: ");
            var model = Console.ReadLine();
            Console.Write("Year: ");
            var year = int.Parse(Console.ReadLine() ?? "0");
            Console.Write("Price: ");
            var price = decimal.Parse(Console.ReadLine() ?? "0");
            Console.Write("Color: ");
            var color = Console.ReadLine();
            Console.Write("HorsePower: ");
            var hp = int.Parse(Console.ReadLine() ?? "0");
            Console.Write("FuelType: ");
            var fuel = Console.ReadLine();

            Vehicle vehicle;
            switch (typeChoice)
            {
                case "1":
                    vehicle = new Car(brand, model, year, price, color, hp, fuel);
                    break;
                case "2":
                    vehicle = new MiniBus(brand, model, year, price, color, hp, fuel);
                    break;
                case "3":
                    vehicle = new Motorbike(brand, model, year, price, color, hp, fuel);
                    break;
                default:
                    Console.WriteLine("Unknown type, defaulting to Car.");
                    vehicle = new Car(brand, model, year, price, color, hp, fuel);
                    break;
            }

            registry.AddVehicle(vehicle);
            Console.WriteLine($"Vehicle added with ID: {vehicle.Id}");
        }

        //static void ListVehicles(List<Vehicle> vehicles)
        //{
        //    Console.Clear();
        //    Console.WriteLine("\n-------------------- Inventory -----------------------");
        //    if (!vehicles.Any())
        //    {
        //        Console.WriteLine("No vehicles in registry.");
        //        return;
        //    }
        //    Console.WriteLine("| ID | Brand | Model | year | Price | Color | Horsepower | Fuel type |");
        //    foreach (var v in vehicles)
        //    {
        //        Console.WriteLine($"(ID: {v.Id})" + v.ToString());
        //    }
        //}


        //Using Spectre console
        static void RenderVehicles(List<Vehicle> vehicles)
        {
            Console.OutputEncoding = System.Text.Encoding.UTF8;

            var table = new Table()
                .Border(TableBorder.Rounded)
                .ShowRowSeparators()
                .Title("[bold green]Auto Registry[/]")
                .Caption($"[grey]Count: {vehicles.Count()}[/]");

            table.AddColumn("[bold]Id[/]");
            table.AddColumn("Brand");
            table.AddColumn("Model");
            table.AddColumn(new TableColumn("Year").RightAligned());
            table.AddColumn(new TableColumn("Price").RightAligned());
            table.AddColumn("Color");
            table.AddColumn(new TableColumn("HP").RightAligned());
            table.AddColumn("Fuel");

            var i = 0;
            foreach (var v in vehicles)
            {
                var row = new[]
                {
                v.Id.ToString(),
                v.Brand,
                v.Model,
                v.Year.ToString(),
                v.Price.ToString("N2"),
                v.Color,
                v.HorsePower.ToString(),
                v.FuelType
            };

                // zebra striping
                if (i++ % 2 == 1)
                    table.AddRow(row.Select(c => $"[grey]{Markup.Escape(c)}[/]").ToArray());
                else
                    table.AddRow(row.Select(Markup.Escape).ToArray());
            }

            AnsiConsole.Write(table);
        }

        static void SortVehicles(AutoRegistry registry)
        {
            Console.WriteLine("\n-------------- Sort registry ---------------");
            Console.Write("Property to sort by (e.g., Brand, Year, Price): ");
            var prop = Console.ReadLine();
            Console.Write("Descending? (y/n): ");
            var descInput = Console.ReadLine();
            bool desc = descInput?.ToLower() == "y";

            var sorted = registry.SortVehiclesByProperty(prop, desc);

            //ListVehicles(sorted);
            RenderVehicles(sorted);
        }

        static void FilterVehicles(AutoRegistry registry)
        {
            Console.WriteLine("\n------------ Filter vehicles ---------------");
            Console.Write("Property to filter by (e.g., Brand, Year, Price): ");
            var prop = Console.ReadLine().ToString().ToLower();


            if (prop == "brand" || prop == "model" || prop == "color" || prop == "fuel*type")
            {
                Console.WriteLine("Operator:  1.Equals  6.Contains");
            }
            else if (prop == "year" || prop == "horsepower" || prop == "price")
            {
                Console.WriteLine("Operator:  1.Equals   2.Greater Than   3.Equal Or Greater Than   4.Less Than   5.Equal Or Less Than    6.Contains");
            }

            Console.Write("Choice: ");
            var opChoice = Console.ReadLine();
            var op = (FilterOperator)(int.Parse(opChoice ?? "1") - 1);
            Console.Write("Value: ");
            var value = Console.ReadLine();
            var filtered = registry.FilterByProperty(prop, value, op);

            //ListVehicles(filtered);
            RenderVehicles(filtered);
        }

        static void RemoveVehicle(AutoRegistry registry)
        {
            Console.WriteLine("\n------------ Remove Vehicle ---------------");
            Console.Write("Enter vehicle ID to remove: ");
            var id = int.Parse(Console.ReadLine() ?? "0");
            registry.RemoveVehicleByID(id);
            Console.WriteLine($"Vehicle with ID {id} removed.");
        }

        static void GetTotalSum(AutoRegistry registry)
        {
            Console.Clear();
            Console.WriteLine("\n--------- Total sum ---------------");
            decimal res = registry.GetInventoryTotalAmount();
            Console.WriteLine("Total amount of inventory in stock in leva is: " + res.ToString());
        }

        static void ModifyExistingVehicle(AutoRegistry registry)
        {
            Console.Clear();
            Console.WriteLine("\n--------- Modify existing vehicle ---------------");

            Console.WriteLine("Enter a ID which you want to modify.(0,1,2,3, etc )");
            int ID = Convert.ToInt32(Console.ReadLine());

            //IDs start from 1, not 0
            if (ID > registry.Vehicles.Count + 1 || ID <= 0)
            {
                throw new ArgumentException("The list does not contain such ID");
            }


            switch (registry.Vehicles[ID].GetVehicleType().ToLower())
            {
                case "car":
                    Console.WriteLine("Would you like a V6 TDI engine? ( Yes,no ):");
                    if (Console.ReadLine().ToLower() == "yes")
                    {
                        var v6 = new V6TDIEngine((Car)registry.Vehicles[ID]);
                        v6.AttachPart();

                    }
                    Console.WriteLine("Would you hybrid traction? ( Yes,no ):");
                    if ((Console.ReadLine().ToLower() == "yes"))
                    {
                        var hybrid = new HybridTraction((Car)registry.Vehicles[ID]);
                        hybrid.AttachPart();
                    }
                    break;
                case "motorbike":
                    Console.WriteLine("Would you like a 750cc engine? ( Yes,no ):");
                    if (Console.ReadLine().ToLower() == "yes")
                    {
                        var supermoto = new _750ccEngine((Motorbike)registry.Vehicles[ID]);
                        supermoto.AttachPart();

                    }
                    break;
                case "minibus":
                    Console.WriteLine("Would you like the Minibus to be long based? ( Yes,no ):");
                    if (Console.ReadLine().ToLower() == "yes")
                    {
                        var longBase = new LongBase((MiniBus)registry.Vehicles[ID]);
                        longBase.AttachPart();

                    }
                    break;


            }

            Console.WriteLine("Would you like a nice color? ( Yes,no ):");
            if (Console.ReadLine().ToLower() == "yes")
            {
                var newColor = new DesignerColorDecocrator(registry.Vehicles[ID]);
                newColor.AttachPart();
            }
        }

    }
   
}
