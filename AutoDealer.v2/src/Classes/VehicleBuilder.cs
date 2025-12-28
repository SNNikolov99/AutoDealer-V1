using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutoDealerV2.src.Classes
{
    public class VehicleBuilder 
    {
        private Vehicle vehicle;
        private Pricelist pricelist;

        public void Reset()
        {
            vehicle = new Vehicle();
        }

        public VehicleBuilder setModel(string model)
        {
            if (!pricelist.models.ContainsKey(model))
            {
                throw new ArgumentException("Model not found in pricelist.");
            }
            vehicle.Brand = pricelist.models[model].brand.ToLower();
            vehicle.Model = model.ToLower();
            vehicle.Type = pricelist.models[model].type.ToLower();
            vehicle.Price = pricelist.models[model].basePrice;
            vehicle.Colour = pricelist.models[model].defaultColour.ToLower();
            vehicle.HorsePower = pricelist.engines[pricelist.models[model].defaultEngine].horsepower;
            vehicle.FuelType = pricelist.engines[pricelist.models[model].defaultEngine].fuelType.ToLower();
            vehicle.Description = $"{vehicle.Brand} {vehicle.Model} with standard features";

            return this;
        }

        public VehicleBuilder setColor(string colour)
        {
            if (!pricelist.colours.ContainsKey(colour))
            {
                throw new ArgumentException("Color not found in pricelist.");
            }
           
            vehicle.Colour = colour;
            vehicle.Price += pricelist.colours[colour];
            if(vehicle.Description.Contains("with standard features"))
            {
                vehicle.Description = vehicle.Description.Replace("with standard features", "");
            }
            vehicle.Description += $" in {colour} color";
            return this;
        }

        public VehicleBuilder setEngine(string engine)
        {
            if (!pricelist.engines.ContainsKey(engine))
            {
                throw new ArgumentException("Engine not found in pricelist.");
            }

            vehicle.HorsePower = pricelist.engines[engine].horsepower;
            vehicle.FuelType = pricelist.engines[engine].fuelType.ToLower();
            vehicle.Price += pricelist.engines[engine].price;

            if (vehicle.Description.Contains("with standard features"))
            {
                vehicle.Description = vehicle.Description.Replace("with standard features", "");
            }
            vehicle.Description += $" equipped with {vehicle.FuelType} {engine} engine generating {vehicle.HorsePower} hp";
            return this;
        }

        public VehicleBuilder setPackage(string package)
        {
            if (!pricelist.packages.ContainsKey(package))
            {
                throw new ArgumentException("Package not found in pricelist.");
            }
            vehicle.Price += pricelist.packages[package].price;

            if (vehicle.Description.Contains("with standard features"))
            {
                vehicle.Description = vehicle.Description.Replace("with standard features", "");
            }
            vehicle.Description += $" improved with " + pricelist.packages[package].description;

            return this;

        }

        public Vehicle Build()
        {
            Vehicle result = this.vehicle;
            this.Reset();
            return result;
        }

        public VehicleBuilder()
        {
            this.Reset();
            pricelist = new Pricelist();
        }

        public VehicleBuilder(Pricelist pricelist)
        {
            this.Reset();
            this.pricelist = pricelist;
        }

    }
}
