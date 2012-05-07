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
  public class MNVItemsProvider : MonoBehaviour
  {
    public const long TRANSACTION_ID_UNDEFINED = 0;
    public const int VITEM_IS_CURRENCY_MASK = 0x0001;
    public const int VITEM_IS_UNIQUE_MASK = 0x0002;
    public const int VITEM_IS_CONSUMABLE_MASK = 0x0004;
    public const int VITEM_ISSUE_ON_CLIENT_MASK = 0x0200;

    public class GameVItemInfo
    {
      public int Id {get;set;}
      public string Name {get;set;}
      public int Model {get;set;}
      public string Description {get;set;}
      public string Params {get;set;}

      public GameVItemInfo () {}
    }

    public class PlayerVItemInfo
    {
      public int Id {get;set;}
      public long Count {get;set;}

      public PlayerVItemInfo () {}
    }

    public class TransactionError
    {
      public long ClientTransactionId {get;set;}
      public long ServerTransactionId {get;set;}
      public long CorrUserId {get;set;}
      public int FailReasonCode {get;set;}
      public string ErrorMessage {get;set;}

      public TransactionError () {}
    }

    public class TransactionInfo
    {
      public long ClientTransactionId {get;set;}
      public long ServerTransactionId {get;set;}
      public long CorrUserId {get;set;}
      public MNVItemsProvider.TransactionVItemInfo[] VItems {get;set;}

      public TransactionInfo () {}
    }

    public class TransactionVItemInfo
    {
      public int Id {get;set;}
      public long Delta {get;set;}

      public TransactionVItemInfo () {}
    }

    #region MNVItemsProviderEventHandler

    public delegate void VItemsListUpdatedEventHandler();
    public event VItemsListUpdatedEventHandler VItemsListUpdated
    {
      [MethodImpl(MethodImplOptions.Synchronized)]
      add
      {
        MNTools.DLog("VItemsListUpdatedEventHandler.add");
        if (VItemsListUpdatedStorage == null) {
          VItemsListUpdatedStorage += value;

          RegisterEventHandler();
        }
        else {
          VItemsListUpdatedStorage += value;
        }
      }

      [MethodImpl(MethodImplOptions.Synchronized)]
      remove
      {
        MNTools.DLog("VItemsListUpdatedEventHandler.remove");
        VItemsListUpdatedStorage -= value;

        if (VItemsListUpdatedStorage == null) {
          UnregisterEventHandler();
        }
      }
    }
    private VItemsListUpdatedEventHandler VItemsListUpdatedStorage;

    public delegate void VItemsTransactionCompletedEventHandler(MNVItemsProvider.TransactionInfo transaction);
    public event VItemsTransactionCompletedEventHandler VItemsTransactionCompleted
    {
      [MethodImpl(MethodImplOptions.Synchronized)]
      add
      {
        MNTools.DLog("VItemsTransactionCompletedEventHandler.add");
        if (VItemsTransactionCompletedStorage == null) {
          VItemsTransactionCompletedStorage += value;

          RegisterEventHandler();
        }
        else {
          VItemsTransactionCompletedStorage += value;
        }
      }

      [MethodImpl(MethodImplOptions.Synchronized)]
      remove
      {
        MNTools.DLog("VItemsTransactionCompletedEventHandler.remove");
        VItemsTransactionCompletedStorage -= value;

        if (VItemsTransactionCompletedStorage == null) {
          UnregisterEventHandler();
        }
      }
    }
    private VItemsTransactionCompletedEventHandler VItemsTransactionCompletedStorage;

    public delegate void VItemsTransactionFailedEventHandler(MNVItemsProvider.TransactionError error);
    public event VItemsTransactionFailedEventHandler VItemsTransactionFailed
    {
      [MethodImpl(MethodImplOptions.Synchronized)]
      add
      {
        MNTools.DLog("VItemsTransactionFailedEventHandler.add");
        if (VItemsTransactionFailedStorage == null) {
          VItemsTransactionFailedStorage += value;

          RegisterEventHandler();
        }
        else {
          VItemsTransactionFailedStorage += value;
        }
      }

      [MethodImpl(MethodImplOptions.Synchronized)]
      remove
      {
        MNTools.DLog("VItemsTransactionFailedEventHandler.remove");
        VItemsTransactionFailedStorage -= value;

        if (VItemsTransactionFailedStorage == null) {
          UnregisterEventHandler();
        }
      }
    }
    private VItemsTransactionFailedEventHandler VItemsTransactionFailedStorage;

    #endregion MNVItemsProviderEventHandler

    #if UNITY_IPHONE

    public void Shutdown() {
      MNTools.DLog("MNVItemsProvider:Shutdown");

      if (Application.platform == RuntimePlatform.IPhonePlayer) {
        // Not used in iOS

      }
      else {
        throw new MNNotOnDeviceExcepton();
      }
    }

    public MNVItemsProvider.GameVItemInfo[] GetGameVItemsList() {
      MNTools.DLog("MNVItemsProvider:GetGameVItemsList");

      if (Application.platform == RuntimePlatform.IPhonePlayer) {
        List<object> deserializedArray = MNUnityCommunicator.Serializer.DeserializeArray(
          _MNVItemsProvider_GetGameVItemsList(),
          typeof(MNVItemsProvider.GameVItemInfo));

        MNVItemsProvider.GameVItemInfo[] resultArray = new MNVItemsProvider.GameVItemInfo[deserializedArray.Count];

        for (int index = 0;index < deserializedArray.Count;index++) {
          resultArray[index] = (MNVItemsProvider.GameVItemInfo)(deserializedArray[index]);
        }

        return resultArray;
      }
      else {
        throw new MNNotOnDeviceExcepton();
      }
    }

    public MNVItemsProvider.GameVItemInfo FindGameVItemById(int vItemId) {
      MNTools.DLog("MNVItemsProvider:FindGameVItemById");

      if (Application.platform == RuntimePlatform.IPhonePlayer) {
        return (MNVItemsProvider.GameVItemInfo)MNUnityCommunicator.Serializer.Deserialize(_MNVItemsProvider_FindGameVItemById(vItemId),typeof(MNVItemsProvider.GameVItemInfo));
      }
      else {
        throw new MNNotOnDeviceExcepton();
      }
    }

    public bool IsGameVItemsListNeedUpdate() {
      MNTools.DLog("MNVItemsProvider:IsGameVItemsListNeedUpdate");

      if (Application.platform == RuntimePlatform.IPhonePlayer) {
        return _MNVItemsProvider_IsGameVItemsListNeedUpdate();
      }
      else {
        throw new MNNotOnDeviceExcepton();
      }
    }

    public void DoGameVItemsListUpdate() {
      MNTools.DLog("MNVItemsProvider:DoGameVItemsListUpdate");

      if (Application.platform == RuntimePlatform.IPhonePlayer) {
        _MNVItemsProvider_DoGameVItemsListUpdate();
      }
      else {
        throw new MNNotOnDeviceExcepton();
      }
    }

    public void ReqAddPlayerVItem(int vItemId, long count, long clientTransactionId) {
      MNTools.DLog("MNVItemsProvider:ReqAddPlayerVItem");

      if (Application.platform == RuntimePlatform.IPhonePlayer) {
        _MNVItemsProvider_ReqAddPlayerVItem(vItemId, count, clientTransactionId);
      }
      else {
        throw new MNNotOnDeviceExcepton();
      }
    }

    public void ReqAddPlayerVItemTransaction(MNVItemsProvider.TransactionVItemInfo[] transactionVItems, long clientTransactionId) {
      MNTools.DLog("MNVItemsProvider:ReqAddPlayerVItemTransaction");

      if (Application.platform == RuntimePlatform.IPhonePlayer) {
        _MNVItemsProvider_ReqAddPlayerVItemTransaction(MNUnityCommunicator.Serializer.Serialize(transactionVItems), clientTransactionId);
      }
      else {
        throw new MNNotOnDeviceExcepton();
      }
    }

    public void ReqTransferPlayerVItem(int vItemId, long count, long toPlayerId, long clientTransactionId) {
      MNTools.DLog("MNVItemsProvider:ReqTransferPlayerVItem");

      if (Application.platform == RuntimePlatform.IPhonePlayer) {
        _MNVItemsProvider_ReqTransferPlayerVItem(vItemId, count, toPlayerId, clientTransactionId);
      }
      else {
        throw new MNNotOnDeviceExcepton();
      }
    }

    public void ReqTransferPlayerVItemTransaction(MNVItemsProvider.TransactionVItemInfo[] transactionVItems, long toPlayerId, long clientTransactionId) {
      MNTools.DLog("MNVItemsProvider:ReqTransferPlayerVItemTransaction");

      if (Application.platform == RuntimePlatform.IPhonePlayer) {
        _MNVItemsProvider_ReqTransferPlayerVItemTransaction(MNUnityCommunicator.Serializer.Serialize(transactionVItems), toPlayerId, clientTransactionId);
      }
      else {
        throw new MNNotOnDeviceExcepton();
      }
    }

    public MNVItemsProvider.PlayerVItemInfo[] GetPlayerVItemList() {
      MNTools.DLog("MNVItemsProvider:GetPlayerVItemList");

      if (Application.platform == RuntimePlatform.IPhonePlayer) {
        List<object> deserializedArray = MNUnityCommunicator.Serializer.DeserializeArray(
          _MNVItemsProvider_GetPlayerVItemList(),
          typeof(MNVItemsProvider.PlayerVItemInfo));

        MNVItemsProvider.PlayerVItemInfo[] resultArray = new MNVItemsProvider.PlayerVItemInfo[deserializedArray.Count];

        for (int index = 0;index < deserializedArray.Count;index++) {
          resultArray[index] = (MNVItemsProvider.PlayerVItemInfo)(deserializedArray[index]);
        }

        return resultArray;
      }
      else {
        throw new MNNotOnDeviceExcepton();
      }
    }

    public long GetPlayerVItemCountById(int vItemId) {
      MNTools.DLog("MNVItemsProvider:GetPlayerVItemCountById");

      if (Application.platform == RuntimePlatform.IPhonePlayer) {
        return _MNVItemsProvider_GetPlayerVItemCountById(vItemId);
      }
      else {
        throw new MNNotOnDeviceExcepton();
      }
    }

    public string GetVItemImageURL(int vItemId) {
      MNTools.DLog("MNVItemsProvider:GetVItemImageURL");

      if (Application.platform == RuntimePlatform.IPhonePlayer) {
        return _MNVItemsProvider_GetVItemImageURL(vItemId);
      }
      else {
        throw new MNNotOnDeviceExcepton();
      }
    }

    public long GetNewClientTransactionId() {
      MNTools.DLog("MNVItemsProvider:GetNewClientTransactionId");

      if (Application.platform == RuntimePlatform.IPhonePlayer) {
        return _MNVItemsProvider_GetNewClientTransactionId();
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

      MNTools.DLog("MNVItemsProvider:RegisterEventHandler");

      if (Application.platform == RuntimePlatform.IPhonePlayer) {
        eventHandlerRegistered = _MNVItemsProvider_RegisterEventHandler();
      }
      else {
        throw new MNNotOnDeviceExcepton();
      }
    }

    [MethodImpl(MethodImplOptions.Synchronized)]
    private void UnregisterEventHandler() {
      if ((VItemsListUpdatedStorage != null) || (VItemsTransactionCompletedStorage != null) || (VItemsTransactionFailedStorage != null)) {
        return;
      }

      MNTools.DLog("MNVItemsProvider:UnregisterEventHandler");

      if (!eventHandlerRegistered) {
        MNTools.DLog("MNVItemsProvider:EventHandler not registered");
        return;
      }

      if (Application.platform == RuntimePlatform.IPhonePlayer) {
        eventHandlerRegistered = !_MNVItemsProvider_UnregisterEventHandler();
      }
      else {
        throw new MNNotOnDeviceExcepton();
      }
    }

    #elif UNITY_ANDROID

    public void Shutdown() {
      MNTools.DLog("MNVItemsProvider:Shutdown");

      if (Application.platform == RuntimePlatform.Android) {
        MNVItemsProviderUnityClass.CallStatic("shutdown");
      }
      else {
        throw new MNNotOnDeviceExcepton();
      }
    }

    public MNVItemsProvider.GameVItemInfo[] GetGameVItemsList() {
      MNTools.DLog("MNVItemsProvider:GetGameVItemsList");

      if (Application.platform == RuntimePlatform.Android) {
        List<object> deserializedArray = MNUnityCommunicator.Serializer.DeserializeArray(
          MNVItemsProviderUnityClass.CallStatic<string>("getGameVItemsList"),
          typeof(MNVItemsProvider.GameVItemInfo));

        MNVItemsProvider.GameVItemInfo[] resultArray = new MNVItemsProvider.GameVItemInfo[deserializedArray.Count];

        for (int index = 0;index < deserializedArray.Count;index++) {
          resultArray[index] = (MNVItemsProvider.GameVItemInfo)(deserializedArray[index]);
        }

        return resultArray;
      }
      else {
        throw new MNNotOnDeviceExcepton();
      }
    }

    public MNVItemsProvider.GameVItemInfo FindGameVItemById(int vItemId) {
      MNTools.DLog("MNVItemsProvider:FindGameVItemById");

      if (Application.platform == RuntimePlatform.Android) {
        return (MNVItemsProvider.GameVItemInfo)MNUnityCommunicator.Serializer.Deserialize(MNVItemsProviderUnityClass.CallStatic<string>("findGameVItemById",vItemId),typeof(MNVItemsProvider.GameVItemInfo));
      }
      else {
        throw new MNNotOnDeviceExcepton();
      }
    }

    public bool IsGameVItemsListNeedUpdate() {
      MNTools.DLog("MNVItemsProvider:IsGameVItemsListNeedUpdate");

      if (Application.platform == RuntimePlatform.Android) {
        return MNVItemsProviderUnityClass.CallStatic<bool>("isGameVItemsListNeedUpdate");
      }
      else {
        throw new MNNotOnDeviceExcepton();
      }
    }

    public void DoGameVItemsListUpdate() {
      MNTools.DLog("MNVItemsProvider:DoGameVItemsListUpdate");

      if (Application.platform == RuntimePlatform.Android) {
        MNVItemsProviderUnityClass.CallStatic("doGameVItemsListUpdate");
      }
      else {
        throw new MNNotOnDeviceExcepton();
      }
    }

    public void ReqAddPlayerVItem(int vItemId, long count, long clientTransactionId) {
      MNTools.DLog("MNVItemsProvider:ReqAddPlayerVItem");

      if (Application.platform == RuntimePlatform.Android) {
        MNVItemsProviderUnityClass.CallStatic("reqAddPlayerVItem",vItemId, count, clientTransactionId);
      }
      else {
        throw new MNNotOnDeviceExcepton();
      }
    }

    public void ReqAddPlayerVItemTransaction(MNVItemsProvider.TransactionVItemInfo[] transactionVItems, long clientTransactionId) {
      MNTools.DLog("MNVItemsProvider:ReqAddPlayerVItemTransaction");

      if (Application.platform == RuntimePlatform.Android) {
        MNVItemsProviderUnityClass.CallStatic("reqAddPlayerVItemTransaction",MNUnityCommunicator.Serializer.Serialize(transactionVItems), clientTransactionId);
      }
      else {
        throw new MNNotOnDeviceExcepton();
      }
    }

    public void ReqTransferPlayerVItem(int vItemId, long count, long toPlayerId, long clientTransactionId) {
      MNTools.DLog("MNVItemsProvider:ReqTransferPlayerVItem");

      if (Application.platform == RuntimePlatform.Android) {
        MNVItemsProviderUnityClass.CallStatic("reqTransferPlayerVItem",vItemId, count, toPlayerId, clientTransactionId);
      }
      else {
        throw new MNNotOnDeviceExcepton();
      }
    }

    public void ReqTransferPlayerVItemTransaction(MNVItemsProvider.TransactionVItemInfo[] transactionVItems, long toPlayerId, long clientTransactionId) {
      MNTools.DLog("MNVItemsProvider:ReqTransferPlayerVItemTransaction");

      if (Application.platform == RuntimePlatform.Android) {
        MNVItemsProviderUnityClass.CallStatic("reqTransferPlayerVItemTransaction",MNUnityCommunicator.Serializer.Serialize(transactionVItems), toPlayerId, clientTransactionId);
      }
      else {
        throw new MNNotOnDeviceExcepton();
      }
    }

    public MNVItemsProvider.PlayerVItemInfo[] GetPlayerVItemList() {
      MNTools.DLog("MNVItemsProvider:GetPlayerVItemList");

      if (Application.platform == RuntimePlatform.Android) {
        List<object> deserializedArray = MNUnityCommunicator.Serializer.DeserializeArray(
          MNVItemsProviderUnityClass.CallStatic<string>("getPlayerVItemList"),
          typeof(MNVItemsProvider.PlayerVItemInfo));

        MNVItemsProvider.PlayerVItemInfo[] resultArray = new MNVItemsProvider.PlayerVItemInfo[deserializedArray.Count];

        for (int index = 0;index < deserializedArray.Count;index++) {
          resultArray[index] = (MNVItemsProvider.PlayerVItemInfo)(deserializedArray[index]);
        }

        return resultArray;
      }
      else {
        throw new MNNotOnDeviceExcepton();
      }
    }

    public long GetPlayerVItemCountById(int vItemId) {
      MNTools.DLog("MNVItemsProvider:GetPlayerVItemCountById");

      if (Application.platform == RuntimePlatform.Android) {
        return MNVItemsProviderUnityClass.CallStatic<long>("getPlayerVItemCountById",vItemId);
      }
      else {
        throw new MNNotOnDeviceExcepton();
      }
    }

    public string GetVItemImageURL(int vItemId) {
      MNTools.DLog("MNVItemsProvider:GetVItemImageURL");

      if (Application.platform == RuntimePlatform.Android) {
        return MNVItemsProviderUnityClass.CallStatic<string>("getVItemImageURL",vItemId);
      }
      else {
        throw new MNNotOnDeviceExcepton();
      }
    }

    public long GetNewClientTransactionId() {
      MNTools.DLog("MNVItemsProvider:GetNewClientTransactionId");

      if (Application.platform == RuntimePlatform.Android) {
        return MNVItemsProviderUnityClass.CallStatic<long>("getNewClientTransactionId");
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

      MNTools.DLog("MNVItemsProvider:RegisterEventHandler");

      if (Application.platform == RuntimePlatform.Android) {
        eventHandlerRegistered = MNVItemsProviderUnityClass.CallStatic<bool>("registerEventHandler");
      }
      else {
        throw new MNNotOnDeviceExcepton();
      }
    }

    [MethodImpl(MethodImplOptions.Synchronized)]
    private void UnregisterEventHandler() {
      if ((VItemsListUpdatedStorage != null) || (VItemsTransactionCompletedStorage != null) || (VItemsTransactionFailedStorage != null)) {
        return;
      }

      MNTools.DLog("MNVItemsProvider:UnregisterEventHandler");

      if (!eventHandlerRegistered) {
        MNTools.DLog("MNVItemsProvider:EventHandler not registered");
        return;
      }

      if (Application.platform == RuntimePlatform.Android) {
        eventHandlerRegistered = !MNVItemsProviderUnityClass.CallStatic<bool>("unregisterEventHandler");
      }
      else {
        throw new MNNotOnDeviceExcepton();
      }
    }

    #endif

    #region MNVItemsProviderEventHandler Messages
    private void MNUM_onVItemsListUpdated(string messageParams) {
      MNTools.DLog("MNVItemsProvider:MNUM_onVItemsListUpdated: messageParams=<" + messageParams + ">");

      if (VItemsListUpdatedStorage != null) {
        VItemsListUpdatedStorage();
      }
    }

    private void MNUM_onVItemsTransactionCompleted(string messageParams) {
      MNTools.DLog("MNVItemsProvider:MNUM_onVItemsTransactionCompleted: messageParams=<" + messageParams + ">");

      if (VItemsTransactionCompletedStorage != null) {
        List<object> paramsArray = (List<object>)MNUnityCommunicator.Serializer.Deserialize(messageParams,typeof(List<object>));

        MNTools.DLogList("MNUM_onVItemsTransactionCompleted params",paramsArray,MNTools.DEBUG_LEVEL_DETAILED);

        VItemsTransactionCompletedStorage(MNSerializerMapper.MNVItemsProviderTransactionInfoFromDictionary((IDictionary)paramsArray[0]));
      }
    }

    private void MNUM_onVItemsTransactionFailed(string messageParams) {
      MNTools.DLog("MNVItemsProvider:MNUM_onVItemsTransactionFailed: messageParams=<" + messageParams + ">");

      if (VItemsTransactionFailedStorage != null) {
        List<object> paramsArray = (List<object>)MNUnityCommunicator.Serializer.Deserialize(messageParams,typeof(List<object>));

        MNTools.DLogList("MNUM_onVItemsTransactionFailed params",paramsArray,MNTools.DEBUG_LEVEL_DETAILED);

        VItemsTransactionFailedStorage(MNSerializerMapper.MNVItemsProviderTransactionErrorFromDictionary((IDictionary)paramsArray[0]));
      }
    }

    #endregion MNVItemsProviderEventHandler Messages

    #if UNITY_IPHONE

    /*
    // Not used in iOS
    [DllImport ("__Internal")]
    private static extern void _MNVItemsProvider_Shutdown ();
    */

    [DllImport ("__Internal")]
    private static extern string _MNVItemsProvider_GetGameVItemsList ();

    [DllImport ("__Internal")]
    private static extern string _MNVItemsProvider_FindGameVItemById (int vItemId);

    [DllImport ("__Internal")]
    private static extern bool _MNVItemsProvider_IsGameVItemsListNeedUpdate ();

    [DllImport ("__Internal")]
    private static extern void _MNVItemsProvider_DoGameVItemsListUpdate ();

    [DllImport ("__Internal")]
    private static extern void _MNVItemsProvider_ReqAddPlayerVItem (int vItemId, long count, long clientTransactionId);

    [DllImport ("__Internal")]
    private static extern void _MNVItemsProvider_ReqAddPlayerVItemTransaction (string transactionVItems, long clientTransactionId);

    [DllImport ("__Internal")]
    private static extern void _MNVItemsProvider_ReqTransferPlayerVItem (int vItemId, long count, long toPlayerId, long clientTransactionId);

    [DllImport ("__Internal")]
    private static extern void _MNVItemsProvider_ReqTransferPlayerVItemTransaction (string transactionVItems, long toPlayerId, long clientTransactionId);

    [DllImport ("__Internal")]
    private static extern string _MNVItemsProvider_GetPlayerVItemList ();

    [DllImport ("__Internal")]
    private static extern long _MNVItemsProvider_GetPlayerVItemCountById (int vItemId);

    [DllImport ("__Internal")]
    private static extern string _MNVItemsProvider_GetVItemImageURL (int vItemId);

    [DllImport ("__Internal")]
    private static extern long _MNVItemsProvider_GetNewClientTransactionId ();

    [DllImport ("__Internal")]
    private static extern bool _MNVItemsProvider_RegisterEventHandler ();

    [DllImport ("__Internal")]
    private static extern bool _MNVItemsProvider_UnregisterEventHandler ();

    #elif UNITY_ANDROID

    private static AndroidJavaClass mnVItemsProviderUnityClass = null;

    private static AndroidJavaClass MNVItemsProviderUnityClass
    {
      get {
        MNTools.DLog("AndroidJavaClass MNVItemsProviderUnityClass");

        if (mnVItemsProviderUnityClass == null) {
          mnVItemsProviderUnityClass = new AndroidJavaClass("com.playphone.multinet.unity.MNVItemsProviderUnity");
        }

        return mnVItemsProviderUnityClass;
      }
    }

    #endif

    private MNVItemsProvider()
    {
      MNTools.DLog("MNVItemsProvider:MNVItemsProvider()");
    }

    private bool eventHandlerRegistered = false;
  }
}
