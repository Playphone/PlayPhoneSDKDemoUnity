//#define CONSOLE_ENABLED

using UnityEngine;
using System;
using System.Collections.Generic;
using PlayPhone.MultiNet;
using PlayPhone.MultiNet.Providers;
using PlayPhone.MultiNet.Core;

public class PPSDemoMain : MonoBehaviour
{
  void Start ()
  {
    MNTools.DLog("Start");

    MNDirect.Init(PPSDemoCommonInfo.gameId,
                  MNDirect.MakeGameSecretByComponents(PPSDemoCommonInfo.gameSecretPart1,
                                                      PPSDemoCommonInfo.gameSecretPart2,
                                                      PPSDemoCommonInfo.gameSecretPart3,
                                                      PPSDemoCommonInfo.gameSecretPart4));

    MNDirectButton.InitWithLocation((int)MNDirectButton.MNDIRECTBUTTON_TOPRIGHT);
    MNDirectButton.Show();
    MNDirectPopup.Init((int)(
    MNDirectPopup.MNDIRECTPOPUP_WELCOME      |
    MNDirectPopup.MNDIRECTPOPUP_ACHIEVEMENTS |
    MNDirectPopup.MNDIRECTPOPUP_NEW_HI_SCORES));

    MNDirectUIHelper.BindHostActivity();
    MNDirectUIHelper.OnHideDashboard += new MNDirectUIHelper.OnHideDashboardEventHandler(OnHideDashboard);

    MNDirect.ViewDoGoBack += new MNDirect.ViewDoGoBackEventHandler(OnViewDoGoBack);
    MNDirect.ErrorOccurred += new MNDirect.ErrorOccurredEventHandler(OnErrorOccured);
    MNDirect.SessionReady += new MNDirect.SessionReadyEventHandler(OnSessionReady);
    MNDirect.DoStartGameWithParams += new MNDirect.DoStartGameWithParamsEventHandler(OnDoStartGameWithParams);
    MNDirect.DidReceiveGameMessage += new MNDirect.DidReceiveGameMessageEventHandler(OnDidReceiveGameMessage);
    
    MNDirect.DoFinishGame += new MNDirect.DoFinishGameEventHandler(OnDoFinishGame);

    stackView.Push(new PPSDemoExitPageView());
    stackView.Push(new PPSDemoMainView());
    
    // set max title label size
    demoSkin.label.fixedWidth = Screen.width - topGap;

    // set title label style
    demoStyle.normal.textColor = Color.white;
    demoStyle.alignment = TextAnchor.MiddleCenter;
  }

  public GUISkin demoSkin;
  public GUIStyle demoStyle;
    
  public static PPSStackView stackView = new PPSStackView();

  void OnGUI ()
  {
    GUI.skin = demoSkin;
    
    GUI.enabled = true;
    
    GUILayout.BeginArea(new Rect(0,topGap,Screen.width,Screen.height - topGap));

    scrollPosition = GUILayout.BeginScrollView(scrollPosition,
                                               GUILayout.Width(Screen.width),
                                               GUILayout.Height(Screen.height - topGap));
    
    GUILayout.BeginVertical();

    GUILayout.BeginHorizontal();
    if (stackView.Count() > MainViewPosition)
    {
      if (GUILayout.Button("Back", GUILayout.Width((int)(Screen.width * 0.25))))
      {
        stackView.Pop();
      }
    }
    GUILayout.Label(stackView.getViewName(),demoStyle);

    GUILayout.EndHorizontal();

    stackView.Draw();

    GUILayout.EndVertical();

    #if CONSOLE_ENABLED
    if (consoleVisible) {
      GUILayout.Label("===== Console =====");
      GUILayout.BeginVertical();
  
      if (GUILayout.Button("Clear Console")) {
        consoleText = "";
      }

      GUILayout.TextArea(consoleText);

      GUILayout.EndVertical();
    }
    #endif

    GUILayout.EndScrollView();
    GUILayout.EndArea();
  }

