using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
namespace fighting_game
{
    public class UIlevel : MonoBehaviour
    {
        public Text textLine1;
        public Text textLine2;
        public Text leveltimer;

        public Slider[] sliders;

        //public GameObject[] winindicatorGrid;
        //public GameObject wininducator;


        /// <summary> /// �V���O���g�� /// </summary>
        public static UIlevel instance;
        public static UIlevel GetInstance()
        {
            return instance;
        }

        private void Awake()
        {
            instance = this;
        }

        /*
        /// <summary>
        /// �e�v���C���[�̏󋵒m�点�i�ǂ��炪�����������j
        /// </summary>
        /// <param name="player"></param>
        public void Addwinindicator(int player)
        {
            GameObject obj = Instantiate(wininducator, this.transform.position,Quaternion.identity) as GameObject;
            obj.transform.SetParent(winindicatorGrid[player].transform);
        }
        */
    }
}

