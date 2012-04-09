package com.playphone.multinet.unity;

import java.lang.reflect.Method;
import java.util.HashMap;

import org.json.JSONArray;
import org.json.JSONException;
import org.json.JSONObject;

import com.playphone.multinet.MNDirect;
import com.playphone.multinet.providers.MNWSInfoRequest;
import com.playphone.multinet.providers.MNWSInfoRequestAnyGame;
import com.playphone.multinet.providers.MNWSInfoRequestAnyUser;
import com.playphone.multinet.providers.MNWSInfoRequestAnyUserGameCookies;
import com.playphone.multinet.providers.MNWSInfoRequestCurrGameRoomList;
import com.playphone.multinet.providers.MNWSInfoRequestCurrGameRoomUserList;
import com.playphone.multinet.providers.MNWSInfoRequestCurrUserBuddyList;
import com.playphone.multinet.providers.MNWSInfoRequestCurrUserSubscriptionStatus;
import com.playphone.multinet.providers.MNWSInfoRequestCurrentUserInfo;
import com.playphone.multinet.providers.MNWSInfoRequestLeaderboard;
import com.playphone.multinet.providers.MNWSInfoRequestSessionSignedClientToken;
import com.playphone.multinet.providers.MNWSInfoRequestSystemGameNetStats;
import com.playphone.multinet.providers.MNWSLoader;

public class MNWSProviderUnity {

    protected static final String          requestIdKey     = "Id";
    protected static final String          requestNameKey   = "Name";
    protected static final String          requestParamsKey = "Parameters";

    public static HashMap<Long,MNWSLoader> loaderMap        = new HashMap<Long,MNWSLoader>();

    protected static MNWSInfoRequestAnyGame prepareMNWSInfoRequestAnyGame(int gameId,final long requestId,final long loaderId) {
        MNWSInfoRequestAnyGame request = new MNWSInfoRequestAnyGame
            (gameId,//deserialize if needed
            new MNWSInfoRequestAnyGame.IEventHandler() {
                @Override
                public void onCompleted(MNWSInfoRequestAnyGame.RequestResult requestResult) {
                    synchronized (loaderMap) {
                        loaderMap.remove(loaderId);
                    }

                    MNUnity.callUnityFunction("MNUM_MNWSInfoRequestEventHandler",requestId,requestResult);
                }
            });

        return request;
    }

    protected static MNWSInfoRequestAnyGame MNWSInfoRequestAnyGame(JSONObject paramsJsonObject,long requestId,long loaderId) throws JSONException {
        return prepareMNWSInfoRequestAnyGame(paramsJsonObject.getInt("gameId"),requestId,loaderId);
    }

    protected static MNWSInfoRequestAnyUser prepareMNWSInfoRequestAnyUser(long userId,final long requestId,final long loaderId) {
        MNWSInfoRequestAnyUser request = new MNWSInfoRequestAnyUser
            (userId,//deserialize if needed
            new MNWSInfoRequestAnyUser.IEventHandler() {
                @Override
                public void onCompleted(MNWSInfoRequestAnyUser.RequestResult requestResult) {
                    synchronized (loaderMap) {
                        loaderMap.remove(loaderId);
                    }

                    MNUnity.callUnityFunction("MNUM_MNWSInfoRequestEventHandler",requestId,requestResult);
                }
            });

        return request;
    }

    protected static MNWSInfoRequestAnyUser MNWSInfoRequestAnyUser(JSONObject paramsJsonObject,long requestId,long loaderId) throws JSONException {
        return prepareMNWSInfoRequestAnyUser(paramsJsonObject.getLong("userId"),requestId,loaderId);
    }

    protected static MNWSInfoRequestAnyUserGameCookies prepareMNWSInfoRequestAnyUserGameCookies(JSONArray userIdListJsonArray,JSONArray cookieKeyListJsonArray,final long requestId,final long loaderId) {
        MNWSInfoRequestAnyUserGameCookies request = new MNWSInfoRequestAnyUserGameCookies
            (MNUnity.serializer.deserialize(userIdListJsonArray.toString(),long[].class),
             MNUnity.serializer.deserialize(cookieKeyListJsonArray.toString(),int[].class),
             new MNWSInfoRequestAnyUserGameCookies.IEventHandler() {
                 @Override
                 public void onCompleted(MNWSInfoRequestAnyUserGameCookies.RequestResult requestResult) {
                     synchronized (loaderMap) {
                         loaderMap.remove(loaderId);
                     }

                     MNUnity.callUnityFunction("MNUM_MNWSInfoRequestEventHandler",requestId,requestResult);
                 }
             });

        return request;
    }

