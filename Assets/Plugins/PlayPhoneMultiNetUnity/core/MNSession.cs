using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using System.Runtime.CompilerServices;
using PlayPhone.MultiNet.Core;
using PlayPhone.MultiNet.Providers;

namespace PlayPhone.MultiNet.Core
{
  public class MNSession : MonoBehaviour
  {
    public const int MN_LOBBY_ROOM_ID_UNDEFINED = -1;
    public const int MN_ROOM_ID_UNDEFINED = -1;
    public const int MN_CREDENTIALS_WIPE_NONE = 0;
    public const int MN_CREDENTIALS_WIPE_USER = 1;
    public const int MN_CREDENTIALS_WIPE_ALL = 2;

    #region MNSessionEventHandler

    public delegate void SessionStatusChangedEventHandler(int newStatus, int oldStatus);
    public event SessionStatusChangedEventHandler SessionStatusChanged
    {
      [MethodImpl(MethodImplOptions.Synchronized)]
      add
      {
        MNTools.DLog("SessionStatusChangedEventHandler.add");
        if (SessionStatusChangedStorage == null) {
          SessionStatusChangedStorage += value;

          RegisterEventHandler();
        }
        else {
          SessionStatusChangedStorage += value;
        }
      }

      [MethodImpl(MethodImplOptions.Synchronized)]
      remove
      {
        MNTools.DLog("SessionStatusChangedEventHandler.remove");
        SessionStatusChangedStorage -= value;

        if (SessionStatusChangedStorage == null) {
          UnregisterEventHandler();
        }
      }
    }
    private SessionStatusChangedEventHandler SessionStatusChangedStorage;

    public delegate void UserChangedEventHandler(long userId);
    public event UserChangedEventHandler UserChanged
    {
      [MethodImpl(MethodImplOptions.Synchronized)]
      add
      {
        MNTools.DLog("UserChangedEventHandler.add");
        if (UserChangedStorage == null) {
          UserChangedStorage += value;

          RegisterEventHandler();
        }
        else {
          UserChangedStorage += value;
        }
      }

      [MethodImpl(MethodImplOptions.Synchronized)]
      remove
      {
        MNTools.DLog("UserChangedEventHandler.remove");
        UserChangedStorage -= value;

        if (UserChangedStorage == null) {
          UnregisterEventHandler();
        }
      }
    }
    private UserChangedEventHandler UserChangedStorage;

    public delegate void RoomUserJoinEventHandler(MNUserInfo userInfo);
    public event RoomUserJoinEventHandler RoomUserJoin
    {
      [MethodImpl(MethodImplOptions.Synchronized)]
      add
      {
        MNTools.DLog("RoomUserJoinEventHandler.add");
        if (RoomUserJoinStorage == null) {
          RoomUserJoinStorage += value;

          RegisterEventHandler();
        }
        else {
          RoomUserJoinStorage += value;
        }
      }

      [MethodImpl(MethodImplOptions.Synchronized)]
      remove
      {
        MNTools.DLog("RoomUserJoinEventHandler.remove");
        RoomUserJoinStorage -= value;

        if (RoomUserJoinStorage == null) {
          UnregisterEventHandler();
        }
      }
    }
    private RoomUserJoinEventHandler RoomUserJoinStorage;

    public delegate void RoomUserLeaveEventHandler(MNUserInfo userInfo);
    public event RoomUserLeaveEventHandler RoomUserLeave
    {
      [MethodImpl(MethodImplOptions.Synchronized)]
      add
      {
        MNTools.DLog("RoomUserLeaveEventHandler.add");
        if (RoomUserLeaveStorage == null) {
          RoomUserLeaveStorage += value;

          RegisterEventHandler();
        }
        else {
          RoomUserLeaveStorage += value;
        }
      }

      [MethodImpl(MethodImplOptions.Synchronized)]
      remove
      {
        MNTools.DLog("RoomUserLeaveEventHandler.remove");
        RoomUserLeaveStorage -= value;

        if (RoomUserLeaveStorage == null) {
          UnregisterEventHandler();
        }
      }
    }
    private RoomUserLeaveEventHandler RoomUserLeaveStorage;

