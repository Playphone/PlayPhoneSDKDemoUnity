package com.playphone.multinet.unity.serializer;

import java.lang.reflect.Array;
import java.util.HashMap;
import java.util.Map;

import org.json.JSONArray;
import org.json.JSONException;
import org.json.JSONObject;

import com.playphone.multinet.MNDirect;
import com.playphone.multinet.MNErrorInfo;
import com.playphone.multinet.MNGameParams;
import com.playphone.multinet.MNUserInfo;
import com.playphone.multinet.core.MNBuddyRoomParams;
import com.playphone.multinet.core.MNJoinRoomInvitationParams;
import com.playphone.multinet.core.ws.data.MNWSAnyGameItem;
import com.playphone.multinet.core.ws.data.MNWSAnyUserItem;
import com.playphone.multinet.core.ws.data.MNWSBuddyListItem;
import com.playphone.multinet.core.ws.data.MNWSCurrUserSubscriptionStatus;
import com.playphone.multinet.core.ws.data.MNWSCurrentUserInfo;
import com.playphone.multinet.core.ws.data.MNWSLeaderboardListItem;
import com.playphone.multinet.core.ws.data.MNWSRoomListItem;
import com.playphone.multinet.core.ws.data.MNWSRoomUserInfoItem;
import com.playphone.multinet.core.ws.data.MNWSSessionSignedClientToken;
import com.playphone.multinet.core.ws.data.MNWSSystemGameNetStats;
import com.playphone.multinet.core.ws.data.MNWSUserGameCookie;
import com.playphone.multinet.providers.MNAchievementsProvider;
import com.playphone.multinet.providers.MNGameSettingsProvider;
import com.playphone.multinet.providers.MNScoreProgressProvider;
import com.playphone.multinet.providers.MNVItemsProvider;
import com.playphone.multinet.providers.MNVShopProvider;
import com.playphone.multinet.providers.MNWSInfoRequestAnyGame;
import com.playphone.multinet.providers.MNWSInfoRequestAnyUser;
import com.playphone.multinet.providers.MNWSInfoRequestAnyUserGameCookies;
import com.playphone.multinet.providers.MNWSInfoRequestCurrGameRoomList;
import com.playphone.multinet.providers.MNWSInfoRequestCurrGameRoomUserList;
import com.playphone.multinet.providers.MNWSInfoRequestCurrUserBuddyList;
import com.playphone.multinet.providers.MNWSInfoRequestCurrUserSubscriptionStatus;
import com.playphone.multinet.providers.MNWSInfoRequestCurrentUserInfo;
import com.playphone.multinet.providers.MNWSInfoRequestLeaderboard;
import com.playphone.multinet.providers.MNWSInfoRequestLeaderboard.LeaderboardMode;
import com.playphone.multinet.providers.MNWSInfoRequestLeaderboard.LeaderboardModeAnyGameGlobal;
import com.playphone.multinet.providers.MNWSInfoRequestLeaderboard.LeaderboardModeAnyUserAnyGameGlobal;
import com.playphone.multinet.providers.MNWSInfoRequestLeaderboard.LeaderboardModeCurrUserAnyGameLocal;
import com.playphone.multinet.providers.MNWSInfoRequestLeaderboard.LeaderboardModeCurrentUser;
import com.playphone.multinet.providers.MNWSInfoRequestSessionSignedClientToken;
import com.playphone.multinet.providers.MNWSInfoRequestSystemGameNetStats;
import com.playphone.multinet.unity.MNUnity;

public class MNJsonSerializer extends MNSerializer {

    @Override
    public String serialize(Object obj) {
        if (obj == null) {
            JSONArray nullJsonArray = new JSONArray();
            nullJsonArray.put(JSONObject.NULL);

            return nullJsonArray.toString();
        }

        MNJsonElement resultJsonElement = getJSONElement(obj);

        if (resultJsonElement == null) {
            // May be it's a boxed primitive type or null. If so, serialize it as array of one element.
            Object[] objects = new Object[1];
            objects[0] = obj;

            resultJsonElement = getJSONElement(objects);
        }

        if (resultJsonElement == null) {
            MNUnity.ELog("Serialization error for object:[" + obj.toString() + "] of type: " + obj.getClass().getName());
            return null;
        }

        return resultJsonElement.toString();
    }

    @Override
    public <T> T deserialize(String jsonString,Class<T> type) {
        if (type == Integer.class) {
            Integer[] array = getObjectFromJSON(jsonString,Integer[].class);
            return type.cast(array[0]);
        }
        else if (type == Long.class) {
            Long[] array = getObjectFromJSON(jsonString,Long[].class);
            return type.cast(array[0]);
        }
        else if (type == Double.class) {
            Double[] array = getObjectFromJSON(jsonString,Double[].class);
            return type.cast(array[0]);
        }
        else if (type == Boolean.class) {
            Boolean[] array = getObjectFromJSON(jsonString,Boolean[].class);
            return type.cast(array[0]);
        }
        else if (type == String.class) {
            String[] array = getObjectFromJSON(jsonString,String[].class);
            return type.cast(array[0]);
        }
        else {
            return getObjectFromJSON(jsonString,type);
        }
    }

    @Override
    public <K, V> Map<K,V> deserializeMap(String jsonString,Class<K> keyType,Class<V> valueType) {
        Map<K,V> result = getMapFromJSON(jsonString,keyType,valueType);

        if (result == null) {
            MNUnity.ELog(String.format("Map<%s,%s> Deserialization error. Source Json: <<%s>>",
                                       keyType.getName(),
                                       valueType.getName(),
                                       jsonString));
        }

        return result;
    }

