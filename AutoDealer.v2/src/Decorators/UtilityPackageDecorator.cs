using AutoDealerV2.src.Classes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutoDealerV2.src.Decorators
{
    public class UtilityPackageDecorator : VehicleDecorator
    {
        

        public UtilityPackageDecorator(Vehicle vehicle) :
          base(vehicle)
        {
            
        }

        public override void AttachPackage()
        {

            if (_vehicle.Description.Contains("police package") || _vehicle.Description.Contains("medical package"))
            {
                throw new InvalidOperationException("Vehicle already has another special package attached.");
            }

            if (_vehicle.Description.Contains("utility package"))
            {
                throw new InvalidOperationException("Vehicle already has a utility package attached.");
            }

            if (_vehicle.Type.ToLower() == "minibus")
            {
                _vehicle.Price += 4000;
                _vehicle.Description += " The vehicle contains a utility package conversion. Now it can serve as a mobile workshop " +
                                        "or a post service due to added internal rails ";
            }
            else if (_vehicle.Type.ToLower() == "car")
            {
                _vehicle.Price += 2500;
                _vehicle.Description += " The vehicle contains a utility package conversion. Now it can serve as a mobile workshop " +
                                        "or a post service due to added internal rails. Rear seats are removed ";
            }
            else
            {
                throw new InvalidOperationException("Utility package is not available for this type of vehicle.");
            }
        }
    }
}
