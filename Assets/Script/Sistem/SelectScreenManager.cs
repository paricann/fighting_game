using fighting_game;
using System.Collections;
using System.Collections.Generic;
using UnityEditor.Build.Player;
using UnityEngine;
using UnityEngine.SceneManagement;
using static CharacterManager;

public class SelectScreenManager : MonoBehaviour
{
    public int numberOfPlayers = 1;
    public List<PlayerInterface> plIF = new List<PlayerInterface>();
    public PotraitInfo[] potraitPrefabs;  //�L�����N�^�[�̃C���[�W�i�ё���I�ȓz�j
    public int maxX;
    public int maxY;
    PotraitInfo[,] potraitGrid;           //select��ʂőI���Ɏg�p���邽�߂�Grid

    public GameObject potraitCanvas;      //���ׂẴL�����N�^�[image�����L

    bool loadlebel;
    public bool bothPlayerSelected;

    private CharacterManager _chManager;

    /// <summary>�@/// singleton�@/// </summary>
    public static SelectScreenManager instance;
    public static SelectScreenManager GetInstance()
    {
        return instance;
    }

    private void Awake()
    {
        instance = this;
    }

    private void Start()
    {
        _chManager = CharacterManager.GetInstance();
        numberOfPlayers = _chManager.numberOfusers;

        potraitGrid = new PotraitInfo[maxX, maxY];

        int x = 0;
        int y = 0;

        potraitPrefabs = potraitCanvas.GetComponentsInChildren<PotraitInfo>();  

        for(int i = 0; i < potraitPrefabs.Length; i++)
        {
            potraitPrefabs[i].PosX += x;
            potraitPrefabs[i].PosY += y;

            potraitGrid[x,y] = potraitPrefabs[i];
            if(x < maxX - 1)
            {
                x++;
            }
            else
            {
                x = 0;
                y++;
            }
        }
    }

    private void Update()
    {
        if (!loadlebel)
        {
            for (int i = 0; i < plIF.Count; i++)
            {
                if (!_chManager.Players[i].hasCharactoer)
                {
                    plIF[i].playerBase = _chManager.Players[i];
                    HandleSelectionPosition(plIF[i]);
                    HandleSelectScreenInput(plIF[i], _chManager.Players[i].inputId);
                   // HandleCharacterPrevew(plIF[i]);
                }
                else
                {
                    _chManager.Players[i].hasCharactoer = true;
                }
            }
        }
        //������(�v���C���[�I������������)
        if (bothPlayerSelected)
        {
            Debug.Log("���̉�ʂ�");
            StartCoroutine(LoadLevel());
            loadlebel = true;
        }
        else
        {
            if (_chManager.Players[0].hasCharactoer && _chManager.Players[1].hasCharactoer)
            {
                bothPlayerSelected = true;
            }
        }
    }

    /// <summary>�@/// gameScene�֑J�ځ@/// </summary>
    /// <returns></returns>
    IEnumerator LoadLevel()
    {
        for(int i = 0; i < _chManager.Players.Count; i++)
        {
            int _ranValue = Random.Range(0, potraitPrefabs.Length);
            _chManager.Players[i].playerPrefab = 
                _chManager.returncharacterWithID(potraitPrefabs[_ranValue].characterID).Prefab;
            Debug.Log(potraitPrefabs[_ranValue].characterID +"AI�L�����I���I");
        }
        yield return new WaitForSeconds(2);
        //��ʑJ�ڏ���
        SceneManager.LoadSceneAsync("testSCENE", LoadSceneMode.Single);
    }

    /// <summary> /// �Z���N�^�[�ړ� /// </summary>
    /// <param name="pl"></param>
    /// <param name="playerId"></param>
    private void HandleSelectScreenInput(PlayerInterface pl,string playerId)
    {
        float y = Input.GetAxis("Vertical" + playerId);
        if(y != 0)
        {
            if (!pl.hitInputOnce)
            {
                if(y > 0)
                {
                    pl.activeY = ( pl.activeY > 0 ) ? pl.activeY -1: maxY - 1;
                }
                else
                {
                    pl.activeY = (pl.activeY < maxY - 1) ? pl.activeY + 1 : 0;
                }
                pl.hitInputOnce = true;
            }
        }

        float x = Input.GetAxis("Horizontal" + playerId);
        if (x != 0)
        {
            if (!pl.hitInputOnce)
            {
                if (y > 0)
                {
                    pl.activeX = (pl.activeX > 0) ? pl.activeY - 1 : maxX - 1;
                }
                else
                {
                    pl.activeX = (pl.activeX < maxX - 1) ? pl.activeX + 1 : 0;
                }
                pl.timerToReset = 0;
                pl.hitInputOnce = true;
            }
        }

        if(y == 0 && x == 0)
        {
            pl.hitInputOnce = false;
        }

        if (pl.hitInputOnce)
        {
            pl.timerToReset += Time.deltaTime;
            if(pl.timerToReset > 0.8f)
            {
                pl.hitInputOnce =false;
                pl.timerToReset = 0;
            }
        }

        if(Input.GetKey(KeyCode.Return))
        {
            //TODO�A�j���[�^�[���擾����createdCharacter�ɑ������
            pl.playerBase.playerPrefab = _chManager.returncharacterWithID(pl.activePotrait.characterID).Prefab;
            pl.playerBase.hasCharactoer = true;
        }
    }

    /// <summary>
    /// �Z���N�g�ʒu
    /// </summary>
    /// <param name="pl"></param>
    private void HandleSelectionPosition(PlayerInterface pl)
    {
        pl.selector.SetActive(true);
        pl.activePotrait = potraitGrid[pl.activeX, pl.activeY];

        Vector2 selectedPosition = pl.activePotrait.transform.localPosition;
        selectedPosition = selectedPosition + new Vector2(potraitCanvas.transform.localPosition.x
            , potraitCanvas.transform.localPosition.y);
        pl.selector.transform.localPosition = selectedPosition;
    }

    /// <summary>
    /// �Z���N�g�����L�����N�^�[���v���r���[�ɐ���
    /// </summary>
    /// <param name="pl"></param>
    private void HandleCharacterPrevew(PlayerInterface pl)
    {
        GameObject go = Instantiate(
            CharacterManager.GetInstance().returncharacterWithID(pl.activePotrait.characterID).Prefab,
            pl.charVisPos.position,
            Quaternion.identity) as GameObject;

        pl.createdCharacter = go;

        pl.preiewPotrait = pl.activePotrait;

        
    }


    [System.Serializable]
    public class PlayerInterface
    {
        public PotraitInfo activePotrait;   //���ݑI��ł���L�����摜
        public PotraitInfo preiewPotrait;
        public GameObject selector;        //�|�W�V�����I��p�I�u�W�F
        public Transform charVisPos;      //player1�̑I��ł���L������position
        public GameObject createdCharacter;

        public int activeX; //player1�̃G���g���A�N�e�B�u
        public int activeY;

        public bool hitInputOnce;
        public float timerToReset;

        public PlayerBase playerBase;
    }

    
}




