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
    private Player player; //플레이어
    private MonsterContainer monsterContainer = null; //몬스터컨테이너 인스턴스

    public GameObject SetUnitText; //유닛 소환 안하고 게임시작 눌렀을때 뜨는 텍스트

    //게임 시작 버튼 함수
    public Tilemap UnitBoard; //유닛 보드
    public Tilemap MonsterBoard; //몬스터 보드

    public GameObject RoundText; //현재 라운드 텍스트

    private bool isStart = false; //게임 시작 여부

    public GameObject StarPoint; //유닛이 이겼을경우 생성되는 스타포인트

    private Dictionary<GameObject, Vector3Int> unitPlace; //게임 시작시 유닛들 배치 기억

    private GameObject disabledObjects; //비활성화된 오브젝트 관리

    public GameObject WinLosePanel; //승패 보여줄 패널

    void Start()
    {
        gamemanager = GameManager.instance; //게임 매니저
        player = Player.instance; //플레이어
        disabledObjects = GameObject.Find("DisabledObjects"); //비활성화 오브젝트 관리
        WinLosePanel.SetActive(false);

        //this.GetComponent<Button>().onClick.AddListener(gamemanager.CreateMonster);

    }
    private void Update()
    {
        RoundText.GetComponent<TextMeshProUGUI>().text = "라운드 " + gamemanager.Round;

        //게임시작하고 살아있는 몬스터가 없는경우
        if (isStart == true && GameObject.FindGameObjectsWithTag("Monster").Length == 0)
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

            StartCoroutine(nameof(GameEndFail));
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
        //살아있는 유닛만큼 스타포인트 지급
        GameObject[] foundUnits = GameObject.FindGameObjectsWithTag("Unit");
        foreach (GameObject foundUnit in foundUnits)
        {
            //분신일 경우 파괴후에 다음으로 넘어감
            if (foundUnit.GetComponent<LivingEntity>().IsAlter == true)
            {
                Destroy(foundUnit);
                continue;
            }

            GameObject starPoint = Instantiate(StarPoint, GameObject.Find("BattleUI").transform);
            starPoint.GetComponent<StarPoint>().Point = foundUnit.GetComponent<LivingEntity>().Level;
            starPoint.transform.position = Camera.main.WorldToScreenPoint(foundUnit.transform.position + new Vector3(0, -0.5f, 0));
            yield return new WaitForSeconds(0.3f);
        }

        int disableCount = disabledObjects.transform.childCount; //비활성화된 오브젝트 개수 얻기
        for (int i = disableCount; i > 0; i--)
        {
            Transform disabledChild = disabledObjects.transform.GetChild(0); //첫번째 자식
            disabledChild.gameObject.SetActive(true); //활성화
            if (disabledChild.gameObject.CompareTag("Unit"))
            {
                Debug.Log("유닛 부활");
                disabledChild.transform.SetParent(null); //최상위 오브젝트로 변경
            }
            else if (disabledChild.gameObject.CompareTag("HPSlider") || disabledChild.gameObject.CompareTag("MPSlider"))
            {
                disabledChild.transform.SetParent(GameObject.Find("UnitUIManager").transform); //UI는 UI관리자 밑에
            }
        }

        //승패 패널 활성화
        WinLosePanel.SetActive(true);
        WinLosePanel.transform.GetChild(0).GetComponent<TextMeshProUGUI>().color = new Color(0 / 255f, 156 / 255f, 255 / 255f);
        WinLosePanel.transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = "승리";
        yield return new WaitForSeconds(0.5f);
        WinLosePanel.SetActive(false);

        player.Crystal += gamemanager.Round * 10; //해당 라운드마다 플레이어에게 수정 지급
        gamemanager.Round++; //라운드 증가
        
        MovePlace();       

        //게임 끝났으니 리프레시
        player.Crystal += 10;
        GameObject.Find("CardSelects").GetComponent<CardSelects>().RefreshUnitCard();

        yield return new WaitForSeconds(1);

        this.GetComponent<Button>().interactable = true; //게임시작 버튼 활성화
    }

    //몬스터가 이겼을때
    IEnumerator GameEndFail()
    {
        //모든 몬스터 파괴
        GameObject[] foundMonsters = GameObject.FindGameObjectsWithTag("Monster");
        foreach (GameObject foundMonster in foundMonsters)
        {
            Destroy(foundMonster.gameObject);
            yield return null;
        }

        GameObject[] foundUnits = GameObject.FindGameObjectsWithTag("Unit");
        foreach (GameObject foundUnit in foundUnits)
        {
            //분신일 경우 파괴후에 다음으로 넘어감
            if (foundUnit.GetComponent<LivingEntity>().IsAlter == true)
            {
                Destroy(foundUnit);
                continue;
            }
        }

        int disableCount = disabledObjects.transform.childCount; //비활성화된 오브젝트 개수 얻기
        for (int i = disableCount; i > 0; i--)
        {
            Transform disabledChild = disabledObjects.transform.GetChild(0); //첫번째 자식
            disabledChild.gameObject.SetActive(true); //활성화
            if (disabledChild.gameObject.CompareTag("Unit"))
            {
                Debug.Log("유닛 부활");
                disabledChild.transform.SetParent(null); //최상위 오브젝트로 변경
            }
            else if (disabledChild.gameObject.CompareTag("HPSlider") || disabledChild.gameObject.CompareTag("MPSlider"))
            {
                disabledChild.transform.SetParent(GameObject.Find("UnitUIManager").transform); //UI는 UI관리자 밑에
            }
        }

        //승패 패널 활성화
        WinLosePanel.SetActive(true);
        WinLosePanel.transform.GetChild(0).GetComponent<TextMeshProUGUI>().color = new Color(255 / 255f, 0 / 255f, 0 / 255f);
        WinLosePanel.transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = "패배";
        yield return new WaitForSeconds(0.5f);
        WinLosePanel.SetActive(false);

        MovePlace();

        //게임 끝났으니 리프레시
        player.Crystal += 10;
        GameObject.Find("CardSelects").GetComponent<CardSelects>().RefreshUnitCard();

        yield return new WaitForSeconds(1);

        this.GetComponent<Button>().interactable = true; //게임시작 버튼 활성화
    }
}