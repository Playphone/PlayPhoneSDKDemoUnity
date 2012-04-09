using System;
using System.Collections;
using System.Collections.Generic;
using PlayPhone.MultiNet.Providers;

namespace PlayPhone.MultiNet.Core
{
  public abstract class MNSerializer
  {/*
    //Commented bacause virtual method with generics are restricted by Full-AOT used for compilation for iOS
    public abstract string Serialize (object obj);
    public abstract T Deserialize<T> (string str);

    //Dictionary serializatoion
    public abstract string SerializeDictionary<TKey,TValue> (IDictionary<TKey,TValue> obj);
    public abstract IDictionary<TKey,TValue> DeserializeDictionary<TKey,TValue> (string str);
    */
  }

  public class MNJsonSerializer : MNSerializer {
    public MNJsonSerializer()
    {
    }

    public string Serialize (object srcObject) {
      MNTools.DetailedLog(string.Format("MNJsonSerializer.Serialize object: {0}",MNTools.SafeToString(srcObject)));

      if (srcObject == null) {
        return "[null]";
      }

      string resultString = null;

      try {
        Type srcObjectType = srcObject.GetType();

        if ((MNTools.IsSimpleType(srcObjectType)) ||
          (MNTools.IsNullableType(srcObjectType)))
        {
          object[] objects = new object[1];
          objects[0] = srcObject;
  
          resultString = MiniJSON.Json.Serialize(objects);
        }
        else if (typeof(Array).IsAssignableFrom(srcObjectType)) {
          Array srcArray = (Array)srcObject;

          if ((MNTools.IsSimpleType(srcObjectType.GetElementType())) ||
            (MNTools.IsNullableType(srcObjectType.GetElementType()))){
            resultString = MiniJSON.Json.Serialize(srcArray);
          }
          else {
            object[] proxyArray = new object [srcArray.Length];

            for (int index = 0;index < srcArray.Length;index++) {
              proxyArray[index] = MNSerializerProxy.ObjectToDictionary(srcArray.GetValue(index),srcObjectType.GetElementType());
            }

            resultString = MiniJSON.Json.Serialize(proxyArray);
          }
        }
        else if (typeof(List<object>).IsAssignableFrom(srcObjectType)) {
          List<object> srcList = (List<object>)srcObject;

          if ((MNTools.IsSimpleType(srcObjectType.GetElementType())) ||
            (MNTools.IsNullableType(srcObjectType.GetElementType()))){
            resultString = MiniJSON.Json.Serialize(srcList);
          }
          else {
            object[] proxyArray = new object [srcList.Count];

            for (int index = 0;index < srcList.Count;index++) {
              proxyArray[index] = MNSerializerProxy.ObjectToDictionary(srcList[index],srcObjectType.GetElementType());
            }
  
            resultString = MiniJSON.Json.Serialize(proxyArray);
          }
        }
        else {
          IDictionary proxyDictionary = MNSerializerProxy.ObjectToDictionary(srcObject,srcObjectType);

          resultString = MiniJSON.Json.Serialize(proxyDictionary);
        }
      }
      catch (Exception e) {
        throw e;
      }

      return resultString;
    }

    public string SerializeDictionary (IDictionary srcDict) {
      MNTools.DetailedLogDictionary("SerializeDictionary",srcDict);

      if (srcDict == null) {
        return Serialize(srcDict);
      }

      List<object> array = new List<object>();

      foreach (object key in srcDict.Keys) {
        array.Add(key);
        array.Add(srcDict[key]);
      }

      return MiniJSON.Json.Serialize(array);
    }

    public object Deserialize (string srcJsonStr,Type targetType) {
      MNTools.DetailedLog(string.Format("Deserialize from {1} to {0})",MNTools.SafeToString(targetType),MNTools.SafeToString(srcJsonStr)));

      if ((srcJsonStr == null) || (srcJsonStr.Length == 0)) {
        throw new MNDeserializationException(srcJsonStr,targetType);
      }

      if (srcJsonStr == "[null]") {
        return null;
      }

      object result = null;