    protected static MNWSInfoRequestAnyUserGameCookies MNWSInfoRequestAnyUserGameCookies(JSONObject paramsJsonObject,long requestId,long loaderId) throws JSONException {
        return prepareMNWSInfoRequestAnyUserGameCookies(paramsJsonObject.getJSONArray("userIdList"),
                                                        paramsJsonObject.getJSONArray("cookieKeyList"),
                                                        requestId,
                                                        loaderId);
    }

    protected static MNWSInfoRequestCurrentUserInfo prepareMNWSInfoRequestCurrentUserInfo(final long requestId,final long loaderId) {
        MNWSInfoRequestCurrentUserInfo request = new MNWSInfoRequestCurrentUserInfo
            (new MNWSInfoRequestCurrentUserInfo.IEventHandler() {
                @Override
                public void onCompleted(MNWSInfoRequestCurrentUserInfo.RequestResult requestResult) {
                    synchronized (loaderMap) {
                        loaderMap.remove(loaderId);
                    }

                    MNUnity.callUnityFunction("MNUM_MNWSInfoRequestEventHandler",requestId,requestResult);
                }
            });

        return request;
    }

    protected static MNWSInfoRequestCurrentUserInfo MNWSInfoRequestCurrentUserInfo(JSONObject paramsJsonObject,long requestId,long loaderId) throws JSONException {
        return prepareMNWSInfoRequestCurrentUserInfo(requestId,loaderId);
    }

    protected static MNWSInfoRequestCurrGameRoomList prepareMNWSInfoRequestCurrGameRoomList(final long requestId,final long loaderId) {
        MNWSInfoRequestCurrGameRoomList request = new MNWSInfoRequestCurrGameRoomList
            (new MNWSInfoRequestCurrGameRoomList.IEventHandler() {
                @Override
                public void onCompleted(MNWSInfoRequestCurrGameRoomList.RequestResult requestResult) {
                    synchronized (loaderMap) {
                        loaderMap.remove(loaderId);
                    }

                    MNUnity.callUnityFunction("MNUM_MNWSInfoRequestEventHandler",requestId,requestResult);
                }
            });

        return request;
    }

    protected static MNWSInfoRequestCurrGameRoomList MNWSInfoRequestCurrGameRoomList(JSONObject paramsJsonObject,long requestId,long loaderId) throws JSONException {
        return prepareMNWSInfoRequestCurrGameRoomList(requestId,loaderId);
    }

    protected static MNWSInfoRequestCurrGameRoomUserList prepareMNWSInfoRequestCurrGameRoomUserList(int roomSFId,final long requestId,final long loaderId) {
        MNWSInfoRequestCurrGameRoomUserList request = new MNWSInfoRequestCurrGameRoomUserList
            (roomSFId,
             new MNWSInfoRequestCurrGameRoomUserList.IEventHandler() {
                 @Override
                 public void onCompleted(MNWSInfoRequestCurrGameRoomUserList.RequestResult requestResult) {
                     synchronized (loaderMap) {
                         loaderMap.remove(loaderId);
                     }

                     MNUnity.callUnityFunction("MNUM_MNWSInfoRequestEventHandler",requestId,requestResult);
                 }
             });

        return request;
    }

    protected static MNWSInfoRequestCurrGameRoomUserList MNWSInfoRequestCurrGameRoomUserList(JSONObject paramsJsonObject,long requestId,long loaderId) throws JSONException {
        return prepareMNWSInfoRequestCurrGameRoomUserList(paramsJsonObject.getInt("roomSFId"),requestId,loaderId);
    }

