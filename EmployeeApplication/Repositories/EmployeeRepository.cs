using EmployeeApplication.Data;
using EmployeeApplication.Models;
using Microsoft.AspNetCore.Components.Server;

namespace EmployeeApplication.Repositories
{
    public interface IEmployeeRepository
    {
        public List<Employee> GetEmployeeList();
        public bool InsertEmployee(Employee employee);
        public bool UpdateEmployee(Employee employee);
        public bool DeleteEmployee(int id);
        public decimal ComputeTakeHomePay(DateTime StartDate, DateTime EndDate, decimal DailyRate);

    }
    public class EmployeeRepository : IEmployeeRepository
    {
        private DataConnectionProvider _connectionProvider;

        public EmployeeRepository(DataConnectionProvider connectionProvider)
        {
            _connectionProvider = connectionProvider;
        }
        public List<Employee> GetEmployeeList()
        {
            var EmployeeList = new List<Employee>();

            using var connection = _connectionProvider.CreateConnection();
            using var cmd = connection.CreateCommand();
            cmd.CommandText = @"Select * from Employees";
            cmd.CommandType = System.Data.CommandType.Text;

            connection.Open();
            using var reader = cmd.ExecuteReader();

            while (reader.Read())
            {
                EmployeeList.Add(new Employee
                {
                    ID = reader["ID"] == DBNull.Value ? 0 : Convert.ToInt32(reader["ID"]),
                    EmployeeNumber = reader["EmployeeNumber"] != DBNull.Value ? reader["EmployeeNumber"].ToString() : string.Empty,
                    FirstName = reader["FirstName"] != DBNull.Value ? reader["FirstName"].ToString() : string.Empty,
                    MiddleName = reader["MiddleName"] != DBNull.Value ? reader["MiddleName"].ToString() : string.Empty,
                    LastName = reader["LastName"] != DBNull.Value ? reader["LastName"].ToString() : string.Empty,
                    BirthDate = reader["BirthDate"] != DBNull.Value ? Convert.ToDateTime(reader["BirthDate"]) : DateTime.MinValue,
                    DailyRate = reader["DailyRate"] == DBNull.Value ? 0 : Convert.ToDecimal(reader["DailyRate"]),
                    WorkingDays = reader["WorkingDays"] != DBNull.Value ? reader["WorkingDays"].ToString() : string.Empty
                });
            }

            return EmployeeList;
        }

        public bool InsertEmployee (Employee employee)
        {
            using var connection = _connectionProvider.CreateConnection();
            using var cmd = connection.CreateCommand();
            cmd.CommandText = @"Insert into Employees(FirstName, MiddleName, LastName, BirthDate, DailyRate, WorkingDays, EmployeeNumber)
                                values (@firstname, @middlename, @lastname, @birthdate, @dailyrate, @workingdays, @employeenumber)";
            cmd.Parameters.AddWithValue("@firstname", employee.FirstName.ToUpper());
            cmd.Parameters.AddWithValue("@middlename", employee.MiddleName.ToUpper());
            cmd.Parameters.AddWithValue("@lastname", employee.LastName.ToUpper());
            cmd.Parameters.AddWithValue("@birthdate", employee.BirthDate);
            cmd.Parameters.AddWithValue("@dailyrate", employee.DailyRate);
            cmd.Parameters.AddWithValue("@workingdays", employee.WorkingDays);
            cmd.Parameters.AddWithValue("@employeenumber", GenerateEmployeeNumber(employee.LastName, employee.BirthDate));
            
            connection.Open();
            cmd.ExecuteNonQuery();
            return true;
        }

        public bool UpdateEmployee(Employee employee)
        {
            using var connection = _connectionProvider.CreateConnection();
            using var cmd = connection.CreateCommand();

            cmd.CommandText = @"Update Employees set 
                                FirstName = @firstname, 
                                MiddleName = @middlename, 
                                LastName = @lastname, 
                                BirthDate = @birthdate, 
                                DailyRate = @dailyrate, 
                                WorkingDays = @workingdays,
                                EmployeeNumber = @EmployeeNumber
                                where ID = @id";

            cmd.Parameters.AddWithValue("@firstname", employee.FirstName.ToUpper());
            cmd.Parameters.AddWithValue("@middlename", employee.MiddleName.ToUpper());
            cmd.Parameters.AddWithValue("@lastname", employee.LastName.ToUpper());
            cmd.Parameters.AddWithValue("@birthdate", employee.BirthDate);
            cmd.Parameters.AddWithValue("@dailyrate", employee.DailyRate);
            cmd.Parameters.AddWithValue("@workingdays", employee.WorkingDays);
            cmd.Parameters.AddWithValue("@EmployeeNumber", GenerateEmployeeNumber(employee.LastName, employee.BirthDate));
            cmd.Parameters.AddWithValue("@id", employee.ID);

            connection.Open();
            cmd.ExecuteNonQuery();
            return true;
        }


        public bool DeleteEmployee(int id)
        {
            using var connection = _connectionProvider.CreateConnection();
            using var cmd = connection.CreateCommand();

            cmd.CommandText = @"Delete from Employees where ID = @id";

            cmd.Parameters.AddWithValue("@id", id);

            connection.Open();
            cmd.ExecuteNonQuery();
            return true;
        }

        public string GenerateEmployeeNumber (string LastName, DateTime Birthday)
        {
            var name = LastName.Length >= 3 ? LastName.Substring(0, 3). ToUpper() : LastName.ToUpper().PadRight(3, '*');
            var RandomNumber = new Random();
            var number = RandomNumber.Next(0,100000).ToString("D5");
            var bday = Birthday.ToString("ddMMMyyyy").ToUpper();

            var EmployeeNumber = name + "-" + number + "-" + bday;

            return EmployeeNumber;
        }

        public decimal ComputeTakeHomePay(DateTime StartDate,  DateTime EndDate, decimal DailyRate)
        {
            using var connection = _connectionProvider.CreateConnection();
            using var cmd = connection.CreateCommand();

            cmd.CommandText = @"[dbo].[TakeHomePay]";
            cmd.CommandType = System.Data.CommandType.StoredProcedure;

            cmd.Parameters.AddWithValue("@StartDate", StartDate);
            cmd.Parameters.AddWithValue("@EndDate", EndDate);
            cmd.Parameters.AddWithValue("@DailyRate", DailyRate);

            connection.Open();

            return Convert.ToDecimal(cmd.ExecuteScalar());
     
        }
    }
}
