using UnityEngine;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

public static class SaveGame{

    /// <summary>
    /// Saves our game data
    /// </summary>
    /// <param name="saveddata">The data we are saving</param>
    public static void SaveGameData(DataToSave saveddata)
    {
        BinaryFormatter formatter = new BinaryFormatter();                  // We set up a formatter so we do not get hacked
        string path = Application.persistentDataPath + "/player.save";      // We set our filepath and filename
        FileStream stream = new FileStream(path, FileMode.Create);          // We open the file in our path

        SaveData data = new SaveData(saveddata);                            // We set the save file to a proper format

        formatter.Serialize(stream, data);                                  // We save the actual data to the file
        stream.Close();                                                     // We close the file
    }

    /// <summary>
    /// Loading our game data
    /// </summary>
    /// <returns></returns>
    public static SaveData LoadGameData()
    {

        string path = Application.persistentDataPath + "/player.save";      // Our file path
        Debug.Log(path);
        if (File.Exists(path))
        {
            BinaryFormatter formatter = new BinaryFormatter();              // We set up a formatter to decode a flie
            FileStream stream = new FileStream(path, FileMode.Open);        // We set our filepath and filename

            SaveData data = formatter.Deserialize(stream) as SaveData;      // We decode our file
            stream.Close();                                                 // We close the file

            return data;                                                    // We return the decoded data
        }
        else
        {
            Debug.LogError("Save file not found in " + path);
            return null;
        }
    }
}
