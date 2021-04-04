using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Data.Sql;
using System.Data.SqlClient;
using System.Linq;
using System.Configuration;
using System.Text;
using System.Threading.Tasks;
using Microsoft.SqlServer.Management.Smo;
using System.Windows.Forms;
using AirlineManagementSystemDatabasesAssistant.views;
using System.Reflection;
using System.Threading;

namespace AirlineManagementSystemDatabasesAssistant
{
    class DAO
    {
        protected DbConnection _connection;
        protected DbCommand _command;

        public DAO()
        {
            _connection = new SqlConnection();
            _command = new SqlCommand();
            _command.CommandType = System.Data.CommandType.Text;
            _command.Connection = _connection;
        }

        public List<string> GetAllTableNames()
        {
            List<string> tableNames = new List<string>();
            _connection.Open();

            var schema = _connection.GetSchema("Tables");
            foreach (DataRow s in schema.Rows)
            {
                string tablename = (string)s[2];
                tableNames.Add(tablename);
            }
            _connection.Close();
            return tableNames;
        }
        internal string GetTableName(Type pocoType)
        {
            string tableName = string.Empty;
            foreach (var s in this.GetAllTableNames())
            {
                //if (s.Contains(pocoType.Name.ChopCharsFromTheEnd(1))) tableName = s;
                string pocoType_Name = string.Empty;
                if (pocoType.Name.Contains("History")) pocoType_Name = pocoType.Name;
                else pocoType_Name = pocoType.Name.PluraliseNoun();

                if (s.Equals(pocoType_Name)) tableName = s;
            }
            return tableName;
        }

        private async Task<string[]> GetServerAndInstanceNamesAsync()
        {
            return await Task.Run(() => 
            {
                string servername = string.Empty;
                string instancename = string.Empty;
                string[] retval = new string[2];
                var table = SqlDataSourceEnumerator.Instance.GetDataSources();

                foreach (DataRow row in table.AsEnumerable())
                {
                    if (!(row["ServerName"] is DBNull)) servername = (string)row["ServerName"];
                    if (!(row["InstanceName"] is DBNull)) instancename = "\\" + (string)row["InstanceName"];
                }

                retval[0] = servername;
                retval[1] = instancename;
                return retval;
            });

        }

        //"\SQLEXPRESS"

        public async Task SetConnectionStringAsync(string dataBaseName)
        {
            ///first shoot
            //trying to build the connection string and connect with empty string as instance name 
            string instanceName = string.Empty;
            string mayBeConnectionString = await TryToConnectCheckConnectionAsync(dataBaseName, instanceName);
            if (mayBeConnectionString != "BAD_CONNECTION")
                SetConnectionStringInternal(mayBeConnectionString);
            else
            {
                //second shoot
                //trying to build the connection string and connect with "\SQLEXPRESS" as instance name 
                instanceName = @"\SQLEXPRESS";
                mayBeConnectionString = await TryToConnectCheckConnectionAsync(dataBaseName, instanceName);
                if(mayBeConnectionString != "BAD_CONNECTION")
                    SetConnectionStringInternal(mayBeConnectionString);
                else
                {
                    //third shoot - let's do this the hard way
                    //trying to get the instance name 
                    //(and by the way also the server name, althout although  we don't really need to get it this way, cause is is the value if "Enviroment.MachineName")
                    //by enumerationg "SqlDataSourceEnumerator"
                    string[] serverAndInstancenames = await GetServerAndInstanceNamesAsync();
                    string serverName = serverAndInstancenames[0];
                    instanceName = serverAndInstancenames[1];
                    mayBeConnectionString = CreateConnectionString(dataBaseName, instanceName);
                    SetConnectionStringInternal(mayBeConnectionString);
                }
            }
        }
        private async Task<string> TryToConnectCheckConnectionAsync(string dataBaseName, string instanceName)
        {
            return await Task.Run(() => 
            {
                string connStr = null;
                try
                {
                    connStr = CreateConnectionString(dataBaseName, instanceName);
                    SetConnectionStringInternal(connStr);
                    try
                    {
                        _connection.Open();
                    }
                    finally { _connection.Close(); }
                    return connStr;
                }
                catch (SqlException)
                {

                }//SqlException
                return "BAD_CONNECTION";
            });

        }

        private string CreateConnectionString(string dataBaseName, string instanceName)
        {
            return $"Data Source={Environment.MachineName}{instanceName};Initial Catalog={dataBaseName};Integrated Security=True";
        }

        private void SetConnectionStringInternal(string connStr)
        {
            var config = ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None);

