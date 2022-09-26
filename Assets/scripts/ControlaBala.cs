using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ControlaBala : MonoBehaviour
{
    public float Velocity = 20;
    private Rigidbody rigidbodyBala;

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
        if(objetoDeColisao.tag == "Inimigo")
        {
            Destroy(objetoDeColisao.gameObject);
        }

        // gameObject proprio objeto do script
        Destroy(gameObject);
    }
}
