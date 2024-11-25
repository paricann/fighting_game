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
        public string charId;         //�L����ID
        public GameObject Prefab;
    }

    [System.Serializable]
    public class PlayerBase
    {
        public string playerID;         //�v���C���[ID
        public string inputId;�@�@�@�@�@//����ID
        public bool hasCharactoer;�@�@�@//�v���C���[���L������I���������ǂ����`�F�b�N�p
        public GameObject playerPrefab; 
        public int score;
        public PLAYER_TYPE _plType;     //player��AI���l�Ԃ�
        public StateManager plStatus;

        public enum PLAYER_TYPE
        {
            USER,
            AI,
        }
    }

}

