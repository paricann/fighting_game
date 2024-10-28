using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class background : MonoBehaviour
{
    [SerializeField] Sprite[] _backSprits;
    public GameObject cam;
    [SerializeField] float   scrollspead;
    // Start is called before the first frame update
    void Start()
    {
        for(int i = 0;i < _backSprits.Length;i++)
        {
             var sprite = _backSprits[i];
        }
    }
     private void Update() 
    {
        //float dist = (cam.transform.position.x * (1 - paraeffe)); //視差硬貨に使用するパラメーター
        this.transform.position += Vector3.right * scrollspead * Time.deltaTime;
        
    }
}
