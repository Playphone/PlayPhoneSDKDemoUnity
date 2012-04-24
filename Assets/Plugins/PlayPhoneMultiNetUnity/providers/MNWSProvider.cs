using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using PlayPhone.MultiNet.Core;
using PlayPhone.MultiNet.Providers;

namespace PlayPhone.MultiNet.Providers
{
  public class MNWSProvider : MonoBehaviour
  {

    public MNWSLoader Send(MNWSInfoRequest request) {
      MNTools.DLog("MNWSProvider:Send");

      return Send(new MNWSInfoRequest[] {request});
    }

    #if UNITY_IPHONE

    public void Shutdown() {
      MNTools.DLog("MNWSProvider:Shutdown");

      if (Application.platform == RuntimePlatform.IPhonePlayer) {
        // Not used in iOS
        //_MNWSProvider_Shutdown();
      }
      else {
        throw new MNNotOnDeviceExcepton();
      }
    }

    public MNWSLoader Send(MNWSInfoRequest[] requests) {
      MNTools.DLog("MNWSProvider:Send");

      lock(delegateDict) {
        foreach (MNWSInfoRequest request in requests) {
          delegateDict[request.Id] = request;
        }
      }

      if (Application.platform == RuntimePlatform.IPhonePlayer) {
        return new MNWSLoader(_MNWSProvider_Send(MNUnityCommunicator.Serializer.Serialize(requests)));
      }
      else {
        throw new MNNotOnDeviceExcepton();
      }
    }

    internal void CancelRequest(long loaderId) {
      if (Application.platform == RuntimePlatform.IPhonePlayer) {
        _MNWSProvider_CancelRequest(loaderId);
      }
      else {
        throw new MNNotOnDeviceExcepton();
      }
    }

    #elif UNITY_ANDROID

    public void Shutdown() {
      MNTools.DLog("MNWSProvider:Shutdown");

      if (Application.platform == RuntimePlatform.Android) {
        MNWSProviderUnityClass.CallStatic("shutdown");
      }
      else {
        throw new MNNotOnDeviceExcepton();
      }
    }

    public MNWSLoader Send(MNWSInfoRequest[] requests) {
      MNTools.DLog("MNWSProvider:Send");

      lock(delegateDict) {
        foreach (MNWSInfoRequest request in requests) {
          delegateDict[request.Id] = request;
        }
      }

      if (Application.platform == RuntimePlatform.Android) {
        return new MNWSLoader(MNWSProviderUnityClass.CallStatic<long>("send",MNUnityCommunicator.Serializer.Serialize(requests)));
      }
      else {
        throw new MNNotOnDeviceExcepton();
      }
    }

    internal void CancelRequest(long loaderId) {
      if (Application.platform == RuntimePlatform.Android) {
        MNWSProviderUnityClass.CallStatic("cancelRequest",loaderId);
      }
      else {
        throw new MNNotOnDeviceExcepton();
      }
    }

    #endif

    private void MNUM_MNWSInfoRequestEventHandler (string messageParams) {
      MNTools.DLog("MNWSProvider:MNUM_MNWSInfoRequestEventHandler(" + messageParams + ")");

      List<object> srcData = (List<object>)MNUnityCommunicator.Serializer.Deserialize(messageParams,typeof(List<object>));

      MNTools.DLogList("MNUM_mnDirectSessionStatusChanged params",srcData,MNTools.DEBUG_LEVEL_DETAILED);

      long requestId = Convert.ToInt64(srcData[0]);
      MNWSInfoRequest request = null;

      lock (delegateDict) {
        request = (MNWSInfoRequest)delegateDict[requestId];
        delegateDict.Remove(requestId);
      }

      if (request == null) {
        return;
      }

      if (request is MNWSInfoRequestAnyGame) {
        MNWSInfoRequestAnyGame.RequestResult requestResult = MNSerializerMapper.MNWSInfoRequestAnyGameRequestResultFromDictionary((IDictionary)srcData[1]);
        (request as MNWSInfoRequestAnyGame).OnCompletedDelegate(requestResult);
      }
      else if (request is MNWSInfoRequestAnyUser) {
        MNWSInfoRequestAnyUser.RequestResult requestResult = MNSerializerMapper.MNWSInfoRequestAnyUserRequestResultFromDictionary((IDictionary)srcData[1]);
        (request as MNWSInfoRequestAnyUser).OnCompletedDelegate(requestResult);
      }
      else if (request is MNWSInfoRequestAnyUserGameCookies) {
        MNWSInfoRequestAnyUserGameCookies.RequestResult requestResult = MNSerializerMapper.MNWSInfoRequestAnyUserGameCookiesRequestResultFromDictionary((IDictionary)srcData[1]);
        (request as MNWSInfoRequestAnyUserGameCookies).OnCompletedDelegate(requestResult);
      }
      else if (request is MNWSInfoRequestCurrentUserInfo) {
        MNWSInfoRequestCurrentUserInfo.RequestResult requestResult = MNSerializerMapper.MNWSInfoRequestCurrentUserInfoRequestResultFromDictionary((IDictionary)srcData[1]);
        (request as MNWSInfoRequestCurrentUserInfo).OnCompletedDelegate(requestResult);
      }
      else if (request is MNWSInfoRequestCurrGameRoomList) {
        MNWSInfoRequestCurrGameRoomList.RequestResult requestResult = MNSerializerMapper.MNWSInfoRequestCurrGameRoomListRequestResultFromDictionary((IDictionary)srcData[1]);
        (request as MNWSInfoRequestCurrGameRoomList).OnCompletedDelegate(requestResult);
      }
      else if (request is MNWSInfoRequestCurrGameRoomUserList) {
        MNWSInfoRequestCurrGameRoomUserList.RequestResult requestResult = MNSerializerMapper.MNWSInfoRequestCurrGameRoomUserListRequestResultFromDictionary((IDictionary)srcData[1]);
        (request as MNWSInfoRequestCurrGameRoomUserList).OnCompletedDelegate(requestResult);
      }
      else if (request is MNWSInfoRequestCurrUserBuddyList) {
        MNWSInfoRequestCurrUserBuddyList.RequestResult requestResult = MNSerializerMapper.MNWSInfoRequestCurrUserBuddyListRequestResultFromDictionary((IDictionary)srcData[1]);
        (request as MNWSInfoRequestCurrUserBuddyList).OnCompletedDelegate(requestResult);
      }
      else if (request is MNWSInfoRequestCurrUserSubscriptionStatus) {
        MNWSInfoRequestCurrUserSubscriptionStatus.RequestResult requestResult = MNSerializerMapper.MNWSInfoRequestCurrUserSubscriptionStatusRequestResultFromDictionary((IDictionary)srcData[1]);
        (request as MNWSInfoRequestCurrUserSubscriptionStatus).OnCompletedDelegate(requestResult);
      }
      else if (request is MNWSInfoRequestLeaderboard) {
        MNWSInfoRequestLeaderboard.RequestResult requestResult = MNSerializerMapper.MNWSInfoRequestLeaderboardRequestResultFromDictionary((IDictionary)srcData[1]);
        (request as MNWSInfoRequestLeaderboard).OnCompletedDelegate(requestResult);
      }
      else if (request is MNWSInfoRequestSessionSignedClientToken) {
        MNWSInfoRequestSessionSignedClientToken.RequestResult requestResult = MNSerializerMapper.MNWSInfoRequestSessionSignedClientTokenRequestResultFromDictionary((IDictionary)srcData[1]);
        (request as MNWSInfoRequestSessionSignedClientToken).OnCompletedDelegate(requestResult);
      }
      else if (request is MNWSInfoRequestSystemGameNetStats) {
        MNWSInfoRequestSystemGameNetStats.RequestResult requestResult = MNSerializerMapper.MNWSInfoRequestSystemGameNetStatsRequestResultFromDictionary((IDictionary)srcData[1]);
        (request as MNWSInfoRequestSystemGameNetStats).OnCompletedDelegate(requestResult);
      }
      else {
        // skip invalid request
      }
    }

