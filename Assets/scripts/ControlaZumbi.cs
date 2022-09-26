using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ControlaZumbi : MonoBehaviour

{
    public GameObject Jogador;
    public float Velocity = 5;
    private Rigidbody rigidbodyZumbi;
    private Animator animatorZumbi;

    private void Start()
    {
        Jogador = GameObject.FindWithTag("Jogador");
        // 1 a 27
        int geraTipoZumbi = Random.Range(1, 28);
        //
        transform.GetChild(geraTipoZumbi).gameObject.SetActive(true);

        rigidbodyZumbi = GetComponent<Rigidbody>();
        animatorZumbi = GetComponent<Animator>();
    }

    private void FixedUpdate()
    {
        // GetComponent<Rigidbody>().position posi��o atual

        Vector3 direction = Jogador.transform.position - transform.position;

        // calcular a distancia do zumbi para o jogador
        // transform.position => posi��o do zumbi
        // Jogador.transform.position => posi��o do jogador
        float distance = Vector3.Distance(transform.position, Jogador.transform.position);

        // olhar para jogador
        Quaternion newRot = Quaternion.LookRotation(direction);
        rigidbodyZumbi.MoveRotation(newRot);


        // est� longe
        // 2.5 => raio do colisor jogador 1 + colisor zumbi 1 = 2
        if (distance > 2.5)
        {
            // direction.normalized => para 1
            rigidbodyZumbi
            .MovePosition(rigidbodyZumbi
            .position + direction.normalized * Velocity * Time.deltaTime);

            animatorZumbi.SetBool("Atacando", false);
        }
        else
        {
            animatorZumbi.SetBool("Atacando", true);
        }
    }

    // Metodo deve ter o mesmo nome do evento
    // Quando evento acontecer, esse metodo ser� chamado
    void AtacaJogador()
    {
        // pausar jogo
        Time.timeScale = 0;
        Jogador.GetComponent<ControlaJogador>().TextoGameOver.SetActive(true);
        Jogador.GetComponent<ControlaJogador>().Vivo = false;
    }
}
