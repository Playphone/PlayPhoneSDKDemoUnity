using UnityEngine;
using System.Collections;

using PlayPhone.MultiNet;
using PlayPhone.MultiNet.Providers;
using PlayPhone.MultiNet.Core;

public class PPSDemoUserInventoryListManageView : PPSDemoViewAbstract 
{
  public PPSDemoUserInventoryListManageView()
  {
    viewName = "User inventory list";

    if (MNDirect.GetVItemsProvider().IsGameVItemsListNeedUpdate())
    {
      MNDirect.GetVItemsProvider().VItemsListUpdated += new MNVItemsProvider.VItemsListUpdatedEventHandler(OnVItemsListUpdated);
      MNDirect.GetVItemsProvider().DoGameVItemsListUpdate();
      vItemsListLoaded = false;
    }
    else
    {
      vItemsListLoaded = true;
    }
  }
  
  public override void Draw()
  {
    if (PPSDemoInfoStorage.currentUserInfo != null)
   {
    if (vItemsListLoaded) 
    {
      GUILayout.Label("Amount to add/remove packs. Input with '-' to remove packs");
      if (PPSDemoInfoStorage.currentUserInfo != null)
      {
        itemsCountStr = GUILayout.TextField(itemsCountStr,10);  
      }
      else
      {
        GUILayout.TextField(PPSDemoCommonInfo.LoggedToManage,PPSDemoCommonInfo.LoggedToManage.Length);
      }
      try
      {
        itemsCountLong = long.Parse(itemsCountStr);
      }
      catch
      {
        GUILayout.Label("Input amount or incorrect amount to add/remove packs!");
        GUI.enabled = false;
      }
      
      if (gameVItemInfoArray == null) 
      {
        gameVItemInfoArray = MNDirect.GetVItemsProvider().GetGameVItemsList();
      }
      
      if (gameVItemInfoArray != null)
      {
        GUILayout.Label("Items:");      
        for (int index = 0;index < gameVItemInfoArray.Length;index++) 
        {
          if (!((gameVItemInfoArray[index].Model & (int)MNVItemsProvider.VITEM_IS_CURRENCY_MASK) != 0)) 
          {
            if (GUILayout.Button(gameVItemInfoArray[index].Name))
            {
              long clientTransactionId = MNDirect.GetVItemsProvider().GetNewClientTransactionId();
              MNDirect.GetVItemsProvider().ReqAddPlayerVItem(gameVItemInfoArray[index].Id,itemsCountLong,clientTransactionId);
            }
          }
        }
      
        GUILayout.Label("Coins:");      
        for (int index = 0;index < gameVItemInfoArray.Length;index++) 
        {
          if (((gameVItemInfoArray[index].Model & (int)MNVItemsProvider.VITEM_IS_CURRENCY_MASK) != 0)) 
          {
            if (GUILayout.Button(gameVItemInfoArray[index].Name))
            {
              long clientTransactionId = MNDirect.GetVItemsProvider().GetNewClientTransactionId();
              MNDirect.GetVItemsProvider().ReqAddPlayerVItem(gameVItemInfoArray[index].Id,itemsCountLong,clientTransactionId);
            }
          }
        }
      }
      else
      {
        GUILayout.Label(PPSDemoCommonInfo.InformationUpdatingMessage);
      }
    }
    else 
    {
      GUILayout.Label(PPSDemoCommonInfo.InformationUpdatingMessage);
    }
    
    GUI.enabled = true;
   }
   else
   {
     GUILayout.Label(PPSDemoCommonInfo.NotLoggedInMessage);
   }
  }

  public override void OnClose()
  {
    MNDirect.GetVItemsProvider().VItemsListUpdated -= new MNVItemsProvider.VItemsListUpdatedEventHandler(OnVItemsListUpdated);
  }

  private void OnVItemsListUpdated()
  {
    vItemsListLoaded = true;
  }

  protected bool vItemsListLoaded = false;
  protected MNVItemsProvider.GameVItemInfo[] gameVItemInfoArray = null;
  protected MNVItemsProvider.GameVItemInfo gameVItemInfo = null;

  protected long itemsCountLong;
  protected long clientTransactionId = -1;

  protected string itemsCountStr = "";
}
