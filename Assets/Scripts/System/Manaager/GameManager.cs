using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static CharacterManager;
namespace fighting_game
{
    public class GameManager : MonoBehaviour
    {
        WaitForSeconds oneSec;
        public Transform[] spawnPositions; //キャラクターの生成場所
        public Timecount _timeCount;
        CharacterManager _chmanager;
        UIlevel _level;
        Player_sc _pl;
        hp_sc _hp;
        
        /// <summary>　/// 最大ターン数　/// </summary>
        [SerializeField] public int MaxTurns = 2;
        private int _currentTurn = 1;

        /// <summary> /// 最大タイマー /// </summary>
        [SerializeField] public int MaxTimer = 60;  
        private bool _countdown;

        private void Start()
        {
            //シングルトン
            _chmanager = CharacterManager.GetInstance();
            _level = UIlevel.GetInstance();

            _timeCount.GetComponent<Timecount>();
            //エラー処理
            if(_timeCount == null)
            {
                Debug.LogError("Timecountが見つからない。");
            }

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
        
        /// <summary> /// タイマー表示 /// </summary>
        private void HandleTurnTimer()
        {
            //timeテキスト表示
            //_level.leveltimer.text = _timeCount.GetRemainingTime().ToString();
            if (_timeCount != null && _level.leveltimer != null)
            {
                float remainingTime = _timeCount.GetRemainingTime(); 

                Debug.Log("処理が通っているよ！");
                _level.leveltimer.text = remainingTime.ToString("F2"); // 残り時間を小数点以下2桁で表示
            
                if(_timeCount.GetRemainingTime() < 0)
                {
                    //ターンを終わらす処理
                    EndTurnFunction(true);
                    _countdown = false;
                }
            }
            else
            {
                Debug.LogError("タイマー又はタイムがnull");
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

            //_timeCount.ResetTime();

            yield return InitializePlayers();

            yield return EnableControl();
        }

        /// <summary> /// キャラクター出現処理 /// </summary>
        /// <returns></returns>
        private IEnumerator CreatePlayers()
        {
            for(int i = 0; i <_chmanager.Players.Count; i++)
            {
                if (i >= spawnPositions.Length)
                {
                    Debug.LogWarning("プレイヤーの数に対してスパンポジションが足りていない");
                    break;
                }

                    GameObject obj = Instantiate(_chmanager.Players[i].playerPrefab,
                    spawnPositions[i].position,Quaternion.identity)as GameObject;

                _chmanager.Players[i].plStatus = obj.GetComponent<StateManager>();
                _chmanager.Players[i].plStatus.helthslider = _level.sliders[i];

            }
            yield return new WaitForEndOfFrame();
        }

        /// <summary> /// キャラクター初期化処理 /// </summary>
        /// <returns></returns>
        IEnumerator InitializePlayers()
        {
            for(int i = 0; i < _chmanager.Players.Count; i++)
            {
                _chmanager.Players[i].plStatus.status_helth = 100;
                _chmanager.Players[i].plStatus.transform.position = spawnPositions[i].position;
            }

            yield return new WaitForEndOfFrame();
        }

        /// <summary> /// ゲーム開始時UI処理 /// </summary>
        /// <returns></returns>
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

        /// <summary> /// ターン終了時UIの処理 /// </summary>
        /// <param name="timeout"></param>
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
}


