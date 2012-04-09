using System;
using System.Collections;
using System.Collections.Generic;
using PlayPhone.MultiNet.Providers;

namespace PlayPhone.MultiNet.Core
{
  public class MNSerializerProxy
  {
    public static IDictionary ObjectToDictionary(object srcObject,Type srcObjectType) {
      MNTools.DetailedLog(string.Format("ObjectToDictionary. ObjectType={0}",MNTools.SafeToString(srcObjectType)));

      IDictionary proxyDictionary = null;

      if (srcObjectType.Equals(typeof(MNErrorInfo))) {
        proxyDictionary = MNSerializerProxy.MNErrorInfoToDictionary((MNErrorInfo)srcObject);
      }
      else if (srcObjectType.Equals(typeof(MNGameParams))) {
        proxyDictionary = MNSerializerProxy.MNGameParamsToDictionary((MNGameParams)srcObject);
      }
      else if (srcObjectType.Equals(typeof(MNUserInfo))) {
        proxyDictionary = MNSerializerProxy.MNUserInfoToDictionary((MNUserInfo)srcObject);
      }
      else if (srcObjectType.Equals(typeof(MNAchievementsProvider.GameAchievementInfo))) {
        proxyDictionary = MNSerializerProxy.MNAchievementsProviderGameAchievementInfoToDictionary((MNAchievementsProvider.GameAchievementInfo)srcObject);
      }
      else if (srcObjectType.Equals(typeof(MNAchievementsProvider.PlayerAchievementInfo))) {
        proxyDictionary = MNSerializerProxy.MNAchievementsProviderPlayerAchievementInfoToDictionary((MNAchievementsProvider.PlayerAchievementInfo)srcObject);
      }
      else if (srcObjectType.Equals(typeof(MNVItemsProvider.GameVItemInfo))) {
        proxyDictionary = MNSerializerProxy.MNVItemsProviderGameVItemInfoToDictionary((MNVItemsProvider.GameVItemInfo)srcObject);
      }
      else if (srcObjectType.Equals(typeof(MNVItemsProvider.PlayerVItemInfo))) {
        proxyDictionary = MNSerializerProxy.MNVItemsProviderPlayerVItemInfoToDictionary((MNVItemsProvider.PlayerVItemInfo)srcObject);
      }
      else if (srcObjectType.Equals(typeof(MNVItemsProvider.TransactionVItemInfo))) {
        proxyDictionary = MNSerializerProxy.MNVItemsProviderTransactionVItemInfoToDictionary((MNVItemsProvider.TransactionVItemInfo)srcObject);
      }
      else if (srcObjectType.Equals(typeof(MNVItemsProvider.TransactionInfo))) {
        proxyDictionary = MNSerializerProxy.MNVItemsProviderTransactionInfoToDictionary((MNVItemsProvider.TransactionInfo)srcObject);
      }
      else if (srcObjectType.Equals(typeof(MNVItemsProvider.TransactionError))) {
        proxyDictionary = MNSerializerProxy.MNVItemsProviderTransactionErrorToDictionary((MNVItemsProvider.TransactionError)srcObject);
      }
      else if (srcObjectType.Equals(typeof(MNVShopProvider.VShopDeliveryInfo))) {
        proxyDictionary = MNSerializerProxy.MNVShopProviderVShopDeliveryInfoToDictionary((MNVShopProvider.VShopDeliveryInfo)srcObject);
      }
      else if (srcObjectType.Equals(typeof(MNVShopProvider.VShopPackInfo))) {
        proxyDictionary = MNSerializerProxy.MNVShopProviderVShopPackInfoToDictionary((MNVShopProvider.VShopPackInfo)srcObject);
      }
      else if (srcObjectType.Equals(typeof(MNVShopProvider.VShopCategoryInfo))) {
        proxyDictionary = MNSerializerProxy.MNVShopProviderVShopCategoryInfoToDictionary((MNVShopProvider.VShopCategoryInfo)srcObject);
      }
      else if (srcObjectType.Equals(typeof(MNVShopProvider.VShopPackBuyRequestItem))) {
        proxyDictionary = MNSerializerProxy.MNVShopProviderVShopPackBuyRequestItemToDictionary((MNVShopProvider.VShopPackBuyRequestItem)srcObject);
      }
      else if (srcObjectType.Equals(typeof(MNScoreProgressProvider.ScoreItem))) {
        proxyDictionary = MNSerializerProxy.MNScoreProgressProviderScoreItemToDictionary((MNScoreProgressProvider.ScoreItem)srcObject);
      }
      else if (srcObjectType.Equals(typeof(MNBuddyRoomParams))) {
        proxyDictionary = MNSerializerProxy.MNBuddyRoomParamsToDictionary((MNBuddyRoomParams)srcObject);
      }
      else if (srcObjectType.Equals(typeof(MNJoinRoomInvitationParams))) {
        proxyDictionary = MNSerializerProxy.MNJoinRoomInvitationParamsToDictionary((MNJoinRoomInvitationParams)srcObject);
      }
      else if (srcObjectType.Equals(typeof(MNVShopProvider.CheckoutVShopPackSuccessInfo))) {
        proxyDictionary = MNSerializerProxy.MNVShopProviderCheckoutVShopPackSuccessInfoToDictionary((MNVShopProvider.CheckoutVShopPackSuccessInfo)srcObject);
      }
      else if (srcObjectType.Equals(typeof(MNVShopProvider.CheckoutVShopPackFailInfo))) {
        proxyDictionary = MNSerializerProxy.MNVShopProviderCheckoutVShopPackFailInfoToDictionary((MNVShopProvider.CheckoutVShopPackFailInfo)srcObject);
      }
      else if (srcObjectType.Equals(typeof(MNGameSettingsProvider.GameSettingInfo))) {
        proxyDictionary = MNSerializerProxy.MNGameSettingsProviderGameSettingInfoToDictionary((MNGameSettingsProvider.GameSettingInfo)srcObject);
      }
      else if (srcObjectType.Equals(typeof(MNWSAnyGameItem))) {
        proxyDictionary = MNSerializerProxy.MNWSAnyGameItemToDictionary((MNWSAnyGameItem)srcObject);
      }
      else if (srcObjectType.Equals(typeof(MNWSInfoRequestAnyGame.RequestResult))) {
        proxyDictionary = MNSerializerProxy.MNWSInfoRequestAnyGameRequestResultToDictionary((MNWSInfoRequestAnyGame.RequestResult)srcObject);
      }
      else if (srcObjectType.Equals(typeof(MNWSAnyUserItem))) {
        proxyDictionary = MNSerializerProxy.MNWSAnyUserItemToDictionary((MNWSAnyUserItem)srcObject);
      }
      else if (srcObjectType.Equals(typeof(MNWSInfoRequestAnyUser.RequestResult))) {
        proxyDictionary = MNSerializerProxy.MNWSInfoRequestAnyUserRequestResultToDictionary((MNWSInfoRequestAnyUser.RequestResult)srcObject);
      }
      else if (srcObjectType.Equals(typeof(MNWSUserGameCookie))) {
        proxyDictionary = MNSerializerProxy.MNWSUserGameCookieToDictionary((MNWSUserGameCookie)srcObject);
      }
      else if (srcObjectType.Equals(typeof(MNWSInfoRequestAnyUserGameCookies.RequestResult))) {
        proxyDictionary = MNSerializerProxy.MNWSInfoRequestAnyUserGameCookiesRequestResultToDictionary((MNWSInfoRequestAnyUserGameCookies.RequestResult)srcObject);
      }
      else if (srcObjectType.Equals(typeof(MNWSCurrentUserInfo))) {
        proxyDictionary = MNSerializerProxy.MNWSCurrentUserInfoToDictionary((MNWSCurrentUserInfo)srcObject);
      }
      else if (srcObjectType.Equals(typeof(MNWSInfoRequestCurrentUserInfo.RequestResult))) {
        proxyDictionary = MNSerializerProxy.MNWSInfoRequestCurrentUserInfoRequestResultToDictionary((MNWSInfoRequestCurrentUserInfo.RequestResult)srcObject);
      }
      else if (srcObjectType.Equals(typeof(MNWSRoomListItem))) {
        proxyDictionary = MNSerializerProxy.MNWSRoomListItemToDictionary((MNWSRoomListItem)srcObject);
      }
      else if (srcObjectType.Equals(typeof(MNWSInfoRequestCurrGameRoomList.RequestResult))) {
        proxyDictionary = MNSerializerProxy.MNWSInfoRequestCurrGameRoomListRequestResultToDictionary((MNWSInfoRequestCurrGameRoomList.RequestResult)srcObject);
      }
      else if (srcObjectType.Equals(typeof(MNWSRoomUserInfoItem))) {
        proxyDictionary = MNSerializerProxy.MNWSRoomUserInfoItemToDictionary((MNWSRoomUserInfoItem)srcObject);
      }
      else if (srcObjectType.Equals(typeof(MNWSInfoRequestCurrGameRoomUserList.RequestResult))) {
        proxyDictionary = MNSerializerProxy.MNWSInfoRequestCurrGameRoomUserListRequestResultToDictionary((MNWSInfoRequestCurrGameRoomUserList.RequestResult)srcObject);
      }
      else if (srcObjectType.Equals(typeof(MNWSBuddyListItem))) {
        proxyDictionary = MNSerializerProxy.MNWSBuddyListItemToDictionary((MNWSBuddyListItem)srcObject);
      }
      else if (srcObjectType.Equals(typeof(MNWSInfoRequestCurrUserBuddyList.RequestResult))) {
        proxyDictionary = MNSerializerProxy.MNWSInfoRequestCurrUserBuddyListRequestResultToDictionary((MNWSInfoRequestCurrUserBuddyList.RequestResult)srcObject);
      }
      else if (srcObjectType.Equals(typeof(MNWSCurrUserSubscriptionStatus))) {
        proxyDictionary = MNSerializerProxy.MNWSCurrUserSubscriptionStatusToDictionary((MNWSCurrUserSubscriptionStatus)srcObject);
      }
      else if (srcObjectType.Equals(typeof(MNWSInfoRequestCurrUserSubscriptionStatus.RequestResult))) {
        proxyDictionary = MNSerializerProxy.MNWSInfoRequestCurrUserSubscriptionStatusRequestResultToDictionary((MNWSInfoRequestCurrUserSubscriptionStatus.RequestResult)srcObject);
      }
      else if (srcObjectType.Equals(typeof(MNWSSessionSignedClientToken))) {
        proxyDictionary = MNSerializerProxy.MNWSSessionSignedClientTokenToDictionary((MNWSSessionSignedClientToken)srcObject);
      }
      else if (srcObjectType.Equals(typeof(MNWSInfoRequestSessionSignedClientToken.RequestResult))) {
        proxyDictionary = MNSerializerProxy.MNWSInfoRequestSessionSignedClientTokenRequestResultToDictionary((MNWSInfoRequestSessionSignedClientToken.RequestResult)srcObject);
      }
      else if (srcObjectType.Equals(typeof(MNWSSystemGameNetStats))) {
        proxyDictionary = MNSerializerProxy.MNWSSystemGameNetStatsToDictionary((MNWSSystemGameNetStats)srcObject);
      }
      else if (srcObjectType.Equals(typeof(MNWSInfoRequestSystemGameNetStats.RequestResult))) {
        proxyDictionary = MNSerializerProxy.MNWSInfoRequestSystemGameNetStatsRequestResultToDictionary((MNWSInfoRequestSystemGameNetStats.RequestResult)srcObject);
      }
      else if (srcObjectType.Equals(typeof(MNWSLeaderboardListItem))) {
        proxyDictionary = MNSerializerProxy.MNWSLeaderboardListItemToDictionary((MNWSLeaderboardListItem)srcObject);
      }
      else if (srcObjectType.Equals(typeof(MNWSInfoRequestLeaderboard.RequestResult))) {
        proxyDictionary = MNSerializerProxy.MNWSInfoRequestLeaderboardRequestResultToDictionary((MNWSInfoRequestLeaderboard.RequestResult)srcObject);
      }
      else if (srcObjectType.Equals(typeof(MNWSInfoRequest))) {
        proxyDictionary = MNSerializerProxy.MNWSInfoRequestToDictionary((MNWSInfoRequest)srcObject);
      }
      else {
        throw new MNSerializationException(srcObjectType);
      }

      return proxyDictionary;
    }

