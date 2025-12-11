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
           
        }
    }
}
