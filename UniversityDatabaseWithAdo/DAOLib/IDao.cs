using System.Collections.Generic;

namespace DAOLib
{
    public interface IDao<T>
    {
        public object Create(T element);
        public List<T> ReadAll();
        public void Update(T oldElement, T newElement);
        public void Delete(T element);
        public void CloseConnect();
    }
}
