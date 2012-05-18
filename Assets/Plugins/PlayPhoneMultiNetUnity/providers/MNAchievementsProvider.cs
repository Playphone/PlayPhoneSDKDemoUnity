using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using System.Runtime.CompilerServices;
using PlayPhone.MultiNet.Core;
using PlayPhone.MultiNet.Providers;

namespace PlayPhone.MultiNet.Providers
{
  public class MNAchievementsProvider : MonoBehaviour
  {
    public const int ACHIEVEMENT_IS_SECRET_MASK = 0x0001;

    public class GameAchievementInfo
    {
      public int Id {get;set;}
      public string Name {get;set;}
      public int Flags {get;set;}
      public string Description {get;set;}
      public string Params {get;set;}
      public int Points {get;set;}

      public GameAchievementInfo () {}
    }

    public class PlayerAchievementInfo
    {
      public int Id {get;set;}

      public PlayerAchievementInfo () {}
    }

    #region MNAchievementsProviderEventHandler

    public delegate void GameAchievementListUpdatedEventHandler();
    public event GameAchievementListUpdatedEventHandler GameAchievementListUpdated
    {
      [MethodImpl(MethodImplOptions.Synchronized)]
      add
      {
        MNTools.DLog("GameAchievementListUpdatedEventHandler.add");
        if (GameAchievementListUpdatedStorage == null) {
          GameAchievementListUpdatedStorage += value;

          RegisterEventHandler();
        }
        else {
          GameAchievementListUpdatedStorage += value;
        }
      }

      [MethodImpl(MethodImplOptions.Synchronized)]
      remove
      {
        MNTools.DLog("GameAchievementListUpdatedEventHandler.remove");
        GameAchievementListUpdatedStorage -= value;

        if (GameAchievementListUpdatedStorage == null) {
          UnregisterEventHandler();
        }
      }
    }
    private GameAchievementListUpdatedEventHandler GameAchievementListUpdatedStorage;

    public delegate void PlayerAchievementUnlockedEventHandler(int achievementId);
    public event PlayerAchievementUnlockedEventHandler PlayerAchievementUnlocked
    {
      [MethodImpl(MethodImplOptions.Synchronized)]
      add
      {
        MNTools.DLog("PlayerAchievementUnlockedEventHandler.add");
        if (PlayerAchievementUnlockedStorage == null) {
          PlayerAchievementUnlockedStorage += value;

          RegisterEventHandler();
        }
        else {
          PlayerAchievementUnlockedStorage += value;
        }
      }

      [MethodImpl(MethodImplOptions.Synchronized)]
      remove
      {
        MNTools.DLog("PlayerAchievementUnlockedEventHandler.remove");
        PlayerAchievementUnlockedStorage -= value;

        if (PlayerAchievementUnlockedStorage == null) {
          UnregisterEventHandler();
        }
      }
    }
    private PlayerAchievementUnlockedEventHandler PlayerAchievementUnlockedStorage;

    #endregion MNAchievementsProviderEventHandler

    #if UNITY_IPHONE

    public MNAchievementsProvider.GameAchievementInfo[] GetGameAchievementsList() {
      MNTools.DLog("MNAchievementsProvider:GetGameAchievementsList");

      if (Application.platform == RuntimePlatform.IPhonePlayer) {
        List<object> deserializedArray = MNUnityCommunicator.Serializer.DeserializeArray(
          _MNAchievementsProvider_GetGameAchievementsList(),
          typeof(MNAchievementsProvider.GameAchievementInfo));

        MNAchievementsProvider.GameAchievementInfo[] resultArray = new MNAchievementsProvider.GameAchievementInfo[deserializedArray.Count];

        for (int index = 0;index < deserializedArray.Count;index++) {
          resultArray[index] = (MNAchievementsProvider.GameAchievementInfo)(deserializedArray[index]);
        }

        return resultArray;
      }
      else {
        return new MNAchievementsProvider.GameAchievementInfo[0];
      }
    }

    public MNAchievementsProvider.GameAchievementInfo FindGameAchievementById(int achievementId) {
      MNTools.DLog("MNAchievementsProvider:FindGameAchievementById");

      if (Application.platform == RuntimePlatform.IPhonePlayer) {
        return (MNAchievementsProvider.GameAchievementInfo)MNUnityCommunicator.Serializer.Deserialize(_MNAchievementsProvider_FindGameAchievementById(achievementId),typeof(MNAchievementsProvider.GameAchievementInfo));
      }
      else {
        return default(MNAchievementsProvider.GameAchievementInfo);
      }
    }

