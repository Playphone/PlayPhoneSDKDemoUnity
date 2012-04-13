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
  public class MNScoreProgressProvider : MonoBehaviour
  {
    public sealed class ScoreComparatorMoreIsBetter : ScoreComparator
    {
      internal override int getNativeCmparatorId() {
        return ScoreComparatorMoreIsBetterId;
      }
    }

    public sealed class ScoreComparatorLessIsBetter : ScoreComparator
    {
      internal override int getNativeCmparatorId() {
        return ScoreComparatorLessIsBetterId;
      }
    }

    public class ScoreItem
    {
      public MNUserInfo UserInfo {get;set;}
      public long Score {get;set;}
      public int Place {get;set;}

      public ScoreItem () {}
    }

    #region MNScoreProgressProviderEventHandler

    public delegate void ScoresUpdatedEventHandler(MNScoreProgressProvider.ScoreItem[] scoreBoard);
    public event ScoresUpdatedEventHandler ScoresUpdated
    {
      [MethodImpl(MethodImplOptions.Synchronized)]
      add
      {
        MNTools.DLog("ScoresUpdatedEventHandler.add");
        if (ScoresUpdatedStorage == null) {
          ScoresUpdatedStorage += value;

          RegisterEventHandler();
        }
        else {
          ScoresUpdatedStorage += value;
        }
      }

      [MethodImpl(MethodImplOptions.Synchronized)]
      remove
      {
        MNTools.DLog("ScoresUpdatedEventHandler.remove");
        ScoresUpdatedStorage -= value;

        if (ScoresUpdatedStorage == null) {
          UnregisterEventHandler();
        }
      }
    }
    private ScoresUpdatedEventHandler ScoresUpdatedStorage;

    #endregion MNScoreProgressProviderEventHandler

    #if UNITY_IPHONE

    public void SetRefreshIntervalAndUpdateDelay(int refreshInterval, int updateDelay) {
      MNTools.DLog("MNScoreProgressProvider:SetRefreshIntervalAndUpdateDelay");

      if (Application.platform == RuntimePlatform.IPhonePlayer) {
        _MNScoreProgressProvider_SetRefreshIntervalAndUpdateDelay(refreshInterval, updateDelay);
      }
      else {
        throw new MNNotOnDeviceExcepton();
      }
    }

    public void Start() {
      MNTools.DLog("MNScoreProgressProvider:Start");

      if (Application.platform == RuntimePlatform.IPhonePlayer) {
        _MNScoreProgressProvider_Start();
      }
      else {
        throw new MNNotOnDeviceExcepton();
      }
    }

    public void Stop() {
      MNTools.DLog("MNScoreProgressProvider:Stop");

      if (Application.platform == RuntimePlatform.IPhonePlayer) {
        _MNScoreProgressProvider_Stop();
      }
      else {
        throw new MNNotOnDeviceExcepton();
      }
    }

    public void SetScoreComparator(ScoreComparator comparator) {
      MNTools.DLog("MNScoreProgressProvider:SetScoreComparator");

      if (Application.platform == RuntimePlatform.IPhonePlayer) {
        _MNScoreProgressProvider_SetScoreComparator(comparator.getNativeCmparatorId());
      }
      else {
        throw new MNNotOnDeviceExcepton();
      }
    }

    public void PostScore(long score) {
      MNTools.DLog("MNScoreProgressProvider:PostScore");

      if (Application.platform == RuntimePlatform.IPhonePlayer) {
        _MNScoreProgressProvider_PostScore(score);
      }
      else {
        throw new MNNotOnDeviceExcepton();
      }
    }

    [MethodImpl(MethodImplOptions.Synchronized)]
    private void RegisterEventHandler() {
      MNTools.DLog("MNScoreProgressProvider:RegisterEventHandler");

      if (eventHandlerRegistered) {
        return;
      }

      if (Application.platform == RuntimePlatform.IPhonePlayer) {
        eventHandlerRegistered = _MNScoreProgressProvider_RegisterEventHandler();
      }
      else {
        throw new MNNotOnDeviceExcepton();
      }
    }

    [MethodImpl(MethodImplOptions.Synchronized)]
    private void UnregisterEventHandler() {
      MNTools.DLog("MNScoreProgressProvider:UnregisterEventHandler");

      if (!eventHandlerRegistered) {
        MNTools.DLog("MNScoreProgressProvider:EventHandler not registered");
        return;
      }

      if (Application.platform == RuntimePlatform.IPhonePlayer) {
        eventHandlerRegistered = !_MNScoreProgressProvider_UnregisterEventHandler();
      }
      else {
        throw new MNNotOnDeviceExcepton();
      }
    }

    #elif UNITY_ANDROID

    public void SetRefreshIntervalAndUpdateDelay(int refreshInterval, int updateDelay) {
      MNTools.DLog("MNScoreProgressProvider:SetRefreshIntervalAndUpdateDelay");

      if (Application.platform == RuntimePlatform.Android) {
        MNScoreProgressProviderUnityClass.CallStatic("setRefreshIntervalAndUpdateDelay",refreshInterval, updateDelay);
      }
      else {
        throw new MNNotOnDeviceExcepton();
      }
    }

    public void Start() {
      MNTools.DLog("MNScoreProgressProvider:Start");

      if (Application.platform == RuntimePlatform.Android) {
        MNScoreProgressProviderUnityClass.CallStatic("start");
      }
      else {
        throw new MNNotOnDeviceExcepton();
      }
    }

    public void Stop() {
      MNTools.DLog("MNScoreProgressProvider:Stop");

      if (Application.platform == RuntimePlatform.Android) {
        MNScoreProgressProviderUnityClass.CallStatic("stop");
      }
      else {
        throw new MNNotOnDeviceExcepton();
      }
    }

    public void SetScoreComparator(ScoreComparator comparator) {
      MNTools.DLog("MNScoreProgressProvider:SetScoreComparator");

      if (Application.platform == RuntimePlatform.Android) {
        MNScoreProgressProviderUnityClass.CallStatic("setScoreComparator",comparator.getNativeCmparatorId());
      }
      else {
        throw new MNNotOnDeviceExcepton();
      }
    }


    public void PostScore(long score) {
      MNTools.DLog("MNScoreProgressProvider:PostScore");

      if (Application.platform == RuntimePlatform.Android) {
        MNScoreProgressProviderUnityClass.CallStatic("postScore",score);
      }
      else {
        throw new MNNotOnDeviceExcepton();
      }
    }

    [MethodImpl(MethodImplOptions.Synchronized)]
    private void RegisterEventHandler() {
      MNTools.DLog("MNScoreProgressProvider:RegisterEventHandler");

      if (eventHandlerRegistered) {
        return;
      }

      if (Application.platform == RuntimePlatform.Android) {
        eventHandlerRegistered = MNScoreProgressProviderUnityClass.CallStatic<bool>("registerEventHandler");
      }
      else {
        throw new MNNotOnDeviceExcepton();
      }
    }

    [MethodImpl(MethodImplOptions.Synchronized)]
    private void UnregisterEventHandler() {
      MNTools.DLog("MNScoreProgressProvider:UnregisterEventHandler");

      if (!eventHandlerRegistered) {
        MNTools.DLog("MNScoreProgressProvider:EventHandler not registered");
        return;
      }

      if (Application.platform == RuntimePlatform.Android) {
        eventHandlerRegistered = !MNScoreProgressProviderUnityClass.CallStatic<bool>("unregisterEventHandler");
      }
      else {
        throw new MNNotOnDeviceExcepton();
      }
    }

    #endif

    #region MNScoreProgressProviderEventHandler Messages
    private void MNUM_onScoresUpdated(string messageParams) {
      MNTools.DLog("MNScoreProgressProvider:MNUM_onScoresUpdated: messageParams=<" + messageParams + ">");

      if (ScoresUpdatedStorage != null) {
        List<object> paramsArray = (List<object>)MNUnityCommunicator.Serializer.Deserialize(messageParams,typeof(List<object>));

        MNTools.DetailedLogList("MNUM_onScoresUpdated params",paramsArray);

        List<object> param1List = MNSerializerMapper.ListOfObjectsFromListOfDictionaries((List<object>)paramsArray[0],typeof(MNScoreProgressProvider.ScoreItem));
        MNScoreProgressProvider.ScoreItem[] param1Array = new MNScoreProgressProvider.ScoreItem[param1List.Count];

        for (int index = 0;index < param1List.Count;index++) {
          param1Array[index] = (MNScoreProgressProvider.ScoreItem)param1List[index];
        }

        ScoresUpdatedStorage(param1Array);
      }
    }

    #endregion MNScoreProgressProviderEventHandler Messages

    #if UNITY_IPHONE

    [DllImport ("__Internal")]
    private static extern void _MNScoreProgressProvider_SetRefreshIntervalAndUpdateDelay (int refreshInterval, int updateDelay);

    [DllImport ("__Internal")]
    private static extern void _MNScoreProgressProvider_Start ();

    [DllImport ("__Internal")]
    private static extern void _MNScoreProgressProvider_Stop ();

    [DllImport ("__Internal")]
    private static extern void _MNScoreProgressProvider_SetScoreComparator (int nativeComparatorId);

    [DllImport ("__Internal")]
    private static extern void _MNScoreProgressProvider_PostScore (long score);

    [DllImport ("__Internal")]
    private static extern bool _MNScoreProgressProvider_RegisterEventHandler ();

    [DllImport ("__Internal")]
    private static extern bool _MNScoreProgressProvider_UnregisterEventHandler ();

    #elif UNITY_ANDROID

    private static AndroidJavaClass mnScoreProgressProviderUnityClass = null;

    private static AndroidJavaClass MNScoreProgressProviderUnityClass
    {
      get {
        MNTools.DLog("AndroidJavaClass MNScoreProgressProviderUnityClass");

        if (mnScoreProgressProviderUnityClass == null) {
          mnScoreProgressProviderUnityClass = new AndroidJavaClass("com.playphone.multinet.unity.MNScoreProgressProviderUnity");
        }

        if (mnScoreProgressProviderUnityClass == null) {
          MNTools.DLog("MNScoreProgressProviderUnityClass return == null");
        }
        else {
          MNTools.DLog("MNScoreProgressProviderUnityClass return != null");
        }

        return mnScoreProgressProviderUnityClass;
      }
    }

    #endif

    private MNScoreProgressProvider()
    {
      MNTools.DLog("MNScoreProgressProvider:MNScoreProgressProvider()");
    }

    private bool eventHandlerRegistered = false;

    protected const int ScoreComparatorMoreIsBetterId = 0;
    protected const int ScoreComparatorLessIsBetterId = 1;

    public abstract class ScoreComparator
    {
      internal abstract int getNativeCmparatorId();
    }
  }
}
