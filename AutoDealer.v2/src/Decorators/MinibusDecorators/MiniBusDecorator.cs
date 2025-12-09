using AutoDealerV2.src.Classes;
using AutoDealerV2.src.Decorators;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutoDealerV2.src.Decorators.MinibusDecorators
{
    public abstract class MiniBusDecorator : VehicleDecorator
    {
        protected MiniBus MiniBus => (MiniBus)_vehicle;
        protected MiniBusDecorator(MiniBus miniBus) : base(miniBus) { }

        public abstract override void AttachPart();
    }
}
