using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

public static class Save
{
    private static string path = Application.persistentDataPath + "/players.dat";

    public static void SavePlayer(int score, string name)
    {
        List<PlayerData> playerList = LoadAllPlayers();

        PlayerData newPlayer = new PlayerData(name, score);
        playerList.Add(newPlayer);

        BinaryFormatter formatter = new BinaryFormatter();
        FileStream stream = new FileStream(path, FileMode.Create);

        formatter.Serialize(stream, playerList);
        stream.Close();
    }

    public static List<PlayerData> LoadAllPlayers()
    {
        if (File.Exists(path))
        {
            BinaryFormatter formatter = new BinaryFormatter();
            FileStream stream = new FileStream(path, FileMode.Open);

            List<PlayerData> data = formatter.Deserialize(stream) as List<PlayerData>;
            stream.Close();

            return data;
        }
        else
        {
            return new List<PlayerData>();
        }
    }
}
