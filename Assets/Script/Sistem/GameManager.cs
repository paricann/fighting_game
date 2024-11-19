using fighting_game;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static CharacterManager;

public class GameManager : MonoBehaviour
{
    WaitForSeconds oneSec;
    public Transform[] spawnPositions; //�L�����N�^�[�̐����ꏊ

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
        //�V���O���g��
        _chmane = CharacterManager.GetInstance();
        _level = UIlevel.GetInstance();

        //onesec��������
        oneSec = new WaitForSeconds(1);

        _level.textLine1.gameObject.SetActive(false);
        _level.textLine2.gameObject.SetActive(false);

        StartCoroutine(StartGame());


    }

    private void FixedUpdate()
    {
        //TODO���E�̌��������킹
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
        //time�e�L�X�g�\��
        _level.leveltimer.text = _timeCount.TimeCountdown().ToString();

        if(_timeCount.TimeCountdown() <= 0)
        {
            //TODO�^�[�����I��炷����
            //EndTurnFunction(true);
            _countdown = false;
        }
        
    }

    /// <summary> /// �X�^�[�g�Q�[�� /// </summary>
    /// <returns></returns>
    private IEnumerator StartGame()
    {
        //�v���C���[�o������
        yield return CreatePlayers();

        yield return InitializeTurn();
    }

    /// <summary> /// �^�[�������� /// </summary>
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

    /// <summary> /// �L�����N�^�[�o������ /// </summary>
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

    /// <summary> /// �L�����N�^�[���������� /// </summary>
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

        //�X�^�[�g���J�E���g�_�E��
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

        //TODO���͂�L���ɂ���
        
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

        //TODO���͎󂯂�����
        //StartCoroutine(Endturn());
    }

    IEnumerator EndTurn()
    {
        yield return oneSec;
        yield return oneSec;
        yield return oneSec;

        //�����v���C���[��������
        //PlayerBase _player = FindWinningplayer();

        //if(_player == )
    }

    




}