    public static MNJsonElement getJSONElement(Object object) {
        try {
            MNUnity.DLog("Serialize object: " + object.getClass().getSimpleName());

            if (object instanceof MNErrorInfo) {
                return getJSONElement((MNErrorInfo)object);
            }

            if (object instanceof MNGameParams) {
                return getJSONElement((MNGameParams)object);
            }

            if (object instanceof MNUserInfo) {
                return getJSONElement((MNUserInfo)object);
            }

            if (object instanceof MNAchievementsProvider.GameAchievementInfo) {
                return getJSONElement((MNAchievementsProvider.GameAchievementInfo)object);
            }

            if (object instanceof MNAchievementsProvider.PlayerAchievementInfo) {
                return getJSONElement((MNAchievementsProvider.PlayerAchievementInfo)object);
            }

            if (object instanceof MNVItemsProvider.GameVItemInfo) {
                return getJSONElement((MNVItemsProvider.GameVItemInfo)object);
            }

            if (object instanceof MNVItemsProvider.PlayerVItemInfo) {
                return getJSONElement((MNVItemsProvider.PlayerVItemInfo)object);
            }

            if (object instanceof MNVItemsProvider.TransactionVItemInfo) {
                return getJSONElement((MNVItemsProvider.TransactionVItemInfo)object);
            }

            if (object instanceof MNVItemsProvider.TransactionInfo) {
                return getJSONElement((MNVItemsProvider.TransactionInfo)object);
            }

            if (object instanceof MNVItemsProvider.TransactionError) {
                return getJSONElement((MNVItemsProvider.TransactionError)object);
            }

            if (object instanceof MNVShopProvider.VShopDeliveryInfo) {
                return getJSONElement((MNVShopProvider.VShopDeliveryInfo)object);
            }

            if (object instanceof MNVShopProvider.VShopPackInfo) {
                return getJSONElement((MNVShopProvider.VShopPackInfo)object);
            }

            if (object instanceof MNVShopProvider.VShopCategoryInfo) {
                return getJSONElement((MNVShopProvider.VShopCategoryInfo)object);
            }

            if (object instanceof MNVShopProvider.VShopPackBuyRequestItem) {
                return getJSONElement((MNVShopProvider.VShopPackBuyRequestItem)object);
            }

            if (object instanceof MNVShopProvider.IEventHandler.CheckoutVShopPackSuccessInfo) {
                return getJSONElement((MNVShopProvider.IEventHandler.CheckoutVShopPackSuccessInfo)object);
            }

            if (object instanceof MNVShopProvider.IEventHandler.CheckoutVShopPackFailInfo) {
                return getJSONElement((MNVShopProvider.IEventHandler.CheckoutVShopPackFailInfo)object);
            }

            if (object instanceof MNGameSettingsProvider.GameSettingInfo) {
                return getJSONElement((MNGameSettingsProvider.GameSettingInfo)object);
            }

            if (object instanceof MNWSAnyGameItem) {
                return getJSONElement((MNWSAnyGameItem)object);
            }

            if (object instanceof MNWSInfoRequestAnyGame.RequestResult) {
                return getJSONElement((MNWSInfoRequestAnyGame.RequestResult)object);
            }

            if (object instanceof MNWSAnyUserItem) {
                return getJSONElement((MNWSAnyUserItem)object);
            }

            if (object instanceof MNWSInfoRequestAnyUser.RequestResult) {
                return getJSONElement((MNWSInfoRequestAnyUser.RequestResult)object);
            }

            if (object instanceof MNWSUserGameCookie) {
                return getJSONElement((MNWSUserGameCookie)object);
            }

            if (object instanceof MNWSInfoRequestAnyUserGameCookies.RequestResult) {
                return getJSONElement((MNWSInfoRequestAnyUserGameCookies.RequestResult)object);
            }

            if (object instanceof MNWSCurrentUserInfo) {
                return getJSONElement((MNWSCurrentUserInfo)object);
            }

            if (object instanceof MNWSInfoRequestCurrentUserInfo.RequestResult) {
                return getJSONElement((MNWSInfoRequestCurrentUserInfo.RequestResult)object);
            }

            if (object instanceof MNWSRoomListItem) {
                return getJSONElement((MNWSRoomListItem)object);
            }

            if (object instanceof MNWSInfoRequestCurrGameRoomList.RequestResult) {
                return getJSONElement((MNWSInfoRequestCurrGameRoomList.RequestResult)object);
            }

            if (object instanceof MNWSRoomUserInfoItem) {
                return getJSONElement((MNWSRoomUserInfoItem)object);
            }

            if (object instanceof MNWSInfoRequestCurrGameRoomUserList.RequestResult) {
                return getJSONElement((MNWSInfoRequestCurrGameRoomUserList.RequestResult)object);
            }

            if (object instanceof MNWSBuddyListItem) {
                return getJSONElement((MNWSBuddyListItem)object);
            }

            if (object instanceof MNWSInfoRequestCurrUserBuddyList.RequestResult) {
                return getJSONElement((MNWSInfoRequestCurrUserBuddyList.RequestResult)object);
            }

            if (object instanceof MNWSCurrUserSubscriptionStatus) {
                return getJSONElement((MNWSCurrUserSubscriptionStatus)object);
            }

            if (object instanceof MNWSInfoRequestCurrUserSubscriptionStatus.RequestResult) {
                return getJSONElement((MNWSInfoRequestCurrUserSubscriptionStatus.RequestResult)object);
            }

            if (object instanceof MNWSSessionSignedClientToken) {
                return getJSONElement((MNWSSessionSignedClientToken)object);
            }

            if (object instanceof MNWSInfoRequestSessionSignedClientToken.RequestResult) {
                return getJSONElement((MNWSInfoRequestSessionSignedClientToken.RequestResult)object);
            }

            if (object instanceof MNWSSystemGameNetStats) {
                return getJSONElement((MNWSSystemGameNetStats)object);
            }

            if (object instanceof MNWSInfoRequestSystemGameNetStats.RequestResult) {
                return getJSONElement((MNWSInfoRequestSystemGameNetStats.RequestResult)object);
            }

            if (object instanceof MNWSLeaderboardListItem) {
                return getJSONElement((MNWSLeaderboardListItem)object);
            }

            if (object instanceof MNWSInfoRequestLeaderboard.RequestResult) {
                return getJSONElement((MNWSInfoRequestLeaderboard.RequestResult)object);
            }

            if (object instanceof MNScoreProgressProvider.ScoreItem) {
                return getJSONElement((MNScoreProgressProvider.ScoreItem)object);
            }

            if (object instanceof MNBuddyRoomParams) {
                return getJSONElement((MNBuddyRoomParams)object);
            }

            if (object instanceof MNJoinRoomInvitationParams) {
                return getJSONElement((MNJoinRoomInvitationParams)object);
            }

            if (object instanceof Map<?,?>) {
                Map<?,?> srcMap = (Map<?,?>)object;
                Object[] dstArray = new Object[srcMap.size() * 2];

                int index = 0;
                for (Object key : srcMap.keySet()) {
                    dstArray[index++] = key;
                    dstArray[index++] = srcMap.get(key);
                }

                return getJSONElement(dstArray);
            }

            if (object.getClass().isArray()) {
                return getJSONElement((Object[])object);
            }
        }
        catch (JSONException e) {
            MNUnity.DLog("Can not create JSONObject from " + object.getClass().getSimpleName());
            e.printStackTrace();
        }

        return null;
    }

    protected static MNJsonElement getJSONElement(Object[] objectsArray) {
        if (objectsArray == null) {
            return null;
        }

        try {
            MNUnity.DLog("Serialize array: " + objectsArray.getClass().getSimpleName());

            JSONArray array = new JSONArray();

            for (int index = 0; index < objectsArray.length; index++) {
                if (MNUnity.isObjectTypeSupportedByJSONLib(objectsArray[index])) {
                    array.put(index,objectsArray[index]);
                }
                else {
                    MNJsonElement jsonElement = getJSONElement(objectsArray[index]);

                    if (jsonElement.isArray()) {
                        array.put(index,jsonElement.getElement(JSONArray.class));
                    }
                    else if (jsonElement.isObject()) {
                        array.put(index,jsonElement.getElement(JSONObject.class));
                    }
                    else {
                        throw new JSONException("");
                    }
                }
            }

            return new MNJsonElement(array);
        }
        catch (JSONException e) {
            MNUnity.DLog("Can not create JSONObject from " + objectsArray.getClass().getSimpleName());
            e.printStackTrace();
        }

        return null;

    }