    #if UNITY_IPHONE
    /*
    // Not used in iOS
    [DllImport ("__Internal")]
    private static extern void _MNWSProvider_Shutdown ();
    */

    [DllImport ("__Internal")]
    private static extern long _MNWSProvider_Send (string request);

    [DllImport ("__Internal")]
    private static extern void _MNWSProvider_CancelRequest (long loaderId);
    #elif UNITY_ANDROID

    private static AndroidJavaClass mnWSProviderUnityClass = null;

    private static AndroidJavaClass MNWSProviderUnityClass
    {
      get {
        MNTools.DLog("AndroidJavaClass MNWSProviderUnityClass");

        if (mnWSProviderUnityClass == null) {
          mnWSProviderUnityClass = new AndroidJavaClass("com.playphone.multinet.unity.MNWSProviderUnity");
          MNUnityCommunicator.registerComponent(typeof(MNVShopProvider));
        }

        return mnWSProviderUnityClass;
      }
    }

    #endif
    private MNWSProvider()
    {
      MNTools.DLog("MNWSProvider:MNWSProvider()");
    }

    private static long requestId = (long)(DateTime.UtcNow - new DateTime(1970,1,1,0,0,0)).TotalSeconds;

    private Hashtable delegateDict = new Hashtable();

    [MethodImpl(MethodImplOptions.Synchronized)]
    internal static long GetRequestId() {
      requestId++;
      return requestId;
    }
  }

  #region MNWSInfoRequestAnyGame

  public class MNWSInfoRequestAnyGame : MNWSInfoRequest {

    public delegate void OnCompleted(RequestResult result);

    internal OnCompleted OnCompletedDelegate;

    public MNWSInfoRequestAnyGame (int gameId,OnCompleted onCompleted) : base () {
      this.Parameters["gameId"] = gameId;
      this.OnCompletedDelegate = onCompleted;
    }

    public new class RequestResult : MNWSInfoRequest.RequestResult {
      public MNWSAnyGameItem DataEntry {get;set;}

      public MNWSAnyGameItem GetDataEntry () {
        return DataEntry;
      }

      public override string ToString() {
        if (DataEntry == null) {
          return base.ToString() + " DataEntry: null";
        }

        return base.ToString() + " DataEntry: " + DataEntry.ToString();
      }
    }
  }

  public class MNWSAnyGameItem : MNWSGenericItem {
    public int? GetGameId () {
      return GameId;
    }
    public string GetGameName () {
      return GameName;
    }
    public string GetGameDesc () {
      return GameDesc;
    }
    public int? GetGameGenreId () {
      return GameGenreId;
    }
    public long? GetGameFlags () {
      return GameFlags;
    }
    public int? GetGameStatus () {
      return GameStatus;
    }
    public int? GetGamePlayModel () {
      return GamePlayModel;
    }
    public string GetGameIconUrl () {
      return GameIconUrl;
    }
    public long? GetDeveloperId () {
      return DeveloperId;
    }

    public int? GameId {get;set;}
    public string GameName {get;set;}
    public string GameDesc {get;set;}
    public int? GameGenreId {get;set;}
    public long? GameFlags {get;set;}
    public int? GameStatus {get;set;}
    public int? GamePlayModel {get;set;}
    public string GameIconUrl {get;set;}
    public long? DeveloperId {get;set;}

    public override string ToString() {
      return "{" + "GameId = " + ((GameId == null)?"null":GameId.ToString()) + ";" +
                   "GameName = " + ((GameName == null)?"null":((GameName.Length == 0) ? "\"\"" : "\"" + GameName + "\"")) + ";" +
                   "GameDesc = " + ((GameDesc == null)?"null":((GameDesc.Length == 0) ? "\"\"" : "\"" + GameDesc + "\"")) + ";" +
                   "GameGenreId = " + ((GameGenreId == null)?"null":GameGenreId.ToString()) + ";" +
                   "GameFlags = " + ((GameFlags == null)?"null":GameFlags.ToString()) + ";" +
                   "GameStatus = " + ((GameStatus == null)?"null":GameStatus.ToString()) + ";" +
                   "GamePlayModel = " + ((GamePlayModel == null)?"null":GamePlayModel.ToString()) + ";" +
                   "GameIconUrl = " + ((GameIconUrl == null)?"null":((GameIconUrl.Length == 0) ? "\"\"" : "\"" + GameIconUrl + "\"")) + ";" +
                   "DeveloperId = " + ((DeveloperId == null)?"null":DeveloperId.ToString()) + "}";
    }
  }

