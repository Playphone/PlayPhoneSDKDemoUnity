using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using System.Runtime.CompilerServices;
using PlayPhone.MultiNet.Core;
using PlayPhone.MultiNet.Providers;

namespace PlayPhone.MultiNet
{
  public class MNDirect : MonoBehaviour
  {
    #region MNDirectEventHandler

    public delegate void DoStartGameWithParamsEventHandler(MNGameParams _params);
    public static event DoStartGameWithParamsEventHandler DoStartGameWithParams;

    public delegate void DoFinishGameEventHandler();
    public static event DoFinishGameEventHandler DoFinishGame;

    public delegate void DoCancelGameEventHandler();
    public static event DoCancelGameEventHandler DoCancelGame;

    public delegate void ViewDoGoBackEventHandler();
    public static event ViewDoGoBackEventHandler ViewDoGoBack;

    public delegate void DidReceiveGameMessageEventHandler(string message, MNUserInfo sender);
    public static event DidReceiveGameMessageEventHandler DidReceiveGameMessage;

    public delegate void SessionStatusChangedEventHandler(int newStatus);
    public static event SessionStatusChangedEventHandler SessionStatusChanged;

    public delegate void ErrorOccurredEventHandler(MNErrorInfo error);
    public static event ErrorOccurredEventHandler ErrorOccurred;

    public delegate void SessionReadyEventHandler(MNSession session);
    public static event SessionReadyEventHandler SessionReady;

    #endregion MNDirectEventHandler

    public static string MakeGameSecretByComponents(uint secret1, uint secret2, uint secret3, uint secret4) {
      MNTools.DLog("MNDirect:MakeGameSecretByComponents");
      return string.Format("{0:x08}-{1:x08}-{2:x08}-{3:x08}", secret1, secret2, secret3, secret4);
    }

    #if UNITY_IPHONE

    public static void Init(int gameId, string gameSecret) {
      MNTools.DLog("MNDirect:Init");

      if (Application.platform == RuntimePlatform.IPhonePlayer) {
        MNUnityCommunicator.Init();

        _MNDirect_Init(gameId, gameSecret);
      }
      else {
        throw new MNNotOnDeviceExcepton();
      }
    }


    public static void ShutdownSession() {
      MNTools.DLog("MNDirect:ShutdownSession");

      if (Application.platform == RuntimePlatform.IPhonePlayer) {
        _MNDirect_ShutdownSession();
      }
      else {
        throw new MNNotOnDeviceExcepton();
      }
    }

    public static bool IsOnline() {
      MNTools.DLog("MNDirect:IsOnline");

      if (Application.platform == RuntimePlatform.IPhonePlayer) {
        return _MNDirect_IsOnline();
      }
      else {
        throw new MNNotOnDeviceExcepton();
      }
    }

    public static bool IsUserLoggedIn() {
      MNTools.DLog("MNDirect:IsUserLoggedIn");

      if (Application.platform == RuntimePlatform.IPhonePlayer) {
        return _MNDirect_IsUserLoggedIn();
      }
      else {
        throw new MNNotOnDeviceExcepton();
      }
    }

    public static int GetSessionStatus() {
      MNTools.DLog("MNDirect:GetSessionStatus");

      if (Application.platform == RuntimePlatform.IPhonePlayer) {
        return _MNDirect_GetSessionStatus();
      }
      else {
        throw new MNNotOnDeviceExcepton();
      }
    }

    public static void PostGameScore(long score) {
      MNTools.DLog("MNDirect:PostGameScore");

      if (Application.platform == RuntimePlatform.IPhonePlayer) {
        _MNDirect_PostGameScore(score);
      }
      else {
        throw new MNNotOnDeviceExcepton();
      }
    }

    public static void PostGameScorePending(long score) {
      MNTools.DLog("MNDirect:PostGameScorePending");

      if (Application.platform == RuntimePlatform.IPhonePlayer) {
        _MNDirect_PostGameScorePending(score);
      }
      else {
        throw new MNNotOnDeviceExcepton();
      }
    }

    public static void CancelGame() {
      MNTools.DLog("MNDirect:CancelGame");

      if (Application.platform == RuntimePlatform.IPhonePlayer) {
        _MNDirect_CancelGame();
      }
      else {
        throw new MNNotOnDeviceExcepton();
      }
    }

    public static void SetDefaultGameSetId(int gameSetId) {
      MNTools.DLog("MNDirect:SetDefaultGameSetId");

      if (Application.platform == RuntimePlatform.IPhonePlayer) {
        _MNDirect_SetDefaultGameSetId(gameSetId);
      }
      else {
        throw new MNNotOnDeviceExcepton();
      }
    }

