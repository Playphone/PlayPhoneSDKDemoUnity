using System;
using System.Collections;
using System.Collections.Generic;
using PlayPhone.MultiNet.Providers;

namespace PlayPhone.MultiNet.Core
{
  public class MNSerializerMapper
  {
    public static object ObjectFromDictionary(IDictionary deserializedObject,Type targetType) {
      MNTools.DLog(string.Format("ObjectFromDictionary. ObjectType={0}",MNTools.SafeToString(targetType)),MNTools.DEBUG_LEVEL_DETAILED);
      object result = null;

      if (targetType.Equals(typeof(MNErrorInfo))) {
        result = MNErrorInfoFromDictionary(deserializedObject);
      }
      else if (targetType.Equals(typeof(MNGameParams))) {
        result = MNGameParamsFromDictionary(deserializedObject);
      }
      else if (targetType.Equals(typeof(MNUserInfo))) {
        result = MNUserInfoFromDictionary(deserializedObject);
      }
      else if (targetType.Equals(typeof(MNAchievementsProvider.GameAchievementInfo))) {
        result = MNAchievementsProviderGameAchievementInfoFromDictionary(deserializedObject);
      }
      else if (targetType.Equals(typeof(MNAchievementsProvider.PlayerAchievementInfo))) {
        result = MNAchievementsProviderPlayerAchievementInfoFromDictionary(deserializedObject);
      }
      else if (targetType.Equals(typeof(MNVItemsProvider.GameVItemInfo))) {
        result = MNVItemsProviderGameVItemInfoFromDictionary(deserializedObject);
      }
      else if (targetType.Equals(typeof(MNVItemsProvider.PlayerVItemInfo))) {
        result = MNVItemsProviderPlayerVItemInfoFromDictionary(deserializedObject);
      }
      else if (targetType.Equals(typeof(MNVItemsProvider.TransactionVItemInfo))) {
        result = MNVItemsProviderTransactionVItemInfoFromDictionary(deserializedObject);
      }
      else if (targetType.Equals(typeof(MNVItemsProvider.TransactionInfo))) {
        result = MNVItemsProviderTransactionInfoFromDictionary(deserializedObject);
      }
      else if (targetType.Equals(typeof(MNVItemsProvider.TransactionError))) {
        result = MNVItemsProviderTransactionErrorFromDictionary(deserializedObject);
      }
      else if (targetType.Equals(typeof(MNVShopProvider.VShopDeliveryInfo))) {
        result = MNVShopProviderVShopDeliveryInfoFromDictionary(deserializedObject);
      }
      else if (targetType.Equals(typeof(MNVShopProvider.VShopPackInfo))) {
        result = MNVShopProviderVShopPackInfoFromDictionary(deserializedObject);
      }
      else if (targetType.Equals(typeof(MNVShopProvider.VShopCategoryInfo))) {
        result = MNVShopProviderVShopCategoryInfoFromDictionary(deserializedObject);
      }
      else if (targetType.Equals(typeof(MNVShopProvider.VShopPackBuyRequestItem))) {
        result = MNVShopProviderVShopPackBuyRequestItemFromDictionary(deserializedObject);
      }
      else if (targetType.Equals(typeof(MNScoreProgressProvider.ScoreItem))) {
        result = MNScoreProgressProviderScoreItemFromDictionary(deserializedObject);
      }
      else if (targetType.Equals(typeof(MNBuddyRoomParams))) {
        result = MNBuddyRoomParamsFromDictionary(deserializedObject);
      }
      else if (targetType.Equals(typeof(MNJoinRoomInvitationParams))) {
        result = MNJoinRoomInvitationParamsFromDictionary(deserializedObject);
      }
      else if (targetType.Equals(typeof(MNVShopProvider.CheckoutVShopPackSuccessInfo))) {
        result = MNVShopProviderCheckoutVShopPackSuccessInfoFromDictionary(deserializedObject);
      }
      else if (targetType.Equals(typeof(MNVShopProvider.CheckoutVShopPackFailInfo))) {
        result = MNVShopProviderCheckoutVShopPackFailInfoFromDictionary(deserializedObject);
      }
      else if (targetType.Equals(typeof(MNGameSettingsProvider.GameSettingInfo))) {
        result = MNGameSettingsProviderGameSettingInfoFromDictionary(deserializedObject);
      }
      else if (targetType.Equals(typeof(MNWSAnyGameItem))) {
        result = MNWSAnyGameItemFromDictionary(deserializedObject);
      }
      else if (targetType.Equals(typeof(MNWSInfoRequestAnyGame.RequestResult))) {
        result = MNWSInfoRequestAnyGameRequestResultFromDictionary(deserializedObject);
      }
      else if (targetType.Equals(typeof(MNWSAnyUserItem))) {
        result = MNWSAnyUserItemFromDictionary(deserializedObject);
      }
      else if (targetType.Equals(typeof(MNWSInfoRequestAnyUser.RequestResult))) {
        result = MNWSInfoRequestAnyUserRequestResultFromDictionary(deserializedObject);
      }
      else if (targetType.Equals(typeof(MNWSUserGameCookie))) {
        result = MNWSUserGameCookieFromDictionary(deserializedObject);
      }
      else if (targetType.Equals(typeof(MNWSInfoRequestAnyUserGameCookies.RequestResult))) {
        result = MNWSInfoRequestAnyUserGameCookiesRequestResultFromDictionary(deserializedObject);
      }
      else if (targetType.Equals(typeof(MNWSCurrentUserInfo))) {
        result = MNWSCurrentUserInfoFromDictionary(deserializedObject);
      }
      else if (targetType.Equals(typeof(MNWSInfoRequestCurrentUserInfo.RequestResult))) {
        result = MNWSInfoRequestCurrentUserInfoRequestResultFromDictionary(deserializedObject);
      }
      else if (targetType.Equals(typeof(MNWSRoomListItem))) {
        result = MNWSRoomListItemFromDictionary(deserializedObject);
      }
      else if (targetType.Equals(typeof(MNWSInfoRequestCurrGameRoomList.RequestResult))) {
        result = MNWSInfoRequestCurrGameRoomListRequestResultFromDictionary(deserializedObject);
      }
      else if (targetType.Equals(typeof(MNWSRoomUserInfoItem))) {
        result = MNWSRoomUserInfoItemFromDictionary(deserializedObject);
      }
      else if (targetType.Equals(typeof(MNWSInfoRequestCurrGameRoomUserList.RequestResult))) {
        result = MNWSInfoRequestCurrGameRoomUserListRequestResultFromDictionary(deserializedObject);
      }
      else if (targetType.Equals(typeof(MNWSBuddyListItem))) {
        result = MNWSBuddyListItemFromDictionary(deserializedObject);
      }
      else if (targetType.Equals(typeof(MNWSInfoRequestCurrUserBuddyList.RequestResult))) {
        result = MNWSInfoRequestCurrUserBuddyListRequestResultFromDictionary(deserializedObject);
      }
      else if (targetType.Equals(typeof(MNWSCurrUserSubscriptionStatus))) {
        result = MNWSCurrUserSubscriptionStatusFromDictionary(deserializedObject);
      }
      else if (targetType.Equals(typeof(MNWSInfoRequestCurrUserSubscriptionStatus.RequestResult))) {
        result = MNWSInfoRequestCurrUserSubscriptionStatusRequestResultFromDictionary(deserializedObject);
      }
      else if (targetType.Equals(typeof(MNWSSessionSignedClientToken))) {
        result = MNWSSessionSignedClientTokenFromDictionary(deserializedObject);
      }
      else if (targetType.Equals(typeof(MNWSInfoRequestSessionSignedClientToken.RequestResult))) {
        result = MNWSInfoRequestSessionSignedClientTokenRequestResultFromDictionary(deserializedObject);
      }
      else if (targetType.Equals(typeof(MNWSSystemGameNetStats))) {
        result = MNWSSystemGameNetStatsFromDictionary(deserializedObject);
      }
      else if (targetType.Equals(typeof(MNWSInfoRequestSystemGameNetStats.RequestResult))) {
        result = MNWSInfoRequestSystemGameNetStatsRequestResultFromDictionary(deserializedObject);
      }
      else if (targetType.Equals(typeof(MNWSLeaderboardListItem))) {
        result = MNWSLeaderboardListItemFromDictionary(deserializedObject);
      }
      else if (targetType.Equals(typeof(MNWSInfoRequestLeaderboard.RequestResult))) {
        result = MNWSInfoRequestLeaderboardRequestResultFromDictionary(deserializedObject);
      }
      else {
        throw new MNDeserializationException(deserializedObject,targetType);
      }

