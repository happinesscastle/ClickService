// Decompiled with JetBrains decompiler
// Type: ClickServerService.CouponClass
// Assembly: ClickServerService, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 6BDFD2F8-7BA8-4B8A-8EC1-401DFA893333
// Assembly location: C:\Users\Win10\Desktop\ClickServerService.exe

using System;
using System.Data;
using System.Data.SqlClient;

namespace ClickServerService
{
  internal class CouponClass
  {
    private MainClass objmain = new MainClass();

    public DataTable Coupon_Get()
    {
      DataTable dataTable = new DataTable();
      try
      {
        using (SqlConnection connection = new SqlConnection(this.objmain.DBPath()))
        {
          connection.Open();
          SqlCommand selectCommand = new SqlCommand("Coupon_GetAll", connection);
          selectCommand.CommandType = CommandType.StoredProcedure;
          new SqlDataAdapter(selectCommand).Fill(dataTable);
        }
        return dataTable;
      }
      catch (Exception ex)
      {
        this.objmain.ErrorLog(ex);
        return dataTable;
      }
    }

    public int Coupon_insert(
      int ID_GameCenter,
      string Title,
      string Barcode,
      string Serial,
      bool IsPercent,
      int Amount,
      bool AmountIsLock,
      bool ForItem,
      bool ForAll,
      string Users_GroupsIds,
      bool EnableDateAlways,
      DateTime EnableDateFrom,
      DateTime EnableDateTo,
      bool EnableTimeAlways,
      string EnableTimeFrom,
      string EnableTimeTo,
      bool EnableEveryDay,
      string EnableDays,
      bool ForAllProduct,
      bool ForAllStockProduct,
      bool ForAllCardProduct,
      bool ForSelectedProduct,
      string SelectedProductList)
    {
      DataTable dataTable = new DataTable();
      try
      {
        using (SqlConnection connection = new SqlConnection(this.objmain.DBPath()))
        {
          connection.Open();
          SqlCommand sqlCommand = new SqlCommand("Coupon_Insert", connection);
          sqlCommand.Parameters.AddWithValue("@ID", (object) (this.objmain.Max_Tbl("Coupon", "ID") + 1));
          sqlCommand.Parameters.AddWithValue("@ID_GameCenter", (object) ID_GameCenter);
          sqlCommand.Parameters.AddWithValue("@Title", (object) Title);
          sqlCommand.Parameters.AddWithValue("@Barcode", (object) Barcode);
          sqlCommand.Parameters.AddWithValue("@Serial", (object) Serial);
          sqlCommand.Parameters.AddWithValue("@IsPercent", (object) IsPercent);
          sqlCommand.Parameters.AddWithValue("@Amount", (object) Amount);
          sqlCommand.Parameters.AddWithValue("@AmountIsLock", (object) AmountIsLock);
          sqlCommand.Parameters.AddWithValue("@ForItem", (object) ForItem);
          sqlCommand.Parameters.AddWithValue("@ForAll", (object) ForAll);
          sqlCommand.Parameters.AddWithValue("@Users_GroupsIds", (object) Users_GroupsIds);
          sqlCommand.Parameters.AddWithValue("@EnableDateAlways", (object) EnableDateAlways);
          sqlCommand.Parameters.AddWithValue("@EnableDateFrom", (object) EnableDateFrom);
          sqlCommand.Parameters.AddWithValue("@EnableDateTo", (object) EnableDateTo);
          sqlCommand.Parameters.AddWithValue("@EnableTimeAlways", (object) EnableTimeAlways);
          sqlCommand.Parameters.AddWithValue("@EnableTimeFrom", (object) EnableTimeFrom);
          sqlCommand.Parameters.AddWithValue("@EnableTimeTo", (object) EnableTimeTo);
          sqlCommand.Parameters.AddWithValue("@EnableEveryDay", (object) EnableEveryDay);
          sqlCommand.Parameters.AddWithValue("@EnableDays", (object) EnableDays);
          sqlCommand.Parameters.AddWithValue("@ForAllProduct", (object) ForAllProduct);
          sqlCommand.Parameters.AddWithValue("@ForAllStockProduct", (object) ForAllStockProduct);
          sqlCommand.Parameters.AddWithValue("@ForAllCardProduct", (object) ForAllCardProduct);
          sqlCommand.Parameters.AddWithValue("@ForSelectedProduct", (object) ForSelectedProduct);
          sqlCommand.Parameters.AddWithValue("@SelectedProductList", (object) SelectedProductList);
          sqlCommand.Parameters.AddWithValue("@CreateDate", (object) DateTime.Now);
          sqlCommand.Parameters.AddWithValue("@IsDeleted", (object) 0);
          sqlCommand.CommandType = CommandType.StoredProcedure;
          sqlCommand.ExecuteNonQuery();
        }
        return 1;
      }
      catch (Exception ex)
      {
        this.objmain.ErrorLog(ex);
        return -1;
      }
    }

