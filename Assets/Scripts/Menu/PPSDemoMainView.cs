using UnityEngine;
using System;
using System.Collections;
using PlayPhone.MultiNet;

public class PPSDemoMainView : PPSDemoViewAbstract {
  
  public PPSDemoMainView()
  {
    viewName = "Main Menu";
  }
  
  public override void Draw()
  {
    GUILayout.Label("Required Integration");

    if (GUILayout.Button("Login User"))
    {
      PPSDemoMain.stackView.Push(new PPSDemoLoginUserView());
    }
    if (GUILayout.Button("Dasboard"))
    {
      PPSDemoMain.stackView.Push(new PPSDemoDasboardView());
    }
    if (GUILayout.Button("Virtual Economy"))
    {
      PPSDemoMain.stackView.Push(new PPSDemoVirtualEconomyView());
    }

    GUILayout.Label("Advanced Functionality");

    if (GUILayout.Button("Current User Info"))
    {
      PPSDemoMain.stackView.Push(new PPSDemoCurrentUserInfoView());
    }
    
    if (GUILayout.Button("Leaderbords"))
    {
      PPSDemoMain.stackView.Push(new PPSDemoLeaderboardsView());
    }
    
    if (GUILayout.Button("Achievements"))
    {
      PPSDemoMain.stackView.Push(new PPSDemoAchievementsView());
    }
    
    if (GUILayout.Button("Social Graph"))
    {
      PPSDemoMain.stackView.Push(new PPSDemoSocialGraphView());
    }
    
    if (GUILayout.Button("Dashboard Control"))
    {
      PPSDemoMain.stackView.Push(new PPSDemoDashboardControlView());
    }
    
    if (GUILayout.Button("Cloud Storage"))
    {
      PPSDemoMain.stackView.Push(new PPSDemoCloudStorageView());
    }
    
    if (GUILayout.Button("Game Settings"))
    {
      PPSDemoMain.stackView.Push(new PPSDemoGameSettingsView());
    }

    if (GUILayout.Button("Room cookies"))
    {
      PPSDemoMain.stackView.Push(new PPSDemoRoomCookiesView());
    }
    
    if (GUILayout.Button("Multiplayer Basics"))
    {
      PPSDemoMain.stackView.Push(new PPSDemoMultiplayerBasicsView());
    }

    if (GUILayout.Button("Server info"))
    {
      PPSDemoMain.stackView.Push(new PPSDemoServerInfoView());
    }

    GUILayout.Label("System Information");
    
    if (GUILayout.Button("Application info"))
    {
      PPSDemoMain.stackView.Push(new PPSDemoApplicationInfoView());
    }
  }
}