    public static int GetDefaultGameSetId() {
      MNTools.DLog("MNDirect:GetDefaultGameSetId");

      if (Application.platform == RuntimePlatform.IPhonePlayer) {
        return _MNDirect_GetDefaultGameSetId();
      }
      else {
        throw new MNNotOnDeviceExcepton();
      }
    }

    public static void SendAppBeacon(string actionName, string beaconData) {
      MNTools.DLog("MNDirect:SendAppBeacon");

      if (Application.platform == RuntimePlatform.IPhonePlayer) {
        _MNDirect_SendAppBeacon(actionName, beaconData);
      }
      else {
        throw new MNNotOnDeviceExcepton();
      }
    }

    public static void ExecAppCommand(string name, string param) {
      MNTools.DLog("MNDirect:ExecAppCommand");

      if (Application.platform == RuntimePlatform.IPhonePlayer) {
        _MNDirect_ExecAppCommand(name, param);
      }
      else {
        throw new MNNotOnDeviceExcepton();
      }
    }

    public static void SendGameMessage(string message) {
      MNTools.DLog("MNDirect:SendGameMessage");

      if (Application.platform == RuntimePlatform.IPhonePlayer) {
        _MNDirect_SendGameMessage(message);
      }
      else {
        throw new MNNotOnDeviceExcepton();
      }
    }

    #elif UNITY_ANDROID

    public static void Init(int gameId, string gameSecret) {
      MNTools.DLog("MNDirect:Init");

      if (Application.platform == RuntimePlatform.Android) {
        MNUnityCommunicator.Init();

        MNDirectUnityClass.CallStatic("init",gameId, gameSecret);
      }
      else {
        throw new MNNotOnDeviceExcepton();
      }
    }

    public static void ShutdownSession() {
      MNTools.DLog("MNDirect:ShutdownSession");

      if (Application.platform == RuntimePlatform.Android) {
        MNDirectUnityClass.CallStatic("shutdownSession");
      }
      else {
        throw new MNNotOnDeviceExcepton();
      }
    }

    public static bool IsOnline() {
      MNTools.DLog("MNDirect:IsOnline");

      if (Application.platform == RuntimePlatform.Android) {
        return MNDirectUnityClass.CallStatic<bool>("isOnline");
      }
      else {
        throw new MNNotOnDeviceExcepton();
      }
    }

    public static bool IsUserLoggedIn() {
      MNTools.DLog("MNDirect:IsUserLoggedIn");

      if (Application.platform == RuntimePlatform.Android) {
        return MNDirectUnityClass.CallStatic<bool>("isUserLoggedIn");
      }
      else {
        throw new MNNotOnDeviceExcepton();
      }
    }

    public static int GetSessionStatus() {
      MNTools.DLog("MNDirect:GetSessionStatus");

      if (Application.platform == RuntimePlatform.Android) {
        return MNDirectUnityClass.CallStatic<int>("getSessionStatus");
      }
      else {
        throw new MNNotOnDeviceExcepton();
      }
    }

    public static void PostGameScore(long score) {
      MNTools.DLog("MNDirect:PostGameScore");

      if (Application.platform == RuntimePlatform.Android) {
        MNDirectUnityClass.CallStatic("postGameScore",score);
      }
      else {
        throw new MNNotOnDeviceExcepton();
      }
    }

    public static void PostGameScorePending(long score) {
      MNTools.DLog("MNDirect:PostGameScorePending");

      if (Application.platform == RuntimePlatform.Android) {
        MNDirectUnityClass.CallStatic("postGameScorePending",score);
      }
      else {
        throw new MNNotOnDeviceExcepton();
      }
    }

    public static void CancelGame() {
      MNTools.DLog("MNDirect:CancelGame");

      if (Application.platform == RuntimePlatform.Android) {
        MNDirectUnityClass.CallStatic("cancelGame");
      }
      else {
        throw new MNNotOnDeviceExcepton();
      }
    }

    public static void SetDefaultGameSetId(int gameSetId) {
      MNTools.DLog("MNDirect:SetDefaultGameSetId");

      if (Application.platform == RuntimePlatform.Android) {
        MNDirectUnityClass.CallStatic("setDefaultGameSetId",gameSetId);
      }
      else {
        throw new MNNotOnDeviceExcepton();
      }
    }

    public static int GetDefaultGameSetId() {
      MNTools.DLog("MNDirect:GetDefaultGameSetId");

      if (Application.platform == RuntimePlatform.Android) {
        return MNDirectUnityClass.CallStatic<int>("getDefaultGameSetId");
      }
      else {
        throw new MNNotOnDeviceExcepton();
      }
    }

    public static void SendAppBeacon(string actionName, string beaconData) {
      MNTools.DLog("MNDirect:SendAppBeacon");

      if (Application.platform == RuntimePlatform.Android) {
        MNDirectUnityClass.CallStatic("sendAppBeacon",actionName, beaconData);
      }
      else {
        throw new MNNotOnDeviceExcepton();
      }
    }

