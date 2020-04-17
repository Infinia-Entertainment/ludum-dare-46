using System;
using System.IO;
using System.Linq;
using UnityEngine;

public static class SaveLoadManager
{
    #region variables

    #region Read_Only

    private static readonly string ACHIEVEMENT_ICON_PATH = "AchievementIcons";
    private static readonly string GAME_SAVE_FOLDER = "Saves";
    private static readonly string ACHIEVEMENTS_SAVE_FOLDER = "Achievements";
    private static readonly string ENCRYPTION_KEY = "12345678901234567890123456789012";//this key is for testing only

    #endregion Read_Only

    #region Enums

    public enum SavingSlot
    {
        Slot1,
        Slot2,
        Slot3
    }

    public enum SavingType
    {
        PlayerSave,
        DefaultSave
    }

    #endregion Enums

    #region Achievement_Structs

    [Serializable]
    private struct _Achievement
    {
        [SerializeField] internal string name, description;
        [SerializeField] internal string achievementImageName;
        [SerializeField] internal int progress, progressGoal;
        [SerializeField] internal bool isCompleted;
    }

    private struct AchievementWrapper
    {
        public _Achievement[] achievements;
    }

    #endregion Achievement_Structs

    #endregion variables

    #region Achievement_Functions

    public static void SaveAchievements(SavingType savingType)
    {
        Debug.Log(Application.persistentDataPath);

        string savingDirectory = Path.Combine(Application.persistentDataPath, ACHIEVEMENTS_SAVE_FOLDER);
        savingDirectory = savingDirectory.Replace(@"\", @"/");

        switch (savingType)
        {
            case SavingType.PlayerSave:
                Path.Combine(savingDirectory, "Achievement_Save");
                break;

            case SavingType.DefaultSave:
                Path.Combine(savingDirectory, "Default_Achievement_Save");
                break;

            default:
                break;
        }

        Achievement[] achievementArray = AchievementManager.achivementDictionary.Values.ToArray();
        _Achievement[] achievementWrapper = new _Achievement[achievementArray.Length];

        for (int i = 0; i < achievementArray.Length; i++)
        {
            achievementWrapper[i].name = achievementArray[i].name;
            achievementWrapper[i].description = achievementArray[i].description;
            achievementWrapper[i].achievementImageName = achievementArray[i].achievementImage.name;
            achievementWrapper[i].progress = achievementArray[i].progress;
            achievementWrapper[i].progressGoal = achievementArray[i].progressGoal;
            achievementWrapper[i].isCompleted = achievementArray[i].isCompleted;
        }

        string outputString = JsonUtility.ToJson(new AchievementWrapper() { achievements = achievementWrapper });

        byte[] encryptedString = RijndaelEncryption.Encrypt(outputString, ENCRYPTION_KEY);

        File.WriteAllBytes(savingDirectory, encryptedString);
    }

    public static void LoadAchievements(SavingType savingType)
    {
        string loadingDirectory = Path.Combine(Application.persistentDataPath, ACHIEVEMENTS_SAVE_FOLDER);
        byte[] encryptedJson;
        string decryptedJson = string.Empty;

        switch (savingType)
        {
            case SavingType.PlayerSave:
                Path.Combine(loadingDirectory, "Achievement_Save");
                break;

            case SavingType.DefaultSave:
                Path.Combine(loadingDirectory, "Default_Achievement_Save");
                break;

            default:
                break;
        }

        loadingDirectory = loadingDirectory.Replace(@"\", @"/");

        encryptedJson = File.ReadAllBytes(loadingDirectory);
        decryptedJson = RijndaelEncryption.Decrypt(encryptedJson, ENCRYPTION_KEY);

        AchievementManager.achivementDictionary.Clear();

        foreach (_Achievement achievement in JsonUtility.FromJson<AchievementWrapper>(decryptedJson).achievements)
        {
            Achievement dictAchievement = new Achievement();

            dictAchievement.name = achievement.name;
            dictAchievement.description = achievement.description;
            dictAchievement.achievementImage = Resources.Load<Sprite>(ACHIEVEMENT_ICON_PATH + '/' + achievement.achievementImageName);
            dictAchievement.progress = achievement.progress;
            dictAchievement.progressGoal = achievement.progressGoal;
            dictAchievement.isCompleted = achievement.isCompleted;

            AchievementManager.achivementDictionary.Add(achievement.name, dictAchievement);
        }
    }

    #endregion Achievement_Functions

    #region Game_Saving_Functions

    public static void SaveSlot(SavingSlot slot)
    {
        switch (slot)
        {
            case SavingSlot.Slot1:
                break;

            case SavingSlot.Slot2:
                break;

            case SavingSlot.Slot3:
                break;

            default:
                break;
        }
    }

    public static void LoadSlot(SavingSlot slot)
    {
        switch (slot)
        {
            case SavingSlot.Slot1:
                break;

            case SavingSlot.Slot2:
                break;

            case SavingSlot.Slot3:
                break;

            default:
                break;
        }
    }

    #endregion Game_Saving_Functions
}