    public bool IsGameAchievementListNeedUpdate() {
      MNTools.DLog("MNAchievementsProvider:IsGameAchievementListNeedUpdate");

      if (Application.platform == RuntimePlatform.IPhonePlayer) {
        return _MNAchievementsProvider_IsGameAchievementListNeedUpdate();
      }
      else {
        return default(bool);
      }
    }

    public void DoGameAchievementListUpdate() {
      MNTools.DLog("MNAchievementsProvider:DoGameAchievementListUpdate");

      if (Application.platform == RuntimePlatform.IPhonePlayer) {
        _MNAchievementsProvider_DoGameAchievementListUpdate();
      }
      else {
      }
    }

    public bool IsPlayerAchievementUnlocked(int achievementId) {
      MNTools.DLog("MNAchievementsProvider:IsPlayerAchievementUnlocked");

      if (Application.platform == RuntimePlatform.IPhonePlayer) {
        return _MNAchievementsProvider_IsPlayerAchievementUnlocked(achievementId);
      }
      else {
        return default(bool);
      }
    }

    public void UnlockPlayerAchievement(int achievementId) {
      MNTools.DLog("MNAchievementsProvider:UnlockPlayerAchievement");

      if (Application.platform == RuntimePlatform.IPhonePlayer) {
        _MNAchievementsProvider_UnlockPlayerAchievement(achievementId);
      }
      else {
      }
    }

    public MNAchievementsProvider.PlayerAchievementInfo[] GetPlayerAchievementsList() {
      MNTools.DLog("MNAchievementsProvider:GetPlayerAchievementsList");

      if (Application.platform == RuntimePlatform.IPhonePlayer) {
        List<object> deserializedArray = MNUnityCommunicator.Serializer.DeserializeArray(
          _MNAchievementsProvider_GetPlayerAchievementsList(),
          typeof(MNAchievementsProvider.PlayerAchievementInfo));

        MNAchievementsProvider.PlayerAchievementInfo[] resultArray = new MNAchievementsProvider.PlayerAchievementInfo[deserializedArray.Count];

        for (int index = 0;index < deserializedArray.Count;index++) {
          resultArray[index] = (MNAchievementsProvider.PlayerAchievementInfo)(deserializedArray[index]);
        }

        return resultArray;
      }
      else {
        return new MNAchievementsProvider.PlayerAchievementInfo[0];
      }
    }

    public string GetAchievementImageURL(int achievementId) {
      MNTools.DLog("MNAchievementsProvider:GetAchievementImageURL");

      if (Application.platform == RuntimePlatform.IPhonePlayer) {
        return _MNAchievementsProvider_GetAchievementImageURL(achievementId);
      }
      else {
        return default(string);
      }
    }

    [MethodImpl(MethodImplOptions.Synchronized)]
    private void RegisterEventHandler() {
      if (eventHandlerRegistered) {
        return;
      }

      MNTools.DLog("MNAchievementsProvider:RegisterEventHandler");

      if (Application.platform == RuntimePlatform.IPhonePlayer) {
        eventHandlerRegistered = _MNAchievementsProvider_RegisterEventHandler();
      }
      else {
      }
    }

    [MethodImpl(MethodImplOptions.Synchronized)]
    private void UnregisterEventHandler() {
      if ((GameAchievementListUpdatedStorage != null) || (PlayerAchievementUnlockedStorage != null)) {
        return;
      }

      MNTools.DLog("MNAchievementsProvider:UnregisterEventHandler");

      if (!eventHandlerRegistered) {
        MNTools.DLog("MNAchievementsProvider:EventHandler not registered");
        return;
      }

      if (Application.platform == RuntimePlatform.IPhonePlayer) {
        eventHandlerRegistered = !_MNAchievementsProvider_UnregisterEventHandler();
      }
      else {
      }
    }

    #elif UNITY_ANDROID

    public MNAchievementsProvider.GameAchievementInfo[] GetGameAchievementsList() {
      MNTools.DLog("MNAchievementsProvider:GetGameAchievementsList");

      if (Application.platform == RuntimePlatform.Android) {
        List<object> deserializedArray = MNUnityCommunicator.Serializer.DeserializeArray(
          MNAchievementsProviderUnityClass.CallStatic<string>("getGameAchievementsList"),
          typeof(MNAchievementsProvider.GameAchievementInfo));

        MNAchievementsProvider.GameAchievementInfo[] resultArray = new MNAchievementsProvider.GameAchievementInfo[deserializedArray.Count];

        for (int index = 0;index < deserializedArray.Count;index++) {
          resultArray[index] = (MNAchievementsProvider.GameAchievementInfo)(deserializedArray[index]);
        }

        return resultArray;
      }
      else {
        return new MNAchievementsProvider.GameAchievementInfo[0];
      }
    }

