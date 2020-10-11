using System;
using System.Collections.Generic;
using System.ComponentModel.Design;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataLibrary.Models.DataAccess;

namespace DataLibrary.Models.Logic
{
	public class CategoryProcessor
	{
		public static List<CategoryModel> LoadCategories() {
			string sql = @"select CategoryName, Id from dbo.Category;";

			return SqlDataAccess.LoadData<CategoryModel>(sql);

		}

		public static int CreateCategory(string categoryName) {
			CategoryModel data = new CategoryModel {
				CategoryName = categoryName
			};
			string sql = @"insert into dbo.Category (CategoryName)
							values (@CategoryName);";

			return SqlDataAccess.SaveData(sql, data);
		}


		public static int DeleteCategory(int id) {
			CategoryModel data = new CategoryModel {
				Id = id
			};
			string sql = @"Delete from dbo.Category where Id = @id;";

			return SqlDataAccess.SaveData(sql, data);

		}

		public static CategoryModel EditCategory(int id) {
			CategoryModel data = new CategoryModel {
				Id = id
			};
			string sql = @"select* from dbo.Category where Id = @Id;";

			var output =  SqlDataAccess.GetData(sql, data);
			var outModel = new CategoryModel() {CategoryName = output[0].CategoryName , Id = output[0].Id};

			return outModel;
		}

		public static int SaveCategory(CategoryModel model) {

			CategoryModel data = new CategoryModel {
				CategoryName = model.CategoryName,
				Id = model.Id
			};
			string sql = @"Update dbo.Category set CategoryName = @CategoryName where Id = @Id;";

			return SqlDataAccess.SaveData(sql, data);
		}

		public static bool checkReferences(int id) {
			CategoryModel data = new CategoryModel {
				Id = id
			};
			string sql = @"select* from dbo.LibraryItem where CategoryId = @Id;";
			var result = SqlDataAccess.GetData(sql, data);
			var output = true;
			if (result.Count > 0) {
				output = false;
			}

			return output;
		}

		public static CategoryModel GetCategoryByName(string name) {
			CategoryModel data = new CategoryModel {
				CategoryName = name
			};
			string sql = @"select* from dbo.Category where CategoryName = @CategoryName;";

			var output = SqlDataAccess.GetData(sql, data);
			var outModel = new CategoryModel() { CategoryName = output[0].CategoryName, Id = output[0].Id };

			return outModel;
		}

	}
}
