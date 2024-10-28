using System;
using UnityEngine;
using Random = UnityEngine.Random;


public class Enemy : MonoBehaviour
{
    [SerializeField] private float atackdistance; 
    [SerializeField] public float movespead ;
    [SerializeField] public float atackpower;
    private Rigidbody2D rb;
    public Animator animator;
    public hp_sc _hp;
    public Playercon enStates;
    public LayerMask playerlayer;
    public Collider2D[] hitplayers;
    public Transform eneatackpoint;
    private bool initiateAI;                          //敵が動くか動かないかの判定用フラグ
    private bool closecombat;                         //敵がプレイヤーを攻撃するかしないのか判定用フラグ   
    private bool gotRandom;                           //フレーム(行動)ごとに乱数を更新しないようにするための管理フラグ
    private float storeRandom;                        //浮動小数点型のランダムな値を取得するための変数
    private float cltimer;
    public  float closeRate = 0.5f;
    private float aitimer;
    public  float aiStateLife = 1;                    //どのくらいで循環するかをどのくらいで決定するか
    private float nrRate = 1;
    private float nrtimer;
    public  float ChangeStateTorance = 3;
    
    [Header("atack")]
    private bool atackmove = false;                   //攻撃判定用フラグ
    private float atackrandom;                      
    public float  eneatackradius;                     
    
    [Header("move")]
    private float Horizontal = 0;                     //Enemymoveに使用
    private float Vertical;                           //Jumpに使用
    private bool  movecheck = false;                  //移動判定用フラグ
   
    public enum State   //エネミーの状態遷移
    {
        CLOSE_STATE,        
        NORMAL_STATE,
        RESET,
    }

