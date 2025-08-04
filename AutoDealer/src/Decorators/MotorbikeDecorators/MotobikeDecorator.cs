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
        public MotobikeDecorator(Motorbike motorbike) : base(motorbike) { }


        public abstract override void AttachPart();
    }
}