    public int Coupon_Update(
      int ID,
      int ID_GameCenter,
      string Title,
      string Barcode,
      string Serial,
      bool IsPercent,
      int Amount,
      bool AmountIsLock,
      bool ForItem,
      bool ForAll,
      string Users_GroupsIds,
      bool EnableDateAlways,
      DateTime EnableDateFrom,
      DateTime EnableDateTo,
      bool EnableTimeAlways,
      string EnableTimeFrom,
      string EnableTimeTo,
      bool EnableEveryDay,
      string EnableDays,
      bool ForAllProduct,
      bool ForAllStockProduct,
      bool ForAllCardProduct,
      bool ForSelectedProduct,
      string SelectedProductList)
    {
      DataTable dataTable = new DataTable();
      try
      {
        using (SqlConnection connection = new SqlConnection(this.objmain.DBPath()))
        {
          connection.Open();
          SqlCommand sqlCommand = new SqlCommand(nameof (Coupon_Update), connection);
          sqlCommand.Parameters.AddWithValue("@ID", (object) ID);
          sqlCommand.Parameters.AddWithValue("@ID_GameCenter", (object) ID_GameCenter);
          sqlCommand.Parameters.AddWithValue("@Title", (object) Title);
          sqlCommand.Parameters.AddWithValue("@Barcode", (object) Barcode);
          sqlCommand.Parameters.AddWithValue("@Serial", (object) Serial);
          sqlCommand.Parameters.AddWithValue("@IsPercent", (object) IsPercent);
          sqlCommand.Parameters.AddWithValue("@Amount", (object) Amount);
          sqlCommand.Parameters.AddWithValue("@AmountIsLock", (object) AmountIsLock);
          sqlCommand.Parameters.AddWithValue("@ForItem", (object) ForItem);
          sqlCommand.Parameters.AddWithValue("@ForAll", (object) ForAll);
          sqlCommand.Parameters.AddWithValue("@Users_GroupsIds", (object) Users_GroupsIds);
          sqlCommand.Parameters.AddWithValue("@EnableDateAlways", (object) EnableDateAlways);
          sqlCommand.Parameters.AddWithValue("@EnableDateFrom", (object) EnableDateFrom);
          sqlCommand.Parameters.AddWithValue("@EnableDateTo", (object) EnableDateTo);
          sqlCommand.Parameters.AddWithValue("@EnableTimeAlways", (object) EnableTimeAlways);
          sqlCommand.Parameters.AddWithValue("@EnableTimeFrom", (object) EnableTimeFrom);
          sqlCommand.Parameters.AddWithValue("@EnableTimeTo", (object) EnableTimeTo);
          sqlCommand.Parameters.AddWithValue("@EnableEveryDay", (object) EnableEveryDay);
          sqlCommand.Parameters.AddWithValue("@EnableDays", (object) EnableDays);
          sqlCommand.Parameters.AddWithValue("@ForAllProduct", (object) ForAllProduct);
          sqlCommand.Parameters.AddWithValue("@ForAllStockProduct", (object) ForAllStockProduct);
          sqlCommand.Parameters.AddWithValue("@ForAllCardProduct", (object) ForAllCardProduct);
          sqlCommand.Parameters.AddWithValue("@ForSelectedProduct", (object) ForSelectedProduct);
          sqlCommand.Parameters.AddWithValue("@SelectedProductList", (object) SelectedProductList);
          sqlCommand.Parameters.AddWithValue("@CreateDate", (object) DateTime.Now);
          sqlCommand.CommandType = CommandType.StoredProcedure;
          sqlCommand.ExecuteNonQuery();
        }
        return 1;
      }
      catch (Exception ex)
      {
        this.objmain.ErrorLog(ex);
        return -1;
      }
    }

    public DataTable Coupon_Get(int ID, int ID_GameCenter)
    {
      DataTable dataTable = new DataTable();
      try
      {
        using (SqlConnection connection = new SqlConnection(this.objmain.DBPath()))
        {
          connection.Open();
          SqlCommand selectCommand = new SqlCommand(nameof (Coupon_Get), connection);
          selectCommand.Parameters.AddWithValue("@ID", (object) ID);
          selectCommand.Parameters.AddWithValue("@ID_GameCenter", (object) ID_GameCenter);
          selectCommand.CommandType = CommandType.StoredProcedure;
          new SqlDataAdapter(selectCommand).Fill(dataTable);
        }
        return dataTable;
      }
      catch (Exception ex)
      {
        this.objmain.ErrorLog(ex);
        return dataTable;
      }
    }

    public int Coupon_Delete(int ID, int ID_GameCenter)
    {
      DataTable dataTable = new DataTable();
      try
      {
        using (SqlConnection connection = new SqlConnection(this.objmain.DBPath()))
        {
          connection.Open();
          SqlCommand sqlCommand = new SqlCommand(nameof (Coupon_Delete), connection);
          sqlCommand.CommandType = CommandType.StoredProcedure;
          sqlCommand.Parameters.AddWithValue("@ID", (object) ID);
          sqlCommand.Parameters.AddWithValue("@ID_GameCenter", (object) ID_GameCenter);
          sqlCommand.ExecuteNonQuery();
        }
        return 1;
      }
      catch (Exception ex)
      {
        this.objmain.ErrorLog(ex);
        return -1;
      }
    }
  }
}