      return result;
    }

    public static List<object> ListOfObjectsFromListOfDictionaries(List<object> dictionariesList,Type targetObjectsType) {
      List<object> resultList = new List<object>(dictionariesList.Count);

      for (int index = 0;index < dictionariesList.Count;index++) {
        resultList.Add(ObjectFromDictionary((IDictionary)dictionariesList[index],targetObjectsType));
      }

      return resultList;
    }

    public static MNErrorInfo MNErrorInfoFromDictionary(IDictionary deserializedObject) {
      if (deserializedObject == null) {
        return null;
      }

      MNErrorInfo typedResult = new MNErrorInfo();

      typedResult.ActionCode = Convert.ToInt32(deserializedObject["ActionCode"]);
      typedResult.ErrorMessage = (string)deserializedObject["ErrorMessage"];

      return typedResult;
    }

    public static MNGameParams MNGameParamsFromDictionary(IDictionary deserializedObject) {
      if (deserializedObject == null) {
        return null;
      }

      MNGameParams typedResult = new MNGameParams();

      typedResult.GameSeed = Convert.ToInt32(deserializedObject["GameSeed"]);
      typedResult.GameSetId = Convert.ToInt32(deserializedObject["GameSetId"]);
      typedResult.GameSetParams = (string)deserializedObject["GameSetParams"];
      typedResult.PlayModel = Convert.ToInt32(deserializedObject["PlayModel"]);
      typedResult.ScorePostLinkId = (string)deserializedObject["ScorePostLinkId"];

      return typedResult;
    }

    public static MNUserInfo MNUserInfoFromDictionary(IDictionary deserializedObject) {
      if (deserializedObject == null) {
        return null;
      }

      MNUserInfo typedResult = new MNUserInfo();

      typedResult.UserId = Convert.ToInt64(deserializedObject["UserId"]);
      typedResult.UserName = (string)deserializedObject["UserName"];
      typedResult.UserSFId = Convert.ToInt32(deserializedObject["UserSFId"]);
      typedResult.UserAvatarUrl = (string)deserializedObject["UserAvatarUrl"];

      return typedResult;
    }

    public static MNAchievementsProvider.GameAchievementInfo MNAchievementsProviderGameAchievementInfoFromDictionary(IDictionary deserializedObject) {
      if (deserializedObject == null) {
        return null;
      }

      MNAchievementsProvider.GameAchievementInfo typedResult = new MNAchievementsProvider.GameAchievementInfo();

      typedResult.Description = (string)deserializedObject["Description"];
      typedResult.Flags = Convert.ToInt32(deserializedObject["Flags"]);
      typedResult.Id = Convert.ToInt32(deserializedObject["Id"]);
      typedResult.Name = (string)deserializedObject["Name"];
      typedResult.Params = (string)deserializedObject["Params"];
      typedResult.Points = Convert.ToInt32(deserializedObject["Points"]);

      return typedResult;
    }

    public static MNAchievementsProvider.PlayerAchievementInfo MNAchievementsProviderPlayerAchievementInfoFromDictionary(IDictionary deserializedObject) {
      if (deserializedObject == null) {
        return null;
      }

      MNAchievementsProvider.PlayerAchievementInfo typedResult = new MNAchievementsProvider.PlayerAchievementInfo();

      typedResult.Id = Convert.ToInt32(deserializedObject["Id"]);

      return typedResult;
    }

