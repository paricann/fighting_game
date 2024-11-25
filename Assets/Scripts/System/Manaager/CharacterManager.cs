using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.TextCore.Text;
using fighting_game;


public class CharacterManager : MonoBehaviour
{
    public int numberOfusers;
    public List<PlayerBase> Players = new List<PlayerBase>();
    public List<CharaBase> _chList = new List<CharaBase>();

    public CharaBase returncharacterWithID(string ID)
    {
        CharaBase retVal = null;
        for (int i = 0; i < _chList.Count; i++)
        {
            if (string.Equals(_chList[i].charId, ID))
            {
                retVal = _chList[i];
            }
        }
        return retVal;
    }

    public static CharacterManager instance;
    public static CharacterManager GetInstance() { return instance; }
    private void Awake()
    {
        instance = this;
        DontDestroyOnLoad(this.gameObject);
    }

    [System.Serializable]
    public class CharaBase
    {
        public string charId;         //キャラID
        public GameObject Prefab;
    }

    [System.Serializable]
    public class PlayerBase
    {
        public string playerID;         //プレイヤーID
        public string inputId;　　　　　//入力ID
        public bool hasCharactoer;　　　//プレイヤーがキャラを選択したかどうかチェック用
        public GameObject playerPrefab; 
        public int score;
        public PLAYER_TYPE _plType;     //playerがAIか人間か
        public StateManager plStatus;

        public enum PLAYER_TYPE
        {
            USER,
            AI,
        }
    }

}

