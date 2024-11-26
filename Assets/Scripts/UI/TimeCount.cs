using UnityEngine;
using UnityEngine.UI;
namespace fighting_game
{
    public class Timecount : MonoBehaviour
    {

        private float _counttime = 60.0F;
        private float _initialTime = 60.0f;

        public int GetRemainingTime()
        {
            // countTimeに、ゲームが開始してからの秒数を格納
            _counttime -= Time.deltaTime;
            return (int)_counttime;
        }

        public void ResetTime()
        {
            _counttime = _initialTime;
        }

        public void SetInitialTime(float time)
        {
            _initialTime = time;
            _counttime =_initialTime;
        }


    }
}

