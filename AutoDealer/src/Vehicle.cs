﻿using System;
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
            //toLower method used for string standartisation
            Id = 0;
            Brand = brand.ToLower();
            Model = model.ToLower();
            Year = year;
            Price = price;
            Color = color.ToLower();
            HorsePower = horsepower;
            FuelType = fuelType.ToLower();
           
        }
      
        public abstract string GetVehicleType();
        public override string ToString()
        {
            return  $" {Brand.ToString()} , {Model.ToString()} ," +
                    $" {Year.ToString()} , {Price.ToString()} , {Color.ToString()}," +
                    $" {HorsePower.ToString()}, {FuelType.ToString()}";
        }
    }
}