    public  State _state ;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }
    private void Update()
    {
        Adjust();           //画面外に出ないようにする
        CheckDistance();    //距離間
        EenState();         //状態遷移
        Agent();            //enemyの行動
        
    }
    void Adjust()
    {
        Vector3 pos = Camera.main.WorldToViewportPoint(transform.position);
        pos.x = (pos.x < 0f) ? 0: (pos.x > 1f) ? 1: pos.x;
        pos.y = (pos.y < 0f) ? 0: (pos.y > 1f) ? 1: pos.y;
        transform.position = Camera.main.ViewportToWorldPoint(pos);
    }

    //enemyの行動用
    private void Agent()  
    {
        if(initiateAI)
        {
            ///enemyの初期状態
            _state = State.RESET;
            float multiplier = 0;

            if(!gotRandom)
            {
                storeRandom = ReturnRandom();
                gotRandom = true;
            } 

            if(!closecombat) { multiplier += 30;}
                else { multiplier -= 30;}

            if(storeRandom + multiplier < 50)
            {
                Eneatack();
            }
            else
            {
                Enemymove();
            }
        }
    }
    public void ENEdame(float dmg)
    {
        movecheck = false;
        animator.SetTrigger("enedame");
        _hp.curentenehelth -= dmg;
        _hp.UpdateenehelthUI();
        if(_hp.curentenehelth <= 0)
        {
            Debug.Log("あなたの負け");
            Enedie();
        }
    }
    
    
    public void Enedie() //enemyの死亡処理
    {   
        movecheck = false;
        _hp.curentenehelth = 0;
        animator.SetTrigger("dead");
    }

    private void Eneatack()
    {
        movecheck = false;
        Horizontal = 0;
        //enemyの攻撃処理
        if(!gotRandom)
        {
            storeRandom = ReturnRandom();
            gotRandom = true;
        } 
        atackrandom = storeRandom ;
        if(atackrandom < 80)
        {
            atackmove = true;
            if(atackmove)
            {
                animator.SetBool("atack1",true);
                //プレイヤーに向けてのダメージ処理
                hitplayers = Physics2D.OverlapCircleAll(eneatackpoint.position,eneatackradius,playerlayer);
                foreach(Collider2D hitplayer in hitplayers)
                {
                   Debug.Log(hitplayer.gameObject.name + "に攻撃！！！");
                    hitplayer.GetComponent<Playercon>().Playerdame(atackpower);
                }
            }
            
        }
        else if(storeRandom < 30)
        {
            //animator.SetTrigger("atack2");
            //プレイヤーにたいして
        }
        else if(storeRandom < 50)
        {
            //animator.SetTrigger("atack3");
            //プレイヤーに対しての処理
        }
        animator.SetBool("atack1",false);
    }

    private void Jump()
    {
        //jumpはenemymoveに処理を書く？ー＞処理がややこしくなるかも？
        
    }

    private void Enemymove()
    {
        movecheck = true;
        //enemyの移動
        if(!gotRandom)
        {
            storeRandom = ReturnRandom();
            gotRandom = true;
        } 
    
        float x = Random.Range(-1,1);;
        Horizontal = x;
        x = Horizontal;
        if (Mathf.Abs(x) < 0.1f) 
        {
            x = 0f;
        }

       
        if(storeRandom < 90)
        {
                Debug.Log(animator);
                if(enStates.transform.position.x < this.transform.position.x)
                {
                    rb.velocity = new Vector2(x * movespead,rb.velocity.y);
                }
                else
                {
                    rb.velocity = new Vector2(x * movespead,rb.velocity.y);
                }
        }
        else 
        {
                Debug.Log("アニメーション動いているよ！");
                if(enStates.transform.position.x < this.transform.position.x)
                {
                    Horizontal = -1;
                    x = Horizontal;
                    rb.velocity = new Vector2(x * movespead,rb.velocity.y);
                }
                else
                {
                    Horizontal = 1;
                    x = Horizontal;
                    rb.velocity = new Vector2(x * movespead,rb.velocity.y);
                }
        }
        if(x < 0)
        {
            transform.localScale = new Vector3 (-1,1,1);
        }
        if(x > 0)
        {
            transform.localScale = new Vector3 (1,1,1);
        }
        //animator.SetTrigger("run");
        if(movecheck)
        {
            animator.SetFloat("Speed" , Math.Abs(x));
        }
    
    }

    

    private void Enebrock()
    {
        //enemyの防御処理
        animator.SetTrigger("block");
    }
    

    private void Resetene()
    {
        aitimer += Time.deltaTime;
        if(aitimer > aiStateLife)
        {
            initiateAI = false;
            aitimer = 0;
            gotRandom = false;
            atackmove = false;
            movecheck = false;
            Horizontal = 0;
            storeRandom = ReturnRandom();
            if(storeRandom < 50)
            {
                _state = State.NORMAL_STATE;
            }
            else
            {
                _state = State.CLOSE_STATE;
            }
        }
    }

    //ランダムな値を取得して返すための関数
    private float ReturnRandom()
    {
        float retval = Random.Range(0,101);
        return retval;
    }

    private void Crosestate()
    {
        cltimer += Time.deltaTime;
        if(cltimer > closeRate)
        {
            cltimer = 0;
            initiateAI = true;
        }
    }

    private void Normalstate()
    {
        nrtimer += Time.deltaTime;
        if(nrtimer > nrRate)
        {
            initiateAI = true;
            nrtimer = 0;
        }
    }


    private void EenState()
    {
        //状態遷移
        switch(_state)
        {
            case State.CLOSE_STATE:
                Crosestate();
                break;
            case State.NORMAL_STATE:
                Normalstate();
                break;
            case State.RESET:
                Resetene();
                break;
                
        }
        //Enebrock();     //防御
        //Jump();         //ジャンプ
    }

    //playerとの距離を測る
    private void CheckDistance()
    {
        //enStatesに関しては宣言していないので現段階ではコメントアウト
        float distance = Vector3.Distance(transform.position,enStates.transform.position);

        if(distance < ChangeStateTorance)
        {
            if(_state != State.RESET) _state = State.CLOSE_STATE;

            closecombat = true; 
        }
        else
        {
            if(_state != State.RESET) _state = State.NORMAL_STATE;

            if(closecombat)
            {
                if(!gotRandom)
                {
                    storeRandom = ReturnRandom();
                    gotRandom = true;
                }
            }
        }  
    }

     private void OnDrawGizmosSelected() 
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(eneatackpoint.position,eneatackradius);
    }
}
