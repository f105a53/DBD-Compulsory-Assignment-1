using System;
using System.Data.SqlClient;

namespace DBD___Compulsory_Assignment_1
{
    internal class Program
    {
        private static int id;

        private static void CreateDepartment(SqlConnection sqlConnection)
        {
            using (var sqlCommand = new SqlCommand("EXEC dbo.usp_CreateDepartment 'Testing', 123456789", sqlConnection))
            {
                id = (int) sqlCommand.ExecuteScalar();
                Console.WriteLine($"Created department with ID {id}");
            }
        }

        private static void DeleteDepartment(SqlConnection sqlConnection)
        {
            using (var sqlCommand = new SqlCommand("EXEC dbo.usp_DeleteDepartment 4", sqlConnection))
            {
                var rowsAffected = sqlCommand.ExecuteNonQuery();
                Console.WriteLine($"{rowsAffected} row(s) affected");
            }
        }

        private static void Main(string[] args)
        {
            using (var sqlConnection =
                new SqlConnection(
                    @"Server=mssql.jacobhinze.dk;Database=Company;Enlist=False;User ID=publicDB;Password=public;"))
            {
                sqlConnection.Open();
                PrintDepartments(sqlConnection);
                CreateDepartment(sqlConnection);
                PrintDepartment(sqlConnection, id);
                DeleteDepartment(sqlConnection);
                PrintDepartments(sqlConnection);
                sqlConnection.Close();
            }
        }

        private static void PrintData(SqlDataReader reader)
        {
            if (reader.HasRows)
                while (reader.Read())
                {
                    for (var i = 0; i < reader.FieldCount; i++) Console.Write(reader.GetValue(i) + "\t");
                    Console.WriteLine();
                }
            else
                Console.WriteLine("No rows found.");

            reader.Close();
        }

        private static void PrintDepartment(SqlConnection sqlConnection, int id)
        {
            Console.WriteLine("Department:");
            using (var sqlCommand = new SqlCommand("EXECUTE dbo.usp_GetDepartment " + id, sqlConnection))
            {
                using (var reader = sqlCommand.ExecuteReader())
                {
                    PrintData(reader);
                }
            }

            Console.WriteLine();
        }

        private static void PrintDepartments(SqlConnection sqlConnection)
        {
            Console.WriteLine("Departments:");
            using (var sqlCommand = new SqlCommand("EXECUTE dbo.usp_GetAllDepartments", sqlConnection))
            {
                using (var reader = sqlCommand.ExecuteReader())
                {
                    PrintData(reader);
                }
            }

            Console.WriteLine();
        }
    }
}