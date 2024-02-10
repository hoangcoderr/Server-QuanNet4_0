using System.Data.SqlClient;
using System.Collections.Generic;
public class Item
{
  public int Id { get; set; }
  public string itemName { get; set; }
  public int amount { get; set; }
  public int quantity { get; set; }
  public string description { get; set; }
  public static List<Item> items = new List<Item>();
}

