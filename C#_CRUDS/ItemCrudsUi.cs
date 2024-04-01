//  Item CRUDS Ui

using System;
using System.Collections.Generic;

class cUi
{
	public string GetDatabasename()
	{
		Console.Write("Enter your database name: ");
		string Databasename = Console.ReadLine();
		return Databasename;
	}

	public void ShowMenu()
	{
		Console.WriteLine("Item Management");
		Console.WriteLine("-----------------------------");
		Console.WriteLine("0. Exit");
		Console.WriteLine("1. Insert Item");
		Console.WriteLine("2. Show all Items");
		Console.WriteLine("3. Update Item");
		Console.WriteLine("4. Delete Item");
		Console.WriteLine("5. Search Item");
		Console.WriteLine("-----------------------------");

	}


	public string GetChoice()
	{
		Console.Write("Enter your choice: ");
		string Choice = Console.ReadLine();
		return Choice;
	}
	public List <string> GetItemDetails()
	{
		List <string> ItemDetails = new List <string>();
		Console.Write("Enter Item ID: ");
		string ItemId = Console.ReadLine();
		ItemDetails.Add(ItemId);

		Console.Write("Enter item description: ");
		string ItemDescription = Console.ReadLine();
		ItemDetails.Add(ItemDescription);

		Console.Write("Enter item price: ");
		string UnitPrice = Console.ReadLine();
		ItemDetails.Add(UnitPrice);

		Console.Write("Enter stock Qty: ");
		string StockQty = Console.ReadLine();
		ItemDetails.Add(StockQty);

		Console.Write("Enter supplier Id: ");
		string SupplierId = Console.ReadLine();
		ItemDetails.Add(SupplierId);

		return ItemDetails;
	}

	public List<string> GetUpdatedItemDetails()
    {
        List<string> updatedItemDetails = new List<string>();

        Console.Write("Enter new item description: ");
        updatedItemDetails.Add(Console.ReadLine());

        Console.Write("Enter new item price: ");
        string updatedPriceInput = Console.ReadLine();
        if (!double.TryParse(updatedPriceInput, out double updatedPrice))
        {
            Console.WriteLine("Invalid price format. Please enter a valid number.");
            return null;
        }
        updatedItemDetails.Add(updatedPrice.ToString());

        Console.Write("Enter new stock Qty: ");
        string updatedStockQtyInput = Console.ReadLine();
        if (!int.TryParse(updatedStockQtyInput, out int updatedStockQty))
        {
            Console.WriteLine("Invalid stock quantity format. Please enter a valid integer.");
            return null;
        }
        updatedItemDetails.Add(updatedStockQty.ToString());

        Console.Write("Enter new supplier Id: ");
        updatedItemDetails.Add(Console.ReadLine());

        return updatedItemDetails;
    }
	public void PrintStatusMessage(int pResponseCode, string pOperation)
	{
		Console.Write("Item details " + pOperation + " is");
		if (pResponseCode == 1)
		{
			Console.Write(" successful.");
		}
		else
		{
			Console.Write(" failed.");
		}
		Console.Write("\n");
	}

	public void PrintInvalidMessage(string pFieldName)
	{
		Console.WriteLine("Invalid input for field " + pFieldName + " .Please enter valid number.\n");
	}

	public string GetItemIDToDoOperation(string operation)
    {
        Console.WriteLine($"Enter the Item ID to {operation}: ");
        return Console.ReadLine();
    }
}
