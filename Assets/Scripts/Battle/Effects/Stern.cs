using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stern : MonoBehaviour
{
    private float destroySec = 1;
    public float DestroySec
    {
        set
        {
            destroySec = value;
        }
    }

    void Start()
    {
        Destroy(this.gameObject, destroySec);
    }
}
