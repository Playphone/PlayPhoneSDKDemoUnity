using UnityEngine;
using System.Collections;

using PlayPhone.MultiNet;
using PlayPhone.MultiNet.Providers;
using PlayPhone.MultiNet.Core;

public class PPSDemoShopCategoriesListView : PPSDemoViewAbstract 
{
  public PPSDemoShopCategoriesListView()
  {
    viewName = "Shop Categories List";
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
      if (vShopCategoryArray == null) 
      {
        vShopCategoryArray = MNDirect.GetVShopProvider().GetVShopCategoryList();
      }
      if (vShopCategoryArray!= null)
      {
        for (int index = 0;index < vShopCategoryArray.Length;index++) 
        {
          GUILayout.Toggle(false,vShopCategoryArray[index].Name);
        }
      }
    }
    else 
    {
      GUILayout.Label(PPSDemoCommonInfo.InformationUpdatingMessage);
    }
  }

  protected bool vShopInfoUpdated = false;
  protected MNVShopProvider.VShopCategoryInfo[] vShopCategoryArray = null;
}
