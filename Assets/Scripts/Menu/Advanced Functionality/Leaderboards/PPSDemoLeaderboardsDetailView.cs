using UnityEngine;
using System.Collections;

using PlayPhone.MultiNet;
using PlayPhone.MultiNet.Providers;
using PlayPhone.MultiNet.Core;

public class PPSDemoLeaderboardsDetailView : PPSDemoViewAbstract 
{
  public PPSDemoLeaderboardsDetailView(MNWSInfoRequestLeaderboard.LeaderboardMode request)
  {
    viewName = "Leaderboards Detail";
    this.request = request;
    MNDirect.GetWSProvider().Send(new MNWSInfoRequestLeaderboard(request,MNWSInfoRequestLeaderboardCompleted));
  }
  
  public override void Draw()
  {
    scoreUpload = GUILayout.TextField(scoreUpload,25);
    try
    {
      scoreUploadLong = long.Parse(scoreUpload);
    }
    catch
    {
      scoreUploadLong = 0;
      GUILayout.Label("Set correct score uploading");
    }
        
    if (GUILayout.Button("Post Gamse score") && (scoreUploadLong != 0))
    {
      MNDirect.PostGameScore(scoreUploadLong);
      scoreUpload="";
            
      MNDirect.GetWSProvider().Send(new MNWSInfoRequestLeaderboard(request,MNWSInfoRequestLeaderboardCompleted));
    }
        
    GUILayout.Label("Username \t\t\t\t\t\t Score");        
    GUILayout.Label("");

    if (leaderboardListItems == null)
    {
      GUILayout.Label(PPSDemoCommonInfo.NoDataMessage);
    }
    else if (leaderboardListItems.Length == 0) {
      GUILayout.Label(PPSDemoCommonInfo.LeaderboardIsEmptyMessage);
    }
    else
    {
      for (int i = 0; i < leaderboardListItems.Length; i++)
      {
        GUILayout.Label(i + ". "+ leaderboardListItems[i].GetUserNickName() + "\t\t\t\t\t\t" + leaderboardListItems[i].GetOutHiScoreText());
      }
    }
  }
  
  public override void UserLoggedIn()
  {
    MNDirect.GetWSProvider().Send(new MNWSInfoRequestLeaderboard(request,MNWSInfoRequestLeaderboardCompleted));
  }
  
  public override void UserLoggedOut()
  {
    leaderboardListItems = null;
  }

  private void MNWSInfoRequestLeaderboardCompleted(MNWSInfoRequestLeaderboard.RequestResult requestResult) 
  {
    MNTools.DLog("MNWSInfoRequestCurrGameRoomListCompleted with result: " + requestResult.ToString());
    leaderboardListItems = (MNWSLeaderboardListItem[])requestResult.DataEntry;
  }

  protected long scoreUploadLong;
  protected string scoreUpload = "";
  protected MNWSLeaderboardListItem[] leaderboardListItems = null;
  protected MNWSInfoRequestLeaderboard.LeaderboardMode request = null;
}
