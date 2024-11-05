using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.Design;
using JetBrains.Annotations;
using Microsoft.Unity.VisualStudio.Editor;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;

public class Comand : MonoBehaviour
{
    /// <summary>
    /// キー入力enum
    /// </summary>
    /// 値は二進数
    /// <param>
    /// ZEROはブランク
    /// </param>
    public enum KEYBIT
    {
        ZERO       = 0x0000_0000,
        UP_BIT     = 0x0000_0001,
        DOWN_BIT   = 0x0000_0002,
        LEFT_BIT 　= 0x0000_0004,
        RIGHT_BIT  = 0x0000_0008,
        ATACK1_BIT = 0x0000_0010,
    }


    public static Comand hadouken = new Comand(
       60,//入力制限時間(フレーム）
        4,//キーパターン数
          new uint[]
          {
               (uint)(Comand.KEYBIT.DOWN_BIT),
               (uint)(Comand.KEYBIT.DOWN_BIT | Comand.KEYBIT.RIGHT_BIT),
               (uint)(Comand.KEYBIT.RIGHT_BIT),
               (uint)(Comand.KEYBIT.ATACK1_BIT),
           }
           );


    public Comand(int fm, int ct, uint[] ky)
    {
        flame = fm;
        count = ct;
        key = ky;
    }

    /// <summary> // 入力制限時間// </summary>
    [SerializeField] public int flame;

    /// <summary>/// キーパターンの数(keyの要素数、最大16) /// </summary>
    [SerializeField] public int count;

    /// <summary> /// キーパターン（KEYBITの組み合わせで指定) /// </summary>
    public uint[] key = new uint[16];

    [SerializeField] public const int bfcount = 256;
    public uint[] gkeyComand = new uint[bfcount];

    private void Start()
    {
        //キー入力の初期化
        gkeyComand[0] = (uint)KEYBIT.ZERO;
    }

    private void Update()
    {
        //過去のキー入力分を１つずつシフト
        for(int i = bfcount - 1; i > 0; i--)
        {
            gkeyComand[i] = gkeyComand[i - 1];
        }

        //キー入力を毎フレーム初期化
        uint curentInput = (uint)KEYBIT.ZERO;

        //入力WASD,OR演算->|=
        if (Input.GetKey(KeyCode.D)) { curentInput |= (uint)KEYBIT.RIGHT_BIT;  }
        if (Input.GetKey(KeyCode.S)) { curentInput |= (uint)KEYBIT.DOWN_BIT;   }
        if (Input.GetKey(KeyCode.W)) { curentInput |= (uint) KEYBIT.UP_BIT ;   }
        if (Input.GetKey(KeyCode.U)) { curentInput |= (uint)KEYBIT.ATACK1_BIT; }
        if (Input.GetKey(KeyCode.A)) { curentInput |= (uint)KEYBIT.LEFT_BIT;   }
            
        gkeyComand[0] = curentInput;

        Debug.Log("入力確認" + gkeyComand[0]);
        CheckComand();
        
    }

    /// <summary>/// コマンド入力判定用/// </summary>
    public void CheckComand()
    {
       int n = 0; //コマンドパターンが何番目まで合致したか示す変数
       for(int i = Comand.hadouken.flame - 1;i >= 0; i--)
        {
            if(gkeyComand[i] == hadouken.key[n])
            {
                //コマンドの入力処理
                Debug.Log("成功！！！！");
            }
        }
    } 
    
}