    public static MNVItemsProvider.GameVItemInfo MNVItemsProviderGameVItemInfoFromDictionary(IDictionary deserializedObject) {
      if (deserializedObject == null) {
        return null;
      }

      MNVItemsProvider.GameVItemInfo typedResult = new MNVItemsProvider.GameVItemInfo();

      typedResult.Description = (string)deserializedObject["Description"];
      typedResult.Id = Convert.ToInt32(deserializedObject["Id"]);
      typedResult.Model = Convert.ToInt32(deserializedObject["Model"]);
      typedResult.Name = (string)deserializedObject["Name"];
      typedResult.Params = (string)deserializedObject["Params"];

      return typedResult;
    }

    public static MNVItemsProvider.PlayerVItemInfo MNVItemsProviderPlayerVItemInfoFromDictionary(IDictionary deserializedObject) {
      if (deserializedObject == null) {
        return null;
      }

      MNVItemsProvider.PlayerVItemInfo typedResult = new MNVItemsProvider.PlayerVItemInfo();

      typedResult.Count = Convert.ToInt64(deserializedObject["Count"]);
      typedResult.Id = Convert.ToInt32(deserializedObject["Id"]);

      return typedResult;
    }

    public static MNVItemsProvider.TransactionVItemInfo MNVItemsProviderTransactionVItemInfoFromDictionary(IDictionary deserializedObject) {
      if (deserializedObject == null) {
        return null;
      }

      MNVItemsProvider.TransactionVItemInfo typedResult = new MNVItemsProvider.TransactionVItemInfo();

      typedResult.Delta = Convert.ToInt64(deserializedObject["Delta"]);
      typedResult.Id = Convert.ToInt32(deserializedObject["Id"]);

      return typedResult;
    }

    public static MNVItemsProvider.TransactionInfo MNVItemsProviderTransactionInfoFromDictionary(IDictionary deserializedObject) {
      if (deserializedObject == null) {
        return null;
      }

      MNVItemsProvider.TransactionInfo typedResult = new MNVItemsProvider.TransactionInfo();

      typedResult.ClientTransactionId = Convert.ToInt64(deserializedObject["ClientTransactionId"]);
      typedResult.CorrUserId = Convert.ToInt64(deserializedObject["CorrUserId"]);
      typedResult.ServerTransactionId = Convert.ToInt64(deserializedObject["ServerTransactionId"]);

      List<object> deserializedItems = (List<object>)deserializedObject["VItems"];
      MNVItemsProvider.TransactionVItemInfo[] items = null;

      if (deserializedItems != null) {
        items = new MNVItemsProvider.TransactionVItemInfo[deserializedItems.Count];

        for (int index = 0;index < deserializedItems.Count;index++) {
          items[index] = MNVItemsProviderTransactionVItemInfoFromDictionary((IDictionary)deserializedItems[index]);
        }
      }

      typedResult.VItems = items;


      return typedResult;
    }

    public static MNVItemsProvider.TransactionError MNVItemsProviderTransactionErrorFromDictionary(IDictionary deserializedObject) {
      if (deserializedObject == null) {
        return null;
      }

      MNVItemsProvider.TransactionError typedResult = new MNVItemsProvider.TransactionError();

      typedResult.ClientTransactionId = Convert.ToInt64(deserializedObject["ClientTransactionId"]);
      typedResult.CorrUserId = Convert.ToInt64(deserializedObject["CorrUserId"]);
      typedResult.ErrorMessage = (string)deserializedObject["ErrorMessage"];
      typedResult.FailReasonCode = Convert.ToInt32(deserializedObject["FailReasonCode"]);
      typedResult.ServerTransactionId = Convert.ToInt64(deserializedObject["ServerTransactionId"]);

      return typedResult;
    }

    public static MNVShopProvider.VShopDeliveryInfo MNVShopProviderVShopDeliveryInfoFromDictionary(IDictionary deserializedObject) {
      if (deserializedObject == null) {
        return null;
      }

      MNVShopProvider.VShopDeliveryInfo typedResult = new MNVShopProvider.VShopDeliveryInfo();

      typedResult.Amount = Convert.ToInt64(deserializedObject["Amount"]);
      typedResult.VItemId = Convert.ToInt32(deserializedObject["VItemId"]);

      return typedResult;
    }

    public static MNVShopProvider.VShopPackInfo MNVShopProviderVShopPackInfoFromDictionary(IDictionary deserializedObject) {
      if (deserializedObject == null) {
        return null;
      }

      MNVShopProvider.VShopPackInfo typedResult = new MNVShopProvider.VShopPackInfo();

      typedResult.AppParams = (string)deserializedObject["AppParams"];
      typedResult.CategoryId = Convert.ToInt32(deserializedObject["CategoryId"]);

      List<object> deserializedItems = (List<object>)deserializedObject["Delivery"];
      MNVShopProvider.VShopDeliveryInfo[] items = null;

      if (deserializedItems != null) {
        items = new MNVShopProvider.VShopDeliveryInfo[deserializedItems.Count];

        for (int index = 0;index < deserializedItems.Count;index++) {
          items[index] = MNVShopProviderVShopDeliveryInfoFromDictionary((IDictionary)deserializedItems[index]);
        }
      }

      typedResult.Delivery = items;

      typedResult.Description = (string)deserializedObject["Description"];
      typedResult.Id = Convert.ToInt32(deserializedObject["Id"]);
      typedResult.Model = Convert.ToInt32(deserializedObject["Model"]);
      typedResult.Name = (string)deserializedObject["Name"];
      typedResult.PriceItemId = Convert.ToInt32(deserializedObject["PriceItemId"]);
      typedResult.PriceValue = Convert.ToInt64(deserializedObject["PriceValue"]);
      typedResult.SortPos = Convert.ToInt32(deserializedObject["SortPos"]);

      return typedResult;
    }

    public static MNVShopProvider.VShopCategoryInfo MNVShopProviderVShopCategoryInfoFromDictionary(IDictionary deserializedObject) {
      if (deserializedObject == null) {
        return null;
      }

      MNVShopProvider.VShopCategoryInfo typedResult = new MNVShopProvider.VShopCategoryInfo();

      typedResult.Id = Convert.ToInt32(deserializedObject["Id"]);
      typedResult.Name = (string)deserializedObject["Name"];
      typedResult.SortPos = Convert.ToInt32(deserializedObject["SortPos"]);

      return typedResult;
    }

