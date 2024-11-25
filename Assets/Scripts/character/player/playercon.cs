
using UnityEngine;

namespace fighting_game
{

    public class Playercon : MonoBehaviour
    {

        [SerializeField] public float power;
        [SerializeField] public float movespead;
        [SerializeField] private float jumpforce;
        [SerializeField] private float Gravity;

        public Transform atackpoint;
        public float atackradius;
        public LayerMask enemylayer;
        public Rigidbody2D rb;
        public Animator animator;
        public Collider2D[] hitenemys;
        public hp_sc _hp;
        private string colision = "yuka1";
        private bool _isground = false;

        void Start()
        {
            rb = GetComponent<Rigidbody2D>();
            animator = GetComponent<Animator>();
        }

        void Update()
        {
            //TODO 敵の方向に必ず向くようにしたい
            //TODO 敵に背を向けずに後ずさりたい　ー＞animation?

            Movement(); //移動処理

            //ジャンプ処理
            if (_isground == true) Jump();
            //画面外に出ないよう
            Adjust();

            //攻撃処理
            //コントローラーの🔳ボタンの処理
            if (Input.GetKey(KeyCode.U)) Atackmove();

        }

        /// <summary> /// 移動関数 /// </summary>
        private void Movement()
        {
            float x;
            x = Input.GetAxisRaw("Horizontal") / 1;//方向キー（横）

            if (Mathf.Abs(x) < 0.1f)
            {
                x = 0f;
            }

            Debug.Log(x);
            if (x < 0)
            {
                transform.localScale = new Vector3(-1, 1, 1);
            }
            if (x > 0)
            {
                transform.localScale = new Vector3(1, 1, 1);
            }
            animator.SetFloat("Speed", Mathf.Abs(x));
            rb.velocity = new Vector2(x * movespead, rb.velocity.y);

        }
        /// <summary> /// 画面外に出さない /// </summary>
        private void Adjust()
        {
            Vector3 pos = Camera.main.WorldToViewportPoint(transform.position);
            pos.x = (pos.x < 0f) ? 0 : (pos.x > 1f) ? 1 : pos.x;
            pos.y = (pos.y < 0f) ? 0 : (pos.y > 1f) ? 1 : pos.y;
            transform.position = Camera.main.ViewportToWorldPoint(pos);
        }

        /// <summary>/// 攻撃関数　/// </summary>
        private void Atackmove()
        {
            Debug.Log("攻撃しているよ");
            animator.SetTrigger("atack");
            //当たり判定処理
            hitenemys = Physics2D.OverlapCircleAll(atackpoint.position, atackradius, enemylayer);
            foreach (Collider2D hitenemy in hitenemys)
            {
                Debug.Log(hitenemy.gameObject.name + "に攻撃");
                hitenemy.GetComponent<Enemy>().ENEdame(power);
            }

        }
        //当たり判定の円に色を付ける関数
        private void OnDrawGizmosSelected()
        {
            Gizmos.color = Color.red;
            Gizmos.DrawWireSphere(atackpoint.position, atackradius);
        }

        private void Jump()
        {
            float y;
            y = Input.GetAxis("Vertical");
            if (Mathf.Abs(y) < 0.1f)
            {
                y = 0f;
            }
            Debug.Log(y);
            animator.SetFloat("Jump", Mathf.Abs(y));
            if (y > 0)
            {
                rb.velocity = new Vector2(rb.velocity.x, y * jumpforce);
            }
            _isground = false;
        }

        public void OnCollisionEnter2D(Collision2D other)
        {
            Debug.Log("地面に当たっているよ");
            if (_isground == false) { _isground = true; }
        }

        /// <summary>　/// プレイヤーダメージ処理　/// </summary>/// <param name="dmg"></param>
        public void Playerdame(float dmg)
        {
            animator.SetTrigger("dame");
            //_hp.curenthelth -= (int)dmg;
            _hp.UpdateplayhelthUI();
            //if (_hp.curenthelth <= 0)
            //{
            //    Debug.Log("Playerの負け");
            //    Playerdead();
            //}
        }

        /// <summary>　/// 死亡処理　/// </summary>
        public void Playerdead()
        {
            //_hp.curenthelth = 0;
            //playerの死亡処理
            animator.SetBool("dead", true);
            Debug.Log("Playerdie");
        }


    }
}
