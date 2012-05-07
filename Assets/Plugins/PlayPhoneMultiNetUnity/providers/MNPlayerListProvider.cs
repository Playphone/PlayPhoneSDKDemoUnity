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
  public class MNPlayerListProvider : MonoBehaviour
  {
    #region MNPlayerListProviderEventHandler

    public delegate void PlayerJoinEventHandler(MNUserInfo player);
    public event PlayerJoinEventHandler PlayerJoin
    {
      [MethodImpl(MethodImplOptions.Synchronized)]
      add
      {
        MNTools.DLog("PlayerJoinEventHandler.add");
        if (PlayerJoinStorage == null) {
          PlayerJoinStorage += value;

          RegisterEventHandler();
        }
        else {
          PlayerJoinStorage += value;
        }
      }

      [MethodImpl(MethodImplOptions.Synchronized)]
      remove
      {
        MNTools.DLog("PlayerJoinEventHandler.remove");
        PlayerJoinStorage -= value;

        if (PlayerJoinStorage == null) {
          UnregisterEventHandler();
        }
      }
    }
    private PlayerJoinEventHandler PlayerJoinStorage;

    public delegate void PlayerLeftEventHandler(MNUserInfo player);
    public event PlayerLeftEventHandler PlayerLeft
    {
      [MethodImpl(MethodImplOptions.Synchronized)]
      add
      {
        MNTools.DLog("PlayerLeftEventHandler.add");
        if (PlayerLeftStorage == null) {
          PlayerLeftStorage += value;

          RegisterEventHandler();
        }
        else {
          PlayerLeftStorage += value;
        }
      }

      [MethodImpl(MethodImplOptions.Synchronized)]
      remove
      {
        MNTools.DLog("PlayerLeftEventHandler.remove");
        PlayerLeftStorage -= value;

        if (PlayerLeftStorage == null) {
          UnregisterEventHandler();
        }
      }
    }
    private PlayerLeftEventHandler PlayerLeftStorage;

    #endregion MNPlayerListProviderEventHandler

    #if UNITY_IPHONE

    public void Shutdown() {
      MNTools.DLog("MNPlayerListProvider:Shutdown");

      if (Application.platform == RuntimePlatform.IPhonePlayer) {
        // Not used in iOS

      }
      else {
        throw new MNNotOnDeviceExcepton();
      }
    }

    public MNUserInfo[] GetPlayerList() {
      MNTools.DLog("MNPlayerListProvider:GetPlayerList");

      if (Application.platform == RuntimePlatform.IPhonePlayer) {
        List<object> deserializedArray = MNUnityCommunicator.Serializer.DeserializeArray(
          _MNPlayerListProvider_GetPlayerList(),
          typeof(MNUserInfo));

        MNUserInfo[] resultArray = new MNUserInfo[deserializedArray.Count];

        for (int index = 0;index < deserializedArray.Count;index++) {
          resultArray[index] = (MNUserInfo)(deserializedArray[index]);
        }

        return resultArray;
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

      MNTools.DLog("MNPlayerListProvider:RegisterEventHandler");

      if (Application.platform == RuntimePlatform.IPhonePlayer) {
        eventHandlerRegistered = _MNPlayerListProvider_RegisterEventHandler();
      }
      else {
        throw new MNNotOnDeviceExcepton();
      }
    }

    [MethodImpl(MethodImplOptions.Synchronized)]
    private void UnregisterEventHandler() {
      if ((PlayerJoinStorage != null) || (PlayerLeftStorage != null)) {
        return;
      }

      MNTools.DLog("MNPlayerListProvider:UnregisterEventHandler");

      if (!eventHandlerRegistered) {
        MNTools.DLog("MNPlayerListProvider:EventHandler not registered");
        return;
      }

      if (Application.platform == RuntimePlatform.IPhonePlayer) {
        eventHandlerRegistered = !_MNPlayerListProvider_UnregisterEventHandler();
      }
      else {
        throw new MNNotOnDeviceExcepton();
      }
    }

    #elif UNITY_ANDROID

    public void Shutdown() {
      MNTools.DLog("MNPlayerListProvider:Shutdown");

      if (Application.platform == RuntimePlatform.Android) {
        MNPlayerListProviderUnityClass.CallStatic("shutdown");
      }
      else {
        throw new MNNotOnDeviceExcepton();
      }
    }

    public MNUserInfo[] GetPlayerList() {
      MNTools.DLog("MNPlayerListProvider:GetPlayerList");

      if (Application.platform == RuntimePlatform.Android) {
        List<object> deserializedArray = MNUnityCommunicator.Serializer.DeserializeArray(
          MNPlayerListProviderUnityClass.CallStatic<string>("getPlayerList"),
          typeof(MNUserInfo));

        MNUserInfo[] resultArray = new MNUserInfo[deserializedArray.Count];

        for (int index = 0;index < deserializedArray.Count;index++) {
          resultArray[index] = (MNUserInfo)(deserializedArray[index]);
        }

        return resultArray;
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

      MNTools.DLog("MNPlayerListProvider:RegisterEventHandler");

      if (Application.platform == RuntimePlatform.Android) {
        eventHandlerRegistered = MNPlayerListProviderUnityClass.CallStatic<bool>("registerEventHandler");
      }
      else {
        throw new MNNotOnDeviceExcepton();
      }
    }

    [MethodImpl(MethodImplOptions.Synchronized)]
    private void UnregisterEventHandler() {
      if ((PlayerJoinStorage != null) || (PlayerLeftStorage != null)) {
        return;
      }

      MNTools.DLog("MNPlayerListProvider:UnregisterEventHandler");

      if (!eventHandlerRegistered) {
        MNTools.DLog("MNPlayerListProvider:EventHandler not registered");
        return;
      }

      if (Application.platform == RuntimePlatform.Android) {
        eventHandlerRegistered = !MNPlayerListProviderUnityClass.CallStatic<bool>("unregisterEventHandler");
      }
      else {
        throw new MNNotOnDeviceExcepton();
      }
    }

    #endif

    #region MNPlayerListProviderEventHandler Messages
    private void MNUM_onPlayerJoin(string messageParams) {
      MNTools.DLog("MNPlayerListProvider:MNUM_onPlayerJoin: messageParams=<" + messageParams + ">");

      if (PlayerJoinStorage != null) {
        List<object> paramsArray = (List<object>)MNUnityCommunicator.Serializer.Deserialize(messageParams,typeof(List<object>));

        MNTools.DLogList("MNUM_onPlayerJoin params",paramsArray,MNTools.DEBUG_LEVEL_DETAILED);

        PlayerJoinStorage(MNSerializerMapper.MNUserInfoFromDictionary((IDictionary)paramsArray[0]));
      }
    }

    private void MNUM_onPlayerLeft(string messageParams) {
      MNTools.DLog("MNPlayerListProvider:MNUM_onPlayerLeft: messageParams=<" + messageParams + ">");

      if (PlayerLeftStorage != null) {
        List<object> paramsArray = (List<object>)MNUnityCommunicator.Serializer.Deserialize(messageParams,typeof(List<object>));

        MNTools.DLogList("MNUM_onPlayerLeft params",paramsArray,MNTools.DEBUG_LEVEL_DETAILED);

        PlayerLeftStorage(MNSerializerMapper.MNUserInfoFromDictionary((IDictionary)paramsArray[0]));
      }
    }

    #endregion MNPlayerListProviderEventHandler Messages

    #if UNITY_IPHONE

    /*
    // Not used in iOS
    [DllImport ("__Internal")]
    private static extern void _MNPlayerListProvider_Shutdown ();
    */

    [DllImport ("__Internal")]
    private static extern string _MNPlayerListProvider_GetPlayerList ();

    [DllImport ("__Internal")]
    private static extern bool _MNPlayerListProvider_RegisterEventHandler ();

    [DllImport ("__Internal")]
    private static extern bool _MNPlayerListProvider_UnregisterEventHandler ();

    #elif UNITY_ANDROID

    private static AndroidJavaClass mnPlayerListProviderUnityClass = null;

    private static AndroidJavaClass MNPlayerListProviderUnityClass
    {
      get {
        MNTools.DLog("AndroidJavaClass MNPlayerListProviderUnityClass");

        if (mnPlayerListProviderUnityClass == null) {
          mnPlayerListProviderUnityClass = new AndroidJavaClass("com.playphone.multinet.unity.MNPlayerListProviderUnity");
        }

        return mnPlayerListProviderUnityClass;
      }
    }

    #endif

    private MNPlayerListProvider()
    {
      MNTools.DLog("MNPlayerListProvider:MNPlayerListProvider()");
    }

    private bool eventHandlerRegistered = false;
  }
}
