using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataLibrary.Models;
using DataLibrary.Models.DataAccess;

namespace DataLibrary.Models.Logic
{
	public static class EmployeeProcessor
	{
		//public static int CreateEmployee( string firstName, string lastName, decimal salary, 
		//bool isCEO, bool isManager,int? managerId) {

		public static int CreateEmployee(EmployeesModel model) { 

			if (model.IsCEO) {
				model.ManagerId = null;
			}
			string sql = @"insert into dbo.Employees (FirstName, LastName, Salary, IsCEO, IsManager, ManagerId)
							values (@FirstName, @LastName, @Salary, @IsCEO, @IsManager, @ManagerId);";

			return SqlDataAccess.SaveData(sql, model);
		}

		public static List<EmployeesModel> LoadEmployees() {
			string sql = @"select Id, FirstName, LastName, Salary, IsCEO, IsManager, ManagerId from dbo.Employees;";

			return SqlDataAccess.LoadData<EmployeesModel>(sql);
		}

		public static EmployeesModel CheckForCEO() {
			string sql = @"select * from dbo.Employees where IsCEO = 'True' ;";
			var tempOut = SqlDataAccess.GetOneData<EmployeesModel>(sql);

			return tempOut[0];
		}

		public static List<EmployeesModel> GetManagers() {
			string sql = @"select * from dbo.Employees where IsCEO = 'True' or IsManager = 'True' ;";
			return SqlDataAccess.LoadData<EmployeesModel>(sql);
		}

		public static int DeleteEmployee(int id) {
			var data = new EmployeesModel {
				Id = id
			};
			string sql = @"Delete from dbo.Employees where Id = @id;";

			return SqlDataAccess.SaveData(sql, data);

		}

		public static int GetEmployeeNumForManager(int id) {
			var data = new EmployeesModel {
				Id = id
			};
			string sql = @"select Id from dbo.Employees where ManagerId = @id;";

				var employees =  SqlDataAccess.GetData<EmployeesModel>(sql, data);
			return employees.Count();
			
		}

		public static List<EmployeesModel> EditEmployee(int id) {
			var data = new EmployeesModel {
				Id = id
			};
			string sql = @"select* from dbo.Employees where Id = @Id;";

			var output = SqlDataAccess.GetData(sql, data);

			return output;
		}

		public static EmployeesModel GetManager(int id) {
			var data = new EmployeesModel {
				Id = id
			};
			string sql = @"select * from dbo.Employees where Id = @Id ;";
			var tempOut = SqlDataAccess.GetData<EmployeesModel>(sql, data);

			return tempOut[0];
		}

		public static int SaveEmployee(EmployeesModel model) {

			string sql = @"Update dbo.Employees set FirstName = @FirstName, LastName = @LastName, Salary = @Salary, IsCEO = @IsCEO, IsManager = @IsManager, 
							ManagerId = @ManagerId where Id = @Id;";
			return SqlDataAccess.SaveData(sql, model);
		}
	}
}
