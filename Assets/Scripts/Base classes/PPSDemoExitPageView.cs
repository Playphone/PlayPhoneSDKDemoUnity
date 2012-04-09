using UnityEngine;
using System.Collections;

public class PPSDemoExitPageView : PPSDemoViewAbstract
{
  public PPSDemoExitPageView()
  {
    viewName = "Really exit?";
  }

  public override void Draw()
  {
    if (GUILayout.Button("Yes, exit"))
    {
      Application.Quit();
    }
    if (GUILayout.Button("No"))
    {
      PPSDemoMain.stackView.Push(new PPSDemoMainView());
    }
  }
}
