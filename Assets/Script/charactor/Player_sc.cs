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

        /// <summary> /// ˆÚ“®ˆ— /// </summary>
        protected override void Move()
        {
            base.Move();
            float _move;
            _move = 0f;

            //'A'ƒL[‚Å¶‚ÉˆÚ“®
            if (Input.GetKey(KeyCode.A)) { _move = -1f; }
            // 'D' ƒL[‚Å‰E‚ÉˆÚ“®
            if (Input.GetKey(KeyCode.D)) { _move = 1f;  }

            if (Mathf.Abs(_move) < 0.1f) { _move = 0f;  }

            if (_move < 0f) { transform.localScale = new Vector2(-1, 1);}
            if (_move > 0f) { transform.localScale = new Vector2(1, 1); }

             //animator‚Ìˆ—

            _rb.velocity = new Vector2(_move * _chSpeed, _rb.velocity.y);
        }



        /// <summary> /// UŒ‚ˆ— /// </summary>
        protected override void Atack()
        {
            Debug.Log("UŒ‚‚µ‚Ä‚¢‚é‚æ");
            _hitEnemys = Physics2D.OverlapCircleAll(_atackPoint.position,_atackradius);
            foreach(Collider2D hitenemy in _hitEnemys)
            {
                Debug.Log(hitenemy.gameObject.name + "‚ÉUŒ‚");
                //“G‚Ö‚ÌUŒ‚iƒ_ƒ[ƒWˆ—j
                //hitenemy.GetComponent<Enemy>().ENEdame();
            }
        }


        /// <summary> /// –hŒäˆ— /// </summary>
        /// /// <param name="_powers"></param>
        protected override void Defense(float[] _powers)
        {
            base.Defense(_powers);
        }

        protected override void Jump()
        {
            //wƒL[“ü—Í‚ÅƒWƒƒƒ“ƒvi’n–Ê‚É‚Â‚¢‚Ä‚¢‚éŠÔj
            if (Input.GetKey(KeyCode.W) && _isGrounded) // ’n–Ê‚É‚¢‚é‚©‚Ç‚¤‚©‚ðƒ`ƒFƒbƒN
            {
                Debug.Log($"{_isGrounded} ƒtƒ‰ƒO‚ª—§‚Á‚Ä‚¢‚éB");
                _rb.velocity = new Vector2(_rb.velocity.x, _chjumpForce); // ƒWƒƒƒ“ƒv—Í‚ðÝ’è
            }

        }

        protected override void Dead()
        {
            
            //animator‚Ìˆ—
            Debug.Log("Ž€–S‚µ‚Ü‚µ‚½B");

        }

    }
}
