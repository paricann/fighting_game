using System.Linq.Expressions;
using Unity.VisualScripting;
using UnityEditor.Experimental.GraphView;
using UnityEngine;
using UnityEngine.UIElements;
namespace fighting_game
{
    public class enemy_sc : charactorbase
    {
        
        [SerializeField] private float _atackDistance;   //攻撃距離
        [SerializeField] private float _retreatHealthThreshold; // 遠ざかるための体力閾値

        public Player_sc _plSc;
        public Collider2D[] _hitPlayer;

        private bool initiateAI;                          //敵が動くか動かないかの判定用フラグ
        private bool closecombat;                         //敵がプレイヤーを攻撃するかしないのか判定用フラグ   
        private bool _gotRandom;                           //フレーム(行動)ごとに乱数を更新しないようにするための管理フラグ

        private float _storeRandom;                        //浮動小数点型のランダムな値を取得するための変数
        private float _clTimer;
        private float _aiTimer;
        private float _nrRate = 1;
        private float _nrTimer;

        public float _ChangeStateTorance = 3;
        public float _closeRate = 0.5f;
        public float _aiStateLife = 1;                     //どのくらいで循環するかをどのくらいで決定するか

        [Header("atack")]
        private bool  _atackmove = false;                   //攻撃判定用フラグ
        private float _atackrandom;

        [Header("move")]
        private bool _moveCheck = false; 

        public enum STATE
        {
            CLOSE_STATE, //距離が近い場合
            NOMAL_STATE, //通常時
            RESET,       //状態リセット
            ZERO,        //なんもなし
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
                //エネミーの初期状態
                _state = STATE.RESET;
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

        /// <summary> /// 移動処理 /// </summary>
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
                //攻撃範囲外
                if (_distance > _atackDistance)
                {
                    MoveToward();
                }
                else
                {
                    StopMove();
                }
            }
            
            // プレイヤーの方向に向けてスケールを変更
            if (_rb.velocity.x < 0)
                transform.localScale = new Vector3(-1, 1, 1); // 左向き
            else if (_rb.velocity.x > 0)
                transform.localScale = new Vector3(1, 1, 1);  // 右向き
            //アニメーターの処理
        }

        /// <summary> /// プレイヤー接近 /// </summary>
        private void MoveToward()
        {
            float _moveDirection = _plSc.transform.position.x > transform.position.x ? 1f : -1f;
            _rb .velocity = new Vector2(_moveDirection * _chSpeed, _rb.velocity.y);

            // プレイヤーの方向に向けてスケールを変更
            if (_rb.velocity.x < 0)
                transform.localScale = new Vector3(-1, 1, 1); // 左向き
            else if (_rb.velocity.x > 0)
                transform.localScale = new Vector3(1, 1, 1);  // 右向き
        }

        /// <summary>　/// 止まる処理　/// </summary>
        private void StopMove()
        {
            _rb.velocity = Vector2.zero;
        }

        /// <summary> /// プレイヤーから遠ざかる /// </summary> 
        private void Retreat()
        {
            float _moveDirection = _plSc.transform.position.x > transform.position.x ? -1f : 1f;
            _rb.velocity = new Vector2(_moveDirection * _chSpeed, _rb.velocity.y);
            // プレイヤーの方向に向けてスケールを変更 
            if (_rb.velocity.x < 0) transform.localScale = new Vector3(-1, 1, 1); // 左向き
            else if (_rb.velocity.x > 0) transform.localScale = new Vector3(1, 1, 1); // 右向き
        }

        /// <summary> /// 攻撃処理 /// </summary>
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
                    //TODOアニメーション処理
                    _hitPlayer = Physics2D.OverlapCircleAll(_atackPoint.position, _atackradius, _layerMask);
                    foreach (Collider2D hitplayer in _hitPlayer)
                    {
                        Debug.Log(hitplayer.gameObject.name + "に攻撃！！！");
                        //hitplayer.GetComponent<Playercon>().Playerdame(_chPower); //エラーが出ているため一旦スルー
                    }
                }
            }
            else if(_atackrandom < 30)
            {
                //攻撃処理２
            }
            else if( 50 >_atackrandom && _atackrandom > 30)
            {
                //攻撃処理3
            }
            //アニメーターリセット処理
            
        }

        /// <summary> /// ジャンプ処理 /// </summary>
        protected override void Jump()
        {
            base.Jump();
        }

        /// <summary> /// 防御処理 /// </summary>
        /// <param name="_powers"></param>
        protected override void Defense(float[] _powers)
        {
            base.Defense(_powers);
        }

        /// <summary> /// 死亡処理 /// </summary>
        protected override void Dead()
        {
            base.Dead();
        }

        /// <summary> /// ランダムな値を取得して返す /// </summary>
        /// <returns></returns>
        private float ReturnRandom()
        {
            float retval = Random.Range(0, 101);
            return retval;
        }

        /// <summary> /// 距離測定 /// </summary>
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

        /// <summary> /// 状態遷移 /// </summary>
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
