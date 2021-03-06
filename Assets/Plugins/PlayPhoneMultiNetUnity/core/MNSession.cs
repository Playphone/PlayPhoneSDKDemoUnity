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

    public delegate void RoomUserStatusChangedEventHandler(int userStatus);
    public event RoomUserStatusChangedEventHandler RoomUserStatusChanged
    {
      [MethodImpl(MethodImplOptions.Synchronized)]
      add
      {
        MNTools.DLog("RoomUserStatusChangedEventHandler.add");
        if (RoomUserStatusChangedStorage == null) {
          RoomUserStatusChangedStorage += value;

          RegisterEventHandler();
        }
        else {
          RoomUserStatusChangedStorage += value;
        }
      }

      [MethodImpl(MethodImplOptions.Synchronized)]
      remove
      {
        MNTools.DLog("RoomUserStatusChangedEventHandler.remove");
        RoomUserStatusChangedStorage -= value;

        if (RoomUserStatusChangedStorage == null) {
          UnregisterEventHandler();
        }
      }
    }
    private RoomUserStatusChangedEventHandler RoomUserStatusChangedStorage;

    public delegate void JoinRoomInvitationReceivedEventHandler(MNJoinRoomInvitationParams _params);
    public event JoinRoomInvitationReceivedEventHandler JoinRoomInvitationReceived
    {
      [MethodImpl(MethodImplOptions.Synchronized)]
      add
      {
        MNTools.DLog("JoinRoomInvitationReceivedEventHandler.add");
        if (JoinRoomInvitationReceivedStorage == null) {
          JoinRoomInvitationReceivedStorage += value;

          RegisterEventHandler();
        }
        else {
          JoinRoomInvitationReceivedStorage += value;
        }
      }

      [MethodImpl(MethodImplOptions.Synchronized)]
      remove
      {
        MNTools.DLog("JoinRoomInvitationReceivedEventHandler.remove");
        JoinRoomInvitationReceivedStorage -= value;

        if (JoinRoomInvitationReceivedStorage == null) {
          UnregisterEventHandler();
        }
      }
    }
    private JoinRoomInvitationReceivedEventHandler JoinRoomInvitationReceivedStorage;

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

    public bool LoginAuto() {
      MNTools.DLog("MNSession:LoginAuto");

      if (Application.platform == RuntimePlatform.IPhonePlayer) {
        return _MNSession_LoginAuto();
      }
      else {
        return default(bool);
      }
    }

    public void Logout() {
      MNTools.DLog("MNSession:Logout");

      if (Application.platform == RuntimePlatform.IPhonePlayer) {
        _MNSession_Logout();
      }
      else {
      }
    }

    public int GetStatus() {
      MNTools.DLog("MNSession:GetStatus");

      if (Application.platform == RuntimePlatform.IPhonePlayer) {
        return _MNSession_GetStatus();
      }
      else {
        return default(int);
      }
    }

    public string GetMyUserName() {
      MNTools.DLog("MNSession:GetMyUserName");

      if (Application.platform == RuntimePlatform.IPhonePlayer) {
        return _MNSession_GetMyUserName();
      }
      else {
        return default(string);
      }
    }

    public MNUserInfo GetMyUserInfo() {
      MNTools.DLog("MNSession:GetMyUserInfo");

      if (Application.platform == RuntimePlatform.IPhonePlayer) {
        return (MNUserInfo)MNUnityCommunicator.Serializer.Deserialize(_MNSession_GetMyUserInfo(),typeof(MNUserInfo));
      }
      else {
        return default(MNUserInfo);
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
        return new MNUserInfo[0];
      }
    }

    public int GetRoomUserStatus() {
      MNTools.DLog("MNSession:GetRoomUserStatus");

      if (Application.platform == RuntimePlatform.IPhonePlayer) {
        return _MNSession_GetRoomUserStatus();
      }
      else {
        return default(int);
      }
    }

    public int GetCurrentRoomId() {
      MNTools.DLog("MNSession:GetCurrentRoomId");

      if (Application.platform == RuntimePlatform.IPhonePlayer) {
        return _MNSession_GetCurrentRoomId();
      }
      else {
        return default(int);
      }
    }

    public int GetRoomGameSetId() {
      MNTools.DLog("MNSession:GetRoomGameSetId");

      if (Application.platform == RuntimePlatform.IPhonePlayer) {
        return _MNSession_GetRoomGameSetId();
      }
      else {
        return default(int);
      }
    }

    public void ReqJoinBuddyRoom(int roomSFId) {
      MNTools.DLog("MNSession:ReqJoinBuddyRoom");

      if (Application.platform == RuntimePlatform.IPhonePlayer) {
        _MNSession_ReqJoinBuddyRoom(roomSFId);
      }
      else {
      }
    }

    public void SendJoinRoomInvitationResponse(MNJoinRoomInvitationParams invitationParams, bool accept) {
      MNTools.DLog("MNSession:SendJoinRoomInvitationResponse");

      if (Application.platform == RuntimePlatform.IPhonePlayer) {
        _MNSession_SendJoinRoomInvitationResponse(MNUnityCommunicator.Serializer.Serialize(invitationParams), accept);
      }
      else {
      }
    }

    public void ReqJoinRandomRoom(string gameSetId) {
      MNTools.DLog("MNSession:ReqJoinRandomRoom");

      if (Application.platform == RuntimePlatform.IPhonePlayer) {
        _MNSession_ReqJoinRandomRoom(gameSetId);
      }
      else {
      }
    }

    public void ReqCreateBuddyRoom(MNBuddyRoomParams buddyRoomParams) {
      MNTools.DLog("MNSession:ReqCreateBuddyRoom");

      if (Application.platform == RuntimePlatform.IPhonePlayer) {
        _MNSession_ReqCreateBuddyRoom(MNUnityCommunicator.Serializer.Serialize(buddyRoomParams));
      }
      else {
      }
    }

    public void ReqSetUserStatus(int userStatus) {
      MNTools.DLog("MNSession:ReqSetUserStatus");

      if (Application.platform == RuntimePlatform.IPhonePlayer) {
        _MNSession_ReqSetUserStatus(userStatus);
      }
      else {
      }
    }

    public void LeaveRoom() {
      MNTools.DLog("MNSession:LeaveRoom");

      if (Application.platform == RuntimePlatform.IPhonePlayer) {
        _MNSession_LeaveRoom();
      }
      else {
      }
    }

    public void ExecUICommand(string name, string param) {
      MNTools.DLog("MNSession:ExecUICommand");

      if (Application.platform == RuntimePlatform.IPhonePlayer) {
        _MNSession_ExecUICommand(name, param);
      }
      else {
      }
    }

    public bool IsInGameRoom() {
      MNTools.DLog("MNSession:IsInGameRoom");

      if (Application.platform == RuntimePlatform.IPhonePlayer) {
        return _MNSession_IsInGameRoom();
      }
      else {
        return default(bool);
      }
    }

    [MethodImpl(MethodImplOptions.Synchronized)]
    private void RegisterEventHandler() {
      if (eventHandlerRegistered) {
        return;
      }

      MNTools.DLog("MNSession:RegisterEventHandler");

      if (Application.platform == RuntimePlatform.IPhonePlayer) {
        eventHandlerRegistered = _MNSession_RegisterEventHandler();
      }
      else {
      }
    }

    [MethodImpl(MethodImplOptions.Synchronized)]
    private void UnregisterEventHandler() {
      if ((SessionStatusChangedStorage != null) || (UserChangedStorage != null) || (RoomUserStatusChangedStorage != null) || (JoinRoomInvitationReceivedStorage != null) || (GameMessageReceivedStorage != null) || (RoomUserJoinStorage != null) || (RoomUserLeaveStorage != null) || (ErrorOccurredStorage != null) || (ExecAppCommandReceivedStorage != null) || (ExecUICommandReceivedStorage != null)) {
        return;
      }

      MNTools.DLog("MNSession:UnregisterEventHandler");

      if (!eventHandlerRegistered) {
        MNTools.DLog("MNSession:EventHandler not registered");
        return;
      }

      if (Application.platform == RuntimePlatform.IPhonePlayer) {
        eventHandlerRegistered = !_MNSession_UnregisterEventHandler();
      }
      else {
      }
    }

    #elif UNITY_ANDROID

    public bool LoginAuto() {
      MNTools.DLog("MNSession:LoginAuto");

      if (Application.platform == RuntimePlatform.Android) {
        return MNSessionUnityClass.CallStatic<bool>("loginAuto");
      }
      else {
        return default(bool);
      }
    }

    public void Logout() {
      MNTools.DLog("MNSession:Logout");

      if (Application.platform == RuntimePlatform.Android) {
        MNSessionUnityClass.CallStatic("logout");
      }
      else {
      }
    }

    public int GetStatus() {
      MNTools.DLog("MNSession:GetStatus");

      if (Application.platform == RuntimePlatform.Android) {
        return MNSessionUnityClass.CallStatic<int>("getStatus");
      }
      else {
        return default(int);
      }
    }

    public string GetMyUserName() {
      MNTools.DLog("MNSession:GetMyUserName");

      if (Application.platform == RuntimePlatform.Android) {
        return MNSessionUnityClass.CallStatic<string>("getMyUserName");
      }
      else {
        return default(string);
      }
    }

    public MNUserInfo GetMyUserInfo() {
      MNTools.DLog("MNSession:GetMyUserInfo");

      if (Application.platform == RuntimePlatform.Android) {
        return (MNUserInfo)MNUnityCommunicator.Serializer.Deserialize(MNSessionUnityClass.CallStatic<string>("getMyUserInfo"),typeof(MNUserInfo));
      }
      else {
        return default(MNUserInfo);
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
        return new MNUserInfo[0];
      }
    }

    public int GetRoomUserStatus() {
      MNTools.DLog("MNSession:GetRoomUserStatus");

      if (Application.platform == RuntimePlatform.Android) {
        return MNSessionUnityClass.CallStatic<int>("getRoomUserStatus");
      }
      else {
        return default(int);
      }
    }

    public int GetCurrentRoomId() {
      MNTools.DLog("MNSession:GetCurrentRoomId");

      if (Application.platform == RuntimePlatform.Android) {
        return MNSessionUnityClass.CallStatic<int>("getCurrentRoomId");
      }
      else {
        return default(int);
      }
    }

    public int GetRoomGameSetId() {
      MNTools.DLog("MNSession:GetRoomGameSetId");

      if (Application.platform == RuntimePlatform.Android) {
        return MNSessionUnityClass.CallStatic<int>("getRoomGameSetId");
      }
      else {
        return default(int);
      }
    }

    public void ReqJoinBuddyRoom(int roomSFId) {
      MNTools.DLog("MNSession:ReqJoinBuddyRoom");

      if (Application.platform == RuntimePlatform.Android) {
        MNSessionUnityClass.CallStatic("reqJoinBuddyRoom",roomSFId);
      }
      else {
      }
    }

    public void SendJoinRoomInvitationResponse(MNJoinRoomInvitationParams invitationParams, bool accept) {
      MNTools.DLog("MNSession:SendJoinRoomInvitationResponse");

      if (Application.platform == RuntimePlatform.Android) {
        MNSessionUnityClass.CallStatic("sendJoinRoomInvitationResponse",MNUnityCommunicator.Serializer.Serialize(invitationParams), accept);
      }
      else {
      }
    }

    public void ReqJoinRandomRoom(string gameSetId) {
      MNTools.DLog("MNSession:ReqJoinRandomRoom");

      if (Application.platform == RuntimePlatform.Android) {
        MNSessionUnityClass.CallStatic("reqJoinRandomRoom",gameSetId);
      }
      else {
      }
    }

    public void ReqCreateBuddyRoom(MNBuddyRoomParams buddyRoomParams) {
      MNTools.DLog("MNSession:ReqCreateBuddyRoom");

      if (Application.platform == RuntimePlatform.Android) {
        MNSessionUnityClass.CallStatic("reqCreateBuddyRoom",MNUnityCommunicator.Serializer.Serialize(buddyRoomParams));
      }
      else {
      }
    }

    public void ReqSetUserStatus(int userStatus) {
      MNTools.DLog("MNSession:ReqSetUserStatus");

      if (Application.platform == RuntimePlatform.Android) {
        MNSessionUnityClass.CallStatic("reqSetUserStatus",userStatus);
      }
      else {
      }
    }

    public void LeaveRoom() {
      MNTools.DLog("MNSession:LeaveRoom");

      if (Application.platform == RuntimePlatform.Android) {
        MNSessionUnityClass.CallStatic("leaveRoom");
      }
      else {
      }
    }

    public void ExecUICommand(string name, string param) {
      MNTools.DLog("MNSession:ExecUICommand");

      if (Application.platform == RuntimePlatform.Android) {
        MNSessionUnityClass.CallStatic("execUICommand",name, param);
      }
      else {
      }
    }

    public bool IsInGameRoom() {
      MNTools.DLog("MNSession:IsInGameRoom");

      if (Application.platform == RuntimePlatform.Android) {
        return MNSessionUnityClass.CallStatic<bool>("isInGameRoom");
      }
      else {
        return default(bool);
      }
    }

    [MethodImpl(MethodImplOptions.Synchronized)]
    private void RegisterEventHandler() {
      if (eventHandlerRegistered) {
        return;
      }

      MNTools.DLog("MNSession:RegisterEventHandler");

      if (Application.platform == RuntimePlatform.Android) {
        eventHandlerRegistered = MNSessionUnityClass.CallStatic<bool>("registerEventHandler");
      }
      else {
      }
    }

    [MethodImpl(MethodImplOptions.Synchronized)]
    private void UnregisterEventHandler() {
      if ((SessionStatusChangedStorage != null) || (UserChangedStorage != null) || (RoomUserStatusChangedStorage != null) || (JoinRoomInvitationReceivedStorage != null) || (GameMessageReceivedStorage != null) || (RoomUserJoinStorage != null) || (RoomUserLeaveStorage != null) || (ErrorOccurredStorage != null) || (ExecAppCommandReceivedStorage != null) || (ExecUICommandReceivedStorage != null)) {
        return;
      }

      MNTools.DLog("MNSession:UnregisterEventHandler");

      if (!eventHandlerRegistered) {
        MNTools.DLog("MNSession:EventHandler not registered");
        return;
      }

      if (Application.platform == RuntimePlatform.Android) {
        eventHandlerRegistered = !MNSessionUnityClass.CallStatic<bool>("unregisterEventHandler");
      }
      else {
      }
    }

    #else
    // Empty implementation for unsupported platforms (such as Unity Editor)
    // Method's arguments are ignored.
    // Non-void methods return default values. If return value is an array empty array is returned.

    public bool LoginAuto() {
      return default(bool);
    }

    public void Logout() {
    }

    public int GetStatus() {
      return default(int);
    }

    public string GetMyUserName() {
      return default(string);
    }

    public MNUserInfo GetMyUserInfo() {
      return default(MNUserInfo);
    }

    public MNUserInfo[] GetRoomUserList() {
      return new MNUserInfo[0];
    }

    public int GetRoomUserStatus() {
      return default(int);
    }

    public int GetCurrentRoomId() {
      return default(int);
    }

    public int GetRoomGameSetId() {
      return default(int);
    }

    public void ReqJoinBuddyRoom(int roomSFId) {
    }

    public void SendJoinRoomInvitationResponse(MNJoinRoomInvitationParams invitationParams, bool accept) {
    }

    public void ReqJoinRandomRoom(string gameSetId) {
    }

    public void ReqCreateBuddyRoom(MNBuddyRoomParams buddyRoomParams) {
    }

    public void ReqSetUserStatus(int userStatus) {
    }

    public void LeaveRoom() {
    }

    public void ExecUICommand(string name, string param) {
    }

    public bool IsInGameRoom() {
      return default(bool);
    }

    private void RegisterEventHandler() {
    }

    private void UnregisterEventHandler() {
    }

    #endif

    #region MNSessionEventHandler Messages
    private void MNUM_mnSessionStatusChanged(string messageParams) {
      MNTools.DLog("MNSession:MNUM_mnSessionStatusChanged: messageParams=<" + messageParams + ">");

      if (SessionStatusChangedStorage != null) {
        List<object> paramsArray = (List<object>)MNUnityCommunicator.Serializer.Deserialize(messageParams,typeof(List<object>));

        MNTools.DLogList("MNUM_mnSessionStatusChanged params",paramsArray,MNTools.DEBUG_LEVEL_DETAILED);

        SessionStatusChangedStorage(Convert.ToInt32(paramsArray[0]), Convert.ToInt32(paramsArray[1]));
      }
    }

    private void MNUM_mnSessionUserChanged(string messageParams) {
      MNTools.DLog("MNSession:MNUM_mnSessionUserChanged: messageParams=<" + messageParams + ">");

      if (UserChangedStorage != null) {
        List<object> paramsArray = (List<object>)MNUnityCommunicator.Serializer.Deserialize(messageParams,typeof(List<object>));

        MNTools.DLogList("MNUM_mnSessionUserChanged params",paramsArray,MNTools.DEBUG_LEVEL_DETAILED);

        UserChangedStorage(Convert.ToInt64(paramsArray[0]));
      }
    }

    private void MNUM_mnSessionRoomUserStatusChanged(string messageParams) {
      MNTools.DLog("MNSession:MNUM_mnSessionRoomUserStatusChanged: messageParams=<" + messageParams + ">");

      if (RoomUserStatusChangedStorage != null) {
        List<object> paramsArray = (List<object>)MNUnityCommunicator.Serializer.Deserialize(messageParams,typeof(List<object>));

        MNTools.DLogList("MNUM_mnSessionRoomUserStatusChanged params",paramsArray,MNTools.DEBUG_LEVEL_DETAILED);

        RoomUserStatusChangedStorage(Convert.ToInt32(paramsArray[0]));
      }
    }

    private void MNUM_mnSessionJoinRoomInvitationReceived(string messageParams) {
      MNTools.DLog("MNSession:MNUM_mnSessionJoinRoomInvitationReceived: messageParams=<" + messageParams + ">");

      if (JoinRoomInvitationReceivedStorage != null) {
        List<object> paramsArray = (List<object>)MNUnityCommunicator.Serializer.Deserialize(messageParams,typeof(List<object>));

        MNTools.DLogList("MNUM_mnSessionJoinRoomInvitationReceived params",paramsArray,MNTools.DEBUG_LEVEL_DETAILED);

        JoinRoomInvitationReceivedStorage(MNSerializerMapper.MNJoinRoomInvitationParamsFromDictionary((IDictionary)paramsArray[0]));
      }
    }

    private void MNUM_mnSessionGameMessageReceived(string messageParams) {
      MNTools.DLog("MNSession:MNUM_mnSessionGameMessageReceived: messageParams=<" + messageParams + ">");

      if (GameMessageReceivedStorage != null) {
        List<object> paramsArray = (List<object>)MNUnityCommunicator.Serializer.Deserialize(messageParams,typeof(List<object>));

        MNTools.DLogList("MNUM_mnSessionGameMessageReceived params",paramsArray,MNTools.DEBUG_LEVEL_DETAILED);

        GameMessageReceivedStorage((string)paramsArray[0], MNSerializerMapper.MNUserInfoFromDictionary((IDictionary)paramsArray[1]));
      }
    }

    private void MNUM_mnSessionRoomUserJoin(string messageParams) {
      MNTools.DLog("MNSession:MNUM_mnSessionRoomUserJoin: messageParams=<" + messageParams + ">");

      if (RoomUserJoinStorage != null) {
        List<object> paramsArray = (List<object>)MNUnityCommunicator.Serializer.Deserialize(messageParams,typeof(List<object>));

        MNTools.DLogList("MNUM_mnSessionRoomUserJoin params",paramsArray,MNTools.DEBUG_LEVEL_DETAILED);

        RoomUserJoinStorage(MNSerializerMapper.MNUserInfoFromDictionary((IDictionary)paramsArray[0]));
      }
    }

    private void MNUM_mnSessionRoomUserLeave(string messageParams) {
      MNTools.DLog("MNSession:MNUM_mnSessionRoomUserLeave: messageParams=<" + messageParams + ">");

      if (RoomUserLeaveStorage != null) {
        List<object> paramsArray = (List<object>)MNUnityCommunicator.Serializer.Deserialize(messageParams,typeof(List<object>));

        MNTools.DLogList("MNUM_mnSessionRoomUserLeave params",paramsArray,MNTools.DEBUG_LEVEL_DETAILED);

        RoomUserLeaveStorage(MNSerializerMapper.MNUserInfoFromDictionary((IDictionary)paramsArray[0]));
      }
    }

    private void MNUM_mnSessionErrorOccurred(string messageParams) {
      MNTools.DLog("MNSession:MNUM_mnSessionErrorOccurred: messageParams=<" + messageParams + ">");

      if (ErrorOccurredStorage != null) {
        List<object> paramsArray = (List<object>)MNUnityCommunicator.Serializer.Deserialize(messageParams,typeof(List<object>));

        MNTools.DLogList("MNUM_mnSessionErrorOccurred params",paramsArray,MNTools.DEBUG_LEVEL_DETAILED);

        ErrorOccurredStorage(MNSerializerMapper.MNErrorInfoFromDictionary((IDictionary)paramsArray[0]));
      }
    }

    private void MNUM_mnSessionExecAppCommandReceived(string messageParams) {
      MNTools.DLog("MNSession:MNUM_mnSessionExecAppCommandReceived: messageParams=<" + messageParams + ">");

      if (ExecAppCommandReceivedStorage != null) {
        List<object> paramsArray = (List<object>)MNUnityCommunicator.Serializer.Deserialize(messageParams,typeof(List<object>));

        MNTools.DLogList("MNUM_mnSessionExecAppCommandReceived params",paramsArray,MNTools.DEBUG_LEVEL_DETAILED);

        ExecAppCommandReceivedStorage((string)paramsArray[0], (string)paramsArray[1]);
      }
    }

    private void MNUM_mnSessionExecUICommandReceived(string messageParams) {
      MNTools.DLog("MNSession:MNUM_mnSessionExecUICommandReceived: messageParams=<" + messageParams + ">");

      if (ExecUICommandReceivedStorage != null) {
        List<object> paramsArray = (List<object>)MNUnityCommunicator.Serializer.Deserialize(messageParams,typeof(List<object>));

        MNTools.DLogList("MNUM_mnSessionExecUICommandReceived params",paramsArray,MNTools.DEBUG_LEVEL_DETAILED);

        ExecUICommandReceivedStorage((string)paramsArray[0], (string)paramsArray[1]);
      }
    }

    #endregion MNSessionEventHandler Messages

    #if UNITY_IPHONE

    [DllImport ("__Internal")]
    private static extern bool _MNSession_LoginAuto ();

    [DllImport ("__Internal")]
    private static extern void _MNSession_Logout ();

    [DllImport ("__Internal")]
    private static extern int _MNSession_GetStatus ();

    [DllImport ("__Internal")]
    private static extern string _MNSession_GetMyUserName ();

    [DllImport ("__Internal")]
    private static extern string _MNSession_GetMyUserInfo ();

    [DllImport ("__Internal")]
    private static extern string _MNSession_GetRoomUserList ();

    [DllImport ("__Internal")]
    private static extern int _MNSession_GetRoomUserStatus ();

    [DllImport ("__Internal")]
    private static extern int _MNSession_GetCurrentRoomId ();

    [DllImport ("__Internal")]
    private static extern int _MNSession_GetRoomGameSetId ();

    [DllImport ("__Internal")]
    private static extern void _MNSession_ReqJoinBuddyRoom (int roomSFId);

    [DllImport ("__Internal")]
    private static extern void _MNSession_SendJoinRoomInvitationResponse (string invitationParams, bool accept);

    [DllImport ("__Internal")]
    private static extern void _MNSession_ReqJoinRandomRoom (string gameSetId);

    [DllImport ("__Internal")]
    private static extern void _MNSession_ReqCreateBuddyRoom (string buddyRoomParams);

    [DllImport ("__Internal")]
    private static extern void _MNSession_ReqSetUserStatus (int userStatus);

    [DllImport ("__Internal")]
    private static extern void _MNSession_LeaveRoom ();

    [DllImport ("__Internal")]
    private static extern void _MNSession_ExecUICommand (string name, string param);

    [DllImport ("__Internal")]
    private static extern bool _MNSession_IsInGameRoom ();

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

    #if UNITY_IPHONE || UNITY_ANDROID
    private bool eventHandlerRegistered = false;
    #endif
  }
}
