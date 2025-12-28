using AutoDealerV2.src.Classes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json;
using System.Threading.Tasks;

namespace AutoDealerV2.src.Services.Serialisation
{
    public class JSONSerialiserService<T>: ISerializer<T>
    {
       public T Load(string pathName) 
        {
            string jsonString = File.ReadAllText(pathName);
            var options = new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true
            };

            if (string.IsNullOrWhiteSpace(jsonString))
                throw new InvalidOperationException("The given file is empty");

            var result = JsonSerializer.Deserialize<T>(jsonString,options);

            return result ?? throw new InvalidOperationException("Deserialization returned null");

        }
       public void Save(T vehicles, string pathName)
       {
            string JsonString = JsonSerializer.Serialize(vehicles,
                new JsonSerializerOptions {WriteIndented = true });
            File.WriteAllText(pathName, JsonString);

       }
    }
}
