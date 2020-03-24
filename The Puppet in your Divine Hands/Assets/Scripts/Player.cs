using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Player : MonoBehaviour
{
    public float Speed;
    public float JumpForce;
    public int MaxHealth;
    public Transform FeetPosition;
    public float CheckRadius;
    public LayerMask IsGround;
    public float JumpTime;
    public Slider healthSlider;
    public Slider speedSlider;
    public Slider weightSlider;
    public bool OnFire = false;
    public bool Invencible;
    public float KnockbackForce;
    public float knockbackLenght;
    public float knockbackTimer;
    public bool knockbackFromRight;
    public bool isDead = false;

    private Rigidbody2D rb;
    private Animator anim;
    private float movement;
    private int currentHealth;
    private bool isGrounded;
    private bool jump = false;
    private bool isJumping;
    private bool facingRight;
    private float jumpTimeCounter;
    private float timerDamage;
    private GameManager gameManager;

    private void Awake()
    {
        rb = this.GetComponent<Rigidbody2D>();
        anim = this.GetComponent<Animator>();
        currentHealth = MaxHealth;
        timerDamage = 2f;
    }

    private void Start()
    {
        gameManager = FindObjectOfType<GameManager>();
        healthSlider.value = currentHealth;
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape) || Input.GetKeyDown(KeyCode.Return))
        {
            gameManager.isPaused = !gameManager.isPaused;
        }

        if (currentHealth <= 0)
        {
            isDead = true;
        }
        else
        {
            isDead = false;
        }

        if (!isDead)
        {
            //Animação ativada ao clicar no botão "Burn!" da UI:
            anim.SetBool("OnFire", OnFire);

            anim.SetFloat("Heavy", rb.mass);

            //Movimentação:
            movement = Input.GetAxis("Horizontal");
            anim.SetInteger("Speed", Mathf.RoundToInt(movement));

            //Verificação de contato com o chão a partir de um círculo invisível:
            isGrounded = Physics2D.OverlapCircle(FeetPosition.position, CheckRadius, IsGround);
            anim.SetBool("Jump", !isGrounded);

            //Detectão de input do pulo:
            if (Input.GetKeyDown(KeyCode.Space) && isGrounded)
            {
                jump = true;
                AudioManager.Instance.PlaySound("PlayerJump");
            }

            //Girar o personagem ao ir para a esquerda ou direita:
            if (movement > 0 && facingRight)
            {
                Flip();
            }
            else if (movement < 0 && !facingRight)
            {
                Flip();
            }

            //Se pegar fogo, perder vida constantemente.
            if (OnFire)
            {
                ContinuousDamage();
            }
            else
            {
                AudioManager.Instance.StopPlay("PlayerBurn");
            }
        }
    }

    private void FixedUpdate()
    {
        if (!isDead)
        {
            if (movement == 0 && isGrounded)
            {
                rb.gravityScale = 0;
            }
            else
            {
                rb.gravityScale = 3;
            }

            //Realização da movimentação para a esquerda ou direita:
            if (knockbackTimer <= 0)
            {
                rb.velocity = new Vector2(movement * Speed * Time.fixedDeltaTime, rb.velocity.y);
            }
            else
            {
                if (knockbackFromRight)
                {
                    rb.velocity = new Vector2(-KnockbackForce, KnockbackForce - 1);
                }
                else
                {
                    rb.velocity = new Vector2(KnockbackForce, KnockbackForce - 1); ;
                }
                knockbackTimer -= Time.deltaTime;
            }


            //Realização do pulo:
            Vector2 jumpHeight = Vector2.up * (JumpForce + 5) * Time.fixedDeltaTime;
            if (jump)
            {
                rb.AddForce(jumpHeight, ForceMode2D.Impulse);
                isJumping = true;
                jumpTimeCounter = JumpTime;
                jump = false;
            }

            //Quando mais tempo for apertada a tecla de pulo, mais alto o player pula:
            if (Input.GetKey(KeyCode.Space) && isJumping)
            {
                if (jumpTimeCounter > 0)
                {
                    jumpTimeCounter -= Time.deltaTime;
                    rb.AddForce(jumpHeight + (jumpHeight * jumpTimeCounter), ForceMode2D.Impulse);
                }
                else
                {
                    isJumping = false;
                }
            }
            if (Input.GetKeyUp(KeyCode.Space))
            {
                isJumping = false;
            }
        }   
    }

    //Implementação do giro do personagem:
    private void Flip()
    {
        facingRight = !facingRight;
        this.transform.Rotate(0, 180, 0);
    }

    //Receber dano de algo:
    public void TookDamage(int damage)
    {
        if (!Invencible && !isDead)
        {
            currentHealth -= damage;
            anim.SetTrigger("Hit");
            AudioManager.Instance.PlaySound("PlayerHit");
            knockbackTimer = knockbackLenght;
            healthSlider.value = currentHealth;
            Invencible = true;
            Invoke("ResetInvulnerability", 2f);
        }
        
    }

    //Slider de controle da velocidade:
    public void ChangeSpeed()
    {
        Speed = speedSlider.value;
    }

    //Slider de controle da massa:
    public void ChangeWeight()
    {
        rb.mass = weightSlider.value;
    }

    //Botão para botar fogo no player:
    public void SetOnFire()
    {
        OnFire = !OnFire;
    }

    //Implementação da diminuição de vida do player a cada 2 segundos enquanto pega fogo:
    public void ContinuousDamage()
    {
        timerDamage -= Time.deltaTime;
        if (timerDamage <= 0)
        {
            AudioManager.Instance.PlaySound("PlayerBurn");
            currentHealth--;
            healthSlider.value = currentHealth;
            timerDamage = 2f;
        }
    }

    //Acaba com a invencibilidade do player após receber dano:
    private void ResetInvulnerability()
    {
        Invencible = false;
    }
}
