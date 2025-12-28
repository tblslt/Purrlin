using UnityEngine;

using System.IO;
public class SaveSystem
{
    private static SaveData saveData = new SaveData();

    [System.Serializable]
    public struct SaveData
    {
        public bool doubleJumpUnlocked;
        public bool dashUnlocked;
        public bool spell1;
        public bool spell2;
        public bool spell3;
        public bool spell4;
        public int maxHealth;
        public int maxMana;

    }

    public static string SaveFileName()
    {
        string saveFile = Application.persistentDataPath + "/save" + ".save";
        return saveFile;
    }

    public void Save()
    {
         
    }

    private static void HandleSaveData()
    {

    }
}