    public static MNVShopProvider.VShopPackBuyRequestItem MNVShopProviderVShopPackBuyRequestItemFromDictionary(IDictionary deserializedObject) {
      if (deserializedObject == null) {
        return null;
      }

      MNVShopProvider.VShopPackBuyRequestItem typedResult = new MNVShopProvider.VShopPackBuyRequestItem();

      typedResult.Amount = Convert.ToInt64(deserializedObject["Amount"]);
      typedResult.Id = Convert.ToInt32(deserializedObject["Id"]);

      return typedResult;
    }

    public static MNScoreProgressProvider.ScoreItem MNScoreProgressProviderScoreItemFromDictionary(IDictionary deserializedObject) {
      if (deserializedObject == null) {
        return null;
      }

      MNScoreProgressProvider.ScoreItem typedResult = new MNScoreProgressProvider.ScoreItem();

      typedResult.Place = Convert.ToInt32(deserializedObject["Place"]);
      typedResult.Score = Convert.ToInt64(deserializedObject["Score"]);
      typedResult.UserInfo = MNUserInfoFromDictionary((IDictionary)deserializedObject["UserInfo"]);

      return typedResult;
    }

    public static MNBuddyRoomParams MNBuddyRoomParamsFromDictionary(IDictionary deserializedObject) {
      if (deserializedObject == null) {
        return null;
      }

      MNBuddyRoomParams typedResult = new MNBuddyRoomParams();

      typedResult.GameSetId = Convert.ToInt32(deserializedObject["GameSetId"]); //GameSetId is int, not int?
      typedResult.InviteText = (string)deserializedObject["InviteText"];
      typedResult.RoomName = (string)deserializedObject["RoomName"];
      typedResult.ToUserIdList = (string)deserializedObject["ToUserIdList"];
      typedResult.ToUserSFIdList = (string)deserializedObject["ToUserSFIdList"];

      return typedResult;
    }

    public static MNJoinRoomInvitationParams MNJoinRoomInvitationParamsFromDictionary(IDictionary deserializedObject) {
      if (deserializedObject == null) {
        return null;
      }

      MNJoinRoomInvitationParams typedResult = new MNJoinRoomInvitationParams();

      typedResult.FromUserName = (string)deserializedObject["FromUserName"];
      typedResult.FromUserSFId = Convert.ToInt32(deserializedObject["FromUserSFId"]);
      typedResult.InviteText = (string)deserializedObject["InviteText"];
      typedResult.RoomGameId = Convert.ToInt32(deserializedObject["RoomGameId"]);
      typedResult.RoomGameSetId = Convert.ToInt32(deserializedObject["RoomGameSetId"]);
      typedResult.RoomName = (string)deserializedObject["RoomName"];
      typedResult.RoomSFId = Convert.ToInt32(deserializedObject["RoomSFId"]);

      return typedResult;
    }

    public static MNVShopProvider.CheckoutVShopPackSuccessInfo MNVShopProviderCheckoutVShopPackSuccessInfoFromDictionary(IDictionary deserializedObject) {
      if (deserializedObject == null) {
        return null;
      }

      MNVShopProvider.CheckoutVShopPackSuccessInfo typedResult = new MNVShopProvider.CheckoutVShopPackSuccessInfo();

      typedResult.Transaction = MNVItemsProviderTransactionInfoFromDictionary((IDictionary)deserializedObject["Transaction"]);

      return typedResult;
    }

    public static MNVShopProvider.CheckoutVShopPackFailInfo MNVShopProviderCheckoutVShopPackFailInfoFromDictionary(IDictionary deserializedObject) {
      if (deserializedObject == null) {
        return null;
      }

      MNVShopProvider.CheckoutVShopPackFailInfo typedResult = new MNVShopProvider.CheckoutVShopPackFailInfo();

      typedResult.ClientTransactionId = Convert.ToInt64(deserializedObject["ClientTransactionId"]);
      typedResult.ErrorCode = Convert.ToInt32(deserializedObject["ErrorCode"]);
      typedResult.ErrorMessage = (string)deserializedObject["ErrorMessage"];

      return typedResult;
    }

    public static MNGameSettingsProvider.GameSettingInfo MNGameSettingsProviderGameSettingInfoFromDictionary(IDictionary deserializedObject) {
      if (deserializedObject == null) {
        return null;
      }

      MNGameSettingsProvider.GameSettingInfo typedResult = new MNGameSettingsProvider.GameSettingInfo();

      typedResult.Id = Convert.ToInt32(deserializedObject["Id"]);
      typedResult.Name = (string)deserializedObject["Name"];
      typedResult.Params = (string)deserializedObject["Params"];
      typedResult.SysParams = (string)deserializedObject["SysParams"];

      return typedResult;
    }

    public static MNWSAnyGameItem MNWSAnyGameItemFromDictionary(IDictionary deserializedObject) {
      if (deserializedObject == null) {
        return null;
      }

      MNWSAnyGameItem typedResult = new MNWSAnyGameItem();

      typedResult.DeveloperId = ((deserializedObject["DeveloperId"] == null) ? null : (long?)Convert.ToInt64(deserializedObject["DeveloperId"]));
      typedResult.GameDesc = (string)deserializedObject["GameDesc"];
      typedResult.GameFlags = ((deserializedObject["GameFlags"] == null) ? null : (long?)Convert.ToInt64(deserializedObject["GameFlags"]));
      typedResult.GameGenreId = ((deserializedObject["GameGenreId"] == null) ? null : (int?)Convert.ToInt32(deserializedObject["GameGenreId"]));
      typedResult.GameIconUrl = (string)deserializedObject["GameIconUrl"];
      typedResult.GameId = ((deserializedObject["GameId"] == null) ? null : (int?)Convert.ToInt32(deserializedObject["GameId"]));
      typedResult.GameName = (string)deserializedObject["GameName"];
      typedResult.GamePlayModel = ((deserializedObject["GamePlayModel"] == null) ? null : (int?)Convert.ToInt32(deserializedObject["GamePlayModel"]));
      typedResult.GameStatus = ((deserializedObject["GameStatus"] == null) ? null : (int?)Convert.ToInt32(deserializedObject["GameStatus"]));

      return typedResult;
    }

