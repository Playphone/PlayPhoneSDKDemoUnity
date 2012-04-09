using System;

namespace PlayPhone.MultiNet.Core
{
  public class MNException : Exception
  {
    public MNException ()
    {
    }
    
    public MNException (string message) : base (message)
    {
    }
  }

  public class MNNotOnDeviceExcepton : MNException
  {
    public MNNotOnDeviceExcepton ()
    {
    }
  }

  public class MNSerializationException : MNException
  {
    public MNSerializationException ()
    {
    }

    public MNSerializationException (string message) : base (message)
    {
    }

    public MNSerializationException(Type srcType) : base (string.Format("Exception during serialization of type: {0}",MNTools.SafeToString(srcType))) {
    }
  }

  public class MNDeserializationException : MNException
  {
    public MNDeserializationException ()
    {
    }

    public MNDeserializationException (string message) : base (message)
    {
    }

    public MNDeserializationException(string srcString,Type toType) : base (string.Format("Exception during deserialization string {0} to type {1}",MNTools.SafeToString(srcString),MNTools.SafeToString(toType))) {
    }

    public MNDeserializationException(object srcObject,Type toType) : base (string.Format("Don't know how to create object of type {0} from {1}",MNTools.SafeToString(toType),MNTools.SafeToString(srcObject))) {
    }
  }
}

