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
  public class MNVShopProvider : MonoBehaviour
  {
    public const int ERROR_CODE_NO_ERROR = 0;
    public const int ERROR_CODE_USER_CANCEL = -999;
    public const int ERROR_CODE_UNDEFINED = -998;
    public const int ERROR_CODE_XML_PARSE_ERROR = -997;
    public const int ERROR_CODE_XML_STRUCTURE_ERROR = -996;
    public const int ERROR_CODE_NETWORK_ERROR = -995;
    public const int ERROR_CODE_GENERIC = -994;
    public const int ERROR_CODE_WF_NOT_READY = 200;

    public class VShopCategoryInfo
    {
      public int Id {get;set;}
      public string Name {get;set;}
      public int SortPos {get;set;}

      public VShopCategoryInfo () {}
    }

    public class VShopDeliveryInfo
    {
      public int VItemId {get;set;}
      public long Amount {get;set;}

      public VShopDeliveryInfo () {}
    }

    public class VShopPackBuyRequestItem
    {
      public int Id {get;set;}
      public long Amount {get;set;}

      public VShopPackBuyRequestItem () {}
    }

    public class VShopPackInfo
    {
      public const int IS_HIDDEN_MASK = 0x0001;
      public const int IS_HOLD_SALES_MASK = 0x0002;

      public int Id {get;set;}
      public string Name {get;set;}
      public int Model {get;set;}
      public string Description {get;set;}
      public string AppParams {get;set;}
      public int SortPos {get;set;}
      public int CategoryId {get;set;}
      public MNVShopProvider.VShopDeliveryInfo[] Delivery {get;set;}
      public int PriceItemId {get;set;}
      public long PriceValue {get;set;}

      public VShopPackInfo () {}
    }

    public class CheckoutVShopPackFailInfo
    {
      public int ErrorCode {get;internal set;}
      public string ErrorMessage {get;internal set;}
      public long ClientTransactionId {get;internal set;}

      public CheckoutVShopPackFailInfo () {}
    }

    public class CheckoutVShopPackSuccessInfo
    {
      public MNVItemsProvider.TransactionInfo Transaction {get;internal set;}

      public CheckoutVShopPackSuccessInfo () {}
    }

    #region MNVShopProviderEventHandler

    public delegate void VShopInfoUpdatedEventHandler();
    public event VShopInfoUpdatedEventHandler VShopInfoUpdated
    {
      [MethodImpl(MethodImplOptions.Synchronized)]
      add
      {
        MNTools.DLog("VShopInfoUpdatedEventHandler.add");
        if (VShopInfoUpdatedStorage == null) {
          VShopInfoUpdatedStorage += value;

          RegisterEventHandler();
        }
        else {
          VShopInfoUpdatedStorage += value;
        }
      }

      [MethodImpl(MethodImplOptions.Synchronized)]
      remove
      {
        MNTools.DLog("VShopInfoUpdatedEventHandler.remove");
        VShopInfoUpdatedStorage -= value;

        if (VShopInfoUpdatedStorage == null) {
          UnregisterEventHandler();
        }
      }
    }
    private VShopInfoUpdatedEventHandler VShopInfoUpdatedStorage;

    public delegate void ShowDashboardEventHandler();
    public event ShowDashboardEventHandler ShowDashboard
    {
      [MethodImpl(MethodImplOptions.Synchronized)]
      add
      {
        MNTools.DLog("ShowDashboardEventHandler.add");
        if (ShowDashboardStorage == null) {
          ShowDashboardStorage += value;

          RegisterEventHandler();
        }
        else {
          ShowDashboardStorage += value;
        }
      }

      [MethodImpl(MethodImplOptions.Synchronized)]
      remove
      {
        MNTools.DLog("ShowDashboardEventHandler.remove");
        ShowDashboardStorage -= value;

        if (ShowDashboardStorage == null) {
          UnregisterEventHandler();
        }
      }
    }
    private ShowDashboardEventHandler ShowDashboardStorage;

    public delegate void HideDashboardEventHandler();
    public event HideDashboardEventHandler HideDashboard
    {
      [MethodImpl(MethodImplOptions.Synchronized)]
      add
      {
        MNTools.DLog("HideDashboardEventHandler.add");
        if (HideDashboardStorage == null) {
          HideDashboardStorage += value;

          RegisterEventHandler();
        }
        else {
          HideDashboardStorage += value;
        }
      }

      [MethodImpl(MethodImplOptions.Synchronized)]
      remove
      {
        MNTools.DLog("HideDashboardEventHandler.remove");
        HideDashboardStorage -= value;

        if (HideDashboardStorage == null) {
          UnregisterEventHandler();
        }
      }
    }
    private HideDashboardEventHandler HideDashboardStorage;

    public delegate void CheckoutVShopPackSuccessEventHandler(MNVShopProvider.CheckoutVShopPackSuccessInfo result);
    public event CheckoutVShopPackSuccessEventHandler CheckoutVShopPackSuccess
    {
      [MethodImpl(MethodImplOptions.Synchronized)]
      add
      {
        MNTools.DLog("CheckoutVShopPackSuccessEventHandler.add");
        if (CheckoutVShopPackSuccessStorage == null) {
          CheckoutVShopPackSuccessStorage += value;

          RegisterEventHandler();
        }
        else {
          CheckoutVShopPackSuccessStorage += value;
        }
      }

      [MethodImpl(MethodImplOptions.Synchronized)]
      remove
      {
        MNTools.DLog("CheckoutVShopPackSuccessEventHandler.remove");
        CheckoutVShopPackSuccessStorage -= value;

        if (CheckoutVShopPackSuccessStorage == null) {
          UnregisterEventHandler();
        }
      }
    }
    private CheckoutVShopPackSuccessEventHandler CheckoutVShopPackSuccessStorage;

    public delegate void CheckoutVShopPackFailEventHandler(MNVShopProvider.CheckoutVShopPackFailInfo result);
    public event CheckoutVShopPackFailEventHandler CheckoutVShopPackFail
    {
      [MethodImpl(MethodImplOptions.Synchronized)]
      add
      {
        MNTools.DLog("CheckoutVShopPackFailEventHandler.add");
        if (CheckoutVShopPackFailStorage == null) {
          CheckoutVShopPackFailStorage += value;

          RegisterEventHandler();
        }
        else {
          CheckoutVShopPackFailStorage += value;
        }
      }

      [MethodImpl(MethodImplOptions.Synchronized)]
      remove
      {
        MNTools.DLog("CheckoutVShopPackFailEventHandler.remove");
        CheckoutVShopPackFailStorage -= value;

        if (CheckoutVShopPackFailStorage == null) {
          UnregisterEventHandler();
        }
      }
    }
    private CheckoutVShopPackFailEventHandler CheckoutVShopPackFailStorage;

    public delegate void VShopReadyStatusChangedEventHandler(bool isVShopReady);
    public event VShopReadyStatusChangedEventHandler VShopReadyStatusChanged
    {
      [MethodImpl(MethodImplOptions.Synchronized)]
      add
      {
        MNTools.DLog("VShopReadyStatusChangedEventHandler.add");
        if (VShopReadyStatusChangedStorage == null) {
          VShopReadyStatusChangedStorage += value;

          RegisterEventHandler();
        }
        else {
          VShopReadyStatusChangedStorage += value;
        }
      }

      [MethodImpl(MethodImplOptions.Synchronized)]
      remove
      {
        MNTools.DLog("VShopReadyStatusChangedEventHandler.remove");
        VShopReadyStatusChangedStorage -= value;

        if (VShopReadyStatusChangedStorage == null) {
          UnregisterEventHandler();
        }
      }
    }
    private VShopReadyStatusChangedEventHandler VShopReadyStatusChangedStorage;

    #endregion MNVShopProviderEventHandler

    #if UNITY_IPHONE

    public void Shutdown() {
      MNTools.DLog("MNVShopProvider:Shutdown");

      if (Application.platform == RuntimePlatform.IPhonePlayer) {
        // Not used in iOS

      }
      else {
        throw new MNNotOnDeviceExcepton();
      }
    }

    public MNVShopProvider.VShopPackInfo[] GetVShopPackList() {
      MNTools.DLog("MNVShopProvider:GetVShopPackList");

      if (Application.platform == RuntimePlatform.IPhonePlayer) {
        List<object> deserializedArray = MNUnityCommunicator.Serializer.DeserializeArray(
          _MNVShopProvider_GetVShopPackList(),
          typeof(MNVShopProvider.VShopPackInfo));

        MNVShopProvider.VShopPackInfo[] resultArray = new MNVShopProvider.VShopPackInfo[deserializedArray.Count];

        for (int index = 0;index < deserializedArray.Count;index++) {
          resultArray[index] = (MNVShopProvider.VShopPackInfo)(deserializedArray[index]);
        }

        return resultArray;
      }
      else {
        throw new MNNotOnDeviceExcepton();
      }
    }

    public MNVShopProvider.VShopCategoryInfo[] GetVShopCategoryList() {
      MNTools.DLog("MNVShopProvider:GetVShopCategoryList");

      if (Application.platform == RuntimePlatform.IPhonePlayer) {
        List<object> deserializedArray = MNUnityCommunicator.Serializer.DeserializeArray(
          _MNVShopProvider_GetVShopCategoryList(),
          typeof(MNVShopProvider.VShopCategoryInfo));

        MNVShopProvider.VShopCategoryInfo[] resultArray = new MNVShopProvider.VShopCategoryInfo[deserializedArray.Count];

        for (int index = 0;index < deserializedArray.Count;index++) {
          resultArray[index] = (MNVShopProvider.VShopCategoryInfo)(deserializedArray[index]);
        }

        return resultArray;
      }
      else {
        throw new MNNotOnDeviceExcepton();
      }
    }

    public MNVShopProvider.VShopPackInfo FindVShopPackById(int packId) {
      MNTools.DLog("MNVShopProvider:FindVShopPackById");

      if (Application.platform == RuntimePlatform.IPhonePlayer) {
        return (MNVShopProvider.VShopPackInfo)MNUnityCommunicator.Serializer.Deserialize(_MNVShopProvider_FindVShopPackById(packId),typeof(MNVShopProvider.VShopPackInfo));
      }
      else {
        throw new MNNotOnDeviceExcepton();
      }
    }

    public MNVShopProvider.VShopCategoryInfo FindVShopCategoryById(int categoryId) {
      MNTools.DLog("MNVShopProvider:FindVShopCategoryById");

      if (Application.platform == RuntimePlatform.IPhonePlayer) {
        return (MNVShopProvider.VShopCategoryInfo)MNUnityCommunicator.Serializer.Deserialize(_MNVShopProvider_FindVShopCategoryById(categoryId),typeof(MNVShopProvider.VShopCategoryInfo));
      }
      else {
        throw new MNNotOnDeviceExcepton();
      }
    }

    public bool IsVShopInfoNeedUpdate() {
      MNTools.DLog("MNVShopProvider:IsVShopInfoNeedUpdate");

      if (Application.platform == RuntimePlatform.IPhonePlayer) {
        return _MNVShopProvider_IsVShopInfoNeedUpdate();
      }
      else {
        throw new MNNotOnDeviceExcepton();
      }
    }

    public void DoVShopInfoUpdate() {
      MNTools.DLog("MNVShopProvider:DoVShopInfoUpdate");

      if (Application.platform == RuntimePlatform.IPhonePlayer) {
        _MNVShopProvider_DoVShopInfoUpdate();
      }
      else {
        throw new MNNotOnDeviceExcepton();
      }
    }

    public string GetVShopPackImageURL(int packId) {
      MNTools.DLog("MNVShopProvider:GetVShopPackImageURL");

      if (Application.platform == RuntimePlatform.IPhonePlayer) {
        return _MNVShopProvider_GetVShopPackImageURL(packId);
      }
      else {
        throw new MNNotOnDeviceExcepton();
      }
    }

    public bool IsVShopReady() {
      MNTools.DLog("MNVShopProvider:IsVShopReady");

      if (Application.platform == RuntimePlatform.IPhonePlayer) {
        return _MNVShopProvider_IsVShopReady();
      }
      else {
        throw new MNNotOnDeviceExcepton();
      }
    }

    public void ExecCheckoutVShopPacks(int[] packIdArray, int[] packCountArray, long clientTransactionId) {
      MNTools.DLog("MNVShopProvider:ExecCheckoutVShopPacks");

      if (Application.platform == RuntimePlatform.IPhonePlayer) {
        _MNVShopProvider_ExecCheckoutVShopPacks(MNUnityCommunicator.Serializer.Serialize(packIdArray), MNUnityCommunicator.Serializer.Serialize(packCountArray), clientTransactionId);
      }
      else {
        throw new MNNotOnDeviceExcepton();
      }
    }

    public void ProcCheckoutVShopPacksSilent(int[] packIdArray, int[] packCountArray, long clientTransactionId) {
      MNTools.DLog("MNVShopProvider:ProcCheckoutVShopPacksSilent");

      if (Application.platform == RuntimePlatform.IPhonePlayer) {
        _MNVShopProvider_ProcCheckoutVShopPacksSilent(MNUnityCommunicator.Serializer.Serialize(packIdArray), MNUnityCommunicator.Serializer.Serialize(packCountArray), clientTransactionId);
      }
      else {
        throw new MNNotOnDeviceExcepton();
      }
    }

    [MethodImpl(MethodImplOptions.Synchronized)]
    private void RegisterEventHandler() {
      MNTools.DLog("MNVShopProvider:RegisterEventHandler");

      if (eventHandlerRegistered) {
        MNTools.DLog("MNVShopProvider:EventHandler already registered");
        return;
      }

      if (Application.platform == RuntimePlatform.IPhonePlayer) {
        eventHandlerRegistered = _MNVShopProvider_RegisterEventHandler();
      }
      else {
        throw new MNNotOnDeviceExcepton();
      }
    }

    [MethodImpl(MethodImplOptions.Synchronized)]
    private void UnregisterEventHandler() {
      MNTools.DLog("MNVShopProvider:UnregisterEventHandler");

      if (!eventHandlerRegistered) {
        MNTools.DLog("MNVShopProvider:EventHandler not registered");
        return;
      }

      if (Application.platform == RuntimePlatform.IPhonePlayer) {
        eventHandlerRegistered = !_MNVShopProvider_UnregisterEventHandler();
      }
      else {
        throw new MNNotOnDeviceExcepton();
      }
    }

    #elif UNITY_ANDROID

    public void Shutdown() {
      MNTools.DLog("MNVShopProvider:Shutdown");

      if (Application.platform == RuntimePlatform.Android) {
        MNVShopProviderUnityClass.CallStatic("shutdown");
      }
      else {
        throw new MNNotOnDeviceExcepton();
      }
    }

    public MNVShopProvider.VShopPackInfo[] GetVShopPackList() {
      MNTools.DLog("MNVShopProvider:GetVShopPackList");

      if (Application.platform == RuntimePlatform.Android) {
        List<object> deserializedArray = MNUnityCommunicator.Serializer.DeserializeArray(
          MNVShopProviderUnityClass.CallStatic<string>("getVShopPackList"),
          typeof(MNVShopProvider.VShopPackInfo));

        MNVShopProvider.VShopPackInfo[] resultArray = new MNVShopProvider.VShopPackInfo[deserializedArray.Count];

        for (int index = 0;index < deserializedArray.Count;index++) {
          resultArray[index] = (MNVShopProvider.VShopPackInfo)(deserializedArray[index]);
        }

        return resultArray;
      }
      else {
        throw new MNNotOnDeviceExcepton();
      }
    }

    public MNVShopProvider.VShopCategoryInfo[] GetVShopCategoryList() {
      MNTools.DLog("MNVShopProvider:GetVShopCategoryList");

      if (Application.platform == RuntimePlatform.Android) {
        List<object> deserializedArray = MNUnityCommunicator.Serializer.DeserializeArray(
          MNVShopProviderUnityClass.CallStatic<string>("getVShopCategoryList"),
          typeof(MNVShopProvider.VShopCategoryInfo));

        MNVShopProvider.VShopCategoryInfo[] resultArray = new MNVShopProvider.VShopCategoryInfo[deserializedArray.Count];

        for (int index = 0;index < deserializedArray.Count;index++) {
          resultArray[index] = (MNVShopProvider.VShopCategoryInfo)(deserializedArray[index]);
        }

        return resultArray;
      }
      else {
        throw new MNNotOnDeviceExcepton();
      }
    }

    public MNVShopProvider.VShopPackInfo FindVShopPackById(int packId) {
      MNTools.DLog("MNVShopProvider:FindVShopPackById");

      if (Application.platform == RuntimePlatform.Android) {
        return (MNVShopProvider.VShopPackInfo)MNUnityCommunicator.Serializer.Deserialize(MNVShopProviderUnityClass.CallStatic<string>("findVShopPackById",packId),typeof(MNVShopProvider.VShopPackInfo));
      }
      else {
        throw new MNNotOnDeviceExcepton();
      }
    }

    public MNVShopProvider.VShopCategoryInfo FindVShopCategoryById(int categoryId) {
      MNTools.DLog("MNVShopProvider:FindVShopCategoryById");

      if (Application.platform == RuntimePlatform.Android) {
        return (MNVShopProvider.VShopCategoryInfo)MNUnityCommunicator.Serializer.Deserialize(MNVShopProviderUnityClass.CallStatic<string>("findVShopCategoryById",categoryId),typeof(MNVShopProvider.VShopCategoryInfo));
      }
      else {
        throw new MNNotOnDeviceExcepton();
      }
    }

    public bool IsVShopInfoNeedUpdate() {
      MNTools.DLog("MNVShopProvider:IsVShopInfoNeedUpdate");

      if (Application.platform == RuntimePlatform.Android) {
        return MNVShopProviderUnityClass.CallStatic<bool>("isVShopInfoNeedUpdate");
      }
      else {
        throw new MNNotOnDeviceExcepton();
      }
    }

    public void DoVShopInfoUpdate() {
      MNTools.DLog("MNVShopProvider:DoVShopInfoUpdate");

      if (Application.platform == RuntimePlatform.Android) {
        MNVShopProviderUnityClass.CallStatic("doVShopInfoUpdate");
      }
      else {
        throw new MNNotOnDeviceExcepton();
      }
    }

    public string GetVShopPackImageURL(int packId) {
      MNTools.DLog("MNVShopProvider:GetVShopPackImageURL");

      if (Application.platform == RuntimePlatform.Android) {
        return MNVShopProviderUnityClass.CallStatic<string>("getVShopPackImageURL",packId);
      }
      else {
        throw new MNNotOnDeviceExcepton();
      }
    }

    public bool IsVShopReady() {
      MNTools.DLog("MNVShopProvider:IsVShopReady");

      if (Application.platform == RuntimePlatform.Android) {
        return MNVShopProviderUnityClass.CallStatic<bool>("isVShopReady");
      }
      else {
        throw new MNNotOnDeviceExcepton();
      }
    }

    public void ExecCheckoutVShopPacks(int[] packIdArray, int[] packCountArray, long clientTransactionId) {
      MNTools.DLog("MNVShopProvider:ExecCheckoutVShopPacks");

      if (Application.platform == RuntimePlatform.Android) {
        MNVShopProviderUnityClass.CallStatic("execCheckoutVShopPacks",MNUnityCommunicator.Serializer.Serialize(packIdArray), MNUnityCommunicator.Serializer.Serialize(packCountArray), clientTransactionId);
      }
      else {
        throw new MNNotOnDeviceExcepton();
      }
    }

    public void ProcCheckoutVShopPacksSilent(int[] packIdArray, int[] packCountArray, long clientTransactionId) {
      MNTools.DLog("MNVShopProvider:ProcCheckoutVShopPacksSilent");

      if (Application.platform == RuntimePlatform.Android) {
        MNVShopProviderUnityClass.CallStatic("procCheckoutVShopPacksSilent",MNUnityCommunicator.Serializer.Serialize(packIdArray), MNUnityCommunicator.Serializer.Serialize(packCountArray), clientTransactionId);
      }
      else {
        throw new MNNotOnDeviceExcepton();
      }
    }

    [MethodImpl(MethodImplOptions.Synchronized)]
    private void RegisterEventHandler() {
      MNTools.DLog("MNVShopProvider:RegisterEventHandler");

      if (eventHandlerRegistered) {
        MNTools.DLog("MNVShopProvider:EventHandler already registered");
        return;
      }

      if (Application.platform == RuntimePlatform.Android) {
        eventHandlerRegistered = MNVShopProviderUnityClass.CallStatic<bool>("registerEventHandler");
      }
      else {
        throw new MNNotOnDeviceExcepton();
      }
    }

    [MethodImpl(MethodImplOptions.Synchronized)]
    private void UnregisterEventHandler() {
      MNTools.DLog("MNVShopProvider:UnregisterEventHandler");

      if (!eventHandlerRegistered) {
        MNTools.DLog("MNVShopProvider:EventHandler not registered");
        return;
      }

      if (Application.platform == RuntimePlatform.Android) {
        eventHandlerRegistered = !MNVShopProviderUnityClass.CallStatic<bool>("unregisterEventHandler");
      }
      else {
        throw new MNNotOnDeviceExcepton();
      }
    }

    #endif

    #region MNVShopProviderEventHandler Messages
    private void MNUM_onVShopInfoUpdated(string messageParams) {
      MNTools.DLog("MNVShopProvider:MNUM_onVShopInfoUpdated: messageParams=<" + messageParams + ">");

      if (VShopInfoUpdatedStorage != null) {
        VShopInfoUpdatedStorage();
      }
    }

    private void MNUM_showDashboard(string messageParams) {
      MNTools.DLog("MNVShopProvider:MNUM_showDashboard: messageParams=<" + messageParams + ">");

      if (ShowDashboardStorage != null) {
        ShowDashboardStorage();
      }
    }

    private void MNUM_hideDashboard(string messageParams) {
      MNTools.DLog("MNVShopProvider:MNUM_hideDashboard: messageParams=<" + messageParams + ">");

      if (HideDashboardStorage != null) {
        HideDashboardStorage();
      }
    }

    private void MNUM_onCheckoutVShopPackSuccess(string messageParams) {
      MNTools.DLog("MNVShopProvider:MNUM_onCheckoutVShopPackSuccess: messageParams=<" + messageParams + ">");

      if (CheckoutVShopPackSuccessStorage != null) {
        List<object> paramsArray = (List<object>)MNUnityCommunicator.Serializer.Deserialize(messageParams,typeof(List<object>));

        MNTools.DetailedLogList("MNUM_onCheckoutVShopPackSuccess params",paramsArray);

        CheckoutVShopPackSuccessStorage(MNSerializerMapper.MNVShopProviderCheckoutVShopPackSuccessInfoFromDictionary((IDictionary)paramsArray[0]));
      }
    }

    private void MNUM_onCheckoutVShopPackFail(string messageParams) {
      MNTools.DLog("MNVShopProvider:MNUM_onCheckoutVShopPackFail: messageParams=<" + messageParams + ">");

      if (CheckoutVShopPackFailStorage != null) {
        List<object> paramsArray = (List<object>)MNUnityCommunicator.Serializer.Deserialize(messageParams,typeof(List<object>));

        MNTools.DetailedLogList("MNUM_onCheckoutVShopPackFail params",paramsArray);

        CheckoutVShopPackFailStorage(MNSerializerMapper.MNVShopProviderCheckoutVShopPackFailInfoFromDictionary((IDictionary)paramsArray[0]));
      }
    }

    private void MNUM_onVShopReadyStatusChanged(string messageParams) {
      MNTools.DLog("MNVShopProvider:MNUM_onVShopReadyStatusChanged: messageParams=<" + messageParams + ">");

      if (VShopReadyStatusChangedStorage != null) {
        List<object> paramsArray = (List<object>)MNUnityCommunicator.Serializer.Deserialize(messageParams,typeof(List<object>));

        MNTools.DetailedLogList("MNUM_onVShopReadyStatusChanged params",paramsArray);

        VShopReadyStatusChangedStorage(Convert.ToBoolean(paramsArray[0]));
      }
    }

    #endregion MNVShopProviderEventHandler Messages

    #if UNITY_IPHONE

    /*
    // Not used in iOS
    [DllImport ("__Internal")]
    private static extern void _MNVShopProvider_Shutdown ();
    */

    [DllImport ("__Internal")]
    private static extern string _MNVShopProvider_GetVShopPackList ();

    [DllImport ("__Internal")]
    private static extern string _MNVShopProvider_GetVShopCategoryList ();

    [DllImport ("__Internal")]
    private static extern string _MNVShopProvider_FindVShopPackById (int packId);

    [DllImport ("__Internal")]
    private static extern string _MNVShopProvider_FindVShopCategoryById (int categoryId);

    [DllImport ("__Internal")]
    private static extern bool _MNVShopProvider_IsVShopInfoNeedUpdate ();

    [DllImport ("__Internal")]
    private static extern void _MNVShopProvider_DoVShopInfoUpdate ();

    [DllImport ("__Internal")]
    private static extern string _MNVShopProvider_GetVShopPackImageURL (int packId);

    [DllImport ("__Internal")]
    private static extern bool _MNVShopProvider_IsVShopReady ();

    [DllImport ("__Internal")]
    private static extern void _MNVShopProvider_ExecCheckoutVShopPacks (string packIdArray, string packCountArray, long clientTransactionId);

    [DllImport ("__Internal")]
    private static extern void _MNVShopProvider_ProcCheckoutVShopPacksSilent (string packIdArray, string packCountArray, long clientTransactionId);

    [DllImport ("__Internal")]
    private static extern bool _MNVShopProvider_RegisterEventHandler ();

    [DllImport ("__Internal")]
    private static extern bool _MNVShopProvider_UnregisterEventHandler ();

    #elif UNITY_ANDROID

    private static AndroidJavaClass mnVShopProviderUnityClass = null;

    private static AndroidJavaClass MNVShopProviderUnityClass
    {
      get {
        MNTools.DLog("AndroidJavaClass MNVShopProviderUnityClass");

        if (mnVShopProviderUnityClass == null) {
          mnVShopProviderUnityClass = new AndroidJavaClass("com.playphone.multinet.unity.MNVShopProviderUnity");
        }

        return mnVShopProviderUnityClass;
      }
    }

    #endif

    private MNVShopProvider()
    {
      MNTools.DLog("MNVShopProvider:MNVShopProvider()");
    }

    private bool eventHandlerRegistered = false;
  }
}
