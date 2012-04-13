using UnityEngine;
using System.Collections;

using PlayPhone.MultiNet;
using PlayPhone.MultiNet.Providers;
using PlayPhone.MultiNet.Core;

public class PPSDemoMultiplayerBasicsView : PPSDemoViewAbstract 
{
  public PPSDemoMultiplayerBasicsView()
  {
    viewName = "Multiplayer Basics";

    MNDirect.DidReceiveGameMessage += new MNDirect.DidReceiveGameMessageEventHandler(OnDidReceiveGameMessage);
  }
  
  public override void Draw()
  {
    if (multiplayerBasicsInfo == null)
    {
      multiplayerBasicsInfo = new MultiplayerBasicsInfo(PPSDemoInfoStorage.currentUserInfo);
    }

    if ((PPSDemoInfoStorage.curSessionStatus == MNConst.MN_OFFLINE) || (PPSDemoInfoStorage.curSessionStatus == MNConst.MN_CONNECTING))
    {
      GUILayout.Label(PPSDemoCommonInfo.NotLoggedInMessage);
      GUILayout.Label("curSessionStatus=" + PPSDemoInfoStorage.curSessionStatus);
    }
    else if (PPSDemoInfoStorage.curSessionStatus == MNConst.MN_LOGGEDIN)
    {
      GUILayout.Label("Press play now to join random room and start the game.");
      if (GUILayout.Button("Play Now!")) 
      {
        MNDirect.GetSession().ReqJoinRandomRoom("0");
      }
    }
    else if (PPSDemoInfoStorage.curSessionStatus == MNConst.MN_IN_GAME_WAIT)
    {
      GUILayout.Label("Waiting for opponent");
    }
    else if (PPSDemoInfoStorage.curSessionStatus == MNConst.MN_IN_GAME_START)
    {
      GUILayout.Label("Starting the game");
      multiplayerBasicsInfo.Start();
    }
    else if (PPSDemoInfoStorage.curSessionStatus == MNConst.MN_IN_GAME_PLAY)
    {
      GUILayout.BeginVertical();
      GUI.enabled = false;
      GUILayout.TextArea(multiplayerBasicsInfo.PlaceIndicator,multiplayerBasicsInfo.PlaceIndicator.Length);
      GUI.enabled = true;
      GUILayout.Label("\n");
      GUILayout.Label("CurrentScore: " + multiplayerBasicsInfo.CurrentScore);

      GUILayout.BeginHorizontal();
      if (GUILayout.Button("+10"))
      {
        multiplayerBasicsInfo.CurrentScore += 10;
        MNDirect.GetScoreProgressProvider().PostScore(multiplayerBasicsInfo.CurrentScore);
      }
      if (GUILayout.Button("-10"))
      {
        multiplayerBasicsInfo.CurrentScore -= 10;
        MNDirect.GetScoreProgressProvider().PostScore(multiplayerBasicsInfo.CurrentScore);
      }

      GUILayout.EndHorizontal();

      GUILayout.Label("Use buttons to change your score. You will see the progress on the top indicator.");

      GUILayout.EndVertical();
    }
    else if (PPSDemoInfoStorage.curSessionStatus == MNConst.MN_IN_GAME_END)
    {
      multiplayerBasicsInfo.Stop();

      GUILayout.Label("Press button for posting score");

      if (GUILayout.Button("Post Score"))
      {
        MNDirect.PostGameScore(multiplayerBasicsInfo.CurrentScore);
      }
    }
    else 
    {
      GUILayout.Label("Invalid state");
    }

    if (isGameMessagesAvailable(PPSDemoInfoStorage.curSessionStatus))
    {
      messageToSend = GUILayout.TextField(messageToSend,200);

      if (GUILayout.Button("Send Message"))
      {
        MNDirect.SendGameMessage(messageToSend);
        messageLog += string.Format("Me: {0}\n",messageToSend);

        messageToSend = "";
      }

      GUILayout.TextArea(messageLog);
    }
  }

  public override void OnClose ()
  {
    MNDirect.DidReceiveGameMessage -= new MNDirect.DidReceiveGameMessageEventHandler(OnDidReceiveGameMessage);
  }

  private bool isGameMessagesAvailable(int sessionStatus)
  {
    bool result = false;

    if ((sessionStatus == MNConst.MN_IN_GAME_WAIT) ||
      (sessionStatus == MNConst.MN_IN_GAME_START) ||
      (sessionStatus == MNConst.MN_IN_GAME_PLAY) ||
      (sessionStatus == MNConst.MN_IN_GAME_END)) {
      result = true;
    }

    return result;
  }

  private void OnDidReceiveGameMessage(string message, MNUserInfo sender)
  {
    messageLog += string.Format("{0} ({1}): {2}\n",sender.UserName,sender.UserId,message);
  }

  protected MultiplayerBasicsInfo multiplayerBasicsInfo = null;
  private string messageToSend = "";
  private string messageLog = "";
}
