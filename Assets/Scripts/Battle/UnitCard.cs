using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Tilemaps;
using System.Text.RegularExpressions;

public class UnitCard : MonoBehaviour
{
    private Tilemap board; //소환될 타일맵

    public GameObject unit; //소환할 유닛
    public int crystal = 10; //선택 시 소모할 수정
    public string unitName; //소환할 유닛이름


    private void Awake()
    {
        board = GameObject.Find("UnitBoard").GetComponent<Tilemap>();
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
        Vector3Int v3Int;
        do
        {
            int randX = Random.Range(0, 4);
            int randY = Random.Range(0, 4);
            v3Int = new Vector3Int(randX, randY, 0);
        } while (board.GetColor(v3Int) == Color.green);

        //Instantiate(unit, board.CellToWorld(v3Int), Quaternion.identity);
        Instantiate(unit, board.CellToWorld(v3Int) + new Vector3(0.9f, 1.4f, 0), Quaternion.identity);
        board.SetTileFlags(v3Int, TileFlags.None);
        board.SetColor(v3Int, Color.green);
        Debug.Log("소환" + v3Int);
        GetComponent<Button>().interactable = false;        
    }
}
