using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel.Composition;
using System.Data;
using System.Data.SqlClient;
using Shinto.Cache;

namespace Shinto.Data
{
    /// <summary>
    /// Encapsulate ADO.Net operations
    /// </summary>
    [Export(typeof(DataAccessAdapter))]
    [PartCreationPolicy(CreationPolicy.NonShared)]
    public class DataAccessAdapter
    {
        public DataAccessAdapter()
        {
            DefaultSchema = "dbo";
        }

        /// <summary>
        /// 
        /// </summary>
        [Import]
        public IConnectionManager ConnectionManager { get; set; }

        [Import(AllowDefault=true)]
        public ICacheProvider CacheProvider { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string DefaultSchema { get; set; }

        public string DefaultConnectionStringName { get; set; }

        protected SqlConnection GetOpenConnection(string connectionString)
        {
            var connection = new SqlConnection(connectionString);
            connection.Open();
            return connection;
        }

        public void ExecuteReader(string cmdText, Action<SqlDataReader> readerCallback, params Parameter[] arguments)
        {
            string conStr = ConnectionManager.GetConnectionString(DefaultConnectionStringName);
            ExecuteReader(cmdText, conStr, readerCallback, arguments);
        }

        public void ExecuteReader(string cmdText, string connectionString, Action<SqlDataReader> readerCallback, params Parameter[] arguments)
        {
            using (var connection = GetOpenConnection(connectionString))
            {
                using (var cmd = connection.CreateCommand())
                {
                    cmd.CommandText = cmdText;
                    AddArguments(cmd, arguments);
                    using (var reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            readerCallback(reader);
                        }
                    }
                }
            }
        }

        public T ExecuteScalar<T>(string cmdText, params Parameter[] arguments)
        {
            string conStr = ConnectionManager.GetConnectionString(DefaultConnectionStringName);
            return ExecuteScalar<T>(cmdText, conStr, arguments);
        }


        public T ExecuteScalar<T>(string cmdText, string connectionString, params Parameter[] arguments)
        {
            using (var con = GetOpenConnection(connectionString))
            {
                using (var cmd = con.CreateCommand())
                {
                    cmd.CommandText = cmdText;
                    AddArguments(cmd, arguments);
                    object value = cmd.ExecuteScalar();
                    T castValue = default(T);
                    try 
	                {
                        castValue = (T)value;
	                }
	                catch (InvalidCastException ex)
	                {
                        string message = string.Format("Cannot cast type {0} to desired type{1}", value.GetType(), typeof(T));
                        throw new InvalidCastException(message, ex);
	                }
                    return castValue;
                }
            }
        }

        public void ExecuteNonQuery(string cmdText, params Parameter[] arguments)
        {
            string conStr = ConnectionManager.GetConnectionString(DefaultConnectionStringName);
            ExecuteNonQuery(cmdText, conStr, arguments);
        }

        public void ExecuteNonQuery(string cmdText, string connectionString, params Parameter[] arguments)
        {
            using(var connection = GetOpenConnection(connectionString))
            {
                using (var cmd = connection.CreateCommand())
                {
                    cmd.CommandText = cmdText;
                    AddArguments(cmd, arguments);
                    int rowsAffected = cmd.ExecuteNonQuery();
                }
            }
        }




