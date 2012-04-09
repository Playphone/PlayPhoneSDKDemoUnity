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
  public class MNDirectUIHelper : MonoBehaviour
  {
    public const int DASHBOARD_STYLE_FULLSCREEN = 1;
    public const int DASHBOARD_STYLE_POPUP = 2;

    #region MNDirectUIHelperEventHandler

    public delegate void OnShowDashboardEventHandler();
    public static event OnShowDashboardEventHandler OnShowDashboard
    {
      [MethodImpl(MethodImplOptions.Synchronized)]
      add
      {
        MNTools.DLog("OnShowDashboardEventHandler.add");
        if (OnShowDashboardStorage == null) {
          OnShowDashboardStorage += value;

          RegisterEventHandler();
        }
        else {
          OnShowDashboardStorage += value;
        }
      }

      [MethodImpl(MethodImplOptions.Synchronized)]
      remove
      {
        MNTools.DLog("OnShowDashboardEventHandler.remove");
        OnShowDashboardStorage -= value;

        if (OnShowDashboardStorage == null) {
          UnregisterEventHandler();
        }
      }
    }
    private static OnShowDashboardEventHandler OnShowDashboardStorage;

    public delegate void OnHideDashboardEventHandler();
    public static event OnHideDashboardEventHandler OnHideDashboard
    {
      [MethodImpl(MethodImplOptions.Synchronized)]
      add
      {
        MNTools.DLog("OnHideDashboardEventHandler.add");
        if (OnHideDashboardStorage == null) {
          OnHideDashboardStorage += value;

          RegisterEventHandler();
        }
        else {
          OnHideDashboardStorage += value;
        }
      }

      [MethodImpl(MethodImplOptions.Synchronized)]
      remove
      {
        MNTools.DLog("OnHideDashboardEventHandler.remove");
        OnHideDashboardStorage -= value;

        if (OnHideDashboardStorage == null) {
          UnregisterEventHandler();
        }
      }
    }
    private static OnHideDashboardEventHandler OnHideDashboardStorage;

    #endregion MNDirectUIHelperEventHandler

    #if UNITY_IPHONE

    public static void SetDashboardStyle(int newStyle) {
      MNTools.DLog("MNDirectUIHelper:SetDashboardStyle");

      if (Application.platform == RuntimePlatform.IPhonePlayer) {
        _MNDirectUIHelper_SetDashboardStyle(newStyle);
      }
      else {
        throw new MNNotOnDeviceExcepton();
      }
    }

    public static int GetDashboardStyle() {
      MNTools.DLog("MNDirectUIHelper:GetDashboardStyle");

      if (Application.platform == RuntimePlatform.IPhonePlayer) {
        return _MNDirectUIHelper_GetDashboardStyle();
      }
      else {
        throw new MNNotOnDeviceExcepton();
      }
    }

    public static void ShowDashboard() {
      MNTools.DLog("MNDirectUIHelper:ShowDashboard");

      if (Application.platform == RuntimePlatform.IPhonePlayer) {
        _MNDirectUIHelper_ShowDashboard();
      }
      else {
        throw new MNNotOnDeviceExcepton();
      }
    }

    public static void HideDashboard() {
      MNTools.DLog("MNDirectUIHelper:HideDashboard");

      if (Application.platform == RuntimePlatform.IPhonePlayer) {
        _MNDirectUIHelper_HideDashboard();
      }
      else {
        throw new MNNotOnDeviceExcepton();
      }
    }

    public static bool IsDashboardHidden() {
      MNTools.DLog("MNDirectUIHelper:IsDashboardHidden");

      if (Application.platform == RuntimePlatform.IPhonePlayer) {
        return _MNDirectUIHelper_IsDashboardHidden();
      }
      else {
        throw new MNNotOnDeviceExcepton();
      }
    }

    public static bool IsDashboardVisible() {
      MNTools.DLog("MNDirectUIHelper:IsDashboardVisible");

      if (Application.platform == RuntimePlatform.IPhonePlayer) {
        return _MNDirectUIHelper_IsDashboardVisible();
      }
      else {
        throw new MNNotOnDeviceExcepton();
      }
    }

    public static void BindHostActivity() {
      //Empty Method for Android compatibility
    }

    public static void UnbindHostActivity() {
      //Empty Method for Android compatibility
    }

    [MethodImpl(MethodImplOptions.Synchronized)]
    private static void RegisterEventHandler() {
      MNTools.DLog("MNDirectUIHelper:RegisterEventHandler");

      if (eventHandlerRegistered) {
        MNTools.DLog("MNDirectUIHelper:EventHandler already registered");
        return;
      }

      if (Application.platform == RuntimePlatform.IPhonePlayer) {
        eventHandlerRegistered = _MNDirectUIHelper_RegisterEventHandler();
      }
      else {
        throw new MNNotOnDeviceExcepton();
      }
    }

    [MethodImpl(MethodImplOptions.Synchronized)]
    private static void UnregisterEventHandler() {
      MNTools.DLog("MNDirectUIHelper:UnregisterEventHandler");

      if (!eventHandlerRegistered) {
        MNTools.DLog("MNDirectUIHelper:EventHandler not registered");
        return;
      }

      if (Application.platform == RuntimePlatform.IPhonePlayer) {
        eventHandlerRegistered = !_MNDirectUIHelper_UnregisterEventHandler();
      }
      else {
        throw new MNNotOnDeviceExcepton();
      }
    }

    #elif UNITY_ANDROID

    public static void SetDashboardStyle(int newStyle) {
      MNTools.DLog("MNDirectUIHelper:SetDashboardStyle");

      if (Application.platform == RuntimePlatform.Android) {
        MNDirectUIHelperUnityClass.CallStatic("setDashboardStyle",newStyle);
      }
      else {
        throw new MNNotOnDeviceExcepton();
      }
    }

    public static int GetDashboardStyle() {
      MNTools.DLog("MNDirectUIHelper:GetDashboardStyle");

      if (Application.platform == RuntimePlatform.Android) {
        return MNDirectUIHelperUnityClass.CallStatic<int>("getDashboardStyle");
      }
      else {
        throw new MNNotOnDeviceExcepton();
      }
    }

    public static void ShowDashboard() {
      MNTools.DLog("MNDirectUIHelper:ShowDashboard");

      if (Application.platform == RuntimePlatform.Android) {
        MNDirectUIHelperUnityClass.CallStatic("showDashboard");
      }
      else {
        throw new MNNotOnDeviceExcepton();
      }
    }

    public static void HideDashboard() {
      MNTools.DLog("MNDirectUIHelper:HideDashboard");

      if (Application.platform == RuntimePlatform.Android) {
        MNDirectUIHelperUnityClass.CallStatic("hideDashboard");
      }
      else {
        throw new MNNotOnDeviceExcepton();
      }
    }

    public static bool IsDashboardHidden() {
      MNTools.DLog("MNDirectUIHelper:IsDashboardHidden");

      if (Application.platform == RuntimePlatform.Android) {
        return MNDirectUIHelperUnityClass.CallStatic<bool>("isDashboardHidden");
      }
      else {
        throw new MNNotOnDeviceExcepton();
      }
    }

    public static bool IsDashboardVisible() {
      MNTools.DLog("MNDirectUIHelper:IsDashboardVisible");

      if (Application.platform == RuntimePlatform.Android) {
        return MNDirectUIHelperUnityClass.CallStatic<bool>("isDashboardVisible");
      }
      else {
        throw new MNNotOnDeviceExcepton();
      }
    }

    public static void BindHostActivity() {
      MNTools.DLog("MNDirectUIHelper:BindHostActivity");

      if (Application.platform == RuntimePlatform.Android) {
        MNDirectUIHelperUnityClass.CallStatic("setHostActivity",true);
      }
      else {
        throw new MNNotOnDeviceExcepton();
      }
    }

    public static void UnbindHostActivity() {
      MNTools.DLog("MNDirectUIHelper:UnbindHostActivity");

      if (Application.platform == RuntimePlatform.Android) {
        MNDirectUIHelperUnityClass.CallStatic("setHostActivity",false);
      }
      else {
        throw new MNNotOnDeviceExcepton();
      }
    }

    [MethodImpl(MethodImplOptions.Synchronized)]
    private static void RegisterEventHandler() {
      MNTools.DLog("MNDirectUIHelper:RegisterEventHandler");

      if (eventHandlerRegistered) {
        MNTools.DLog("MNDirectUIHelper:EventHandler already registered");
        return;
      }

      if (Application.platform == RuntimePlatform.Android) {
        eventHandlerRegistered = MNDirectUIHelperUnityClass.CallStatic<bool>("registerEventHandler");
      }
      else {
        throw new MNNotOnDeviceExcepton();
      }
    }

    [MethodImpl(MethodImplOptions.Synchronized)]
    private static void UnregisterEventHandler() {
      MNTools.DLog("MNDirectUIHelper:UnregisterEventHandler");

      if (!eventHandlerRegistered) {
        MNTools.DLog("MNDirectUIHelper:EventHandler not registered");
        return;
      }

      if (Application.platform == RuntimePlatform.Android) {
        eventHandlerRegistered = !MNDirectUIHelperUnityClass.CallStatic<bool>("unregisterEventHandler");
      }
      else {
        throw new MNNotOnDeviceExcepton();
      }
    }

    #endif

    #region MNDirectUIHelperEventHandler Messages
    private void MNUM_onShowDashboard(string messageParams) {
      MNTools.DLog("MNDirectUIHelper:MNUM_onShowDashboard: messageParams=<" + messageParams + ">");

      if (OnShowDashboardStorage != null) {
        OnShowDashboardStorage();
      }
    }

    private void MNUM_onHideDashboard(string messageParams) {
      MNTools.DLog("MNDirectUIHelper:MNUM_onHideDashboard: messageParams=<" + messageParams + ">");

      if (OnHideDashboardStorage != null) {
        OnHideDashboardStorage();
      }
    }

    #endregion MNDirectUIHelperEventHandler Messages

    #if UNITY_IPHONE

    [DllImport ("__Internal")]
    private static extern void _MNDirectUIHelper_SetDashboardStyle (int newStyle);

    [DllImport ("__Internal")]
    private static extern int _MNDirectUIHelper_GetDashboardStyle ();

    [DllImport ("__Internal")]
    private static extern void _MNDirectUIHelper_ShowDashboard ();

    [DllImport ("__Internal")]
    private static extern void _MNDirectUIHelper_HideDashboard ();

    [DllImport ("__Internal")]
    private static extern bool _MNDirectUIHelper_IsDashboardHidden ();

    [DllImport ("__Internal")]
    private static extern bool _MNDirectUIHelper_IsDashboardVisible ();


    [DllImport ("__Internal")]
    private static extern bool _MNDirectUIHelper_RegisterEventHandler ();

    [DllImport ("__Internal")]
    private static extern bool _MNDirectUIHelper_UnregisterEventHandler ();

    #elif UNITY_ANDROID

    private static AndroidJavaClass mnDirectUIHelperUnityClass = null;

    private static AndroidJavaClass MNDirectUIHelperUnityClass
    {
      get {
        MNTools.DLog("AndroidJavaClass MNDirectUIHelperUnityClass");

        if (mnDirectUIHelperUnityClass == null) {
          mnDirectUIHelperUnityClass = new AndroidJavaClass("com.playphone.multinet.unity.MNDirectUIHelperUnity");
        }

        return mnDirectUIHelperUnityClass;
      }
    }

    #endif

    private MNDirectUIHelper()
    {
      MNTools.DLog("MNDirectUIHelper:MNDirectUIHelper()");
    }

    private static bool eventHandlerRegistered = false;
  }
}
