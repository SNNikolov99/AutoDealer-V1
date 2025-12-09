using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutoDealerV2.src.Classes
{
    public class MiniBus : Vehicle
    {
        public MiniBus(string make, string model, int year, decimal price, string color, int horsepower, string fuelType) :
            base(make, model, year, price, color, horsepower, fuelType)
        {
        }
        public override string GetVehicleType()
        {
            return "MiniBus";
        }
    }
}
