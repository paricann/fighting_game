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

        

        protected void Start()
        {
            //体力の初期化
            //componentの取得
            //_Chpowerの初期化
        }

        protected void Update() 
        {
            Move();
            Jump();
            Defense();
        } 

        /// <summary> /// damage処理　/// </summary>
        /// <param name="dmg"> /// _chPowerがダメージ </param>
        protected void Damage(float dmg)
        {
            //ダメージの処理

            //体力表示のUIの処理

            //条件文player or enemyの死亡処理

        }

        protected void Move()
        {
            //それぞれの移動処理
        }
         
        protected void Atack()
        {
            //それぞれの攻撃処理
        } 
        
        protected void Jump()
        {
            //それぞれのジャンプの処理
        }

        protected void Defense()
        {
            //それぞれの防御
        }
    }
    
}

