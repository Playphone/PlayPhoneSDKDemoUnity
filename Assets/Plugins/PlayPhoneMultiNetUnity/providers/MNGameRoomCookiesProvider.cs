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
  public class MNGameRoomCookiesProvider : MonoBehaviour
  {
    #region MNGameRoomCookiesProviderEventHandler

    public delegate void GameRoomCookieDownloadSucceededEventHandler(int roomSFId, int key, string cookie);
    public event GameRoomCookieDownloadSucceededEventHandler GameRoomCookieDownloadSucceeded
    {
      [MethodImpl(MethodImplOptions.Synchronized)]
      add
      {
        MNTools.DLog("GameRoomCookieDownloadSucceededEventHandler.add");
        if (GameRoomCookieDownloadSucceededStorage == null) {
          GameRoomCookieDownloadSucceededStorage += value;

          RegisterEventHandler();
        }
        else {
          GameRoomCookieDownloadSucceededStorage += value;
        }
      }

      [MethodImpl(MethodImplOptions.Synchronized)]
      remove
      {
        MNTools.DLog("GameRoomCookieDownloadSucceededEventHandler.remove");
        GameRoomCookieDownloadSucceededStorage -= value;

        if (GameRoomCookieDownloadSucceededStorage == null) {
          UnregisterEventHandler();
        }
      }
    }
    private GameRoomCookieDownloadSucceededEventHandler GameRoomCookieDownloadSucceededStorage;

    public delegate void GameRoomCookieDownloadFailedWithErrorEventHandler(int roomSFId, int key, string error);
    public event GameRoomCookieDownloadFailedWithErrorEventHandler GameRoomCookieDownloadFailedWithError
    {
      [MethodImpl(MethodImplOptions.Synchronized)]
      add
      {
        MNTools.DLog("GameRoomCookieDownloadFailedWithErrorEventHandler.add");
        if (GameRoomCookieDownloadFailedWithErrorStorage == null) {
          GameRoomCookieDownloadFailedWithErrorStorage += value;

          RegisterEventHandler();
        }
        else {
          GameRoomCookieDownloadFailedWithErrorStorage += value;
        }
      }

      [MethodImpl(MethodImplOptions.Synchronized)]
      remove
      {
        MNTools.DLog("GameRoomCookieDownloadFailedWithErrorEventHandler.remove");
        GameRoomCookieDownloadFailedWithErrorStorage -= value;

        if (GameRoomCookieDownloadFailedWithErrorStorage == null) {
          UnregisterEventHandler();
        }
      }
    }
    private GameRoomCookieDownloadFailedWithErrorEventHandler GameRoomCookieDownloadFailedWithErrorStorage;

    public delegate void CurrentGameRoomCookieUpdatedEventHandler(int key, string newCookieValue);
    public event CurrentGameRoomCookieUpdatedEventHandler CurrentGameRoomCookieUpdated
    {
      [MethodImpl(MethodImplOptions.Synchronized)]
      add
      {
        MNTools.DLog("CurrentGameRoomCookieUpdatedEventHandler.add");
        if (CurrentGameRoomCookieUpdatedStorage == null) {
          CurrentGameRoomCookieUpdatedStorage += value;

          RegisterEventHandler();
        }
        else {
          CurrentGameRoomCookieUpdatedStorage += value;
        }
      }

      [MethodImpl(MethodImplOptions.Synchronized)]
      remove
      {
        MNTools.DLog("CurrentGameRoomCookieUpdatedEventHandler.remove");
        CurrentGameRoomCookieUpdatedStorage -= value;

        if (CurrentGameRoomCookieUpdatedStorage == null) {
          UnregisterEventHandler();
        }
      }
    }
    private CurrentGameRoomCookieUpdatedEventHandler CurrentGameRoomCookieUpdatedStorage;

    #endregion MNGameRoomCookiesProviderEventHandler

    #if UNITY_IPHONE

    public void Shutdown() {
      MNTools.DLog("MNGameRoomCookiesProvider:Shutdown");

      if (Application.platform == RuntimePlatform.IPhonePlayer) {
        // Not used in iOS

      }
      else {
        throw new MNNotOnDeviceExcepton();
      }
    }

    public void DownloadGameRoomCookie(int roomSFId, int key) {
      MNTools.DLog("MNGameRoomCookiesProvider:DownloadGameRoomCookie");

      if (Application.platform == RuntimePlatform.IPhonePlayer) {
        _MNGameRoomCookiesProvider_DownloadGameRoomCookie(roomSFId, key);
      }
      else {
        throw new MNNotOnDeviceExcepton();
      }
    }

    public void SetCurrentGameRoomCookie(int key, string cookie) {
      MNTools.DLog("MNGameRoomCookiesProvider:SetCurrentGameRoomCookie");

      if (Application.platform == RuntimePlatform.IPhonePlayer) {
        _MNGameRoomCookiesProvider_SetCurrentGameRoomCookie(key, cookie);
      }
      else {
        throw new MNNotOnDeviceExcepton();
      }
    }

    public string GetCurrentGameRoomCookie(int key) {
      MNTools.DLog("MNGameRoomCookiesProvider:GetCurrentGameRoomCookie");

      if (Application.platform == RuntimePlatform.IPhonePlayer) {
        return _MNGameRoomCookiesProvider_GetCurrentGameRoomCookie(key);
      }
      else {
        throw new MNNotOnDeviceExcepton();
      }
    }

    [MethodImpl(MethodImplOptions.Synchronized)]
    private void RegisterEventHandler() {
      MNTools.DLog("MNGameRoomCookiesProvider:RegisterEventHandler");

      if (eventHandlerRegistered) {
        return;
      }

      if (Application.platform == RuntimePlatform.IPhonePlayer) {
        eventHandlerRegistered = _MNGameRoomCookiesProvider_RegisterEventHandler();
      }
      else {
        throw new MNNotOnDeviceExcepton();
      }
    }

    [MethodImpl(MethodImplOptions.Synchronized)]
    private void UnregisterEventHandler() {
      MNTools.DLog("MNGameRoomCookiesProvider:UnregisterEventHandler");

      if (!eventHandlerRegistered) {
        MNTools.DLog("MNGameRoomCookiesProvider:EventHandler not registered");
        return;
      }

      if (Application.platform == RuntimePlatform.IPhonePlayer) {
        eventHandlerRegistered = !_MNGameRoomCookiesProvider_UnregisterEventHandler();
      }
      else {
        throw new MNNotOnDeviceExcepton();
      }
    }

    #elif UNITY_ANDROID

    public void Shutdown() {
      MNTools.DLog("MNGameRoomCookiesProvider:Shutdown");

      if (Application.platform == RuntimePlatform.Android) {
        MNGameRoomCookiesProviderUnityClass.CallStatic("shutdown");
      }
      else {
        throw new MNNotOnDeviceExcepton();
      }
    }

    public void DownloadGameRoomCookie(int roomSFId, int key) {
      MNTools.DLog("MNGameRoomCookiesProvider:DownloadGameRoomCookie");

      if (Application.platform == RuntimePlatform.Android) {
        MNGameRoomCookiesProviderUnityClass.CallStatic("downloadGameRoomCookie",roomSFId, key);
      }
      else {
        throw new MNNotOnDeviceExcepton();
      }
    }

    public void SetCurrentGameRoomCookie(int key, string cookie) {
      MNTools.DLog("MNGameRoomCookiesProvider:SetCurrentGameRoomCookie");

      if (Application.platform == RuntimePlatform.Android) {
        MNGameRoomCookiesProviderUnityClass.CallStatic("setCurrentGameRoomCookie",key, cookie);
      }
      else {
        throw new MNNotOnDeviceExcepton();
      }
    }

    public string GetCurrentGameRoomCookie(int key) {
      MNTools.DLog("MNGameRoomCookiesProvider:GetCurrentGameRoomCookie");

      if (Application.platform == RuntimePlatform.Android) {
        return MNGameRoomCookiesProviderUnityClass.CallStatic<string>("getCurrentGameRoomCookie",key);
      }
      else {
        throw new MNNotOnDeviceExcepton();
      }
    }

    [MethodImpl(MethodImplOptions.Synchronized)]
    private void RegisterEventHandler() {
      MNTools.DLog("MNGameRoomCookiesProvider:RegisterEventHandler");

      if (eventHandlerRegistered) {
        return;
      }

      if (Application.platform == RuntimePlatform.Android) {
        eventHandlerRegistered = MNGameRoomCookiesProviderUnityClass.CallStatic<bool>("registerEventHandler");
      }
      else {
        throw new MNNotOnDeviceExcepton();
      }
    }

    [MethodImpl(MethodImplOptions.Synchronized)]
    private void UnregisterEventHandler() {
      MNTools.DLog("MNGameRoomCookiesProvider:UnregisterEventHandler");

      if (!eventHandlerRegistered) {
        MNTools.DLog("MNGameRoomCookiesProvider:EventHandler not registered");
        return;
      }

      if (Application.platform == RuntimePlatform.Android) {
        eventHandlerRegistered = !MNGameRoomCookiesProviderUnityClass.CallStatic<bool>("unregisterEventHandler");
      }
      else {
        throw new MNNotOnDeviceExcepton();
      }
    }

    #endif

    #region MNGameRoomCookiesProviderEventHandler Messages
    private void MNUM_onGameRoomCookieDownloadSucceeded(string messageParams) {
      MNTools.DLog("MNGameRoomCookiesProvider:MNUM_onGameRoomCookieDownloadSucceeded: messageParams=<" + messageParams + ">");

      if (GameRoomCookieDownloadSucceededStorage != null) {
        List<object> paramsArray = (List<object>)MNUnityCommunicator.Serializer.Deserialize(messageParams,typeof(List<object>));

        MNTools.DLogList("MNUM_onGameRoomCookieDownloadSucceeded params",paramsArray,MNTools.DEBUG_LEVEL_DETAILED);

        GameRoomCookieDownloadSucceededStorage(Convert.ToInt32(paramsArray[0]), Convert.ToInt32(paramsArray[1]), (string)paramsArray[2]);
      }
    }

    private void MNUM_onGameRoomCookieDownloadFailedWithError(string messageParams) {
      MNTools.DLog("MNGameRoomCookiesProvider:MNUM_onGameRoomCookieDownloadFailedWithError: messageParams=<" + messageParams + ">");

      if (GameRoomCookieDownloadFailedWithErrorStorage != null) {
        List<object> paramsArray = (List<object>)MNUnityCommunicator.Serializer.Deserialize(messageParams,typeof(List<object>));

        MNTools.DLogList("MNUM_onGameRoomCookieDownloadFailedWithError params",paramsArray,MNTools.DEBUG_LEVEL_DETAILED);

        GameRoomCookieDownloadFailedWithErrorStorage(Convert.ToInt32(paramsArray[0]), Convert.ToInt32(paramsArray[1]), (string)paramsArray[2]);
      }
    }

    private void MNUM_onCurrentGameRoomCookieUpdated(string messageParams) {
      MNTools.DLog("MNGameRoomCookiesProvider:MNUM_onCurrentGameRoomCookieUpdated: messageParams=<" + messageParams + ">");

      if (CurrentGameRoomCookieUpdatedStorage != null) {
        List<object> paramsArray = (List<object>)MNUnityCommunicator.Serializer.Deserialize(messageParams,typeof(List<object>));

        MNTools.DLogList("MNUM_onCurrentGameRoomCookieUpdated params",paramsArray,MNTools.DEBUG_LEVEL_DETAILED);

        CurrentGameRoomCookieUpdatedStorage(Convert.ToInt32(paramsArray[0]), (string)paramsArray[1]);
      }
    }

    #endregion MNGameRoomCookiesProviderEventHandler Messages

    #if UNITY_IPHONE

    /*
    // Not used in iOS
    [DllImport ("__Internal")]
    private static extern void _MNGameRoomCookiesProvider_Shutdown ();
    */

    [DllImport ("__Internal")]
    private static extern void _MNGameRoomCookiesProvider_DownloadGameRoomCookie (int roomSFId, int key);

    [DllImport ("__Internal")]
    private static extern void _MNGameRoomCookiesProvider_SetCurrentGameRoomCookie (int key, string cookie);

    [DllImport ("__Internal")]
    private static extern string _MNGameRoomCookiesProvider_GetCurrentGameRoomCookie (int key);

    [DllImport ("__Internal")]
    private static extern bool _MNGameRoomCookiesProvider_RegisterEventHandler ();

    [DllImport ("__Internal")]
    private static extern bool _MNGameRoomCookiesProvider_UnregisterEventHandler ();

    #elif UNITY_ANDROID

    private static AndroidJavaClass mnGameRoomCookiesProviderUnityClass = null;

    private static AndroidJavaClass MNGameRoomCookiesProviderUnityClass
    {
      get {
        MNTools.DLog("AndroidJavaClass MNGameRoomCookiesProviderUnityClass");

        if (mnGameRoomCookiesProviderUnityClass == null) {
          mnGameRoomCookiesProviderUnityClass = new AndroidJavaClass("com.playphone.multinet.unity.MNGameRoomCookiesProviderUnity");
        }

        return mnGameRoomCookiesProviderUnityClass;
      }
    }

    #endif

    private MNGameRoomCookiesProvider()
    {
      MNTools.DLog("MNGameRoomCookiesProvider:MNGameRoomCookiesProvider()");
    }

    private bool eventHandlerRegistered = false;
  }
}