    protected static MNJsonElement getJSONElement(MNErrorInfo errorInfo) throws JSONException {
        JSONObject json = new JSONObject();

        json.put("ActionCode",errorInfo.actionCode);
        json.put("ErrorMessage",errorInfo.errorMessage);

        return new MNJsonElement(json);
    }

    protected static MNJsonElement getJSONElement(MNGameParams gameParams) throws JSONException {
        JSONObject json = new JSONObject();

        json.put("GameSetId",gameParams.gameSetId);
        json.put("GameSetParams",gameParams.gameSetParams);
        json.put("ScorePostLinkId",gameParams.scorePostLinkId);
        json.put("GameSeed",gameParams.gameSeed);
        json.put("PlayModel",gameParams.playModel);

        return new MNJsonElement(json);
    }

    protected static MNJsonElement getJSONElement(MNUserInfo userInfo) throws JSONException {
        JSONObject json = new JSONObject();

        json.put("UserId",userInfo.userId);
        json.put("UserSFId",userInfo.userSFId);
        json.put("UserName",userInfo.userName);
        json.put("UserAvatarUrl",userInfo.getAvatarUrl());

        return new MNJsonElement(json);
    }

    protected static MNJsonElement getJSONElement(MNAchievementsProvider.GameAchievementInfo achInfo) throws JSONException {
        JSONObject json = new JSONObject();

        json.put("Id",achInfo.id);
        json.put("Name",achInfo.name);
        json.put("Flags",achInfo.flags);
        json.put("Description",achInfo.description);
        json.put("Points",achInfo.points);
        json.put("Params",achInfo.params);

        return new MNJsonElement(json);
    }

    protected static MNJsonElement getJSONElement(MNAchievementsProvider.PlayerAchievementInfo achInfo) throws JSONException {
        JSONObject json = new JSONObject();

        json.put("Id",achInfo.id);

        return new MNJsonElement(json);
    }

    protected static MNJsonElement getJSONElement(MNVItemsProvider.GameVItemInfo gameVItemInfo) throws JSONException {
        JSONObject json = new JSONObject();

        json.put("Id",gameVItemInfo.id);
        json.put("Name",gameVItemInfo.name);
        json.put("Model",gameVItemInfo.model);
        json.put("Description",gameVItemInfo.description);
        json.put("Params",gameVItemInfo.params);

        return new MNJsonElement(json);
    }

    protected static MNJsonElement getJSONElement(MNVItemsProvider.PlayerVItemInfo playerVItemInfo) throws JSONException {
        JSONObject json = new JSONObject();

        json.put("Id",playerVItemInfo.id);
        json.put("Count",playerVItemInfo.count);

        return new MNJsonElement(json);
    }

    protected static MNJsonElement getJSONElement(MNVItemsProvider.TransactionVItemInfo transVItemInfo) throws JSONException {
        JSONObject json = new JSONObject();

        json.put("Id",transVItemInfo.id);
        json.put("Delta",transVItemInfo.delta);

        return new MNJsonElement(json);
    }

    protected static MNJsonElement getJSONElement(MNVItemsProvider.TransactionInfo transInfo) throws JSONException {
        JSONObject json = new JSONObject();

        json.put("ClientTransactionId",transInfo.clientTransactionId);
        json.put("ServerTransactionId",transInfo.serverTransactionId);
        json.put("CorrUserId",transInfo.corrUserId);
        json.put("VItems",getJSONElement(transInfo.vItems).getElement(JSONArray.class));

        return new MNJsonElement(json);
    }

    protected static MNJsonElement getJSONElement(MNVItemsProvider.TransactionError transError) throws JSONException {
        JSONObject json = new JSONObject();

        json.put("ClientTransactionId",transError.clientTransactionId);
        json.put("ServerTransactionId",transError.serverTransactionId);
        json.put("CorrUserId",transError.corrUserId);
        json.put("FailReasonCode",transError.failReasonCode);
        json.put("ErrorMessage",transError.errorMessage);

        return new MNJsonElement(json);
    }

    protected static MNJsonElement getJSONElement(MNVShopProvider.VShopDeliveryInfo deliveryInfo) throws JSONException {
        JSONObject json = new JSONObject();

        json.put("VItemId",deliveryInfo.vItemId);
        json.put("Amount",deliveryInfo.amount);

        return new MNJsonElement(json);
    }

    protected static MNJsonElement getJSONElement(MNVShopProvider.VShopPackInfo vPackInfo) throws JSONException {
        JSONObject json = new JSONObject();

        json.put("Id",vPackInfo.id);
        json.put("Name",vPackInfo.name);
        json.put("Model",vPackInfo.model);
        json.put("Description",vPackInfo.description);
        json.put("AppParams",vPackInfo.appParams);
        json.put("SortPos",vPackInfo.sortPos);
        json.put("CategoryId",vPackInfo.categoryId);
        json.put("Delivery",getJSONElement(vPackInfo.delivery).getElement(JSONArray.class));
        json.put("PriceItemId",vPackInfo.priceItemId);
        json.put("PriceValue",vPackInfo.priceValue);

        return new MNJsonElement(json);
    }

    protected static MNJsonElement getJSONElement(MNVShopProvider.VShopCategoryInfo vCategoryInfo) throws JSONException {
        JSONObject json = new JSONObject();

        json.put("Id",vCategoryInfo.id);
        json.put("Name",vCategoryInfo.name);
        json.put("SortPos",vCategoryInfo.sortPos);

        return new MNJsonElement(json);
    }

    protected static MNJsonElement getJSONElement(MNVShopProvider.VShopPackBuyRequestItem vPackRequest) throws JSONException {
        JSONObject json = new JSONObject();

        json.put("Id",vPackRequest.id);
        json.put("Amount",vPackRequest.amount);

        return new MNJsonElement(json);
    }

    protected static MNJsonElement getJSONElement(MNVShopProvider.IEventHandler.CheckoutVShopPackSuccessInfo successInfo) throws JSONException {
        JSONObject json = new JSONObject();

        json.put("Transaction",getJSONElement(successInfo.getTransaction()).jsonObject);

        return new MNJsonElement(json);
    }

    protected static MNJsonElement getJSONElement(MNVShopProvider.IEventHandler.CheckoutVShopPackFailInfo failInfo) throws JSONException {
        JSONObject json = new JSONObject();

        json.put("ErrorCode",failInfo.getErrorCode());
        json.put("ErrorMessage",failInfo.getErrorMessage());
        json.put("ClientTransactionId",failInfo.getClientTransactionId());

        return new MNJsonElement(json);
    }

    protected static MNJsonElement getJSONElement(MNGameSettingsProvider.GameSettingInfo settingInfo) throws JSONException {
        JSONObject json = new JSONObject();

        json.put("Id",settingInfo.getId());
        json.put("Name",settingInfo.getName());
        json.put("Params",settingInfo.getParams());
        json.put("SysParams",settingInfo.getSysParams());
        json.put("MultiplayerEnabled",settingInfo.isMultiplayerEnabled());
        json.put("LeaderboardVisible",settingInfo.isLeaderboardVisible());

        return new MNJsonElement(json);
    }

