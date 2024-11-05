using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace kakutou
{
    public class scenemane : MonoBehaviour
    {
        [SerializeField] private float _changesceneTime = 1.0f;
        private static scenemane _instance;
        private bool loadscene = false;

        public bool Loadscene { get { return loadscene; } }

        private void Awake()
        {
            if(_instance == null)
            {
                _instance = this;
            }
            else if(_instance != this) 
            {
                Destroy(this.gameObject);
            }
        }

        /// <summary>
        /// �����ɃV�[���J��
        /// </summary>
        /// <param name="index"�V�[���ԍ�>
        /// </param>
        private static void LoadScene(int index)
        {
            if(!CheckInstance() || _instance.loadscene)return;
            
        } 

        private static bool CheckInstance() 
        { 
            if( _instance == null)
            {
                Debug.Log("scenemane�����݂��܂���B");
                return false;
            }
            return true;
        }

    }

}
