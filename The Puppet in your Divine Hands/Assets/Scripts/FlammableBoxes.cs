using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlammableBoxes : MonoBehaviour
{
    private Animator anim;
    private Player player;

    private void Awake()
    {
        anim = this.GetComponent<Animator>();
    }

    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            if (player.OnFire)
            {
                anim.enabled = true;
                AudioManager.Instance.PlaySound("FireInBoxes");
                Destroy(this.gameObject, 0.5f);
            }
        }
    }
}