    protected static MNJsonElement getJSONElement(MNWSAnyGameItem item) throws JSONException {
        if (item == null) {
            return null;
        }

        JSONObject json = new JSONObject();

        json.put("GameId",item.getGameId());
        json.put("GameName",item.getGameName());
        json.put("GameDesc",item.getGameDesc());
        json.put("GameGenreId",item.getGameGenreId());
        json.put("GameFlags",item.getGameFlags());
        json.put("GameStatus",item.getGameStatus());
        json.put("GamePlayModel",item.getGamePlayModel());
        json.put("GameIconUrl",item.getGameIconUrl());
        json.put("DeveloperId",item.getDeveloperId());

        return new MNJsonElement(json);
    }

    protected static MNJsonElement getJSONElement(MNWSInfoRequestAnyGame.RequestResult result) throws JSONException {
        JSONObject json = new JSONObject();

        json.put("HadError",result.hadError());
        json.put("ErrorMessage",result.getErrorMessage() == null ? "" : result.getErrorMessage());

        MNJsonElement dataEntryJson = getJSONElement(result.getDataEntry());

        json.put("DataEntry",dataEntryJson != null ? dataEntryJson.jsonObject : JSONObject.NULL);

        return new MNJsonElement(json);
    }

    protected static MNJsonElement getJSONElement(MNWSAnyUserItem item) throws JSONException {
        if (item == null) {
            return null;
        }

        JSONObject json = new JSONObject();

        json.put("UserId",item.getUserId());
        json.put("UserNickName",item.getUserNickName());
        json.put("UserAvatarExists",item.getUserAvatarExists());
        json.put("UserAvatarUrl",item.getUserAvatarUrl());
        json.put("UserOnlineNow",item.getUserOnlineNow());
        json.put("UserGamePoints",item.getUserGamePoints());
        json.put("MyFriendLinkStatus",item.getMyFriendLinkStatus());

        return new MNJsonElement(json);
    }

    protected static MNJsonElement getJSONElement(MNWSInfoRequestAnyUser.RequestResult result) throws JSONException {
        JSONObject json = new JSONObject();

        json.put("HadError",result.hadError());
        json.put("ErrorMessage",result.getErrorMessage() == null ? "" : result.getErrorMessage());

        MNJsonElement dataEntryJson = getJSONElement(result.getDataEntry());

        json.put("DataEntry",dataEntryJson != null ? dataEntryJson.jsonObject : JSONObject.NULL);

        return new MNJsonElement(json);
    }

    protected static MNJsonElement getJSONElement(MNWSUserGameCookie item) throws JSONException {
        if (item == null) {
            return null;
        }

        JSONObject json = new JSONObject();

        json.put("UserId",item.getUserId());
        json.put("CookieKey",item.getCookieKey());
        json.put("CookieValue",item.getCookieValue());

        return new MNJsonElement(json);
    }

    protected static MNJsonElement getJSONElement(MNWSInfoRequestAnyUserGameCookies.RequestResult result) throws JSONException {
        JSONObject json = new JSONObject();

        json.put("HadError",result.hadError());
        json.put("ErrorMessage",result.getErrorMessage() == null ? "" : result.getErrorMessage());

        MNJsonElement dataEntryJson = getJSONElement(result.getDataEntry());

        json.put("DataEntry",dataEntryJson != null ? dataEntryJson.jsonArray : JSONObject.NULL);

        return new MNJsonElement(json);
    }

    protected static MNJsonElement getJSONElement(MNWSCurrentUserInfo item) throws JSONException {
        if (item == null) {
            return null;
        }

        JSONObject json = new JSONObject();

        json.put("UserId",item.getUserId());
        json.put("UserNickName",item.getUserNickName());
        json.put("UserAvatarExists",item.getUserAvatarExists());
        json.put("UserAvatarUrl",item.getUserAvatarUrl());
        json.put("UserOnlineNow",item.getUserOnlineNow());
        json.put("UserGamePoints",item.getUserGamePoints());
        json.put("UserEmail",item.getUserEmail());
        json.put("UserStatus",item.getUserStatus());
        json.put("UserAvatarHasCustomImg",item.getUserAvatarHasCustomImg());
        json.put("UserAvatarHasExternalUrl",item.getUserAvatarHasExternalUrl());

        return new MNJsonElement(json);
    }

    protected static MNJsonElement getJSONElement(MNWSInfoRequestCurrentUserInfo.RequestResult result) throws JSONException {
        JSONObject json = new JSONObject();

        json.put("HadError",result.hadError());
        json.put("ErrorMessage",result.getErrorMessage() == null ? "" : result.getErrorMessage());

        MNJsonElement dataEntryJson = getJSONElement(result.getDataEntry());

        json.put("DataEntry",dataEntryJson != null ? dataEntryJson.jsonObject : JSONObject.NULL);

        return new MNJsonElement(json);
    }

    protected static MNJsonElement getJSONElement(MNWSRoomListItem item) throws JSONException {
        if (item == null) {
            return null;
        }

        JSONObject json = new JSONObject();

        json.put("GameId",item.getGameId());
        json.put("RoomSFId",item.getRoomSFId());
        json.put("RoomName",item.getRoomName());
        json.put("RoomUserCount",item.getRoomUserCount());
        json.put("RoomIsLobby",item.getRoomIsLobby());
        json.put("GameSetId",item.getGameSetId());

        return new MNJsonElement(json);
    }

    protected static MNJsonElement getJSONElement(MNWSInfoRequestCurrGameRoomList.RequestResult result) throws JSONException {
        JSONObject json = new JSONObject();

        json.put("HadError",result.hadError());
        json.put("ErrorMessage",result.getErrorMessage() == null ? "" : result.getErrorMessage());

        MNJsonElement dataEntryJson = getJSONElement(result.getDataEntry());

        json.put("DataEntry",dataEntryJson != null ? dataEntryJson.jsonArray : JSONObject.NULL);

        return new MNJsonElement(json);
    }

    protected static MNJsonElement getJSONElement(MNWSRoomUserInfoItem item) throws JSONException {
        if (item == null) {
            return null;
        }

        JSONObject json = new JSONObject();

        json.put("UserId",item.getUserId());
        json.put("UserNickName",item.getUserNickName());
        json.put("UserAvatarExists",item.getUserAvatarExists());
        json.put("UserAvatarUrl",item.getUserAvatarUrl());
        json.put("UserOnlineNow",item.getUserOnlineNow());
        json.put("RoomSFId",item.getRoomSFId());

        return new MNJsonElement(json);
    }

