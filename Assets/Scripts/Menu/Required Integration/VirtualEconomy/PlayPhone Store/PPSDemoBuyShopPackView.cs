#define USE_JS

using UnityEngine;
using System.Collections;

using PlayPhone.MultiNet;
using PlayPhone.MultiNet.Providers;
using PlayPhone.MultiNet.Core;

public class PPSDemoBuyShopPackView : PPSDemoViewAbstract 
{
  #if USE_JS
  public PPSDemoBuyShopPackView()
  {
    viewName = "Buy Shop Pack";
    PPSDemoCommonInfo.GetJSGameObject().SendMessage("PPSDemoJsBuyShopPackConstruct");
  }

  public override void Draw()
  {
    PPSDemoCommonInfo.GetJSGameObject().SendMessage("PPSDemoJsBuyShopPackDraw");
  }

  public override void OnClose ()
  {
    PPSDemoCommonInfo.GetJSGameObject().SendMessage("PPSDemoJsBuyShopPackClose");
  }

  public override void UserLoggedIn()
  {
    PPSDemoCommonInfo.GetJSGameObject().SendMessage("PPSDemoJsBuyShopPackUserLoggedIn");
  }

  public override void UserLoggedOut()
  {
    PPSDemoCommonInfo.GetJSGameObject().SendMessage("PPSDemoJsBuyShopPackUserLoggedOut");
  }
  #else
  public PPSDemoBuyShopPackView()
  {
    viewName = "Buy Shop Pack";
    //MNDirect.GetVShopProvider().ShowDashboard += new MNVShopProvider.ShowDashboardEventHandler(OnShowDashboard);
    //MNDirect.GetVShopProvider().HideDashboard += new MNVShopProvider.HideDashboardEventHandler(OnHideDashboard);
    MNDirect.GetVShopProvider().CheckoutVShopPackSuccess += new MNVShopProvider.CheckoutVShopPackSuccessEventHandler(OnCheckoutVShopPackSuccess);
    MNDirect.GetVShopProvider().CheckoutVShopPackFail += new MNVShopProvider.CheckoutVShopPackFailEventHandler(OnCheckoutVShopPackFail);
  }

  public override void Draw()
  {    
    if (!vShopInfoUpdated) 
    {
      if (MNDirect.GetVShopProvider().IsVShopInfoNeedUpdate()) 
      {
        MNDirect.GetVShopProvider().DoVShopInfoUpdate();
      }
      else 
      {
        vShopInfoUpdated = true;
      }
    }

    if (vShopInfoUpdated) 
    {
      if (vShopPackArray == null) 
      {
        vShopPackArray = MNDirect.GetVShopProvider().GetVShopPackList();
      }

      if (PPSDemoInfoStorage.currentUserInfo == null)
      {
        GUILayout.Label(PPSDemoCommonInfo.NotLoggedInMessage);
      }

      for (int index = 0;index < vShopPackArray.Length;index++) 
      {
        if ((vShopPackArray[index].PriceValue == 0) || (PPSDemoInfoStorage.currentUserInfo == null) || waitBuyRequest)
        {
          GUI.enabled= false;
        }
        if (GUILayout.Button("Buy ID:" + vShopPackArray[index].Id + " " + vShopPackArray[index].Name + " " + (float)vShopPackArray[index].PriceValue/100 + " $"))
        {
           int[] packs   = { vShopPackArray[index].Id };
           int[] amounts = { 1 };
           
           waitBuyRequest = true;
           
           MNDirect.GetVShopProvider().ExecCheckoutVShopPacks(packs,amounts,MNDirect.GetVItemsProvider().GetNewClientTransactionId());
        }
        GUI.enabled= true;
      }
      
      if (waitBuyRequest)
      {
        GUILayout.Label("Please wait! Processing purchase's request.");
      }
      
      if (buySuccess)
      {
        GUILayout.Label("success buy!");
        waitBuyRequest = false; 
      }
      if (buyFail)
      {
        GUILayout.Label("ErrorCode = " + vShopPackFailInfo.ErrorCode);
        GUILayout.Label("ErrorMessage = " + vShopPackFailInfo.ErrorMessage);
        GUILayout.Label("ClientTransactionId = " + vShopPackFailInfo.ClientTransactionId);
        
        waitBuyRequest = false; 
      }
    }
    else 
    {
      GUILayout.Label(PPSDemoCommonInfo.InformationUpdatingMessage);
    }
  }

  public override void OnClose ()
  {
    MNDirect.GetVShopProvider().CheckoutVShopPackSuccess -= new MNVShopProvider.CheckoutVShopPackSuccessEventHandler(OnCheckoutVShopPackSuccess);
    MNDirect.GetVShopProvider().CheckoutVShopPackFail -= new MNVShopProvider.CheckoutVShopPackFailEventHandler(OnCheckoutVShopPackFail);
    //MNDirect.GetVShopProvider().ShowDashboard -= new MNVShopProvider.ShowDashboardEventHandler(OnShowDashboard);
    //MNDirect.GetVShopProvider().HideDashboard -= new MNVShopProvider.HideDashboardEventHandler(OnHideDashboard);
  }
  
  private void OnCheckoutVShopPackSuccess(MNVShopProvider.CheckoutVShopPackSuccessInfo result) 
  {
    MNTools.DLog("MNDirectVShopProvider_OnCheckoutVShopPackSuccess");
    buySuccess = true;
  }

  private void OnCheckoutVShopPackFail(MNVShopProvider.CheckoutVShopPackFailInfo result) 
  {
    MNTools.DLog("MNDirectVShopProvider_OnCheckoutVShopPackFail");
    buyFail = true;
    vShopPackFailInfo = result;
  }
  /*
  private void OnShowDashboard() 
  {
    MNTools.DLog("MNDirectVShopProvider_OnShowDashboard");
    MNDirectUIHelper.ShowDashboard();
  }

  private void OnHideDashboard() 
  {
    MNTools.DLog("MNDirectVShopProvider_OnHideDashboard");
    MNDirectUIHelper.HideDashboard();
  }
  */
  protected bool buySuccess = false;
  protected bool buyFail = false;
  protected bool waitBuyRequest = false;
  protected bool vShopInfoUpdated = false;
  protected MNVShopProvider.CheckoutVShopPackFailInfo vShopPackFailInfo = null;
  protected MNVShopProvider.VShopPackInfo[] vShopPackArray = null;
  #endif
}
