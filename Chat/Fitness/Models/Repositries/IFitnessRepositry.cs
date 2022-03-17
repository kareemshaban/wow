using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Fitness.Models.Repositries
{
    public interface IFitnessRepositry<Tentity>
    {
        List<Tentity> list(string variable = "");
        IEnumerable<Tentity> IList(string variable="",string name="");
        Tentity firstOrdefault(int id,string name="",string name2="");
        Task<bool> Add(Tentity entity);
        Task<bool> Delete(int id, Tentity entity, List<Tentity> entites, string name="", string name2 = "");
        Task<bool> update(Tentity entity);
    }
}
