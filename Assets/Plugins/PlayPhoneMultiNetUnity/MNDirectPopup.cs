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
  public class MNDirectPopup : MonoBehaviour
  {
    #if UNITY_IPHONE
    public const int MNDIRECTPOPUP_WELCOME       = 1;
    public const int MNDIRECTPOPUP_ACHIEVEMENTS  = 2;
    public const int MNDIRECTPOPUP_NEW_HI_SCORES = 4;
    public const int MNDIRECTPOPUP_WEB_EVENT     = 65536;
    public const int MNDIRECTPOPUP_ALL           = 65543;
    #elif UNITY_ANDROID
    public const int MNDIRECTPOPUP_WELCOME       = 1;
    public const int MNDIRECTPOPUP_ACHIEVEMENTS  = 2;
    public const int MNDIRECTPOPUP_NEW_HI_SCORES = 4;
    public const int MNDIRECTPOPUP_WEB_EVENT     = 8;
    public const int MNDIRECTPOPUP_ALL           = 15;
    #else
    public const int MNDIRECTPOPUP_WELCOME       = 1;
    public const int MNDIRECTPOPUP_ACHIEVEMENTS  = 2;
    public const int MNDIRECTPOPUP_NEW_HI_SCORES = 4;
    public const int MNDIRECTPOPUP_WEB_EVENT     = 65536;
    public const int MNDIRECTPOPUP_ALL           = 65543;
    #endif

    #if UNITY_IPHONE

    public static void Init(int actionsBitMask) {
      MNTools.DLog("MNDirectPopup:Init");

      if (Application.platform == RuntimePlatform.IPhonePlayer) {
        _MNDirectPopup_Init(actionsBitMask);
      }
      else {
      }
    }

    public static bool IsActive() {
      MNTools.DLog("MNDirectPopup:IsActive");

      if (Application.platform == RuntimePlatform.IPhonePlayer) {
        return _MNDirectPopup_IsActive();
      }
      else {
        return default(bool);
      }
    }

    public static void SetActive(bool activeFlag) {
      MNTools.DLog("MNDirectPopup:SetActive");

      if (Application.platform == RuntimePlatform.IPhonePlayer) {
        _MNDirectPopup_SetActive(activeFlag);
      }
      else {
      }
    }

    #elif UNITY_ANDROID

    public static void Init(int actionsBitMask) {
      MNTools.DLog("MNDirectPopup:Init");

      if (Application.platform == RuntimePlatform.Android) {
        MNDirectPopupUnityClass.CallStatic("init",actionsBitMask);
      }
      else {
      }
    }

    public static bool IsActive() {
      MNTools.DLog("MNDirectPopup:IsActive");

      if (Application.platform == RuntimePlatform.Android) {
        return MNDirectPopupUnityClass.CallStatic<bool>("isActive");
      }
      else {
        return default(bool);
      }
    }

    public static void SetActive(bool activeFlag) {
      MNTools.DLog("MNDirectPopup:SetActive");

      if (Application.platform == RuntimePlatform.Android) {
        MNDirectPopupUnityClass.CallStatic("setActive",activeFlag);
      }
      else {
      }
    }

    #else
    // Empty implementation for unsupported platforms (such as Unity Editor)
    // Method's arguments are ignored.
    // Non-void methods return default values. If return value is an array empty array is returned.

    public static void Init(int actionsBitMask) {
    }

    public static bool IsActive() {
      return default(bool);
    }

    public static void SetActive(bool activeFlag) {
    }

    #endif

    #if UNITY_IPHONE

    [DllImport ("__Internal")]
    private static extern void _MNDirectPopup_Init (int actionsBitMask);

    [DllImport ("__Internal")]
    private static extern bool _MNDirectPopup_IsActive ();

    [DllImport ("__Internal")]
    private static extern void _MNDirectPopup_SetActive (bool activeFlag);

    #elif UNITY_ANDROID

    private static AndroidJavaClass mnDirectPopupUnityClass = null;

    private static AndroidJavaClass MNDirectPopupUnityClass
    {
      get {
        MNTools.DLog("AndroidJavaClass MNDirectPopupUnityClass");

        if (mnDirectPopupUnityClass == null) {
          mnDirectPopupUnityClass = new AndroidJavaClass("com.playphone.multinet.unity.MNDirectPopupUnity");
        }

        return mnDirectPopupUnityClass;
      }
    }

    #endif

    private MNDirectPopup()
    {
      MNTools.DLog("MNDirectPopup:MNDirectPopup()");
    }
  }
}