    public MNAchievementsProvider.GameAchievementInfo FindGameAchievementById(int achievementId) {
      MNTools.DLog("MNAchievementsProvider:FindGameAchievementById");

      if (Application.platform == RuntimePlatform.Android) {
        return (MNAchievementsProvider.GameAchievementInfo)MNUnityCommunicator.Serializer.Deserialize(MNAchievementsProviderUnityClass.CallStatic<string>("findGameAchievementById",achievementId),typeof(MNAchievementsProvider.GameAchievementInfo));
      }
      else {
        return default(MNAchievementsProvider.GameAchievementInfo);
      }
    }

    public bool IsGameAchievementListNeedUpdate() {
      MNTools.DLog("MNAchievementsProvider:IsGameAchievementListNeedUpdate");

      if (Application.platform == RuntimePlatform.Android) {
        return MNAchievementsProviderUnityClass.CallStatic<bool>("isGameAchievementListNeedUpdate");
      }
      else {
        return default(bool);
      }
    }

    public void DoGameAchievementListUpdate() {
      MNTools.DLog("MNAchievementsProvider:DoGameAchievementListUpdate");

      if (Application.platform == RuntimePlatform.Android) {
        MNAchievementsProviderUnityClass.CallStatic("doGameAchievementListUpdate");
      }
      else {
      }
    }

    public bool IsPlayerAchievementUnlocked(int achievementId) {
      MNTools.DLog("MNAchievementsProvider:IsPlayerAchievementUnlocked");

      if (Application.platform == RuntimePlatform.Android) {
        return MNAchievementsProviderUnityClass.CallStatic<bool>("isPlayerAchievementUnlocked",achievementId);
      }
      else {
        return default(bool);
      }
    }

    public void UnlockPlayerAchievement(int achievementId) {
      MNTools.DLog("MNAchievementsProvider:UnlockPlayerAchievement");

      if (Application.platform == RuntimePlatform.Android) {
        MNAchievementsProviderUnityClass.CallStatic("unlockPlayerAchievement",achievementId);
      }
      else {
      }
    }

    public MNAchievementsProvider.PlayerAchievementInfo[] GetPlayerAchievementsList() {
      MNTools.DLog("MNAchievementsProvider:GetPlayerAchievementsList");

      if (Application.platform == RuntimePlatform.Android) {
        List<object> deserializedArray = MNUnityCommunicator.Serializer.DeserializeArray(
          MNAchievementsProviderUnityClass.CallStatic<string>("getPlayerAchievementsList"),
          typeof(MNAchievementsProvider.PlayerAchievementInfo));

        MNAchievementsProvider.PlayerAchievementInfo[] resultArray = new MNAchievementsProvider.PlayerAchievementInfo[deserializedArray.Count];

        for (int index = 0;index < deserializedArray.Count;index++) {
          resultArray[index] = (MNAchievementsProvider.PlayerAchievementInfo)(deserializedArray[index]);
        }

        return resultArray;
      }
      else {
        return new MNAchievementsProvider.PlayerAchievementInfo[0];
      }
    }

    public string GetAchievementImageURL(int achievementId) {
      MNTools.DLog("MNAchievementsProvider:GetAchievementImageURL");

      if (Application.platform == RuntimePlatform.Android) {
        return MNAchievementsProviderUnityClass.CallStatic<string>("getAchievementImageURL",achievementId);
      }
      else {
        return default(string);
      }
    }

    [MethodImpl(MethodImplOptions.Synchronized)]
    private void RegisterEventHandler() {
      if (eventHandlerRegistered) {
        return;
      }

      MNTools.DLog("MNAchievementsProvider:RegisterEventHandler");

      if (Application.platform == RuntimePlatform.Android) {
        eventHandlerRegistered = MNAchievementsProviderUnityClass.CallStatic<bool>("registerEventHandler");
      }
      else {
      }
    }

    [MethodImpl(MethodImplOptions.Synchronized)]
    private void UnregisterEventHandler() {
      if ((GameAchievementListUpdatedStorage != null) || (PlayerAchievementUnlockedStorage != null)) {
        return;
      }

      MNTools.DLog("MNAchievementsProvider:UnregisterEventHandler");

      if (!eventHandlerRegistered) {
        MNTools.DLog("MNAchievementsProvider:EventHandler not registered");
        return;
      }

      if (Application.platform == RuntimePlatform.Android) {
        eventHandlerRegistered = !MNAchievementsProviderUnityClass.CallStatic<bool>("unregisterEventHandler");
      }
      else {
      }
    }

