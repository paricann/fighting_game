using System.Collections;
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
        private bool _loadlevel;
        public int activeElement;

        private void Start()
        {
            Debug.Log("処理が通っている。");
            StartCoroutine(ForceSelect());
        }


        private IEnumerator ForceSelect()
        {
            yield return null;
            EventSystem.current.SetSelectedGameObject(_button.gameObject);
        }

        /// <summary> /// single_mode /// </summary>
        public void Single()
        {
            activeElement = 0;
            OnCheck();
        }

        /// <summary> /// ２player_mode /// </summary>
        public void Double()
        {
            activeElement = 1;
            OnCheck();
        }

        /// <summary>  /// チェック用関数 /// </summary>
        private void OnCheck()
        {
            Debug.Log("load");
            _loadlevel = true;
            StartCoroutine(Loadlevel());

        }

        /// <summary> /// どちらのモードが選択されたか /// </summary>
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

        /// <summary> /// 遷移処理 /// </summary>
        /// <returns></returns>
        private IEnumerator Loadlevel()
        {
            HandlieSelectedOption();
            yield return new WaitForSeconds(1.5f);
            SceneManager.LoadSceneAsync("SelectScene", LoadSceneMode.Single);
        }
    }
}

