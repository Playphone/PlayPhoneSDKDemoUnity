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
  public class MNGameSettingsProvider : MonoBehaviour
  {
    public class GameSettingInfo
    {
      public int Id {get;internal set;}
      public string Name {get;internal set;}
      public string Params {get;internal set;}
      public string SysParams {get;internal set;}
      public bool MultiplayerEnabled {get;internal set;}
      public bool LeaderboardVisible {get;internal set;}

      public GameSettingInfo () {}
    }

    #region MNGameSettingsProviderEventHandler

    public delegate void GameSettingListUpdatedEventHandler();
    public event GameSettingListUpdatedEventHandler GameSettingListUpdated
    {
      [MethodImpl(MethodImplOptions.Synchronized)]
      add
      {
        MNTools.DLog("GameSettingListUpdatedEventHandler.add");
        if (GameSettingListUpdatedStorage == null) {
          GameSettingListUpdatedStorage += value;

          RegisterEventHandler();
        }
        else {
          GameSettingListUpdatedStorage += value;
        }
      }

      [MethodImpl(MethodImplOptions.Synchronized)]
      remove
      {
        MNTools.DLog("GameSettingListUpdatedEventHandler.remove");
        GameSettingListUpdatedStorage -= value;

        if (GameSettingListUpdatedStorage == null) {
          UnregisterEventHandler();
        }
      }
    }
    private GameSettingListUpdatedEventHandler GameSettingListUpdatedStorage;

    #endregion MNGameSettingsProviderEventHandler

    #if UNITY_IPHONE

    public void Shutdown() {
      MNTools.DLog("MNGameSettingsProvider:Shutdown");

      if (Application.platform == RuntimePlatform.IPhonePlayer) {
        // Not used in iOS

      }
      else {
        throw new MNNotOnDeviceExcepton();
      }
    }

    public MNGameSettingsProvider.GameSettingInfo[] GetGameSettingList() {
      MNTools.DLog("MNGameSettingsProvider:GetGameSettingList");

      if (Application.platform == RuntimePlatform.IPhonePlayer) {
        List<object> deserializedArray = MNUnityCommunicator.Serializer.DeserializeArray(
          _MNGameSettingsProvider_GetGameSettingList(),
          typeof(MNGameSettingsProvider.GameSettingInfo));

        MNGameSettingsProvider.GameSettingInfo[] resultArray = new MNGameSettingsProvider.GameSettingInfo[deserializedArray.Count];

        for (int index = 0;index < deserializedArray.Count;index++) {
          resultArray[index] = (MNGameSettingsProvider.GameSettingInfo)(deserializedArray[index]);
        }

        return resultArray;
      }
      else {
        throw new MNNotOnDeviceExcepton();
      }
    }

    public MNGameSettingsProvider.GameSettingInfo FindGameSettingById(int gameSetId) {
      MNTools.DLog("MNGameSettingsProvider:FindGameSettingById");

      if (Application.platform == RuntimePlatform.IPhonePlayer) {
        return (MNGameSettingsProvider.GameSettingInfo)MNUnityCommunicator.Serializer.Deserialize(_MNGameSettingsProvider_FindGameSettingById(gameSetId),typeof(MNGameSettingsProvider.GameSettingInfo));
      }
      else {
        throw new MNNotOnDeviceExcepton();
      }
    }

    public bool IsGameSettingListNeedUpdate() {
      MNTools.DLog("MNGameSettingsProvider:IsGameSettingListNeedUpdate");

      if (Application.platform == RuntimePlatform.IPhonePlayer) {
        return _MNGameSettingsProvider_IsGameSettingListNeedUpdate();
      }
      else {
        throw new MNNotOnDeviceExcepton();
      }
    }

    public void DoGameSettingListUpdate() {
      MNTools.DLog("MNGameSettingsProvider:DoGameSettingListUpdate");

      if (Application.platform == RuntimePlatform.IPhonePlayer) {
        _MNGameSettingsProvider_DoGameSettingListUpdate();
      }
      else {
        throw new MNNotOnDeviceExcepton();
      }
    }

    [MethodImpl(MethodImplOptions.Synchronized)]
    private void RegisterEventHandler() {
      MNTools.DLog("MNGameSettingsProvider:RegisterEventHandler");

      if (eventHandlerRegistered) {
        MNTools.DLog("MNGameSettingsProvider:EventHandler already registered");
        return;
      }

      if (Application.platform == RuntimePlatform.IPhonePlayer) {
        eventHandlerRegistered = _MNGameSettingsProvider_RegisterEventHandler();
      }
      else {
        throw new MNNotOnDeviceExcepton();
      }
    }

    [MethodImpl(MethodImplOptions.Synchronized)]
    private void UnregisterEventHandler() {
      MNTools.DLog("MNGameSettingsProvider:UnregisterEventHandler");

      if (!eventHandlerRegistered) {
        MNTools.DLog("MNGameSettingsProvider:EventHandler not registered");
        return;
      }

      if (Application.platform == RuntimePlatform.IPhonePlayer) {
        eventHandlerRegistered = !_MNGameSettingsProvider_UnregisterEventHandler();
      }
      else {
        throw new MNNotOnDeviceExcepton();
      }
    }

    #elif UNITY_ANDROID

    public void Shutdown() {
      MNTools.DLog("MNGameSettingsProvider:Shutdown");

      if (Application.platform == RuntimePlatform.Android) {
        MNGameSettingsProviderUnityClass.CallStatic("shutdown");
      }
      else {
        throw new MNNotOnDeviceExcepton();
      }
    }

    public MNGameSettingsProvider.GameSettingInfo[] GetGameSettingList() {
      MNTools.DLog("MNGameSettingsProvider:GetGameSettingList");

      if (Application.platform == RuntimePlatform.Android) {
        List<object> deserializedArray = MNUnityCommunicator.Serializer.DeserializeArray(
          MNGameSettingsProviderUnityClass.CallStatic<string>("getGameSettingList"),
          typeof(MNGameSettingsProvider.GameSettingInfo));

        MNGameSettingsProvider.GameSettingInfo[] resultArray = new MNGameSettingsProvider.GameSettingInfo[deserializedArray.Count];

        for (int index = 0;index < deserializedArray.Count;index++) {
          resultArray[index] = (MNGameSettingsProvider.GameSettingInfo)(deserializedArray[index]);
        }

        return resultArray;
      }
      else {
        throw new MNNotOnDeviceExcepton();
      }
    }

    public MNGameSettingsProvider.GameSettingInfo FindGameSettingById(int gameSetId) {
      MNTools.DLog("MNGameSettingsProvider:FindGameSettingById");

      if (Application.platform == RuntimePlatform.Android) {
        return (MNGameSettingsProvider.GameSettingInfo)MNUnityCommunicator.Serializer.Deserialize(MNGameSettingsProviderUnityClass.CallStatic<string>("findGameSettingById",gameSetId),typeof(MNGameSettingsProvider.GameSettingInfo));
      }
      else {
        throw new MNNotOnDeviceExcepton();
      }
    }

    public bool IsGameSettingListNeedUpdate() {
      MNTools.DLog("MNGameSettingsProvider:IsGameSettingListNeedUpdate");

      if (Application.platform == RuntimePlatform.Android) {
        return MNGameSettingsProviderUnityClass.CallStatic<bool>("isGameSettingListNeedUpdate");
      }
      else {
        throw new MNNotOnDeviceExcepton();
      }
    }

    public void DoGameSettingListUpdate() {
      MNTools.DLog("MNGameSettingsProvider:DoGameSettingListUpdate");

      if (Application.platform == RuntimePlatform.Android) {
        MNGameSettingsProviderUnityClass.CallStatic("doGameSettingListUpdate");
      }
      else {
        throw new MNNotOnDeviceExcepton();
      }
    }

    [MethodImpl(MethodImplOptions.Synchronized)]
    private void RegisterEventHandler() {
      MNTools.DLog("MNGameSettingsProvider:RegisterEventHandler");

      if (eventHandlerRegistered) {
        MNTools.DLog("MNGameSettingsProvider:EventHandler already registered");
        return;
      }

      if (Application.platform == RuntimePlatform.Android) {
        eventHandlerRegistered = MNGameSettingsProviderUnityClass.CallStatic<bool>("registerEventHandler");
      }
      else {
        throw new MNNotOnDeviceExcepton();
      }
    }

    [MethodImpl(MethodImplOptions.Synchronized)]
    private void UnregisterEventHandler() {
      MNTools.DLog("MNGameSettingsProvider:UnregisterEventHandler");

      if (!eventHandlerRegistered) {
        MNTools.DLog("MNGameSettingsProvider:EventHandler not registered");
        return;
      }

      if (Application.platform == RuntimePlatform.Android) {
        eventHandlerRegistered = !MNGameSettingsProviderUnityClass.CallStatic<bool>("unregisterEventHandler");
      }
      else {
        throw new MNNotOnDeviceExcepton();
      }
    }

    #endif

    #region MNGameSettingsProviderEventHandler Messages
    private void MNUM_onGameSettingListUpdated(string messageParams) {
      MNTools.DLog("MNGameSettingsProvider:MNUM_onGameSettingListUpdated: messageParams=<" + messageParams + ">");

      if (GameSettingListUpdatedStorage != null) {
        GameSettingListUpdatedStorage();
      }
    }

    #endregion MNGameSettingsProviderEventHandler Messages

    #if UNITY_IPHONE

    /*
    // Not used in iOS
    [DllImport ("__Internal")]
    private static extern void _MNGameSettingsProvider_Shutdown ();
    */

    [DllImport ("__Internal")]
    private static extern string _MNGameSettingsProvider_GetGameSettingList ();

    [DllImport ("__Internal")]
    private static extern string _MNGameSettingsProvider_FindGameSettingById (int gameSetId);

    [DllImport ("__Internal")]
    private static extern bool _MNGameSettingsProvider_IsGameSettingListNeedUpdate ();

    [DllImport ("__Internal")]
    private static extern void _MNGameSettingsProvider_DoGameSettingListUpdate ();

    [DllImport ("__Internal")]
    private static extern bool _MNGameSettingsProvider_RegisterEventHandler ();

    [DllImport ("__Internal")]
    private static extern bool _MNGameSettingsProvider_UnregisterEventHandler ();

    #elif UNITY_ANDROID

    private static AndroidJavaClass mnGameSettingsProviderUnityClass = null;

    private static AndroidJavaClass MNGameSettingsProviderUnityClass
    {
      get {
        MNTools.DLog("AndroidJavaClass MNGameSettingsProviderUnityClass");

        if (mnGameSettingsProviderUnityClass == null) {
          mnGameSettingsProviderUnityClass = new AndroidJavaClass("com.playphone.multinet.unity.MNGameSettingsProviderUnity");
        }

        return mnGameSettingsProviderUnityClass;
      }
    }

    #endif

    private MNGameSettingsProvider()
    {
      MNTools.DLog("MNGameSettingsProvider:MNGameSettingsProvider()");
    }

    private bool eventHandlerRegistered = false;
  }
}
