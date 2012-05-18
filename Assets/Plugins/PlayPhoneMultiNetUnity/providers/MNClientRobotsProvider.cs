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
  public class MNClientRobotsProvider : MonoBehaviour
  {
    #if UNITY_IPHONE

    public void Shutdown() {
      MNTools.DLog("MNClientRobotsProvider:Shutdown");

      if (Application.platform == RuntimePlatform.IPhonePlayer) {
        // Not used in iOS

      }
      else {
      }
    }

    public bool IsRobot(MNUserInfo userInfo) {
      MNTools.DLog("MNClientRobotsProvider:IsRobot");

      if (Application.platform == RuntimePlatform.IPhonePlayer) {
        return _MNClientRobotsProvider_IsRobot(MNUnityCommunicator.Serializer.Serialize(userInfo));
      }
      else {
        return default(bool);
      }
    }

    public void PostRobotScore(MNUserInfo userInfo, long score) {
      MNTools.DLog("MNClientRobotsProvider:PostRobotScore");

      if (Application.platform == RuntimePlatform.IPhonePlayer) {
        _MNClientRobotsProvider_PostRobotScore(MNUnityCommunicator.Serializer.Serialize(userInfo), score);
      }
      else {
      }
    }

    public void SetRoomRobotLimit(int robotCount) {
      MNTools.DLog("MNClientRobotsProvider:SetRoomRobotLimit");

      if (Application.platform == RuntimePlatform.IPhonePlayer) {
        _MNClientRobotsProvider_SetRoomRobotLimit(robotCount);
      }
      else {
      }
    }

    public int GetRoomRobotLimit() {
      MNTools.DLog("MNClientRobotsProvider:GetRoomRobotLimit");

      if (Application.platform == RuntimePlatform.IPhonePlayer) {
        return _MNClientRobotsProvider_GetRoomRobotLimit();
      }
      else {
        return default(int);
      }
    }

    #elif UNITY_ANDROID

    public void Shutdown() {
      MNTools.DLog("MNClientRobotsProvider:Shutdown");

      if (Application.platform == RuntimePlatform.Android) {
        MNClientRobotsProviderUnityClass.CallStatic("shutdown");
      }
      else {
      }
    }

    public bool IsRobot(MNUserInfo userInfo) {
      MNTools.DLog("MNClientRobotsProvider:IsRobot");

      if (Application.platform == RuntimePlatform.Android) {
        return MNClientRobotsProviderUnityClass.CallStatic<bool>("isRobot",MNUnityCommunicator.Serializer.Serialize(userInfo));
      }
      else {
        return default(bool);
      }
    }

    public void PostRobotScore(MNUserInfo userInfo, long score) {
      MNTools.DLog("MNClientRobotsProvider:PostRobotScore");

      if (Application.platform == RuntimePlatform.Android) {
        MNClientRobotsProviderUnityClass.CallStatic("postRobotScore",MNUnityCommunicator.Serializer.Serialize(userInfo), score);
      }
      else {
      }
    }

    public void SetRoomRobotLimit(int robotCount) {
      MNTools.DLog("MNClientRobotsProvider:SetRoomRobotLimit");

      if (Application.platform == RuntimePlatform.Android) {
        MNClientRobotsProviderUnityClass.CallStatic("setRoomRobotLimit",robotCount);
      }
      else {
      }
    }

    public int GetRoomRobotLimit() {
      MNTools.DLog("MNClientRobotsProvider:GetRoomRobotLimit");

      if (Application.platform == RuntimePlatform.Android) {
        return MNClientRobotsProviderUnityClass.CallStatic<int>("getRoomRobotLimit");
      }
      else {
        return default(int);
      }
    }

    #else
    // Empty implementation for unsupported platforms (such as Unity Editor)
    // Method's arguments are ignored.
    // Non-void methods return default values. If return value is an array empty array is returned.

    public void Shutdown() {
    }

    public bool IsRobot(MNUserInfo userInfo) {
      return default(bool);
    }

    public void PostRobotScore(MNUserInfo userInfo, long score) {
    }

    public void SetRoomRobotLimit(int robotCount) {
    }

    public int GetRoomRobotLimit() {
      return default(int);
    }

    #endif

    #if UNITY_IPHONE

    /*
    // Not used in iOS
    [DllImport ("__Internal")]
    private static extern void _MNClientRobotsProvider_Shutdown ();
    */

    [DllImport ("__Internal")]
    private static extern bool _MNClientRobotsProvider_IsRobot (string userInfo);

    [DllImport ("__Internal")]
    private static extern void _MNClientRobotsProvider_PostRobotScore (string userInfo, long score);

    [DllImport ("__Internal")]
    private static extern void _MNClientRobotsProvider_SetRoomRobotLimit (int robotCount);

    [DllImport ("__Internal")]
    private static extern int _MNClientRobotsProvider_GetRoomRobotLimit ();

    #elif UNITY_ANDROID

    private static AndroidJavaClass mnClientRobotsProviderUnityClass = null;

    private static AndroidJavaClass MNClientRobotsProviderUnityClass
    {
      get {
        MNTools.DLog("AndroidJavaClass MNClientRobotsProviderUnityClass");

        if (mnClientRobotsProviderUnityClass == null) {
          mnClientRobotsProviderUnityClass = new AndroidJavaClass("com.playphone.multinet.unity.MNClientRobotsProviderUnity");
        }

        return mnClientRobotsProviderUnityClass;
      }
    }

    #endif

    private MNClientRobotsProvider()
    {
      MNTools.DLog("MNClientRobotsProvider:MNClientRobotsProvider()");
    }
  }
}
