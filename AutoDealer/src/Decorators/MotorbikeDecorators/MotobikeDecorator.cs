using AutoDealer.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutoDealer.src.Decorators
{
    public abstract class MotobikeDecorator : VehicleDecorator
    {
        protected Motorbike Motorbike => (Motorbike)_vehicle;
        public MotobikeDecorator(Motorbike motorbike) : base(motorbike) { }


        public abstract override void AttachPart();
    }
}