    protected static MNJsonElement getJSONElement(MNWSInfoRequestCurrGameRoomUserList.RequestResult result) throws JSONException {
        JSONObject json = new JSONObject();

        json.put("HadError",result.hadError());
        json.put("ErrorMessage",result.getErrorMessage() == null ? "" : result.getErrorMessage());

        MNJsonElement dataEntryJson = getJSONElement(result.getDataEntry());

        json.put("DataEntry",dataEntryJson != null ? dataEntryJson.jsonArray : JSONObject.NULL);

        return new MNJsonElement(json);
    }

    protected static MNJsonElement getJSONElement(MNWSBuddyListItem item) throws JSONException {
        if (item == null) {
            return null;
        }

        JSONObject json = new JSONObject();

        json.put("FriendUserId",item.getFriendUserId());
        json.put("FriendUserNickName",item.getFriendUserNickName());
        json.put("FriendSnIdList",item.getFriendSnIdList());
        json.put("FriendSnUserAsnIdList",item.getFriendSnUserAsnIdList());
        json.put("FriendInGameId",item.getFriendInGameId());
        json.put("FriendInGameName",item.getFriendInGameName());
        json.put("FriendInGameIconUrl",item.getFriendInGameIconUrl());
        json.put("FriendHasCurrentGame",item.getFriendHasCurrentGame());
        json.put("FriendUserLocale",item.getFriendUserLocale());
        json.put("FriendUserAvatarUrl",item.getFriendUserAvatarUrl());
        json.put("FriendUserOnlineNow",item.getFriendUserOnlineNow());
        json.put("FriendUserSfid",item.getFriendUserSfid());
        json.put("FriendSnId",item.getFriendSnId());
        json.put("FriendSnUserAsnId",item.getFriendSnUserAsnId());
        json.put("FriendFlags",item.getFriendFlags());
        json.put("FriendIsIgnored",item.getFriendIsIgnored());
        json.put("FriendInRoomSfid",item.getFriendInRoomSfid());
        json.put("FriendInRoomIsLobby",item.getFriendInRoomIsLobby());
        json.put("FriendCurrGameAchievementsList",item.getFriendCurrGameAchievementsList());

        return new MNJsonElement(json);
    }

    protected static MNJsonElement getJSONElement(MNWSInfoRequestCurrUserBuddyList.RequestResult result) throws JSONException {
        JSONObject json = new JSONObject();

        json.put("HadError",result.hadError());
        json.put("ErrorMessage",result.getErrorMessage() == null ? "" : result.getErrorMessage());

        MNJsonElement dataEntryJson = getJSONElement(result.getDataEntry());

        json.put("DataEntry",dataEntryJson != null ? dataEntryJson.jsonArray : JSONObject.NULL);

        return new MNJsonElement(json);
    }

    protected static MNJsonElement getJSONElement(MNWSCurrUserSubscriptionStatus item) throws JSONException {
        if (item == null) {
            return null;
        }

        JSONObject json = new JSONObject();

        json.put("HasSubscription",item.getHasSubscription());
        json.put("OffersAvailable",item.getOffersAvailable());
        json.put("IsSubscriptionAvailable",item.getIsSubscriptionAvailable());

        return new MNJsonElement(json);
    }

    protected static MNJsonElement getJSONElement(MNWSInfoRequestCurrUserSubscriptionStatus.RequestResult result) throws JSONException {
        JSONObject json = new JSONObject();

        json.put("HadError",result.hadError());
        json.put("ErrorMessage",result.getErrorMessage() == null ? "" : result.getErrorMessage());

        MNJsonElement dataEntryJson = getJSONElement(result.getDataEntry());

        json.put("DataEntry",dataEntryJson != null ? dataEntryJson.jsonObject : JSONObject.NULL);

        return new MNJsonElement(json);
    }

    protected static MNJsonElement getJSONElement(MNWSSessionSignedClientToken item) throws JSONException {
        if (item == null) {
            return null;
        }

        JSONObject json = new JSONObject();

        json.put("ClientTokenBody",item.getClientTokenBody());
        json.put("ClientTokenSign",item.getClientTokenSign());

        return new MNJsonElement(json);
    }

    protected static MNJsonElement getJSONElement(MNWSInfoRequestSessionSignedClientToken.RequestResult result) throws JSONException {
        JSONObject json = new JSONObject();

        json.put("HadError",result.hadError());
        json.put("ErrorMessage",result.getErrorMessage() == null ? "" : result.getErrorMessage());

        MNJsonElement dataEntryJson = getJSONElement(result.getDataEntry());

        json.put("DataEntry",dataEntryJson != null ? dataEntryJson.jsonObject : JSONObject.NULL);

        return new MNJsonElement(json);
    }

    protected static MNJsonElement getJSONElement(MNWSSystemGameNetStats item) throws JSONException {
        if (item == null) {
            return null;
        }

        JSONObject json = new JSONObject();

        json.put("ServTotalUsers",item.getServTotalUsers());
        json.put("ServTotalGames",item.getServTotalGames());
        json.put("ServOnlineUsers",item.getServOnlineUsers());
        json.put("ServOnlineRooms",item.getServOnlineRooms());
        json.put("ServOnlineGames",item.getServOnlineGames());
        json.put("GameOnlineUsers",item.getGameOnlineUsers());
        json.put("GameOnlineRooms",item.getGameOnlineRooms());

        return new MNJsonElement(json);
    }

    protected static MNJsonElement getJSONElement(MNWSInfoRequestSystemGameNetStats.RequestResult result) throws JSONException {
        JSONObject json = new JSONObject();

        json.put("HadError",result.hadError());
        json.put("ErrorMessage",result.getErrorMessage() == null ? "" : result.getErrorMessage());

        MNJsonElement dataEntryJson = getJSONElement(result.getDataEntry());

        json.put("DataEntry",dataEntryJson != null ? dataEntryJson.jsonObject : JSONObject.NULL);

        return new MNJsonElement(json);
    }