    public delegate void GameMessageReceivedEventHandler(string message, MNUserInfo sender);
    public event GameMessageReceivedEventHandler GameMessageReceived
    {
      [MethodImpl(MethodImplOptions.Synchronized)]
      add
      {
        MNTools.DLog("GameMessageReceivedEventHandler.add");
        if (GameMessageReceivedStorage == null) {
          GameMessageReceivedStorage += value;

          RegisterEventHandler();
        }
        else {
          GameMessageReceivedStorage += value;
        }
      }

      [MethodImpl(MethodImplOptions.Synchronized)]
      remove
      {
        MNTools.DLog("GameMessageReceivedEventHandler.remove");
        GameMessageReceivedStorage -= value;

        if (GameMessageReceivedStorage == null) {
          UnregisterEventHandler();
        }
      }
    }
    private GameMessageReceivedEventHandler GameMessageReceivedStorage;

    public delegate void ErrorOccurredEventHandler(MNErrorInfo errorInfo);
    public event ErrorOccurredEventHandler ErrorOccurred
    {
      [MethodImpl(MethodImplOptions.Synchronized)]
      add
      {
        MNTools.DLog("ErrorOccurredEventHandler.add");
        if (ErrorOccurredStorage == null) {
          ErrorOccurredStorage += value;

          RegisterEventHandler();
        }
        else {
          ErrorOccurredStorage += value;
        }
      }

      [MethodImpl(MethodImplOptions.Synchronized)]
      remove
      {
        MNTools.DLog("ErrorOccurredEventHandler.remove");
        ErrorOccurredStorage -= value;

        if (ErrorOccurredStorage == null) {
          UnregisterEventHandler();
        }
      }
    }
    private ErrorOccurredEventHandler ErrorOccurredStorage;

    public delegate void ExecAppCommandReceivedEventHandler(string cmdName, string cmdParam);
    public event ExecAppCommandReceivedEventHandler ExecAppCommandReceived
    {
      [MethodImpl(MethodImplOptions.Synchronized)]
      add
      {
        MNTools.DLog("ExecAppCommandReceivedEventHandler.add");
        if (ExecAppCommandReceivedStorage == null) {
          ExecAppCommandReceivedStorage += value;

          RegisterEventHandler();
        }
        else {
          ExecAppCommandReceivedStorage += value;
        }
      }

      [MethodImpl(MethodImplOptions.Synchronized)]
      remove
      {
        MNTools.DLog("ExecAppCommandReceivedEventHandler.remove");
        ExecAppCommandReceivedStorage -= value;

        if (ExecAppCommandReceivedStorage == null) {
          UnregisterEventHandler();
        }
      }
    }
    private ExecAppCommandReceivedEventHandler ExecAppCommandReceivedStorage;

    public delegate void ExecUICommandReceivedEventHandler(string cmdName, string cmdParam);
    public event ExecUICommandReceivedEventHandler ExecUICommandReceived
    {
      [MethodImpl(MethodImplOptions.Synchronized)]
      add
      {
        MNTools.DLog("ExecUICommandReceivedEventHandler.add");
        if (ExecUICommandReceivedStorage == null) {
          ExecUICommandReceivedStorage += value;

          RegisterEventHandler();
        }
        else {
          ExecUICommandReceivedStorage += value;
        }
      }

      [MethodImpl(MethodImplOptions.Synchronized)]
      remove
      {
        MNTools.DLog("ExecUICommandReceivedEventHandler.remove");
        ExecUICommandReceivedStorage -= value;

        if (ExecUICommandReceivedStorage == null) {
          UnregisterEventHandler();
        }
      }
    }
    private ExecUICommandReceivedEventHandler ExecUICommandReceivedStorage;

