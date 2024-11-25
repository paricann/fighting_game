using UnityEngine;
using UnityEngine.UI;
namespace fighting_game
{

    public class hp_sc : MonoBehaviour
    {
        CharacterManager _chmane;
        public UIlevel _level;
        private  int curenthelth = 0;
        private  int curentenehelth = 0;

        private void Start()
        {
            _chmane = CharacterManager.GetInstance(); // CharacterManagerを取得
            _level = UIlevel.GetInstance(); // UIlevelを取得

            Debug.Log("ここまで来てるよ");
            for (int i = 0; i < _chmane.Players.Count; i++)
            {
                Debug.Log(curenthelth);
                curenthelth    = _chmane.Players[i].plStatus.status_helth;   //初期体力MAX
                curentenehelth = _chmane.Players[i].plStatus.status_helth;   //上と同じ
            }
            UpdateenehelthUI();
            UpdateplayhelthUI();
        }

        /// <summary> /// player体力減少処理 /// </summary>　
        /// /// <param name="dmgs">_chbaseの_chpower</param>
        public void Hitplayerdamge(float[] dmgs)
        {
            Debug.Log(curenthelth);
            foreach (float dmg in dmgs)
            {
                curenthelth -= (int)dmg;
            }
            UpdateplayhelthUI();
        }

        /// <summary>/// enemy体力減少処理/// </summary> ///
        /// <param name="dmgs">_chbaseの_chpower </param>
        public void Hitenemydame(float[] dmgs)
        {
            Debug.Log(curentenehelth);
            foreach (float dmg in dmgs)
            {
                curentenehelth -= (int)dmg;
            }
            UpdateenehelthUI();
        }

        /// <summary> /// playerのスライダー増減処理 /// </summary>
        public void UpdateplayhelthUI()
        {
            Debug.Log("playerhelth");
            for (int i = 0; i < _chmane.Players.Count; i++)
            {
                if (_level.sliders.Length > 0)
                {
                    Debug.Log(curenthelth + "現在の体力");
                    _level.sliders[0].value = curenthelth / _chmane.Players[i].plStatus.status_helth; // プレイヤーのHPをスライダーに反映
                }
            }

        }

        /// <summary> /// enemyのスライダー増減処理 /// </summary>
        public void UpdateenehelthUI()
        {
            Debug.Log("enehelth");
            for (int i = 0; i < _chmane.Players.Count; i++)
            {
                if (_level.sliders.Length > 1)
                {
                    Debug.Log("現在の敵の体力" + curentenehelth);
                    _level.sliders[1].value = curentenehelth / _chmane.Players[i].plStatus.status_helth; // enemyのHPをスライダーに反映
                }
            }
           
        }

    }
}
