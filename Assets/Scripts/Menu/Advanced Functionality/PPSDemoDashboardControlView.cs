using UnityEngine;
using System.Collections;

using PlayPhone.MultiNet;
using PlayPhone.MultiNet.Providers;
using PlayPhone.MultiNet.Core;

public class PPSDemoDashboardControlView : PPSDemoViewAbstract 
{
  public PPSDemoDashboardControlView()
  {
    viewName = "Dashboard Control";
  }
  
  public override void Draw()
  {
    if (GUILayout.Button("Leaderboards"))
    {
      MNDirect.ExecAppCommand("jumpToLeaderboard","");
      MNDirectUIHelper.ShowDashboard();
    }
        
    if (GUILayout.Button("Friend list"))
    {
      MNDirect.ExecAppCommand("jumpToBuddyList","");
      MNDirectUIHelper.ShowDashboard();
    }
        
    if (GUILayout.Button("User Profle"))
    {
      MNDirect.ExecAppCommand("jumpToUserProfile","");
      MNDirectUIHelper.ShowDashboard();
    }
        
    if (GUILayout.Button("User Home"))
    {
      MNDirect.ExecAppCommand("jumpToUserHome","");
      MNDirectUIHelper.ShowDashboard();
    }
        
    if (GUILayout.Button("Achievements"))
    {
      MNDirect.ExecAppCommand("jumpToAchievements","");
      MNDirectUIHelper.ShowDashboard();
    }
        
    if (GUILayout.Button("Game Info"))
    {
      MNDirect.ExecAppCommand("jumpToGameInfo","");
      MNDirectUIHelper.ShowDashboard();
    }
        
    if (GUILayout.Button("Add Friends"))
    {
      MNDirect.ExecAppCommand("jumpToAddFriends","");
      MNDirectUIHelper.ShowDashboard();
    }
        
    if (GUILayout.Button("PlayCredits Shop"))
    {
      MNDirect.ExecAppCommand("jumpToGameShop","");
      MNDirectUIHelper.ShowDashboard();
    }

    if (GUILayout.Button("Redeem"))
    {
      MNDirect.ExecAppCommand("_redeem","");
      MNDirectUIHelper.ShowDashboard();
    }
        
    if (GUILayout.Button("Shop Catalog"))
    {
      MNDirect.ExecAppCommand("jumpToGameShop","");
      MNDirectUIHelper.ShowDashboard();
    }
  }
}