    public static Hashtable MNErrorInfoToDictionary (MNErrorInfo srcObject) {
      Hashtable result = new Hashtable(2);

      result["ActionCode"] = srcObject.ActionCode;
      result["ErrorMessage"] = srcObject.ErrorMessage;

      return result;
    }

    public static Hashtable MNGameParamsToDictionary (MNGameParams srcObject) {
      Hashtable result = new Hashtable(6);

      result["GameSeed"] = srcObject.GameSeed;
      result["GameSetId"] = srcObject.GameSetId;
      result["GameSetParams"] = srcObject.GameSetParams;
      result["PlayModel"] = srcObject.PlayModel;
      result["ScorePostLinkId"] = srcObject.ScorePostLinkId;

      return result;
    }

    public static Hashtable MNUserInfoToDictionary (MNUserInfo srcObject) {
      Hashtable result = new Hashtable(3);

      result["UserId"] = srcObject.UserId;
      result["UserName"] = srcObject.UserName;
      result["UserSFId"] = srcObject.UserSFId;

      return result;
    }

    public static Hashtable MNAchievementsProviderGameAchievementInfoToDictionary (MNAchievementsProvider.GameAchievementInfo srcObject) {
      Hashtable result = new Hashtable(6);

      result["Description"] = srcObject.Description;
      result["Flags"] = srcObject.Flags;
      result["Id"] = srcObject.Id;
      result["Name"] = srcObject.Name;
      result["Params"] = srcObject.Params;
      result["Points"] = srcObject.Points;

      return result;
    }