    protected static MNJsonElement getJSONElement(MNWSLeaderboardListItem item) throws JSONException {
        if (item == null) {
            return null;
        }

        JSONObject json = new JSONObject();
        /*
        MNUnity.DLog("GameId="+((item.getGameId()==null)?"null":item.getGameId().toString())+
        ";UserId="+((item.getUserId()==null)?"null":item.getUserId().toString())+
        ";UserNickName="+((item.getUserNickName()==null)?"null":item.getUserNickName().toString())+
        ";UserAvatarUrl="+((item.getUserAvatarUrl()==null)?"null":item.getUserAvatarUrl().toString())+
        ";UserOnlineNow="+((item.getUserOnlineNow()==null)?"null":item.getUserOnlineNow().toString())+
        ";UserIsFriend="+((item.getUserIsFriend()==null)?"null":item.getUserIsFriend().toString())+
        ";UserSfid="+((item.getUserSfid()==null)?"null":item.getUserSfid().toString())+
        ";UserIsIgnored="+((item.getUserIsIgnored()==null)?"null":item.getUserIsIgnored().toString())+
        ";UserLocale="+((item.getUserLocale()==null)?"null":item.getUserLocale().toString())+
        ";OutHiScore="+((item.getOutHiScore()==null)?"null":item.getOutHiScore().toString())+
        ";OutHiScoreText="+((item.getOutHiScoreText()==null)?"null":item.getOutHiScoreText().toString())+
        ";OutHiDateTime="+((item.getOutHiDateTime()==null)?"null":item.getOutHiDateTime().toString())+
        ";OutHiDateTimeDiff="+((item.getOutHiDateTimeDiff()==null)?"null":item.getOutHiDateTimeDiff().toString())+
        ";OutUserPlace="+((item.getOutUserPlace()==null)?"null":item.getOutUserPlace().toString())+
        ";GamesetId="+((item.getGamesetId()==null)?"null":item.getGamesetId().toString())+
        ";UserAchievementsList="+((item.getUserAchievementsList()==null)?"null":item.getUserAchievementsList().toString()));
        */
        json.put("GameId",item.getGameId());
        json.put("UserId",item.getUserId());
        json.put("UserNickName",item.getUserNickName());
        json.put("UserAvatarUrl",item.getUserAvatarUrl());
        json.put("UserOnlineNow",item.getUserOnlineNow());
        json.put("UserIsFriend",item.getUserIsFriend());
        json.put("UserSfid",item.getUserSfid());
        json.put("UserIsIgnored",item.getUserIsIgnored());
        json.put("UserLocale",item.getUserLocale());
        json.put("OutHiScore",item.getOutHiScore());
        json.put("OutHiScoreText",item.getOutHiScoreText());
        json.put("OutHiDateTime",item.getOutHiDateTime());
        json.put("OutHiDateTimeDiff",item.getOutHiDateTimeDiff());
        json.put("OutUserPlace",item.getOutUserPlace());
        json.put("GamesetId",item.getGamesetId());
        json.put("UserAchievementsList",item.getUserAchievementsList());

        return new MNJsonElement(json);
    }

    protected static MNJsonElement getJSONElement(MNWSInfoRequestLeaderboard.RequestResult result) throws JSONException {
        JSONObject json = new JSONObject();

        json.put("HadError",result.hadError());
        json.put("ErrorMessage",result.getErrorMessage() == null ? "" : result.getErrorMessage());

        MNJsonElement dataEntryJson = getJSONElement(result.getDataEntry());

        json.put("DataEntry",dataEntryJson != null ? dataEntryJson.jsonArray : JSONObject.NULL);

        return new MNJsonElement(json);
    }

    protected static MNJsonElement getJSONElement(MNScoreProgressProvider.ScoreItem scoreItem) throws JSONException {
        JSONObject json = new JSONObject();

        json.put("UserInfo",getJSONElement(scoreItem.userInfo).jsonObject);
        json.put("Score",scoreItem.score);
        json.put("Place",scoreItem.place);

        return new MNJsonElement(json);
    }

    protected static MNJsonElement getJSONElement(MNBuddyRoomParams buddyRoomParams) throws JSONException {
        JSONObject json = new JSONObject();

        json.put("RoomName",buddyRoomParams.roomName);
        json.put("GameSetId",buddyRoomParams.gameSetId);
        json.put("ToUserIdList",buddyRoomParams.toUserIdList);
        json.put("ToUserSFIdList",buddyRoomParams.toUserSFIdList);
        json.put("InviteText",buddyRoomParams.inviteText);

        return new MNJsonElement(json);
    }

    protected static MNJsonElement getJSONElement(MNJoinRoomInvitationParams params) throws JSONException {
        JSONObject json = new JSONObject();

        json.put("RoomGameId",params.roomGameId);
        json.put("RoomName",params.roomName);
        json.put("RoomSFId",params.roomSFId);
        json.put("InviteText",params.inviteText);
        json.put("RoomGameSetId",params.roomGameSetId);
        json.put("FromUserName",params.fromUserName);
        json.put("FromUserSFId",params.fromUserSFId);

        return new MNJsonElement(json);
    }

    protected static <K, V> Map<K,V> getMapFromJSON(String jsonString,Class<K> keyType,Class<V> valueType) {
        try {
            Map<K,V> resultMap = new HashMap<K,V>();
            K key;
            V value;

            JSONArray mapJsonArray = new JSONArray(jsonString);

            int index = 0;

            if (mapJsonArray.length() % 2 != 0) {
                MNUnity.ELog("Invalid Map serialization: <<" + jsonString + ">>");
                return null;
            }

            while (index < mapJsonArray.length()) {
                if ((keyType.equals(Integer.class)) || (keyType.equals(Integer.TYPE))) {
                    key = getJSONArrayElement(mapJsonArray,index,keyType);
                    value = getJSONArrayElement(mapJsonArray,index + 1,valueType);

                    resultMap.put(key,value);
                    index += 2;
                }
            }

            return resultMap;
        }
        catch (JSONException e) {
            e.printStackTrace();
        }

        return null;
    }

    protected static <T> T getObjectFromJSON(String jsonString,Class<T> type) {
        try {
            if (type.isArray()) {
                if (type.equals(Class.forName("[I"))) {
                    Integer[] intArray = getObjectFromJSONArray(jsonString,Integer.class);

                    return type.cast(integerArrayToPrimitiveIntArray(intArray));
                }
                else if (type.equals(Class.forName("[J"))) {
                    Long[] longArray = getObjectFromJSONArray(jsonString,Long.class);

                    return type.cast(longArrayToPrimitiveLongArray(longArray));
                }
                else if (type.equals(Class.forName("[D"))) {
                    Double[] longArray = getObjectFromJSONArray(jsonString,Double.class);

                    return type.cast(doubleArrayToPrimitiveDoubleArray(longArray));
                }
                else if (type.equals(Class.forName("[Z"))) {
                    Boolean[] longArray = getObjectFromJSONArray(jsonString,Boolean.class);

                    return type.cast(booleanArrayToPrimitiveBooleanArray(longArray));
                }

                return type.cast(getObjectFromJSONArray(jsonString,type.getComponentType()));
            }
            else {
                JSONObject json = new JSONObject(jsonString);
                return getObjectFromJSONObject(json,type);
            }
        }
        catch (JSONException e) {
            e.printStackTrace();
        }
        catch (ClassNotFoundException e) {
            e.printStackTrace();
        }

        return null;
    }

