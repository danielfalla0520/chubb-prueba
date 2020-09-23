using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Reflection;
using System.Text;

namespace Data.Core.Connect
{
    public class DataModelService
    {

        private readonly SqlConnection _connection;

        public DataModelService()
        {
            _connection = dbContext.CreateConnection();
        }
        public List<TModel> GetListModel<TModel>(string procedure)
         where TModel : class, new()
        {
            var responseModel = new List<TModel>();

            using (_connection)
            {
                using (var command = _connection.CreateCommand())
                {
                    command.CommandText = procedure;
                    command.CommandType = CommandType.StoredProcedure;
                    _connection.Close();
                    _connection.Open();
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            TModel model = new TModel();
                            var properties = model.GetType().GetProperties();

                            foreach (var property in properties)
                            {
                                var obj = GetNullableValue(reader, property);
                                property.SetValue(model, obj);
                            }
                            responseModel.Add(model);
                        }
                    }
                }
            }
            return responseModel;
        }
        public List<TModel> GetListByParameter<TModel, TObject>(string procedure, TObject actualModel)
        where TModel : class, new()
        where TObject : new()
        {
            var responseModel = new List<TModel>();

            using (_connection)
            {
                using (var command = CreateCustomCommand(procedure))
                {
                    CommandSetParameters<TObject>(command, actualModel);
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            TModel model = new TModel();
                            var properties = model.GetType().GetProperties();

                            foreach (var property in properties)
                            {
                                try
                                {
                                    var obj = GetNullableValue(reader, property);
                                    property.SetValue(model, obj);
                                }
                                catch
                                {

                                }
                            }
                            responseModel.Add(model);
                        }

                    }
                }
            }
            _connection.Close();
            return responseModel;
        }
        public TModel GetObjectByParameter<TModel, TObject>(string procedure, TObject actualModel)
            where TModel : new()
            where TObject : new()
        {
            TModel returnValue = new TModel();
            using (_connection)
            {
                using (var command = CreateCustomCommand(procedure))
                {
                    CommandSetParameters<TObject>(command, actualModel);
                    CommandSetOutPutParameters<TModel>(command, returnValue);
                    int icommand = command.ExecuteNonQuery();

                    var properties = returnValue.GetType().GetProperties();
                    foreach (var property in properties)
                    {
                        try
                        {
                            var obj = command.Parameters["@" + property.Name].Value;
                            property.SetValue(returnValue, obj);
                        }
                        catch
                        {
                        }
                    }
                }
            }
            _connection.Close();
            return returnValue;
        }
        public void CommandSetOutPutParameters<TObject>(SqlCommand command, TObject actualModel)
           where TObject : new()
        {
            var inAttributes = actualModel.GetType().GetProperties();
            foreach (var inAttribute in inAttributes)
            {
                SqlParameter returnParameter = command.Parameters.AddWithValue("@" + inAttribute.Name, inAttribute.GetValue(actualModel));
                returnParameter.Direction = ParameterDirection.Output;
            }
        }
        private object GetNullableValue(SqlDataReader reader, PropertyInfo property)
        {
            var t = property.PropertyType;

            if (reader[property.Name] == null || reader[property.Name] is DBNull)
                return null;

            if (property.PropertyType.Name.Equals(typeof(Nullable<>).Name))
            {
                t = Nullable.GetUnderlyingType(t);
                return Convert.ChangeType(reader[property.Name], t);
            }

            return Convert.ChangeType(reader[property.Name], property.PropertyType);
        }
        public SqlCommand CreateCustomCommand(string CommandText)
        {
            SqlCommand sqlcommand = _connection.CreateCommand();
            sqlcommand.CommandTimeout = 30; //30 seconds Time out 
            sqlcommand.CommandType = CommandType.StoredProcedure;
            sqlcommand.CommandText = CommandText;
            return sqlcommand;
        }
        public void CommandSetParameters<TObject>(SqlCommand command, TObject actualModel)
            where TObject : new()
        {
            var inAttributes = actualModel.GetType().GetProperties();
            foreach (var inAttribute in inAttributes)
            {
                object parameterValue = inAttribute.GetValue(actualModel);
                if (parameterValue == null) parameterValue = DBNull.Value;
                command.Parameters.AddWithValue("@" + inAttribute.Name, parameterValue);
            }
        }
    }
}