            config.ConnectionStrings.ConnectionStrings["connString1"].ConnectionString = connStr;
            config.ConnectionStrings.ConnectionStrings["connString1"].ProviderName = "System.Data.SqlClient";
            config.Save(ConfigurationSaveMode.Modified);

            _connection.ConnectionString = config.ConnectionStrings.ConnectionStrings["connString1"].ConnectionString;
        }



        /// <summary>
        /// Implementing mechanism that provides names of all the MSSQL databases.
        /// Takes some time to 
        /// </summary>
        /// <returns>List<string> with names of all the databases</returns>
        public async Task<List<string>> GetAllDataBasesAsync()
        {
            return await Task.Run(async() => 
            {
                List<string> databases = new List<string>();
                string[] serverAndInstanceNames = await GetServerAndInstanceNamesAsync();
                string serverName = serverAndInstanceNames[0];
                string instanceName = serverAndInstanceNames[1];
                //
                //the server name also represented by Environment.MachineName,
                //as: 
                //serverName = Environment.MachineName;
                //so uncommenting the previous line is a way to get this machine name for Smo object
                //if the method "GetServerAndInstanceNames" don't working
                //

                var server = new Microsoft.SqlServer.Management.Smo.Server(serverName + instanceName);
                foreach (Database db in server.Databases)
                {
                    databases.Add(db.Name);
                }
                return databases;
            });

            
        }

        /// <summary>
        /// Set the SQL command
        /// </summary>
        /// <param name="commandType"></param>
        /// <param name="commandText"></param>
        protected void SetCommand(CommandType commandType, string commandText)
        {
            //in this overload, CommandType will be "Text"
            _command.CommandType = commandType;
            ////in this overload, CommandText will be the command text string.
            _command.CommandText = commandText;
        }

        public async Task<T> GetOneById<T>(long id) where T : class, new()
        {
            string tablename = GetTableName(typeof(T));
            Func<string, string> connStrFunc = (tablenameToFunc) =>
            {
                return $"SELECT * FROM {tablenameToFunc} WHERE ID = {id}";
            };
            return (await BaseMethodGet<T>(null, null, connStrFunc, null, -1, tablename)).FirstOrDefault();
        }

        public async Task<T> GetOneByRegUserId<T>(long user_id) where T : class, IAsUserForConvertingToData, new()
        {
            string tablename = GetTableName(typeof(T));
            Func<string, string> connStrFunc = (tablenameToFunc) =>
            {
                return $"SELECT * FROM {tablenameToFunc} WHERE USER_ID = {user_id}";
            };
            return (await BaseMethodGet<T>(null, null, connStrFunc, null, -1, tablename)).FirstOrDefault();
        }

        public async Task<IEnumerable<T>> GetAll<T>() where T: class, new()
        {
            string tablename = GetTableName(typeof(T));
            Func<string, string> connStrFunc = (tablenameToFunc) => 
            {
                return $"SELECT * FROM {tablenameToFunc}";
            };
            return await BaseMethodGet<T>(null, null, connStrFunc, null, -1, tablename);
        }
        public async Task<IEnumerable<Flight>> GetAllFlightsByAirlineName(long airlineUtilitiClassUserId)
        {
            Func<long, string> connStrFunc = (airlineUtilitiClassUserIdToFunc) => 
            {
                return $"select Flights.* from Flights JOIN AirlineCompanies ON Flights.AIRLINECOMPANY_ID = AirlineCompanies.ID JOIN Utility_class_Users ON AirlineCompanies.USER_ID = Utility_class_Users.ID WHERE Utility_class_Users.ID = {airlineUtilitiClassUserIdToFunc}";
            };
            return await BaseMethodGet<Flight>(null, connStrFunc, null, null, airlineUtilitiClassUserId, null);
        }