    public static Hashtable MNAchievementsProviderPlayerAchievementInfoToDictionary (MNAchievementsProvider.PlayerAchievementInfo srcObject) {
      Hashtable result = new Hashtable(1);

      result["Id"] = srcObject.Id;

      return result;
    }

    public static Hashtable MNVItemsProviderGameVItemInfoToDictionary (MNVItemsProvider.GameVItemInfo srcObject) {
      Hashtable result = new Hashtable(5);

      result["Description"] = srcObject.Description;
      result["Id"] = srcObject.Id;
      result["Model"] = srcObject.Model;
      result["Name"] = srcObject.Name;
      result["Params"] = srcObject.Params;

      return result;
    }

    public static Hashtable MNVItemsProviderPlayerVItemInfoToDictionary (MNVItemsProvider.PlayerVItemInfo srcObject) {
      Hashtable result = new Hashtable(2);

      result["Count"] = srcObject.Count;
      result["Id"] = srcObject.Id;

      return result;
    }

    public static Hashtable MNVItemsProviderTransactionVItemInfoToDictionary (MNVItemsProvider.TransactionVItemInfo srcObject) {
      Hashtable result = new Hashtable(2);

      result["Delta"] = srcObject.Delta;
      result["Id"] = srcObject.Id;

      return result;
    }