    protected static MNWSInfoRequestCurrUserBuddyList prepareMNWSInfoRequestCurrUserBuddyList(final long requestId,final long loaderId) {
        MNWSInfoRequestCurrUserBuddyList request = new MNWSInfoRequestCurrUserBuddyList
            (new MNWSInfoRequestCurrUserBuddyList.IEventHandler() {
                @Override
                public void onCompleted(MNWSInfoRequestCurrUserBuddyList.RequestResult requestResult) {
                    synchronized (loaderMap) {
                        loaderMap.remove(loaderId);
                    }

                    MNUnity.callUnityFunction("MNUM_MNWSInfoRequestEventHandler",requestId,requestResult);
                }
            });

        return request;
    }

    protected static MNWSInfoRequestCurrUserBuddyList MNWSInfoRequestCurrUserBuddyList(JSONObject paramsJsonObject,long requestId,long loaderId) throws JSONException {
        return prepareMNWSInfoRequestCurrUserBuddyList(requestId,loaderId);
    }

    protected static MNWSInfoRequestCurrUserSubscriptionStatus prepareMNWSInfoRequestCurrUserSubscriptionStatus(final long requestId,final long loaderId) {
        MNWSInfoRequestCurrUserSubscriptionStatus request = new MNWSInfoRequestCurrUserSubscriptionStatus
            (new MNWSInfoRequestCurrUserSubscriptionStatus.IEventHandler() {
                @Override
                public void onCompleted(MNWSInfoRequestCurrUserSubscriptionStatus.RequestResult requestResult) {
                    synchronized (loaderMap) {
                        loaderMap.remove(loaderId);
                    }

                    MNUnity.callUnityFunction("MNUM_MNWSInfoRequestEventHandler",requestId,requestResult);
                }
            });

        return request;
    }

    protected static MNWSInfoRequestCurrUserSubscriptionStatus MNWSInfoRequestCurrUserSubscriptionStatus(JSONObject paramsJsonObject,long requestId,long loaderId) throws JSONException {
        return prepareMNWSInfoRequestCurrUserSubscriptionStatus(requestId,loaderId);
    }

    protected static MNWSInfoRequestSessionSignedClientToken prepareMNWSInfoRequestSessionSignedClientToken(String payload,final long requestId,final long loaderId) {
        MNWSInfoRequestSessionSignedClientToken request = new MNWSInfoRequestSessionSignedClientToken
            (payload,
             new MNWSInfoRequestSessionSignedClientToken.IEventHandler() {
                 @Override
                 public void onCompleted(MNWSInfoRequestSessionSignedClientToken.RequestResult requestResult) {
                     synchronized (loaderMap) {
                         loaderMap.remove(loaderId);
                     }

                     MNUnity.callUnityFunction("MNUM_MNWSInfoRequestEventHandler",requestId,requestResult);
                 }
             });

        return request;
    }

    protected static MNWSInfoRequestSessionSignedClientToken MNWSInfoRequestSessionSignedClientToken(JSONObject paramsJsonObject,long requestId,long loaderId) throws JSONException {
        return prepareMNWSInfoRequestSessionSignedClientToken(paramsJsonObject.getString("payload"),requestId,loaderId);
    }

    protected static MNWSInfoRequestSystemGameNetStats prepareMNWSInfoRequestSystemGameNetStats(final long requestId,final long loaderId) {
        MNWSInfoRequestSystemGameNetStats request = new MNWSInfoRequestSystemGameNetStats
            (new MNWSInfoRequestSystemGameNetStats.IEventHandler() {
                @Override
                public void onCompleted(MNWSInfoRequestSystemGameNetStats.RequestResult requestResult) {
                    synchronized (loaderMap) {
                        loaderMap.remove(loaderId);
                    }

                    MNUnity.callUnityFunction("MNUM_MNWSInfoRequestEventHandler",requestId,requestResult);
                }
            });

        return request;
    }

    protected static MNWSInfoRequestSystemGameNetStats MNWSInfoRequestSystemGameNetStats(JSONObject paramsJsonObject,long requestId,long loaderId) throws JSONException {
        return prepareMNWSInfoRequestSystemGameNetStats(requestId,loaderId);
    }

