using UnityEngine;
using System.Collections;

public class PPSDemoUserInventoryView : PPSDemoViewAbstract 
{
  public PPSDemoUserInventoryView()
  {
    viewName = "User Inventory";
  }
  
  public override void Draw()
  {
    if (GUILayout.Button("Inventory list"))
    {
      PPSDemoMain.stackView.Push(new PPSDemoUserInventoryListView());
    }

    if (GUILayout.Button("Manage inventory"))
    {
      PPSDemoMain.stackView.Push(new PPSDemoUserInventoryListManageView());
    }
  }
}
