using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutoDealerV2.src.Classes
{
    public class MiniBus : Vehicle
    {
        public MiniBus(int Id,string make, string model, int year, decimal price, string color, int horsepower, string fuelType) :
            base(Id, make, model, year, price, color, horsepower, fuelType)
        {
        }

        public MiniBus( string make, string model, int year, decimal price, string color, int horsepower, string fuelType) :
          base(make, model, year, price, color, horsepower, fuelType)
        {
        }

        public override string GetVehicleType()
        {
            return "MiniBus";
        }

        public override string ToString()
        {
            return $" {Id.ToString()}, minibus , {Brand.ToString()} , {Model.ToString()} ," +
                    $" {Year.ToString()} , {Price.ToString()} , {Color.ToString()}," +
                    $" {HorsePower.ToString()}, {FuelType.ToString()}";
        }

    }
}
