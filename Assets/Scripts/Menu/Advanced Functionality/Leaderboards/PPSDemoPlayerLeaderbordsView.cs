using UnityEngine;
using System.Collections;

using PlayPhone.MultiNet;
using PlayPhone.MultiNet.Providers;
using PlayPhone.MultiNet.Core;

public class PPSDemoPlayerLeaderbordsView : PPSDemoViewAbstract 
{
  public PPSDemoPlayerLeaderbordsView()
  {
    viewName = "Player Leaderbords";
  }
  
  public override void Draw()
  {
    GUILayout.Label("Player:");
    if (GUILayout.Toggle(LeaderboardsDetailShow_PlayerCurrent,"Current"))
    {
      LeaderboardsDetailShow_PlayerCurrent = true;
      LeaderboardsDetailShow_PlayerCustom = false;
    };
    if (GUILayout.Toggle(LeaderboardsDetailShow_PlayerCustom,"Custom"))
    {
          LeaderboardsDetailShow_PlayerCurrent = false;
          LeaderboardsDetailShow_PlayerCustom = true;    
    };
      
    if (LeaderboardsDetailShow_PlayerCustom)
    {
      GUILayout.Label("Player id:");
      strBuffer = LeaderboardsDetailShow_PlayerId.ToString();
      if (PPSDemoInfoStorage.currentUserInfo != null)
      {
        strBuffer = GUILayout.TextField(strBuffer,25);
      }
      else
      {
        GUILayout.TextField(PPSDemoCommonInfo.LoggedToManage,PPSDemoCommonInfo.LoggedToManage);
      }
      LeaderboardsDetailShow_PlayerId = int.Parse(strBuffer);     
    }
    else
    {
      LeaderboardsDetailShow_PlayerId = PPSDemoCommonInfo.playerId;
      GUILayout.Label("Player id:"+ LeaderboardsDetailShow_PlayerId);    
    }
        
    GUILayout.Label("Gamset:");
    if (GUILayout.Toggle(LeaderboardsDetailShow_GameCurrent,"Current"))
    {
      LeaderboardsDetailShow_GameCurrent = true;
      LeaderboardsDetailShow_GameCustom = false;
    };
    if (GUILayout.Toggle(LeaderboardsDetailShow_GameCustom,"Custom"))
    {
      LeaderboardsDetailShow_GameCurrent = false;
      LeaderboardsDetailShow_GameCustom = true;    
    };
    if (LeaderboardsDetailShow_GameCustom)
    {
      GUILayout.Label("Game id:");
      strBuffer = LeaderboardsDetailShow_GameId.ToString();
      if (PPSDemoInfoStorage.currentUserInfo != null)
      {
        strBuffer = GUILayout.TextField(strBuffer,25);
      }
      else
      {
        GUILayout.TextField(PPSDemoCommonInfo.LoggedToManage,PPSDemoCommonInfo.LoggedToManage);
      }
      LeaderboardsDetailShow_GameId = int.Parse(strBuffer);        
    }
    else
    {
      gameId = 10900;
      GUILayout.Label("Game id: 10900");    
    }
        
    GUILayout.Label("Game set:");
    strBuffer = LeaderboardsDetailShow_GameSet.ToString();
    if (PPSDemoInfoStorage.currentUserInfo != null)
    {
      strBuffer = GUILayout.TextField(strBuffer,25);
    }
    else
    {
      GUILayout.TextField(PPSDemoCommonInfo.LoggedToManage,PPSDemoCommonInfo.LoggedToManage);
    }
    LeaderboardsDetailShow_GameSet = int.Parse(strBuffer);
    
    if (LeaderboardsDetailShow_GameSet < 0)
    {
      LeaderboardsDetailShow_GameSet = 0;
    }
        
    GUILayout.Label("Period:");
        
    if (GUILayout.Toggle(LeaderboardsDetailShow_PeriodAllTime,"All time"))
    {
      LeaderboardsDetailShow_PeriodAllTime = true;
      LeaderboardsDetailShow_PeriodWeek = false;
      LeaderboardsDetailShow_PeriodMonth = false;
      LeaderboardsDetailShow_TimePeriod = MNWSInfoRequestLeaderboard.LEADERBOARD_PERIOD_ALL_TIME;    
    }

    if (GUILayout.Toggle(LeaderboardsDetailShow_PeriodWeek,"Week"))
    {
      LeaderboardsDetailShow_PeriodAllTime = false;
      LeaderboardsDetailShow_PeriodWeek = true;
      LeaderboardsDetailShow_PeriodMonth = false;
      LeaderboardsDetailShow_TimePeriod = MNWSInfoRequestLeaderboard.LEADERBOARD_PERIOD_THIS_WEEK;
    }

    if (GUILayout.Toggle(LeaderboardsDetailShow_PeriodMonth,"Month"))
    {
      LeaderboardsDetailShow_PeriodAllTime = false;
      LeaderboardsDetailShow_PeriodWeek = false;
      LeaderboardsDetailShow_PeriodMonth = true;
      LeaderboardsDetailShow_TimePeriod = MNWSInfoRequestLeaderboard.LEADERBOARD_PERIOD_THIS_MONTH;
    }
        
    if (GUILayout.Button("Send request"))
    {
      MNWSInfoRequestLeaderboard.LeaderboardMode request = null;
      if (LeaderboardsDetailShow_GameCurrent && LeaderboardsDetailShow_PlayerCurrent)
      {
         request = new MNWSInfoRequestLeaderboard.LeaderboardModeCurrUserAnyGameLocal(gameId,
                                                                                      LeaderboardsDetailShow_GameSet,
                                                                                      LeaderboardsDetailShow_TimePeriod);
      }
      else
      {
        request = new MNWSInfoRequestLeaderboard.LeaderboardModeAnyUserAnyGameGlobal(LeaderboardsDetailShow_PlayerId,
                                                                                     gameId,
                                                                                     LeaderboardsDetailShow_GameSet,
                                                                                     LeaderboardsDetailShow_TimePeriod);
      }
      PPSDemoMain.stackView.Push(new PPSDemoLeaderboardsDetailView(request));
    }
  }

  protected bool LeaderboardsDetailShow_GameCurrent = true;
  protected bool LeaderboardsDetailShow_GameCustom = false;

  protected bool LeaderboardsDetailShow_PlayerCurrent = true;
  protected bool LeaderboardsDetailShow_PlayerCustom = false;

  protected bool LeaderboardsDetailShow_PeriodAllTime = true;
  protected bool LeaderboardsDetailShow_PeriodWeek = false;
  protected bool LeaderboardsDetailShow_PeriodMonth = false;
  protected int LeaderboardsDetailShow_TimePeriod = -1;
  protected int LeaderboardsDetailShow_GameId = PPSDemoCommonInfo.gameId;
  protected int gameId;

  protected int LeaderboardsDetailShow_PlayerId = 2103407;
  protected int LeaderboardsDetailShow_GameSet = 0;
  protected string strBuffer = "";
}
