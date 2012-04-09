using UnityEngine;
using System.Collections;

using PlayPhone.MultiNet;
using PlayPhone.MultiNet.Providers;
using PlayPhone.MultiNet.Core;

public class PPSDemoShopPacksListView : PPSDemoViewAbstract 
{
  public PPSDemoShopPacksListView()
  {
    viewName = "Shop Packs List";
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

      for (int index = 0;index < vShopPackArray.Length;index++) 
      {
        if (GUILayout.Button("ID:"+vShopPackArray[index].Id+" "+vShopPackArray[index].Name+" "+(float)vShopPackArray[index].PriceValue/100+" $"))
        {
           vShopPackItem = vShopPackArray[index];
         
           PPSDemoMain.stackView.Push(new PPSDemoShopPacksDetailView(vShopPackItem));   
        }
      }
    }
    else 
    {
      GUILayout.Label(PPSDemoCommonInfo.InformationUpdatingMessage);
    }
  }

  protected bool vShopInfoUpdated = false;
  protected MNVShopProvider.VShopPackInfo[] vShopPackArray = null;
  protected MNVShopProvider.VShopPackInfo vShopPackItem = null;
}
