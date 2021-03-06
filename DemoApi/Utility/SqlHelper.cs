﻿using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;

namespace DemoApi.Utility
{
    public static class SqlHelper
    {
        public static string ExecuteProcedureReturnString(string connString, string procName,params SqlParameter[] paramters){
            string result = "";
            using (var sqlConnection = new SqlConnection(connString)){
                using (var command = sqlConnection.CreateCommand()){
                    command.CommandType = CommandType.StoredProcedure;
                    command.CommandText = procName;
                    if (paramters != null)
                        command.Parameters.AddRange(paramters);
                    sqlConnection.Open();
                    var ret = command.ExecuteScalar();
                    if (ret != null)
                        result = Convert.ToString(ret);
                }
            }
            return result;
        }

        public static T ExtecuteProcedureReturnData<T>(string connString, string procName,Func<SqlDataReader, T> translator,params SqlParameter[] parameters){
            using (var sqlConnection = new SqlConnection(connString)){
                using (var sqlCommand = sqlConnection.CreateCommand()){
                    sqlCommand.CommandType = CommandType.StoredProcedure;
                    sqlCommand.CommandText = procName;
                    if (parameters != null)
                        sqlCommand.Parameters.AddRange(parameters);
                    sqlConnection.Open();
                    using (var reader = sqlCommand.ExecuteReader()){
                        T elements;
                        try
                        {
                            elements = translator(reader);
                        }
                        finally
                        {
                            while (reader.NextResult())
                            { }
                        }
                        return elements;
                    }
                }
            }
        }

        public static T ExtecuteStatementData<T>(string connString, string consulta, Func<SqlDataReader, T> translator, params SqlParameter[] parameters){
            using (var sqlConnection = new SqlConnection(connString)){
                using (var sqlCommand = sqlConnection.CreateCommand()){
                    sqlCommand.CommandType = CommandType.Text;
                    sqlCommand.CommandText = consulta;
                    if (parameters != null)
                        sqlCommand.Parameters.AddRange(parameters);
                    sqlConnection.Open();
                    using (var reader = sqlCommand.ExecuteReader()){
                        T element;
                        try{
                            element = translator(reader);
                        }
                        finally{
                            while (reader.NextResult()){ }
                        }
                        return element;
                    }
                }
            }
        }

        #region Get Values from Sql Data Reader
        public static string GetNullableString(SqlDataReader reader, string colName){
            return reader.IsDBNull(reader.GetOrdinal(colName)) ? null : Convert.ToString(reader[colName]);
        }

        public static int GetNullableInt32(SqlDataReader reader, string colName){
            return reader.IsDBNull(reader.GetOrdinal(colName)) ? 0 : Convert.ToInt32(reader[colName]);
        }

        public static bool GetBoolean(SqlDataReader reader, string colName){
            return reader.IsDBNull(reader.GetOrdinal(colName)) ? default(bool) : Convert.ToBoolean(reader[colName]);
        }

        //this method is to check wheater column exists or not in data reader
        public static bool IsColumnExists(this System.Data.IDataRecord dr, string colName){
            try{
                return (dr.GetOrdinal(colName) >= 0);
            }
            catch (Exception){
                return false;
            }
        }
        #endregion
    }
}