  #endregion MNWSInfoRequestAnyGame
  #region MNWSInfoRequestAnyUser

  public class MNWSInfoRequestAnyUser : MNWSInfoRequest {

    public delegate void OnCompleted(RequestResult result);

    internal OnCompleted OnCompletedDelegate;

    public MNWSInfoRequestAnyUser (long userId,OnCompleted onCompleted) : base () {
      this.Parameters["userId"] = userId;
      this.OnCompletedDelegate = onCompleted;
    }

    public new class RequestResult : MNWSInfoRequest.RequestResult {
      public MNWSAnyUserItem DataEntry {get;set;}

      public MNWSAnyUserItem GetDataEntry () {
        return DataEntry;
      }

      public override string ToString() {
        if (DataEntry == null) {
          return base.ToString() + " DataEntry: null";
        }

        return base.ToString() + " DataEntry: " + DataEntry.ToString();
      }
    }
  }

  public class MNWSAnyUserItem : MNWSGenericItem {
    public long? GetUserId () {
      return UserId;
    }
    public string GetUserNickName () {
      return UserNickName;
    }
    public bool? GetUserAvatarExists () {
      return UserAvatarExists;
    }
    public string GetUserAvatarUrl () {
      return UserAvatarUrl;
    }
    public bool? GetUserOnlineNow () {
      return UserOnlineNow;
    }
    public long? GetUserGamePoints () {
      return UserGamePoints;
    }
    public int? GetMyFriendLinkStatus () {
      return MyFriendLinkStatus;
    }

    public long? UserId {get;set;}
    public string UserNickName {get;set;}
    public bool? UserAvatarExists {get;set;}
    public string UserAvatarUrl {get;set;}
    public bool? UserOnlineNow {get;set;}
    public long? UserGamePoints {get;set;}
    public int? MyFriendLinkStatus {get;set;}

    public override string ToString() {
      return "{" + "UserId = " + ((UserId == null)?"null":UserId.ToString()) + ";" +
                   "UserNickName = " + ((UserNickName == null)?"null":((UserNickName.Length == 0) ? "\"\"" : "\"" + UserNickName + "\"")) + ";" +
                   "UserAvatarExists = " + ((UserAvatarExists == null)?"null":UserAvatarExists.ToString()) + ";" +
                   "UserAvatarUrl = " + ((UserAvatarUrl == null)?"null":((UserAvatarUrl.Length == 0) ? "\"\"" : "\"" + UserAvatarUrl + "\"")) + ";" +
                   "UserOnlineNow = " + ((UserOnlineNow == null)?"null":UserOnlineNow.ToString()) + ";" +
                   "UserGamePoints = " + ((UserGamePoints == null)?"null":UserGamePoints.ToString()) + ";" +
                   "MyFriendLinkStatus = " + ((MyFriendLinkStatus == null)?"null":MyFriendLinkStatus.ToString()) + "}";
    }
  }

  #endregion MNWSInfoRequestAnyUser
  #region MNWSInfoRequestAnyUserGameCookies

  public class MNWSInfoRequestAnyUserGameCookies : MNWSInfoRequest {

    public delegate void OnCompleted(RequestResult result);

    internal OnCompleted OnCompletedDelegate;

    public MNWSInfoRequestAnyUserGameCookies (long[] userIdList,int[] cookieKeyList,OnCompleted onCompleted) : base () {
      this.Parameters["userIdList"   ] = userIdList;
      this.Parameters["cookieKeyList"] = cookieKeyList;
      this.OnCompletedDelegate = onCompleted;
    }

    public new class RequestResult : MNWSInfoRequest.RequestResult {
      public MNWSUserGameCookie[] DataEntry {get;set;}

      public MNWSUserGameCookie[] GetDataEntry () {
        return DataEntry;
      }

      public override string ToString() {
        if (DataEntry == null) {
          return base.ToString() + " DataEntry: null";
        }

        string itemsString = "";

        foreach (MNWSUserGameCookie item in DataEntry) {
          if (itemsString.Length != 0) {
            itemsString += ",";
          }

          itemsString += item.ToString();
        }

        return base.ToString() + " DataEntry: [" + itemsString + "]";
      }
    }
  }

  public class MNWSUserGameCookie : MNWSGenericItem {
    public long? GetUserId () {
      return UserId;
    }
    public int? GetCookieKey () {
      return CookieKey;
    }
    public string GetCookieValue () {
      return CookieValue;
    }

    public long? UserId {get;set;}
    public int? CookieKey {get;set;}
    public string CookieValue {get;set;}

    public override string ToString() {
      return "{" + "UserId = " + ((UserId == null)?"null":UserId.ToString()) + ";" +
                   "CookieKey = " + ((CookieKey == null)?"null":CookieKey.ToString()) + ";" +
                   "CookieValue = " + ((CookieValue == null)?"null":((CookieValue.Length == 0) ? "\"\"" : "\"" + CookieValue + "\"")) + "}";
    }
  }

  #endregion MNWSInfoRequestAnyUserGameCookies
  #region MNWSInfoRequestCurrentUserInfo

  public class MNWSInfoRequestCurrentUserInfo : MNWSInfoRequest {

    public delegate void OnCompleted(RequestResult result);

    internal OnCompleted OnCompletedDelegate;

    public MNWSInfoRequestCurrentUserInfo (OnCompleted onCompleted) : base () {
      this.OnCompletedDelegate = onCompleted;
    }

    public new class RequestResult : MNWSInfoRequest.RequestResult {
      public MNWSCurrentUserInfo DataEntry {get;set;}

      public MNWSCurrentUserInfo GetDataEntry () {
        return DataEntry;
      }

      public override string ToString() {
        if (DataEntry == null) {
          return base.ToString() + " DataEntry: null";
        }

        return base.ToString() + " DataEntry: " + DataEntry.ToString();
      }
    }
  }

  public class MNWSCurrentUserInfo : MNWSGenericItem {
    public long? GetUserId () {
      return UserId;
    }
    public string GetUserNickName () {
      return UserNickName;
    }
    public bool? GetUserAvatarExists () {
      return UserAvatarExists;
    }
    public string GetUserAvatarUrl () {
      return UserAvatarUrl;
    }
    public bool? GetUserOnlineNow () {
      return UserOnlineNow;
    }
    public int? GetUserGamePoints () {
      return UserGamePoints;
    }
    public string GetUserEmail () {
      return UserEmail;
    }
    public int? GetUserStatus () {
      return UserStatus;
    }
    public bool? GetUserAvatarHasCustomImg () {
      return UserAvatarHasCustomImg;
    }
    public bool? GetUserAvatarHasExternalUrl () {
      return UserAvatarHasExternalUrl;
    }