    public static MNWSInfoRequestAnyGame.RequestResult MNWSInfoRequestAnyGameRequestResultFromDictionary(IDictionary deserializedObject) {
      if (deserializedObject == null) {
        return null;
      }

      MNWSInfoRequestAnyGame.RequestResult typedResult = new MNWSInfoRequestAnyGame.RequestResult();

      typedResult.DataEntry = MNWSAnyGameItemFromDictionary((IDictionary)deserializedObject["DataEntry"]);
      typedResult.HadError = Convert.ToBoolean(deserializedObject["HadError"]);
      typedResult.ErrorMessage = (string)deserializedObject["ErrorMessage"];

      return typedResult;
    }

    public static MNWSAnyUserItem MNWSAnyUserItemFromDictionary(IDictionary deserializedObject) {
      if (deserializedObject == null) {
        return null;
      }

      MNWSAnyUserItem typedResult = new MNWSAnyUserItem();

      typedResult.MyFriendLinkStatus = ((deserializedObject["MyFriendLinkStatus"] == null) ? null : (int?)Convert.ToInt32(deserializedObject["MyFriendLinkStatus"]));
      typedResult.UserAvatarExists = ((deserializedObject["UserAvatarExists"] == null) ? null : (bool?)Convert.ToBoolean(deserializedObject["UserAvatarExists"]));
      typedResult.UserAvatarUrl = (string)deserializedObject["UserAvatarUrl"];
      typedResult.UserGamePoints = ((deserializedObject["UserGamePoints"] == null) ? null : (long?)Convert.ToInt64(deserializedObject["UserGamePoints"]));
      typedResult.UserId = ((deserializedObject["UserId"] == null) ? null : (long?)Convert.ToInt64(deserializedObject["UserId"]));
      typedResult.UserNickName = (string)deserializedObject["UserNickName"];
      typedResult.UserOnlineNow = ((deserializedObject["UserOnlineNow"] == null) ? null : (bool?)Convert.ToBoolean(deserializedObject["UserOnlineNow"]));

      return typedResult;
    }

    public static MNWSInfoRequestAnyUser.RequestResult MNWSInfoRequestAnyUserRequestResultFromDictionary(IDictionary deserializedObject) {
      if (deserializedObject == null) {
        return null;
      }

      MNWSInfoRequestAnyUser.RequestResult typedResult = new MNWSInfoRequestAnyUser.RequestResult();

      typedResult.DataEntry = MNWSAnyUserItemFromDictionary((IDictionary)deserializedObject["DataEntry"]);
      typedResult.HadError = Convert.ToBoolean(deserializedObject["HadError"]);
      typedResult.ErrorMessage = (string)deserializedObject["ErrorMessage"];

      return typedResult;
    }

    public static MNWSUserGameCookie MNWSUserGameCookieFromDictionary(IDictionary deserializedObject) {
      if (deserializedObject == null) {
        return null;
      }

      MNWSUserGameCookie typedResult = new MNWSUserGameCookie();

      typedResult.CookieKey = ((deserializedObject["CookieKey"] == null) ? null : (int?)Convert.ToInt32(deserializedObject["CookieKey"]));
      typedResult.CookieValue = (string)deserializedObject["CookieValue"];
      typedResult.UserId = ((deserializedObject["UserId"] == null) ? null : (long?)Convert.ToInt64(deserializedObject["UserId"]));

      return typedResult;
    }

    public static MNWSInfoRequestAnyUserGameCookies.RequestResult MNWSInfoRequestAnyUserGameCookiesRequestResultFromDictionary(IDictionary deserializedObject) {
      if (deserializedObject == null) {
        return null;
      }

      MNWSInfoRequestAnyUserGameCookies.RequestResult typedResult = new MNWSInfoRequestAnyUserGameCookies.RequestResult();


      List<object> deserializedItems = (List<object>)deserializedObject["DataEntry"];
      MNWSUserGameCookie[] items = null;

      if (deserializedItems != null) {
        items = new MNWSUserGameCookie[deserializedItems.Count];

        for (int index = 0;index < deserializedItems.Count;index++) {
          items[index] = MNWSUserGameCookieFromDictionary((IDictionary)deserializedItems[index]);
        }
      }

      typedResult.DataEntry = items;

      typedResult.HadError = Convert.ToBoolean(deserializedObject["HadError"]);
      typedResult.ErrorMessage = (string)deserializedObject["ErrorMessage"];

      return typedResult;
    }

    public static MNWSCurrentUserInfo MNWSCurrentUserInfoFromDictionary(IDictionary deserializedObject) {
      if (deserializedObject == null) {
        return null;
      }

      MNWSCurrentUserInfo typedResult = new MNWSCurrentUserInfo();

      typedResult.UserAvatarExists = ((deserializedObject["UserAvatarExists"] == null) ? null : (bool?)Convert.ToBoolean(deserializedObject["UserAvatarExists"]));
      typedResult.UserAvatarHasCustomImg = ((deserializedObject["UserAvatarHasCustomImg"] == null) ? null : (bool?)Convert.ToBoolean(deserializedObject["UserAvatarHasCustomImg"]));
      typedResult.UserAvatarHasExternalUrl = ((deserializedObject["UserAvatarHasExternalUrl"] == null) ? null : (bool?)Convert.ToBoolean(deserializedObject["UserAvatarHasExternalUrl"]));
      typedResult.UserAvatarUrl = (string)deserializedObject["UserAvatarUrl"];
      typedResult.UserEmail = (string)deserializedObject["UserEmail"];
      typedResult.UserGamePoints = ((deserializedObject["UserGamePoints"] == null) ? null : (int?)Convert.ToInt32(deserializedObject["UserGamePoints"]));
      typedResult.UserId = ((deserializedObject["UserId"] == null) ? null : (long?)Convert.ToInt64(deserializedObject["UserId"]));
      typedResult.UserNickName = (string)deserializedObject["UserNickName"];
      typedResult.UserOnlineNow = ((deserializedObject["UserOnlineNow"] == null) ? null : (bool?)Convert.ToBoolean(deserializedObject["UserOnlineNow"]));
      typedResult.UserStatus = ((deserializedObject["UserStatus"] == null) ? null : (int?)Convert.ToInt32(deserializedObject["UserStatus"]));

      return typedResult;
    }

