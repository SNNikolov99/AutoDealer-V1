using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutoDealerV2.src.Classes
{
    public class ModelSpec
    {
        //string name;
        string brand;
        string type;
        decimal basePrice;
        string defaultEngine;
        string defaultColor;
        string defaultPackage;
        List<string> avaliableEngines;
        List<string> avaliableColors;
        List<string> avaliablePackages;

        public ModelSpec() {
            brand = "";
            type = "";
            basePrice = 00;
            defaultEngine = "";
            defaultColor = "";
            defaultPackage = "";
            avaliableEngines = new List<string>();
            avaliableColors = new List<string>();
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
            this.defaultColor = defaultColor;
            this.defaultPackage = defaultPackage;
            this.avaliableColors = avaliableColors;
            this.avaliablePackages = avaliablePackages;
            this.avaliableEngines = avaliableEngines;
        }


    }

    public class EngineSpec
    {
        //string name;
        decimal price;
        int horsepower;
        string fuelType;

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
        decimal price;
        string description;
        bool extraSpace;
        bool reinforcedFloor;
        bool LuggageRacks;
        bool PerformanceMode;

        public PackageSpec()
        {
            this.price = decimal.Zero;
            this.description = string.Empty;
            this.extraSpace = false;
            this.reinforcedFloor = false;
            this.LuggageRacks = false;
            this.PerformanceMode = false;
        }
    }

    public class Pricelist
    {
        Dictionary<string, ModelSpec> models;
        Dictionary<string, PackageSpec> packages;
        Dictionary<string, EngineSpec> engines;
        Dictionary<string, decimal> colours;

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
            models = models;
            packages = packages;
            engines = engines;
            colours = colours;
        }

    }
}
