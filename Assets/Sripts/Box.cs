using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Box : Player
{
    public Block block;
    [SerializeField] float RaycastDistance;
    [SerializeField] LayerMask Layer;

    bool Forward, Left, Right, Back, trigger;

    public override void Awake()
    {
        base.Awake();
    }
    void Start()
    {
        trigger = true;
        gridManager = FindObjectOfType<GridManager>();
        NewTerrain();
    }

    void Update()
    {
        ActualPosition();
        Direction();
    }

    private void NewTerrain()
    {
        foreach (var item in gridManager.arrayBlocks)
        {
            if (ActualHorizontal == item.horizontal && ActualVertical == item.vertical)
                block = item;
        }
    }

    void Direction()
    {

        Forward = Physics.Raycast(transform.position, Vector3.forward, RaycastDistance, Layer);
        Left = Physics.Raycast(transform.position, Vector3.left, RaycastDistance, Layer);
        Right = Physics.Raycast(transform.position, Vector3.right, RaycastDistance, Layer);
        Back = Physics.Raycast(transform.position, Vector3.back, RaycastDistance, Layer);

        if (Forward)
        {
            if (IsOcuped(ActualVertical - 1, ActualHorizontal)) block.occupied = true;
            else block.occupied = false;
        }
        else if (Back)
        {
            if (IsOcuped(ActualVertical + 1, ActualHorizontal)) block.occupied = true;
            else block.occupied = false;
        }
        else if (Right)
        {
            if (IsOcuped(ActualVertical, ActualHorizontal - 1)) block.occupied = true;
            else block.occupied = false;
        }
        else if (Left)
        {
            if (IsOcuped(ActualVertical, ActualHorizontal + 1)) block.occupied = true;
            else block.occupied = false;
        }
        else block.occupied = false;
    }

    bool IsOcuped(int NewVertical, int NewHorizontal)
    {
        foreach (var item in gridManager.arrayBlocks)
        {
            if (NewHorizontal == item.horizontal && NewVertical == item.vertical)
            {
                if (item.box) return true;

                else return false;
            }
        }
        return true;
    }

    void Move()
    {
        if (Forward) NewPosition(ActualVertical - 1, ActualHorizontal);
        if (Back) NewPosition(ActualVertical + 1, ActualHorizontal);
        if (Right) NewPosition(ActualVertical, ActualHorizontal - 1);
        if (Left) NewPosition(ActualVertical, ActualHorizontal + 1);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.GetComponent<Player>() != null)
        {
            if (trigger)
            {
                Move();
                NewTerrain();
            }
            trigger = false;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.GetComponent<Player>() != null)
        {
            trigger = true;
        }
    }
}
