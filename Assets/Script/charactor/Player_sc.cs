using UnityEngine;

namespace fighting_game
{
    public class Player_sc : charactorbase
    {
        public Collider2D[] _hitEnemys;

        protected override void Update()
        {
            base.Update();
            Debug.Log("tamesi");
        }

        /// <summary> /// プレイヤーの動き/// </summary>
        protected override void Move()
        {
            base.Move();
            float _move;
            _move = 0f;

            //'A'で左移動
            if (Input.GetKey(KeyCode.A)) { _move = -1f; }
            // 'D' で右移動
            if (Input.GetKey(KeyCode.D)) { _move = 1f;  }

            if (Mathf.Abs(_move) < 0.1f) { _move = 0f;  }

            if (_move < 0f) { transform.localScale = new Vector2(-1, 1);}
            if (_move > 0f) { transform.localScale = new Vector2(1, 1); }

             //animator‚Ìˆ—

            _rb.velocity = new Vector2(_move * _chSpeed, _rb.velocity.y);
        }



        /// <summary> /// 攻撃処理　/// </summary>
        protected override void Atack()
        {
            Debug.Log("攻撃しているよ");
            _hitEnemys = Physics2D.OverlapCircleAll(_atackPoint.position,_atackradius);
            foreach(Collider2D hitenemy in _hitEnemys)
            {
                Debug.Log(hitenemy.gameObject.name + "攻撃しているよ");
                //ダメージ処理
                //hitenemy.GetComponent<Enemy>().ENEdame();
            }
        }


        /// <summary> /// 防御処理/// </summary>
        /// /// <param name="_powers"></param>
        protected override void Defense(float[] _powers)
        {
            base.Defense(_powers);
        }

        protected override void Jump()
        {
            //地面についているときにWキー入力
            if (Input.GetKey(KeyCode.W) && _isGrounded) 
            {
                Debug.Log($"{_isGrounded} ƒtƒ‰ƒO‚ª—§‚Á‚Ä‚¢‚éB");
                _rb.velocity = new Vector2(_rb.velocity.x, _chjumpForce);
            }

        }

        protected override void Dead()
        {
            
            //アニメーターの処理
            Debug.Log("死亡しました");

        }

    }
}
