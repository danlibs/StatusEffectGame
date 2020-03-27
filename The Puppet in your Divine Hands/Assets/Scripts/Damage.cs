using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Damage : MonoBehaviour
{
    public int damage;

    private Player player;

    private void Start()
    {
        player = FindObjectOfType<Player>();
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            if (this.transform.position.x > collision.transform.position.x)
            {
                player.knockbackFromRight = true;
            }
            else
            {
                player.knockbackFromRight = false;
            }
            player.TookDamage(damage);
        }
    }

    private void OnCollisionStay2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            if (this.transform.position.x > collision.transform.position.x)
            {
                player.knockbackFromRight = true;
            }
            else
            {
                player.knockbackFromRight = false;
            }
            player.TookDamage(damage);
        }
    }
}
