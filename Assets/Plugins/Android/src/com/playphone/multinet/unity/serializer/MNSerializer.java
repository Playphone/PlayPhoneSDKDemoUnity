package com.playphone.multinet.unity.serializer;

import java.util.Map;

public abstract class MNSerializer {

    public abstract String serialize(Object obj);
    public abstract <T> T deserialize(String jsonString,Class<T> type);
    
    public abstract <K,V> Map<K,V> deserializeMap(String jsonString,Class<K> keyType,Class<V> valueType);
}
