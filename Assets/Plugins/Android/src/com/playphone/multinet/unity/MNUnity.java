package com.playphone.multinet.unity;

import org.json.JSONArray;
import org.json.JSONObject;

import android.util.Log;

import com.playphone.multinet.unity.serializer.MNJsonElement;
import com.playphone.multinet.unity.serializer.MNJsonSerializer;
import com.playphone.multinet.unity.serializer.MNSerializer;
import com.unity3d.player.UnityPlayer;

public class MNUnity {
    private static String  MNUnityLogTag    = "MNUnity";
    private static String  MNUnityLogPrefix = "MNUW:Java:";
    public static MNSerializer serializer = new MNJsonSerializer();
    
    public static final int DEBUG_LEVEL_OFF = 0;
    public static final int DEBUG_LEVEL_NORMAL = 1;
    public static final int DEBUG_LEVEL_DETAILED = 2;
    
    private static int currentDebugLevel = DEBUG_LEVEL_OFF;

    public static void setDebugLevel(int debugLevel) {
    	currentDebugLevel = debugLevel;
    }
    
    public static void MARK() {
        if (currentDebugLevel >= DEBUG_LEVEL_NORMAL) {
            int markedStackTraceElementIndex = 3;
            Log.d(MNUnityLogTag,MNUnityLogPrefix + Thread.currentThread().getStackTrace()[markedStackTraceElementIndex].toString());
        }
    }

    public static void DLog(String message) {
    	DLog(message,DEBUG_LEVEL_NORMAL);
    }
    
    public static void DLog(String message,int debugLevel) {
    	if (currentDebugLevel >= debugLevel) {
            Log.d(MNUnityLogTag,MNUnityLogPrefix + message);
        }
    }
    
    public static void ELog(String message) {
        Log.e(MNUnityLogTag,MNUnityLogPrefix + message);
    }
    

    public static void callUnityFunction(String functionName,Object... params) {
        String paramPassString = null;

        if (params.length == 0) {
            paramPassString = "";
        }
        else {
            JSONArray paramArray = new JSONArray();

            for (int index = 0; index < params.length; index++) {
                if (MNUnity.isObjectTypeSupportedByJSONLib(params[index])) {
                    paramArray.put(params[index]);
                }
                else {
                    MNJsonElement jsonElement = MNJsonSerializer.getJSONElement(params[index]);

                    if (jsonElement != null) {
                        if (jsonElement.isArray()) {
                            paramArray.put(jsonElement.getElement(JSONArray.class));
                        }
                        else {
                            paramArray.put(jsonElement.getElement(JSONObject.class));
                        }
                    }
                    else {
                        MNUnity.DLog(String.format("Can not convert param of type [%s] to JSON string",params[index].getClass().getName()));

                        assert false;
                    }
                }
            }

            paramPassString = paramArray.toString();
        }

        MNUnity.DLog(String.format("UnitySendMessage(\"MNUnityCommunicator\",\"%s\",\"%s\")",functionName,paramPassString));

        UnityPlayer.UnitySendMessage("MNUnityCommunicator",functionName,paramPassString);
    }
    
    public static boolean isObjectTypeSupportedByJSONLib(Object object) {
        boolean result = false;

        if ((object instanceof JSONObject) ||
            (object instanceof JSONArray) ||
            (object instanceof String) ||
            (object instanceof Boolean) ||
            (object instanceof Integer) ||
            (object instanceof Long) ||
            (object instanceof Double) ||
            (object == null)) {
            result = true;
        }

        return result;
    }
}
