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
    private GameManager gamemanager; //���ӸŴ���
    private Player player; //�÷��̾�
    private MonsterContainer monsterContainer = null; //���������̳� �ν��Ͻ�

    public GameObject SetUnitText; //���� ��ȯ ���ϰ� ���ӽ��� �������� �ߴ� �ؽ�Ʈ

    //���� ���� ��ư �Լ�
    public Tilemap UnitBoard; //���� ����
    public Tilemap MonsterBoard; //���� ����

    public GameObject RoundText; //���� ���� �ؽ�Ʈ

    private bool isStart = false; //���� ���� ����

    public GameObject StarPoint; //������ �̰������ �����Ǵ� ��Ÿ����Ʈ

    private Dictionary<GameObject, Vector3Int> unitPlace; //���� ���۽� ���ֵ� ��ġ ���

    private GameObject disabledObjects; //��Ȱ��ȭ�� ������Ʈ ����

    public GameObject WinLosePanel; //���� ������ �г�

    void Start()
    {
        gamemanager = GameManager.instance; //���� �Ŵ���
        player = Player.instance; //�÷��̾�
        disabledObjects = GameObject.Find("DisabledObjects"); //��Ȱ��ȭ ������Ʈ ����
        WinLosePanel.SetActive(false);

        //this.GetComponent<Button>().onClick.AddListener(gamemanager.CreateMonster);

    }
    private void Update()
    {
        RoundText.GetComponent<TextMeshProUGUI>().text = "���� " + gamemanager.Round;

        //���ӽ����ϰ� ����ִ� ���Ͱ� ���°��
        if (isStart == true && GameObject.FindGameObjectsWithTag("Monster").Length == 0)
        {
            Debug.Log("���� ��");

            isStart = false;
            gamemanager.IsStart = isStart;

            StartCoroutine(nameof(GameEndPass));
        }
        //���ӽ����ϰ� ����ִ� ������ ���°��
        else if (isStart == true && GameObject.FindGameObjectsWithTag("Unit").Length == 0)
        {
            Debug.Log("���� ��");

            isStart = false;
            gamemanager.IsStart = isStart;

            StartCoroutine(nameof(GameEndFail));
        }
    }

    //���ֵ��� ����ߴ� ��ǥ�� �̵�
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
        //������ ��ȯ �ȵǾ� ������� �ؽ�Ʈ ����
        if (GameObject.FindGameObjectsWithTag("Unit").Length == 0)
        {
            //Debug.Log("���� �����ؾ� ��");
            StartCoroutine(CreateText(SetUnitText));
        }
        //���� ����
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

            //���ӽ��� ���� ���ֵ� ��ġ ����
            unitPlace = new Dictionary<GameObject, Vector3Int>();
            List<GameObject> FoundUnits = new List<GameObject>(GameObject.FindGameObjectsWithTag("Unit")); //ã�� ��� ���ֵ�
            for (int i = 0; i < FoundUnits.Count; i++)
            {
                unitPlace.Add(FoundUnits[i], UnitBoard.WorldToCell(FoundUnits[i].transform.position - new Vector3(0.9f, 0.7f, 0)));
            }

            int count = 0; //���� ���� ��
            List<GameObject> monsterlist = new List<GameObject>(); //������ ���� ��޵� ��ü
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
            Debug.Log("���� " + count + "���� ��ȯ");

            //���ͺ��忡 �������� ��ȯ
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
            //���ֺ���� ���ͺ��� �� �ʱ�ȭ
            UnitBoard.RefreshAllTiles();
            MonsterBoard.RefreshAllTiles();

            isStart = true; //���� ����
            gamemanager.IsStart = isStart;
            this.GetComponent<Button>().interactable = false; //���ӽ��� ��ư ��Ȱ��ȭ
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

    //������ �̰�����
    IEnumerator GameEndPass()
    {
        //����ִ� ���ָ�ŭ ��Ÿ����Ʈ ����
        GameObject[] foundUnits = GameObject.FindGameObjectsWithTag("Unit");
        foreach (GameObject foundUnit in foundUnits)
        {
            //�н��� ��� �ı��Ŀ� �������� �Ѿ
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

        int disableCount = disabledObjects.transform.childCount; //��Ȱ��ȭ�� ������Ʈ ���� ���
        for (int i = disableCount; i > 0; i--)
        {
            Transform disabledChild = disabledObjects.transform.GetChild(0); //ù��° �ڽ�
            disabledChild.gameObject.SetActive(true); //Ȱ��ȭ
            if (disabledChild.gameObject.CompareTag("Unit"))
            {
                Debug.Log("���� ��Ȱ");
                disabledChild.transform.SetParent(null); //�ֻ��� ������Ʈ�� ����
            }
            else if (disabledChild.gameObject.CompareTag("HPSlider") || disabledChild.gameObject.CompareTag("MPSlider"))
            {
                disabledChild.transform.SetParent(GameObject.Find("UnitUIManager").transform); //UI�� UI������ �ؿ�
            }
        }

        //���� �г� Ȱ��ȭ
        WinLosePanel.SetActive(true);
        WinLosePanel.transform.GetChild(0).GetComponent<TextMeshProUGUI>().color = new Color(0 / 255f, 156 / 255f, 255 / 255f);
        WinLosePanel.transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = "�¸�";
        yield return new WaitForSeconds(0.5f);
        WinLosePanel.SetActive(false);

        player.Crystal += gamemanager.Round * 10; //�ش� ���帶�� �÷��̾�� ���� ����
        gamemanager.Round++; //���� ����
        
        MovePlace();       

        //���� �������� ��������
        player.Crystal += 10;
        GameObject.Find("CardSelects").GetComponent<CardSelects>().RefreshUnitCard();

        yield return new WaitForSeconds(1);

        this.GetComponent<Button>().interactable = true; //���ӽ��� ��ư Ȱ��ȭ
    }

    //���Ͱ� �̰�����
    IEnumerator GameEndFail()
    {
        //��� ���� �ı�
        GameObject[] foundMonsters = GameObject.FindGameObjectsWithTag("Monster");
        foreach (GameObject foundMonster in foundMonsters)
        {
            Destroy(foundMonster.gameObject);
            yield return null;
        }

        GameObject[] foundUnits = GameObject.FindGameObjectsWithTag("Unit");
        foreach (GameObject foundUnit in foundUnits)
        {
            //�н��� ��� �ı��Ŀ� �������� �Ѿ
            if (foundUnit.GetComponent<LivingEntity>().IsAlter == true)
            {
                Destroy(foundUnit);
                continue;
            }
        }

        int disableCount = disabledObjects.transform.childCount; //��Ȱ��ȭ�� ������Ʈ ���� ���
        for (int i = disableCount; i > 0; i--)
        {
            Transform disabledChild = disabledObjects.transform.GetChild(0); //ù��° �ڽ�
            disabledChild.gameObject.SetActive(true); //Ȱ��ȭ
            if (disabledChild.gameObject.CompareTag("Unit"))
            {
                Debug.Log("���� ��Ȱ");
                disabledChild.transform.SetParent(null); //�ֻ��� ������Ʈ�� ����
            }
            else if (disabledChild.gameObject.CompareTag("HPSlider") || disabledChild.gameObject.CompareTag("MPSlider"))
            {
                disabledChild.transform.SetParent(GameObject.Find("UnitUIManager").transform); //UI�� UI������ �ؿ�
            }
        }

        //���� �г� Ȱ��ȭ
        WinLosePanel.SetActive(true);
        WinLosePanel.transform.GetChild(0).GetComponent<TextMeshProUGUI>().color = new Color(255 / 255f, 0 / 255f, 0 / 255f);
        WinLosePanel.transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = "�й�";
        yield return new WaitForSeconds(0.5f);
        WinLosePanel.SetActive(false);

        MovePlace();

        //���� �������� ��������
        player.Crystal += 10;
        GameObject.Find("CardSelects").GetComponent<CardSelects>().RefreshUnitCard();

        yield return new WaitForSeconds(1);

        this.GetComponent<Button>().interactable = true; //���ӽ��� ��ư Ȱ��ȭ
    }
}