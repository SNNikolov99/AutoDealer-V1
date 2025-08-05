﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoDealer.Models;

namespace AutoDealer.src.Decorators.MotorbikeDecorators
{
    public class _750ccEngine: MotobikeDecorator
    {
        public _750ccEngine(Motorbike bike) : base(bike) { }

        public override void AttachPart()
        {
            _vehicle.Price += 3500;
            _vehicle.HorsePower += 35;
            _vehicle.Model += " SuperMoto";
        }
    }
}