    #endregion MNSessionEventHandler

    #if UNITY_IPHONE

    public MNUserInfo GetMyUserInfo() {
      MNTools.DLog("MNSession:GetMyUserInfo");

      if (Application.platform == RuntimePlatform.IPhonePlayer) {
        return (MNUserInfo)MNUnityCommunicator.Serializer.Deserialize(_MNSession_GetMyUserInfo(),typeof(MNUserInfo));
      }
      else {
        throw new MNNotOnDeviceExcepton();
      }
    }

    public void ReqJoinRandomRoom(string gameSetId) {
      MNTools.DLog("MNSession:ReqJoinRandomRoom");

      if (Application.platform == RuntimePlatform.IPhonePlayer) {
        _MNSession_ReqJoinRandomRoom(gameSetId);
      }
      else {
        throw new MNNotOnDeviceExcepton();
      }
    }

    public string GetMyUserName() {
      MNTools.DLog("MNSession:GetMyUserName");

      if (Application.platform == RuntimePlatform.IPhonePlayer) {
        return _MNSession_GetMyUserName();
      }
      else {
        throw new MNNotOnDeviceExcepton();
      }
    }

    public void ExecUICommand(string name, string param) {
      MNTools.DLog("MNSession:ExecUICommand");

      if (Application.platform == RuntimePlatform.IPhonePlayer) {
        _MNSession_ExecUICommand(name, param);
      }
      else {
        throw new MNNotOnDeviceExcepton();
      }
    }

    public int GetStatus() {
      MNTools.DLog("MNSession:GetStatus");

      if (Application.platform == RuntimePlatform.IPhonePlayer) {
        return _MNSession_GetStatus();
      }
      else {
        throw new MNNotOnDeviceExcepton();
      }
    }

    public void LeaveRoom() {
      MNTools.DLog("MNSession:LeaveRoom");

      if (Application.platform == RuntimePlatform.IPhonePlayer) {
        _MNSession_LeaveRoom();
      }
      else {
        throw new MNNotOnDeviceExcepton();
      }
    }

    public int GetRoomUserStatus() {
      MNTools.DLog("MNSession:GetRoomUserStatus");

      if (Application.platform == RuntimePlatform.IPhonePlayer) {
        return _MNSession_GetRoomUserStatus();
      }
      else {
        throw new MNNotOnDeviceExcepton();
      }
    }

    public int GetCurrentRoomId() {
      MNTools.DLog("MNSession:GetCurrentRoomId");

      if (Application.platform == RuntimePlatform.IPhonePlayer) {
        return _MNSession_GetCurrentRoomId();
      }
      else {
        throw new MNNotOnDeviceExcepton();
      }
    }

    public void ReqCreateBuddyRoom(MNBuddyRoomParams buddyRoomParams) {
      MNTools.DLog("MNSession:ReqCreateBuddyRoom");

      if (Application.platform == RuntimePlatform.IPhonePlayer) {
        _MNSession_ReqCreateBuddyRoom(MNUnityCommunicator.Serializer.Serialize(buddyRoomParams));
      }
      else {
        throw new MNNotOnDeviceExcepton();
      }
    }

    public void ReqJoinBuddyRoom(int roomSFId) {
      MNTools.DLog("MNSession:ReqJoinBuddyRoom");

      if (Application.platform == RuntimePlatform.IPhonePlayer) {
        _MNSession_ReqJoinBuddyRoom(roomSFId);
      }
      else {
        throw new MNNotOnDeviceExcepton();
      }
    }

    public void SendJoinRoomInvitationResponse(MNJoinRoomInvitationParams invitationParams, bool accept) {
      MNTools.DLog("MNSession:SendJoinRoomInvitationResponse");

      if (Application.platform == RuntimePlatform.IPhonePlayer) {
        _MNSession_SendJoinRoomInvitationResponse(MNUnityCommunicator.Serializer.Serialize(invitationParams), accept);
      }
      else {
        throw new MNNotOnDeviceExcepton();
      }
    }

