using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutoDealer.Models
{
     public abstract class Vehicle
    {

        public int Id { get; set; }
        public string Brand { get; set; }
        public string Model { get; set; }
        public int Year { get; set; }
        public decimal Price { get; set; }
        public string Color { get; set; }
        public int HorsePower { get; set; } // optional, for performance vehicles
        public string FuelType { get; set; } // petrol,diesel, electric, hybrid

        // Constructor
        protected Vehicle(string brand, string model, int year, decimal price, string color,int horsepower, string fuelType)
        {
            Id = 0;
            Brand = brand;
            Model = model;
            Year = year;
            Price = price;
            Color = color;
            HorsePower = horsepower;
            FuelType = fuelType;
           
        }
      
        public abstract string GetVehicleType();
    }
}