    protected static MNWSInfoRequestLeaderboard prepareMNWSInfoRequestLeaderboard(JSONObject leaderboardMode,final long requestId,final long loaderId) {
        MNWSInfoRequestLeaderboard request = new MNWSInfoRequestLeaderboard
            (MNUnity.serializer.deserialize(leaderboardMode.toString(),MNWSInfoRequestLeaderboard.LeaderboardMode.class),
             new MNWSInfoRequestLeaderboard.IEventHandler() {
                 @Override
                 public void onCompleted(MNWSInfoRequestLeaderboard.RequestResult requestResult) {
                     synchronized (loaderMap) {
                         loaderMap.remove(loaderId);
                     }

                     MNUnity.callUnityFunction("MNUM_MNWSInfoRequestEventHandler",requestId,requestResult);
                 }
             });

        return request;
    }

    protected static MNWSInfoRequestLeaderboard MNWSInfoRequestLeaderboard(JSONObject paramsJsonObject,long requestId,long loaderId) throws JSONException {
        return prepareMNWSInfoRequestLeaderboard(paramsJsonObject.getJSONObject("LeaderboardMode"),requestId,loaderId);
    }

    protected static MNWSInfoRequest prepareInfoRequest(JSONObject requestJsonObject,long loaderId) throws Exception {
        MNWSInfoRequest request = null;

        String requestName = requestJsonObject.getString(requestNameKey);

        MNUnity.DLog("prepareInfoRequest.getMethod(" + requestName + ")");

        Method method = MNWSProviderUnity.class.getDeclaredMethod(requestName,JSONObject.class,Long.TYPE,Long.TYPE);
        request = (MNWSInfoRequest)method.invoke(MNWSProviderUnity.class,
                                                 requestJsonObject.getJSONObject(requestParamsKey),
                                                 getRequestId(requestJsonObject),
                                                 loaderId);

        return request;
    }

    protected static long getRequestId(JSONObject requestJsonObject) throws JSONException {
        return requestJsonObject.getLong(requestIdKey);
    }

    /*
        public static void send(String requestJsonString) {
            try {
                JSONObject requestJsonObject = new JSONObject(requestJsonString);
                int requestId = getRequestId(requestJsonObject);
                MNWSInfoRequest request = prepareInfoRequest(requestJsonObject,requestId); //requestId == loaderId for single request

                synchronized (loaderMap) {
                    MNWSLoader loader = MNDirect.getWSProvider().send(request);

                    loaderMap.put(requestId,loader);
                }
            }
            catch (Exception e) {
                e.printStackTrace();
            }
        }
    */
    public static final long LOADER_ID_INVALID = -1;

    public static long send(String requestJsonArray) {
        MNUnity.DLog("MNWSProviderUnity.send(" + requestJsonArray + ")");

        long loaderId = getLoaderId();
        MNWSInfoRequest[] requests = null;
        JSONArray requestArray;

        try {
            requestArray = new JSONArray(requestJsonArray);

            if (requestArray != null) {
                requests = new MNWSInfoRequest[requestArray.length()];

                for (int requestIndex = 0; requestIndex < requestArray.length(); requestIndex++) {
                    JSONObject requestJsonObject = requestArray.getJSONObject(requestIndex);

                    requests[requestIndex] = prepareInfoRequest(requestJsonObject,loaderId);
                }

                synchronized (loaderMap) {
                    MNWSLoader loader = MNDirect.getWSProvider().send(requests);

                    loaderMap.put(loaderId,loader);
                }
            }
        }
        catch (Exception e) {
            e.printStackTrace();
            loaderId = LOADER_ID_INVALID;
        }

        /*
          [{"id":9001, "name":"MNWSInfoRequestAnyGame", "parameters":{"gameId":1}}]
          
          [
           {"id":9001, "name":"MNWSInfoRequestAnyGame", "parameters":{"gameId":1}},
           {"id":9002, "name":"MNWSInfoRequestAnyGame", "parameters":{"gameId":2}},
           {"id":9003, "name":"MNWSInfoRequestAnyGame", "parameters":{"gameId":3}}
          ]
        */

        return loaderId;
    }

    public static void cancelRequest(final long requestId) {
        synchronized (loaderMap) {
            MNWSLoader loader = loaderMap.get(requestId);

            if (loader != null) {
                loaderMap.remove(requestId);
                loader.cancel();
            }
        }
    }

    private static long loaderId = System.currentTimeMillis();

    protected static synchronized long getLoaderId() {
        loaderId++;
        return loaderId;
    }

}