    public long? UserId {get;set;}
    public string UserNickName {get;set;}
    public bool? UserAvatarExists {get;set;}
    public string UserAvatarUrl {get;set;}
    public bool? UserOnlineNow {get;set;}
    public int? UserGamePoints {get;set;}
    public string UserEmail {get;set;}
    public int? UserStatus {get;set;}
    public bool? UserAvatarHasCustomImg {get;set;}
    public bool? UserAvatarHasExternalUrl {get;set;}

    public override string ToString() {
      return "{" + "UserId = " + ((UserId == null)?"null":UserId.ToString()) + ";" +
                   "UserNickName = " + ((UserNickName == null)?"null":((UserNickName.Length == 0) ? "\"\"" : "\"" + UserNickName + "\"")) + ";" +
                   "UserAvatarExists = " + ((UserAvatarExists == null)?"null":UserAvatarExists.ToString()) + ";" +
                   "UserAvatarUrl = " + ((UserAvatarUrl == null)?"null":((UserAvatarUrl.Length == 0) ? "\"\"" : "\"" + UserAvatarUrl + "\"")) + ";" +
                   "UserOnlineNow = " + ((UserOnlineNow == null)?"null":UserOnlineNow.ToString()) + ";" +
                   "UserGamePoints = " + ((UserGamePoints == null)?"null":UserGamePoints.ToString()) + ";" +
                   "UserEmail = " + ((UserEmail == null)?"null":((UserEmail.Length == 0) ? "\"\"" : "\"" + UserEmail + "\"")) + ";" +
                   "UserStatus = " + ((UserStatus == null)?"null":UserStatus.ToString()) + ";" +
                   "UserAvatarHasCustomImg = " + ((UserAvatarHasCustomImg == null)?"null":UserAvatarHasCustomImg.ToString()) + ";" +
                   "UserAvatarHasExternalUrl = " + ((UserAvatarHasExternalUrl == null)?"null":UserAvatarHasExternalUrl.ToString()) + "}";
    }
  }

  #endregion MNWSInfoRequestCurrentUserInfo
  #region MNWSInfoRequestCurrGameRoomList

  public class MNWSInfoRequestCurrGameRoomList : MNWSInfoRequest {

    public delegate void OnCompleted(RequestResult result);

    internal OnCompleted OnCompletedDelegate;

    public MNWSInfoRequestCurrGameRoomList (OnCompleted onCompleted) : base () {
      this.OnCompletedDelegate = onCompleted;
    }

    public new class RequestResult : MNWSInfoRequest.RequestResult {
      public MNWSRoomListItem[] DataEntry {get;set;}

      public MNWSRoomListItem[] GetDataEntry () {
        return DataEntry;
      }

      public override string ToString() {
        if (DataEntry == null) {
          return base.ToString() + " DataEntry: null";
        }

        string itemsString = "";

        foreach (MNWSRoomListItem item in DataEntry) {
          if (itemsString.Length != 0) {
            itemsString += ",";
          }

          itemsString += item.ToString();
        }

        return base.ToString() + " DataEntry: [" + itemsString + "]";
      }
    }
  }

  public class MNWSRoomListItem : MNWSGenericItem {
    public int? GetGameId () {
      return GameId;
    }
    public int? GetRoomSFId () {
      return RoomSFId;
    }
    public string GetRoomName () {
      return RoomName;
    }
    public int? GetRoomUserCount () {
      return RoomUserCount;
    }
    public bool? GetRoomIsLobby () {
      return RoomIsLobby;
    }
    public int? GetGameSetId () {
      return GameSetId;
    }

    public int? GameId {get;set;}
    public int? RoomSFId {get;set;}
    public string RoomName {get;set;}
    public int? RoomUserCount {get;set;}
    public bool? RoomIsLobby {get;set;}
    public int? GameSetId {get;set;}

    public override string ToString() {
      return "{" + "GameId = " + ((GameId == null)?"null":GameId.ToString()) + ";" +
                   "RoomSFId = " + ((RoomSFId == null)?"null":RoomSFId.ToString()) + ";" +
                   "RoomName = " + ((RoomName == null)?"null":((RoomName.Length == 0) ? "\"\"" : "\"" + RoomName + "\"")) + ";" +
                   "RoomUserCount = " + ((RoomUserCount == null)?"null":RoomUserCount.ToString()) + ";" +
                   "RoomIsLobby = " + ((RoomIsLobby == null)?"null":RoomIsLobby.ToString()) + ";" +
                   "GameSetId = " + ((GameSetId == null)?"null":GameSetId.ToString()) + "}";
    }
  }

  #endregion MNWSInfoRequestCurrGameRoomList
  #region MNWSInfoRequestCurrGameRoomUserList

  public class MNWSInfoRequestCurrGameRoomUserList : MNWSInfoRequest {

    public delegate void OnCompleted(RequestResult result);

    internal OnCompleted OnCompletedDelegate;

    public MNWSInfoRequestCurrGameRoomUserList (int roomSFId,OnCompleted onCompleted) : base () {
      this.Parameters["roomSFId"] = roomSFId;
      this.OnCompletedDelegate = onCompleted;
    }

    public new class RequestResult : MNWSInfoRequest.RequestResult {
      public MNWSRoomUserInfoItem[] DataEntry {get;set;}

      public MNWSRoomUserInfoItem[] GetDataEntry () {
        return DataEntry;
      }

      public override string ToString() {
        if (DataEntry == null) {
          return base.ToString() + " DataEntry: null";
        }

        string itemsString = "";

        foreach (MNWSRoomUserInfoItem item in DataEntry) {
          if (itemsString.Length != 0) {
            itemsString += ",";
          }

          itemsString += item.ToString();
        }

        return base.ToString() + " DataEntry: [" + itemsString + "]";
      }
    }
  }

