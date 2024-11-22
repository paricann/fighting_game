using fighting_game;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static CharacterManager;

public class GameManager : MonoBehaviour
{
    WaitForSeconds oneSec;
    public Transform[] spawnPositions; //キャラクターの生成場所

    CharacterManager _chmane;
    UIlevel _level;
    Timecount _timeCount;
    Player_sc _pl;

    [SerializeField] public int MaxTurns = 2;
    private int _currentTurn = 1;

    [SerializeField] public int MaxTimer = 60;
    private float _internalTimer;
    private bool _countdown;

    private void Start()
    {
        //シングルトン
        _chmane = CharacterManager.GetInstance();
        _level = UIlevel.GetInstance();

        //onesecを初期化
        oneSec = new WaitForSeconds(1);

        _level.textLine1.gameObject.SetActive(false);
        _level.textLine2.gameObject.SetActive(false);

        StartCoroutine(StartGame());


    }

    private void FixedUpdate()
    {
        //TODO左右の向かい合わせ
    }

    private void Update()
    {
        if (_countdown)
        {
            HandleTurnTimer();
        }
    }
    
    private void HandleTurnTimer()
    {
        //timeテキスト表示
        _level.leveltimer.text = _timeCount.TimeCountdown().ToString();

        if(_timeCount.TimeCountdown() <= 0)
        {
            //TODOターンを終わらす処理
            //EndTurnFunction(true);
            _countdown = false;
        }
        
    }

    /// <summary> /// スタートゲーム /// </summary>
    /// <returns></returns>
    private IEnumerator StartGame()
    {
        //プレイヤー出現処理
        yield return CreatePlayers();

        yield return InitializeTurn();
    }

    /// <summary> /// ターン初期化 /// </summary>
    /// <returns></returns>
    private IEnumerator InitializeTurn()
    {
        _level.textLine1.gameObject.SetActive(false);
        _level.textLine2.gameObject.SetActive(false);

        //timereset
        _timeCount.ResetTime();

        yield return InitializePlayers();

        yield return EnableControl();
    }

    /// <summary> /// キャラクター出現処理 /// </summary>
    /// <returns></returns>
    private IEnumerator CreatePlayers()
    {
        for(int i = 0; i <_chmane.Players.Count; i++)
        {
            GameObject obj = Instantiate(_chmane.Players[i].playerPrefab,
                spawnPositions[i].position,Quaternion.identity)as GameObject;
           
        }
        yield return new WaitForEndOfFrame();
    }

    /// <summary> /// キャラクター初期化処理 /// </summary>
    /// <returns></returns>
    IEnumerator InitializePlayers()
    {
        for(int i = 0; i < _chmane.Players.Count; i++)
        {
            _pl.transform.position = spawnPositions[i].position;
        }

        yield return new WaitForEndOfFrame();
    }

    IEnumerator EnableControl()
    {
        _level.textLine1.gameObject.SetActive(true);
        _level.textLine1.text = "Turn" + _currentTurn;
        _level.textLine1.color = Color.white;
        yield return oneSec;
        yield return oneSec;

        //スタート時カウントダウン
        _level.textLine1.text = "3";
        _level.textLine1.color = Color.green;
        yield return oneSec;

        _level.textLine1.text = "2";
        _level.textLine1.color = Color.yellow;
        yield return oneSec;

        _level.textLine1.text = "1";
        _level.textLine1.color = Color.red;
        yield return oneSec;

        _level.textLine1.color = Color.white;
        _level.textLine1.text = "Fight!!!!!";

        //TODO入力を有効にする
        
        yield return oneSec;
        _level.textLine1.gameObject.SetActive(false);
        _countdown = true;
        
       
    }

    public void EndTurnFunction(bool timeout = false)
    {
        _countdown = false;
        _level.leveltimer.text = MaxTimer.ToString();

        if (timeout)
        {
            _level.textLine1.gameObject.SetActive(true) ;
            _level.textLine1.text = "Time Out!!!";
            _level.textLine1.color= Color.red;
        }
        else
        {
            _level.textLine1.gameObject.SetActive(true);
            _level.textLine1.text = "K.O";
            _level.textLine1.color = Color.red;
        }

        //TODO入力受けつけ無効
        //StartCoroutine(Endturn());
    }

    IEnumerator EndTurn()
    {
        yield return oneSec;
        yield return oneSec;
        yield return oneSec;

        //勝利プレイヤーを見つける
        //PlayerBase _player = FindWinningplayer();

        //if(_player == )
    }

    




}
