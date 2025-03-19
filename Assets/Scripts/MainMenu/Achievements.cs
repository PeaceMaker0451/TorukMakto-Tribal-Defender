using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[System.Serializable]
public class PlayerAchievements
{
    public string achievementName;
    public string achievementDescription;
    public string achievementRewardDescription;
    public int achievementRewardType;
    public int rewardID;
    public int achievementStatus;
}

public class Achievements : MonoBehaviour
{
    public Progress progress;
    public PlayerAchievements[] allAchievments;
    void OnEnable()
    {
        AchievementsLoad();
    }

    public void AchievementsLoad()
    {
        for(int i = 0; i < allAchievments.Length; i++)
        {
            allAchievments[i].achievementStatus = progress.playerData.achievementsStatus[i];
        }
    }
    
    public void AchievementsSave()
    {
        for(int i = 0; i < allAchievments.Length; i++)
        {
            progress.playerData.achievementsStatus[i] = allAchievments[i].achievementStatus;
        }
    }

}
