using AutoDealerV2.src.Classes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutoDealerV2.src.Decorators
{
    public class PolicePackageDecorator : VehicleDecorator
    {
        public PolicePackageDecorator(Vehicle vehicle) : base(vehicle) { }

        public override void AttachPackage()
        {

            if (_vehicle.Description.Contains("medical package") || _vehicle.Description.Contains("utility package"))
            {
                throw new InvalidOperationException("Vehicle already has another special package attached.");
            }

            if (_vehicle.Description.Contains("police package"))
            {
                throw new InvalidOperationException("Vehicle already has a police package attached.");
            }

            if (_vehicle.Type.ToLower() == "minibus")
            {
                _vehicle.Price += 10000;
                _vehicle.Horsepower += 50;
                _vehicle.Colour = "blue green and white";
                _vehicle.Description += " The vehicle contains a police package conversion like cages and additional armor plates ";
            }
            if (_vehicle.Type.ToLower() == "car")
            {
                _vehicle.Price += 8000;
                _vehicle.Horsepower += 40;
                _vehicle.Colour = "blue green and white";
                _vehicle.Description += " The vehicle contains a police package conversion like reinforced bumpers sirens radio station and camera ";
            }
            if (_vehicle.Type.ToLower() == "motorbike")
            {
                _vehicle.Price += 4000;
                _vehicle.Horsepower += 15;
                _vehicle.Colour = "blue green and white";
                _vehicle.Description += " The vehicle contains a police package conversion like sirens and internal radio station and camera ";
            }


        }
    }
}
