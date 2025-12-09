using AutoDealer.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json;
using System.Threading.Tasks;

namespace AutoDealer.src.Services
{
    public class JSONSerialiserService: ISerializer
    {
       public List<Vehicle> Load(string pathName) 
        {
            return new List<Vehicle>(); 
        }
       public void Save(List<Vehicle> vehicles, string pathName)
       {
            var json = jsonSerializer.Serialize(vehicles);
       }
    }
}