        private async Task<IEnumerable<T>> BaseMethodGet<T>(Func<string> connStrFuncParamless, Func<long, string> connStrFuncOneLongParam, Func<string, string> connStrFuncOneStringParam,  Func<long, string, string> connStrFuncOneLongAndOneStringParam, long funcParamLong, string funcParamString) where T : class, new()
        {
            string connStr = null;
            if (funcParamLong > 0 && funcParamString != null)
            {
                if (connStrFuncOneLongAndOneStringParam != null && connStrFuncOneLongAndOneStringParam is MulticastDelegate)
                    connStr = connStrFuncOneLongAndOneStringParam(funcParamLong, funcParamString);
                else throw new Exception("delegate \"Func<long, string, string>\" is missing!");
            }
            else if (funcParamLong > 0 && funcParamString == null)
            {
                if (connStrFuncOneLongParam != null && connStrFuncOneLongParam is MulticastDelegate)
                    connStr = connStrFuncOneLongParam(funcParamLong);
                else throw new Exception("delegate \"Func<long, string>\" is missing!");
            }
            else if (funcParamLong < 0 && funcParamString != null)
            {
                if (connStrFuncOneStringParam != null && connStrFuncOneStringParam is MulticastDelegate)
                    connStr = connStrFuncOneStringParam(funcParamString);
                else throw new Exception("delegate \"Func<string, string>\" is missing!");
            }
            else if (funcParamLong < 0 && funcParamString == null)
            {
                if (connStrFuncParamless != null && connStrFuncParamless is MulticastDelegate)
                    connStr = connStrFuncParamless();
                else throw new Exception("delegate \"Func<string>\" is missing!");
            }

            Func<string, Task<IEnumerable<T>>> func = ReadFromDBReader<T>;
            return await ProcessExceptions<T>(func, connStr);
        }
        private async Task<IEnumerable<T>> ReadFromDBReader<T>(string connStr) where T: class, new()
        {
            List<T> returnValLst = new List<T>();
            try
            {
                _connection.Open();
                _command.CommandType = CommandType.Text;
                _command.CommandText = connStr;

                using (DbDataReader reader = _command.ExecuteReader())
                {
                    while (await reader.ReadAsync())
                    {
                        //yield return ReadFromDbAndFillPoco<T>(reader);
                        returnValLst.Add(await ReadFromDbAndFillPocoAsync<T>(reader));

                    }
                }
            }
            finally
            {
                _connection.Close();
            }
            return returnValLst;
        }
        private async Task<T> ReadFromDbAndFillPocoAsync<T>(DbDataReader reader) where T: class, new()
        {
            Func<DbDataReader, Task<T>> func = async(readerToFunc) =>
            {

                T poco = new T();
                for (int i = 0; i < reader.FieldCount; i++)
                {
                    object value = await reader.GetFieldValueAsync<object>(i);
                    if (reader[i] is DBNull && typeof(T).GetProperties()[i].GetType().Name.ToLower().Equals("string"))
                    {
                        typeof(T).GetProperties()[i].SetValue(poco, string.Empty);
                    }
                    if (reader[i] is DBNull && typeof(T).GetProperties()[i].GetType().Name.ToLower().Contains("int"))
                    {
                        typeof(T).GetProperties()[i].SetValue(poco, 0);
                    }


                    //if (!(reader[i] is DBNull)) { typeof(T).GetProperties()[i].SetValue(poco, value); }
                    if (!(reader[i] is DBNull)) { typeof(T).GetProperties().OrderBy(x => x.MetadataToken).ToArray()[i].SetValue(poco, value); }

                    string type = typeof(T).Name;
                    PropertyInfo[] propArr = typeof(T).GetProperties();
                    PropertyInfo[] sortedArr = typeof(T).GetProperties().OrderBy(x => x.MetadataToken).ToArray();
                }
                return poco;
            };

            return await ProcessExceptionsAsync(func, reader);
        }






        protected async Task<IEnumerable<T>> ProcessExceptions<T>(Func<string, Task<IEnumerable<T>>> func, string strParam) where T: class, new()
        {
            object val = null;
            try
            {
                val = await func(strParam);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"{ex.GetType().Name}\n\n{ex.Message}\n\n\n{ex.StackTrace}");
            }
            return val as IEnumerable<T>;
        }
        protected T ProcessExceptions<T>(Func<T> func) where T : class, new()
        {
            object val = null;
            try
            {
                val = func();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"{ex.GetType().Name}\n\n{ex.Message}\n\n\n{ex.StackTrace}");
            }
            return val as T;
        }
        protected async Task<T> ProcessExceptionsAsync<T, TFuncParam>(Func<TFuncParam, Task<T>> func, TFuncParam funcParam) where T : class, new()
        {
            object val = null;
            try
            {
                val = await func(funcParam);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"{ex.GetType().Name}\n\n{ex.Message}\n\n\n{ex.StackTrace}");
            }
            return val as T;
        }
        protected T_out ProcessExceptions<T_in, T_out>(Func<T_in, T_out> func, T_in parameter) where T_out : class, new()
        {
            T_out val = null;
            try
            {
                val = func(parameter);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"{ex.GetType().Name}\n\n{ex.Message}\n\n\n{ex.StackTrace}");
            }
            return val;
        }
        protected void ProcessExceptions(Action act)
        {

            try
            {
                act();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"{ex.GetType().Name}\n\n{ex.Message}\n\n\n{ex.StackTrace}");
            }
        }
    }
}
