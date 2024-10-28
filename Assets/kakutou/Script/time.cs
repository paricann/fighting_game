using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
public class time : MonoBehaviour
{
    public UnityEngine.UI.Text times;
    private float counttime = 60.0F;

    private void Update()
    {
          // countTimeに、ゲームが開始してからの秒数を格納
         counttime -= Time.deltaTime;
        // 小数2桁にして表示
        GetComponent<Text>().text = counttime.ToString("0");
        if (counttime <= 0.0F)
        {
            counttime = 0.0F;
            SceneManager.LoadScene("GameoverScene");
        }
    }

    
}
