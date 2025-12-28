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
       
        public decimal Price { get; set; }
        public string Colour { get; set; }
        public int Horsepower { get; set; }
        public string Fueltype { get; set; } 

        public string Description { get; set; }

        // Constructor
        public Vehicle()
        {
            Id = 0;
            Brand = "unknown";
            Model = "unknown";
            Type = "unknown";    
            Price = 0;
            Colour = "unknown";
            Horsepower = 0;
            Fueltype = "unknown";
            Description = "unknown";
        }

        public Vehicle(int ID,string brand, string model,string type,  decimal price, string colour,int horsepower, string fueltype,string description)
        {
            //toLower method used for string standartisation
            Id = ID;
            Brand = brand.ToLower();
            Model = model.ToLower();
            Type = type.ToLower();
            Price = price;
            Colour = colour.ToLower();
            Horsepower = horsepower;
            Fueltype = fueltype.ToLower();
            Description = description.ToLower();
           
        }


        public Vehicle(string brand, string model,string type,  decimal price, string colour, int horsepower, string fueltype,string description)
        {
            //toLower method used for string standartisation
            Id = 0;
            Brand = brand.ToLower();
            Model = model.ToLower();
            Type = type.ToLower();
            Price = price;
            Colour = colour.ToLower();
            Horsepower = horsepower;
            Fueltype = fueltype.ToLower();
            Description = description.ToLower();

        }

        public override string ToString()
        {
            return  $" {Id}, {Type} , {Brand} , {Model} ," +
                    $" {Price} , {Colour}," +
                    $" {Horsepower}, {Fueltype} , {Description}";
        }
    }
}
