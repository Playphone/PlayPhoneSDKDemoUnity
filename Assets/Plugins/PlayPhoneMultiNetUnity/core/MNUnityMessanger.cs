using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using PlayPhone.MultiNet.Providers;

namespace PlayPhone.MultiNet.Core
{
  internal sealed class MNUnityCommunicator : MonoBehaviour
  {
    private static volatile MNUnityCommunicator instance;
    private static object syncRoot = new object();
    private static Hashtable registeredComponents = new Hashtable();

    private static GameObject MNUnityCommunicatorGameObject = null;

    private MNUnityCommunicator() {}

    internal static void Init() {
      MNTools.DLog("MNUnityCommunicator Init");

      if (instance == null) {
        lock (syncRoot) {
          if (instance == null) {
            MNUnityCommunicatorGameObject = new GameObject("MNUnityCommunicator");

            instance = MNUnityCommunicatorGameObject.AddComponent<MNUnityCommunicator>();

            registeredComponents[typeof(MNDirect)] = MNUnityCommunicatorGameObject.AddComponent(typeof(MNDirect));
            registeredComponents[typeof(MNDirectButton)] = MNUnityCommunicatorGameObject.AddComponent(typeof(MNDirectButton));
            registeredComponents[typeof(MNDirectUIHelper)] = MNUnityCommunicatorGameObject.AddComponent(typeof(MNDirectUIHelper));
            registeredComponents[typeof(MNDirectPopup)] = MNUnityCommunicatorGameObject.AddComponent(typeof(MNDirectPopup));
            registeredComponents[typeof(MNWSProvider)] = MNUnityCommunicatorGameObject.AddComponent(typeof(MNWSProvider));

            Serializer = new MNJsonSerializer();
          }
        }
      }
    }

    internal static object registerComponent (Type compnentType) {
      object result = null;
      lock (syncRoot) {
        if (!registeredComponents.ContainsKey(compnentType)) {
          result = MNUnityCommunicatorGameObject.AddComponent(compnentType);
          registeredComponents[compnentType] = result;
          MNTools.DLog("registerComponent<" + MNTools.SafeToString(compnentType) + ">. ComponentsCount = " + registeredComponents.Count);
        }
        else {
          result = registeredComponents[compnentType];
        }
      }

      return result;
    }
    
    internal static bool removeComponent (Type compnentType) {
      lock (syncRoot) {
        if (registeredComponents.ContainsKey(compnentType)) {
          Destroy(MNUnityCommunicatorGameObject.GetComponent(compnentType));
          registeredComponents.Remove(compnentType);
          MNTools.DLog("removedComponent<" + MNTools.SafeToString(compnentType) + ">. ComponentsCount = " + registeredComponents.Count);
        }
        else {
          return false;
        }
      }
      return true;
    }
    
    public static MNJsonSerializer Serializer = null;
  }
}
