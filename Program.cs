using System;
using System.Data;
using System.Data.SqlClient;

namespace DBD___Compulsory_Assignment_1
{
    class Program
    {
        static void Main(string[] args)
        {
            using (var sqlConnection = new SqlConnection(@"Server=mssql.jacobhinze.dk;Database=DB_SearchEngine;Enlist=False;User ID=mikkel;Password=eerrddff11,,;"))
            {
                sqlConnection.Open();
                using (var sqlCommand = new SqlCommand("EXECUTE dbo.usp_GetAllDepartments", sqlConnection))
                {
                    var reader = sqlCommand.ExecuteReader();
                    var schemaTable = reader.GetSchemaTable();

                    foreach (DataRow row in schemaTable.Rows)
                    {
                        foreach (DataColumn column in schemaTable.Columns)
                        {
                            Console.WriteLine($"{column.ColumnName} = {row[column]}");
                        }
                    }
                }
            }
        }
    }
}
