using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LeverToWayDoor : MonoBehaviour
{
    public Sprite ActivatedLeverSprite;
    public GameObject BarrierToRemove;

    private SpriteRenderer sp;

    private void Awake()
    {
        sp = this.GetComponent<SpriteRenderer>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        { 
            sp.sprite = ActivatedLeverSprite;
            AudioManager.Instance.PlaySound("Lever");
            BarrierToRemove.GetComponent<BoxCollider2D>().enabled = false;
        }
    }

}
