using UnityEngine;
using System.Collections;

using PlayPhone.MultiNet;
using PlayPhone.MultiNet.Providers;
using PlayPhone.MultiNet.Core;

public class PPSDemoLoginUserView : PPSDemoViewAbstract 
{
  public PPSDemoLoginUserView()
  {
    viewName = "Login User";
  }
  
  public override void Draw()
  {
    if (PPSDemoInfoStorage.currentUserInfo == null) 
    {
      GUILayout.Label("User Id: [null]");
      GUILayout.Label("User Name: [null]");
      GUILayout.Label(PPSDemoCommonInfo.NotLoggedInMessage);
    }
    else 
    {
      GUILayout.Label("User Id: " + PPSDemoInfoStorage.currentUserInfo.UserId);
      GUILayout.Label("User Name: " + PPSDemoInfoStorage.currentUserInfo.UserName);
      GUILayout.Label("User is logged in");
    }

    GUI.enabled = (PPSDemoInfoStorage.currentUserInfo == null);
    if (GUILayout.Button("Login"))
    {
      MNDirect.ExecAppCommand("jumpToUserHome","");
      MNDirectUIHelper.ShowDashboard();
    }

    GUI.enabled = (PPSDemoInfoStorage.currentUserInfo != null);
    if (GUILayout.Button("Logout"))
    {
      MNDirect.ExecAppCommand("jumpToUserProfile","");
      MNDirectUIHelper.ShowDashboard();
    }
    GUI.enabled = true;
  }
}