    public static MNWSInfoRequestCurrentUserInfo.RequestResult MNWSInfoRequestCurrentUserInfoRequestResultFromDictionary(IDictionary deserializedObject) {
      if (deserializedObject == null) {
        return null;
      }

      MNWSInfoRequestCurrentUserInfo.RequestResult typedResult = new MNWSInfoRequestCurrentUserInfo.RequestResult();

      typedResult.DataEntry = MNWSCurrentUserInfoFromDictionary((IDictionary)deserializedObject["DataEntry"]);
      typedResult.HadError = Convert.ToBoolean(deserializedObject["HadError"]);
      typedResult.ErrorMessage = (string)deserializedObject["ErrorMessage"];

      return typedResult;
    }

    public static MNWSRoomListItem MNWSRoomListItemFromDictionary(IDictionary deserializedObject) {
      if (deserializedObject == null) {
        return null;
      }

      MNWSRoomListItem typedResult = new MNWSRoomListItem();

      typedResult.GameId = ((deserializedObject["GameId"] == null) ? null : (int?)Convert.ToInt32(deserializedObject["GameId"]));
      typedResult.GameSetId = ((deserializedObject["GameSetId"] == null) ? null : (int?)Convert.ToInt32(deserializedObject["GameSetId"]));
      typedResult.RoomIsLobby = ((deserializedObject["RoomIsLobby"] == null) ? null : (bool?)Convert.ToBoolean(deserializedObject["RoomIsLobby"]));
      typedResult.RoomName = (string)deserializedObject["RoomName"];
      typedResult.RoomSFId = ((deserializedObject["RoomSFId"] == null) ? null : (int?)Convert.ToInt32(deserializedObject["RoomSFId"]));
      typedResult.RoomUserCount = ((deserializedObject["RoomUserCount"] == null) ? null : (int?)Convert.ToInt32(deserializedObject["RoomUserCount"]));

      return typedResult;
    }

    public static MNWSInfoRequestCurrGameRoomList.RequestResult MNWSInfoRequestCurrGameRoomListRequestResultFromDictionary(IDictionary deserializedObject) {
      if (deserializedObject == null) {
        return null;
      }

      MNWSInfoRequestCurrGameRoomList.RequestResult typedResult = new MNWSInfoRequestCurrGameRoomList.RequestResult();


      List<object> deserializedItems = (List<object>)deserializedObject["DataEntry"];
      MNWSRoomListItem[] items = null;

      if (deserializedItems != null) {
        items = new MNWSRoomListItem[deserializedItems.Count];

        for (int index = 0;index < deserializedItems.Count;index++) {
          items[index] = MNWSRoomListItemFromDictionary((IDictionary)deserializedItems[index]);
        }
      }

      typedResult.DataEntry = items;

      typedResult.HadError = Convert.ToBoolean(deserializedObject["HadError"]);
      typedResult.ErrorMessage = (string)deserializedObject["ErrorMessage"];

      return typedResult;
    }

    public static MNWSRoomUserInfoItem MNWSRoomUserInfoItemFromDictionary(IDictionary deserializedObject) {
      if (deserializedObject == null) {
        return null;
      }

      MNWSRoomUserInfoItem typedResult = new MNWSRoomUserInfoItem();

      typedResult.RoomSFId = ((deserializedObject["RoomSFId"] == null) ? null : (int?)Convert.ToInt32(deserializedObject["RoomSFId"]));
      typedResult.UserAvatarExists = ((deserializedObject["UserAvatarExists"] == null) ? null : (bool?)Convert.ToBoolean(deserializedObject["UserAvatarExists"]));
      typedResult.UserAvatarUrl = (string)deserializedObject["UserAvatarUrl"];
      typedResult.UserId = ((deserializedObject["UserId"] == null) ? null : (long?)Convert.ToInt64(deserializedObject["UserId"]));
      typedResult.UserNickName = (string)deserializedObject["UserNickName"];
      typedResult.UserOnlineNow = ((deserializedObject["UserOnlineNow"] == null) ? null : (bool?)Convert.ToBoolean(deserializedObject["UserOnlineNow"]));

      return typedResult;
    }

    public static MNWSInfoRequestCurrGameRoomUserList.RequestResult MNWSInfoRequestCurrGameRoomUserListRequestResultFromDictionary(IDictionary deserializedObject) {
      if (deserializedObject == null) {
        return null;
      }

      MNWSInfoRequestCurrGameRoomUserList.RequestResult typedResult = new MNWSInfoRequestCurrGameRoomUserList.RequestResult();


      List<object> deserializedItems = (List<object>)deserializedObject["DataEntry"];
      MNWSRoomUserInfoItem[] items = null;

      if (deserializedItems != null) {
        items = new MNWSRoomUserInfoItem[deserializedItems.Count];

        for (int index = 0;index < deserializedItems.Count;index++) {
          items[index] = MNWSRoomUserInfoItemFromDictionary((IDictionary)deserializedItems[index]);
        }
      }

      typedResult.DataEntry = items;

      typedResult.HadError = Convert.ToBoolean(deserializedObject["HadError"]);
      typedResult.ErrorMessage = (string)deserializedObject["ErrorMessage"];

      return typedResult;
    }

