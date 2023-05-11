using Microsoft.Practices.EnterpriseLibrary.Data.Sql;
using System.Data.Common;
using System.Data.SqlClient;
using System.Data;

namespace Food_Ordering.DAL
{
    public class CategoryDAL:ConnectionDAL
    {
        public DataTable PR_Category_SelectAll()
        {
            try
            {
                SqlDatabase sqlDB = new SqlDatabase(SQL_Connection);
                DbCommand dbCMD = sqlDB.GetStoredProcCommand("PR_Category_SelectAll");

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

        public bool? PR_Category_Delete(int? CategoryId)
        {
            try
            {
                SqlDatabase sqlDB = new SqlDatabase(SQL_Connection);
                DbCommand dbCMD = sqlDB.GetStoredProcCommand("PR_Category_DeleteByPK");
                sqlDB.AddInParameter(dbCMD, "CategoryId", SqlDbType.Int, CategoryId);
                int vReturnValue = sqlDB.ExecuteNonQuery(dbCMD);
                return (vReturnValue == -1 ? false : true);
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public DataTable PR_Category_SelectByPK(int? CategoryId)
        {
            try
            {
                SqlDatabase sqlDB = new SqlDatabase(SQL_Connection);
                DbCommand dbCMD = sqlDB.GetStoredProcCommand("PR_Category_SelectByPK");
                sqlDB.AddInParameter(dbCMD, "CategoryId", SqlDbType.Int, CategoryId);

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

        public DataTable PR_Category_Dropdown()
        {
            try
            {
                SqlDatabase sqlDB = new SqlDatabase(SQL_Connection);
                DbCommand dbCMD = sqlDB.GetStoredProcCommand("PR_Category_SelectForDropDown");

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

        public bool? PR_Category_Save(int? CategoryId, string? CategoryName, string? ImageUrl)
        {
            SqlDatabase sqlDB = new SqlDatabase(SQL_Connection);
            DbCommand dbCMD;

            try
            {
                if (CategoryId == null)
                {
                    dbCMD = sqlDB.GetStoredProcCommand("PR_Category_Insert");
                }
                else
                {
                    dbCMD = sqlDB.GetStoredProcCommand("PR_Category_UpdateByPK");
                    sqlDB.AddInParameter(dbCMD, "@CategoryId", SqlDbType.Int, CategoryId);
                }
                sqlDB.AddInParameter(dbCMD, "@CategoryName", SqlDbType.NVarChar, CategoryName);
                sqlDB.AddInParameter(dbCMD, "@ImageUrl", SqlDbType.NVarChar, ImageUrl);
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