    public static Hashtable MNVItemsProviderTransactionInfoToDictionary (MNVItemsProvider.TransactionInfo srcObject) {
      Hashtable result = new Hashtable(4);

      result["ClientTransactionId"] = srcObject.ClientTransactionId;
      result["CorrUserId"] = srcObject.CorrUserId;
      result["ServerTransactionId"] = srcObject.ServerTransactionId;

      Hashtable[] dictionaryArray = new Hashtable[srcObject.VItems.Length];

      for (int index = 0;index < srcObject.VItems.Length;index++) {
        dictionaryArray[index] = MNVItemsProviderTransactionVItemInfoToDictionary(srcObject.VItems[index]);
      }

      result["VItems"] = dictionaryArray;

      return result;
    }

    public static Hashtable MNVItemsProviderTransactionErrorToDictionary (MNVItemsProvider.TransactionError srcObject) {
      Hashtable result = new Hashtable(5);

      result["ClientTransactionId"] = srcObject.ClientTransactionId;
      result["CorrUserId"] = srcObject.CorrUserId;
      result["ErrorMessage"] = srcObject.ErrorMessage;
      result["FailReasonCode"] = srcObject.FailReasonCode;
      result["ServerTransactionId"] = srcObject.ServerTransactionId;

      return result;
    }

    public static Hashtable MNVShopProviderVShopDeliveryInfoToDictionary (MNVShopProvider.VShopDeliveryInfo srcObject) {
      Hashtable result = new Hashtable(2);

      result["Amount"] = srcObject.Amount;
      result["VItemId"] = srcObject.VItemId;

      return result;
    }

