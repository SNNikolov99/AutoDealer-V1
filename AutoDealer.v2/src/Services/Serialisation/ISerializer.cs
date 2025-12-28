using AutoDealerV2.src.Classes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutoDealerV2.src.Services.Serialisation
{
    public interface ISerializer<T>
    {
        T Load(string pathName);
        void Save(T vehicles, string pathName);

    }
}
