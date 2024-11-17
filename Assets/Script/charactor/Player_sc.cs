using UnityEngine;

namespace fighting_game
{
    public class Player_sc : charactorbase
    {
        public Collider2D[] _hitEnemys;

        protected override void Update()
        {
            base.Update();
        }

        /// <summary> /// 移動処理 /// </summary>
        protected override void Move()
        {
            base.Move();
            float _move;
            _move = 0f;

            //'A'キーで左に移動
            if (Input.GetKey(KeyCode.A)) { _move = -1f; }
            // 'D' キーで右に移動
            if (Input.GetKey(KeyCode.D)) { _move = 1f;  }

            if (Mathf.Abs(_move) < 0.1f) { _move = 0f;  }

            if (_move < 0f) { transform.localScale = new Vector2(-1, 1);}
            if (_move > 0f) { transform.localScale = new Vector2(1, 1); }

             //animatorの処理

            _rb.velocity = new Vector2(_move * _chSpeed, _rb.velocity.y);
        }


        /// <summary> /// 攻撃処理 /// </summary>
        protected override void Atack()
        {
            Debug.Log("攻撃しているよ");
            _hitEnemys = Physics2D.OverlapCircleAll(_atackPoint.position,_atackradius);
            foreach(Collider2D hitenemy in _hitEnemys)
            {
                Debug.Log(hitenemy.gameObject.name + "に攻撃");
                //敵への攻撃（ダメージ処理）
                //hitenemy.GetComponent<Enemy>().ENEdame();
            }
        }


        /// <summary> /// 防御処理 /// </summary>
        /// /// <param name="_powers"></param>
        protected override void Defense(float[] _powers)
        {
            base.Defense(_powers);
        }

        protected override void Jump()
        {
            //wキー入力でジャンプ（地面についている間）
            if (Input.GetKey(KeyCode.W) && _isGrounded) // 地面にいるかどうかをチェック
            {
                Debug.Log($"{_isGrounded} フラグが立っている。");
                _rb.velocity = new Vector2(_rb.velocity.x, _chjumpForce); // ジャンプ力を設定
            }

        }

        protected override void Dead()
        {
            
            //animatorの処理
            Debug.Log("死亡しました。");

        }
    }
}
