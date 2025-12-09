using AutoDealerV2.src.Classes;
using AutoDealerV2.src.Decorators;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutoDealerV2.src.Decorators.MotorbikeDecorators
{
    public abstract class MotobikeDecorator : VehicleDecorator
    {
        protected Motorbike Motorbike => (Motorbike)_vehicle;
        public MotobikeDecorator(Motorbike motorbike) : base(motorbike) { }


        public abstract override void AttachPart();
    }
}