    private static <T> T getObjectFromJSONObject(JSONObject json,Class<T> type) {
        try {
            if (type == MNErrorInfo.class) {
                MNErrorInfo error;
                error = new MNErrorInfo(json.getInt("ActionCode"),json.getString("ErrorMessage"));

                return type.cast(error);
            }
            else if (type == MNGameParams.class) {
                MNGameParams gameParams = new MNGameParams(json.getInt("GameSetId"),
                                                           json.getString("GameSetParams"),
                                                           json.getString("ScorePostLinkId"),
                                                           json.getInt("GameSeed"),
                                                           json.getInt("PlayModel"));

                return type.cast(gameParams);
            }
            else if (type == MNUserInfo.class) {
                String webBaseUrl = null;

                if (MNDirect.getSession() != null) {
                    webBaseUrl = MNDirect.getSession().getWebServerURL();
                }

                MNUserInfo userInfo = new MNUserInfo(json.getLong("UserId"),
                                                     json.getInt("UserSFId"),
                                                     json.getString("UserName"),
                                                     webBaseUrl);

                return type.cast(userInfo);
            }
            else if (type == MNAchievementsProvider.GameAchievementInfo.class) {
                MNAchievementsProvider.GameAchievementInfo achInfo = new MNAchievementsProvider.GameAchievementInfo(json.getInt("Id"),
                                                                                                                    json.getString("Name"),
                                                                                                                    json.getInt("Flags"),
                                                                                                                    json.getString("Description"),
                                                                                                                    json.getString("Params"),
                                                                                                                    json.getInt("Points"));

                return type.cast(achInfo);
            }
            else if (type == MNAchievementsProvider.PlayerAchievementInfo.class) {
                MNAchievementsProvider.PlayerAchievementInfo achInfo = new MNAchievementsProvider.PlayerAchievementInfo(json.getInt("Id"));

                return type.cast(achInfo);
            }
            else if (type == MNVItemsProvider.GameVItemInfo.class) {
                MNVItemsProvider.GameVItemInfo gameVItemInfo = new MNVItemsProvider.GameVItemInfo(json.getInt("Id"),
                                                                                                  json.getString("Name"),
                                                                                                  json.getInt("Model"),
                                                                                                  json.getString("Description"),
                                                                                                  json.getString("Params"));
                return type.cast(gameVItemInfo);
            }
            else if (type == MNVItemsProvider.PlayerVItemInfo.class) {
                MNVItemsProvider.PlayerVItemInfo playerVItemInfo = new MNVItemsProvider.PlayerVItemInfo(json.getInt("Id"),
                                                                                                        json.getLong("Count"));
                return type.cast(playerVItemInfo);
            }
            else if (type == MNVItemsProvider.TransactionVItemInfo.class) {
                MNVItemsProvider.TransactionVItemInfo transVItemInfo = new MNVItemsProvider.TransactionVItemInfo(json.getInt("Id"),
                                                                                                                 json.getLong("Delta"));
                return type.cast(transVItemInfo);
            }
            else if (type == MNVItemsProvider.TransactionInfo.class) {
                JSONArray vItemsJsonArray = json.getJSONArray("VItems");

                MNVItemsProvider.TransactionVItemInfo[] vItems = null;

                if (vItemsJsonArray.length() > 0) {
                    vItems = new MNVItemsProvider.TransactionVItemInfo[vItemsJsonArray.length()];

                    for (int index = 0; index < vItemsJsonArray.length(); index++) {
                        vItems[index] = getObjectFromJSON(vItemsJsonArray.getJSONObject(index).toString(),MNVItemsProvider.TransactionVItemInfo.class);
                    }
                }

                MNVItemsProvider.TransactionInfo transInfo = new MNVItemsProvider.TransactionInfo(json.getLong("ClientTransactionId"),
                                                                                                  json.getLong("ServerTransactionId"),
                                                                                                  json.getLong("CorrUserId"),
                                                                                                  vItems);

                return type.cast(transInfo);
            }
            else if (type == MNVItemsProvider.TransactionError.class) {
                MNVItemsProvider.TransactionError transError = new MNVItemsProvider.TransactionError(json.getLong("ClientTransactionId"),
                                                                                                     json.getLong("ServerTransactionId"),
                                                                                                     json.getLong("CorrUserId"),
                                                                                                     json.getInt("FailReasonCode"),
                                                                                                     json.getString("ErrorMessage"));
                return type.cast(transError);
            }
            else if (type == MNVShopProvider.VShopDeliveryInfo.class) {
                MNVShopProvider.VShopDeliveryInfo transError = new MNVShopProvider.VShopDeliveryInfo(json.getInt("VItemId"),
                                                                                                     json.getLong("Amount"));
                return type.cast(transError);
            }
            else if (type == MNVShopProvider.VShopPackInfo.class) {
                int id = json.getInt("Id");
                String name = json.getString("Name");
                int model = json.getInt("Model");
                String description = json.getString("Description");
                String appParams = json.getString("AppParams");
                int sortPos = json.getInt("SortPos");
                int categoryId = json.getInt("CategoryId");
                int priceItemId = json.getInt("PriceItemId");
                long priceValue = json.getLong("PriceValue");

                JSONArray deliveryJsonArray = json.getJSONArray("Delivery");

                MNVShopProvider.VShopDeliveryInfo[] deliveryInfoItems = null;

                if (deliveryJsonArray.length() > 0) {
                    deliveryInfoItems = new MNVShopProvider.VShopDeliveryInfo[deliveryJsonArray.length()];

                    for (int index = 0; index < deliveryJsonArray.length(); index++) {
                        deliveryInfoItems[index] = getObjectFromJSON(deliveryJsonArray.getJSONObject(index).toString(),MNVShopProvider.VShopDeliveryInfo.class);
                    }
                }

                MNVShopProvider.VShopPackInfo vPackInfo = new MNVShopProvider.VShopPackInfo(id,name);
                vPackInfo.model = model;
                vPackInfo.description = description;
                vPackInfo.appParams = appParams;
                vPackInfo.sortPos = sortPos;
                vPackInfo.categoryId = categoryId;
                vPackInfo.priceItemId = priceItemId;
                vPackInfo.priceValue = priceValue;
                vPackInfo.delivery = deliveryInfoItems.clone();

                return type.cast(vPackInfo);
            }
            else if (type == MNVShopProvider.VShopCategoryInfo.class) {
                int id = json.getInt("Id");
                String name = json.getString("Name");
                int sortPos = json.getInt("SortPos");

                MNVShopProvider.VShopCategoryInfo categoryinfo = new MNVShopProvider.VShopCategoryInfo(id,name);
                categoryinfo.sortPos = sortPos;

                return type.cast(categoryinfo);
            }
            else if (type == MNVShopProvider.VShopPackBuyRequestItem.class) {
                MNVShopProvider.VShopPackBuyRequestItem vPackBuyRequest = new MNVShopProvider.VShopPackBuyRequestItem(json.getInt("Id"),
                                                                                                                      json.getLong("Amount"));
                return type.cast(vPackBuyRequest);
            }
            else if (type == MNVShopProvider.IEventHandler.CheckoutVShopPackSuccessInfo.class) {
                MNVItemsProvider.TransactionInfo transaction = getObjectFromJSONObject(json.getJSONObject("Transaction"),MNVItemsProvider.TransactionInfo.class);

                MNVShopProvider.IEventHandler.CheckoutVShopPackSuccessInfo successInfo = new MNVShopProvider.IEventHandler.CheckoutVShopPackSuccessInfo(transaction);

                return type.cast(successInfo);
            }
            else if (type == MNVShopProvider.IEventHandler.CheckoutVShopPackFailInfo.class) {
                MNVShopProvider.IEventHandler.CheckoutVShopPackFailInfo failInfo = new MNVShopProvider.IEventHandler.CheckoutVShopPackFailInfo(json.getInt("ErrorCode"),
                                                                                                                                               json.getString("ErrorMessage"),
                                                                                                                                               json.getLong("ClientTransactionId"));

                return type.cast(failInfo);
            }
            else if (type == MNGameSettingsProvider.GameSettingInfo.class) {
                MNGameSettingsProvider.GameSettingInfo settingInfo = new MNGameSettingsProvider.GameSettingInfo(json.getInt("Id"),
                                                                                                                json.getString("Name"),
                                                                                                                json.getString("Params"),
                                                                                                                json.getString("SysParams"),
                                                                                                                json.getBoolean("MultiplayerEnabled"),
                                                                                                                json.getBoolean("LeaderboardVisible"));
                return type.cast(settingInfo);
            }
            else if (type == MNScoreProgressProvider.ScoreItem.class) {
                MNScoreProgressProvider.ScoreItem scoreItem = new MNScoreProgressProvider.ScoreItem(getObjectFromJSON(json.getJSONObject("UserInfo").toString(),MNUserInfo.class),
                                                                                                    json.getLong("Score"),
                                                                                                    json.getInt("Place"));

                return type.cast(scoreItem);
            }
            else if (type == MNWSInfoRequestLeaderboard.LeaderboardMode.class) {
                LeaderboardMode mode = null;
                String specificModeClassName = json.getString("Mode");

                if (specificModeClassName.equals("LeaderboardModeCurrentUser")) {
                    mode = new LeaderboardModeCurrentUser(json.getInt("Scope"),
                                                          json.getInt("Period"));
                }
                else if (specificModeClassName.equals("LeaderboardModeAnyGameGlobal")) {
                    mode = new LeaderboardModeAnyGameGlobal(json.getInt("GameId"),
                                                            json.getInt("GameSetId"),
                                                            json.getInt("Period"));
                }
                else if (specificModeClassName.equals("LeaderboardModeAnyUserAnyGameGlobal")) {
                    mode = new LeaderboardModeAnyUserAnyGameGlobal(json.getLong("UserId"),
                                                                   json.getInt("GameId"),
                                                                   json.getInt("GameSetId"),
                                                                   json.getInt("Period"));
                }
                else if (specificModeClassName.equals("LeaderboardModeCurrUserAnyGameLocal")) {
                    mode = new LeaderboardModeCurrUserAnyGameLocal(json.getInt("GameId"),
                                                                   json.getInt("GameSetId"),
                                                                   json.getInt("Period"));
                }
                else {
                    return null;
                }

                return type.cast(mode);
            }
            else if (type == MNBuddyRoomParams.class) {
                MNBuddyRoomParams params = new MNBuddyRoomParams(json.getString("RoomName"),
                                                                 json.getInt("GameSetId"),
                                                                 json.getString("ToUserIdList"),
                                                                 json.getString("ToUserSFIdList"),
                                                                 json.getString("InviteText"));
                return type.cast(params);
            }
            else if (type == MNJoinRoomInvitationParams.class) {
                MNJoinRoomInvitationParams params = new MNJoinRoomInvitationParams(json.getInt("FromUserSFId"),
                                                                                   json.getString("FromUserName"),
                                                                                   json.getInt("RoomSFId"),
                                                                                   json.getString("RoomName"),
                                                                                   json.getInt("RoomGameId"),
                                                                                   json.getInt("RoomGameSetId"),
                                                                                   json.getString("InviteText"));
                return type.cast(params);
            }
            else {
                return null;
            }
        }
        catch (JSONException e) {
            e.printStackTrace();
        }

        return null;
    }

