using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class GridPassPlatform : MonoBehaviour
{
    private Transform player;
    private Vector3Int cellPosition;

    private void Awake()
    {
        GridLayout gridLayout = transform.parent.GetComponentInParent<GridLayout>();
        cellPosition = gridLayout.WorldToCell(transform.position);
        this.transform.position = gridLayout.CellToWorld(cellPosition);
    }

    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
    }

    private void Update()
    {
        if (this.cellPosition.y > player.position.y)
        {
            this.gameObject.layer = 13;
        }
        else
        {
            this.gameObject.layer = 8;
        }
    }
}
