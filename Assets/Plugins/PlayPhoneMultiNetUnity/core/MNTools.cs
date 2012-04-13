//#define MN_DEBUG

#if MN_DEBUG
//#define MN_DETAILED_LOG
#endif

using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;

namespace PlayPhone.MultiNet.Core
{
  public class MNTools
  {
    private const bool MN_DEBUG = true;

    public const string NullString = "null";

    public static void DLog(string message)
    {
      #if MN_DEBUG
      Debug.Log("MNUW:C#:" + message);
      #endif
    }

    public static void DetailedLog(string message) {
      #if MN_DETAILED_LOG
      DLog(message);
      #endif
    }

    public static void DetailedLogDictionary(string name,IDictionary deserializedObject) {
      string logString = "DetailedLogDictionary. " + name + ":\n";

      foreach (object key in deserializedObject.Keys) {
        logString += key.ToString() + ":" + deserializedObject[key] + "\n";
      }

      MNTools.DetailedLog(logString);
    }

    public static void DetailedLogArrayList(string listName,ArrayList list) {
      string logString = "DetailedLogArrayList. " + listName + ":\n";

      for (int index = 0;index < list.Count;index++) {
        logString += string.Format("{0}. {1}\n",index,SafeToString(list[index]));
      }

      MNTools.DetailedLog(logString);
    }

    public static void DetailedLogList(string listName,List<object> list) {
      string logString = "DetailedLogList. " + listName + ":\n";

      for (int index = 0;index < list.Count;index++) {
        logString += string.Format("{0}. {1}\n",index,SafeToString(list[index]));
      }

      MNTools.DetailedLog(logString);
    }

    public static void ELog(string message)
    {
      Debug.Log("MNUW:Error:C#:" + message);
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