  public class MNWSRoomUserInfoItem : MNWSGenericItem {
    public long? GetUserId () {
      return UserId;
    }
    public string GetUserNickName () {
      return UserNickName;
    }
    public bool? GetUserAvatarExists () {
      return UserAvatarExists;
    }
    public string GetUserAvatarUrl () {
      return UserAvatarUrl;
    }
    public bool? GetUserOnlineNow () {
      return UserOnlineNow;
    }
    public int? GetRoomSFId () {
      return RoomSFId;
    }

    public long? UserId {get;set;}
    public string UserNickName {get;set;}
    public bool? UserAvatarExists {get;set;}
    public string UserAvatarUrl {get;set;}
    public bool? UserOnlineNow {get;set;}
    public int? RoomSFId {get;set;}

    public override string ToString() {
      return "{" + "UserId = " + ((UserId == null)?"null":UserId.ToString()) + ";" +
                   "UserNickName = " + ((UserNickName == null)?"null":((UserNickName.Length == 0) ? "\"\"" : "\"" + UserNickName + "\"")) + ";" +
                   "UserAvatarExists = " + ((UserAvatarExists == null)?"null":UserAvatarExists.ToString()) + ";" +
                   "UserAvatarUrl = " + ((UserAvatarUrl == null)?"null":((UserAvatarUrl.Length == 0) ? "\"\"" : "\"" + UserAvatarUrl + "\"")) + ";" +
                   "UserOnlineNow = " + ((UserOnlineNow == null)?"null":UserOnlineNow.ToString()) + ";" +
                   "RoomSFId = " + ((RoomSFId == null)?"null":RoomSFId.ToString()) + "}";
    }
  }

  #endregion MNWSInfoRequestCurrGameRoomUserList
  #region MNWSInfoRequestCurrUserBuddyList

  public class MNWSInfoRequestCurrUserBuddyList : MNWSInfoRequest {

    public delegate void OnCompleted(RequestResult result);

    internal OnCompleted OnCompletedDelegate;

    public MNWSInfoRequestCurrUserBuddyList (OnCompleted onCompleted) : base () {
      this.OnCompletedDelegate = onCompleted;
    }

    public new class RequestResult : MNWSInfoRequest.RequestResult {
      public MNWSBuddyListItem[] DataEntry {get;set;}

      public MNWSBuddyListItem[] GetDataEntry () {
        return DataEntry;
      }

      public override string ToString() {
        if (DataEntry == null) {
          return base.ToString() + " DataEntry: null";
        }

        string itemsString = "";

        foreach (MNWSBuddyListItem item in DataEntry) {
          if (itemsString.Length != 0) {
            itemsString += ",";
          }

          itemsString += item.ToString();
        }

        return base.ToString() + " DataEntry: [" + itemsString + "]";
      }
    }
  }

  public class MNWSBuddyListItem : MNWSGenericItem {
    public long? GetFriendUserId () {
      return FriendUserId;
    }
    public string GetFriendUserNickName () {
      return FriendUserNickName;
    }
    public string GetFriendSnIdList () {
      return FriendSnIdList;
    }
    public string GetFriendSnUserAsnIdList () {
      return FriendSnUserAsnIdList;
    }
    public int? GetFriendInGameId () {
      return FriendInGameId;
    }
    public string GetFriendInGameName () {
      return FriendInGameName;
    }
    public string GetFriendInGameIconUrl () {
      return FriendInGameIconUrl;
    }
    public bool? GetFriendHasCurrentGame () {
      return FriendHasCurrentGame;
    }
    public string GetFriendUserLocale () {
      return FriendUserLocale;
    }
    public string GetFriendUserAvatarUrl () {
      return FriendUserAvatarUrl;
    }
    public bool? GetFriendUserOnlineNow () {
      return FriendUserOnlineNow;
    }
    public int? GetFriendUserSfid () {
      return FriendUserSfid;
    }
    public int? GetFriendSnId () {
      return FriendSnId;
    }
    public long? GetFriendSnUserAsnId () {
      return FriendSnUserAsnId;
    }
    public long? GetFriendFlags () {
      return FriendFlags;
    }
    public bool? GetFriendIsIgnored () {
      return FriendIsIgnored;
    }
    public int? GetFriendInRoomSfid () {
      return FriendInRoomSfid;
    }
    public bool? GetFriendInRoomIsLobby () {
      return FriendInRoomIsLobby;
    }
    public string GetFriendCurrGameAchievementsList () {
      return FriendCurrGameAchievementsList;
    }

    public long? FriendUserId {get;set;}
    public string FriendUserNickName {get;set;}
    public string FriendSnIdList {get;set;}
    public string FriendSnUserAsnIdList {get;set;}
    public int? FriendInGameId {get;set;}
    public string FriendInGameName {get;set;}
    public string FriendInGameIconUrl {get;set;}
    public bool? FriendHasCurrentGame {get;set;}
    public string FriendUserLocale {get;set;}
    public string FriendUserAvatarUrl {get;set;}
    public bool? FriendUserOnlineNow {get;set;}
    public int? FriendUserSfid {get;set;}
    public int? FriendSnId {get;set;}
    public long? FriendSnUserAsnId {get;set;}
    public long? FriendFlags {get;set;}
    public bool? FriendIsIgnored {get;set;}
    public int? FriendInRoomSfid {get;set;}
    public bool? FriendInRoomIsLobby {get;set;}
    public string FriendCurrGameAchievementsList {get;set;}

