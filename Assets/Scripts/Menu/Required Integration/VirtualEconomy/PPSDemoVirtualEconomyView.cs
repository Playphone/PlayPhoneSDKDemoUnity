using UnityEngine;
using System.Collections;

public class PPSDemoVirtualEconomyView : PPSDemoViewAbstract 
{
  public PPSDemoVirtualEconomyView()
  {
    viewName = "Virtual Economy";
  }
  
  public override void Draw()
  {
    if (GUILayout.Button("Virtual Items\n These are the items used in game"))
    {
      PPSDemoMain.stackView.Push(new PPSDemoVirtualItemsView());
    }
    if (GUILayout.Button("PlayPhone Store\n User can purchase these items"))
    {
      PPSDemoMain.stackView.Push(new PPSDemoPlayPhoneStoreView());
    }
    if (GUILayout.Button("User Inventory\n Inventory can be tracked per user"))
    {
      PPSDemoMain.stackView.Push(new PPSDemoUserInventoryView());
    }
  }
}
