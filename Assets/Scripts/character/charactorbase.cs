using UnityEngine;
namespace fighting_game
{
    public class charactorbase : MonoBehaviour
    {

        [SerializeField] protected float   _chSpeed;             //キャラの移動速度
        [SerializeField] protected float   _chStatus;            //キャラの体力
        [SerializeField] protected float[] _chPower;             //キャラの攻撃力[配列で一つずつ管理]
        [SerializeField] protected float   _chjumpForce;
        [SerializeField] protected float   _gravity;
        [SerializeField] protected bool    _isGrounded;
        [SerializeField] protected bool    _isDead;
        [SerializeField]protected float          _atackradius;
        [SerializeField] protected  Transform    _atackPoint;

        protected SpriteRenderer _sprite;
        protected Rigidbody2D    _rb;
        protected Collider2D     _hitCharcter;
        protected Animator _animator;
        public LayerMask _layerMask;
        protected hp_sc _hp;
        protected StateManager _stManager;

        protected virtual void Start()
        {   
            //componentの取得
            _rb = GetComponent<Rigidbody2D>();   
            _animator = GetComponent<Animator>(); 
        }

        protected virtual void Update() 
        {
            Adjust();
            Move();
            Jump();
            //Defense(_chPower);
            //Damage(_chPower);
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
        protected virtual void OnDrawGizmosSelected()
        {
            Gizmos.color = Color.red;
            Gizmos.DrawWireSphere(_atackPoint.position,_atackradius);
        }


        /// <summary> /// damage処理　/// </summary>
        /// <param name="dmg"> /// _chPowerがダメージ </param>
        protected virtual void Damage(float[] _dmgs)
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
        protected virtual void Move()
        {
            //それぞれの移動処理
        }
         
        /// <summary> /// 各キャラの攻撃 /// </summary>
        protected virtual void Atack()
        {
            //それぞれの攻撃処理
        } 
        
        /// <summary> /// 各キャラのジャンプ処理 /// </summary>
        protected virtual void Jump()
        {
            //それぞれのジャンプの処理
        }

        /// <summary> /// 各キャラの防御/// </summary>
        /// <param name="_power">  防御に関連するパワー(配列) </param>
        protected virtual void Defense(float[] _powers)
        {
            //それぞれの防御
            foreach (float power in _powers)
            {
                Debug.Log("防御力" + power);
            }
        }

        /// <summary> /// 各キャラの死亡処理 /// </summary>
        protected virtual void Dead()
        {
            //各死亡処理
            //animatorによるアニメーション処理
        }
    }
    
}