    #else
    // Empty implementation for unsupported platforms (such as Unity Editor)
    // Method's arguments are ignored.
    // Non-void methods return default values. If return value is an array empty array is returned.

    public MNAchievementsProvider.GameAchievementInfo[] GetGameAchievementsList() {
      return new MNAchievementsProvider.GameAchievementInfo[0];
    }

    public MNAchievementsProvider.GameAchievementInfo FindGameAchievementById(int achievementId) {
      return default(MNAchievementsProvider.GameAchievementInfo);
    }

    public bool IsGameAchievementListNeedUpdate() {
      return default(bool);
    }

    public void DoGameAchievementListUpdate() {
    }

    public bool IsPlayerAchievementUnlocked(int achievementId) {
      return default(bool);
    }

    public void UnlockPlayerAchievement(int achievementId) {
    }

    public MNAchievementsProvider.PlayerAchievementInfo[] GetPlayerAchievementsList() {
      return new MNAchievementsProvider.PlayerAchievementInfo[0];
    }

    public string GetAchievementImageURL(int achievementId) {
      return default(string);
    }

    private void RegisterEventHandler() {
    }

    private void UnregisterEventHandler() {
    }

    #endif

    #region MNAchievementsProviderEventHandler Messages
    private void MNUM_onGameAchievementListUpdated(string messageParams) {
      MNTools.DLog("MNAchievementsProvider:MNUM_onGameAchievementListUpdated: messageParams=<" + messageParams + ">");

      if (GameAchievementListUpdatedStorage != null) {
        GameAchievementListUpdatedStorage();
      }
    }

    private void MNUM_onPlayerAchievementUnlocked(string messageParams) {
      MNTools.DLog("MNAchievementsProvider:MNUM_onPlayerAchievementUnlocked: messageParams=<" + messageParams + ">");

      if (PlayerAchievementUnlockedStorage != null) {
        List<object> paramsArray = (List<object>)MNUnityCommunicator.Serializer.Deserialize(messageParams,typeof(List<object>));

        MNTools.DLogList("MNUM_onPlayerAchievementUnlocked params",paramsArray,MNTools.DEBUG_LEVEL_DETAILED);

        PlayerAchievementUnlockedStorage(Convert.ToInt32(paramsArray[0]));
      }
    }

    #endregion MNAchievementsProviderEventHandler Messages

    #if UNITY_IPHONE

    [DllImport ("__Internal")]
    private static extern string _MNAchievementsProvider_GetGameAchievementsList ();

    [DllImport ("__Internal")]
    private static extern string _MNAchievementsProvider_FindGameAchievementById (int achievementId);

    [DllImport ("__Internal")]
    private static extern bool _MNAchievementsProvider_IsGameAchievementListNeedUpdate ();

    [DllImport ("__Internal")]
    private static extern void _MNAchievementsProvider_DoGameAchievementListUpdate ();

    [DllImport ("__Internal")]
    private static extern bool _MNAchievementsProvider_IsPlayerAchievementUnlocked (int achievementId);

    [DllImport ("__Internal")]
    private static extern void _MNAchievementsProvider_UnlockPlayerAchievement (int achievementId);

    [DllImport ("__Internal")]
    private static extern string _MNAchievementsProvider_GetPlayerAchievementsList ();

    [DllImport ("__Internal")]
    private static extern string _MNAchievementsProvider_GetAchievementImageURL (int achievementId);

    [DllImport ("__Internal")]
    private static extern bool _MNAchievementsProvider_RegisterEventHandler ();

    [DllImport ("__Internal")]
    private static extern bool _MNAchievementsProvider_UnregisterEventHandler ();

    #elif UNITY_ANDROID

    private static AndroidJavaClass mnAchievementsProviderUnityClass = null;

    private static AndroidJavaClass MNAchievementsProviderUnityClass
    {
      get {
        MNTools.DLog("AndroidJavaClass MNAchievementsProviderUnityClass");

        if (mnAchievementsProviderUnityClass == null) {
          mnAchievementsProviderUnityClass = new AndroidJavaClass("com.playphone.multinet.unity.MNAchievementsProviderUnity");
        }

        return mnAchievementsProviderUnityClass;
      }
    }

    #endif

    private MNAchievementsProvider()
    {
      MNTools.DLog("MNAchievementsProvider:MNAchievementsProvider()");
    }

    #if UNITY_IPHONE || UNITY_ANDROID
    private bool eventHandlerRegistered = false;
    #endif
  }
}
