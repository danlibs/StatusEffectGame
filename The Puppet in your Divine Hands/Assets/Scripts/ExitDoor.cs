using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExitDoor : MonoBehaviour
{
    public CutsceneManager cutscene;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            if (Input.GetKeyDown(KeyCode.W))
            {
                if (collision.GetComponent<Player>().currentHealth < collision.GetComponent<Player>().MaxHealth)
                {
                    cutscene.PlayCutscene(2);
                }
                if (collision.GetComponent<Player>().currentHealth == collision.GetComponent<Player>().MaxHealth)
                {
                    cutscene.PlayCutscene(3);
                }
            }
        }
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            if (Input.GetKeyDown(KeyCode.W))
            {
                if (collision.GetComponent<Player>().currentHealth < collision.GetComponent<Player>().MaxHealth)
                {
                    cutscene.PlayCutscene(2);
                }
                if (collision.GetComponent<Player>().currentHealth == collision.GetComponent<Player>().MaxHealth)
                {
                    cutscene.PlayCutscene(3);
                }
            }
        }
    }
}
