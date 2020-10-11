using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataLibrary.Models.DataAccess;

namespace DataLibrary.Models.Logic
{
	public class LibraryItemProcessor
	{
		public static List<LibraryItemModel> LoadLibraryItem() {
			string sql = @"select * from dbo.LibraryItem;";

			return SqlDataAccess.LoadData<LibraryItemModel>(sql);
		}


		public static int CreateLibraryItem(LibraryItemModel model) {

			string sql = @"insert into dbo.LibraryItem (CategoryId, Title, Author, Pages, RunTimeMinutes, IsBorrowable, Type)
							values (@CategoryId, @Title, @Author, @Pages, @RunTimeMinutes, @IsBorrowable, @Type);";

			return SqlDataAccess.SaveData(sql, model);
		}

		public static int DeleteItem(int id) {
			var data = new LibraryItemModel {
				Id = id
			};
			string sql = @"Delete from dbo.LibraryItem where Id = @id;";

			return SqlDataAccess.SaveData(sql, data);

		}
public static LibraryItemModel EditLibraryItem(int id) {
			var data = new LibraryItemModel {
				Id = id
			};
			string sql = @"select* from dbo.LibraryItem where Id = @Id;";

			var output = SqlDataAccess.GetData(sql, data);
			var outModel = new LibraryItemModel() { Id = output[0].Id,  CategoryId = output[0].CategoryId, Title = output[0].Title, Author = output[0].Author, Pages = output[0].Pages, RunTimeMinutes = output[0].RunTimeMinutes,
													IsBorrowable = output[0].IsBorrowable, Borrower = output[0].Borrower, BorrowDate = output[0].BorrowDate , Type = output[0].Type };

			return outModel;
		}
		

		public static int SaveLibraryItem(LibraryItemModel model) {

			string sql = @"Update dbo.LibraryItem set Author = @Author, BorrowDate = @BorrowDate, Borrower = @Borrower, CategoryId = @CategoryId, IsBorrowable = @IsBorrowable, 
							Pages = @Pages, RunTimeMinutes = @RunTimeMinutes, Title = @Title, Type = @Type where Id = @Id;";
			return SqlDataAccess.SaveData(sql, model);
		}
	}
}
