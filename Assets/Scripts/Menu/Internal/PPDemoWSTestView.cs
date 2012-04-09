using UnityEngine;
using System;

using PlayPhone.MultiNet;
using PlayPhone.MultiNet.Providers;
using PlayPhone.MultiNet.Core;

public class PPDemoWSTestView : PPSDemoViewAbstract
{
  public PPDemoWSTestView ()
  {
    consoleVisible = PPSDemoMain.consoleIsVisible();
    PPSDemoMain.consoleHide();
    viewName = "Web Services API tests";
  }

  public override void Draw()
  {
    if (GUILayout.Button("MNWSInfoRequestAnyGame"))
    {
      MNDirect.GetWSProvider().Send(new MNWSInfoRequestAnyGame(PPSDemoCommonInfo.gameId,PPSInfoRequestAnyGameCompleted));
      wsResult = "";
    }

    if (GUILayout.Button("MNWSInfoRequestAnyUser")) 
    {
      MNDirect.GetWSProvider().Send(new MNWSInfoRequestAnyUser(41275/*xtest*/,PPSInfoRequestAnyUserCompleted));
      wsResult = "";
    }

    if (GUILayout.Button("MNWSInfoRequestAnyUserGameCookies")) 
    {
      long[] userIds = {41275/*xtest*/};
      int[] cookies = {0,1,2,3,4};

      MNDirect.GetWSProvider().Send(new MNWSInfoRequestAnyUserGameCookies(userIds,cookies,MNWSInfoRequestAnyUserGameCookiesCompleted));
      wsResult = "";
    }

    if (GUILayout.Button("MNWSInfoRequestCurrentUserInfo")) 
    {
      MNDirect.GetWSProvider().Send(new MNWSInfoRequestCurrentUserInfo(MNWSInfoRequestCurrentUserInfoCompleted));
      wsResult = "";
    }

    if (GUILayout.Button("MNWSInfoRequestCurrGameRoomList")) 
    {
      MNDirect.GetWSProvider().Send(new MNWSInfoRequestCurrGameRoomList(MNWSInfoRequestCurrGameRoomListCompleted));
      wsResult = "";
    }

    if (GUILayout.Button("MNWSInfoRequestCurrGameRoomUserList")) 
    {
      MNDirect.GetWSProvider().Send(new MNWSInfoRequestCurrGameRoomUserList(113,MNWSInfoRequestCurrGameRoomUserListCompleted));
      wsResult = "";
    }

    if (GUILayout.Button("MNWSInfoRequestCurrUserBuddyList")) 
    {
      MNDirect.GetWSProvider().Send(new MNWSInfoRequestCurrUserBuddyList(MNWSInfoRequestCurrUserBuddyListCompleted));
      wsResult = "";
    }

    if (GUILayout.Button("MNWSInfoRequestCurrUserSubscriptionStatus")) 
    {
      MNDirect.GetWSProvider().Send(new MNWSInfoRequestCurrUserSubscriptionStatus(MNWSInfoRequestCurrUserSubscriptionStatusCompleted));
      wsResult = "";
    }

    if (GUILayout.Button("MNWSInfoRequestSessionSignedClientToken")) 
    {
      MNDirect.GetWSProvider().Send(new MNWSInfoRequestSessionSignedClientToken("TEST",MNWSInfoRequestSessionSignedClientTokenCompleted));
      wsResult = "";
    }

    if (GUILayout.Button("MNWSInfoRequestSystemGameNetStats")) 
    {
      MNDirect.GetWSProvider().Send(new MNWSInfoRequestSystemGameNetStats(MNWSInfoRequestSystemGameNetStatsCompleted));
      wsResult = "";
    }

    if (GUILayout.Button("LeaderboardModeCurrentUser")) 
    {
      MNDirect.GetWSProvider().Send(new MNWSInfoRequestLeaderboard(new MNWSInfoRequestLeaderboard.LeaderboardModeCurrentUser(MNWSInfoRequestLeaderboard.LEADERBOARD_SCOPE_GLOBAL,MNWSInfoRequestLeaderboard.LEADERBOARD_PERIOD_ALL_TIME),MNWSInfoRequestLeaderboardCompleted));
      wsResult = "";
    }

    if (GUILayout.Button("LeaderboardModeCurrUserAnyGameLocal")) 
    {
      MNDirect.GetWSProvider().Send(new MNWSInfoRequestLeaderboard(new MNWSInfoRequestLeaderboard.LeaderboardModeCurrUserAnyGameLocal(10900,0,MNWSInfoRequestLeaderboard.LEADERBOARD_PERIOD_ALL_TIME),MNWSInfoRequestLeaderboardCompleted));
      wsResult = "";
    }

    if (GUILayout.Button("LeaderboardModeAnyUserAnyGameGlobal")) 
    {
      MNDirect.GetWSProvider().Send(new MNWSInfoRequestLeaderboard(new MNWSInfoRequestLeaderboard.LeaderboardModeAnyUserAnyGameGlobal(777,10900,0,MNWSInfoRequestLeaderboard.LEADERBOARD_PERIOD_THIS_MONTH),MNWSInfoRequestLeaderboardCompleted));
      wsResult = "";
    }

    if (GUILayout.Button("LeaderboardModeAnyGameGlobal")) 
    {
      MNDirect.GetWSProvider().Send(new MNWSInfoRequestLeaderboard(new MNWSInfoRequestLeaderboard.LeaderboardModeAnyGameGlobal(10900,0,MNWSInfoRequestLeaderboard.LEADERBOARD_PERIOD_THIS_WEEK),MNWSInfoRequestLeaderboardCompleted));
      wsResult = "";
    }

    GUILayout.Label("\nWS Results:");

    GUILayout.Label(wsResult);
  }

