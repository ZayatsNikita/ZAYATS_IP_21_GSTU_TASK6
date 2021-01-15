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
        public void Update(T element);
        public void Delete(T element);
        public T Read(T element);

        public List<T> ReadAll();
    }
}
