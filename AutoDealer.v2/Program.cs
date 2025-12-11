using AutoDealerV2.src.Classes;
using AutoDealerV2.src.Services;
using AutoDealerV2.src.Decorators;
using AutoDealerV2.src.Decorators.CarDecorators;
using AutoDealerV2.src.Decorators.MotorbikeDecorators;
using AutoDealerV2.src.Decorators.MinibusDecorators;
using Microsoft.Win32;
using Spectre;
using Spectre.Console;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Globalization;
using System.Linq;
using System.Reflection;

namespace AutoDealerV2
{
    class Program
    {
        static void Main(string[] args)
        {

            //if (args.Length == 0)
            //{
            //    Console.WriteLine("Usage: AutoRegistry <path-to-file>");
            //    return;
            //}

           // string filePath = args[0];

            SerialisationService serialisationService = new SerialisationService("C:\\Users\\Simeon\\source\\repos\\SNNikolov99\\AutoDealer\\AutoDealer.v2\\resources\\list1.csv");
            AutoRegistryService registry = new AutoRegistryService(serialisationService.Load());
            ShowMainConsole(registry,serialisationService);

        }

        static void ShowMainConsole(AutoRegistryService registry, SerialisationService serialisationService)
        {
            bool exit = false;

            while (!exit)
            {
                AnsiConsole.Clear();


                string choice = AnsiConsole.Prompt(
                     new SelectionPrompt<string>()
                     .Title("\"\\n========================= AutoDealer Registry ==========================\"").
                     AddChoices(new string[]
                     {
                     "Add Vehicle",
                     "List Vehicles",
                     "Sort Vehicles",
                     "Filter Vehicles",
                     "Remove Vehicle",
                     "Get total sum",
                     "Modify vehicle by ID",
                     "Save & Exit"
                     }));

                try
                {
                    switch (choice)
                    {
                        case "Add Vehicle":
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
                        case "List Vehicles":
                            RenderVehicles(registry.Vehicles);
                            Console.WriteLine("\nTo return to the main console, press any key ...");
                            Console.ReadKey(true);
                            break;
                        case "Sort Vehicles":
                            SortVehicles(registry);
                            Console.WriteLine("\nTo return to the main console, press any key ...");
                            Console.ReadKey(true);
                            break;
                        case "Filter Vehicles":
                            FilterVehicles(registry);
                            Console.WriteLine("\nTo return to the main console, press any key ...");
                            Console.ReadKey(true);
                            break;
                        case "Remove Vehicle":
                            RemoveVehicle(registry);
                            Console.WriteLine("\nTo return to the main console, press any key ...");
                            Console.ReadKey(true);
                            break;
                        case "Get total sum":
                            GetTotalSum(registry);
                            Console.WriteLine("\nTo return to the main console, press any key ...");
                            Console.ReadKey(true);
                            break;
                        case "Modify vehicle by ID":
                            //ModifyExistingVehicle(registry);
                            Console.WriteLine("\nTo return to the main console, press any key ...");
                            Console.ReadKey(true);
                            break;
                        case "Save & Exit":
                            serialisationService.Save(registry.Vehicles);
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



        static void AddVehicle(AutoRegistryService registry)
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
                    vehicle = new Car(brand!, model!, year, price, color!, hp, fuel!);
                    break;
                case "2":
                    vehicle = new MiniBus(brand!, model!, year, price, color!, hp, fuel!);
                    break;
                case "3":
                    vehicle = new Motorbike(brand!, model!, year, price, color!, hp, fuel!);
                    break;
                default:
                    Console.WriteLine("Unknown type, defaulting to Car.");
                    vehicle = new Car(brand!, model!, year, price, color!, hp, fuel!);
                    break;
            }

            registry.AddVehicle(vehicle);
            Console.WriteLine($"Vehicle added with ID: {vehicle.Id}");
        }



        //Using Spectre console
        static void RenderVehicles(List<Vehicle> vehicles)
        {
            //Console.OutputEncoding = System.Text.Encoding.UTF8;

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

        static void SortVehicles(AutoRegistryService registry)
        {
            Console.WriteLine("\n-------------- Sort registry ---------------");


            string property = AnsiConsole.Prompt(
             new SelectionPrompt<string>()
             .Title("Select a property to sort by")
             .AddChoices(new string[]
             {
                    "Brand",
                    "Model",
                    "Year",
                    "Price",
                    "Color",
                    "Horsepower",
                    "Fuel type"
             }));

            string descending = AnsiConsole.Prompt(
                new SelectionPrompt<string>()
                .Title("Descending or Ascending?")
                .AddChoices(new string[] { "Descending", "Ascending" })

                );

            bool isDescending = descending == "Descending" ? true : false;
            var sorted = registry.SortVehiclesByProperty(property, isDescending);

            RenderVehicles(sorted);
        }

        static void FilterVehicles(AutoRegistryService registry)
        {
            Console.WriteLine("\n------------ Filter vehicles ---------------");
            // Console.Write("Property to filter by (e.g., Brand, Year, Price): ");
            // var prop = Console.ReadLine().ToString().ToLower();


            string property = AnsiConsole.Prompt(
               new SelectionPrompt<string>()
               .Title("Select a property to filter by")
               .AddChoices(new string[]
               {
                    "Brand",
                    "Model",
                    "Year",
                    "Price",
                    "Color",
                    "Horsepower",
                    "Fuel type"
               })).ToLower();


            FilterOperator op = FilterOperator.Contains;

            if (property == "brand" || property == "model" || property == "color" || property == "fuel*type")
            {
                op = AnsiConsole.Prompt(
                  new SelectionPrompt<FilterOperator>()
                  .Title("Select an operation")
                  .AddChoices(new FilterOperator[]
                  {
                        FilterOperator.Equals,
                        FilterOperator.Contains

                  }));

            }
            else if (property == "year" || property == "horsepower" || property == "price")
            {
                op = AnsiConsole.Prompt(
                   new SelectionPrompt<FilterOperator>()
                   .Title("Select a property to filter by")
                   .AddChoices(new FilterOperator[]
                   {
                         FilterOperator.Equals,
                         FilterOperator.GreaterThan,
                         FilterOperator.EqualOrGreaterThan,
                         FilterOperator.LessThan,
                         FilterOperator.EqualOrLessThan,
                         FilterOperator.Contains
                   }));



            }


            Console.Write("Value: ");
            var value = Console.ReadLine();
            var filtered = registry.FilterByProperty(property, value, op);

            RenderVehicles(filtered);
        }

        static void RemoveVehicle(AutoRegistryService registry)
        {
            Console.WriteLine("\n------------ Remove Vehicle ---------------");
            Console.Write("Enter vehicle ID to remove: ");
            var id = int.Parse(Console.ReadLine() ?? "0");
            registry.RemoveVehicleByID(id);
            Console.WriteLine($"Vehicle with ID {id} removed.");
        }

        static void GetTotalSum(AutoRegistryService registry)
        {
            Console.Clear();
            Console.WriteLine("\n--------- Total sum ---------------");
            decimal res = registry.GetInventoryTotalAmount();
            Console.WriteLine("Total amount of inventory in stock in leva is: " + res.ToString());
        }

        static void ModifyExistingVehicle(AutoRegistryService registry)
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
                    if (Console.ReadLine().ToLower() == "yes")
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
