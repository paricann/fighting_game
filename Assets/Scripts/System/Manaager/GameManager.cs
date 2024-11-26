using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static CharacterManager;
namespace fighting_game
{
    public class GameManager : MonoBehaviour
    {
        WaitForSeconds oneSec;
        public Transform[] spawnPositions; //�L�����N�^�[�̐����ꏊ
        public Timecount _timeCount;
        CharacterManager _chmanager;
        UIlevel _level;
        Player_sc _pl;
        hp_sc _hp;
        
        /// <summary>�@/// �ő�^�[�����@/// </summary>
        [SerializeField] public int MaxTurns = 2;
        private int _currentTurn = 1;

        /// <summary> /// �ő�^�C�}�[ /// </summary>
        [SerializeField] public int MaxTimer = 60;  
        private bool _countdown;

        private void Start()
        {
            //�V���O���g��
            _chmanager = CharacterManager.GetInstance();
            _level = UIlevel.GetInstance();

            _timeCount.GetComponent<Timecount>();
            //�G���[����
            if(_timeCount == null)
            {
                Debug.LogError("Timecount��������Ȃ��B");
            }

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
        
        /// <summary> /// �^�C�}�[�\�� /// </summary>
        private void HandleTurnTimer()
        {
            //time�e�L�X�g�\��
            //_level.leveltimer.text = _timeCount.GetRemainingTime().ToString();
            if (_timeCount != null && _level.leveltimer != null)
            {
                float remainingTime = _timeCount.GetRemainingTime(); 

                Debug.Log("�������ʂ��Ă����I");
                _level.leveltimer.text = remainingTime.ToString("F2"); // �c�莞�Ԃ������_�ȉ�2���ŕ\��
            
                if(_timeCount.GetRemainingTime() < 0)
                {
                    //�^�[�����I��炷����
                    EndTurnFunction(true);
                    _countdown = false;
                }
            }
            else
            {
                Debug.LogError("�^�C�}�[���̓^�C����null");
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

            //_timeCount.ResetTime();

            yield return InitializePlayers();

            yield return EnableControl();
        }

        /// <summary> /// �L�����N�^�[�o������ /// </summary>
        /// <returns></returns>
        private IEnumerator CreatePlayers()
        {
            for(int i = 0; i <_chmanager.Players.Count; i++)
            {
                if (i >= spawnPositions.Length)
                {
                    Debug.LogWarning("�v���C���[�̐��ɑ΂��ăX�p���|�W�V����������Ă��Ȃ�");
                    break;
                }

                    GameObject obj = Instantiate(_chmanager.Players[i].playerPrefab,
                    spawnPositions[i].position,Quaternion.identity)as GameObject;

                _chmanager.Players[i].plStatus = obj.GetComponent<StateManager>();
                _chmanager.Players[i].plStatus.helthslider = _level.sliders[i];

            }
            yield return new WaitForEndOfFrame();
        }

        /// <summary> /// �L�����N�^�[���������� /// </summary>
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

        /// <summary> /// �Q�[���J�n��UI���� /// </summary>
        /// <returns></returns>
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

        /// <summary> /// �^�[���I����UI�̏��� /// </summary>
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
}


