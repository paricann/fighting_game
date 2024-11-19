using System.Collections;
using System.Collections.Generic;
using System.Linq.Expressions;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using static CharacterManager;

namespace fighting_game
{
    public class TitleManager : MonoBehaviour
    {
        [SerializeField] private Button _button;
        private float _timer;
        private bool _loadlevel;

        public int activeElement;

        private void Start()
        {
            StartCoroutine(ForceSelect());
        }

        private IEnumerator ForceSelect()
        {
            yield return null;
            EventSystem.current.SetSelectedGameObject(_button.gameObject);
        }

        public void Single()
        {
            activeElement = 0;
            OnCheck();
        }

        public void Double()
        {
            activeElement = 1;
            OnCheck();
        }
        private void OnCheck()
        {
            Debug.Log("load");
            _loadlevel = true;
            StartCoroutine(Loadlevel());

        }

        private void HandlieSelectedOption()
        {
            switch(activeElement)
            {
                case 0:
                    CharacterManager.GetInstance().numberOfusers = 1; 
                    break;
                case 1:
                    CharacterManager.GetInstance().numberOfusers = 2;
                    CharacterManager.GetInstance().Players[1]._plType = PlayerBase.PLAYER_TYPE.USER;
                    break;
            }
        }

        private IEnumerator Loadlevel()
        {
            HandlieSelectedOption();
            yield return new WaitForSeconds(1.5f);
            SceneManager.LoadSceneAsync("SelectScene", LoadSceneMode.Single);
        }


    }


}

