using UnityEngine;
using System.Collections;

public class PPSDemoPlayPhoneStoreView : PPSDemoViewAbstract 
{
  public PPSDemoPlayPhoneStoreView()
  {
    viewName = "Play Phone Store";
  }
  
  public override void Draw()
  {
    if (GUILayout.Button("Shop Categories List"))
    {
      PPSDemoMain.stackView.Push(new PPSDemoShopCategoriesListView());
    }

    if (GUILayout.Button("Shop Packs List"))
    {
      PPSDemoMain.stackView.Push(new PPSDemoShopPacksListView());
    }

    if (GUILayout.Button("Buy Shop Pack"))
    {
      PPSDemoMain.stackView.Push(new PPSDemoBuyShopPackView());
    }
  }
}
