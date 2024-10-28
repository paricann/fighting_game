using System.Collections;
using System.Collections.Generic;
using JetBrains.Annotations;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;

public class Comand : MonoBehaviour
{
   /// <summary>　/// コマンド技名　/// </summary>
    private enum COMANDID
    {
        hadouken = 8,
        tatumaki = 18,
        syouryuuken = 20,
    }

    /// <summary> /// 入力値 /// </summary>
    private enum COMANDNUM
    {
        neutral = 0b_00000000,
        inputup = 0b_00000001,
        inputdown = 0b_00000010,
        inputleft = 0b_00000011,
        inputright = 0b_00000100,
         U_punchi = 0b_00000110,

    }

    /*
    /// <summary> /// 攻撃ボタン /// </summary>
    private enum INPUTBUTTON
    {
        neutral = 0b_00000000,
        U_punchi = 0b_00000110,
        I_tyupan,
        J_kikku,
    }
    */

    /// <summary> /// コマンド入力時間 /// </summary>
    [SerializeField] private float inputsecond = 0.2f; //入力時間
    [SerializeField] private float inputstarttime; //入力開始時間
    private bool inputflg = false; //入力判定用フラグ
    private bool comandflag = false;
    private string comandhold; //testコマンドID照合用変数
    private string  comandname; //enumの中身を照合する用の変数
    
    
    
    /// <summary>　/// コマンド入力用関数　/// </summary>
    private void Update()
    {
        //宣言＆初期化
        float x = 0;
        float y = 0;
    
        //入力
        x = Input.GetAxisRaw("Horizontal") / 1;//方向キー（右左）
        y = Input.GetAxisRaw("Vertical") / 1;  //方向キー(上下)

        ///値確認用Debug.Log
        Debug.Log(x + "xの値確認");
        Debug.Log(y + "yの値確認");

        var comand = COMANDNUM.neutral;  //コマンド初期状態
        //var inputbutton = INPUTBUTTON.neutral; //攻撃ボタン用変数


            if(!inputflg)
            {
                inputstarttime = Time.time;
                inputflg = true;
                comandflag = true;
            }
        
            if(inputflg && Time.time - inputstarttime <= inputsecond)
            {
                
                //コマンド入力された時の処理
                if(x < -1)
                { 
                    comand |= COMANDNUM.inputleft; 
                    Debug.Log(comand + "左入力確認");
                }
                if(x > 1){comand |= COMANDNUM.inputright;}
                if(y < -1){comand |= COMANDNUM.inputdown;}
                if(y > 1){comand |= COMANDNUM.inputup;}
                if(Input.GetKeyDown(KeyCode.U)) {comand &= COMANDNUM.U_punchi;}
                Debug.Log("コマンド入力確認" + comand);

                //入力したコマンドをholdにstring型として代入
                ///上記の代入した変数をfloat型変換
                comandhold = comand.ToString();
                //int.Parse(comandhold);

                //コマンド照合用変数（仮）
                ///enumの中身をstring型に変換
                ///float型に変換
                comandname = COMANDID.hadouken.ToString(); 
                //int.Parse(comandname);
                
                if(comandhold == comandname)
                {
                    Debug.Log("確認");
                    ComandActive();
                }
                

            }
            else if(inputflg && Time.time - inputstarttime > inputsecond)
            {
                //コマンド入力終了時の処理
                inputflg = false;
                comandflag = false;
            }
        
   
    }
    private void ComandActive()
    {
        Debug.Log("波動拳発動");
    }
   
}
