using System.Linq.Expressions;
using Unity.VisualScripting;
using UnityEditor.Experimental.GraphView;
using UnityEngine;
using UnityEngine.UIElements;
namespace fighting_game
{
    public class enemy_sc : charactorbase
    {
        
        [SerializeField] private float _atackDistance;   //�U������
        [SerializeField] private float _retreatHealthThreshold; // �������邽�߂̗̑�臒l

        public Player_sc _plSc;
        public Collider2D[] _hitPlayer;

        private bool initiateAI;                          //�G�������������Ȃ����̔���p�t���O
        private bool closecombat;                         //�G���v���C���[���U�����邩���Ȃ��̂�����p�t���O   
        private bool _gotRandom;                           //�t���[��(�s��)���Ƃɗ������X�V���Ȃ��悤�ɂ��邽�߂̊Ǘ��t���O

        private float _storeRandom;                        //���������_�^�̃����_���Ȓl���擾���邽�߂̕ϐ�
        private float _clTimer;
        private float _aiTimer;
        private float _nrRate = 1;
        private float _nrTimer;

        public float _ChangeStateTorance = 3;
        public float _closeRate = 0.5f;
        public float _aiStateLife = 1;                     //�ǂ̂��炢�ŏz���邩���ǂ̂��炢�Ō��肷�邩

        [Header("atack")]
        private bool  _atackmove = false;                   //�U������p�t���O
        private float _atackrandom;

        [Header("move")]
        private bool _moveCheck = false; 

        public enum STATE
        {
            CLOSE_STATE, //�������߂��ꍇ
            NOMAL_STATE, //�ʏ펞
            RESET,       //��ԃ��Z�b�g

            ZERO,        //�Ȃ���Ȃ�
        }

        public STATE _state;

        protected override void Update()
        {
            base.Update();
            CheckDistance();
            State();
            Agent();
        }

        private void Agent()
        {
            if(initiateAI)
            {
                //�G�l�~�[�̏������
                _state = STATE.RESET;
                float multiplier = 0;
                if(!_gotRandom)
                {
                    _storeRandom = ReturnRandom();
                    _gotRandom = true;
                }

                if (!closecombat) { _storeRandom += 30f; }
                else { _storeRandom -= 30f; }

                if(_storeRandom < 50f)
                {
                    Atack();
                }
                else
                {
                    Move();
                }
            }
        }

        /// <summary> /// �ړ����� /// </summary>
        protected override void Move()
        {
            base.Move();
            float _distance = Vector3.Distance(transform.position, _plSc.transform.position);
            
            if(_chStatus <= _retreatHealthThreshold)
            {
                Retreat();
            }
            else
            {
                //�U���͈͊O
                if (_distance > _atackDistance)
                {
                    MoveToward();
                }
                else
                {
                    StopMove();
                }
            }
            

            // �v���C���[�̕����Ɍ����ăX�P�[����ύX
            if (_rb.velocity.x < 0)
                transform.localScale = new Vector3(-1, 1, 1); // ������
            else if (_rb.velocity.x > 0)
                transform.localScale = new Vector3(1, 1, 1);  // �E����
            //�A�j���[�^�[�̏���
        }

        /// <summary> /// �v���C���[�ڋ� /// </summary>
        private void MoveToward()
        {
            float _moveDirection = _plSc.transform.position.x > transform.position.x ? 1f : -1f;
            _rb .velocity = new Vector2(_moveDirection * _chSpeed, _rb.velocity.y);

            // �v���C���[�̕����Ɍ����ăX�P�[����ύX
            if (_rb.velocity.x < 0)
                transform.localScale = new Vector3(-1, 1, 1); // ������
            else if (_rb.velocity.x > 0)
                transform.localScale = new Vector3(1, 1, 1);  // �E����
        }

        /// <summary>�@/// �~�܂鏈���@/// </summary>
        private void StopMove()
        {
            _rb.velocity = Vector2.zero;
        }

