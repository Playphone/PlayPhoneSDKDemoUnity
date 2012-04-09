using UnityEngine;
using System.Collections;

using PlayPhone.MultiNet;
using PlayPhone.MultiNet.Providers;
using PlayPhone.MultiNet.Core;

public class PPSDemoUnlockAchievementsView : PPSDemoViewAbstract 
{
  public PPSDemoUnlockAchievementsView()
  {
    viewName = "Unlock Achievements";
  }
  
  public override void Draw()
  {
    GUILayout.Label("Unlock");
    if (PPSDemoInfoStorage.currentUserInfo != null)
    {
      achievementToUnlockStr = GUILayout.TextField(achievementToUnlockStr,25);
    }
    else
    {
      GUILayout.TextField(PPSDemoCommonInfo.LoggedToManage,PPSDemoCommonInfo.LoggedToManage);
    }
    try
    {
      achievementToUnlockInt = int.Parse(achievementToUnlockStr);
    }
    catch
    {
      GUILayout.Label("Input Achievement Id or Incorrect Achievement Id!");
      GUI.enabled = false;
    }
    
    if(GUILayout.Button("Unlock achievement"))
    {
      MNDirect.GetAchievementsProvider().UnlockPlayerAchievement(achievementToUnlockInt);
    }
    
    GUI.enabled = true; 
  }

  protected string achievementToUnlockStr = "";
  protected int achievementToUnlockInt;
}