    public static MNWSBuddyListItem MNWSBuddyListItemFromDictionary(IDictionary deserializedObject) {
      if (deserializedObject == null) {
        return null;
      }

      MNWSBuddyListItem typedResult = new MNWSBuddyListItem();

      typedResult.FriendCurrGameAchievementsList = (string)deserializedObject["FriendCurrGameAchievementsList"];
      typedResult.FriendFlags = ((deserializedObject["FriendFlags"] == null) ? null : (long?)Convert.ToInt64(deserializedObject["FriendFlags"]));
      typedResult.FriendHasCurrentGame = ((deserializedObject["FriendHasCurrentGame"] == null) ? null : (bool?)Convert.ToBoolean(deserializedObject["FriendHasCurrentGame"]));
      typedResult.FriendInGameIconUrl = (string)deserializedObject["FriendInGameIconUrl"];
      typedResult.FriendInGameId = ((deserializedObject["FriendInGameId"] == null) ? null : (int?)Convert.ToInt32(deserializedObject["FriendInGameId"]));
      typedResult.FriendInGameName = (string)deserializedObject["FriendInGameName"];
      typedResult.FriendInRoomIsLobby = ((deserializedObject["FriendInRoomIsLobby"] == null) ? null : (bool?)Convert.ToBoolean(deserializedObject["FriendInRoomIsLobby"]));
      typedResult.FriendInRoomSfid = ((deserializedObject["FriendInRoomSfid"] == null) ? null : (int?)Convert.ToInt32(deserializedObject["FriendInRoomSfid"]));
      typedResult.FriendIsIgnored = ((deserializedObject["FriendIsIgnored"] == null) ? null : (bool?)Convert.ToBoolean(deserializedObject["FriendIsIgnored"]));
      typedResult.FriendSnId = ((deserializedObject["FriendSnId"] == null) ? null : (int?)Convert.ToInt32(deserializedObject["FriendSnId"]));
      typedResult.FriendSnIdList = (string)deserializedObject["FriendSnIdList"];
      typedResult.FriendSnUserAsnId = ((deserializedObject["FriendSnUserAsnId"] == null) ? null : (long?)Convert.ToInt64(deserializedObject["FriendSnUserAsnId"]));
      typedResult.FriendSnUserAsnIdList = (string)deserializedObject["FriendSnUserAsnIdList"];
      typedResult.FriendUserAvatarUrl = (string)deserializedObject["FriendUserAvatarUrl"];
      typedResult.FriendUserId = ((deserializedObject["FriendUserId"] == null) ? null : (long?)Convert.ToInt64(deserializedObject["FriendUserId"]));
      typedResult.FriendUserLocale = (string)deserializedObject["FriendUserLocale"];
      typedResult.FriendUserNickName = (string)deserializedObject["FriendUserNickName"];
      typedResult.FriendUserOnlineNow = ((deserializedObject["FriendUserOnlineNow"] == null) ? null : (bool?)Convert.ToBoolean(deserializedObject["FriendUserOnlineNow"]));
      typedResult.FriendUserSfid = ((deserializedObject["FriendUserSfid"] == null) ? null : (int?)Convert.ToInt32(deserializedObject["FriendUserSfid"]));

      return typedResult;
    }

    public static MNWSInfoRequestCurrUserBuddyList.RequestResult MNWSInfoRequestCurrUserBuddyListRequestResultFromDictionary(IDictionary deserializedObject) {
      if (deserializedObject == null) {
        return null;
      }

      MNWSInfoRequestCurrUserBuddyList.RequestResult typedResult = new MNWSInfoRequestCurrUserBuddyList.RequestResult();


      List<object> deserializedItems = (List<object>)deserializedObject["DataEntry"];
      MNWSBuddyListItem[] items = null;

      if (deserializedItems != null) {
        items = new MNWSBuddyListItem[deserializedItems.Count];

        for (int index = 0;index < deserializedItems.Count;index++) {
          items[index] = MNWSBuddyListItemFromDictionary((IDictionary)deserializedItems[index]);
        }
      }

      typedResult.DataEntry = items;

      typedResult.HadError = Convert.ToBoolean(deserializedObject["HadError"]);
      typedResult.ErrorMessage = (string)deserializedObject["ErrorMessage"];

      return typedResult;
    }

    public static MNWSCurrUserSubscriptionStatus MNWSCurrUserSubscriptionStatusFromDictionary(IDictionary deserializedObject) {
      if (deserializedObject == null) {
        return null;
      }

      MNWSCurrUserSubscriptionStatus typedResult = new MNWSCurrUserSubscriptionStatus();

      typedResult.HasSubscription = ((deserializedObject["HasSubscription"] == null) ? null : (bool?)Convert.ToBoolean(deserializedObject["HasSubscription"]));
      typedResult.IsSubscriptionAvailable = ((deserializedObject["IsSubscriptionAvailable"] == null) ? null : (bool?)Convert.ToBoolean(deserializedObject["IsSubscriptionAvailable"]));
      typedResult.OffersAvailable = (string)deserializedObject["OffersAvailable"];

      return typedResult;
    }

    public static MNWSInfoRequestCurrUserSubscriptionStatus.RequestResult MNWSInfoRequestCurrUserSubscriptionStatusRequestResultFromDictionary(IDictionary deserializedObject) {
      if (deserializedObject == null) {
        return null;
      }

      MNWSInfoRequestCurrUserSubscriptionStatus.RequestResult typedResult = new MNWSInfoRequestCurrUserSubscriptionStatus.RequestResult();

      typedResult.DataEntry = MNWSCurrUserSubscriptionStatusFromDictionary((IDictionary)deserializedObject["DataEntry"]);
      typedResult.HadError = Convert.ToBoolean(deserializedObject["HadError"]);
      typedResult.ErrorMessage = (string)deserializedObject["ErrorMessage"];

      return typedResult;
    }

    public static MNWSSessionSignedClientToken MNWSSessionSignedClientTokenFromDictionary(IDictionary deserializedObject) {
      if (deserializedObject == null) {
        return null;
      }

      MNWSSessionSignedClientToken typedResult = new MNWSSessionSignedClientToken();

      typedResult.ClientTokenBody = (string)deserializedObject["ClientTokenBody"];
      typedResult.ClientTokenSign = (string)deserializedObject["ClientTokenSign"];

      return typedResult;
    }

    public static MNWSInfoRequestSessionSignedClientToken.RequestResult MNWSInfoRequestSessionSignedClientTokenRequestResultFromDictionary(IDictionary deserializedObject) {
      if (deserializedObject == null) {
        return null;
      }

      MNWSInfoRequestSessionSignedClientToken.RequestResult typedResult = new MNWSInfoRequestSessionSignedClientToken.RequestResult();

      typedResult.DataEntry = MNWSSessionSignedClientTokenFromDictionary((IDictionary)deserializedObject["DataEntry"]);
      typedResult.HadError = Convert.ToBoolean(deserializedObject["HadError"]);
      typedResult.ErrorMessage = (string)deserializedObject["ErrorMessage"];

      return typedResult;
    }

