using DemoApi.Models;
using DemoApi.Translators;
using DemoApi.Utility;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;

namespace DemoApi.Repository
{
    public class UsuarioDao
    {
        public List<Usuario> GetAllUsers(string connString)
        {
            return SqlHelper.ExtecuteProcedureReturnData<List<Usuario>>(connString,"GetUsers", r => r.TranslateAsUsersList());
        }

        public string SaveUser(Usuario model, string connString)
        {
            var outParam = new SqlParameter("@ReturnCode", SqlDbType.NVarChar, 20){
                Direction = ParameterDirection.Output
            };
            SqlParameter[] param = {
                new SqlParameter("@Id",model.Id),
                new SqlParameter("@Name",model.Name),
                new SqlParameter("@Email",model.Email),
                new SqlParameter("@Mobile",model.Mobile),
                new SqlParameter("@Address",model.Address),
                outParam
            };
            SqlHelper.ExecuteProcedureReturnString(connString, "SaveUser", param);
            return (string)outParam.Value;
        }

        public Usuario GetUser(int id, string connString){
            string sql = "";
            sql = "select * from users where Id = @Id";
            SqlParameter[] param = {
                new SqlParameter("@Id",id)
            };
            return SqlHelper.ExtecuteStatementData<Usuario>(connString, sql, r => r.TranslateAsUser(),param);
        }

        public string DeleteUser(int id, string connString)
        {
            var outParam = new SqlParameter("@ReturnCode", SqlDbType.NVarChar, 20)
            {
                Direction = ParameterDirection.Output
            };
            SqlParameter[] param = {
                new SqlParameter("@Id",id),
                outParam
            };
            SqlHelper.ExecuteProcedureReturnString(connString, "DeleteUser", param);
            return (string)outParam.Value;
        }
    }
}
