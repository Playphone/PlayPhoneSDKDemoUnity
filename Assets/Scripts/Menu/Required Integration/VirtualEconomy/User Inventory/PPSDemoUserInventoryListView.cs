using UnityEngine;
using System.Collections;

using PlayPhone.MultiNet;
using PlayPhone.MultiNet.Providers;
using PlayPhone.MultiNet.Core;

public class PPSDemoUserInventoryListView : PPSDemoViewAbstract 
{
  public PPSDemoUserInventoryListView()
  {
    viewName = "User inventory list";
  }
  
  public override void Draw()
  {
    if (PPSDemoInfoStorage.currentUserInfo != null)
   {
    if (!vItemsListUpdated) 
    {
      if (MNDirect.GetVItemsProvider().IsGameVItemsListNeedUpdate() && isGameVItemsListNeedUpdate) 
      {
        MNDirect.GetVItemsProvider().VItemsListUpdated += new MNVItemsProvider.VItemsListUpdatedEventHandler(OnGameVItemsListUpdated);
        MNDirect.GetVItemsProvider().DoGameVItemsListUpdate();
        isGameVItemsListNeedUpdate = false;
      }
      vItemsListUpdated = true;
      playerVItemInfoArrayNeedUpdate = true;
    }
    
    if (vItemsListUpdated) 
    {
      if (playerVItemInfoArrayNeedUpdate) 
      {
        playerVItemInfoArray = MNDirect.GetVItemsProvider().GetPlayerVItemList();
        playerVItemInfoArrayNeedUpdate = false;
        gameVItemInfoArray = new MNVItemsProvider.GameVItemInfo[playerVItemInfoArray.Length];
        
        for (int index = 0; index < playerVItemInfoArray.Length; index++) 
        {
          gameVItemInfoArray[index] = MNDirect.GetVItemsProvider().FindGameVItemById(playerVItemInfoArray[index].Id);
        }
      }
      if (playerVItemInfoArray != null)
      {
        GUILayout.Label("Total " + playerVItemInfoArray.Length);
        GUILayout.Label("Items: ");
        for (int index = 0;index < playerVItemInfoArray.Length;index++) 
        {
          gameVItemInfo = gameVItemInfoArray[index];
          if (!((gameVItemInfo.Model & (int)MNVItemsProvider.VITEM_IS_CURRENCY_MASK) != 0)) 
          {
            GUILayout.Label(" " + gameVItemInfoArray[index].Name + " "+ playerVItemInfoArray[index].Count);
          }
        }
            
        GUILayout.Label("Coins: ");
        for (int index = 0;index < playerVItemInfoArray.Length;index++) 
        {
          gameVItemInfo = gameVItemInfoArray[index];
          if (((gameVItemInfo.Model & (int)MNVItemsProvider.VITEM_IS_CURRENCY_MASK) != 0)) 
          {
            GUILayout.Label(" " + gameVItemInfoArray[index].Name + " "+ playerVItemInfoArray[index].Count);
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
     GUILayout.Label(PPSDemoCommonInfo.NotLoggedInMessage);
   }
  }

  public override void UserLoggedIn()
  {
    vItemsListUpdated = false;  
  }
  public override void UserLoggedOut()
  {
    playerVItemInfoArray = null;
  }
  
  public override void OnClose ()
  {
    MNDirect.GetVItemsProvider().VItemsListUpdated -= new MNVItemsProvider.VItemsListUpdatedEventHandler(OnGameVItemsListUpdated);
  }

  private void OnGameVItemsListUpdated()
  {
    vItemsListUpdated = true;
  }

  protected bool vItemsListUpdated = false;
  protected bool playerVItemInfoArrayNeedUpdate = false;
  protected bool isGameVItemsListNeedUpdate = true;
  protected MNVItemsProvider.PlayerVItemInfo[] playerVItemInfoArray = null;
  protected MNVItemsProvider.GameVItemInfo[] gameVItemInfoArray = null;
  protected MNVItemsProvider.GameVItemInfo gameVItemInfo = null;
}
