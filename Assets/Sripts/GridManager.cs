using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridManager : MonoBehaviour
{
    public Block[] arrayBlocks;
    public int MaxVertical, MaxHorizontal;

    void Awake()
    {
        arrayBlocks = FindObjectsOfType<Block>();
    }

    void Update()
    {
        
    }
}
