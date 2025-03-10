using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public float velocidade = 10f;
    public float focaPulo = 10f;
    public float forcaQuedaRapida = 20f; // Nova vari�vel para a for�a da queda r�pida

    public bool noChao = false;
    public bool andando = false;

    private Rigidbody2D _rigidbody2D;
    private SpriteRenderer _spriteRenderer;
    private Animator _animator;

    // Vari�veis para o pulo
    private int puloContagem = 0; // Contador de pulos
    private const int maxPulos = 1; // M�ximo de pulos (1 normal + 1 duplo)

    void Start()
    {
        _rigidbody2D = gameObject.GetComponent<Rigidbody2D>();
        _spriteRenderer = gameObject.GetComponent<SpriteRenderer>();
        _animator = gameObject.GetComponent<Animator>();
    }

    void OnCollisionStay2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "chao")
        {
            noChao = true;
            puloContagem = 0; // Reseta a contagem de pulos ao tocar o ch�o
        }
    }

    void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "chao")
        {
            noChao = false;
        }
    }

    void Update()
    {
        andando = false;

        if (Input.GetKey(KeyCode.LeftArrow))
        {
            gameObject.transform.position += new Vector3(-velocidade * Time.deltaTime, 0, 0);
            _spriteRenderer.flipX = true;
            Debug.Log("LeftArrow");
            if (noChao) andando = true;
        }

        if (Input.GetKey(KeyCode.RightArrow))
        {
            gameObject.transform.position += new Vector3(velocidade * Time.deltaTime, 0, 0);
            _spriteRenderer.flipX = false;
            Debug.Log("RightArrow");
            if (noChao) andando = true;
        }

        // L�gica para o pulo
        if (Input.GetKeyDown(KeyCode.Space))
        {
            if (puloContagem < maxPulos) // Permite pulo se ainda n�o atingiu o m�ximo
            {
                _rigidbody2D.AddForce(new Vector2(0, 1) * focaPulo, ForceMode2D.Impulse);
                puloContagem++; // Incrementa a contagem de pulos
                Debug.Log("Jump");
            }
        }

        // L�gica para a queda r�pida
        if (Input.GetKey(KeyCode.DownArrow) && !noChao)
        {
            _rigidbody2D.velocity = new Vector2(_rigidbody2D.velocity.x, -forcaQuedaRapida); // Aplica a for�a de queda r�pida
            Debug.Log("Cair R�pido");
        }

        _animator.SetBool("Andando", andando);
    }
}
