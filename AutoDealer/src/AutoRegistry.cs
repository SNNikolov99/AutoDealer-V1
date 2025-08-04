using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
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

    class AutoRegistry
    {
        private List<Vehicle> vehicles;
        private int nextId = 1;

        public AutoRegistry(List<Vehicle> vehicles)
        {
            if(vehicles == null)
            {
                throw new ArgumentNullException(nameof(vehicles), "Input cannot be null");
            }
            this.vehicles = vehicles;

        }

        public AutoRegistry()
        {
            vehicles = new List<Vehicle>();
        }

        public void AddVehicle(Vehicle vehicle)
        {
            if (vehicle == null)
            {
                throw new ArgumentNullException(nameof(vehicle), "Input cannot be null");
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

            PropertyInfo prop = typeof(Vehicle).GetProperty(propertyName);
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

            PropertyInfo prop = typeof(Vehicle).GetProperty(propertyName);
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
                                throw new InvalidOperationException($"Operator '{op}' is not valid for numeric properties.");

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

    }
}
