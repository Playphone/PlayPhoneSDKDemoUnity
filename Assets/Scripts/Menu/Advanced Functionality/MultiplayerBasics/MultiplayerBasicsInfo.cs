using PlayPhone.MultiNet;
using PlayPhone.MultiNet.Providers;
using PlayPhone.MultiNet.Core;

public class MultiplayerBasicsInfo 
{
  public string PlaceIndicator = "";
  public long CurrentScore = 0;
  public MNUserInfo currentUserInfo = null;

  public MultiplayerBasicsInfo(MNUserInfo currentUserInfo) 
  {
    this.currentUserInfo = currentUserInfo;
  }  

  ~MultiplayerBasicsInfo() 
  {
    MNDirect.GetScoreProgressProvider().ScoresUpdated -= new MNScoreProgressProvider.ScoresUpdatedEventHandler(ScoresUpdated);
  }

  public void Start() 
  {
    if (!Started) 
    {
      Started = true;

      Reset();
      MNDirect.GetScoreProgressProvider().ScoresUpdated += new MNScoreProgressProvider.ScoresUpdatedEventHandler(ScoresUpdated);
      MNDirect.GetScoreProgressProvider().Start();
    }
  }

  public void Stop() 
  {
    if (Started) 
    {
      Started = false;
      MNDirect.GetScoreProgressProvider().Stop();
      MNDirect.GetScoreProgressProvider().ScoresUpdated -= new MNScoreProgressProvider.ScoresUpdatedEventHandler(ScoresUpdated);
    }
  }

  public void Reset() 
  {
    CurrentScore = 0;
  }

  public void ScoresUpdated(MNScoreProgressProvider.ScoreItem[] scoreBoard) 
  {
    PlaceIndicator = "";
    bool meFlag = false;

    foreach (MNScoreProgressProvider.ScoreItem scoreItem in scoreBoard) 
    {
      if (scoreItem.UserInfo.UserId == currentUserInfo.UserId) 
      {
        meFlag = true;
      }
      else 
      {
        meFlag = false;
      }

      PlaceIndicator += string.Format("{0}\t{1}. {2}\t\t{3}\n",meFlag?"me->":"    ",scoreItem.Place,scoreItem.UserInfo.UserName,scoreItem.Score);
    }
  }

  public bool Started = false;
}