    public bool LoginAuto() {
      MNTools.DLog("MNSession:LoginAuto");

      if (Application.platform == RuntimePlatform.IPhonePlayer) {
        return _MNSession_LoginAuto();
      }
      else {
        throw new MNNotOnDeviceExcepton();
      }
    }

    public MNUserInfo[] GetRoomUserList() {
      MNTools.DLog("MNSession:GetRoomUserList");

      if (Application.platform == RuntimePlatform.IPhonePlayer) {
        List<object> deserializedArray = MNUnityCommunicator.Serializer.DeserializeArray(
          _MNSession_GetRoomUserList(),
          typeof(MNUserInfo));

        MNUserInfo[] resultArray = new MNUserInfo[deserializedArray.Count];

        for (int index = 0;index < deserializedArray.Count;index++) {
          resultArray[index] = (MNUserInfo)(deserializedArray[index]);
        }

        return resultArray;
      }
      else {
        throw new MNNotOnDeviceExcepton();
      }
    }

    [MethodImpl(MethodImplOptions.Synchronized)]
    private void RegisterEventHandler() {
      MNTools.DLog("MNSession:RegisterEventHandler");

      if (eventHandlerRegistered) {
        MNTools.DLog("MNSession:EventHandler already registered");
        return;
      }

      if (Application.platform == RuntimePlatform.IPhonePlayer) {
        eventHandlerRegistered = _MNSession_RegisterEventHandler();
      }
      else {
        throw new MNNotOnDeviceExcepton();
      }
    }

    [MethodImpl(MethodImplOptions.Synchronized)]
    private void UnregisterEventHandler() {
      MNTools.DLog("MNSession:UnregisterEventHandler");

      if (!eventHandlerRegistered) {
        MNTools.DLog("MNSession:EventHandler not registered");
        return;
      }

      if (Application.platform == RuntimePlatform.IPhonePlayer) {
        eventHandlerRegistered = !_MNSession_UnregisterEventHandler();
      }
      else {
        throw new MNNotOnDeviceExcepton();
      }
    }

    #elif UNITY_ANDROID

    public MNUserInfo GetMyUserInfo() {
      MNTools.DLog("MNSession:GetMyUserInfo");

      if (Application.platform == RuntimePlatform.Android) {
        return (MNUserInfo)MNUnityCommunicator.Serializer.Deserialize(MNSessionUnityClass.CallStatic<string>("getMyUserInfo"),typeof(MNUserInfo));
      }
      else {
        throw new MNNotOnDeviceExcepton();
      }
    }

    public void ReqJoinRandomRoom(string gameSetId) {
      MNTools.DLog("MNSession:ReqJoinRandomRoom");

      if (Application.platform == RuntimePlatform.Android) {
        MNSessionUnityClass.CallStatic("reqJoinRandomRoom",gameSetId);
      }
      else {
        throw new MNNotOnDeviceExcepton();
      }
    }

    public string GetMyUserName() {
      MNTools.DLog("MNSession:GetMyUserName");

      if (Application.platform == RuntimePlatform.Android) {
        return MNSessionUnityClass.CallStatic<string>("getMyUserName");
      }
      else {
        throw new MNNotOnDeviceExcepton();
      }
    }

    public void ExecUICommand(string name, string param) {
      MNTools.DLog("MNSession:ExecUICommand");

      if (Application.platform == RuntimePlatform.Android) {
        MNSessionUnityClass.CallStatic("execUICommand",name, param);
      }
      else {
        throw new MNNotOnDeviceExcepton();
      }
    }

    public int GetStatus() {
      MNTools.DLog("MNSession:GetStatus");

      if (Application.platform == RuntimePlatform.Android) {
        return MNSessionUnityClass.CallStatic<int>("getStatus");
      }
      else {
        throw new MNNotOnDeviceExcepton();
      }
    }

