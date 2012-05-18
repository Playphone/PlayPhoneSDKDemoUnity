#pragma strict

private var buySuccess : boolean = false;
private var buyFail : boolean = false;
private var waitBuyRequest : boolean = false;
private var vShopInfoUpdated : boolean = false;
private var vShopPackFailInfo : PlayPhone.MultiNet.Providers.MNVShopProvider.CheckoutVShopPackFailInfo = null;
private var vShopPackArray : PlayPhone.MultiNet.Providers.MNVShopProvider.VShopPackInfo[] = null;
private var userIsLoggedIn : boolean = false;


private var NotLoggedInMessage         = "User is not logged in";
private var InformationUpdatingMessage = "Information updating...";


function PPSDemoJsBuyShopPackConstruct() {
  PlayPhone.MultiNet.MNDirect.GetVShopProvider().add_CheckoutVShopPackSuccess(PPSDemoJsOnCheckoutVShopPackSuccess);
  PlayPhone.MultiNet.MNDirect.GetVShopProvider().add_CheckoutVShopPackFail(PPSDemoJsOnCheckoutVShopPackFail);

  userIsLoggedIn = PlayPhone.MultiNet.MNDirect.IsUserLoggedIn();
}

function PPSDemoJsBuyShopPackDraw() {
  if (!vShopInfoUpdated)
  {
    if (PlayPhone.MultiNet.MNDirect.GetVShopProvider().IsVShopInfoNeedUpdate())
    {
      PlayPhone.MultiNet.MNDirect.GetVShopProvider().DoVShopInfoUpdate();
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
      vShopPackArray = PlayPhone.MultiNet.MNDirect.GetVShopProvider().GetVShopPackList();
    }

    if (!userIsLoggedIn)
    {
      GUILayout.Label(NotLoggedInMessage);
    }

    var index : int = 0;
    for (index = 0;index < vShopPackArray.Length;index++)
    {
      if ((vShopPackArray[index].PriceValue == 0) || (!userIsLoggedIn) || waitBuyRequest)
      {
        GUI.enabled= false;
      }

      var buttonName = "Buy ID:" + vShopPackArray[index].Id + " " + vShopPackArray[index].Name + " " + vShopPackArray[index].PriceValue/100.0 + " $";

      if (GUILayout.Button(buttonName))
      {
         var packs = new int[1];
         var amounts = new int[1];
         packs[0] = vShopPackArray[index].Id;
         amounts[0] = 1;

         waitBuyRequest = true;

         PlayPhone.MultiNet.MNDirect.GetVShopProvider().ExecCheckoutVShopPacks(packs,amounts,PlayPhone.MultiNet.MNDirect.GetVItemsProvider().GetNewClientTransactionId());
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
    GUILayout.Label(InformationUpdatingMessage);
  }
}

function PPSDemoJsBuyShopPackClose() {
  PlayPhone.MultiNet.MNDirect.GetVShopProvider().remove_CheckoutVShopPackSuccess(PPSDemoJsOnCheckoutVShopPackSuccess);
  PlayPhone.MultiNet.MNDirect.GetVShopProvider().remove_CheckoutVShopPackFail(PPSDemoJsOnCheckoutVShopPackFail);
}

function PPSDemoJsBuyShopPackUserLoggedIn() {
  userIsLoggedIn = true;
}

function PPSDemoJsBuyShopPackUserLoggedOut() {
  userIsLoggedIn = false;
}

function PPSDemoJsOnCheckoutVShopPackSuccess(result : PlayPhone.MultiNet.Providers.MNVShopProvider.CheckoutVShopPackSuccessInfo) {
  buySuccess = true;
}

function PPSDemoJsOnCheckoutVShopPackFail(result : PlayPhone.MultiNet.Providers.MNVShopProvider.CheckoutVShopPackFailInfo) {
  buyFail = true;
  vShopPackFailInfo = result;
}
