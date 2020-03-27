using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LeverToDoor : MonoBehaviour
{
    public bool Active = false;
    public Sprite ActivatedLeverSprite;
    public GameObject DoorToChange;
    public Sprite SpriteOpenedDoor;
    public GameObject OtherExitLever;

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
            DoorToChange.GetComponent<SpriteRenderer>().sprite = SpriteOpenedDoor;
            DoorToChange.GetComponent<BoxCollider2D>().enabled = true;
            OtherExitLever.GetComponent<SpriteRenderer>().sprite = OtherExitLever.GetComponent<LeverToDoor>().ActivatedLeverSprite;
            OtherExitLever.GetComponent<BoxCollider2D>().enabled = false;
            this.GetComponent<BoxCollider2D>().enabled = false;

        }
    }
}