    public void LeaveRoom() {
      MNTools.DLog("MNSession:LeaveRoom");

      if (Application.platform == RuntimePlatform.Android) {
        MNSessionUnityClass.CallStatic("leaveRoom");
      }
      else {
        throw new MNNotOnDeviceExcepton();
      }
    }

    public int GetRoomUserStatus() {
      MNTools.DLog("MNSession:GetRoomUserStatus");

      if (Application.platform == RuntimePlatform.Android) {
        return MNSessionUnityClass.CallStatic<int>("getRoomUserStatus");
      }
      else {
        throw new MNNotOnDeviceExcepton();
      }
    }

    public int GetCurrentRoomId() {
      MNTools.DLog("MNSession:GetCurrentRoomId");

      if (Application.platform == RuntimePlatform.Android) {
        return MNSessionUnityClass.CallStatic<int>("getCurrentRoomId");
      }
      else {
        throw new MNNotOnDeviceExcepton();
      }
    }

    public void ReqCreateBuddyRoom(MNBuddyRoomParams buddyRoomParams) {
      MNTools.DLog("MNSession:ReqCreateBuddyRoom");

      if (Application.platform == RuntimePlatform.Android) {
        MNSessionUnityClass.CallStatic("reqCreateBuddyRoom",MNUnityCommunicator.Serializer.Serialize(buddyRoomParams));
      }
      else {
        throw new MNNotOnDeviceExcepton();
      }
    }

    public void ReqJoinBuddyRoom(int roomSFId) {
      MNTools.DLog("MNSession:ReqJoinBuddyRoom");

      if (Application.platform == RuntimePlatform.Android) {
        MNSessionUnityClass.CallStatic("reqJoinBuddyRoom",roomSFId);
      }
      else {
        throw new MNNotOnDeviceExcepton();
      }
    }

    public void SendJoinRoomInvitationResponse(MNJoinRoomInvitationParams invitationParams, bool accept) {
      MNTools.DLog("MNSession:SendJoinRoomInvitationResponse");

      if (Application.platform == RuntimePlatform.Android) {
        MNSessionUnityClass.CallStatic("sendJoinRoomInvitationResponse",MNUnityCommunicator.Serializer.Serialize(invitationParams), accept);
      }
      else {
        throw new MNNotOnDeviceExcepton();
      }
    }

    public bool LoginAuto() {
      MNTools.DLog("MNSession:LoginAuto");

      if (Application.platform == RuntimePlatform.Android) {
        return MNSessionUnityClass.CallStatic<bool>("loginAuto");
      }
      else {
        throw new MNNotOnDeviceExcepton();
      }
    }

    public MNUserInfo[] GetRoomUserList() {
      MNTools.DLog("MNSession:GetRoomUserList");

      if (Application.platform == RuntimePlatform.Android) {
        List<object> deserializedArray = MNUnityCommunicator.Serializer.DeserializeArray(
          MNSessionUnityClass.CallStatic<string>("getRoomUserList"),
          typeof(MNUserInfo));

        MNUserInfo[] resultArray = new MNUserInfo[deserializedArray.Count];

        for (int index = 0;index < deserializedArray.Count;index++) {
          resultArray[index] = (MNUserInfo)(deserializedArray[index]);
        }

        return resultArray;
      }
      else {
        throw new MNNotOnDeviceExcepton();
      }
    }

    [MethodImpl(MethodImplOptions.Synchronized)]
    private void RegisterEventHandler() {
      MNTools.DLog("MNSession:RegisterEventHandler");

      if (eventHandlerRegistered) {
        MNTools.DLog("MNSession:EventHandler already registered");
        return;
      }

      if (Application.platform == RuntimePlatform.Android) {
        eventHandlerRegistered = MNSessionUnityClass.CallStatic<bool>("registerEventHandler");
      }
      else {
        throw new MNNotOnDeviceExcepton();
      }
    }

