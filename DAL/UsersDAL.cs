using Microsoft.Practices.EnterpriseLibrary.Data.Sql;
using System.Data.Common;
using System.Data;

namespace Food_Ordering.DAL
{
    public class UsersDAL : ConnectionDAL
    {
        public DataTable PR_User_SelectByIDPass(String? Email, String? Password)
        {
            try
            {
                SqlDatabase sqlDB = new SqlDatabase(SQL_Connection);
                DbCommand dbCMD = sqlDB.GetStoredProcCommand("PR_User_SelectByIDPass");
                sqlDB.AddInParameter(dbCMD, "Email", SqlDbType.NVarChar, Email);
                sqlDB.AddInParameter(dbCMD, "Password", SqlDbType.NVarChar, Password);

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
    }
}
