using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Tilemaps;
using TMPro;

public class GameManager : MonoBehaviour
{
    public static GameManager instance; //게임매니저 인스턴스
    public GameObject SetUnitText; //유닛 소환 안하고 게임시작 눌렀을때 뜨는 텍스트

    private int round = 1;

    private MonsterContainer monsterContainer = null; //몬스터컨테이너 인스턴스

    //게임 시작 버튼 함수
    //몬스터들 <- 몬스터를 따로 저장?
    private Tilemap UnitBoard; //유닛 보드
    private Tilemap MonsterBoard; //몬스터 보드

    private void Awake()
    {
        if (instance != null)
        {
            Destroy(gameObject);
            return;
        }

        instance = this;
        DontDestroyOnLoad(gameObject);
    }
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }
    public void GameStart()
    {

    }

    //몬스터 스폰
    public void CreateMonster()
    {
        //유닛이 소환 안되어 있을경우 텍스트 생성
        if (GameObject.FindGameObjectsWithTag("Unit").Length == 0)
        {
            StartCoroutine(CreateText(SetUnitText));
        }
        //게임 시작
        else
        {
            if (monsterContainer == null)
            {
                monsterContainer = MonsterContainer.instance;
            }
            if (MonsterBoard == null)
            {
                MonsterBoard = GameObject.Find("MonsterBoard").GetComponent<Tilemap>();
            }
            if (UnitBoard == null)
            {
                UnitBoard = GameObject.Find("UnitBoard").GetComponent<Tilemap>();
            }
            int count = 0; //몬스터 스폰 수
            List<GameObject> monsterlist = new List<GameObject>(); //스폰할 몬스터 등급들 전체
            if (round >= 1 && round <= 3)
            {
                count = round;
                monsterlist.AddRange(monsterContainer.MonsterPrefabs[0].Monster);
            }
            else if (round >= 4 && round <= 8)
            {
                count = 4;
                monsterlist.AddRange(monsterContainer.MonsterPrefabs[0].Monster);
                monsterlist.AddRange(monsterContainer.MonsterPrefabs[1].Monster);
            }
            else if (round == 9)
            {
                count = 4;
                monsterlist.AddRange(monsterContainer.MonsterPrefabs[1].Monster);
                monsterlist.AddRange(monsterContainer.MonsterPrefabs[2].Monster);
            }
            else if (round >= 10 && round <= 13)
            {
                count = 5;
                monsterlist.AddRange(monsterContainer.MonsterPrefabs[1].Monster);
                monsterlist.AddRange(monsterContainer.MonsterPrefabs[2].Monster);
            }
            else if (round >= 14 && round <= 15)
            {
                count = 5;
                monsterlist.AddRange(monsterContainer.MonsterPrefabs[1].Monster);
                monsterlist.AddRange(monsterContainer.MonsterPrefabs[2].Monster);
                monsterlist.AddRange(monsterContainer.MonsterPrefabs[3].Monster);
            }
            else if (round >= 16 && round <= 18)
            {
                count = 6;
                monsterlist.AddRange(monsterContainer.MonsterPrefabs[1].Monster);
                monsterlist.AddRange(monsterContainer.MonsterPrefabs[2].Monster);
                monsterlist.AddRange(monsterContainer.MonsterPrefabs[3].Monster);
            }
            else if (round >= 19 && round <= 21)
            {
                count = 6;
                monsterlist.AddRange(monsterContainer.MonsterPrefabs[2].Monster);
                monsterlist.AddRange(monsterContainer.MonsterPrefabs[3].Monster);
            }
            else if (round >= 22 && round <= 23)
            {
                count = 7;
                monsterlist.AddRange(monsterContainer.MonsterPrefabs[2].Monster);
                monsterlist.AddRange(monsterContainer.MonsterPrefabs[3].Monster);
            }
            else if (round >= 24 && round <= 27)
            {
                count = 7;
                monsterlist.AddRange(monsterContainer.MonsterPrefabs[2].Monster);
                monsterlist.AddRange(monsterContainer.MonsterPrefabs[3].Monster);
                monsterlist.AddRange(monsterContainer.MonsterPrefabs[4].Monster);
            }
            else if (round == 28)
            {
                count = 8;
                monsterlist.AddRange(monsterContainer.MonsterPrefabs[2].Monster);
                monsterlist.AddRange(monsterContainer.MonsterPrefabs[3].Monster);
                monsterlist.AddRange(monsterContainer.MonsterPrefabs[4].Monster);
            }
            else if (round >= 29 && round <= 33)
            {
                count = 8;
                monsterlist.AddRange(monsterContainer.MonsterPrefabs[3].Monster);
                monsterlist.AddRange(monsterContainer.MonsterPrefabs[4].Monster);
            }
            else if (round >= 34 && round <= 38)
            {
                count = 9;
                monsterlist.AddRange(monsterContainer.MonsterPrefabs[3].Monster);
                monsterlist.AddRange(monsterContainer.MonsterPrefabs[4].Monster);
                monsterlist.AddRange(monsterContainer.MonsterPrefabs[5].Monster);
            }
            else if (round >= 39)
            {
                count = 10;
                monsterlist.AddRange(monsterContainer.MonsterPrefabs[0].Monster);
                monsterlist.AddRange(monsterContainer.MonsterPrefabs[1].Monster);
                monsterlist.AddRange(monsterContainer.MonsterPrefabs[2].Monster);
                monsterlist.AddRange(monsterContainer.MonsterPrefabs[3].Monster);
                monsterlist.AddRange(monsterContainer.MonsterPrefabs[4].Monster);
                monsterlist.AddRange(monsterContainer.MonsterPrefabs[5].Monster);
            }
            Debug.Log(count + "마리 소환");

            for (int i = 0; i < count; i++)
            {
                Vector3Int v3Int;
                do
                {
                    int randX = Random.Range(0, 4);
                    int randY = Random.Range(0, 4);
                    v3Int = new Vector3Int(randX, randY, 0);
                } while (MonsterBoard.GetColor(v3Int) == Color.red);
                //Debug.Log(v3Int);
                GameObject mst = monsterlist.OrderBy(g => Random.value).FirstOrDefault();
                Instantiate(mst, MonsterBoard.CellToWorld(v3Int) + new Vector3(0.9f, 0.7f, 0), Quaternion.identity);
                MonsterBoard.SetTileFlags(v3Int, TileFlags.None);
                MonsterBoard.SetColor(v3Int, Color.red);
            }
            UnitBoard.RefreshAllTiles();
            MonsterBoard.RefreshAllTiles();
            GameObject.Find("GameStartButton").GetComponent<Button>().interactable = false;
        }
        
    }

    IEnumerator CreateText(GameObject data)
    {

        GameObject setUnitText = Instantiate(data, GameObject.Find("BattleUI").transform);
        for (float i = 1f; i > 0; i -= 0.1f)
        {
            setUnitText.GetComponent<TextMeshProUGUI>().color = new Vector4(1, 1, 1, i);
            yield return new WaitForSeconds(0.1f);
        }
        Destroy(setUnitText);
    }
}