    public override string ToString() {
      return "{" + "FriendUserId = " + ((FriendUserId == null)?"null":FriendUserId.ToString()) + ";" +
                   "FriendUserNickName = " + ((FriendUserNickName == null)?"null":((FriendUserNickName.Length == 0) ? "\"\"" : "\"" + FriendUserNickName + "\"")) + ";" +
                   "FriendSnIdList = " + ((FriendSnIdList == null)?"null":((FriendSnIdList.Length == 0) ? "\"\"" : "\"" + FriendSnIdList + "\"")) + ";" +
                   "FriendSnUserAsnIdList = " + ((FriendSnUserAsnIdList == null)?"null":((FriendSnUserAsnIdList.Length == 0) ? "\"\"" : "\"" + FriendSnUserAsnIdList + "\"")) + ";" +
                   "FriendInGameId = " + ((FriendInGameId == null)?"null":FriendInGameId.ToString()) + ";" +
                   "FriendInGameName = " + ((FriendInGameName == null)?"null":((FriendInGameName.Length == 0) ? "\"\"" : "\"" + FriendInGameName + "\"")) + ";" +
                   "FriendInGameIconUrl = " + ((FriendInGameIconUrl == null)?"null":((FriendInGameIconUrl.Length == 0) ? "\"\"" : "\"" + FriendInGameIconUrl + "\"")) + ";" +
                   "FriendHasCurrentGame = " + ((FriendHasCurrentGame == null)?"null":FriendHasCurrentGame.ToString()) + ";" +
                   "FriendUserLocale = " + ((FriendUserLocale == null)?"null":((FriendUserLocale.Length == 0) ? "\"\"" : "\"" + FriendUserLocale + "\"")) + ";" +
                   "FriendUserAvatarUrl = " + ((FriendUserAvatarUrl == null)?"null":((FriendUserAvatarUrl.Length == 0) ? "\"\"" : "\"" + FriendUserAvatarUrl + "\"")) + ";" +
                   "FriendUserOnlineNow = " + ((FriendUserOnlineNow == null)?"null":FriendUserOnlineNow.ToString()) + ";" +
                   "FriendUserSfid = " + ((FriendUserSfid == null)?"null":FriendUserSfid.ToString()) + ";" +
                   "FriendSnId = " + ((FriendSnId == null)?"null":FriendSnId.ToString()) + ";" +
                   "FriendSnUserAsnId = " + ((FriendSnUserAsnId == null)?"null":FriendSnUserAsnId.ToString()) + ";" +
                   "FriendFlags = " + ((FriendFlags == null)?"null":FriendFlags.ToString()) + ";" +
                   "FriendIsIgnored = " + ((FriendIsIgnored == null)?"null":FriendIsIgnored.ToString()) + ";" +
                   "FriendInRoomSfid = " + ((FriendInRoomSfid == null)?"null":FriendInRoomSfid.ToString()) + ";" +
                   "FriendInRoomIsLobby = " + ((FriendInRoomIsLobby == null)?"null":FriendInRoomIsLobby.ToString()) + ";" +
                   "FriendCurrGameAchievementsList = " + ((FriendCurrGameAchievementsList == null)?"null":((FriendCurrGameAchievementsList.Length == 0) ? "\"\"" : "\"" + FriendCurrGameAchievementsList + "\"")) + "}";
    }
  }

  #endregion MNWSInfoRequestCurrUserBuddyList
  #region MNWSInfoRequestCurrUserSubscriptionStatus

  public class MNWSInfoRequestCurrUserSubscriptionStatus : MNWSInfoRequest {

    public delegate void OnCompleted(RequestResult result);

    internal OnCompleted OnCompletedDelegate;

    public MNWSInfoRequestCurrUserSubscriptionStatus (OnCompleted onCompleted) : base () {
      this.OnCompletedDelegate = onCompleted;
    }

    public new class RequestResult : MNWSInfoRequest.RequestResult {
      public MNWSCurrUserSubscriptionStatus DataEntry {get;set;}

      public MNWSCurrUserSubscriptionStatus GetDataEntry () {
        return DataEntry;
      }

      public override string ToString() {
        if (DataEntry == null) {
          return base.ToString() + " DataEntry: null";
        }

        return base.ToString() + " DataEntry: " + DataEntry.ToString();
      }
    }
  }

  public class MNWSCurrUserSubscriptionStatus : MNWSGenericItem {
    public bool? GetHasSubscription () {
      return HasSubscription;
    }
    public string GetOffersAvailable () {
      return OffersAvailable;
    }
    public bool? GetIsSubscriptionAvailable () {
      return IsSubscriptionAvailable;
    }

    public bool? HasSubscription {get;set;}
    public string OffersAvailable {get;set;}
    public bool? IsSubscriptionAvailable {get;set;}

    public override string ToString() {
      return "{" + "HasSubscription = " + ((HasSubscription == null)?"null":HasSubscription.ToString()) + ";" +
                   "OffersAvailable = " + ((OffersAvailable == null)?"null":((OffersAvailable.Length == 0) ? "\"\"" : "\"" + OffersAvailable + "\"")) + ";" +
                   "IsSubscriptionAvailable = " + ((IsSubscriptionAvailable == null)?"null":IsSubscriptionAvailable.ToString()) + "}";
    }
  }

  #endregion MNWSInfoRequestCurrUserSubscriptionStatus
  #region MNWSInfoRequestSessionSignedClientToken

  public class MNWSInfoRequestSessionSignedClientToken : MNWSInfoRequest {

    public delegate void OnCompleted(RequestResult result);

    internal OnCompleted OnCompletedDelegate;

    public MNWSInfoRequestSessionSignedClientToken (string payload,OnCompleted onCompleted) : base () {
      this.Parameters["payload"] = payload;
      this.OnCompletedDelegate = onCompleted;
    }

    public new class RequestResult : MNWSInfoRequest.RequestResult {
      public MNWSSessionSignedClientToken DataEntry {get;set;}

      public MNWSSessionSignedClientToken GetDataEntry () {
        return DataEntry;
      }

      public override string ToString() {
        if (DataEntry == null) {
          return base.ToString() + " DataEntry: null";
        }

        return base.ToString() + " DataEntry: " + DataEntry.ToString();
      }
    }
  }

  public class MNWSSessionSignedClientToken : MNWSGenericItem {
    public string GetClientTokenBody () {
      return ClientTokenBody;
    }
    public string GetClientTokenSign () {
      return ClientTokenSign;
    }

    public string ClientTokenBody {get;set;}
    public string ClientTokenSign {get;set;}

    public override string ToString() {
      return "{" + "ClientTokenBody = " + ((ClientTokenBody == null)?"null":((ClientTokenBody.Length == 0) ? "\"\"" : "\"" + ClientTokenBody + "\"")) + ";" +
                   "ClientTokenSign = " + ((ClientTokenSign == null)?"null":((ClientTokenSign.Length == 0) ? "\"\"" : "\"" + ClientTokenSign + "\"")) + "}";
    }
  }

  #endregion MNWSInfoRequestSessionSignedClientToken
  #region MNWSInfoRequestSystemGameNetStats

  public class MNWSInfoRequestSystemGameNetStats : MNWSInfoRequest {

