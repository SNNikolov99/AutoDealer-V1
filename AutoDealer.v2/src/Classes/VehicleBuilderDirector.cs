using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutoDealerV2.src.Classes
{
    public class VehicleBuilderDirector
    {
        private VehicleBuilder builder;
        public VehicleBuilderDirector(VehicleBuilder builder)
        {
            this.builder = builder;
        }
      
        public Vehicle constructBaseVehicle(string model)
        {
            builder.Reset();
            builder.setModel(model);
            return builder.Build();
        }
    }
}
