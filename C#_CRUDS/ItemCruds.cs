// Item CRUDS Program

using System;
using System.Data.Common;
using MySql.Data.MySqlClient;
using System.Collections.Generic;
class cItemCruds
{
	public MySqlConnection connection;
	public cItemCruds(String pDatabaseName)
	{
		string ConnectionString = "server=138.68.140.83;user=Rani;password=Rani@123;database=" + pDatabaseName +";";
		connection = new MySqlConnection(ConnectionString);
		connection.Open();
		Console.WriteLine("Connected to the database \n");
	}
	public int InsertItem(List<string> pItemDetails)
	{
		if (pItemDetails.Count != 5)
		{
			Console.WriteLine("Insufficient item details provided.");
			return 0;
		}

		string query = "INSERT INTO tblItem (ItemId, ItemDescription, UnitPrice, StockQty, SupplierId) " +
					"VALUES (@ItemId, @ItemDescription, @UnitPrice, @StockQty, @SupplierId)";
		MySqlCommand Command = new MySqlCommand(query, connection);

		// Add parameters to the command
		Command.Parameters.AddWithValue("@ItemId", pItemDetails[0]);
		Command.Parameters.AddWithValue("@ItemDescription", pItemDetails[1]);
		Command.Parameters.AddWithValue("@UnitPrice", double.Parse(pItemDetails[2]));
		Command.Parameters.AddWithValue("@StockQty", int.Parse(pItemDetails[3]));
		Command.Parameters.AddWithValue("@SupplierId", pItemDetails[4]);
		return Command.ExecuteNonQuery();
	}
	public string[][] ShowAll()
	{
		List<string[]> itemList = new List<string[]>();

		string query = "SELECT * FROM tblItem";
		MySqlCommand command = new MySqlCommand(query, connection);
		MySqlDataReader reader = command.ExecuteReader();

		while (reader.Read())
		{
			string[] itemDetails = new string[5];
			itemDetails[0] = reader.GetString(0); // Item ID
			itemDetails[1] = reader.GetString(1); // Description
			itemDetails[2] = reader.GetDouble(2).ToString(); // Price
			itemDetails[3] = reader.IsDBNull(4) ? "NULL" : reader.GetInt32(4).ToString(); // Stock Qty
			itemDetails[4] = reader.IsDBNull(3) ? "NULL" : reader.GetString(3); // Supplier ID

			itemList.Add(itemDetails);
		}
		reader.Close();
		string[][] result = itemList.ToArray();
		return result;
	}
	public void PrintAllItems()
	{
		string[][] allItems = ShowAll();
		Console.WriteLine("Items in the Database:");
		Console.WriteLine(new string('-', 71));
		Console.WriteLine("| Item ID    | Description \t\t| Price | Stock Qty | Supplier ID |");
		Console.WriteLine(new string('-', 71));
		foreach (string[] item in allItems)
		{
			Console.WriteLine($"| {item[0],-10} | {item[1],-20} | {item[2],-5} | {item[3],-9} | {item[4],-11} |");
		}
		Console.WriteLine(new string('-', 71));
	}
	public int UpdateItem(string itemId, string newDescription, double newPrice, int newStockQty, string newSupplierId)
	{
		string query = "UPDATE tblItem SET ItemDescription=@NewDescription, UnitPrice=@NewPrice, StockQty=@NewStockQty, SupplierId=@NewSupplierId WHERE ItemId=@ItemId";
		MySqlCommand Command = new MySqlCommand(query, connection);
		// Add parameters to the command
		Command.Parameters.AddWithValue("@NewDescription", newDescription);
		Command.Parameters.AddWithValue("@NewPrice", newPrice);
		Command.Parameters.AddWithValue("@NewStockQty", newStockQty);
		Command.Parameters.AddWithValue("@NewSupplierId", newSupplierId);
		Command.Parameters.AddWithValue("@ItemId", itemId);
		return Command.ExecuteNonQuery();
	}
	public int DeleteItem(string itemId)
	{
		string query = "DELETE FROM tblItem WHERE ItemId=@ItemId";
		MySqlCommand Command = new MySqlCommand(query, connection);
		Command.Parameters.AddWithValue("@ItemId", itemId);
		int rowsDeleted = Command.ExecuteNonQuery();
		return rowsDeleted;
	}
	public void SearchItem(string itemId)
	{
		string query = "SELECT * FROM tblItem WHERE ItemId=@ItemId";
		MySqlCommand command = new MySqlCommand(query, connection);
		command.Parameters.AddWithValue("@ItemId", itemId);
		using (MySqlDataReader reader = command.ExecuteReader())
		{
			if (reader.Read())
			{
				string description = reader.GetString(1);
				double price = reader.GetDouble(2);
				string stockQty = reader.IsDBNull(4) ? "Not Available" : reader.GetInt32(4).ToString();
				string supplierId = reader.GetString(3);
				Console.WriteLine("+------------+-------------------------------+------------+--------------+-------------+");
				Console.WriteLine("| Item ID    | Description                   | Price      | Stock Qty    | Supplier ID |");
				Console.WriteLine("+------------+-------------------------------+------------+--------------+-------------+");
				Console.WriteLine($"| {itemId,-10} | {description,-30} | {price,10:0.00} | {stockQty,12} | {supplierId,-11} |");
				Console.WriteLine("+------------+-------------------------------+------------+--------------+-------------+");
			}
			else
			{
				Console.WriteLine("Item not found.");
			}
		}
	}
}