    public delegate void OnCompleted(RequestResult result);

    internal OnCompleted OnCompletedDelegate;

    public MNWSInfoRequestSystemGameNetStats (OnCompleted onCompleted) : base () {
      this.OnCompletedDelegate = onCompleted;
    }

    public new class RequestResult : MNWSInfoRequest.RequestResult {
      public MNWSSystemGameNetStats DataEntry {get;set;}

      public MNWSSystemGameNetStats GetDataEntry () {
        return DataEntry;
      }

      public override string ToString() {
        if (DataEntry == null) {
          return base.ToString() + " DataEntry: null";
        }

        return base.ToString() + " DataEntry: " + DataEntry.ToString();
      }
    }
  }

  public class MNWSSystemGameNetStats : MNWSGenericItem {
    public long? GetServTotalUsers () {
      return ServTotalUsers;
    }
    public long? GetServTotalGames () {
      return ServTotalGames;
    }
    public long? GetServOnlineUsers () {
      return ServOnlineUsers;
    }
    public long? GetServOnlineRooms () {
      return ServOnlineRooms;
    }
    public long? GetServOnlineGames () {
      return ServOnlineGames;
    }
    public long? GetGameOnlineUsers () {
      return GameOnlineUsers;
    }
    public long? GetGameOnlineRooms () {
      return GameOnlineRooms;
    }

    public long? ServTotalUsers {get;set;}
    public long? ServTotalGames {get;set;}
    public long? ServOnlineUsers {get;set;}
    public long? ServOnlineRooms {get;set;}
    public long? ServOnlineGames {get;set;}
    public long? GameOnlineUsers {get;set;}
    public long? GameOnlineRooms {get;set;}

    public override string ToString() {
      return "{" + "ServTotalUsers = " + ((ServTotalUsers == null)?"null":ServTotalUsers.ToString()) + ";" +
                   "ServTotalGames = " + ((ServTotalGames == null)?"null":ServTotalGames.ToString()) + ";" +
                   "ServOnlineUsers = " + ((ServOnlineUsers == null)?"null":ServOnlineUsers.ToString()) + ";" +
                   "ServOnlineRooms = " + ((ServOnlineRooms == null)?"null":ServOnlineRooms.ToString()) + ";" +
                   "ServOnlineGames = " + ((ServOnlineGames == null)?"null":ServOnlineGames.ToString()) + ";" +
                   "GameOnlineUsers = " + ((GameOnlineUsers == null)?"null":GameOnlineUsers.ToString()) + ";" +
                   "GameOnlineRooms = " + ((GameOnlineRooms == null)?"null":GameOnlineRooms.ToString()) + "}";
    }
  }

  #endregion MNWSInfoRequestSystemGameNetStats
  #region MNWSInfoRequestLeaderboard

  public class MNWSInfoRequestLeaderboard : MNWSInfoRequest {
    public const int LEADERBOARD_PERIOD_ALL_TIME = 0;
    public const int LEADERBOARD_PERIOD_THIS_WEEK = 1;
    public const int LEADERBOARD_PERIOD_THIS_MONTH = 2;

    public const int LEADERBOARD_SCOPE_GLOBAL = 0;
    public const int LEADERBOARD_SCOPE_LOCAL = 1;

    public class LeaderboardModeCurrentUser : LeaderboardMode
    {
      public LeaderboardModeCurrentUser (int scope, int period)
      {
        this.Scope  = scope;
        this.Period = period;
      }
  
      public int Scope {get;private set;}
      public int Period {get;private set;}

      public override IDictionary GetParametersDictionary ()
      {
        Hashtable paramsDict = new Hashtable();

        paramsDict["Scope"] = Scope;
        paramsDict["Period"] = Period;
        paramsDict["Mode"] = Mode;

        return paramsDict;
      }
    }
    public class LeaderboardModeAnyGameGlobal : LeaderboardMode
    {
      public LeaderboardModeAnyGameGlobal (int gameId, int gameSetId, int period)
      {
        this.GameId    = gameId;
        this.GameSetId = gameSetId;
        this.Period    = period;
      }

      public int GameId {get;private set;}
      public int GameSetId {get;private set;}
      public int Period {get;private set;}

      public override IDictionary GetParametersDictionary ()
      {
        Hashtable paramsDict = new Hashtable();

        paramsDict["GameId"] = GameId;
        paramsDict["GameSetId"] = GameSetId;
        paramsDict["Period"] = Period;
        paramsDict["Mode"] = Mode;

        return paramsDict;
      }
    }
    public class LeaderboardModeAnyUserAnyGameGlobal : LeaderboardMode
    {
      public LeaderboardModeAnyUserAnyGameGlobal (long userId, int gameId, int gameSetId, int period)
      {
        this.UserId    = userId;
        this.GameId    = gameId;
        this.GameSetId = gameSetId;
        this.Period    = period;
      }
  
      public long UserId {get;private set;}
      public int  GameId {get;private set;}
      public int  GameSetId {get;private set;}
      public int  Period {get;private set;}

      public override IDictionary GetParametersDictionary ()
      {
        Hashtable paramsDict = new Hashtable();

        paramsDict["UserId"] = UserId;
        paramsDict["GameId"] = GameId;
        paramsDict["GameSetId"] = GameSetId;
        paramsDict["Period"] = Period;
        paramsDict["Mode"] = Mode;

        return paramsDict;
      }
    }
    public class LeaderboardModeCurrUserAnyGameLocal : LeaderboardMode
    {
      public LeaderboardModeCurrUserAnyGameLocal (int gameId, int gameSetId, int period)
      {
        this.GameId    = gameId;
        this.GameSetId = gameSetId;
        this.Period    = period;
      }
  
      public int GameId {get;private set;}
      public int GameSetId {get;private set;}
      public int Period {get;private set;}
      public override IDictionary GetParametersDictionary ()
      {
        Hashtable paramsDict = new Hashtable();

        paramsDict["GameId"] = GameId;
        paramsDict["GameSetId"] = GameSetId;
        paramsDict["Period"] = Period;
        paramsDict["Mode"] = Mode;

        return paramsDict;
      }
    }
    public abstract class LeaderboardMode
    {
      protected LeaderboardMode () {
        this.Mode = this.GetType().Name;
      }

      public string Mode {get;set;}