        /// <summary> /// �v���C���[���牓������ /// </summary> 
        private void Retreat()
        {
            float _moveDirection = _plSc.transform.position.x > transform.position.x ? -1f : 1f;
            _rb.velocity = new Vector2(_moveDirection * _chSpeed, _rb.velocity.y);
            // �v���C���[�̕����Ɍ����ăX�P�[����ύX 
            if (_rb.velocity.x < 0) transform.localScale = new Vector3(-1, 1, 1); // ������
            else if (_rb.velocity.x > 0) transform.localScale = new Vector3(1, 1, 1); // �E����
        }

        /// <summary> /// �U������ /// </summary>
        protected override void Atack()
        {
            base.Atack();

            _atackmove = false;
            if (!_gotRandom)
            {
                _storeRandom = ReturnRandom();
                _gotRandom = true;
            }
            _atackrandom = _storeRandom;
            if (_atackrandom < 80)
            {
                _atackmove = true;
                if (_atackmove)
                {
                    //TODO�A�j���[�V��������
                    _hitPlayer = Physics2D.OverlapCircleAll(_atackPoint.position, _atackradius, _layerMask);
                    foreach (Collider2D hitplayer in _hitPlayer)
                    {
                        Debug.Log(hitplayer.gameObject.name + "�ɍU���I�I�I");
                        //hitplayer.GetComponent<Playercon>().Playerdame(_chPower); //�G���[���o�Ă��邽�߈�U�X���[
                    }
                }
            }
            else if(_atackrandom < 30)
            {
                //�U�������Q
            }
            else if( 50 >_atackrandom && _atackrandom > 30)
            {
                //�U������3
            }
            //�A�j���[�^�[���Z�b�g����
            
        }

        /// <summary> /// �W�����v���� /// </summary>
        protected override void Jump()
        {
            base.Jump();
        }

        /// <summary> /// �h�䏈�� /// </summary>
        /// <param name="_powers"></param>
        protected override void Defense(float[] _powers)
        {
            base.Defense(_powers);
        }

        /// <summary> /// ���S���� /// </summary>
        protected override void Dead()
        {
            base.Dead();
        }

        /// <summary> /// �����_���Ȓl���擾���ĕԂ� /// </summary>
        /// <returns></returns>
        private float ReturnRandom()
        {
            float retval = Random.Range(0, 101);
            return retval;
        }

        /// <summary> /// �������� /// </summary>
        private void CheckDistance()
        {
            float _distance = Vector3.Distance(transform.position, _plSc.transform.position);
            if(_distance < _ChangeStateTorance)
            {
                if (_state != STATE.RESET) _state = STATE.CLOSE_STATE;
                closecombat = true;
            }
            else
            {
                if (STATE.RESET != _state) _state = STATE.NOMAL_STATE;
                closecombat = false;
            }
        }

        /// <summary> /// RESET /// </summary>
        private void Resetene()
        {
            _aiTimer += Time.deltaTime;
            if (_aiTimer > _aiStateLife)
            {
                initiateAI = false;
                _aiTimer = 0;
                _gotRandom = false;
                _atackmove = false;
                _moveCheck = false;
                _storeRandom = ReturnRandom();
                if (_storeRandom < 50)
                {
                    _state = STATE.NOMAL_STATE;
                }
                else
                {
                    _state = STATE.CLOSE_STATE;
                }
            }
        }


        private void Crosestate()
        {
            _clTimer += Time.deltaTime;
            if (_clTimer > _closeRate)
            {
                _clTimer = 0;
                initiateAI = true;
            }
        }

        private void Normalstate()
        {
            _nrTimer += Time.deltaTime;
            if (_nrTimer > _nrRate)
            {
                initiateAI = true;
                _nrTimer = 0;
            }
        }


        /// <summary> /// ��ԑJ�� /// </summary>
        private void State()
        {
            switch (_state)
            {
                case STATE.CLOSE_STATE:
                    Crosestate();
                    break;
                case STATE.NOMAL_STATE:
                    Normalstate();
                    break;
                case STATE.RESET:
                    Resetene();
                    break;

            }
        }

        


    }
}
