using UnityEngine;
using System.Collections;

using PlayPhone.MultiNet;
using PlayPhone.MultiNet.Providers;
using PlayPhone.MultiNet.Core;

public class PPSDemoShopPacksDetailView : PPSDemoViewAbstract 
{
  public PPSDemoShopPacksDetailView(MNVShopProvider.VShopPackInfo vShopPackItem)
  {
    viewName = "Shop Packs Detail";
    this.vShopPackItem = vShopPackItem;
  }
  
  public override void Draw()
  {
    if (!vShopPacksDetailUpdated) 
    {
      vShopCategoryInfo = MNDirect.GetVShopProvider().FindVShopCategoryById(vShopPackItem.CategoryId);
      vShopPacksDetailUpdated = true; 
      shopPacksDetailToShow = MNDirect.GetVItemsProvider().FindGameVItemById(vShopPackItem.Delivery[0].VItemId);
    }
    
    if (vShopPackItem != null)
    {
      GUILayout.Label("Pack ID: " + vShopPackItem.Id);
      GUILayout.Label("Name: " + vShopPackItem.Name);
      GUILayout.Label("Category: " + vShopCategoryInfo.Name);
            
      GUILayout.Label("Items in pack: ");
      
      GUILayout.Label("Item name: " + shopPacksDetailToShow.Name);
      GUILayout.Label("Quantity: " + vShopPackItem.Delivery[0].Amount);
      GUILayout.Label("Price: $ " + (float)vShopPackItem.PriceValue/100);
      GUILayout.Label("");
      GUILayout.Label("Description: ");
      GUILayout.Label(vShopPackItem.Description);    
      
      GUILayout.Toggle((vShopPackItem.Model & 1) != 0,"Is Hidden");
      GUILayout.Toggle((vShopPackItem.Model & 2) != 0,"Hold Sales");
            
      GUILayout.Label("Application Parametres");
      GUI.enabled = false;
      GUILayout.TextArea(vShopPackItem.AppParams);
      GUI.enabled = true;
    }
    else
    {
      GUILayout.Label(PPSDemoCommonInfo.InformationUpdatingMessage);
    }
  }

  protected bool vShopPacksDetailUpdated = false;
  protected MNVShopProvider.VShopCategoryInfo vShopCategoryInfo = null;
  protected MNVItemsProvider.GameVItemInfo shopPacksDetailToShow = null;
  protected MNVShopProvider.VShopPackInfo vShopPackItem = null;
}
