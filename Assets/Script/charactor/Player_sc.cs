using UnityEngine;

namespace fighting_game
{
    public class Player_sc : charactorbase
    {
        public Collider2D[] _hitEnemys;
        public LayerMask _layerMask;
        protected override void Update()
        {
            base.Update();
        }

        protected override void Move()
        {
            base.Move();
            float _move;
            _move = Input.GetAxisRaw("Horizontal") / 1;
            if (Mathf.Abs(_move) < 0.1f)
            {
                _move = 0f;
            }

            if (_move < 0f) { transform.localScale = new Vector2(-1, 1);}
            if (_move > 0f) { transform.localScale = new Vector2(1, 1); }

             //animatorÇÃèàóù

            _rb.velocity = new Vector2(_move * _chSpeed, _rb.velocity.y);
        }


        protected override void Atack()
        {
            Debug.Log("???????");
            _hitEnemys = Physics2D.OverlapCircleAll(_atackPoint.position,_atackradius);
            foreach(Collider2D hitenemy in _hitEnemys)
            {

            }
        }

        protected override void Defense(float[] _powers)
        {
            base.Defense(_powers);
        }

        protected override void Jump()
        {
            
        }

        protected override void Dead()
        {
            
        }
    }
}
