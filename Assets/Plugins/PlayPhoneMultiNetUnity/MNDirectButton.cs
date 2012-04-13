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
  public class MNDirectButton : MonoBehaviour
  {
    #if UNITY_IPHONE
    public const int MNDIRECTBUTTON_TOPLEFT     = 1;
    public const int MNDIRECTBUTTON_TOPRIGHT    = 2;
    public const int MNDIRECTBUTTON_BOTTOMRIGHT = 3;
    public const int MNDIRECTBUTTON_BOTTOMLEFT  = 4;
    public const int MNDIRECTBUTTON_LEFT        = 5;
    public const int MNDIRECTBUTTON_TOP         = 6;
    public const int MNDIRECTBUTTON_RIGHT       = 7;
    public const int MNDIRECTBUTTON_BOTTOM      = 8;
    #elif UNITY_ANDROID
    public const int MNDIRECTBUTTON_TOPLEFT     = 51;
    public const int MNDIRECTBUTTON_TOPRIGHT    = 53;
    public const int MNDIRECTBUTTON_BOTTOMRIGHT = 85;
    public const int MNDIRECTBUTTON_BOTTOMLEFT  = 83;
    public const int MNDIRECTBUTTON_LEFT        = 19;
    public const int MNDIRECTBUTTON_TOP         = 49;
    public const int MNDIRECTBUTTON_RIGHT       = 21;
    public const int MNDIRECTBUTTON_BOTTOM      = 81;
    #endif

    #if UNITY_IPHONE

    public static bool IsVisible() {
      MNTools.DLog("MNDirectButton:IsVisible");

      if (Application.platform == RuntimePlatform.IPhonePlayer) {
        return _MNDirectButton_IsVisible();
      }
      else {
        throw new MNNotOnDeviceExcepton();
      }
    }

    public static bool IsHidden() {
      MNTools.DLog("MNDirectButton:IsHidden");

      if (Application.platform == RuntimePlatform.IPhonePlayer) {
        return _MNDirectButton_IsHidden();
      }
      else {
        throw new MNNotOnDeviceExcepton();
      }
    }

    public static void InitWithLocation(int location) {
      MNTools.DLog("MNDirectButton:InitWithLocation");

      if (Application.platform == RuntimePlatform.IPhonePlayer) {
        _MNDirectButton_InitWithLocation(location);
      }
      else {
        throw new MNNotOnDeviceExcepton();
      }
    }

    public static void Show() {
      MNTools.DLog("MNDirectButton:Show");

      if (Application.platform == RuntimePlatform.IPhonePlayer) {
        _MNDirectButton_Show();
      }
      else {
        throw new MNNotOnDeviceExcepton();
      }
    }

    public static void Hide() {
      MNTools.DLog("MNDirectButton:Hide");

      if (Application.platform == RuntimePlatform.IPhonePlayer) {
        _MNDirectButton_Hide();
      }
      else {
        throw new MNNotOnDeviceExcepton();
      }
    }

    public static void SetVShopEventAutoHandleEnabled(bool isEnabled) {
      MNTools.DLog("MNDirectButton:SetVShopEventAutoHandleEnabled");

      if (Application.platform == RuntimePlatform.IPhonePlayer) {
        _MNDirectButton_SetVShopEventAutoHandleEnabled(isEnabled);
      }
      else {
        throw new MNNotOnDeviceExcepton();
      }
    }

    #elif UNITY_ANDROID

    public static bool IsVisible() {
      MNTools.DLog("MNDirectButton:IsVisible");

      if (Application.platform == RuntimePlatform.Android) {
        return MNDirectButtonUnityClass.CallStatic<bool>("isVisible");
      }
      else {
        throw new MNNotOnDeviceExcepton();
      }
    }

    public static bool IsHidden() {
      MNTools.DLog("MNDirectButton:IsHidden");

      if (Application.platform == RuntimePlatform.Android) {
        return MNDirectButtonUnityClass.CallStatic<bool>("isHidden");
      }
      else {
        throw new MNNotOnDeviceExcepton();
      }
    }

    public static void InitWithLocation(int location) {
      MNTools.DLog("MNDirectButton:InitWithLocation");

      if (Application.platform == RuntimePlatform.Android) {
        MNDirectButtonUnityClass.CallStatic("initWithLocation",location);
      }
      else {
        throw new MNNotOnDeviceExcepton();
      }
    }

    public static void Show() {
      MNTools.DLog("MNDirectButton:Show");

      if (Application.platform == RuntimePlatform.Android) {
        MNDirectButtonUnityClass.CallStatic("show");
      }
      else {
        throw new MNNotOnDeviceExcepton();
      }
    }

    public static void Hide() {
      MNTools.DLog("MNDirectButton:Hide");

      if (Application.platform == RuntimePlatform.Android) {
        MNDirectButtonUnityClass.CallStatic("hide");
      }
      else {
        throw new MNNotOnDeviceExcepton();
      }
    }

    public static void SetVShopEventAutoHandleEnabled(bool isEnabled) {
      MNTools.DLog("MNDirectButton:SetVShopEventAutoHandleEnabled");

      if (Application.platform == RuntimePlatform.Android) {
        MNDirectButtonUnityClass.CallStatic("setVShopEventAutoHandleEnabled",isEnabled);
      }
      else {
        throw new MNNotOnDeviceExcepton();
      }
    }

    #endif

    #if UNITY_IPHONE

    [DllImport ("__Internal")]
    private static extern bool _MNDirectButton_IsVisible ();

    [DllImport ("__Internal")]
    private static extern bool _MNDirectButton_IsHidden ();

    [DllImport ("__Internal")]
    private static extern void _MNDirectButton_InitWithLocation (int location);

    [DllImport ("__Internal")]
    private static extern void _MNDirectButton_Show ();

    [DllImport ("__Internal")]
    private static extern void _MNDirectButton_Hide ();

    [DllImport ("__Internal")]
    private static extern void _MNDirectButton_SetVShopEventAutoHandleEnabled (bool isEnabled);

    #elif UNITY_ANDROID

    private static AndroidJavaClass mnDirectButtonUnityClass = null;

    private static AndroidJavaClass MNDirectButtonUnityClass
    {
      get {
        MNTools.DLog("AndroidJavaClass MNDirectButtonUnityClass");

        if (mnDirectButtonUnityClass == null) {
          mnDirectButtonUnityClass = new AndroidJavaClass("com.playphone.multinet.unity.MNDirectButtonUnity");
        }

        return mnDirectButtonUnityClass;
      }
    }

    #endif

    private MNDirectButton()
    {
      MNTools.DLog("MNDirectButton:MNDirectButton()");
    }
  }
}
