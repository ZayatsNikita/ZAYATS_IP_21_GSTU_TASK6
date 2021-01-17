using DAOLib.SqlDao;
using System;

namespace DAOLib
{
    public class DaoFactory<G> where G: new()
    {
        IDaoFactory<G>[] daoFactorys = new IDaoFactory<G>[] {new TransactSqlDaoFactory<G>() };
        public IDao<G> CreateDao(string daoTipeName,string paramsOfCreating)
        {
            switch(daoTipeName)
            {
                case "TransatSqlDao":
                    return daoFactorys[0].CreateDao(paramsOfCreating);
            }
            throw new ArgumentException();
        }
    }
}
