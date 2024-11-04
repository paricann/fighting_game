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
        ATACK1_BIT = 0x0000_0016,
    }

    /// <summary> // 入力制限時間// </summary>
    [SerializeField] public int flame;

    /// <summary>/// キーパターンの数(keyの要素数、最大16) /// </summary>
    [SerializeField] public int count;

    /// <summary> /// キーパターン（KEYBITの組み合わせで指定) /// </summary>
    public uint[] key = new uint[16];

    [SerializeField] public const int bfcount = 256;
    public uint[] gkeyComand = new uint[bfcount];

    private void Update()
    {
        //過去のキー入力分を１つずつずらす
        for(int i = bfcount - 1; i > 0; i--)
        {
            gkeyComand[i] = gkeyComand[i - 1];
        }

        //今回のフレームで入力されたキーを保存する
        gkeyComand[0] = (uint)KEYBIT.ZERO;
        //入力WASD,OR演算->|=
        if (Input.GetKeyDown(KeyCode.A)) { gkeyComand[0] |= (uint)KEYBIT.LEFT_BIT;   }
        if (Input.GetKeyDown(KeyCode.D)) { gkeyComand[0] |= (uint)KEYBIT.RIGHT_BIT;  }
        if (Input.GetKeyDown(KeyCode.S)) { gkeyComand[0] |= (uint)KEYBIT.DOWN_BIT;   }
        if (Input.GetKeyDown(KeyCode.W)) { gkeyComand[0] |= (uint)(KEYBIT.UP_BIT);   }

        Debug.Log(gkeyComand[0] + "コマンド入力確認");

    }


    public Comand(int fm,int ct, uint[] ky) 
    {
        flame = fm;
        count = ct;
        key = ky;
    }
}

public static class CommandPattern
{
    public static Comand gcomandRed = new Comand(
        60,
        4,
        new uint[]
        {
           
        }
        );
}
