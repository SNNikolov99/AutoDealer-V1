using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.IO;
using System.Text;
using System.Threading.Tasks;

namespace AutoDealer.Models
{

    public enum FilterOperator
    {
        Equals,
        GreaterThan,
        EqualOrGreaterThan,
        LessThan,
        EqualOrLessThan,
        Contains
    }


    public static class StringExtensions
    {
        // have a string look as a property name 
        public static string CapitalizeFirst(this string s) =>
            string.IsNullOrEmpty(s)
                ? s
                : char.ToUpper(s[0]) + s.Substring(1).ToLower();
    }


    public class AutoRegistryService
    {
        private List<Vehicle> vehicles;
        private int nextId = 1;
        private string CSVFilename;
        
      
        public List<Vehicle> Vehicles => vehicles;

        public AutoRegistryService(List<Vehicle> vehicles)
        {
            if (vehicles == null)
            {
                throw new ArgumentNullException(nameof(vehicles), "Input cannot be null");
            }
            this.vehicles = new List<Vehicle>(vehicles);

        }
        
        public AutoRegistryService(string filename)
        {
            vehicles = new List<Vehicle>();
            ReadFromCSVFile(filename);
            CSVFilename = filename;
        }


        public AutoRegistryService()
        {
            vehicles = new List<Vehicle>();
        }


        public void AddVehicle(Vehicle vehicle)
        {
            if (vehicle == null)
            {
                throw new ArgumentNullException(nameof(vehicle), "Input cannot be null");
            }

            if (string.IsNullOrWhiteSpace(vehicle.Brand)
              || string.IsNullOrWhiteSpace(vehicle.Model)
              || string.IsNullOrWhiteSpace(vehicle.FuelType)
              || string.IsNullOrWhiteSpace(vehicle.Color)
              || vehicle?.HorsePower == null
              || vehicle?.Price == null
              || vehicle?.Year == null)
            {
                throw new ArgumentException("One or more vehicle fields are missing or invalid");
            }
            vehicle.Id = nextId++;
            vehicles.Add(vehicle);
        }

        public void RemoveVehicleByID(int id)
        {
            Vehicle vehicle = vehicles.FirstOrDefault(v => v.Id == id);
            if (vehicle == null)
            {
                throw new ArgumentException($"No vehicle found with ID {id}");
            }
            vehicles.Remove(vehicle);
        }

        public List<Vehicle> SortVehiclesByProperty(string propertyName, bool sortDescending)
        {
           
            if (string.IsNullOrEmpty(propertyName))
            {
                throw new ArgumentException("Property name cannot be null or empty", nameof(propertyName));
            }

            PropertyInfo prop = typeof(Vehicle).GetProperty(propertyName.CapitalizeFirst());
            if (prop == null)
            {
                throw new ArgumentException($"Property '{propertyName}' does not exist");
            }

            List<Vehicle> res = sortDescending 
                ? vehicles.OrderByDescending(v => prop.GetValue(v)).ToList()
                : vehicles.OrderBy(v => prop.GetValue(v)).ToList();

            return res;
        }

        public List<Vehicle> FilterByProperty(string propertyName,string value,FilterOperator op )
        {
            if (string.IsNullOrEmpty(propertyName))
            {
                throw new ArgumentException("Property name cannot be null or empty", nameof(propertyName));
            }
            if (string.IsNullOrEmpty(value))
            {
                throw new ArgumentException("Value cannot be null or empty", nameof(propertyName));
            }

            PropertyInfo prop = typeof(Vehicle).GetProperty(propertyName.CapitalizeFirst());
            if (prop == null)
            {
                throw new ArgumentException($"Property '{propertyName}' does not exist");
            }

            return vehicles.Where(v =>
            {
                object propValue = prop.GetValue(v);
                if (propValue == null)
                {
                    return false;
                }

                /*  SWITCH ON PROP VALUE TYPE:
                // Here we determine what kind of value the property holds (string, int, decimal, etc.)
                 and then apply filtering based on that.
                */
                switch(propValue)
                {
                    case string strVal:
                        switch (op)
                        {
                            case FilterOperator.Equals:
                                return strVal.Equals(value);

                            case FilterOperator.Contains:
                                return strVal.Contains(value);

                            default:
                                throw new InvalidOperationException($"Operator '{op}' is not valid for string properties.");

                        }
                    case IComparable cmp:
                        object converted;
                        try
                        {
                            // Convert the input string `value` to the same type as the property
                            converted = Convert.ChangeType(value, prop.PropertyType);
                        }
                        catch
                        {
                            return false; // Skip this item if conversion fails
                        }

                        int comparison = cmp.CompareTo(converted);

                        switch(op)
                        {
                            case FilterOperator.Equals:
                                return comparison == 0;

                            case FilterOperator.GreaterThan:
                                return comparison > 0;

                            case FilterOperator.EqualOrGreaterThan:
                                return comparison >= 0;

                            case FilterOperator.EqualOrLessThan:
                                return comparison <= 0;

                            case FilterOperator.LessThan:
                                return comparison < 0;

                            default:
                                throw new InvalidOperationException($"Operator '{op}' is not valid for numeric properties.");
                        }

                    //  Unsupported property type
                    default:
                        return false;
                }
            }).ToList();
        }

        public decimal GetInventoryTotalAmount()
        {
            decimal acc = 0;

            if (vehicles.Count == 0)
            {
                throw new ArgumentException("The Registry is empty");
            }
            foreach (Vehicle vehicle in vehicles)
            {
                acc += vehicle.Price;
            }

            return acc;
        }

        public void ReadFromCSVFile(string filename)
        {
            if(string.IsNullOrEmpty(filename) || !System.IO.File.Exists(filename))
            {
                throw new ArgumentException("File path is invalid or file is emptry");
            }

            string[] lines = System.IO.File.ReadAllLines(filename);
            int row = 0;

            foreach (string line in lines)
            {
                if(string.IsNullOrWhiteSpace(line) || line.StartsWith("#"))
                {
                    throw new ArgumentException("line number " + row.ToString() + " is empty");
                }

                string[] parts = line.Split(',');
                if(parts.Length != 8)
                {
                    throw new ArgumentException("There is an emptry field in this row" + row.ToString());
                }

                var type = parts[0].Trim();
                var brand = parts[1].Trim();
                var model = parts[2].Trim();
                var year = int.Parse(parts[3]);
                var price = decimal.Parse(parts[4]);
                var color = parts[5].Trim();
                var hp = int.Parse(parts[6]);
                var fuel = parts[7].Trim();


                Vehicle vehicle = null;

                switch (type.ToLower()) {
                    case "car":
                        vehicle = new Car(brand, model, year, price, color, hp, fuel);
                        break;
                    case "minibus":
                        vehicle = new MiniBus(brand, model, year, price, color, hp, fuel);
                        break;
                    case "motorbike":
                        vehicle = new Motorbike(brand, model, year, price, color, hp, fuel);
                        break;
                    default:
                        throw new ArgumentException("such type doesn`t exist");


                }

                if (vehicle != null)
                {
                    AddVehicle(vehicle);
                    row++;
                }
            }
        }

        public void WriteToCSVFile(string filename)
        {
            if (!File.Exists(filename))
            {
                File.Create(filename);
            }
            //File.OpenWrite(filename);

            foreach(Vehicle vehicle in Vehicles)
            {
                File.AppendAllText(filename, vehicle.ToString() + "\n");
                
            }
            
        }

    }
}
