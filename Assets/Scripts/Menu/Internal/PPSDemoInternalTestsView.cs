using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;
using PlayPhone.MultiNet;
using PlayPhone.MultiNet.Core;

public class PPSDemoInternalTestsView : PPSDemoViewAbstract
{
  public PPSDemoInternalTestsView ()
  {
  }

  public override void Draw ()
  {
    GUILayout.Label("\nInternal tests");

    if (GUILayout.Button("Get Player List"))
    {
      List<object> playerArray = new List<object>();
      playerArray.AddRange(MNDirect.GetPlayerListProvider().GetPlayerList());

      int index = 1;
      foreach (MNUserInfo userInfo in playerArray) {
        PPSDemoMain.consolePrintln(string.Format("{0}. {1} ({2})",index++,userInfo.UserName,userInfo.UserId));
      }
    }

    if (GUILayout.Button("Get Room User List"))
    {
      List<object> playerArray = new List<object>();
      playerArray.AddRange(MNDirect.GetSession().GetRoomUserList());

      int index = 1;
      foreach (MNUserInfo userInfo in playerArray) {
        PPSDemoMain.consolePrintln(string.Format("{0}. {1} ({2})",index++,userInfo.UserName,userInfo.UserId));
      }
    }

    if (GUILayout.Button("PostScore (lastScore+10)"))
    {
      long? myScore = MNDirect.GetMyHiScoresProvider().GetMyHiScore(0);
      if (myScore == null) {
        MNTools.DLog("myScore = null");
        MNDirect.PostGameScore(10);
      }
      else {
        MNTools.DLog("myScore = " + myScore.ToString());
        MNDirect.PostGameScore(myScore.Value + 10);
      }
    }
    if (GUILayout.Button("Get My Hi-Score (long?)")) {
      long? myScore = MNDirect.GetMyHiScoresProvider().GetMyHiScore(0);

      if (myScore == null) {
        PPSDemoMain.consolePrintln("myScore == null");
      }
      else {
        PPSDemoMain.consolePrintln("Hi-Score = " + myScore.ToString());
      }
    }
    if (GUILayout.Button("Get My Score Map")) {
      IDictionary scoreMap = MNDirect.GetMyHiScoresProvider().GetMyHiScores();

      if (scoreMap == null) {
        PPSDemoMain.consolePrintln("scoreMap == null");
      }
      else if (scoreMap.Count == 0) {
        PPSDemoMain.consolePrintln("scoreMap is Empty");
      }
      else {
        foreach (object gameSetId in scoreMap.Keys) {
          PPSDemoMain.consolePrintln(string.Format("GameSetId = {0}, Score = {1}",Convert.ToInt32(gameSetId),Convert.ToInt64(scoreMap[gameSetId])));
        }
      }
    }

    if (GUILayout.Button("Create Buddy room")) {
      int gameSetId = 0;
      MNBuddyRoomParams buddyRoomParams = new MNBuddyRoomParams("my room", gameSetId, " ", " ", "hello");
      MNDirect.GetSession().ReqCreateBuddyRoom(buddyRoomParams);
    }
  }

  public override void OnClose ()
  {
   // empty implementation
  }
}