  void Update()
  {
    if (stackView.Count() > 1)
    {
      if (Input.GetKeyUp(KeyCode.Escape))
      {
        stackView.Pop();
      }
    }
    else 
    {
      if (Input.GetKeyUp(KeyCode.Escape))
      {
        PPSDemoMain.stackView.Push(new PPSDemoMainView());
      }
    }
  }

  
  #region PPS Delegates
  protected string gameMessages = "";

  public void OnSessionReady (MNSession session)
  {
    MNDirect.GetAchievementsProvider().PlayerAchievementUnlocked += new MNAchievementsProvider.PlayerAchievementUnlockedEventHandler(OnPlayerAchievementUnlocked);

    MNDirect.GetVItemsProvider().VItemsListUpdated += new MNVItemsProvider.VItemsListUpdatedEventHandler(OnVItemsListUpdated);

    MNDirect.GetVShopProvider().VShopInfoUpdated += new MNVShopProvider.VShopInfoUpdatedEventHandler(OnVShopInfoUpdated);

    MNDirect.GetPlayerListProvider().PlayerJoin += new MNPlayerListProvider.PlayerJoinEventHandler(OnPlayerJoined);
    MNDirect.GetPlayerListProvider().PlayerLeft += new MNPlayerListProvider.PlayerLeftEventHandler(OnPlayerLeft);

    MNDirect.GetMyHiScoresProvider().NewHiScore += new MNMyHiScoresProvider.NewHiScoreEventHandler(OnNewHiScore);

    MNDirect.GetSession().SessionStatusChanged += new MNSession.SessionStatusChangedEventHandler(OnSessionStatusChanged);
    MNDirect.GetSession().UserChanged += new MNSession.UserChangedEventHandler(OnUserChanged);
    MNDirect.GetSession().RoomUserJoin += new MNSession.RoomUserJoinEventHandler(OnRoomUserJoin);
    MNDirect.GetSession().RoomUserLeave += new MNSession.RoomUserLeaveEventHandler(OnRoomUserLeave);

    MNTools.DLog("MNDirect_SessionReady");
  }

  public void OnViewDoGoBack ()
  {
    MNTools.DLog("MNDirect_ViewDoGoBack");
  }

  public void OnDoStartGameWithParams (MNGameParams param1_MNGameParams )
  {
    MNTools.DLog("MNDirect_DoStartGameWithParams:" + param1_MNGameParams.ToString());
    gameStarted = true;
    gameFinished = false;
    gameParams = param1_MNGameParams;
  }
 
  public void OnDoFinishGame ()
  {
    MNTools.DLog("MNDirect_DoFinishGame");
    gameFinished = true;
  }

  public void OnErrorOccured (MNErrorInfo error)
  {
    MNTools.DLog("MNDirect_ErrorOccured:" + error.ToString());
  }

  public void OnDidReceiveGameMessage (string param1_String, MNUserInfo param2_MNUserInfo)
  {
    MNTools.DLog("MNDirect_DidReceiveGameMessage:\"" + param1_String + "\" FROM:" + param2_MNUserInfo.ToString());
    gameMessages += "FROM: "+ param2_MNUserInfo.UserName+" >> " + param1_String +"\n";
  }

  public void OnHideDashboard() 
  {
    MNTools.DLog("MNDirectUIHelper_OnHideDashboard");
  }

  public void OnPlayerAchievementUnlocked (int achievementId) 
  {
    MNTools.DLog("MNDirect_PlayerAchievementUnlocked");
  }

  public void OnVItemsListUpdated () 
  {
    MNTools.DLog("PPS_VItemsListUpdated");
  }

  public void OnVShopInfoUpdated () 
  {
    MNTools.DLog("PPS_VShopInfoUpdated");
  }

  public void OnPlayerJoined(MNUserInfo player) 
  {
    MNTools.DLog("PPS_PlayerJoined");
    playerArray.Add(player);
  }

