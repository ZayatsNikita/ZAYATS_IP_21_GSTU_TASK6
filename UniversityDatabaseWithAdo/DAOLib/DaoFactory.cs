using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DAOLib.SqlDao;
using System.Threading.Tasks;

namespace DAOLib
{
    public class DaoFactory<G> where G: new()
    {
        IDaoFactory<G>[] daoFactorys = new IDaoFactory<G>[] {new TransactSqlDaoFactory<G>() };
        public IDao<G> CreateDao(string daoTipeName,string paramsOfCreating)
        {
           return daoFactorys[0].CreateDao(paramsOfCreating);
        }
    }
}
