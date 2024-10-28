using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class hp_sc : MonoBehaviour
{
    public Enemy ene;
    public Playercon _player;
    [SerializeField] public Slider slider;
    [SerializeField] public float maxhelth;
    public float curenthelth = 0; 
    public float curentenehelth = 0;

    private void Start()
    {
        Debug.Log("ここまで来てるよ");
        curenthelth = maxhelth;      //初期体力MAX
        curentenehelth = maxhelth;   //上と同じ
        UpdateenehelthUI();
        UpdateplayhelthUI();
    }

    public void Hitplayerdamge(float dmg)
    {
        Debug.Log(curenthelth);
        curenthelth -= dmg;
        UpdateplayhelthUI();
    }   

    public void Hitenemydame(float dmg)
    {
        Debug.Log(curentenehelth);
        curentenehelth -= dmg;
        UpdateenehelthUI();
    }
    
    public void UpdateplayhelthUI()
    {
        Debug.Log("playerhelth");
        slider.value = curenthelth / maxhelth;
    }

    public void UpdateenehelthUI()
    {
        Debug.Log("enehelth");
        slider.value = curentenehelth / maxhelth;
    }

}
