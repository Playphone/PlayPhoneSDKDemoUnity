using UnityEngine;
using System.Collections;

public class PPSDemoApplicationInfoView : PPSDemoViewAbstract {

  public PPSDemoApplicationInfoView()
  {
    viewName = "Application Info";
  }
  
  public override void Draw()
  {
    GUILayout.Label("PPS Demo Unity SDK");
  }
}
