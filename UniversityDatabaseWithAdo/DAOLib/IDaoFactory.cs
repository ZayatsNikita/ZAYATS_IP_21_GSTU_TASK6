namespace DAOLib
{
    public interface IDaoFactory<T>
    {
        public IDao<T> CreateDao(string paramsOfCreating);
    }
}
