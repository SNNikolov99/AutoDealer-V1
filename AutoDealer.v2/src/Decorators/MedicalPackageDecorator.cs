using AutoDealerV2.src.Classes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutoDealerV2.src.Decorators
{
    public class MedicalPackageDecorator:VehicleDecorator
    {
        public MedicalPackageDecorator(Vehicle vehicle): base(vehicle) { }

        public override void AttachPackage()
        {
            if(_vehicle.Description.Contains("police package") || _vehicle.Description.Contains("utility package"))
            {
                throw new InvalidOperationException("Vehicle already has another special package attached.");
            }

            if (_vehicle.Description.Contains("medical package"))
            {
                throw new InvalidOperationException("Vehicle already has a medical package attached.");
            }


            if (_vehicle.Type.ToLower() != "minibus")
            {
                throw new InvalidOperationException("Medical package is only available for minibuses.");
            }

            _vehicle.Price += 25500;
            _vehicle.HorsePower += 30;
            _vehicle.Colour = "white with red stripes";
            _vehicle.Description += " The vehicle contains a medical package conversion. It contains needed equipment for sustaining a person alive ";
        }
    }
}
