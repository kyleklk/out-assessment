using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ContactAnalyser.Contracts
{
    public interface ISortService<Tin, Tout> 
        where Tin: class
        where Tout: class
    {
        IEnumerable<Tout> Sort(IEnumerable<Tin> items); 
    }
}