      public abstract IDictionary GetParametersDictionary();
    }

    public delegate void OnCompleted(RequestResult result);

    internal OnCompleted OnCompletedDelegate;

    public MNWSInfoRequestLeaderboard (LeaderboardMode mode,OnCompleted onCompleted) : base () {
      this.Parameters["LeaderboardMode"] = mode.GetParametersDictionary();
      this.OnCompletedDelegate = onCompleted;
    }

    public new class RequestResult : MNWSInfoRequest.RequestResult {
      public MNWSLeaderboardListItem[] DataEntry {get;set;}

      public MNWSLeaderboardListItem[] GetDataEntry () {
        return DataEntry;
      }

      public override string ToString() {
        if (DataEntry == null) {
          return base.ToString() + " DataEntry: null";
        }

        string itemsString = "";

        foreach (MNWSLeaderboardListItem item in DataEntry) {
          if (itemsString.Length != 0) {
            itemsString += ",";
          }

          itemsString += item.ToString();
        }

        return base.ToString() + " DataEntry: [" + itemsString + "]";
      }
    }
  }

  public class MNWSLeaderboardListItem : MNWSGenericItem {
    public int? GetGameId () {
      return GameId;
    }
    public long? GetUserId () {
      return UserId;
    }
    public string GetUserNickName () {
      return UserNickName;
    }
    public string GetUserAvatarUrl () {
      return UserAvatarUrl;
    }
    public bool? GetUserOnlineNow () {
      return UserOnlineNow;
    }
    public bool? GetUserIsFriend () {
      return UserIsFriend;
    }
    public int? GetUserSfid () {
      return UserSfid;
    }
    public bool? GetUserIsIgnored () {
      return UserIsIgnored;
    }
    public string GetUserLocale () {
      return UserLocale;
    }
    public long? GetOutHiScore () {
      return OutHiScore;
    }
    public string GetOutHiScoreText () {
      return OutHiScoreText;
    }
    public long? GetOutHiDateTime () {
      return OutHiDateTime;
    }
    public long? GetOutHiDateTimeDiff () {
      return OutHiDateTimeDiff;
    }
    public long? GetOutUserPlace () {
      return OutUserPlace;
    }
    public int? GetGamesetId () {
      return GamesetId;
    }
    public string GetUserAchievementsList () {
      return UserAchievementsList;
    }

    public int? GameId {get;set;}
    public long? UserId {get;set;}
    public string UserNickName {get;set;}
    public string UserAvatarUrl {get;set;}
    public bool? UserOnlineNow {get;set;}
    public bool? UserIsFriend {get;set;}
    public int? UserSfid {get;set;}
    public bool? UserIsIgnored {get;set;}
    public string UserLocale {get;set;}
    public long? OutHiScore {get;set;}
    public string OutHiScoreText {get;set;}
    public long? OutHiDateTime {get;set;}
    public long? OutHiDateTimeDiff {get;set;}
    public long? OutUserPlace {get;set;}
    public int? GamesetId {get;set;}
    public string UserAchievementsList {get;set;}

    public override string ToString() {
      return "{" + "GameId = " + ((GameId == null)?"null":GameId.ToString()) + ";" +
                   "UserId = " + ((UserId == null)?"null":UserId.ToString()) + ";" +
                   "UserNickName = " + ((UserNickName == null)?"null":((UserNickName.Length == 0) ? "\"\"" : "\"" + UserNickName + "\"")) + ";" +
                   "UserAvatarUrl = " + ((UserAvatarUrl == null)?"null":((UserAvatarUrl.Length == 0) ? "\"\"" : "\"" + UserAvatarUrl + "\"")) + ";" +
                   "UserOnlineNow = " + ((UserOnlineNow == null)?"null":UserOnlineNow.ToString()) + ";" +
                   "UserIsFriend = " + ((UserIsFriend == null)?"null":UserIsFriend.ToString()) + ";" +
                   "UserSfid = " + ((UserSfid == null)?"null":UserSfid.ToString()) + ";" +
                   "UserIsIgnored = " + ((UserIsIgnored == null)?"null":UserIsIgnored.ToString()) + ";" +
                   "UserLocale = " + ((UserLocale == null)?"null":((UserLocale.Length == 0) ? "\"\"" : "\"" + UserLocale + "\"")) + ";" +
                   "OutHiScore = " + ((OutHiScore == null)?"null":OutHiScore.ToString()) + ";" +
                   "OutHiScoreText = " + ((OutHiScoreText == null)?"null":((OutHiScoreText.Length == 0) ? "\"\"" : "\"" + OutHiScoreText + "\"")) + ";" +
                   "OutHiDateTime = " + ((OutHiDateTime == null)?"null":OutHiDateTime.ToString()) + ";" +
                   "OutHiDateTimeDiff = " + ((OutHiDateTimeDiff == null)?"null":OutHiDateTimeDiff.ToString()) + ";" +
                   "OutUserPlace = " + ((OutUserPlace == null)?"null":OutUserPlace.ToString()) + ";" +
                   "GamesetId = " + ((GamesetId == null)?"null":GamesetId.ToString()) + ";" +
                   "UserAchievementsList = " + ((UserAchievementsList == null)?"null":((UserAchievementsList.Length == 0) ? "\"\"" : "\"" + UserAchievementsList + "\"")) + "}";
    }
  }

  #endregion
  #region Generics

  public class MNWSGenericItem {
  }

  public abstract class MNWSInfoRequest {
    public class RequestResult {

      public string GetErrorMessage ()
      {
        return ErrorMessage;
      }

      public bool HadError {get;set;}
      public string ErrorMessage {get;set;}

      public override string ToString ()
      {
        string errorMessageToString = "";

        if (ErrorMessage == null) {
          errorMessageToString = "null";
        }
        else if (ErrorMessage.Equals("")) {
          errorMessageToString = "\"\"";
        }
        else {
          errorMessageToString = ErrorMessage;
        }

        return "HadError = " + HadError.ToString() + "; ErrorMessage = " + errorMessageToString  + ";";
      }
    }

    public long Id;
    public string Name;
    public Hashtable Parameters = new Hashtable();

    internal MNWSInfoRequest () {
      Id = MNWSProvider.GetRequestId();
      Name = this.GetType().Name;
    }
  }

  #endregion
}

