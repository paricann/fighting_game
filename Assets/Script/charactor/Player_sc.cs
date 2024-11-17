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

        /// <summary> /// �ړ����� /// </summary>
        protected override void Move()
        {
            base.Move();
            float _move;
            _move = 0f;

            //'A'�L�[�ō��Ɉړ�
            if (Input.GetKey(KeyCode.A)) { _move = -1f; }
            // 'D' �L�[�ŉE�Ɉړ�
            if (Input.GetKey(KeyCode.D)) { _move = 1f;  }

            if (Mathf.Abs(_move) < 0.1f) { _move = 0f;  }

            if (_move < 0f) { transform.localScale = new Vector2(-1, 1);}
            if (_move > 0f) { transform.localScale = new Vector2(1, 1); }

             //animator�̏���

            _rb.velocity = new Vector2(_move * _chSpeed, _rb.velocity.y);
        }


        /// <summary> /// �U������ /// </summary>
        protected override void Atack()
        {
            Debug.Log("�U�����Ă����");
            _hitEnemys = Physics2D.OverlapCircleAll(_atackPoint.position,_atackradius);
            foreach(Collider2D hitenemy in _hitEnemys)
            {
                Debug.Log(hitenemy.gameObject.name + "�ɍU��");
                //�G�ւ̍U���i�_���[�W�����j
                //hitenemy.GetComponent<Enemy>().ENEdame();
            }
        }


        /// <summary> /// �h�䏈�� /// </summary>
        /// /// <param name="_powers"></param>
        protected override void Defense(float[] _powers)
        {
            base.Defense(_powers);
        }

        protected override void Jump()
        {
            //w�L�[���͂ŃW�����v�i�n�ʂɂ��Ă���ԁj
            if (Input.GetKey(KeyCode.W) && _isGrounded) // �n�ʂɂ��邩�ǂ������`�F�b�N
            {
                Debug.Log($"{_isGrounded} �t���O�������Ă���B");
                _rb.velocity = new Vector2(_rb.velocity.x, _chjumpForce); // �W�����v�͂�ݒ�
            }

        }

        protected override void Dead()
        {
            
            //animator�̏���
            Debug.Log("���S���܂����B");

        }
    }
}