    public static void ExecAppCommand(string name, string param) {
      MNTools.DLog("MNDirect:ExecAppCommand");

      if (Application.platform == RuntimePlatform.Android) {
        MNDirectUnityClass.CallStatic("execAppCommand",name, param);
      }
      else {
        throw new MNNotOnDeviceExcepton();
      }
    }

    public static void SendGameMessage(string message) {
      MNTools.DLog("MNDirect:SendGameMessage");

      if (Application.platform == RuntimePlatform.Android) {
        MNDirectUnityClass.CallStatic("sendGameMessage",message);
      }
      else {
        throw new MNNotOnDeviceExcepton();
      }
    }

    #endif

    #region MNDirectEventHandler Messages
    private void MNUM_mnDirectDoStartGameWithParams(string messageParams) {
      MNTools.DLog("MNDirect:MNUM_mnDirectDoStartGameWithParams: messageParams=<" + messageParams + ">");

      if (DoStartGameWithParams != null) {
        List<object> paramsArray = (List<object>)MNUnityCommunicator.Serializer.Deserialize(messageParams,typeof(List<object>));

        MNTools.DLogList("MNUM_mnDirectDoStartGameWithParams params",paramsArray,MNTools.DEBUG_LEVEL_DETAILED);

        DoStartGameWithParams(MNSerializerMapper.MNGameParamsFromDictionary((IDictionary)paramsArray[0]));
      }
    }

    private void MNUM_mnDirectDoFinishGame(string messageParams) {
      MNTools.DLog("MNDirect:MNUM_mnDirectDoFinishGame: messageParams=<" + messageParams + ">");

      if (DoFinishGame != null) {
        DoFinishGame();
      }
    }

    private void MNUM_mnDirectDoCancelGame(string messageParams) {
      MNTools.DLog("MNDirect:MNUM_mnDirectDoCancelGame: messageParams=<" + messageParams + ">");

      if (DoCancelGame != null) {
        DoCancelGame();
      }
    }

    private void MNUM_mnDirectViewDoGoBack(string messageParams) {
      MNTools.DLog("MNDirect:MNUM_mnDirectViewDoGoBack: messageParams=<" + messageParams + ">");

      if (ViewDoGoBack != null) {
        ViewDoGoBack();
      }
    }

    private void MNUM_mnDirectDidReceiveGameMessage(string messageParams) {
      MNTools.DLog("MNDirect:MNUM_mnDirectDidReceiveGameMessage: messageParams=<" + messageParams + ">");

      if (DidReceiveGameMessage != null) {
        List<object> paramsArray = (List<object>)MNUnityCommunicator.Serializer.Deserialize(messageParams,typeof(List<object>));

        MNTools.DLogList("MNUM_mnDirectDidReceiveGameMessage params",paramsArray,MNTools.DEBUG_LEVEL_DETAILED);

        DidReceiveGameMessage((string)paramsArray[0], MNSerializerMapper.MNUserInfoFromDictionary((IDictionary)paramsArray[1]));
      }
    }

    private void MNUM_mnDirectSessionStatusChanged(string messageParams) {
      MNTools.DLog("MNDirect:MNUM_mnDirectSessionStatusChanged: messageParams=<" + messageParams + ">");

      if (SessionStatusChanged != null) {
        List<object> paramsArray = (List<object>)MNUnityCommunicator.Serializer.Deserialize(messageParams,typeof(List<object>));

        MNTools.DLogList("MNUM_mnDirectSessionStatusChanged params",paramsArray,MNTools.DEBUG_LEVEL_DETAILED);

        SessionStatusChanged(Convert.ToInt32(paramsArray[0]));
      }
    }

    private void MNUM_mnDirectErrorOccurred(string messageParams) {
      MNTools.DLog("MNDirect:MNUM_mnDirectErrorOccurred: messageParams=<" + messageParams + ">");

      if (ErrorOccurred != null) {
        List<object> paramsArray = (List<object>)MNUnityCommunicator.Serializer.Deserialize(messageParams,typeof(List<object>));

        MNTools.DLogList("MNUM_mnDirectErrorOccurred params",paramsArray,MNTools.DEBUG_LEVEL_DETAILED);

        ErrorOccurred(MNSerializerMapper.MNErrorInfoFromDictionary((IDictionary)paramsArray[0]));
      }
    }

    private void MNUM_mnDirectSessionReady(string messageParams) {
      MNTools.DLog("MNDirect:MNUM_mnDirectSessionReady: messageParams=<" + messageParams + ">");
      session = (MNSession)MNUnityCommunicator.registerComponent(typeof(MNSession));

      if (SessionReady != null) {
        SessionReady(session);
      }
    }

    #endregion MNDirectEventHandler Messages