    protected static <T> T[] getObjectFromJSONArray(String jsonString,Class<T> type) throws JSONException {
        JSONArray srcJSONArray = new JSONArray(jsonString);
        @SuppressWarnings("unchecked")
        T[] result = (T[])Array.newInstance(type,srcJSONArray.length());

        for (int index = 0; index < srcJSONArray.length(); index++) {
            result[index] = getJSONArrayElement(srcJSONArray,index,type);

            /*
            if ((type.equals(Integer.class)) || (type.equals(Integer.TYPE))) {
                result[index] = type.cast(srcJSONArray.getInt(index));
            }
            else {
                JSONObject childJSONObject = srcJSONArray.optJSONObject(index);

                if (childJSONObject != null) {
                    result[index] = getObjectFromJSON(childJSONObject.toString(),type);
                }
                else {
                    result[index] = getObjectFromJSON(srcJSONArray.getJSONArray(index).toString(),type);
                }
            }
            */
        }

        return result;
    }

    protected static int[] integerArrayToPrimitiveIntArray(Integer[] srcArray) {
        int[] resultArray = new int[srcArray.length];

        for (int index = 0; index < srcArray.length; index++) {
            resultArray[index] = srcArray[index].intValue();
        }

        return resultArray;
    }

    protected static long[] longArrayToPrimitiveLongArray(Long[] srcArray) {
        long[] resultArray = new long[srcArray.length];

        for (int index = 0; index < srcArray.length; index++) {
            resultArray[index] = srcArray[index].longValue();
        }

        return resultArray;
    }

    protected static double[] doubleArrayToPrimitiveDoubleArray(Double[] srcArray) {
        double[] resultArray = new double[srcArray.length];

        for (int index = 0; index < srcArray.length; index++) {
            resultArray[index] = srcArray[index].doubleValue();
        }

        return resultArray;
    }

    protected static boolean[] booleanArrayToPrimitiveBooleanArray(Boolean[] srcArray) {
        boolean[] resultArray = new boolean[srcArray.length];

        for (int index = 0; index < srcArray.length; index++) {
            resultArray[index] = srcArray[index].booleanValue();
        }

        return resultArray;
    }

    protected static <T> T getJSONArrayElement(JSONArray srcJSONArray,int index,Class<T> elementType) throws JSONException {
        T result;

        if ((elementType.equals(Integer.class)) || (elementType.equals(Integer.TYPE))) {
            result = elementType.cast(srcJSONArray.getInt(index));
        }
        else if ((elementType.equals(Long.class)) || (elementType.equals(Long.TYPE))) {
            result = elementType.cast(srcJSONArray.getLong(index));
        }
        else if ((elementType.equals(Boolean.class)) || (elementType.equals(Boolean.TYPE))) {
            result = elementType.cast(srcJSONArray.getBoolean(index));
        }
        else if ((elementType.equals(Double.class)) || (elementType.equals(Double.TYPE))) {
            result = elementType.cast(srcJSONArray.getDouble(index));
        }
        else if (elementType.equals(String.class)) {
            result = elementType.cast(srcJSONArray.getString(index));
        }
        else {
            JSONObject childJSONObject = srcJSONArray.optJSONObject(index);

            if (childJSONObject != null) {
                result = getObjectFromJSON(childJSONObject.toString(),elementType);
            }
            else {
                result = getObjectFromJSON(srcJSONArray.getJSONArray(index).toString(),elementType);
            }
        }

        return result;
    }
}
