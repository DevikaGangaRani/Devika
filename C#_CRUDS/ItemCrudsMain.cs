//  Items Main

using System;
using System.Collections.Generic;

class cItemCrudsMain
{
	public static string Databasename;
	public static void Main(String[] args)
	{
		cUi oUi = new cUi();
		Databasename = oUi.GetDatabasename();
		cItemCruds oItemCruds = new cItemCruds(Databasename);
		while (true)
		{
		oUi.ShowMenu();
		string ChoiceString = oUi.GetChoice();
		int Choice;
			if (int.TryParse(ChoiceString, out Choice))
			{
				if (Choice < 6 && Choice >= 0)
				{
					string Operation;
					switch(Choice)
					{
						case 0:
							Environment.Exit(0);
							break;
						case 1:
							Operation = "insertion";
							List<string> ItemDetails = oUi.GetItemDetails();
							int RowInserted = oItemCruds.InsertItem(ItemDetails);
							oUi.PrintStatusMessage(RowInserted, Operation);
							break;
						case 2:
							oItemCruds.PrintAllItems();
							break;
						case 3:
						Operation = "update";
						string itemIdToUpdate = oUi.GetItemIDToDoOperation(Operation);
						List<string> updatedItemDetails = oUi.GetUpdatedItemDetails();

						if (updatedItemDetails.Count >= 4)
						{
							double updatedPrice;
							int updatedStockQty;
							if (!double.TryParse(updatedItemDetails[1], out updatedPrice))
							{
								oUi.PrintInvalidMessage("price");
								break;
							}
							if (!int.TryParse(updatedItemDetails[2], out updatedStockQty))
							{
								oUi.PrintInvalidMessage("stock quantity");
								break;
							}
							int rowsUpdated = oItemCruds.UpdateItem(itemIdToUpdate, updatedItemDetails[0], updatedPrice, updatedStockQty, updatedItemDetails[3]);
							oUi.PrintStatusMessage(rowsUpdated, Operation);
						}
						else
						{
							Console.WriteLine("Insufficient item details provided.");
						}
						break;

						case 4:
                            Operation = "deletion";
							string itemIdToDelete = oUi.GetItemIDToDoOperation(Operation);
							int rowsDeleted = oItemCruds.DeleteItem(itemIdToDelete);
							oUi.PrintStatusMessage(rowsDeleted, Operation);
							break;

						case 5:
                            Operation = "search";
                            string itemIdToSearch = oUi.GetItemIDToDoOperation(Operation);
                            oItemCruds.SearchItem(itemIdToSearch);
                            break;

                        default:
                            Console.WriteLine("Invalid choice.");
                            break;
					}
				}
			}
			else
			{
				Console.WriteLine("You entered invalid choice. Please try again.");
			}

			Console.WriteLine("Press Enter to continue...");
       		Console.ReadLine();
		}
	}
}