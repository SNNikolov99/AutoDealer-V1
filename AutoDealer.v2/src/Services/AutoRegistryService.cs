using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.IO;
using System.Text;
using System.Threading.Tasks;
using AutoDealerV2.src.Classes;



namespace AutoDealerV2.src.Services
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
        
      
        public List<Vehicle> Vehicles => vehicles;

        public AutoRegistryService(List<Vehicle> vehicles)
        {
            if (vehicles == null)
            {
                throw new ArgumentNullException(nameof(vehicles), "Input cannot be null");
            }
            this.vehicles = vehicles;
            nextId = vehicles.Max(v => v.Id);

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

        

    }
}
