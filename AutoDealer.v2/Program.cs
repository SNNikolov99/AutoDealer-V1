using AutoDealerV2.src.Classes;
using AutoDealerV2.src.Services;
using AutoDealerV2.src.Decorators;
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
            //    AnsiConsole.WriteLine("Usage: AutoRegistry <path-to-file>");
            //    return;
            //}

            // string filePath = args[0];

            SerialisationService<List<Vehicle>> serialisationService = new SerialisationService<List<Vehicle>>("C:\\Users\\Simeon\\Desktop\\Projects\\C#\\AutoDealer\\AutoDealer.v2\\resources\\list1.csv");
            SerialisationService<Pricelist> PriceListSerialisation = new SerialisationService<Pricelist>("C:\\Users\\Simeon\\Desktop\\Projects\\C#\\AutoDealer\\AutoDealer.v2\\resources\\pricelist.json");
            AutoRegistryService registry = new AutoRegistryService(serialisationService.Load());


            Pricelist? pricelist = PriceListSerialisation.Load();


            ShowMainConsole(registry, serialisationService, pricelist);
        }

        static void ShowMainConsole(AutoRegistryService registry, SerialisationService<List<Vehicle>> serialisationService, Pricelist pricelist)
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
                                AddVehicle(registry, pricelist);
                                AnsiConsole.WriteLine("Would you like to add another? (Yes , no)");
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
                            AnsiConsole.WriteLine("\nTo return to the main AnsiConsole, press any key ...");
                            Console.ReadKey(true);
                            break;
                        case "Sort Vehicles":
                            SortVehicles(registry);
                            AnsiConsole.WriteLine("\nTo return to the main AnsiConsole, press any key ...");
                            Console.ReadKey(true);
                            break;
                        case "Filter Vehicles":
                            FilterVehicles(registry);
                            AnsiConsole.WriteLine("\nTo return to the main AnsiConsole, press any key ...");
                            Console.ReadKey(true);
                            break;
                        case "Remove Vehicle":
                            RemoveVehicle(registry);
                            AnsiConsole.WriteLine("\nTo return to the main AnsiConsole, press any key ...");
                            Console.ReadKey(true);
                            break;
                        case "Get total sum":
                            GetTotalSum(registry);
                            AnsiConsole.WriteLine("\nTo return to the main AnsiConsole, press any key ...");
                            Console.ReadKey(true);
                            break;
                        case "Modify vehicle by ID":
                            ModifyExistingVehicle(registry);
                            AnsiConsole.WriteLine("\nTo return to the main AnsiConsole, press any key ...");
                            Console.ReadKey(true);
                            break;
                        case "Save & Exit":
                            serialisationService.Save(registry.Vehicles);
                            exit = true;
                            break;
                        default:
                            AnsiConsole.WriteLine("Invalid option. Please try again.");
                            break;
                    }
                }
                catch (Exception ex)
                {
                    AnsiConsole.WriteLine($"Error: {ex.Message}");
                    AnsiConsole.WriteLine("\nTo return to the main AnsiConsole, press any key ...");
                    Console.ReadKey(true);
                }
            }

            AnsiConsole.WriteLine("Goodbye!");
        }



        static void AddVehicle(AutoRegistryService registry, Pricelist pricelist)
        {
            VehicleBuilder builder = new VehicleBuilder(pricelist);

            AnsiConsole.Clear();
            AnsiConsole.WriteLine("\n---------------- Add vehicle ----------------------");
            AnsiConsole.WriteLine("Select a vehicle from the given list ");

            string modelChoice = AnsiConsole.Prompt(
                new SelectionPrompt<string>()
                .Title("\"\\n========================= Avaliable Models ==========================\"").
                AddChoices(pricelist.models.Keys.ToArray()));

            string engineChoice = AnsiConsole.Prompt(
                new SelectionPrompt<string>()
                .Title("\"\\n========================= Avaliable Engines ==========================\"")
                .AddChoices(pricelist.models[modelChoice].avaliableEngines));

            string colourChoice = AnsiConsole.Prompt(
                new SelectionPrompt<string>()
                .Title("\"\\n========================= Avaliable Colours ==========================\"")
                .AddChoices(pricelist.models[modelChoice].avaliableColours));

            string enginePackages = AnsiConsole.Prompt(
                new SelectionPrompt<string>()
                .Title("\"\\n========================= Avaliable Packages ==========================\"")
                .AddChoices(pricelist.models[modelChoice].avaliablePackages));


            Vehicle vehicle = builder.
                 setModel(modelChoice)
                .setEngine(engineChoice)
                .setColor(colourChoice)
                .setPackage(enginePackages)
                .Build();


            registry.AddVehicle(vehicle);
            AnsiConsole.WriteLine($"Vehicle added with ID: {vehicle.Id}");
        }



        //Using Spectre AnsiConsole
        static void RenderVehicles(List<Vehicle> vehicles)
        {
            //AnsiConsole.OutputEncoding = System.Text.Encoding.UTF8;

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
            table.AddColumn("Description");

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
                v.Colour,
                v.HorsePower.ToString(),
                v.FuelType,
                v.Description
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
            AnsiConsole.WriteLine("\n-------------- Sort registry ---------------");


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
            AnsiConsole.WriteLine("\n------------ Filter vehicles ---------------");
            // AnsiConsole.Write("Property to filter by (e.g., Brand, Year, Price): ");
            // var prop = AnsiConsole.ReadLine().ToString().ToLower();


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


            AnsiConsole.Write("Value: ");
            var value = Console.ReadLine();
            var filtered = registry.FilterByProperty(property, value, op);

            RenderVehicles(filtered);
        }

        static void RemoveVehicle(AutoRegistryService registry)
        {
            AnsiConsole.WriteLine("\n------------ Remove Vehicle ---------------");
            AnsiConsole.Write("Enter vehicle ID to remove: ");
            var id = int.Parse(Console.ReadLine() ?? "0");
            registry.RemoveVehicleByID(id);
            AnsiConsole.WriteLine($"Vehicle with ID {id} removed.");
        }

        static void GetTotalSum(AutoRegistryService registry)
        {
            AnsiConsole.Clear();
            AnsiConsole.WriteLine("\n--------- Total sum ---------------");
            decimal res = registry.GetInventoryTotalAmount();
            AnsiConsole.WriteLine("Total amount of inventory in stock in leva is: " + res.ToString());
        }

        static void ModifyExistingVehicle(AutoRegistryService registry)
        {
            AnsiConsole.Clear();
            AnsiConsole.WriteLine("\n--------- Modify existing vehicle ---------------");

            // Corrected the SelectionPrompt to use string instead of Vehicle
            string IDChoice = AnsiConsole.Prompt(
                new SelectionPrompt<string>()
                .Title("Select a vehicle to modify:")
                .AddChoices(registry.Vehicles.Select(v => $"{v.Id}: {v.Brand} {v.Model}").ToArray()));

            // Extract the ID from the selected string
            int ID = int.Parse(IDChoice.Split(':')[0]);

            string choice = AnsiConsole.Prompt(
                 new SelectionPrompt<string>()
                 .Title("What special package should be added:")
                 .AddChoices(new string[]
                 {
                     "Medical Package",
                     "Police Package",
                     "Utility Package",
                 }));

            switch (choice)
            {
                case "Medical Package":
                    MedicalPackageDecorator medicalPackage = new MedicalPackageDecorator(registry.Vehicles.First(v => v.Id == ID));
                    medicalPackage.AttachPackage();
                    break;
                case "Police Package":
                    PolicePackageDecorator policePackage = new PolicePackageDecorator(registry.Vehicles.First(v => v.Id == ID));
                    policePackage.AttachPackage();
                    break;
                case "Utility Package":
                    UtilityPackageDecorator utilityPackage = new UtilityPackageDecorator(registry.Vehicles.First(v => v.Id == ID));
                    utilityPackage.AttachPackage();
                    break;
                default:
                    AnsiConsole.WriteLine("Invalid option. Please try again.");
                    break;
            }

            AnsiConsole.WriteLine("Vehicle modified successfully.");
        }
    }

}
