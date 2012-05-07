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
  public class MNGameCookiesProvider : MonoBehaviour
  {
    #region MNGameCookiesProviderEventHandler

    public delegate void GameCookieDownloadSucceededEventHandler(int key, string cookie);
    public event GameCookieDownloadSucceededEventHandler GameCookieDownloadSucceeded
    {
      [MethodImpl(MethodImplOptions.Synchronized)]
      add
      {
        MNTools.DLog("GameCookieDownloadSucceededEventHandler.add");
        if (GameCookieDownloadSucceededStorage == null) {
          GameCookieDownloadSucceededStorage += value;

          RegisterEventHandler();
        }
        else {
          GameCookieDownloadSucceededStorage += value;
        }
      }

      [MethodImpl(MethodImplOptions.Synchronized)]
      remove
      {
        MNTools.DLog("GameCookieDownloadSucceededEventHandler.remove");
        GameCookieDownloadSucceededStorage -= value;

        if (GameCookieDownloadSucceededStorage == null) {
          UnregisterEventHandler();
        }
      }
    }
    private GameCookieDownloadSucceededEventHandler GameCookieDownloadSucceededStorage;

    public delegate void GameCookieDownloadFailedWithErrorEventHandler(int key, string error);
    public event GameCookieDownloadFailedWithErrorEventHandler GameCookieDownloadFailedWithError
    {
      [MethodImpl(MethodImplOptions.Synchronized)]
      add
      {
        MNTools.DLog("GameCookieDownloadFailedWithErrorEventHandler.add");
        if (GameCookieDownloadFailedWithErrorStorage == null) {
          GameCookieDownloadFailedWithErrorStorage += value;

          RegisterEventHandler();
        }
        else {
          GameCookieDownloadFailedWithErrorStorage += value;
        }
      }

      [MethodImpl(MethodImplOptions.Synchronized)]
      remove
      {
        MNTools.DLog("GameCookieDownloadFailedWithErrorEventHandler.remove");
        GameCookieDownloadFailedWithErrorStorage -= value;

        if (GameCookieDownloadFailedWithErrorStorage == null) {
          UnregisterEventHandler();
        }
      }
    }
    private GameCookieDownloadFailedWithErrorEventHandler GameCookieDownloadFailedWithErrorStorage;

    public delegate void GameCookieUploadSucceededEventHandler(int key);
    public event GameCookieUploadSucceededEventHandler GameCookieUploadSucceeded
    {
      [MethodImpl(MethodImplOptions.Synchronized)]
      add
      {
        MNTools.DLog("GameCookieUploadSucceededEventHandler.add");
        if (GameCookieUploadSucceededStorage == null) {
          GameCookieUploadSucceededStorage += value;

          RegisterEventHandler();
        }
        else {
          GameCookieUploadSucceededStorage += value;
        }
      }

      [MethodImpl(MethodImplOptions.Synchronized)]
      remove
      {
        MNTools.DLog("GameCookieUploadSucceededEventHandler.remove");
        GameCookieUploadSucceededStorage -= value;

        if (GameCookieUploadSucceededStorage == null) {
          UnregisterEventHandler();
        }
      }
    }
    private GameCookieUploadSucceededEventHandler GameCookieUploadSucceededStorage;

    public delegate void GameCookieUploadFailedWithErrorEventHandler(int key, string error);
    public event GameCookieUploadFailedWithErrorEventHandler GameCookieUploadFailedWithError
    {
      [MethodImpl(MethodImplOptions.Synchronized)]
      add
      {
        MNTools.DLog("GameCookieUploadFailedWithErrorEventHandler.add");
        if (GameCookieUploadFailedWithErrorStorage == null) {
          GameCookieUploadFailedWithErrorStorage += value;

          RegisterEventHandler();
        }
        else {
          GameCookieUploadFailedWithErrorStorage += value;
        }
      }

      [MethodImpl(MethodImplOptions.Synchronized)]
      remove
      {
        MNTools.DLog("GameCookieUploadFailedWithErrorEventHandler.remove");
        GameCookieUploadFailedWithErrorStorage -= value;

        if (GameCookieUploadFailedWithErrorStorage == null) {
          UnregisterEventHandler();
        }
      }
    }
    private GameCookieUploadFailedWithErrorEventHandler GameCookieUploadFailedWithErrorStorage;

    #endregion MNGameCookiesProviderEventHandler

    #if UNITY_IPHONE

    public void Shutdown() {
      MNTools.DLog("MNGameCookiesProvider:Shutdown");

      if (Application.platform == RuntimePlatform.IPhonePlayer) {
        // Not used in iOS

      }
      else {
        throw new MNNotOnDeviceExcepton();
      }
    }

    public void DownloadUserCookie(int key) {
      MNTools.DLog("MNGameCookiesProvider:DownloadUserCookie");

      if (Application.platform == RuntimePlatform.IPhonePlayer) {
        _MNGameCookiesProvider_DownloadUserCookie(key);
      }
      else {
        throw new MNNotOnDeviceExcepton();
      }
    }

    public void UploadUserCookie(int key, string cookie) {
      MNTools.DLog("MNGameCookiesProvider:UploadUserCookie");

      if (Application.platform == RuntimePlatform.IPhonePlayer) {
        _MNGameCookiesProvider_UploadUserCookie(key, cookie);
      }
      else {
        throw new MNNotOnDeviceExcepton();
      }
    }

    [MethodImpl(MethodImplOptions.Synchronized)]
    private void RegisterEventHandler() {
      if (eventHandlerRegistered) {
        return;
      }

      MNTools.DLog("MNGameCookiesProvider:RegisterEventHandler");

      if (Application.platform == RuntimePlatform.IPhonePlayer) {
        eventHandlerRegistered = _MNGameCookiesProvider_RegisterEventHandler();
      }
      else {
        throw new MNNotOnDeviceExcepton();
      }
    }

    [MethodImpl(MethodImplOptions.Synchronized)]
    private void UnregisterEventHandler() {
      if ((GameCookieDownloadSucceededStorage != null) || (GameCookieDownloadFailedWithErrorStorage != null) || (GameCookieUploadSucceededStorage != null) || (GameCookieUploadFailedWithErrorStorage != null)) {
        return;
      }

      MNTools.DLog("MNGameCookiesProvider:UnregisterEventHandler");

      if (!eventHandlerRegistered) {
        MNTools.DLog("MNGameCookiesProvider:EventHandler not registered");
        return;
      }

      if (Application.platform == RuntimePlatform.IPhonePlayer) {
        eventHandlerRegistered = !_MNGameCookiesProvider_UnregisterEventHandler();
      }
      else {
        throw new MNNotOnDeviceExcepton();
      }
    }

    #elif UNITY_ANDROID

    public void Shutdown() {
      MNTools.DLog("MNGameCookiesProvider:Shutdown");

      if (Application.platform == RuntimePlatform.Android) {
        MNGameCookiesProviderUnityClass.CallStatic("shutdown");
      }
      else {
        throw new MNNotOnDeviceExcepton();
      }
    }

    public void DownloadUserCookie(int key) {
      MNTools.DLog("MNGameCookiesProvider:DownloadUserCookie");

      if (Application.platform == RuntimePlatform.Android) {
        MNGameCookiesProviderUnityClass.CallStatic("downloadUserCookie",key);
      }
      else {
        throw new MNNotOnDeviceExcepton();
      }
    }

    public void UploadUserCookie(int key, string cookie) {
      MNTools.DLog("MNGameCookiesProvider:UploadUserCookie");

      if (Application.platform == RuntimePlatform.Android) {
        MNGameCookiesProviderUnityClass.CallStatic("uploadUserCookie",key, cookie);
      }
      else {
        throw new MNNotOnDeviceExcepton();
      }
    }

    [MethodImpl(MethodImplOptions.Synchronized)]
    private void RegisterEventHandler() {
      if (eventHandlerRegistered) {
        return;
      }

      MNTools.DLog("MNGameCookiesProvider:RegisterEventHandler");

      if (Application.platform == RuntimePlatform.Android) {
        eventHandlerRegistered = MNGameCookiesProviderUnityClass.CallStatic<bool>("registerEventHandler");
      }
      else {
        throw new MNNotOnDeviceExcepton();
      }
    }

    [MethodImpl(MethodImplOptions.Synchronized)]
    private void UnregisterEventHandler() {
      if ((GameCookieDownloadSucceededStorage != null) || (GameCookieDownloadFailedWithErrorStorage != null) || (GameCookieUploadSucceededStorage != null) || (GameCookieUploadFailedWithErrorStorage != null)) {
        return;
      }

      MNTools.DLog("MNGameCookiesProvider:UnregisterEventHandler");

      if (!eventHandlerRegistered) {
        MNTools.DLog("MNGameCookiesProvider:EventHandler not registered");
        return;
      }

      if (Application.platform == RuntimePlatform.Android) {
        eventHandlerRegistered = !MNGameCookiesProviderUnityClass.CallStatic<bool>("unregisterEventHandler");
      }
      else {
        throw new MNNotOnDeviceExcepton();
      }
    }

    #endif

    #region MNGameCookiesProviderEventHandler Messages
    private void MNUM_onGameCookieDownloadSucceeded(string messageParams) {
      MNTools.DLog("MNGameCookiesProvider:MNUM_onGameCookieDownloadSucceeded: messageParams=<" + messageParams + ">");

      if (GameCookieDownloadSucceededStorage != null) {
        List<object> paramsArray = (List<object>)MNUnityCommunicator.Serializer.Deserialize(messageParams,typeof(List<object>));

        MNTools.DLogList("MNUM_onGameCookieDownloadSucceeded params",paramsArray,MNTools.DEBUG_LEVEL_DETAILED);

        GameCookieDownloadSucceededStorage(Convert.ToInt32(paramsArray[0]), (string)paramsArray[1]);
      }
    }

    private void MNUM_onGameCookieDownloadFailedWithError(string messageParams) {
      MNTools.DLog("MNGameCookiesProvider:MNUM_onGameCookieDownloadFailedWithError: messageParams=<" + messageParams + ">");

      if (GameCookieDownloadFailedWithErrorStorage != null) {
        List<object> paramsArray = (List<object>)MNUnityCommunicator.Serializer.Deserialize(messageParams,typeof(List<object>));

        MNTools.DLogList("MNUM_onGameCookieDownloadFailedWithError params",paramsArray,MNTools.DEBUG_LEVEL_DETAILED);

        GameCookieDownloadFailedWithErrorStorage(Convert.ToInt32(paramsArray[0]), (string)paramsArray[1]);
      }
    }

    private void MNUM_onGameCookieUploadSucceeded(string messageParams) {
      MNTools.DLog("MNGameCookiesProvider:MNUM_onGameCookieUploadSucceeded: messageParams=<" + messageParams + ">");

      if (GameCookieUploadSucceededStorage != null) {
        List<object> paramsArray = (List<object>)MNUnityCommunicator.Serializer.Deserialize(messageParams,typeof(List<object>));

        MNTools.DLogList("MNUM_onGameCookieUploadSucceeded params",paramsArray,MNTools.DEBUG_LEVEL_DETAILED);

        GameCookieUploadSucceededStorage(Convert.ToInt32(paramsArray[0]));
      }
    }

    private void MNUM_onGameCookieUploadFailedWithError(string messageParams) {
      MNTools.DLog("MNGameCookiesProvider:MNUM_onGameCookieUploadFailedWithError: messageParams=<" + messageParams + ">");

      if (GameCookieUploadFailedWithErrorStorage != null) {
        List<object> paramsArray = (List<object>)MNUnityCommunicator.Serializer.Deserialize(messageParams,typeof(List<object>));

        MNTools.DLogList("MNUM_onGameCookieUploadFailedWithError params",paramsArray,MNTools.DEBUG_LEVEL_DETAILED);

        GameCookieUploadFailedWithErrorStorage(Convert.ToInt32(paramsArray[0]), (string)paramsArray[1]);
      }
    }

    #endregion MNGameCookiesProviderEventHandler Messages

    #if UNITY_IPHONE

    /*
    // Not used in iOS
    [DllImport ("__Internal")]
    private static extern void _MNGameCookiesProvider_Shutdown ();
    */

    [DllImport ("__Internal")]
    private static extern void _MNGameCookiesProvider_DownloadUserCookie (int key);

    [DllImport ("__Internal")]
    private static extern void _MNGameCookiesProvider_UploadUserCookie (int key, string cookie);

    [DllImport ("__Internal")]
    private static extern bool _MNGameCookiesProvider_RegisterEventHandler ();

    [DllImport ("__Internal")]
    private static extern bool _MNGameCookiesProvider_UnregisterEventHandler ();

    #elif UNITY_ANDROID

    private static AndroidJavaClass mnGameCookiesProviderUnityClass = null;

    private static AndroidJavaClass MNGameCookiesProviderUnityClass
    {
      get {
        MNTools.DLog("AndroidJavaClass MNGameCookiesProviderUnityClass");

        if (mnGameCookiesProviderUnityClass == null) {
          mnGameCookiesProviderUnityClass = new AndroidJavaClass("com.playphone.multinet.unity.MNGameCookiesProviderUnity");
        }

        return mnGameCookiesProviderUnityClass;
      }
    }

    #endif

    private MNGameCookiesProvider()
    {
      MNTools.DLog("MNGameCookiesProvider:MNGameCookiesProvider()");
    }

    private bool eventHandlerRegistered = false;
  }
}
