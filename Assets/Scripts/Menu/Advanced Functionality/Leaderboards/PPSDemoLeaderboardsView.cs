using UnityEngine;
using System.Collections;

using PlayPhone.MultiNet;
using PlayPhone.MultiNet.Providers;
using PlayPhone.MultiNet.Core;

public class PPSDemoLeaderboardsView : PPSDemoViewAbstract 
{
  public PPSDemoLeaderboardsView()
  {
    viewName = "Leaderboards";
  }
  
  public override void Draw()
  {
    if (PPSDemoInfoStorage.currentUserInfo == null)
      {
        GUILayout.Label(PPSDemoCommonInfo.NotLoggedInMessage);
        GUI.enabled = false;
      }
      else 
      {
        GUI.enabled = true;
      }
      
      GUILayout.Label("Score Posting");
        
      if (GUILayout.Button("Default"))
      {
        MNDirect.SetDefaultGameSetId(DEFAULT_GameSetId);
        MNWSInfoRequestLeaderboard.LeaderboardMode request = new MNWSInfoRequestLeaderboard.LeaderboardModeCurrentUser(MNWSInfoRequestLeaderboard.LEADERBOARD_SCOPE_LOCAL,
                                                                                                                       MNWSInfoRequestLeaderboard.LEADERBOARD_PERIOD_ALL_TIME);
        PPSDemoMain.stackView.Push(new PPSDemoLeaderboardsDetailView(request));
      }
        
      if (GUILayout.Button("Simple"))
      {
        MNDirect.SetDefaultGameSetId(SIMPLE_GameSetId);
        MNWSInfoRequestLeaderboard.LeaderboardMode request = new MNWSInfoRequestLeaderboard.LeaderboardModeCurrentUser(MNWSInfoRequestLeaderboard.LEADERBOARD_SCOPE_LOCAL,
                                                                                                                       MNWSInfoRequestLeaderboard.LEADERBOARD_PERIOD_ALL_TIME);
        PPSDemoMain.stackView.Push(new PPSDemoLeaderboardsDetailView(request));   
      }
        
      if (GUILayout.Button("Advanced"))
      {
        MNDirect.SetDefaultGameSetId(ADVANCED_GameSetId);
        MNWSInfoRequestLeaderboard.LeaderboardMode request = new MNWSInfoRequestLeaderboard.LeaderboardModeCurrentUser(MNWSInfoRequestLeaderboard.LEADERBOARD_SCOPE_LOCAL,
                                                                                                                       MNWSInfoRequestLeaderboard.LEADERBOARD_PERIOD_ALL_TIME);
        PPSDemoMain.stackView.Push(new PPSDemoLeaderboardsDetailView(request));
      }
        
      GUILayout.Label("Information extraction");
      if (GUILayout.Button("Game leaderboard"))
      {
        PPSDemoMain.stackView.Push(new PPSDemoGameLeaderbordsView());
      }
      if (GUILayout.Button("Player leaderboard"))
      {
        PPSDemoMain.stackView.Push(new PPSDemoPlayerLeaderbordsView());
      }
  }

  protected int DEFAULT_GameSetId = 0;
  protected int SIMPLE_GameSetId = 1;
  protected int ADVANCED_GameSetId = 2;
}
