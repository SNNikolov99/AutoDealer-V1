using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace AutoDealerV2.src.Classes
{
    public class ModelSpec
    {
        //string name;
        [JsonPropertyName("Brand")]
        public string brand { get; set; } 

        [JsonPropertyName("Type")]
        public string type { get; set; } 

        [JsonPropertyName("BasePrice")]
        public decimal basePrice { get; set; }

        [JsonPropertyName("DefaultEngine")]
        public string defaultEngine { get; set; } 

        [JsonPropertyName("DefaultColour")] 
        public string defaultColour { get; set; } 

        [JsonPropertyName("DefaultPackage")]
        public string defaultPackage { get; set; } 

        [JsonPropertyName("AvailableEngines")]
        public List<string> avaliableEngines { get; set; } 

        [JsonPropertyName("AvailableColours")] 
        public List<string> avaliableColours { get; set; } 

        [JsonPropertyName("AvailablePackages")]
        public List<string> avaliablePackages { get; set; } 

        public ModelSpec() {
            brand = "";
            type = "";
            basePrice = 00;
            defaultEngine = "";
            defaultColour = "";
            defaultPackage = "";
            avaliableEngines = new List<string>();
            avaliableColours = new List<string>();
            avaliablePackages = new List<string>();
        }

        public ModelSpec(string brand, 
                         string type,
                         decimal basePrice,
                         string defaultEngine,
                         string defaultColor,
                         string defaultPackage,
                         List<string> avaliableEngines,
                         List<string> avaliableColors,
                         List<string> avaliablePackages)
        {

            this.brand = brand;
            this.type = type;
            this.basePrice = basePrice;
            this.defaultEngine = defaultEngine;
            this.defaultColour = defaultColor;
            this.defaultPackage = defaultPackage;
            this.avaliableColours = avaliableColors;
            this.avaliablePackages = avaliablePackages;
            this.avaliableEngines = avaliableEngines;
        }


    }

    public class EngineSpec
    {
        //string name;
        [JsonPropertyName("Price")]
        public decimal price { get; set; }
        [JsonPropertyName("Horsepower")]
        public int horsepower { get; set; }
        [JsonPropertyName("FuelType")]
        public string fuelType { get; set; }

        public EngineSpec()
        {
            price = decimal.Zero;
            horsepower = 0;
            fuelType = string.Empty;
        }

        public EngineSpec(decimal price, 
                          int horsepower, 
                          string fuelType)
        {
            this.price = price;
            this.horsepower = horsepower;
            this.fuelType = fuelType;
        }
    }

    public class PackageSpec
    {
        //string name;
        [JsonPropertyName("Price")]
        public decimal price { get; set; }
        [JsonPropertyName("Description")]
        public string description { get; set; }
        [JsonPropertyName("HorsepowerBoost")]
        public int HorsepowerBoost { get; set; }
        [JsonPropertyName("FuelTypeOverride")]
        public string FuelTypeOverride { get; set; }

        public PackageSpec()
        {
            this.price = decimal.Zero;
            this.description = string.Empty;
            this.FuelTypeOverride = string.Empty;
            this.HorsepowerBoost = 0;
        }
    }

    public class Pricelist
    {
       public Dictionary<string, ModelSpec> models { get; set; }
       public Dictionary<string, PackageSpec> packages { get; set; }
       public Dictionary<string, EngineSpec> engines { get; set; }
       public Dictionary<string, decimal> colours { get; set; }

        public Pricelist()
        {
            models = new Dictionary<string, ModelSpec>();
            packages = new Dictionary<string, PackageSpec>();
            engines = new Dictionary<string, EngineSpec>();
            colours = new Dictionary<string, decimal>();
        }
        public Pricelist(Dictionary<string, ModelSpec> models,
                         Dictionary<string, EngineSpec> engines,
                         Dictionary<string, PackageSpec> packages,
                         Dictionary<string, decimal> colours)
        {
            this.models = models;
            this.packages = packages;
            this.engines = engines;
            this.colours = colours;
        }

    }
}
