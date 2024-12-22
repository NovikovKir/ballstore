using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Store
{
    public interface IBallRepository
    {
        Ball[] GetAllByBrandOrModel(string brandOrModel);
        Ball[] GetAllByIds(IEnumerable<int> ballIds);
        Ball GetById(int id);
    }
}