    #if UNITY_IPHONE

    [DllImport ("__Internal")]
    private static extern void _MNDirect_Init (int gameId, string gameSecret);

    [DllImport ("__Internal")]
    private static extern void _MNDirect_ShutdownSession ();

    [DllImport ("__Internal")]
    private static extern bool _MNDirect_IsOnline ();

    [DllImport ("__Internal")]
    private static extern bool _MNDirect_IsUserLoggedIn ();

    [DllImport ("__Internal")]
    private static extern int _MNDirect_GetSessionStatus ();

    [DllImport ("__Internal")]
    private static extern void _MNDirect_PostGameScore (long score);

    [DllImport ("__Internal")]
    private static extern void _MNDirect_PostGameScorePending (long score);

    [DllImport ("__Internal")]
    private static extern void _MNDirect_CancelGame ();

    [DllImport ("__Internal")]
    private static extern void _MNDirect_SetDefaultGameSetId (int gameSetId);

    [DllImport ("__Internal")]
    private static extern int _MNDirect_GetDefaultGameSetId ();

    [DllImport ("__Internal")]
    private static extern void _MNDirect_SendAppBeacon (string actionName, string beaconData);

    [DllImport ("__Internal")]
    private static extern void _MNDirect_ExecAppCommand (string name, string param);

    [DllImport ("__Internal")]
    private static extern void _MNDirect_SendGameMessage (string message);

    #elif UNITY_ANDROID

    private static AndroidJavaClass mnDirectUnityClass = null;

    private static AndroidJavaClass MNDirectUnityClass
    {
      get {
        MNTools.DLog("AndroidJavaClass MNDirectUnityClass");

        if (mnDirectUnityClass == null) {
          mnDirectUnityClass = new AndroidJavaClass("com.playphone.multinet.unity.MNDirectUnity");
        }

        return mnDirectUnityClass;
      }
    }

    #endif

    private MNDirect()
    {
      MNTools.DLog("MNDirect:MNDirect()");
    }

    #if UNITY_IPHONE
    #elif UNITY_ANDROID
    void OnApplicationPause(bool pause) {
      MNTools.DLog("MNDirect:OnApplicationPause");

      if (pause) {
        MNDirectUIHelper.UnbindHostActivity();
      }
      else {
        MNDirectUIHelper.BindHostActivity();
      }
    }

    #endif

    public static MNAchievementsProvider GetAchievementsProvider() {
      return (MNAchievementsProvider)MNUnityCommunicator.registerComponent(typeof(MNAchievementsProvider));
    }

    public static MNClientRobotsProvider GetClientRobotsProvider() {
      return (MNClientRobotsProvider)MNUnityCommunicator.registerComponent(typeof(MNClientRobotsProvider));
    }

    public static MNGameCookiesProvider GetGameCookiesProvider() {
      return (MNGameCookiesProvider)MNUnityCommunicator.registerComponent(typeof(MNGameCookiesProvider));
    }

    public static MNGameRoomCookiesProvider GetGameRoomCookiesProvider() {
      return (MNGameRoomCookiesProvider)MNUnityCommunicator.registerComponent(typeof(MNGameRoomCookiesProvider));
    }

    public static MNMyHiScoresProvider GetMyHiScoresProvider() {
      return (MNMyHiScoresProvider)MNUnityCommunicator.registerComponent(typeof(MNMyHiScoresProvider));
    }

    public static MNPlayerListProvider GetPlayerListProvider() {
      return (MNPlayerListProvider)MNUnityCommunicator.registerComponent(typeof(MNPlayerListProvider));
    }

    public static MNServerInfoProvider GetServerInfoProvider() {
      return (MNServerInfoProvider)MNUnityCommunicator.registerComponent(typeof(MNServerInfoProvider));
    }

    public static MNVItemsProvider GetVItemsProvider() {
      return (MNVItemsProvider)MNUnityCommunicator.registerComponent(typeof(MNVItemsProvider));
    }

    public static MNVShopProvider GetVShopProvider() {
      return (MNVShopProvider)MNUnityCommunicator.registerComponent(typeof(MNVShopProvider));
    }

    public static MNWSProvider GetWSProvider() {
      return (MNWSProvider)MNUnityCommunicator.registerComponent(typeof(MNWSProvider));
    }

    public static MNGameSettingsProvider GetGameSettingsProvider() {
      return (MNGameSettingsProvider)MNUnityCommunicator.registerComponent(typeof(MNGameSettingsProvider));
    }

    public static MNScoreProgressProvider GetScoreProgressProvider() {
      return (MNScoreProgressProvider)MNUnityCommunicator.registerComponent(typeof(MNScoreProgressProvider));
    }

    public static MNSession GetSession() {
      return session;
    }

    private static MNSession session = null;
  }
}
