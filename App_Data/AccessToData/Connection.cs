using System;
using System.Data;
using System.Configuration;
using System.Data.SqlClient;
using System.Collections.Generic;
using System.Reflection;
using System.Web.UI.WebControls.WebParts;

public class Connection
{
    // Cadena de conexión a la base de datos
    private readonly string connectionString;

    public Connection()
    {
        // Se obtiene la cadena de conexión desde web.config
        connectionString = ConfigurationManager.ConnectionStrings["MiConexion"].ConnectionString;
    }

    #region ExecuteQueryDataTable Retorna una tabla de datos
    //Si la query es correcta retornara una tabla y si no es el caso retorna un error
    public DataTable ExecuteQueryDataTable(string query)
    {
        try
        {
            // Se crea una nueva conexión a la base de datos utilizando la cadena de conexión
            using (var connection = new SqlConnection(connectionString))
            {
                // Se crea un nuevo comando SQL utilizando la conexión y la consulta proporcionadas
                using (var command = new SqlCommand(query, connection))
                {
                    // Se especifica que el tipo de comando es de tipo Texto (consulta SQL)
                    command.CommandType = CommandType.Text;
                    // Se establece el tiempo máximo de espera para la ejecución del comando
                    command.CommandTimeout = 72000;
                    // Se abre la conexión a la base de datos
                    connection.Open();
                    // Se ejecuta el comando y se obtiene un lector de datos para leer los resultados
                    using (var reader = command.ExecuteReader())
                    {
                        // Se crea una nueva tabla de datos para almacenar los resultados
                        var dataTable = new DataTable();
                        // Se carga el lector de datos en la tabla de datos
                        dataTable.Load(reader);
                        // Se devuelve la tabla de datos con los resultados
                        return dataTable;
                    }
                }
            }
        }
        catch (Exception ex)
        {
            // Se lanza una excepción personalizada con un mensaje de error específico y la excepción original como causa
            throw new QueryExecutionException("Error al ejecutar la consulta.", ex);
        }
    }
    #endregion

    #region ExecuteQueryString Regresa un string
    public string ExecuteQueryString(string query)
    {
        try
        {
            using (var connection = new SqlConnection(connectionString))
            {
                using (var command = new SqlCommand(query, connection))
                {
                    command.CommandType = CommandType.Text;
                    command.CommandTimeout = 72000;

                    connection.Open();

                    object result = command.ExecuteScalar();

                    // Se verifica si el resultado no es nulo y se convierte a string
                    if (result != null)
                    {
                        return result.ToString();
                    }
                    else
                    {
                        // Si el resultado es nulo, se devuelve una cadena vacía
                        return string.Empty;
                    }
                }
            }
        }
        catch (Exception ex)
        {
            throw new QueryExecutionException("Error al ejecutar la consulta.", ex);
        }
    }
    #endregion

    #region ExecuteQueryStringList Retorna un List<string>
    public List<string> ExecuteQueryStringList(string query)
    {
        try
        {
            using (var connection = new SqlConnection(connectionString))
            {
                using (var command = new SqlCommand(query, connection))
                {
                    command.CommandType = CommandType.Text;
                    command.CommandTimeout = 72000;

                    connection.Open();

                    using (var reader = command.ExecuteReader())
                    {
                        var resultList = new List<string>();

                        while (reader.Read())
                        {
                            // Se obtiene el valor de la primera columna como string
                            string value = reader.GetString(0);
                            resultList.Add(value);
                        }

                        return resultList;
                    }
                }
            }
        }
        catch (Exception ex)
        {
            throw new QueryExecutionException("Error al ejecutar la consulta.", ex);
        }
    }
    #endregion

    #region ExecuteNonQuery Regresa un true o false
    public bool ExecuteNonQuery(string query)
    {
        try
        {
            using (var connection = new SqlConnection(connectionString))
            {
                using (var command = new SqlCommand(query, connection))
                {
                    command.CommandType = CommandType.Text;
                    command.CommandTimeout = 72000;

                    connection.Open();

                    // almacena el número de filas afectadas en la variable rowsAffected
                    int rowsAffected = command.ExecuteNonQuery();

                    // Si se afectó al menos una fila, se devuelve true
                    return rowsAffected > 0;
                }
            }
        }
        catch (Exception ex)
        {
            throw new QueryExecutionException("Error al ejecutar la consulta.", ex);
        }
    }
    #endregion
}

public class QueryExecutionException : Exception
{
    public QueryExecutionException() : base() { }
    public QueryExecutionException(string message) : base(message) { }
    public QueryExecutionException(string message, Exception innerException) : base(message, innerException) { }
}