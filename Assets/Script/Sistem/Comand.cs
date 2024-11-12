using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.Design;
using JetBrains.Annotations;
using Microsoft.Unity.VisualStudio.Editor;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;

public class Command : MonoBehaviour
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
        LEFT_BIT   = 0x0000_0004,
        RIGHT_BIT  = 0x0000_0008,
        ATACK1_BIT = 0x0000_0010,
    }


    public static Command hadouken = new Command(
       60,//入力制限時間(フレーム）
        4,//キーパターン数
          new uint[]
          {
               (uint)(Command.KEYBIT.DOWN_BIT),
               (uint)(Command.KEYBIT.DOWN_BIT | Command.KEYBIT.RIGHT_BIT),
               (uint)(Command.KEYBIT.RIGHT_BIT),
               (uint)(Command.KEYBIT.ATACK1_BIT),
           }
           );


    public Command(int fm, int ct, uint[] ky)
    {
        flame = fm;
        count = ct;
        key = ky;
    }

    [SerializeField] public const int bfcount = 256;
    [SerializeField] public int flame; //入力制限時間
    [SerializeField] public int count; // キーパターンの数(keyの要素数、最大16)
    public uint[] key = new uint[16];  // キーパターン（KEYBITの組み合わせで指定)
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
       int _commandIndex = 0; //コマンドパターンが何番目まで合致したか示す変数
       for(int i = flame - 1;i >= 0; i--)
        {
            if(gkeyComand[i] == hadouken.key[_commandIndex])
            {
                //一致した場合、次のパターンへ
                Debug.Log("コマンド一部成功" + gkeyComand[i]);
                _commandIndex++;
                //すべてのコマンドパターン一致
                if(_commandIndex == hadouken.count)
                {
                    ExecuteComand();
                    break;
                }
            }
            else
            {
                //一致しない場合、コマンドをリセット
                _commandIndex = 0;
            }
        }
    } 
    
    /// <summary> /// コマンド実行処理 /// </summary>
    private void ExecuteComand()
    {
        Debug.Log("コマンド成功");
        //アニメーション
    }
}


