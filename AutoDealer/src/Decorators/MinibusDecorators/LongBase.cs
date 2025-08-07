﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoDealer.Models;

namespace AutoDealer.src.Decorators.MinibusDecorators
{
    public class LongBase : MiniBusDecorator
    {

        public LongBase(MiniBus miniBus) : base(miniBus) { }

        public override void AttachPart()
        {
            _vehicle.Price += 7000;
            _vehicle.Model += " Long Base";
        }
    }
}
