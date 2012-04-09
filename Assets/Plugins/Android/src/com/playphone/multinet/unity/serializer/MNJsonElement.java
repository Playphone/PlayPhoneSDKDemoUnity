package com.playphone.multinet.unity.serializer;

import org.json.*;

public class MNJsonElement {

    JSONArray  jsonArray  = null;
    JSONObject jsonObject = null;

    public MNJsonElement(Object obj) {
        if (obj != null) {
            if (obj instanceof JSONObject) {
                this.jsonObject = (JSONObject)obj;
            }
            
            if (obj instanceof JSONArray) {
                this.jsonArray = (JSONArray)obj;
            }
        }    
    }
    /*
    public MNJsonElement(JSONObject jsonObject) {
        this.jsonObject = jsonObject;
    }

    public MNJsonElement(JSONArray jsonArray) {
        this.jsonArray = jsonArray;
    }
    */
    
    // public JSONArray getArray() { return jsonArray; }

    // public JSONObject getObject() { return jsonObject; }

    public boolean isArray() {
        return jsonArray != null;
    }

    public boolean isObject() {
        return jsonObject != null;
    }

    public <T> T getElement(Class<T> elementType) {
        if (elementType.equals(JSONObject.class)) {
            return elementType.cast(jsonObject);
        }

        if (elementType.equals(JSONArray.class)) {
            return elementType.cast(jsonArray);
        }

        return null;
    }

    @Override
    public String toString() {
        if (jsonArray != null) {
            return jsonArray.toString();
        }

        if (jsonObject != null) {
            return jsonObject.toString();
        }

        return "";
    }
}
