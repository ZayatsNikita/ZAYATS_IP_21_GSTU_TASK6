using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAOLib
{
    public interface IDao<T>
    {
        public void Create(T element);
        public List<T> ReadAll();
        public void Update(T oldElement, T newElement);
        public void Delete(T element);
    }
}
