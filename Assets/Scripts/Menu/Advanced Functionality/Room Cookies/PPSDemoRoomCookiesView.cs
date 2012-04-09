using UnityEngine;
using System.Collections;

public class PPSDemoRoomCookiesView : PPSDemoViewAbstract 
{
  public PPSDemoRoomCookiesView()
  {
    viewName = "Room Cookies";
  }
  
  public override void Draw()
  {
    if (PPSDemoInfoStorage.currentUserInfo == null)
    {
      GUILayout.Label(PPSDemoCommonInfo.NotLoggedInMessage);
    }
    else 
    {
      if (GUILayout.Button("Current game room"))
      {
        PPSDemoMain.stackView.Push(new PPSDemoCurrentGameRoomCookiesView());
      }
      
      if (GUILayout.Button("Any room"))
      {
        PPSDemoMain.stackView.Push(new PPSDemoAnyRoomCookiesView());
      }
    }
  }
}
