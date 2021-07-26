using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Tilemaps;
using System.Text.RegularExpressions;

public class UnitCard : MonoBehaviour
{
    private Tilemap UnitBoard; //소환될 타일맵
    private Player player; //플레이어

    public GameObject unit; //소환할 유닛
    public int crystal = 10; //선택 시 소모할 수정
    public string unitName; //소환할 유닛이름


    private void Awake()
    {
        UnitBoard = GameObject.Find("UnitBoard").GetComponent<Tilemap>();
        player = Player.instance;
    }
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {        

    }
    public void CallUnit()
    {
        int currentCount = GameObject.FindGameObjectsWithTag("Unit").Length; //소환된 유닛 수

        //소환 가능한 최대 유닛수 보다 작아야 소환 가능
        if (currentCount < player.CallUnitCountMax)
        {
            Vector3Int v3Int;
            do
            {
                int randX = Random.Range(0, 4);
                int randY = Random.Range(0, 4);
                v3Int = new Vector3Int(randX, randY, 0);
            } while (UnitBoard.GetColor(v3Int) == Color.green);

            //Instantiate(unit, board.CellToWorld(v3Int), Quaternion.identity);
            Instantiate(unit, UnitBoard.CellToWorld(v3Int) + new Vector3(0.9f, 1.4f, 0), Quaternion.identity);
            UnitBoard.SetTileFlags(v3Int, TileFlags.None);
            UnitBoard.SetColor(v3Int, Color.green);
            Debug.Log("소환" + v3Int);
            GetComponent<Button>().interactable = false;
        }
        else
        {
            Debug.Log("소환 불가");
        }

        GameObject.Find("CallUnitCountText").GetComponent<CallUnitCountText>().RenewText(); //유닛 소환 텍스트 갱신
    }
}
