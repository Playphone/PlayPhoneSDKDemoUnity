using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;

namespace PlayPhone.MultiNet.Core
{
  public class MNTools
  {
    public const int DEBUG_LEVEL_OFF = 0;
    public const int DEBUG_LEVEL_NORMAL = 1;
    public const int DEBUG_LEVEL_DETAILED = 2;

    public const string NullString = "null";

    private static int CurrentDebugLevel = DEBUG_LEVEL_OFF;

    public static void SetDebugLevel(int debugLevel) {
      CurrentDebugLevel = debugLevel;

      #if UNITY_ANDROID
      AndroidJavaClass mnUnityClass = new AndroidJavaClass("com.playphone.multinet.unity.MNUnity");

      if (mnUnityClass != null) {
        mnUnityClass.CallStatic("setDebugLevel",debugLevel);
      }
      #endif
    }

    public static void DLog(string message) {
      DLog(message,DEBUG_LEVEL_NORMAL);
    }

    public static void DLog(string message,int debugLevel) {
      if (CurrentDebugLevel >= debugLevel) {
        Debug.Log("MNUW:C#:" + message);
      }
    }

    public static void DLogDictionary(string name,IDictionary deserializedObject,int debugLevel) {
      string logString = "DLogDictionary. " + name + ":\n";

      foreach (object key in deserializedObject.Keys) {
        logString += key.ToString() + ":" + deserializedObject[key] + "\n";
      }

      MNTools.DLog(logString,debugLevel);
    }

    public static void DLogArrayList(string listName,ArrayList list,int debugLevel) {
      string logString = "DLogArrayList. " + listName + ":\n";

      for (int index = 0;index < list.Count;index++) {
        logString += string.Format("{0}. {1}\n",index,SafeToString(list[index]));
      }

      MNTools.DLog(logString,debugLevel);
    }

    public static void DLogList(string listName,List<object> list,int debugLevel) {
      string logString = "DLogList. " + listName + ":\n";

      for (int index = 0;index < list.Count;index++) {
        logString += string.Format("{0}. {1}\n",index,SafeToString(list[index]));
      }

      MNTools.DLog(logString,debugLevel);
    }

    public static void ELog(string message)
    {
      Debug.LogError("MNUW:Error:C#:" + message);
    }

    public static bool IsNullableType(Type type) {
      return (type.IsGenericType &&
              type.GetGenericTypeDefinition().Equals(typeof(Nullable<>)));
    }

    public static bool IsSimpleType(Type type) {
      if ((type == typeof(int))     ||
        (type == typeof(long))    ||
        (type == typeof(double))  ||
        (type == typeof(bool))    ||
        (type == typeof(string))) {

        return true;
      }

      return false;
    }

    public static string SafeToString(object obj) {
      string result = "";

      if (obj == null) {
        result = "<" + NullString + ">";
      }
      else {
        result = obj.ToString();

        if (obj is string) {
          result = string.Format("<{0}>",result);
        }
      }
      
      return result;
    }

    private MNTools ()
    {
    }
  }
}
