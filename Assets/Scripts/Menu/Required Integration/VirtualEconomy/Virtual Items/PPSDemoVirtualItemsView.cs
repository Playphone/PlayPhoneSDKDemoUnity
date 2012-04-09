using UnityEngine;
using System.Collections;

public class PPSDemoVirtualItemsView : PPSDemoViewAbstract 
{
  public PPSDemoVirtualItemsView()
  {
    viewName = "Virtual Items";
  }
  
  public override void Draw()
  {
    if (GUILayout.Button("Get Virtual Items"))
    {
      PPSDemoMain.stackView.Push(new PPSDemoGetVirtualItemsView());
    }

    if (GUILayout.Button("Get Virtual Currencies"))
    {
      PPSDemoMain.stackView.Push(new PPSDemoGetVirtualCurrenciesView());
    }
  }
}
