using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Tilemaps;
using UnityEngine.SceneManagement;
using TMPro;


public class GameStartButton : MonoBehaviour
{
    private GameManager gamemanager; //게임매니저
    private MonsterContainer monsterContainer = null; //몬스터컨테이너 인스턴스

    public GameObject SetUnitText; //유닛 소환 안하고 게임시작 눌렀을때 뜨는 텍스트

    //게임 시작 버튼 함수
    public Tilemap UnitBoard; //유닛 보드
    public Tilemap MonsterBoard; //몬스터 보드

    private bool isStart = false; //게임 시작 여부

    public GameObject StarPoint; //유닛이 이겼을경우 생성되는 스타포인트

    private Dictionary<GameObject, Vector3Int> unitPlace; //게임 시작시 유닛들 배치 기억

    void Start()
    {
        gamemanager = GameManager.instance;
        //this.GetComponent<Button>().onClick.AddListener(gamemanager.CreateMonster);

    }
    private void Update()
    {
        //게임시작하고 살아있는 몬스터가 없는경우
        if(isStart==true && GameObject.FindGameObjectsWithTag("Monster").Length == 0)
        {
            Debug.Log("유닛 승");
            
            isStart = false;
            gamemanager.IsStart = isStart;

            StartCoroutine(nameof(GameEndPass));
        }
        //게임시작하고 살아있는 유닛이 없는경우
        else if (isStart == true && GameObject.FindGameObjectsWithTag("Unit").Length == 0)
        {
            Debug.Log("유닛 패");
            isStart = false;
            gamemanager.IsStart = isStart;
            this.GetComponent<Button>().interactable = true; //게임시작 버튼 활성화
        }
    }

    //유닛들을 기억했던 좌표로 이동
    private void MovePlace()
    {
        foreach (var pair in unitPlace)
        {
            //Debug.Log(pair.Value);
            pair.Key.transform.position = UnitBoard.CellToWorld(pair.Value) + new Vector3(0.9f, 1.4f, 0);
            UnitBoard.SetTileFlags(pair.Value, TileFlags.None);
            UnitBoard.SetColor(pair.Value, Color.green);
        }
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

            //게임시작 직전 유닛들 위치 저장
            unitPlace = new Dictionary<GameObject, Vector3Int>();
            List<GameObject> FoundUnits = new List<GameObject>(GameObject.FindGameObjectsWithTag("Unit")); //찾은 모든 유닛들
            for (int i = 0; i < FoundUnits.Count; i++)
            {
                unitPlace.Add(FoundUnits[i], UnitBoard.WorldToCell(FoundUnits[i].transform.position - new Vector3(0.9f, 0.7f, 0)));
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
            Debug.Log("몬스터 " + count + "마리 소환");

            //몬스터보드에 랜덤으로 소환
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
            //유닛보드와 몬스터보드 색 초기화
            UnitBoard.RefreshAllTiles(); 
            MonsterBoard.RefreshAllTiles();

            isStart = true; //게임 시작
            gamemanager.IsStart = isStart;
            this.GetComponent<Button>().interactable = false; //게임시작 버튼 비활성화
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

    //유닛이 이겼을때
    IEnumerator GameEndPass()
    {
        GameObject[] foundUnits = GameObject.FindGameObjectsWithTag("Unit");
        foreach (GameObject foundUnit in foundUnits)
        {
            GameObject starPoint = Instantiate(StarPoint, GameObject.Find("BattleUI").transform);
            starPoint.GetComponent<StarPoint>().Point = foundUnit.GetComponent<LivingEntity>().Level;
            starPoint.transform.position = Camera.main.WorldToScreenPoint(foundUnit.transform.position+new Vector3(0,-0.5f,0));
            yield return new WaitForSeconds(0.3f);
        }        
        gamemanager.Round++; //라운드 증가
        MovePlace();
        this.GetComponent<Button>().interactable = true; //게임시작 버튼 활성화
    }
    IEnumerator GameEndFail()
    {
        yield return new WaitForSeconds(0);
    }
}