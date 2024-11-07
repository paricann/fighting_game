using UnityEngine;
namespace fighting_game
{
    public class charactorbase : MonoBehaviour
    {

        [SerializeField] protected float   _chSpeed;
        [SerializeField] protected float   _chStatus;
        [SerializeField] protected float[] _chPower;
        [SerializeField] protected float   _chjumpForce;
        [SerializeField] protected float   _gravity;
        [SerializeField] protected bool    _isGrounded;
        [SerializeField] protected bool    _isDead;

        protected float          _atackradius;
        protected Transform      _atackPoint;
        protected SpriteRenderer _sprite;
        protected Rigidbody2D    _rb;
        protected Collider2D     _hitColi;
        protected Animator _animator;
        
        protected hp_sc _hp;

        /// <summary> /// hp_scへ体力受け渡し/// </summary>
        /// <returns>最大体力</returns>
        public float Maxhelth()
        {
            float _maxHelth = _chStatus;
            return _maxHelth;
        }

        protected void Start()
        {
            //体力の初期化
            //componentの取得
            _rb = GetComponent<Rigidbody2D>();
            _animator = GetComponent<Animator>(); 
            //_Chpowerの初期化
        }

        protected void Update() 
        {
            Adjust();
            Move();
            Jump();
            Defense(_chPower);
            Damage(_chPower);
        }

        /// <summary> /// 画面外に出さない処理 /// </summary>
        private void Adjust()
        {
            Vector3 pos = Camera.main.WorldToViewportPoint(transform.position);
            pos.x = (pos.x < 0f) ? 0f: (pos.x > 1f) ? 1f : pos.x;
            pos.y = (pos.y < 0f) ? 0f: (pos.y > 1f) ? 1f : pos.y;
            transform.position = Camera.main.ViewportToWorldPoint(pos);
        }

        /// <summary> /// 当たり判定の円に色をつける /// </summary>
        private void OnDrawGizmosSelected()
        {
            Gizmos.color = Color.red;
            Gizmos.DrawWireSphere(_atackPoint.position,_atackradius);
        }


        /// <summary> /// damage処理　/// </summary>
        /// <param name="dmg"> /// _chPowerがダメージ </param>
        protected void Damage(float[] _dmgs)
        {
            //アニメーターの処理
            foreach (float dm in _dmgs)
            {
                //ダメージの処理
            }
            //体力表示のUIの処理

            //条件文player or enemyの死亡処理
            Dead();

        }

        /// <summary> /// 各キャラの動き /// </summary>
        protected void Move()
        {
            //それぞれの移動処理
        }
         
        /// <summary> /// 各キャラの攻撃 /// </summary>
        protected void Atack()
        {
            //それぞれの攻撃処理
        } 
        
        /// <summary> /// 各キャラのジャンプ処理 /// </summary>
        protected void Jump()
        {
            //それぞれのジャンプの処理
        }

        /// <summary> /// 各キャラの防御/// </summary>
        /// <param name="_power">  防御に関連するパワー(配列) </param>
        protected void Defense(float[] _powers)
        {
            //それぞれの防御
            foreach (float power in _powers)
            {
                Debug.Log("防御力" + power);
            }
        }

        /// <summary> /// 各キャラの死亡処理 /// </summary>
        protected void Dead()
        {
            //各死亡処理
            //animatorによるアニメーション処理
        }
    }
    
}