    public static Hashtable MNVShopProviderVShopPackInfoToDictionary (MNVShopProvider.VShopPackInfo srcObject) {
      Hashtable result = new Hashtable(10);

      result["AppParams"] = srcObject.AppParams;
      result["CategoryId"] = srcObject.CategoryId;

      Hashtable[] dictionaryArray = new Hashtable[srcObject.Delivery.Length];

      for (int index = 0;index < srcObject.Delivery.Length;index++) {
        dictionaryArray[index] = MNVShopProviderVShopDeliveryInfoToDictionary(srcObject.Delivery[index]);
      }

      result["Delivery"] = dictionaryArray;
      result["Description"] = srcObject.Description;
      result["Id"] = srcObject.Id;
      result["Model"] = srcObject.Model;
      result["Name"] = srcObject.Name;
      result["PriceItemId"] = srcObject.PriceItemId;
      result["PriceValue"] = srcObject.PriceValue;
      result["SortPos"] = srcObject.SortPos;

      return result;
    }

    public static Hashtable MNVShopProviderVShopCategoryInfoToDictionary (MNVShopProvider.VShopCategoryInfo srcObject) {
      Hashtable result = new Hashtable(3);

      result["Id"] = srcObject.Id;
      result["Name"] = srcObject.Name;
      result["SortPos"] = srcObject.SortPos;

      return result;
    }

    public static Hashtable MNVShopProviderVShopPackBuyRequestItemToDictionary (MNVShopProvider.VShopPackBuyRequestItem srcObject) {
      Hashtable result = new Hashtable(2);

      result["Amount"] = srcObject.Amount;
      result["Id"] = srcObject.Id;

      return result;
    }

    public static Hashtable MNScoreProgressProviderScoreItemToDictionary (MNScoreProgressProvider.ScoreItem srcObject) {
      Hashtable result = new Hashtable(3);

      result["Place"] = srcObject.Place;
      result["Score"] = srcObject.Score;
      result["UserInfo"] = MNUserInfoToDictionary(srcObject.UserInfo);

      return result;
    }

    public static Hashtable MNBuddyRoomParamsToDictionary (MNBuddyRoomParams srcObject) {
      Hashtable result = new Hashtable(5);

      result["GameSetId"] = srcObject.GameSetId;
      result["InviteText"] = srcObject.InviteText;
      result["RoomName"] = srcObject.RoomName;
      result["ToUserIdList"] = srcObject.ToUserIdList;
      result["ToUserSFIdList"] = srcObject.ToUserSFIdList;

      return result;
    }

    public static Hashtable MNJoinRoomInvitationParamsToDictionary (MNJoinRoomInvitationParams srcObject) {
      Hashtable result = new Hashtable(7);

      result["FromUserName"] = srcObject.FromUserName;
      result["FromUserSFId"] = srcObject.FromUserSFId;
      result["InviteText"] = srcObject.InviteText;
      result["RoomGameId"] = srcObject.RoomGameId;
      result["RoomGameSetId"] = srcObject.RoomGameSetId;
      result["RoomName"] = srcObject.RoomName;
      result["RoomSFId"] = srcObject.RoomSFId;

      return result;
    }

    public static Hashtable MNVShopProviderCheckoutVShopPackSuccessInfoToDictionary (MNVShopProvider.CheckoutVShopPackSuccessInfo srcObject) {
      Hashtable result = new Hashtable(1);

      result["Transaction"] = MNVItemsProviderTransactionInfoToDictionary(srcObject.Transaction);

      return result;
    }

    public static Hashtable MNVShopProviderCheckoutVShopPackFailInfoToDictionary (MNVShopProvider.CheckoutVShopPackFailInfo srcObject) {
      Hashtable result = new Hashtable(3);

      result["ClientTransactionId"] = srcObject.ClientTransactionId;
      result["ErrorCode"] = srcObject.ErrorCode;
      result["ErrorMessage"] = srcObject.ErrorMessage;

      return result;
    }

    public static Hashtable MNGameSettingsProviderGameSettingInfoToDictionary (MNGameSettingsProvider.GameSettingInfo srcObject) {
      Hashtable result = new Hashtable(4);

      result["Id"] = srcObject.Id;
      result["Name"] = srcObject.Name;
      result["Params"] = srcObject.Params;
      result["SysParams"] = srcObject.SysParams;

      return result;
    }

    public static Hashtable MNWSAnyGameItemToDictionary (MNWSAnyGameItem srcObject) {
      Hashtable result = new Hashtable(9);

      result["DeveloperId"] = srcObject.DeveloperId;
      result["GameDesc"] = srcObject.GameDesc;
      result["GameFlags"] = srcObject.GameFlags;
      result["GameGenreId"] = srcObject.GameGenreId;
      result["GameIconUrl"] = srcObject.GameIconUrl;
      result["GameId"] = srcObject.GameId;
      result["GameName"] = srcObject.GameName;
      result["GamePlayModel"] = srcObject.GamePlayModel;
      result["GameStatus"] = srcObject.GameStatus;

      return result;
    }