  public override void OnClose ()
  {
    if (consoleVisible) {
      PPSDemoMain.consoleShow();
    }
  }

  public string wsResult = "";

  public void PPSInfoRequestAnyGameCompleted(MNWSInfoRequestAnyGame.RequestResult requestResult) 
  {
    MNTools.DLog("PPSInfoRequestAnyGameCompleted with result: " + requestResult.ToString());
    wsResult = requestResult.ToString();
  }

  public void PPSInfoRequestAnyUserCompleted(MNWSInfoRequestAnyUser.RequestResult requestResult) 
  {
    MNTools.DLog("PPSInfoRequestAnyUserCompleted with result: " + requestResult.ToString());
    wsResult = requestResult.ToString();
  }

  public void MNWSInfoRequestAnyUserGameCookiesCompleted(MNWSInfoRequestAnyUserGameCookies.RequestResult requestResult) 
  {
    MNTools.DLog("MNWSInfoRequestAnyUserGameCookiesCompleted with result: " + requestResult.ToString());
    wsResult = requestResult.ToString();
  }

  public void MNWSInfoRequestCurrentUserInfoCompleted(MNWSInfoRequestCurrentUserInfo.RequestResult requestResult) 
  {
    MNTools.DLog("MNWSInfoRequestCurrentUserInfoCompleted with result: " + requestResult.ToString());
    wsResult = requestResult.ToString();
  }

  public void MNWSInfoRequestCurrGameRoomListCompleted(MNWSInfoRequestCurrGameRoomList.RequestResult requestResult) 
  {
    MNTools.DLog("MNWSInfoRequestCurrGameRoomListCompleted with result: " + requestResult.ToString());
    wsResult = requestResult.ToString();
  }

  public void MNWSInfoRequestCurrGameRoomUserListCompleted(MNWSInfoRequestCurrGameRoomUserList.RequestResult requestResult) 
  {
    MNTools.DLog("MNWSInfoRequestCurrGameRoomUserListCompleted with result: " + requestResult.ToString());
    wsResult = requestResult.ToString();
  }

  public void MNWSInfoRequestCurrUserBuddyListCompleted(MNWSInfoRequestCurrUserBuddyList.RequestResult requestResult) 
  {
    MNTools.DLog("MNWSInfoRequestCurrUserBuddyListCompleted with result: " + requestResult.ToString());
    wsResult = requestResult.ToString();
  }

  public void MNWSInfoRequestCurrUserSubscriptionStatusCompleted(MNWSInfoRequestCurrUserSubscriptionStatus.RequestResult requestResult) 
  {
    MNTools.DLog("MNWSInfoRequestCurrUserSubscriptionStatusCompleted with result: " + requestResult.ToString());
    wsResult = requestResult.ToString();
  }

  public void MNWSInfoRequestSessionSignedClientTokenCompleted(MNWSInfoRequestSessionSignedClientToken.RequestResult requestResult) 
  {
    MNTools.DLog("MNWSInfoRequestSessionSignedClientTokenCompleted with result: " + requestResult.ToString());
    wsResult = requestResult.ToString();
  }

  public void MNWSInfoRequestSystemGameNetStatsCompleted(MNWSInfoRequestSystemGameNetStats.RequestResult requestResult) 
  {
    MNTools.DLog("MNWSInfoRequestSystemGameNetStatsCompleted with result: " + requestResult.ToString());
    wsResult = requestResult.ToString();
  }

  public void MNWSInfoRequestLeaderboardCompleted(MNWSInfoRequestLeaderboard.RequestResult requestResult) 
  {
    MNTools.DLog("MNWSInfoRequestLeaderboardCompleted with result: " + requestResult.ToString());
    wsResult = requestResult.ToString();
  }

  private bool consoleVisible = false;
}

