using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutoDealerV2.src.Classes
{
     public class Vehicle
    {

        public int Id { get; set; }
        public string Brand { get; set; }
        public string Model { get; set; }
        public string Type { get; set; }
        public int Year { get; set; }
        public decimal Price { get; set; }
        public string Colour { get; set; }
        public int HorsePower { get; set; }
        public string FuelType { get; set; } 

        public string Description { get; set; }

        // Constructor
        public Vehicle()
        {
            Id = 0;
            Brand = "unknown";
            Model = "unknown";
            Type = "unknown";
            Year = 0;
            Price = 0;
            Colour = "unknown";
            HorsePower = 0;
            FuelType = "unknown";
            Description = "unknown";
        }

        public Vehicle(int ID,string brand, string model,string type, int year, decimal price, string colour,int horsepower, string fuelType,string description)
        {
            //toLower method used for string standartisation
            Id = ID;
            Brand = brand.ToLower();
            Model = model.ToLower();
            Type = type.ToLower();
            Year = year;
            Price = price;
            Colour = colour.ToLower();
            HorsePower = horsepower;
            FuelType = fuelType.ToLower();
            Description = description.ToLower();
           
        }


        public Vehicle(string brand, string model,string type, int year, decimal price, string Colour, int horsepower, string fuelType,string description)
        {
            //toLower method used for string standartisation
            Id = 0;
            Brand = brand.ToLower();
            Model = model.ToLower();
            Type = type.ToLower();
            Year = year;
            Price = price;
            Colour = Colour.ToLower();
            HorsePower = horsepower;
            FuelType = fuelType.ToLower();
            Description = description.ToLower();

        }

        public override string ToString()
        {
            return  $" {Id}, {Type} , {Brand} , {Model} ," +
                    $" {Year} , {Price} , {Colour}," +
                    $" {HorsePower}, {FuelType} , {Description}";
        }
    }
}