    public static Hashtable MNWSInfoRequestAnyGameRequestResultToDictionary (MNWSInfoRequestAnyGame.RequestResult srcObject) {
      Hashtable result = new Hashtable(1);

      result["DataEntry"] = MNWSAnyGameItemToDictionary(srcObject.DataEntry);

      return result;
    }

    public static Hashtable MNWSAnyUserItemToDictionary (MNWSAnyUserItem srcObject) {
      Hashtable result = new Hashtable(7);

      result["MyFriendLinkStatus"] = srcObject.MyFriendLinkStatus;
      result["UserAvatarExists"] = srcObject.UserAvatarExists;
      result["UserAvatarUrl"] = srcObject.UserAvatarUrl;
      result["UserGamePoints"] = srcObject.UserGamePoints;
      result["UserId"] = srcObject.UserId;
      result["UserNickName"] = srcObject.UserNickName;
      result["UserOnlineNow"] = srcObject.UserOnlineNow;

      return result;
    }

    public static Hashtable MNWSInfoRequestAnyUserRequestResultToDictionary (MNWSInfoRequestAnyUser.RequestResult srcObject) {
      Hashtable result = new Hashtable(1);

      result["DataEntry"] = MNWSAnyUserItemToDictionary(srcObject.DataEntry);

      return result;
    }

    public static Hashtable MNWSUserGameCookieToDictionary (MNWSUserGameCookie srcObject) {
      Hashtable result = new Hashtable(3);

      result["CookieKey"] = srcObject.CookieKey;
      result["CookieValue"] = srcObject.CookieValue;
      result["UserId"] = srcObject.UserId;

      return result;
    }

    public static Hashtable MNWSInfoRequestAnyUserGameCookiesRequestResultToDictionary (MNWSInfoRequestAnyUserGameCookies.RequestResult srcObject) {
      Hashtable result = new Hashtable(1);


      Hashtable[] dictionaryArray = new Hashtable[srcObject.DataEntry.Length];

      for (int index = 0;index < srcObject.DataEntry.Length;index++) {
        dictionaryArray[index] = MNWSUserGameCookieToDictionary(srcObject.DataEntry[index]);
      }

      result["DataEntry"] = dictionaryArray;

      return result;
    }

    public static Hashtable MNWSCurrentUserInfoToDictionary (MNWSCurrentUserInfo srcObject) {
      Hashtable result = new Hashtable(10);

      result["UserAvatarExists"] = srcObject.UserAvatarExists;
      result["UserAvatarHasCustomImg"] = srcObject.UserAvatarHasCustomImg;
      result["UserAvatarHasExternalUrl"] = srcObject.UserAvatarHasExternalUrl;
      result["UserAvatarUrl"] = srcObject.UserAvatarUrl;
      result["UserEmail"] = srcObject.UserEmail;
      result["UserGamePoints"] = srcObject.UserGamePoints;
      result["UserId"] = srcObject.UserId;
      result["UserNickName"] = srcObject.UserNickName;
      result["UserOnlineNow"] = srcObject.UserOnlineNow;
      result["UserStatus"] = srcObject.UserStatus;

      return result;
    }

    public static Hashtable MNWSInfoRequestCurrentUserInfoRequestResultToDictionary (MNWSInfoRequestCurrentUserInfo.RequestResult srcObject) {
      Hashtable result = new Hashtable(1);

      result["DataEntry"] = MNWSCurrentUserInfoToDictionary(srcObject.DataEntry);

      return result;
    }

    public static Hashtable MNWSRoomListItemToDictionary (MNWSRoomListItem srcObject) {
      Hashtable result = new Hashtable(6);

      result["GameId"] = srcObject.GameId;
      result["GameSetId"] = srcObject.GameSetId;
      result["RoomIsLobby"] = srcObject.RoomIsLobby;
      result["RoomName"] = srcObject.RoomName;
      result["RoomSFId"] = srcObject.RoomSFId;
      result["RoomUserCount"] = srcObject.RoomUserCount;

      return result;
    }

