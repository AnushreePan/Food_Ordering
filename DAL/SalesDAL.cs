using Microsoft.Practices.EnterpriseLibrary.Data.Sql;
using System.Data.Common;
using System.Data;

namespace Food_Ordering.DAL
{
    public class SalesDAL : ConnectionDAL
    {

        public bool? PR_Sales_Delete(int? SalesId)
        {
            try
            {
                SqlDatabase sqlDB = new SqlDatabase(SQL_Connection);
                DbCommand dbCMD = sqlDB.GetStoredProcCommand("PR_Sales_DeleteByPK");
                sqlDB.AddInParameter(dbCMD, "SalesId", SqlDbType.Int, SalesId);
                int vReturnValue = sqlDB.ExecuteNonQuery(dbCMD);
                return (vReturnValue == -1 ? false : true);
            }
            catch (Exception ex)
            {
                return null;
            }
        }
        public bool? PR_Sales_Save(int? SalesId, int? ProductId , int? Discount)
        {
            SqlDatabase sqlDB = new SqlDatabase(SQL_Connection);
            DbCommand dbCMD;

            try
            {
                dbCMD = sqlDB.GetStoredProcCommand("PR_Sales_Insert");
                sqlDB.AddInParameter(dbCMD, "@SalesId", SqlDbType.Int, SalesId);
               
                sqlDB.AddInParameter(dbCMD, "@ProductId", SqlDbType.Int, ProductId);
                sqlDB.AddInParameter(dbCMD, "@Discount", SqlDbType.Int, Discount);
                int vReturnValue = sqlDB.ExecuteNonQuery(dbCMD);
                return (vReturnValue == -1 ? false : true);
            }
            catch (Exception ex)
            {
                return null;
            }
        }
    }
}
