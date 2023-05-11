using Microsoft.Practices.EnterpriseLibrary.Data.Sql;
using System.Data.Common;
using System.Data;

namespace Food_Ordering.DAL
{
    public class ProductsDAL : ConnectionDAL
    {
        public DataTable PR_Products_SelectAll()
        {
            try
            {
                SqlDatabase sqlDB = new SqlDatabase(SQL_Connection);
                DbCommand dbCMD = sqlDB.GetStoredProcCommand("PR_Products_SelectAll");

                DataTable dt = new DataTable();
                using (IDataReader dr = sqlDB.ExecuteReader(dbCMD))
                {
                    dt.Load(dr);
                }
                return dt;

            }
            catch (Exception ex)
            {
                return null;
            }

        }

        public bool? PR_Products_Delete(int? ProductId)
        {
            try
            {
                SqlDatabase sqlDB = new SqlDatabase(SQL_Connection);
                DbCommand dbCMD = sqlDB.GetStoredProcCommand("PR_Products_DeleteByPK");
                sqlDB.AddInParameter(dbCMD, "ProductId", SqlDbType.Int, ProductId);
                int vReturnValue = sqlDB.ExecuteNonQuery(dbCMD);
                return (vReturnValue == -1 ? false : true);
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public DataTable PR_Products_SelectByPK(int? ProductId)
        {
            try
            {
                SqlDatabase sqlDB = new SqlDatabase(SQL_Connection);
                DbCommand dbCMD = sqlDB.GetStoredProcCommand("PR_Products_SelectByPK");
                sqlDB.AddInParameter(dbCMD, "ProductId", SqlDbType.Int, ProductId);

                DataTable dt = new DataTable();
                using (IDataReader dr = sqlDB.ExecuteReader(dbCMD))
                {
                    dt.Load(dr);
                }

                return dt;
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public bool? PR_Products_Save(int? ProductId, string? ProductName, string? Description, decimal? Price, int? Quantity, string? ImageUrl, int? CategoryId)
        {
            SqlDatabase sqlDB = new SqlDatabase(SQL_Connection);
            DbCommand dbCMD;

            try
            {
                if (ProductId == null)
                {
                    dbCMD = sqlDB.GetStoredProcCommand("PR_Products_Insert");
                }
                else
                {
                    dbCMD = sqlDB.GetStoredProcCommand("PR_Products_UpdateByPK");
                    sqlDB.AddInParameter(dbCMD, "@ProductId", SqlDbType.Int, ProductId);
                }
                sqlDB.AddInParameter(dbCMD, "@ProductName", SqlDbType.NVarChar, ProductName);
                sqlDB.AddInParameter(dbCMD, "@Description", SqlDbType.NVarChar, Description);
                sqlDB.AddInParameter(dbCMD, "@Price", SqlDbType.Decimal, Price);
                sqlDB.AddInParameter(dbCMD, "@Quantity", SqlDbType.Int, Quantity);
                sqlDB.AddInParameter(dbCMD, "@ImageUrl", SqlDbType.NVarChar, ImageUrl);
                sqlDB.AddInParameter(dbCMD, "@CategoryId", SqlDbType.Int, CategoryId);
                sqlDB.AddInParameter(dbCMD, "@CreatedDate", SqlDbType.Date, DBNull.Value);


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
