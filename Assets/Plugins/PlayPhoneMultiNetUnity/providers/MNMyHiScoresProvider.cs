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
  public class MNMyHiScoresProvider : MonoBehaviour
  {
    public const int MN_HS_PERIOD_MASK_ALLTIME = 0x0001;
    public const int MN_HS_PERIOD_MASK_WEEK = 0x0002;
    public const int MN_HS_PERIOD_MASK_MONTH = 0x0004;

    #region MNMyHiScoresProviderEventHandler

    public delegate void NewHiScoreEventHandler(long newScore, int gameSetId, int periodMask);
    public event NewHiScoreEventHandler NewHiScore
    {
      [MethodImpl(MethodImplOptions.Synchronized)]
      add
      {
        MNTools.DLog("NewHiScoreEventHandler.add");
        if (NewHiScoreStorage == null) {
          NewHiScoreStorage += value;

          RegisterEventHandler();
        }
        else {
          NewHiScoreStorage += value;
        }
      }

      [MethodImpl(MethodImplOptions.Synchronized)]
      remove
      {
        MNTools.DLog("NewHiScoreEventHandler.remove");
        NewHiScoreStorage -= value;

        if (NewHiScoreStorage == null) {
          UnregisterEventHandler();
        }
      }
    }
    private NewHiScoreEventHandler NewHiScoreStorage;

    #endregion MNMyHiScoresProviderEventHandler

    #if UNITY_IPHONE

    public void Shutdown() {
      MNTools.DLog("MNMyHiScoresProvider:Shutdown");

      if (Application.platform == RuntimePlatform.IPhonePlayer) {
        // Not used in iOS

      }
      else {
      }
    }

    public long? GetMyHiScore(int gameSetId) {
      MNTools.DLog("MNMyHiScoresProvider:GetMyHiScore");

      if (Application.platform == RuntimePlatform.IPhonePlayer) {
        return (long?)MNUnityCommunicator.Serializer.Deserialize(_MNMyHiScoresProvider_GetMyHiScore(gameSetId),typeof(long?));
      }
      else {
        return default(long?);
      }
    }

    //return IDictionary with key type = int, value type = long
    //public IDictionary<int,long> GetMyHiScores() {
    public IDictionary GetMyHiScores() {
      MNTools.DLog("MNMyHiScoresProvider:GetMyHiScores");

      if (Application.platform == RuntimePlatform.IPhonePlayer) {
        return MNUnityCommunicator.Serializer.DeserializeDictionary(_MNMyHiScoresProvider_GetMyHiScores());
      }
      else {
        return new Hashtable();
      }
    }

    [MethodImpl(MethodImplOptions.Synchronized)]
    private void RegisterEventHandler() {
      if (eventHandlerRegistered) {
        return;
      }

      MNTools.DLog("MNMyHiScoresProvider:RegisterEventHandler");

      if (Application.platform == RuntimePlatform.IPhonePlayer) {
        eventHandlerRegistered = _MNMyHiScoresProvider_RegisterEventHandler();
      }
      else {
      }
    }

    [MethodImpl(MethodImplOptions.Synchronized)]
    private void UnregisterEventHandler() {
      if ((NewHiScoreStorage != null)) {
        return;
      }

      MNTools.DLog("MNMyHiScoresProvider:UnregisterEventHandler");

      if (!eventHandlerRegistered) {
        MNTools.DLog("MNMyHiScoresProvider:EventHandler not registered");
        return;
      }

      if (Application.platform == RuntimePlatform.IPhonePlayer) {
        eventHandlerRegistered = !_MNMyHiScoresProvider_UnregisterEventHandler();
      }
      else {
      }
    }

    #elif UNITY_ANDROID

    public void Shutdown() {
      MNTools.DLog("MNMyHiScoresProvider:Shutdown");

      if (Application.platform == RuntimePlatform.Android) {
        MNMyHiScoresProviderUnityClass.CallStatic("shutdown");
      }
      else {
      }
    }

    public long? GetMyHiScore(int gameSetId) {
      MNTools.DLog("MNMyHiScoresProvider:GetMyHiScore");

      if (Application.platform == RuntimePlatform.Android) {
        return (long?)MNUnityCommunicator.Serializer.Deserialize(MNMyHiScoresProviderUnityClass.CallStatic<string>("getMyHiScore",gameSetId),typeof(long?));
      }
      else {
        return default(long?);
      }
    }

    //return IDictionary with key type = int, value type = long
    //public IDictionary<int,long> GetMyHiScores() {
    public IDictionary GetMyHiScores() {
      MNTools.DLog("MNMyHiScoresProvider:GetMyHiScores");

      if (Application.platform == RuntimePlatform.Android) {
        return MNUnityCommunicator.Serializer.DeserializeDictionary(MNMyHiScoresProviderUnityClass.CallStatic<string>("getMyHiScores"));
      }
      else {
        return new Hashtable();
      }
    }

    [MethodImpl(MethodImplOptions.Synchronized)]
    private void RegisterEventHandler() {
      if (eventHandlerRegistered) {
        return;
      }

      MNTools.DLog("MNMyHiScoresProvider:RegisterEventHandler");

      if (Application.platform == RuntimePlatform.Android) {
        eventHandlerRegistered = MNMyHiScoresProviderUnityClass.CallStatic<bool>("registerEventHandler");
      }
      else {
      }
    }

    [MethodImpl(MethodImplOptions.Synchronized)]
    private void UnregisterEventHandler() {
      if ((NewHiScoreStorage != null)) {
        return;
      }

      MNTools.DLog("MNMyHiScoresProvider:UnregisterEventHandler");

      if (!eventHandlerRegistered) {
        MNTools.DLog("MNMyHiScoresProvider:EventHandler not registered");
        return;
      }

      if (Application.platform == RuntimePlatform.Android) {
        eventHandlerRegistered = !MNMyHiScoresProviderUnityClass.CallStatic<bool>("unregisterEventHandler");
      }
      else {
      }
    }

    #else
    // Empty implementation for unsupported platforms (such as Unity Editor)
    // Method's arguments are ignored.
    // Non-void methods return default values. If return value is an array empty array is returned.

    public void Shutdown() {
    }

    public long? GetMyHiScore(int gameSetId) {
      return default(long?);
    }

    public IDictionary GetMyHiScores() {
      return new Hashtable();
    }

    private void RegisterEventHandler() {
    }

    private void UnregisterEventHandler() {
    }

    #endif

    #region MNMyHiScoresProviderEventHandler Messages
    private void MNUM_onNewHiScore(string messageParams) {
      MNTools.DLog("MNMyHiScoresProvider:MNUM_onNewHiScore: messageParams=<" + messageParams + ">");

      if (NewHiScoreStorage != null) {
        List<object> paramsArray = (List<object>)MNUnityCommunicator.Serializer.Deserialize(messageParams,typeof(List<object>));

        MNTools.DLogList("MNUM_onNewHiScore params",paramsArray,MNTools.DEBUG_LEVEL_DETAILED);

        NewHiScoreStorage(Convert.ToInt64(paramsArray[0]), Convert.ToInt32(paramsArray[1]), Convert.ToInt32(paramsArray[2]));
      }
    }

    #endregion MNMyHiScoresProviderEventHandler Messages

    #if UNITY_IPHONE

    /*
    // Not used in iOS
    [DllImport ("__Internal")]
    private static extern void _MNMyHiScoresProvider_Shutdown ();
    */

    [DllImport ("__Internal")]
    private static extern string _MNMyHiScoresProvider_GetMyHiScore (int gameSetId);

    [DllImport ("__Internal")]
    private static extern string _MNMyHiScoresProvider_GetMyHiScores ();

    [DllImport ("__Internal")]
    private static extern bool _MNMyHiScoresProvider_RegisterEventHandler ();

    [DllImport ("__Internal")]
    private static extern bool _MNMyHiScoresProvider_UnregisterEventHandler ();

    #elif UNITY_ANDROID

    private static AndroidJavaClass mnMyHiScoresProviderUnityClass = null;

    private static AndroidJavaClass MNMyHiScoresProviderUnityClass
    {
      get {
        MNTools.DLog("AndroidJavaClass MNMyHiScoresProviderUnityClass");

        if (mnMyHiScoresProviderUnityClass == null) {
          mnMyHiScoresProviderUnityClass = new AndroidJavaClass("com.playphone.multinet.unity.MNMyHiScoresProviderUnity");
        }

        return mnMyHiScoresProviderUnityClass;
      }
    }

    #endif

    private MNMyHiScoresProvider()
    {
      MNTools.DLog("MNMyHiScoresProvider:MNMyHiScoresProvider()");
    }

    #if UNITY_IPHONE || UNITY_ANDROID
    private bool eventHandlerRegistered = false;
    #endif
  }
}
