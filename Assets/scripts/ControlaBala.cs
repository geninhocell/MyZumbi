using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ControlaBala : MonoBehaviour
{
    public float Velocity = 20;
    public AudioClip SomMorteZumbi;
    private Rigidbody rigidbodyBala;
    private int danoDoTiro = 1;

    private void Start()
    {
        rigidbodyBala = GetComponent<Rigidbody>();
    }
    void FixedUpdate()
    {
        rigidbodyBala
            .MovePosition(
                rigidbodyBala.position + transform.forward * Velocity * Time.deltaTime
            );
    }

    // quando há colisão esse metodo é chamado
    void OnTriggerEnter(Collider objetoDeColisao)
    {
        switch(objetoDeColisao.tag)
        {
            case Tags.Inimigo:
                objetoDeColisao.GetComponent<ControlaZumbi>().TomarDano(danoDoTiro);
                break;
            case Tags.ChefeDeFase:
                objetoDeColisao.GetComponent<ControlaChefe>().TomarDano(danoDoTiro);
                break;
        }

        // gameObject proprio objeto do script
        Destroy(gameObject);
    }
}
