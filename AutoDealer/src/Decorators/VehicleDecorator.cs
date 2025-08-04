using AutoDealer.Models;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net.Mail;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace AutoDealer.src.Decorators
{
    public abstract class VehicleDecorator : Vehicle
    {
        protected Vehicle _vehicle;

        public VehicleDecorator(Vehicle vehicle) : 
            base(vehicle.Brand,  vehicle.Model, vehicle.Year, vehicle.Price,vehicle.Color, vehicle.HorsePower, vehicle.FuelType)
        {
            _vehicle = vehicle;
        }

        public abstract void AttachPart();
    }
}