        public DataSet ExecuteDataSet(string cmdText, params Parameter[] arguments)
        {
            string conStr = ConnectionManager.GetConnectionString(DefaultConnectionStringName);
            return ExecuteDataSet(cmdText, conStr, arguments);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="cmdText"></param>
        /// <param name="connectionString"></param>
        /// <param name="arguments"></param>
        /// <returns></returns>
        public DataSet ExecuteDataSet(string cmdText, string connectionString, params Parameter[] arguments)
        {
            var ds = new DataSet();
            using (var con = GetOpenConnection(connectionString))
            {
                using (var cmd = con.CreateCommand())
                {
                    cmd.CommandText = cmdText;
                    AddArguments(cmd, arguments);
                    using (var reader = cmd.ExecuteReader())
                    {
                        bool hasMoreResults = true;
                        do
                        {                            
                            var schema = BuildSchema(reader);
                            int fieldCount = schema.Values.Count;
                            var table = new RowSet(schema);
                            ds.Tables.Add(table);
                            while (reader.Read())
                            {
                                object[] values = new object[fieldCount];
                                for (int i = 0; i < fieldCount; ++i)
                                {
                                    values[i] = reader.GetValue(i);
                                }
                                table.AddRow(values);
                            }

                            hasMoreResults = reader.NextResult();

                        } while (hasMoreResults);
                    }
                }

            }

            return ds;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sprocName"></param>
        /// <param name="connectionString"></param>
        /// <param name="arguments"></param>
        /// <returns></returns>
        public DataSet ExecuteSprocDataSet(string sprocName, string schemaName, string connectionString, params object[] arguments)
        {
            var ds = new DataSet();
            using (var con = GetOpenConnection(connectionString))
            {
                using (var cmd = con.CreateCommand())
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.CommandText = string.Concat(schemaName,".", sprocName);
                    SniffSprocParameters(cmd);
                    AddSprocArguments(cmd, arguments);
                    using (var reader = cmd.ExecuteReader())
                    {
                        bool hasMoreResults = true;
                        do
                        {
                            var schema = BuildSchema(reader);
                            int fieldCount = schema.Values.Count;
                            var table = new RowSet(schema);
                            ds.Tables.Add(table);
                            while (reader.Read())
                            {
                                object[] values = new object[fieldCount];
                                for (int i = 0; i < fieldCount; ++i)
                                {
                                    values[i] = reader.GetValue(i);
                                }
                                table.AddRow(values);
                            }

                            hasMoreResults = reader.NextResult();

                        } while (hasMoreResults);
                    }
                }

            }

            return ds;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="command"></param>
        void SniffSprocParameters(SqlCommand command)
        {
            string cacheKey = command.CommandText;
            SqlParameter[] sprocParams = null;
            if (CacheProvider.Contains(cacheKey))
            {
                sprocParams = CacheProvider.Get<SqlParameter[]>(cacheKey);
            }
            else
            {
                SqlCommandBuilder.DeriveParameters(command);
                sprocParams = new SqlParameter[command.Parameters.Count];
                for (int i = 0; i < sprocParams.Length; ++i)
                {
                    var param = command.Parameters[i] as SqlParameter;
                    var paramClone = param as ICloneable;
                    var clonedParam = paramClone.Clone();
                    sprocParams[i] = clonedParam as SqlParameter;
                }
                CacheProvider.Put(cacheKey, sprocParams, new Shinto.Cache.Modules.AbsoluteExpirationPolicy(cacheKey, sprocParams, DateTime.Now.AddHours(1)));
                return;//Command already has parameters, return to avoid adding, below
            }

            if (sprocParams != null && sprocParams.Length > 0)
            {
                //Add the params to the given command
                for (int i = 0; i < sprocParams.Length; ++i)
                {
                    SqlParameter param = sprocParams[i];
                    var clonedParam = new SqlParameter(param.ParameterName, param.SqlDbType, param.Size, param.Direction, param.IsNullable, param.Precision, param.Scale, param.SourceColumn, param.SourceVersion, param.Value);
                    command.Parameters.Add(clonedParam);
                }
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="reader"></param>
        /// <returns></returns>
        Dictionary<int, string> BuildSchema(SqlDataReader reader)
        {
            var schemaTable = reader.GetSchemaTable();
            Dictionary<int, string> schema = new Dictionary<int, string>();
            foreach (DataRow row in schemaTable.Rows)
            {
                int ord = (int)row["ColumnOrdinal"];
                string name = (string)row["ColumnName"];
                schema[ord] = name;
            }
            return schema;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="cmd"></param>
        /// <param name="arguments"></param>
        void AddArguments(SqlCommand cmd, params Parameter[] arguments)
        {
            if (null != arguments)
            {
                for (int i = 0; i < arguments.Length; ++i)
                {
                    cmd.Parameters.AddWithValue(arguments[i].Name, arguments[i].Value);
                }
            }
        }

        /// <summary>
        /// Add un-named pre-sniffed parameter values to the sproc command
        /// </summary>
        /// <param name="cmd"></param>
        /// <param name="arguments"></param>
        void AddSprocArguments(SqlCommand cmd, object[] arguments)
        {
            int argC = 0;
            for (int i = 0; i < cmd.Parameters.Count; ++i)
            {
                var parameter = cmd.Parameters[i];
                parameter.TypeName = string.Empty;
                if (ParameterDirection.Input == parameter.Direction
                    || ParameterDirection.InputOutput == parameter.Direction)
                {
                    parameter.Value = arguments[argC];
                    ++argC;
                }
            }
        }
    }
}
