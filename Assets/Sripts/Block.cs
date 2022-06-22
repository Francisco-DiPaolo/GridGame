using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Block : MonoBehaviour
{
    public Vector3 position;
    public int horizontal, vertical;

    void Start()
    {
        position = this.transform.position;
    }

    void Update()
    {
        
    }
}