    [MethodImpl(MethodImplOptions.Synchronized)]
    private void UnregisterEventHandler() {
      MNTools.DLog("MNSession:UnregisterEventHandler");

      if (!eventHandlerRegistered) {
        MNTools.DLog("MNSession:EventHandler not registered");
        return;
      }

      if (Application.platform == RuntimePlatform.Android) {
        eventHandlerRegistered = !MNSessionUnityClass.CallStatic<bool>("unregisterEventHandler");
      }
      else {
        throw new MNNotOnDeviceExcepton();
      }
    }

    #endif

    #region MNSessionEventHandler Messages
    private void MNUM_mnSessionStatusChanged(string messageParams) {
      MNTools.DLog("MNSession:MNUM_mnSessionStatusChanged: messageParams=<" + messageParams + ">");

      if (SessionStatusChangedStorage != null) {
        List<object> paramsArray = (List<object>)MNUnityCommunicator.Serializer.Deserialize(messageParams,typeof(List<object>));

        MNTools.DetailedLogList("MNUM_mnSessionStatusChanged params",paramsArray);

        SessionStatusChangedStorage(Convert.ToInt32(paramsArray[0]), Convert.ToInt32(paramsArray[1]));
      }
    }

    private void MNUM_mnSessionUserChanged(string messageParams) {
      MNTools.DLog("MNSession:MNUM_mnSessionUserChanged: messageParams=<" + messageParams + ">");

      if (UserChangedStorage != null) {
        List<object> paramsArray = (List<object>)MNUnityCommunicator.Serializer.Deserialize(messageParams,typeof(List<object>));

        MNTools.DetailedLogList("MNUM_mnSessionUserChanged params",paramsArray);

        UserChangedStorage(Convert.ToInt64(paramsArray[0]));
      }
    }

    private void MNUM_mnSessionRoomUserJoin(string messageParams) {
      MNTools.DLog("MNSession:MNUM_mnSessionRoomUserJoin: messageParams=<" + messageParams + ">");

      if (RoomUserJoinStorage != null) {
        List<object> paramsArray = (List<object>)MNUnityCommunicator.Serializer.Deserialize(messageParams,typeof(List<object>));

        MNTools.DetailedLogList("MNUM_mnSessionRoomUserJoin params",paramsArray);

        RoomUserJoinStorage(MNSerializerMapper.MNUserInfoFromDictionary((IDictionary)paramsArray[0]));
      }
    }

    private void MNUM_mnSessionRoomUserLeave(string messageParams) {
      MNTools.DLog("MNSession:MNUM_mnSessionRoomUserLeave: messageParams=<" + messageParams + ">");

      if (RoomUserLeaveStorage != null) {
        List<object> paramsArray = (List<object>)MNUnityCommunicator.Serializer.Deserialize(messageParams,typeof(List<object>));

        MNTools.DetailedLogList("MNUM_mnSessionRoomUserLeave params",paramsArray);

        RoomUserLeaveStorage(MNSerializerMapper.MNUserInfoFromDictionary((IDictionary)paramsArray[0]));
      }
    }

    private void MNUM_mnSessionGameMessageReceived(string messageParams) {
      MNTools.DLog("MNSession:MNUM_mnSessionGameMessageReceived: messageParams=<" + messageParams + ">");

      if (GameMessageReceivedStorage != null) {
        List<object> paramsArray = (List<object>)MNUnityCommunicator.Serializer.Deserialize(messageParams,typeof(List<object>));

        MNTools.DetailedLogList("MNUM_mnSessionGameMessageReceived params",paramsArray);

        GameMessageReceivedStorage((string)paramsArray[0], MNSerializerMapper.MNUserInfoFromDictionary((IDictionary)paramsArray[1]));
      }
    }

