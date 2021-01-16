using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAOLib
{
    public interface IDaoFactory<T>
    {
        public IDao<T> CreateDao(string paramsOfCreating);
    }
}