    public static MNWSSystemGameNetStats MNWSSystemGameNetStatsFromDictionary(IDictionary deserializedObject) {
      if (deserializedObject == null) {
        return null;
      }

      MNWSSystemGameNetStats typedResult = new MNWSSystemGameNetStats();

      typedResult.GameOnlineRooms = ((deserializedObject["GameOnlineRooms"] == null) ? null : (long?)Convert.ToInt64(deserializedObject["GameOnlineRooms"]));
      typedResult.GameOnlineUsers = ((deserializedObject["GameOnlineUsers"] == null) ? null : (long?)Convert.ToInt64(deserializedObject["GameOnlineUsers"]));
      typedResult.ServOnlineGames = ((deserializedObject["ServOnlineGames"] == null) ? null : (long?)Convert.ToInt64(deserializedObject["ServOnlineGames"]));
      typedResult.ServOnlineRooms = ((deserializedObject["ServOnlineRooms"] == null) ? null : (long?)Convert.ToInt64(deserializedObject["ServOnlineRooms"]));
      typedResult.ServOnlineUsers = ((deserializedObject["ServOnlineUsers"] == null) ? null : (long?)Convert.ToInt64(deserializedObject["ServOnlineUsers"]));
      typedResult.ServTotalGames = ((deserializedObject["ServTotalGames"] == null) ? null : (long?)Convert.ToInt64(deserializedObject["ServTotalGames"]));
      typedResult.ServTotalUsers = ((deserializedObject["ServTotalUsers"] == null) ? null : (long?)Convert.ToInt64(deserializedObject["ServTotalUsers"]));

      return typedResult;
    }

    public static MNWSInfoRequestSystemGameNetStats.RequestResult MNWSInfoRequestSystemGameNetStatsRequestResultFromDictionary(IDictionary deserializedObject) {
      if (deserializedObject == null) {
        return null;
      }

      MNWSInfoRequestSystemGameNetStats.RequestResult typedResult = new MNWSInfoRequestSystemGameNetStats.RequestResult();

      typedResult.DataEntry = MNWSSystemGameNetStatsFromDictionary((IDictionary)deserializedObject["DataEntry"]);
      typedResult.HadError = Convert.ToBoolean(deserializedObject["HadError"]);
      typedResult.ErrorMessage = (string)deserializedObject["ErrorMessage"];

      return typedResult;
    }

    public static MNWSLeaderboardListItem MNWSLeaderboardListItemFromDictionary(IDictionary deserializedObject) {
      if (deserializedObject == null) {
        return null;
      }

      MNWSLeaderboardListItem typedResult = new MNWSLeaderboardListItem();

      typedResult.GameId = ((deserializedObject["GameId"] == null) ? null : (int?)Convert.ToInt32(deserializedObject["GameId"]));
      typedResult.GamesetId = ((deserializedObject["GamesetId"] == null) ? null : (int?)Convert.ToInt32(deserializedObject["GamesetId"]));
      typedResult.OutHiDateTime = ((deserializedObject["OutHiDateTime"] == null) ? null : (long?)Convert.ToInt64(deserializedObject["OutHiDateTime"]));
      typedResult.OutHiDateTimeDiff = ((deserializedObject["OutHiDateTimeDiff"] == null) ? null : (long?)Convert.ToInt64(deserializedObject["OutHiDateTimeDiff"]));
      typedResult.OutHiScore = ((deserializedObject["OutHiScore"] == null) ? null : (long?)Convert.ToInt64(deserializedObject["OutHiScore"]));
      typedResult.OutHiScoreText = (string)deserializedObject["OutHiScoreText"];
      typedResult.OutUserPlace = ((deserializedObject["OutUserPlace"] == null) ? null : (long?)Convert.ToInt64(deserializedObject["OutUserPlace"]));
      typedResult.UserAchievementsList = (string)deserializedObject["UserAchievementsList"];
      typedResult.UserAvatarUrl = (string)deserializedObject["UserAvatarUrl"];
      typedResult.UserId = ((deserializedObject["UserId"] == null) ? null : (long?)Convert.ToInt64(deserializedObject["UserId"]));
      typedResult.UserIsFriend = ((deserializedObject["UserIsFriend"] == null) ? null : (bool?)Convert.ToBoolean(deserializedObject["UserIsFriend"]));
      typedResult.UserIsIgnored = ((deserializedObject["UserIsIgnored"] == null) ? null : (bool?)Convert.ToBoolean(deserializedObject["UserIsIgnored"]));
      typedResult.UserLocale = (string)deserializedObject["UserLocale"];
      typedResult.UserNickName = (string)deserializedObject["UserNickName"];
      typedResult.UserOnlineNow = ((deserializedObject["UserOnlineNow"] == null) ? null : (bool?)Convert.ToBoolean(deserializedObject["UserOnlineNow"]));
      typedResult.UserSfid = ((deserializedObject["UserSfid"] == null) ? null : (int?)Convert.ToInt32(deserializedObject["UserSfid"]));

      return typedResult;
    }

    public static MNWSInfoRequestLeaderboard.RequestResult MNWSInfoRequestLeaderboardRequestResultFromDictionary(IDictionary deserializedObject) {
      if (deserializedObject == null) {
        return null;
      }

      MNWSInfoRequestLeaderboard.RequestResult typedResult = new MNWSInfoRequestLeaderboard.RequestResult();


      List<object> deserializedItems = (List<object>)deserializedObject["DataEntry"];
      MNWSLeaderboardListItem[] items = null;

      if (deserializedItems != null) {
        items = new MNWSLeaderboardListItem[deserializedItems.Count];

        for (int index = 0;index < deserializedItems.Count;index++) {
          items[index] = MNWSLeaderboardListItemFromDictionary((IDictionary)deserializedItems[index]);
        }
      }

      typedResult.DataEntry = items;

      typedResult.HadError = Convert.ToBoolean(deserializedObject["HadError"]);
      typedResult.ErrorMessage = (string)deserializedObject["ErrorMessage"];

      return typedResult;
    }
  }
}