using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine;

public static class ObjectCopyExtensions
{
    public static T DeepCopy<T>(this T obj)
    {
        Debug.Log(typeof(T));

        if (!typeof(T).IsSerializable)
            throw new ArgumentException("Type must be serializable");

        if (ReferenceEquals(obj, null))
            return default;

        var formatter = new BinaryFormatter();
        using (var stream = new MemoryStream())
        {
            formatter.Serialize(stream, obj);
            stream.Seek(0, SeekOrigin.Begin);
            return (T)formatter.Deserialize(stream);
        }
    }
}