    public static Hashtable MNWSInfoRequestCurrGameRoomListRequestResultToDictionary (MNWSInfoRequestCurrGameRoomList.RequestResult srcObject) {
      Hashtable result = new Hashtable(1);


      Hashtable[] dictionaryArray = new Hashtable[srcObject.DataEntry.Length];

      for (int index = 0;index < srcObject.DataEntry.Length;index++) {
        dictionaryArray[index] = MNWSRoomListItemToDictionary(srcObject.DataEntry[index]);
      }

      result["DataEntry"] = dictionaryArray;

      return result;
    }

    public static Hashtable MNWSRoomUserInfoItemToDictionary (MNWSRoomUserInfoItem srcObject) {
      Hashtable result = new Hashtable(6);

      result["RoomSFId"] = srcObject.RoomSFId;
      result["UserAvatarExists"] = srcObject.UserAvatarExists;
      result["UserAvatarUrl"] = srcObject.UserAvatarUrl;
      result["UserId"] = srcObject.UserId;
      result["UserNickName"] = srcObject.UserNickName;
      result["UserOnlineNow"] = srcObject.UserOnlineNow;

      return result;
    }

    public static Hashtable MNWSInfoRequestCurrGameRoomUserListRequestResultToDictionary (MNWSInfoRequestCurrGameRoomUserList.RequestResult srcObject) {
      Hashtable result = new Hashtable(1);


      Hashtable[] dictionaryArray = new Hashtable[srcObject.DataEntry.Length];

      for (int index = 0;index < srcObject.DataEntry.Length;index++) {
        dictionaryArray[index] = MNWSRoomUserInfoItemToDictionary(srcObject.DataEntry[index]);
      }

      result["DataEntry"] = dictionaryArray;

      return result;
    }

    public static Hashtable MNWSBuddyListItemToDictionary (MNWSBuddyListItem srcObject) {
      Hashtable result = new Hashtable(19);

      result["FriendCurrGameAchievementsList"] = srcObject.FriendCurrGameAchievementsList;
      result["FriendFlags"] = srcObject.FriendFlags;
      result["FriendHasCurrentGame"] = srcObject.FriendHasCurrentGame;
      result["FriendInGameIconUrl"] = srcObject.FriendInGameIconUrl;
      result["FriendInGameId"] = srcObject.FriendInGameId;
      result["FriendInGameName"] = srcObject.FriendInGameName;
      result["FriendInRoomIsLobby"] = srcObject.FriendInRoomIsLobby;
      result["FriendInRoomSfid"] = srcObject.FriendInRoomSfid;
      result["FriendIsIgnored"] = srcObject.FriendIsIgnored;
      result["FriendSnId"] = srcObject.FriendSnId;
      result["FriendSnIdList"] = srcObject.FriendSnIdList;
      result["FriendSnUserAsnId"] = srcObject.FriendSnUserAsnId;
      result["FriendSnUserAsnIdList"] = srcObject.FriendSnUserAsnIdList;
      result["FriendUserAvatarUrl"] = srcObject.FriendUserAvatarUrl;
      result["FriendUserId"] = srcObject.FriendUserId;
      result["FriendUserLocale"] = srcObject.FriendUserLocale;
      result["FriendUserNickName"] = srcObject.FriendUserNickName;
      result["FriendUserOnlineNow"] = srcObject.FriendUserOnlineNow;
      result["FriendUserSfid"] = srcObject.FriendUserSfid;

      return result;
    }

    public static Hashtable MNWSInfoRequestCurrUserBuddyListRequestResultToDictionary (MNWSInfoRequestCurrUserBuddyList.RequestResult srcObject) {
      Hashtable result = new Hashtable(1);


      Hashtable[] dictionaryArray = new Hashtable[srcObject.DataEntry.Length];

      for (int index = 0;index < srcObject.DataEntry.Length;index++) {
        dictionaryArray[index] = MNWSBuddyListItemToDictionary(srcObject.DataEntry[index]);
      }

      result["DataEntry"] = dictionaryArray;

      return result;
    }

    public static Hashtable MNWSCurrUserSubscriptionStatusToDictionary (MNWSCurrUserSubscriptionStatus srcObject) {
      Hashtable result = new Hashtable(3);

      result["HasSubscription"] = srcObject.HasSubscription;
      result["IsSubscriptionAvailable"] = srcObject.IsSubscriptionAvailable;
      result["OffersAvailable"] = srcObject.OffersAvailable;

      return result;
    }

