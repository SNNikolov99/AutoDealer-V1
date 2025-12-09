using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoDealerV2.src.Classes;

namespace AutoDealerV2.src.Decorators
{
    public class DesignerColorDecocrator : VehicleDecorator
    {
        
        public DesignerColorDecocrator(Vehicle vehicle) : base(vehicle) { }
      
        public override void AttachPart()
        {
            _vehicle.Color = "Venom green with white stripes and lime kant";
        }

        public override string GetVehicleType()
        {
            throw new NotImplementedException();
        }
    }
}
