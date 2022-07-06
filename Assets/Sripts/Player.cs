using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Player : MonoBehaviour
{
    [SerializeField] float Velocity;
    [SerializeField] protected int ActualHorizontal, ActualVertical;
    [SerializeField] float NewX, NewZ;

    protected GridManager gridManager;
    bool Change;

    public virtual void Awake()
    {
        Ray ray = new Ray(transform.position, Vector3.down);
        RaycastHit hit;
        if (Physics.Raycast(ray, out hit, 4f))
        {
            if (hit.collider.GetComponent<Block>() != null)
            {
                Block b = hit.collider.GetComponent<Block>();
                ActualHorizontal = b.horizontal;
                ActualVertical = b.vertical;
                NewX = b.transform.position.x;
                NewZ = b.transform.position.z;
            }
        }
    }

    void Start()
    {
        gridManager = FindObjectOfType<GridManager>();
    }

    void Update()
    {
        Movement();
        ActualPosition();
    }

    public virtual void ActualPosition()
    {
        transform.position = Vector3.MoveTowards(transform.position, new Vector3(NewX, transform.position.y, NewZ), Velocity * Time.deltaTime);
    }

    public virtual void NewPosition(int NewVertical, int NewHorizontal)
    {
        foreach (var item in gridManager.arrayBlocks)
        {
            if (NewHorizontal == item.horizontal && NewVertical == item.vertical)
            {
                if (!item.occupied)
                {
                    NewX = item.position.x;
                    NewZ = item.position.z;
                    ActualHorizontal = NewHorizontal;
                    ActualVertical = NewVertical;
                }
            }
            else print("Espacio ocupado");
        }
    }

    void Movement()
    {
        int NewVertical = ActualVertical, NewHorizontal = ActualHorizontal;

        if (Input.GetKeyDown("w"))
        {
            NewVertical++;
            transform.rotation = Quaternion.Euler(0, 0, 0);

            NewPosition(NewVertical, NewHorizontal);
        }

        if (Input.GetKeyDown("s"))
        {
            NewVertical--;
            transform.rotation = Quaternion.Euler(0, 180, 0);

            NewPosition(NewVertical, NewHorizontal);
        }

        if (Input.GetKeyDown("d"))
        {
            NewHorizontal++;
            transform.rotation = Quaternion.Euler(0, 90, 0);

            NewPosition(NewVertical, NewHorizontal);
        }

        if (Input.GetKeyDown("a"))
        {
            NewHorizontal--;
            transform.rotation = Quaternion.Euler(0, 270, 0);

            NewPosition(NewVertical, NewHorizontal);
        }
    }
}
