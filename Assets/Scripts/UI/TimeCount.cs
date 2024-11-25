using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
namespace fighting_game
{
    public class Timecount : MonoBehaviour
    {
        private float _counttime = 60.0F;
        private float _initialTime = 60.0f;

        public int GetRemaingTime()
        {
            // countTimeに、ゲームが開始してからの秒数を格納
            _counttime -= Time.deltaTime;
            return Mathf.Max((int)_counttime, 0);
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