      if ((MNTools.IsSimpleType(targetType)) || (MNTools.IsNullableType(targetType))) {
        List<object> o = (List<object>)Deserialize(srcJsonStr,typeof(List<object>));

        if (o == null) {
          throw new MNDeserializationException(srcJsonStr,targetType);
        }

        if ((o[0] == null) &&
          (!(MNTools.IsNullableType(targetType))) &&
          (!targetType.Equals(typeof(string)))) {
          throw new MNDeserializationException(srcJsonStr,targetType);
        }

        if ((targetType.Equals(typeof(int))) || (targetType.Equals(typeof(int?)))) {
          result = Convert.ToInt32(o[0]);
        }
        else if ((targetType.Equals(typeof(long))) || (targetType.Equals(typeof(long?)))) {
          result = Convert.ToInt64(o[0]);
        }
        else if ((targetType.Equals(typeof(double))) || (targetType.Equals(typeof(double?)))) {
          result = Convert.ToDouble(o[0]);
        }
        else if ((targetType.Equals(typeof(bool))) || (targetType.Equals(typeof(bool?)))) {
          result = Convert.ToBoolean(o[0]);
        }
        else {
          throw new MNDeserializationException(srcJsonStr,targetType);
        }
      }
      else if ((typeof(List<object>).IsAssignableFrom(targetType)) ||
               (typeof(Array).IsAssignableFrom(targetType))) {
        Type elementType = typeof(object);

        if (targetType.HasElementType) {
          elementType = targetType.GetElementType();
        }

        result = DeserializeArray(srcJsonStr,elementType);
      }
      else {
        IDictionary deserializedObject = (IDictionary)MiniJSON.Json.Deserialize(srcJsonStr);

        result = MNSerializerMapper.ObjectFromDictionary(deserializedObject,targetType);
      }

      return result;
    }

    public List<object> DeserializeArray(string srcJsonStr,Type elementType) {
      MNTools.DetailedLog(string.Format("DeserializeArray from {1} to {0}",MNTools.SafeToString(elementType),MNTools.SafeToString(srcJsonStr)));

      List<object> deserializedList = (List<object>)MiniJSON.Json.Deserialize(srcJsonStr);

      List<object> result = DeserializedListToTypedList(deserializedList,elementType);

      return result;
    }

    public IDictionary DeserializeDictionary (string str) {
      MNTools.DetailedLog(String.Format("DeserializeDictionary from {0}",MNTools.SafeToString(str)));

      IDictionary result = new Hashtable();

      try {
        List<object> srcDataArray = (List<object>)MiniJSON.Json.Deserialize(str);

        object _key;
        object _value;

        int index = 0;

        while (index < srcDataArray.Count) {
          _key   = srcDataArray[index    ];
          _value = srcDataArray[index + 1];

          result.Add(_key,_value);

          index += 2;
        }
      }
      catch (Exception e) {
          throw e;
      }

      return result;
    }

    protected List<object> DeserializedListToTypedList(List<object> deserializedList,Type elementType) {
      MNTools.DetailedLog("DeserializedListToTypedList. Elements type: " + MNTools.SafeToString(elementType));

      List<object> result = new List<object>(deserializedList.Count);

      for (int index = 0;index < deserializedList.Count;index++) {
        if (elementType.Equals(typeof(object))) {
          result.Add(deserializedList[index]);
        }
        else if (elementType.Equals(typeof(string))) {
          result.Add((string)deserializedList[index]);
        }
        else if ((MNTools.IsSimpleType(elementType)) || (MNTools.IsNullableType(elementType))){
          if (deserializedList[index] == null) {
             result.Add(null);
          }
          else {
            if ((elementType.Equals(typeof(int))) || (elementType.Equals(typeof(int?)))) {
              result.Add(Convert.ToInt32(deserializedList[index]));
            }
            else if ((elementType.Equals(typeof(long))) || (elementType.Equals(typeof(long?)))) {
              result.Add(Convert.ToInt64(deserializedList[index]));
            }
            else if ((elementType.Equals(typeof(double))) || (elementType.Equals(typeof(double?)))) {
              result.Add(Convert.ToDouble(deserializedList[index]));
            }
            else if ((elementType.Equals(typeof(bool))) || (elementType.Equals(typeof(bool?)))){
              result.Add(Convert.ToBoolean(deserializedList[index]));
            }
            else {
              throw new MNDeserializationException(string.Format("Exception during converting deserialized list {0} to list with elements of type: {1}",MNTools.SafeToString(deserializedList),MNTools.SafeToString(elementType)));
            }
          }
        }
        else if ((typeof(List<object>).IsAssignableFrom(elementType)) ||
                 (typeof(Array).IsAssignableFrom(elementType))) {
          result.Add(DeserializedListToTypedList((List<object>)deserializedList[index],elementType.GetElementType()));
        }
        else {
          result.Add(MNSerializerMapper.ObjectFromDictionary((IDictionary)deserializedList[index],elementType));
        }
      }

      return result;
    }
  }
}