  public void OnPlayerLeft (MNUserInfo player) 
  {
    MNTools.DLog("PPS_PlayerLeft");
    bool found = false;
    int index = 0;

    while ((!found) && (index < playerArray.Count)) 
    {
      if (((MNUserInfo)playerArray[index]).UserId == player.UserId)
      {
        found = true;
      }
      else 
      {
        index++;
      }
    }

    playerArray.RemoveAt(index);
  }

  void OnNewHiScore (long newScore,int gameSetId,int periodMask)
  {
    PPSDemoMain.consolePrintln(string.Format("PPS_NewHiScore({0},{1},{2})",newScore,gameSetId,periodMask));
  }

  public void OnSessionStatusChanged(int newStatus,int oldStatus) 
  {
    PPSDemoMain.consolePrintln(string.Format("MNSession_SessionStatusChanged(newStatus={0},oldStatus={1})",newStatus,oldStatus));

    if ((oldStatus == MNConst.MN_LOGGEDIN) &&
       ((newStatus == MNConst.MN_IN_GAME_WAIT ) ||
        (newStatus == MNConst.MN_IN_GAME_START) ||
        (newStatus == MNConst.MN_IN_GAME_PLAY ) ||
        (newStatus == MNConst.MN_IN_GAME_END  ))) 
    {
      PPSDemoMain.consolePrintln(string.Format("User joined to game room with Id: {0}",MNDirect.GetSession().GetCurrentRoomId()));
      PPSDemoMain.consolePrintln(string.Format("User status: {0}",MNDirect.GetSession().GetRoomUserStatus()));
    }

    if (((oldStatus == MNConst.MN_IN_GAME_WAIT ) ||
         (oldStatus == MNConst.MN_IN_GAME_START) ||
         (oldStatus == MNConst.MN_IN_GAME_PLAY ) ||
         (oldStatus == MNConst.MN_IN_GAME_END  )) &&
        (newStatus == MNConst.MN_LOGGEDIN))
    {
      PPSDemoMain.consolePrintln(string.Format("User left the game room"));
    }
  }

  public void OnUserChanged(long userId) 
  {
    PPSDemoMain.consolePrintln(string.Format("MNSession_UserChanged({0})",userId));
  }

  protected void OnRoomUserJoin(MNUserInfo userInfo) 
  {
    if (userInfo == null) 
    {
      MNTools.DLog("userInfo = null");
    }
    else 
    {
       MNTools.DLog("userInfo.UserName = " + userInfo.UserName);
    }

    PPSDemoMain.consolePrintln(string.Format("{0} ({1}) joined to room",userInfo.UserName,userInfo.UserId));
  }

  protected void OnRoomUserLeave(MNUserInfo userInfo) 
  {
    PPSDemoMain.consolePrintln(string.Format("{0} ({1}) left the room",userInfo.UserName,userInfo.UserId));
  }
  
  #endregion PPSDelegates
  
  protected int topGap = 50;
  
  protected Vector2 scrollPosition;
  
  protected bool gameStarted = false;
  protected bool gameFinished = false;
  protected MNGameParams gameParams = null;
  protected bool getUpByEscapeButton = false;
  protected List<object> playerArray = new List<object>();
  protected int MainViewPosition = 2;

  #if CONSOLE_ENABLED
  protected static string consoleText = "";
  protected static bool consoleVisible = true;

  public static void consolePrintln(string line) {
    consoleText += line + "\n";
  }

  public static void consoleClear() {
    consoleText = "";
  }

  public static void consoleShow() {
    consoleVisible = true;
  }

  public static void consoleHide() {
    consoleVisible = false;
  }

  public static bool consoleIsVisible() {
    return consoleVisible;
  }

  #else
  public static void consolePrintln(string line){}

  public static void consoleClear(){}

  public static void consoleShow(){}

  public static void consoleHide(){}

  public static bool consoleIsVisible()
  {
    return false;
  }
  #endif
}
