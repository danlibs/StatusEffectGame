using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingPlatforms : MonoBehaviour
{
    public float Speed;
    public float MinX, MaxX;
    public float MinY, MaxY;
    public bool VerticalMovement;

    private bool moveRight;
    private bool moveUp;

    private void FixedUpdate()
    {
        //Plataformas movidas horizontalmente:
        if (!VerticalMovement)
        {
            if (this.transform.position.x <= MinX)
            {
                moveRight = true;
            }
            else if (this.transform.position.x >= MaxX)
            {
                moveRight = false;
            }

            if (moveRight)
            {
                this.transform.Translate(Vector2.right * Speed * Time.deltaTime);
            }
            else
            {
                this.transform.Translate(Vector2.left * Speed * Time.deltaTime);
            }
        }
        else //Plataformas movidas verticalmente:
        {
            if (this.transform.position.y <= MinY)
            {
                moveUp = true;
            }
            else if (this.transform.position.y >= MaxY)
            {
                moveUp = false;
            }

            if (moveUp)
            {
                this.transform.Translate(Vector2.up * Speed * Time.deltaTime);
            }
            else
            {
                this.transform.Translate(Vector2.down * Speed * Time.deltaTime);
            }
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            collision.transform.SetParent(this.transform);
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            collision.transform.SetParent(null);
        }
    }
}
