using DAOLib.SqlDao;
using System;

namespace DAOLib
{
    /// <summary>
    /// Abstract Factory for Dao objects.
    /// </summary>
    public class DaoFactory<G> where G: new()
    {
        IDaoFactory<G>[] daoFactorys = new IDaoFactory<G>[] {new TransactSqlDaoFactory<G>() };
        /// <summary>
        /// A method which create dao objects. 
        /// </summary>
        /// <param name="daoTipeName">Name of necessary dao class.</param>
        /// <param name="paramsOfCreating">Parameters for creating a dao.</param>
        /// <returns>Class wich realyse IDao interface.</returns>
        /// <exception cref="ArgumentNullException">Thrown if daoTipeName is equals to null</exception>
        /// <exception cref="ArgumentException">Thrown if there are no dao with specified name.</exception>
        public IDao<G> CreateDao(string daoTipeName,string paramsOfCreating)
        {
            if(daoTipeName==null)
            {
                throw new ArgumentNullException();
            }
            switch(daoTipeName)
            {
                case "TransatSqlDao":
                    return daoFactorys[0].CreateDao(paramsOfCreating);
            }
            throw new ArgumentException();
        }
    }
}
