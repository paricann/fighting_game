using fighting_game;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class hp_sc : MonoBehaviour
{
    public charactorbase _chbase;
    [SerializeField] public Slider slider;
    public float curenthelth = 0; 
    public float curentenehelth = 0;

    private void Start()
    {
        Debug.Log("ここまで来てるよ");
        curenthelth = _chbase.Maxhelth();      //初期体力MAX
        curentenehelth = _chbase.Maxhelth();   //上と同じ
        UpdateenehelthUI();
        UpdateplayhelthUI();
    }

    /// <summary> /// player体力減少処理 /// </summary>　
    /// /// <param name="dmgs">_chbaseの_chpower</param>
    public void Hitplayerdamge(float[] dmgs)
    {
        Debug.Log(curenthelth);
        foreach( float dmg in dmgs)
        {
            curenthelth -= dmg;
        }
        UpdateplayhelthUI();
    }

    /// <summary>/// enemy体力減少処理/// </summary> ///
    /// <param name="dmgs">_chbaseの_chpower </param>
    public void Hitenemydame(float[] dmgs)
    {
        Debug.Log(curentenehelth);
        foreach (float dmg in dmgs)
        {
            curenthelth -= dmg;
        }
        UpdateenehelthUI();
    }
    
    /// <summary> /// playerのスライダー増減処理 /// </summary>
    public void UpdateplayhelthUI()
    {
        Debug.Log("playerhelth");
        slider.value = curenthelth / _chbase.Maxhelth();
    }

    /// <summary> /// enemyのスライダー増減処理 /// </summary>
    public void UpdateenehelthUI()
    {
        Debug.Log("enehelth");
        slider.value = curentenehelth / _chbase.Maxhelth();
    }

}
