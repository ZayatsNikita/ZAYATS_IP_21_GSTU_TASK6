using System.Reflection;
using System.Text;


namespace DAOLib.SqlDao
{
    internal static class GetComandForTransactSql
    {
        private static StringBuilder builderForName = new StringBuilder();
        private static StringBuilder builderForParams = new StringBuilder();

        internal static string GetDeleteItemComand(string tableName,PropertyInfo[] propertys)
        {
            string result;
            
            builderForParams.Append($"{propertys[0].Name} = @{propertys[0].Name}");

            for (int i = 1; i < propertys.Length; i++)
            {
                builderForParams.Append($" and {propertys[i].Name} = @{propertys[i].Name}");
            }

            result = $"Delete {tableName} where {builderForParams}";

            builderForParams.Clear();

            return result;
        }

        internal static string GetCreateItemComand(string tableName, PropertyInfo[] propertys)
        {

            if (propertys[0].Name.ToUpper() != "ID")
            {
                builderForName.Append(propertys[0].Name);
                builderForParams.Append("@" + propertys[0].Name);
            }
            for (int i = 1; i < propertys.Length; i++)
            {
                if (propertys[i].Name.ToUpper() != "ID")
                {
                    builderForName.Append(", " + propertys[i].Name);
                    builderForParams.Append(", @" + propertys[i].Name);
                }
            }

            string result = $"insert into {tableName} ({builderForName.ToString().TrimStart(',')}) values ({builderForParams.ToString().TrimStart(',')}) select SCOPE_IDENTITY()";

            builderForName.Clear();
            builderForParams.Clear();

            return result;
        }
        
        internal static string GetUpdateItemComand(string tableName, PropertyInfo[] propertys)
        {


            if (propertys[0].Name.ToUpper() != "ID")
            {
                builderForParams.Append($"{propertys[0].Name} =  @{propertys[0].Name}New");
            }
            builderForName.Append($"{propertys[0].Name} =  @{propertys[0].Name}");

            for (int i = 1; i < propertys.Length; i++)
            {
                if (propertys[i].Name.ToUpper() != "ID")
                {
                    builderForParams.Append($", {propertys[i].Name} =  @{propertys[i].Name}New");
                }
                builderForName.Append($" and {propertys[i].Name} =  @{propertys[i].Name}");
            }
            

            string result = $"update {tableName} set {builderForParams.ToString().TrimStart(',')} where {builderForName}";

            builderForName.Clear();
            builderForParams.Clear();

            return result;
        }

        
        internal static string GetReadAllComand(string tableName, PropertyInfo[] propertys)
        {

            builderForParams.Append(propertys[0].Name);
            for (int i = 1; i < propertys.Length; i++)
            {
                builderForParams.Append("," + propertys[i].Name);
            }

            string result = $"select {builderForParams} from {tableName}";

            builderForName.Clear();
            builderForParams.Clear();

            return result;
        }
    }
}
