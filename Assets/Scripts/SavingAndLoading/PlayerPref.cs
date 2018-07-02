﻿using UnityEngine;
using System.Collections;
using System;
using System.IO;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;

//Serializes data into binary
public class PlayerPref {

    public static BinaryFormatter binaryFormatter = new BinaryFormatter();

    public static void Save(string saveTag, object obj) {
        MemoryStream memoryStream = new MemoryStream();
        binaryFormatter.Serialize(memoryStream, obj); //serialize
        string temp = System.Convert.ToBase64String(memoryStream.ToArray());
        PlayerPrefs.SetString(saveTag, temp);
        PlayerPrefs.Save();
    }

    public static object Load(string saveTag) {
        string temp = PlayerPrefs.GetString(saveTag);
        if (temp == string.Empty) {
            return null;
        } else {
            MemoryStream memoryStream = new MemoryStream(System.Convert.FromBase64String(temp));
            return binaryFormatter.Deserialize(memoryStream); //deserialize
        }
    }


}