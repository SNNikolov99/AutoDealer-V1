using AutoDealerV2.src.Classes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json;
using System.Threading.Tasks;

namespace AutoDealerV2.src.Services.Serialisation
{
    public class JSONSerialiserService: ISerializer
    {
       public List<Vehicle> Load(string pathName) 
        {
            List<Vehicle>? list = new List<Vehicle>();
            string jsonString = File.ReadAllText(pathName);
            list = JsonSerializer.Deserialize<List<Vehicle>>(jsonString);
            return list;
        }
       public void Save(List<Vehicle> vehicles, string pathName)
       {
            string JsonString = JsonSerializer.Serialize(vehicles,
                new JsonSerializerOptions {WriteIndented = true });
            File.WriteAllText(pathName, JsonString);

       }
    }
}
