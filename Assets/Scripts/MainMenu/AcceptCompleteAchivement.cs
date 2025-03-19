using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AcceptCompleteAchivement : MonoBehaviour
{
    public AchivementCntrl achivementCntrl;
    public void OnPress()
    {
        achivementCntrl.GetReward();

    }
}
