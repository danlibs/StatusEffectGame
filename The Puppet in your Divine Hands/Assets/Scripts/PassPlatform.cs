using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PassPlatform : MonoBehaviour
{
    private Transform player;

    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
    }

    private void Update()
    {
        if (this.transform.position.y > player.position.y)
        {
            this.gameObject.layer = 13;
        }
        else
        {
            this.gameObject.layer = 8;
        }
    }

}
