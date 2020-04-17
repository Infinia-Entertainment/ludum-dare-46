using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class AchievementManager : MonoBehaviour
{

    public static Dictionary<string, Achievement> achivementDictionary = new Dictionary<string, Achievement>();
    public Achievement[] achievements;

    private void Start()
    {
        foreach (Achievement achievement in achievements)
        {
            achivementDictionary.Add(achievement.name, achievement);
        }

      //  SaveLoadManager.SaveAchievements(SaveLoadManager.SavingType.DefaultSave);
        SaveLoadManager.LoadAchievements(SaveLoadManager.SavingType.DefaultSave);

        achievements = new Achievement[achivementDictionary.Count];

        for (int i = 0; i < achievements.Length; i++)
        {
            achievements[i] = achivementDictionary[achivementDictionary.Keys.ToArray()[i]];
        }
        //this Stuff is going to be moved to SaveLoadManager

        /*
         *  else if(no save file)
         *  {
         *      initialize achievements from inspector instead.
         *      (or from a default file maybe, since there's already a loading feature why not use it to simplify code xD)
         *  }
         */
    }
}



[Serializable]
public class Achievement
{
    [SerializeField]  internal string name, description;
    [SerializeField]  internal Sprite achievementImage;
    [SerializeField]  internal int progress, progressGoal;
    [SerializeField]  internal bool isCompleted = false;

    void AppendProgress()
    {
        if (!isCompleted)
        {
            progress++;
            if (progress >= progressGoal)
            {
                isCompleted = true;
            }
        }
    }


    //public static AchievementManager.VoidDelegate EnemyKilled;

    //public static void EnemyKilledTrigger()
    //{
    //    EnemyKilled();

    //}

    //private void Start()
    //{
    //    EnemyKilled += KillCount;
    //}

    //private void KillCount()
    //{
    //    Debug.Log("Killed enemy");
    //}

    



}