    public static Hashtable MNWSInfoRequestCurrUserSubscriptionStatusRequestResultToDictionary (MNWSInfoRequestCurrUserSubscriptionStatus.RequestResult srcObject) {
      Hashtable result = new Hashtable(1);

      result["DataEntry"] = MNWSCurrUserSubscriptionStatusToDictionary(srcObject.DataEntry);

      return result;
    }

    public static Hashtable MNWSSessionSignedClientTokenToDictionary (MNWSSessionSignedClientToken srcObject) {
      Hashtable result = new Hashtable(2);

      result["ClientTokenBody"] = srcObject.ClientTokenBody;
      result["ClientTokenSign"] = srcObject.ClientTokenSign;

      return result;
    }

    public static Hashtable MNWSInfoRequestSessionSignedClientTokenRequestResultToDictionary (MNWSInfoRequestSessionSignedClientToken.RequestResult srcObject) {
      Hashtable result = new Hashtable(1);

      result["DataEntry"] = MNWSSessionSignedClientTokenToDictionary(srcObject.DataEntry);

      return result;
    }

    public static Hashtable MNWSSystemGameNetStatsToDictionary (MNWSSystemGameNetStats srcObject) {
      Hashtable result = new Hashtable(7);

      result["GameOnlineRooms"] = srcObject.GameOnlineRooms;
      result["GameOnlineUsers"] = srcObject.GameOnlineUsers;
      result["ServOnlineGames"] = srcObject.ServOnlineGames;
      result["ServOnlineRooms"] = srcObject.ServOnlineRooms;
      result["ServOnlineUsers"] = srcObject.ServOnlineUsers;
      result["ServTotalGames"] = srcObject.ServTotalGames;
      result["ServTotalUsers"] = srcObject.ServTotalUsers;

      return result;
    }

    public static Hashtable MNWSInfoRequestSystemGameNetStatsRequestResultToDictionary (MNWSInfoRequestSystemGameNetStats.RequestResult srcObject) {
      Hashtable result = new Hashtable(1);

      result["DataEntry"] = MNWSSystemGameNetStatsToDictionary(srcObject.DataEntry);

      return result;
    }

    public static Hashtable MNWSLeaderboardListItemToDictionary (MNWSLeaderboardListItem srcObject) {
      Hashtable result = new Hashtable(16);

      result["GameId"] = srcObject.GameId;
      result["GamesetId"] = srcObject.GamesetId;
      result["OutHiDateTime"] = srcObject.OutHiDateTime;
      result["OutHiDateTimeDiff"] = srcObject.OutHiDateTimeDiff;
      result["OutHiScore"] = srcObject.OutHiScore;
      result["OutHiScoreText"] = srcObject.OutHiScoreText;
      result["OutUserPlace"] = srcObject.OutUserPlace;
      result["UserAchievementsList"] = srcObject.UserAchievementsList;
      result["UserAvatarUrl"] = srcObject.UserAvatarUrl;
      result["UserId"] = srcObject.UserId;
      result["UserIsFriend"] = srcObject.UserIsFriend;
      result["UserIsIgnored"] = srcObject.UserIsIgnored;
      result["UserLocale"] = srcObject.UserLocale;
      result["UserNickName"] = srcObject.UserNickName;
      result["UserOnlineNow"] = srcObject.UserOnlineNow;
      result["UserSfid"] = srcObject.UserSfid;

      return result;
    }

    public static Hashtable MNWSInfoRequestLeaderboardRequestResultToDictionary (MNWSInfoRequestLeaderboard.RequestResult srcObject) {
      Hashtable result = new Hashtable(1);


      Hashtable[] dictionaryArray = new Hashtable[srcObject.DataEntry.Length];

      for (int index = 0;index < srcObject.DataEntry.Length;index++) {
        dictionaryArray[index] = MNWSLeaderboardListItemToDictionary(srcObject.DataEntry[index]);
      }

      result["DataEntry"] = dictionaryArray;

      return result;
    }

    public static Hashtable MNWSInfoRequestToDictionary (MNWSInfoRequest srcObject) {
      Hashtable result = new Hashtable(3);

      result["Id"] = srcObject.Id;
      result["Name"] = srcObject.Name;
      result["Parameters"] = srcObject.Parameters;

      return result;
    }
  }
}