using System;
using System.Data.SqlClient;

namespace DBD___Compulsory_Assignment_1
{
    internal class Program
    {
        private static int id;

        private static void CreateDepartment(SqlConnection sqlConnection)
        {
            using (var sqlCommand = new SqlCommand("EXEC dbo.usp_CreateDepartment 'Testing', 333445555", sqlConnection))
            {
                var result = sqlCommand.ExecuteScalar();
                id = (int) result;
                Console.WriteLine($"Created department with ID {id}");
            }
        }


        private static void DeleteDepartment(SqlConnection sqlConnection)
        {
            using (var sqlCommand = new SqlCommand("EXEC dbo.usp_DeleteDepartment " + id, sqlConnection))
            {
                var rowsAffected = sqlCommand.ExecuteNonQuery();
                Console.WriteLine($"{rowsAffected} row(s) affected");
            }
        }
        private static void UpdateDepartmentName(SqlConnection sqlConnection)
        {
            using (var sqlCommand = new SqlCommand($"EXEC dbo.usp_UpdateDepartmentName {id}, 'TestingEdited'", sqlConnection))
            {
                var rowsAffected = sqlCommand.ExecuteNonQuery();
                Console.WriteLine($"{rowsAffected} row(s) affected");
            }
        }

        private static void UpdateDepartmentManager(SqlConnection sqlConnection)
        {
            using (var sqlCommand = new SqlCommand($"EXEC dbo.usp_UpdateDepartmentManager {id}, 453453453", sqlConnection))
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
                Console.WriteLine("Before:");
                PrintDepartments(sqlConnection);
                Console.WriteLine("Create:");
                CreateDepartment(sqlConnection);
                PrintDepartments(sqlConnection);
                Console.WriteLine("Update name:");
                UpdateDepartmentName(sqlConnection);
                PrintDepartment(sqlConnection, id);
                Console.WriteLine("Update manager:");
                UpdateDepartmentManager(sqlConnection);
                PrintDepartment(sqlConnection, id);
                Console.WriteLine("Delete:");
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