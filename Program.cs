using System;
using System.Data;
using System.Data.SqlClient;

namespace DBD___Compulsory_Assignment_1
{
    class Program
    {
        static void Main(string[] args)
        {
            using (var sqlConnection = new SqlConnection(@"Server=mssql.jacobhinze.dk;Database=Company;Enlist=False;User ID=mikkel;Password=eerrddff11,,;"))
            {
                sqlConnection.Open();
                using (var sqlCommand = new SqlCommand("EXECUTE dbo.usp_GetAllDepartments", sqlConnection))
                {
                    using (var reader = sqlCommand.ExecuteReader())
                    {
                        PrintData(reader);
                    }
                }
            }
        }

        private static void PrintData(SqlDataReader reader)
        {
            if (reader.HasRows)
            {
                while (reader.Read())
                {
                    for (var i = 0; i < reader.FieldCount; i++)
                    {
                        Console.Write(reader.GetValue(i) + "\t");
                    }
                    Console.WriteLine();
                }
            }
            else
            {
                Console.WriteLine("No rows found.");
            }
            reader.Close();
        }
    }
}
