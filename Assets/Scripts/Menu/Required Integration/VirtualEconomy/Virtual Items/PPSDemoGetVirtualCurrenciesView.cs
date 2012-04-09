using UnityEngine;
using System.Collections;

using PlayPhone.MultiNet;
using PlayPhone.MultiNet.Providers;
using PlayPhone.MultiNet.Core;

public class PPSDemoGetVirtualCurrenciesView : PPSDemoViewAbstract 
{
  public PPSDemoGetVirtualCurrenciesView()
  {
    viewName = "Get Virtual Items";

    if (MNDirect.GetVItemsProvider().IsGameVItemsListNeedUpdate())
    {
      MNDirect.GetVItemsProvider().VItemsListUpdated += new MNVItemsProvider.VItemsListUpdatedEventHandler(OnvItemsListLoaded);
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

    if (vItemsListLoaded) 
    {
      if (gameVItemInfoArray == null) 
      {
        gameVItemInfoArray = MNDirect.GetVItemsProvider().GetGameVItemsList();
      }

      for (int index = 0; index < gameVItemInfoArray.Length; index++) 
      {
      //___\/___ for Currencies
        if (((gameVItemInfoArray[index].Model & (int)MNVItemsProvider.VITEM_IS_CURRENCY_MASK) != 0)) 
        {
          if (GUILayout.Button(gameVItemInfoArray[index].Name))
          {
            gameVItemInfo = gameVItemInfoArray[index];
            PPSDemoMain.stackView.Push(new PPSDemoVirtualItemsInfoView(gameVItemInfo));
          }
        }
      }
    }
    else 
    {
      GUILayout.Label(PPSDemoCommonInfo.InformationUpdatingMessage);
    }

  }

  public override void OnClose()
  {
    MNDirect.GetVItemsProvider().VItemsListUpdated -= new MNVItemsProvider.VItemsListUpdatedEventHandler(OnvItemsListLoaded);
  }

  private void OnvItemsListLoaded()
  {
    vItemsListLoaded = true;
  }

  protected bool vItemsListLoaded = false;
  protected MNVItemsProvider.GameVItemInfo[] gameVItemInfoArray = null;
  protected MNVItemsProvider.GameVItemInfo gameVItemInfo = null;
}
