using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    private int coin = 1000000; //코인    
    public int Coin {
        get { 
            return coin; 
        } set {
            coin = value;
        } 
    }//코인 get, set

    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }
}
