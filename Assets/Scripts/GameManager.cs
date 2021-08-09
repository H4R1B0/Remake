using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Tilemaps;
using TMPro;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager instance; //���ӸŴ��� �ν��Ͻ�

    private int round = 1;
    public int Round
    {
        get
        {
            return round;
        }
        set
        {
            round = value;
        }
    }

    private bool isStart = false;
    public bool IsStart
    {
        get
        {
            return isStart;
        }
        set
        {
            isStart = value;
        }
    }

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

    void Update()
    {

    }
}
