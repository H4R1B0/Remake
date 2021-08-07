using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Tilemaps;
using TMPro;


public class GameStartButton : MonoBehaviour
{
    private GameManager gamemanager; //게임매니저
    private MonsterContainer monsterContainer = null; //몬스터컨테이너 인스턴스

    public GameObject SetUnitText; //유닛 소환 안하고 게임시작 눌렀을때 뜨는 텍스트

    //게임 시작 버튼 함수
    public Tilemap UnitBoard; //유닛 보드
    public Tilemap MonsterBoard; //몬스터 보드

    void Start()
    {
        gamemanager = GameManager.instance;
        //this.GetComponent<Button>().onClick.AddListener(gamemanager.CreateMonster);

    }

    public void GameStart()
    {
        //유닛이 소환 안되어 있을경우 텍스트 생성
        if (GameObject.FindGameObjectsWithTag("Unit").Length == 0)
        {
            //Debug.Log("유닛 생성해야 함");
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
            if (gamemanager.Round >= 1 && gamemanager.Round <= 3)
            {
                count = gamemanager.Round;
                monsterlist.AddRange(monsterContainer.MonsterPrefabs[0].Monster);
            }
            else if (gamemanager.Round >= 4 && gamemanager.Round <= 8)
            {
                count = 4;
                monsterlist.AddRange(monsterContainer.MonsterPrefabs[0].Monster);
                monsterlist.AddRange(monsterContainer.MonsterPrefabs[1].Monster);
            }
            else if (gamemanager.Round == 9)
            {
                count = 4;
                monsterlist.AddRange(monsterContainer.MonsterPrefabs[1].Monster);
                monsterlist.AddRange(monsterContainer.MonsterPrefabs[2].Monster);
            }
            else if (gamemanager.Round >= 10 && gamemanager.Round <= 13)
            {
                count = 5;
                monsterlist.AddRange(monsterContainer.MonsterPrefabs[1].Monster);
                monsterlist.AddRange(monsterContainer.MonsterPrefabs[2].Monster);
            }
            else if (gamemanager.Round >= 14 && gamemanager.Round <= 15)
            {
                count = 5;
                monsterlist.AddRange(monsterContainer.MonsterPrefabs[1].Monster);
                monsterlist.AddRange(monsterContainer.MonsterPrefabs[2].Monster);
                monsterlist.AddRange(monsterContainer.MonsterPrefabs[3].Monster);
            }
            else if (gamemanager.Round >= 16 && gamemanager.Round <= 18)
            {
                count = 6;
                monsterlist.AddRange(monsterContainer.MonsterPrefabs[1].Monster);
                monsterlist.AddRange(monsterContainer.MonsterPrefabs[2].Monster);
                monsterlist.AddRange(monsterContainer.MonsterPrefabs[3].Monster);
            }
            else if (gamemanager.Round >= 19 && gamemanager.Round <= 21)
            {
                count = 6;
                monsterlist.AddRange(monsterContainer.MonsterPrefabs[2].Monster);
                monsterlist.AddRange(monsterContainer.MonsterPrefabs[3].Monster);
            }
            else if (gamemanager.Round >= 22 && gamemanager.Round <= 23)
            {
                count = 7;
                monsterlist.AddRange(monsterContainer.MonsterPrefabs[2].Monster);
                monsterlist.AddRange(monsterContainer.MonsterPrefabs[3].Monster);
            }
            else if (gamemanager.Round >= 24 && gamemanager.Round <= 27)
            {
                count = 7;
                monsterlist.AddRange(monsterContainer.MonsterPrefabs[2].Monster);
                monsterlist.AddRange(monsterContainer.MonsterPrefabs[3].Monster);
                monsterlist.AddRange(monsterContainer.MonsterPrefabs[4].Monster);
            }
            else if (gamemanager.Round == 28)
            {
                count = 8;
                monsterlist.AddRange(monsterContainer.MonsterPrefabs[2].Monster);
                monsterlist.AddRange(monsterContainer.MonsterPrefabs[3].Monster);
                monsterlist.AddRange(monsterContainer.MonsterPrefabs[4].Monster);
            }
            else if (gamemanager.Round >= 29 && gamemanager.Round <= 33)
            {
                count = 8;
                monsterlist.AddRange(monsterContainer.MonsterPrefabs[3].Monster);
                monsterlist.AddRange(monsterContainer.MonsterPrefabs[4].Monster);
            }
            else if (gamemanager.Round >= 34 && gamemanager.Round <= 38)
            {
                count = 9;
                monsterlist.AddRange(monsterContainer.MonsterPrefabs[3].Monster);
                monsterlist.AddRange(monsterContainer.MonsterPrefabs[4].Monster);
                monsterlist.AddRange(monsterContainer.MonsterPrefabs[5].Monster);
            }
            else if (gamemanager.Round >= 39)
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

            this.GetComponent<Button>().interactable = false;
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
