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
        Quaternion rotacaoOpostaABala = Quaternion.LookRotation(-transform.forward);
        switch(objetoDeColisao.tag)
        {
            case Tags.Inimigo:
                ControlaZumbi zumbi = objetoDeColisao.GetComponent<ControlaZumbi>();
                zumbi.TomarDano(danoDoTiro);
                zumbi.ParticulaSangue(transform.position, rotacaoOpostaABala);
                break;
            case Tags.ChefeDeFase:
                ControlaChefe chefe = objetoDeColisao.GetComponent<ControlaChefe>();
                chefe.TomarDano(danoDoTiro);
                chefe.ParticulaSangue(transform.position, rotacaoOpostaABala);
                break;
        }

        // gameObject proprio objeto do script
        Destroy(gameObject);
    }
}
