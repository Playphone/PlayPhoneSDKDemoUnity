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
        throw new MNNotOnDeviceExcepton();
      }
    }

    public MNAchievementsProvider.GameAchievementInfo FindGameAchievementById(int achievementId) {
      MNTools.DLog("MNAchievementsProvider:FindGameAchievementById");

      if (Application.platform == RuntimePlatform.IPhonePlayer) {
        return (MNAchievementsProvider.GameAchievementInfo)MNUnityCommunicator.Serializer.Deserialize(_MNAchievementsProvider_FindGameAchievementById(achievementId),typeof(MNAchievementsProvider.GameAchievementInfo));
      }
      else {
        throw new MNNotOnDeviceExcepton();
      }
    }

    public bool IsGameAchievementListNeedUpdate() {
      MNTools.DLog("MNAchievementsProvider:IsGameAchievementListNeedUpdate");

      if (Application.platform == RuntimePlatform.IPhonePlayer) {
        return _MNAchievementsProvider_IsGameAchievementListNeedUpdate();
      }
      else {
        throw new MNNotOnDeviceExcepton();
      }
    }

    public void DoGameAchievementListUpdate() {
      MNTools.DLog("MNAchievementsProvider:DoGameAchievementListUpdate");

      if (Application.platform == RuntimePlatform.IPhonePlayer) {
        _MNAchievementsProvider_DoGameAchievementListUpdate();
      }
      else {
        throw new MNNotOnDeviceExcepton();
      }
    }

    public bool IsPlayerAchievementUnlocked(int achievementId) {
      MNTools.DLog("MNAchievementsProvider:IsPlayerAchievementUnlocked");

      if (Application.platform == RuntimePlatform.IPhonePlayer) {
        return _MNAchievementsProvider_IsPlayerAchievementUnlocked(achievementId);
      }
      else {
        throw new MNNotOnDeviceExcepton();
      }
    }

    public void UnlockPlayerAchievement(int achievementId) {
      MNTools.DLog("MNAchievementsProvider:UnlockPlayerAchievement");

      if (Application.platform == RuntimePlatform.IPhonePlayer) {
        _MNAchievementsProvider_UnlockPlayerAchievement(achievementId);
      }
      else {
        throw new MNNotOnDeviceExcepton();
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
        throw new MNNotOnDeviceExcepton();
      }
    }

    public string GetAchievementImageURL(int achievementId) {
      MNTools.DLog("MNAchievementsProvider:GetAchievementImageURL");

      if (Application.platform == RuntimePlatform.IPhonePlayer) {
        return _MNAchievementsProvider_GetAchievementImageURL(achievementId);
      }
      else {
        throw new MNNotOnDeviceExcepton();
      }
    }

    [MethodImpl(MethodImplOptions.Synchronized)]
    private void RegisterEventHandler() {
      MNTools.DLog("MNAchievementsProvider:RegisterEventHandler");

      if (eventHandlerRegistered) {
        MNTools.DLog("MNAchievementsProvider:EventHandler already registered");
        return;
      }

      if (Application.platform == RuntimePlatform.IPhonePlayer) {
        eventHandlerRegistered = _MNAchievementsProvider_RegisterEventHandler();
      }
      else {
        throw new MNNotOnDeviceExcepton();
      }
    }

    [MethodImpl(MethodImplOptions.Synchronized)]
    private void UnregisterEventHandler() {
      MNTools.DLog("MNAchievementsProvider:UnregisterEventHandler");

      if (!eventHandlerRegistered) {
        MNTools.DLog("MNAchievementsProvider:EventHandler not registered");
        return;
      }

      if (Application.platform == RuntimePlatform.IPhonePlayer) {
        eventHandlerRegistered = !_MNAchievementsProvider_UnregisterEventHandler();
      }
      else {
        throw new MNNotOnDeviceExcepton();
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
        throw new MNNotOnDeviceExcepton();
      }
    }

    public MNAchievementsProvider.GameAchievementInfo FindGameAchievementById(int achievementId) {
      MNTools.DLog("MNAchievementsProvider:FindGameAchievementById");

      if (Application.platform == RuntimePlatform.Android) {
        return (MNAchievementsProvider.GameAchievementInfo)MNUnityCommunicator.Serializer.Deserialize(MNAchievementsProviderUnityClass.CallStatic<string>("findGameAchievementById",achievementId),typeof(MNAchievementsProvider.GameAchievementInfo));
      }
      else {
        throw new MNNotOnDeviceExcepton();
      }
    }

    public bool IsGameAchievementListNeedUpdate() {
      MNTools.DLog("MNAchievementsProvider:IsGameAchievementListNeedUpdate");

      if (Application.platform == RuntimePlatform.Android) {
        return MNAchievementsProviderUnityClass.CallStatic<bool>("isGameAchievementListNeedUpdate");
      }
      else {
        throw new MNNotOnDeviceExcepton();
      }
    }

    public void DoGameAchievementListUpdate() {
      MNTools.DLog("MNAchievementsProvider:DoGameAchievementListUpdate");

      if (Application.platform == RuntimePlatform.Android) {
        MNAchievementsProviderUnityClass.CallStatic("doGameAchievementListUpdate");
      }
      else {
        throw new MNNotOnDeviceExcepton();
      }
    }

    public bool IsPlayerAchievementUnlocked(int achievementId) {
      MNTools.DLog("MNAchievementsProvider:IsPlayerAchievementUnlocked");

      if (Application.platform == RuntimePlatform.Android) {
        return MNAchievementsProviderUnityClass.CallStatic<bool>("isPlayerAchievementUnlocked",achievementId);
      }
      else {
        throw new MNNotOnDeviceExcepton();
      }
    }

    public void UnlockPlayerAchievement(int achievementId) {
      MNTools.DLog("MNAchievementsProvider:UnlockPlayerAchievement");

      if (Application.platform == RuntimePlatform.Android) {
        MNAchievementsProviderUnityClass.CallStatic("unlockPlayerAchievement",achievementId);
      }
      else {
        throw new MNNotOnDeviceExcepton();
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
        throw new MNNotOnDeviceExcepton();
      }
    }

    public string GetAchievementImageURL(int achievementId) {
      MNTools.DLog("MNAchievementsProvider:GetAchievementImageURL");

      if (Application.platform == RuntimePlatform.Android) {
        return MNAchievementsProviderUnityClass.CallStatic<string>("getAchievementImageURL",achievementId);
      }
      else {
        throw new MNNotOnDeviceExcepton();
      }
    }

    [MethodImpl(MethodImplOptions.Synchronized)]
    private void RegisterEventHandler() {
      MNTools.DLog("MNAchievementsProvider:RegisterEventHandler");

      if (eventHandlerRegistered) {
        MNTools.DLog("MNAchievementsProvider:EventHandler already registered");
        return;
      }

      if (Application.platform == RuntimePlatform.Android) {
        eventHandlerRegistered = MNAchievementsProviderUnityClass.CallStatic<bool>("registerEventHandler");
      }
      else {
        throw new MNNotOnDeviceExcepton();
      }
    }

    [MethodImpl(MethodImplOptions.Synchronized)]
    private void UnregisterEventHandler() {
      MNTools.DLog("MNAchievementsProvider:UnregisterEventHandler");

      if (!eventHandlerRegistered) {
        MNTools.DLog("MNAchievementsProvider:EventHandler not registered");
        return;
      }

      if (Application.platform == RuntimePlatform.Android) {
        eventHandlerRegistered = !MNAchievementsProviderUnityClass.CallStatic<bool>("unregisterEventHandler");
      }
      else {
        throw new MNNotOnDeviceExcepton();
      }
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

        MNTools.DetailedLogList("MNUM_onPlayerAchievementUnlocked params",paramsArray);

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

    private bool eventHandlerRegistered = false;
  }
}