    private void MNUM_mnSessionErrorOccurred(string messageParams) {
      MNTools.DLog("MNSession:MNUM_mnSessionErrorOccurred: messageParams=<" + messageParams + ">");

      if (ErrorOccurredStorage != null) {
        List<object> paramsArray = (List<object>)MNUnityCommunicator.Serializer.Deserialize(messageParams,typeof(List<object>));

        MNTools.DetailedLogList("MNUM_mnSessionErrorOccurred params",paramsArray);

        ErrorOccurredStorage(MNSerializerMapper.MNErrorInfoFromDictionary((IDictionary)paramsArray[0]));
      }
    }

    private void MNUM_mnSessionExecAppCommandReceived(string messageParams) {
      MNTools.DLog("MNSession:MNUM_mnSessionExecAppCommandReceived: messageParams=<" + messageParams + ">");

      if (ExecAppCommandReceivedStorage != null) {
        List<object> paramsArray = (List<object>)MNUnityCommunicator.Serializer.Deserialize(messageParams,typeof(List<object>));

        MNTools.DetailedLogList("MNUM_mnSessionExecAppCommandReceived params",paramsArray);

        ExecAppCommandReceivedStorage((string)paramsArray[0], (string)paramsArray[1]);
      }
    }

    private void MNUM_mnSessionExecUICommandReceived(string messageParams) {
      MNTools.DLog("MNSession:MNUM_mnSessionExecUICommandReceived: messageParams=<" + messageParams + ">");

      if (ExecUICommandReceivedStorage != null) {
        List<object> paramsArray = (List<object>)MNUnityCommunicator.Serializer.Deserialize(messageParams,typeof(List<object>));

        MNTools.DetailedLogList("MNUM_mnSessionExecUICommandReceived params",paramsArray);

        ExecUICommandReceivedStorage((string)paramsArray[0], (string)paramsArray[1]);
      }
    }

    #endregion MNSessionEventHandler Messages

    #if UNITY_IPHONE

    [DllImport ("__Internal")]
    private static extern string _MNSession_GetMyUserInfo ();

    [DllImport ("__Internal")]
    private static extern void _MNSession_ReqJoinRandomRoom (string gameSetId);

    [DllImport ("__Internal")]
    private static extern string _MNSession_GetMyUserName ();

    [DllImport ("__Internal")]
    private static extern void _MNSession_ExecUICommand (string name, string param);

    [DllImport ("__Internal")]
    private static extern int _MNSession_GetStatus ();

    [DllImport ("__Internal")]
    private static extern void _MNSession_LeaveRoom ();

    [DllImport ("__Internal")]
    private static extern int _MNSession_GetRoomUserStatus ();

    [DllImport ("__Internal")]
    private static extern int _MNSession_GetCurrentRoomId ();

    [DllImport ("__Internal")]
    private static extern void _MNSession_ReqCreateBuddyRoom (string buddyRoomParams);

    [DllImport ("__Internal")]
    private static extern void _MNSession_ReqJoinBuddyRoom (int roomSFId);

    [DllImport ("__Internal")]
    private static extern void _MNSession_SendJoinRoomInvitationResponse (string invitationParams, bool accept);

    [DllImport ("__Internal")]
    private static extern bool _MNSession_LoginAuto ();

    [DllImport ("__Internal")]
    private static extern string _MNSession_GetRoomUserList ();

    [DllImport ("__Internal")]
    private static extern bool _MNSession_RegisterEventHandler ();

    [DllImport ("__Internal")]
    private static extern bool _MNSession_UnregisterEventHandler ();

    #elif UNITY_ANDROID

    private static AndroidJavaClass mnSessionUnityClass = null;

    private static AndroidJavaClass MNSessionUnityClass
    {
      get {
        MNTools.DLog("AndroidJavaClass MNSessionUnityClass");

        if (mnSessionUnityClass == null) {
          mnSessionUnityClass = new AndroidJavaClass("com.playphone.multinet.unity.MNSessionUnity");
        }

        return mnSessionUnityClass;
      }
    }

    #endif

    private MNSession()
    {
      MNTools.DLog("MNSession:MNSession()");
    }

    private bool eventHandlerRegistered = false;
  }
}
