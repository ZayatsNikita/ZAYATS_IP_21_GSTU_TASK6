namespace DAOLib
{
    /// <summary>
    /// The interface that is used to describe the methods of Dao objects.
    /// </summary>
    public interface IDaoFactory<T>
    {   /// <summary>
        /// A string describing the parameters of the created dao object. 
        /// </summary>
        /// <param name="paramsOfCreating">String with specif</param>
        /// <returns>Dao</returns>
        public IDao<T> CreateDao(string paramsOfCreating);
    }
}
