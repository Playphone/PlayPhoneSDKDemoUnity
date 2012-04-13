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
  public class MNServerInfoProvider : MonoBehaviour
  {
    public const int SERVER_TIME_INFO_KEY = 1;

    #region MNServerInfoProviderEventHandler

    public delegate void ServerInfoItemReceivedEventHandler(int key, string _value);
    public event ServerInfoItemReceivedEventHandler ServerInfoItemReceived
    {
      [MethodImpl(MethodImplOptions.Synchronized)]
      add
      {
        MNTools.DLog("ServerInfoItemReceivedEventHandler.add");
        if (ServerInfoItemReceivedStorage == null) {
          ServerInfoItemReceivedStorage += value;

          RegisterEventHandler();
        }
        else {
          ServerInfoItemReceivedStorage += value;
        }
      }

      [MethodImpl(MethodImplOptions.Synchronized)]
      remove
      {
        MNTools.DLog("ServerInfoItemReceivedEventHandler.remove");
        ServerInfoItemReceivedStorage -= value;

        if (ServerInfoItemReceivedStorage == null) {
          UnregisterEventHandler();
        }
      }
    }
    private ServerInfoItemReceivedEventHandler ServerInfoItemReceivedStorage;

    public delegate void ServerInfoItemRequestFailedWithErrorEventHandler(int key, string error);
    public event ServerInfoItemRequestFailedWithErrorEventHandler ServerInfoItemRequestFailedWithError
    {
      [MethodImpl(MethodImplOptions.Synchronized)]
      add
      {
        MNTools.DLog("ServerInfoItemRequestFailedWithErrorEventHandler.add");
        if (ServerInfoItemRequestFailedWithErrorStorage == null) {
          ServerInfoItemRequestFailedWithErrorStorage += value;

          RegisterEventHandler();
        }
        else {
          ServerInfoItemRequestFailedWithErrorStorage += value;
        }
      }

      [MethodImpl(MethodImplOptions.Synchronized)]
      remove
      {
        MNTools.DLog("ServerInfoItemRequestFailedWithErrorEventHandler.remove");
        ServerInfoItemRequestFailedWithErrorStorage -= value;

        if (ServerInfoItemRequestFailedWithErrorStorage == null) {
          UnregisterEventHandler();
        }
      }
    }
    private ServerInfoItemRequestFailedWithErrorEventHandler ServerInfoItemRequestFailedWithErrorStorage;

    #endregion MNServerInfoProviderEventHandler

    #if UNITY_IPHONE

    public void Shutdown() {
      MNTools.DLog("MNServerInfoProvider:Shutdown");

      if (Application.platform == RuntimePlatform.IPhonePlayer) {
        // Not used in iOS

      }
      else {
        throw new MNNotOnDeviceExcepton();
      }
    }

    public void RequestServerInfoItem(int key) {
      MNTools.DLog("MNServerInfoProvider:RequestServerInfoItem");

      if (Application.platform == RuntimePlatform.IPhonePlayer) {
        _MNServerInfoProvider_RequestServerInfoItem(key);
      }
      else {
        throw new MNNotOnDeviceExcepton();
      }
    }

    [MethodImpl(MethodImplOptions.Synchronized)]
    private void RegisterEventHandler() {
      MNTools.DLog("MNServerInfoProvider:RegisterEventHandler");

      if (eventHandlerRegistered) {
        return;
      }

      if (Application.platform == RuntimePlatform.IPhonePlayer) {
        eventHandlerRegistered = _MNServerInfoProvider_RegisterEventHandler();
      }
      else {
        throw new MNNotOnDeviceExcepton();
      }
    }

    [MethodImpl(MethodImplOptions.Synchronized)]
    private void UnregisterEventHandler() {
      MNTools.DLog("MNServerInfoProvider:UnregisterEventHandler");

      if (!eventHandlerRegistered) {
        MNTools.DLog("MNServerInfoProvider:EventHandler not registered");
        return;
      }

      if (Application.platform == RuntimePlatform.IPhonePlayer) {
        eventHandlerRegistered = !_MNServerInfoProvider_UnregisterEventHandler();
      }
      else {
        throw new MNNotOnDeviceExcepton();
      }
    }

    #elif UNITY_ANDROID

    public void Shutdown() {
      MNTools.DLog("MNServerInfoProvider:Shutdown");

      if (Application.platform == RuntimePlatform.Android) {
        MNServerInfoProviderUnityClass.CallStatic("shutdown");
      }
      else {
        throw new MNNotOnDeviceExcepton();
      }
    }

    public void RequestServerInfoItem(int key) {
      MNTools.DLog("MNServerInfoProvider:RequestServerInfoItem");

      if (Application.platform == RuntimePlatform.Android) {
        MNServerInfoProviderUnityClass.CallStatic("requestServerInfoItem",key);
      }
      else {
        throw new MNNotOnDeviceExcepton();
      }
    }

    [MethodImpl(MethodImplOptions.Synchronized)]
    private void RegisterEventHandler() {
      MNTools.DLog("MNServerInfoProvider:RegisterEventHandler");

      if (eventHandlerRegistered) {
        return;
      }

      if (Application.platform == RuntimePlatform.Android) {
        eventHandlerRegistered = MNServerInfoProviderUnityClass.CallStatic<bool>("registerEventHandler");
      }
      else {
        throw new MNNotOnDeviceExcepton();
      }
    }

    [MethodImpl(MethodImplOptions.Synchronized)]
    private void UnregisterEventHandler() {
      MNTools.DLog("MNServerInfoProvider:UnregisterEventHandler");

      if (!eventHandlerRegistered) {
        MNTools.DLog("MNServerInfoProvider:EventHandler not registered");
        return;
      }

      if (Application.platform == RuntimePlatform.Android) {
        eventHandlerRegistered = !MNServerInfoProviderUnityClass.CallStatic<bool>("unregisterEventHandler");
      }
      else {
        throw new MNNotOnDeviceExcepton();
      }
    }

    #endif

    #region MNServerInfoProviderEventHandler Messages
    private void MNUM_onServerInfoItemReceived(string messageParams) {
      MNTools.DLog("MNServerInfoProvider:MNUM_onServerInfoItemReceived: messageParams=<" + messageParams + ">");

      if (ServerInfoItemReceivedStorage != null) {
        List<object> paramsArray = (List<object>)MNUnityCommunicator.Serializer.Deserialize(messageParams,typeof(List<object>));

        MNTools.DetailedLogList("MNUM_onServerInfoItemReceived params",paramsArray);

        ServerInfoItemReceivedStorage(Convert.ToInt32(paramsArray[0]), (string)paramsArray[1]);
      }
    }

    private void MNUM_onServerInfoItemRequestFailedWithError(string messageParams) {
      MNTools.DLog("MNServerInfoProvider:MNUM_onServerInfoItemRequestFailedWithError: messageParams=<" + messageParams + ">");

      if (ServerInfoItemRequestFailedWithErrorStorage != null) {
        List<object> paramsArray = (List<object>)MNUnityCommunicator.Serializer.Deserialize(messageParams,typeof(List<object>));

        MNTools.DetailedLogList("MNUM_onServerInfoItemRequestFailedWithError params",paramsArray);

        ServerInfoItemRequestFailedWithErrorStorage(Convert.ToInt32(paramsArray[0]), (string)paramsArray[1]);
      }
    }

    #endregion MNServerInfoProviderEventHandler Messages

    #if UNITY_IPHONE

    /*
    // Not used in iOS
    [DllImport ("__Internal")]
    private static extern void _MNServerInfoProvider_Shutdown ();
    */

    [DllImport ("__Internal")]
    private static extern void _MNServerInfoProvider_RequestServerInfoItem (int key);

    [DllImport ("__Internal")]
    private static extern bool _MNServerInfoProvider_RegisterEventHandler ();

    [DllImport ("__Internal")]
    private static extern bool _MNServerInfoProvider_UnregisterEventHandler ();

    #elif UNITY_ANDROID

    private static AndroidJavaClass mnServerInfoProviderUnityClass = null;

    private static AndroidJavaClass MNServerInfoProviderUnityClass
    {
      get {
        MNTools.DLog("AndroidJavaClass MNServerInfoProviderUnityClass");

        if (mnServerInfoProviderUnityClass == null) {
          mnServerInfoProviderUnityClass = new AndroidJavaClass("com.playphone.multinet.unity.MNServerInfoProviderUnity");
        }

        return mnServerInfoProviderUnityClass;
      }
    }

    #endif

    private MNServerInfoProvider()
    {
      MNTools.DLog("MNServerInfoProvider:MNServerInfoProvider()");
    }

    private bool eventHandlerRegistered = false;
  }
}
