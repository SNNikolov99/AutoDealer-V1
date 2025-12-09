using AutoDealer.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutoDealer.src.Services
{
    public interface ISerializer
    {
        List<Vehicle> Load(string pathName);
        void Save(List<Vehicle> vehicles, string pathName);

    }
}
