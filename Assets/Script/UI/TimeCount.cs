using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
public class Timecount : MonoBehaviour
{
    private float counttime = 60.0F;

    public int TimeCountdown()
    {
        // countTimeに、ゲームが開始してからの秒数を格納
         counttime -= Time.deltaTime;
        return (int)counttime;
    }

    public void ResetTime()
    {
        counttime = 0.0F;
    }

    
}
