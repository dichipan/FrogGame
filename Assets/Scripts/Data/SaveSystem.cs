using UnityEngine;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

public static class SaveSystem 
{
    public static void SavePlayerData(Player player)
    {
        BinaryFormatter formatter = new BinaryFormatter();
        string path = Application.persistentDataPath + "/player.bin";
        FileStream stream = new FileStream(path, FileMode.Create);
        SaveData data = new SaveData(player);
        formatter.Serialize(stream, data);
        stream.Close();
    }

    public static SaveData LoadPlayer(Player player)
    {
        string path = Application.persistentDataPath + "/player.bin";
        if (File.Exists(path))
        {
            BinaryFormatter formatter = new BinaryFormatter();
            FileStream stream = new FileStream(path, FileMode.Open);
            SaveData data = formatter.Deserialize(stream) as SaveData;
            stream.Close();
            return data;
        }
        else
        {
            SavePlayerData(player);
            return LoadPlayer(player);
        }
    }
}
