using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GeradorChefe : MonoBehaviour
{
    public GameObject chefePrefab;
    public float TempoEntreGeracoes = 60;
    public Transform[] PosicoesPossiveisDeGeracao;
    private ControlaInterface scriptControlaInterface;
    private float tempoProximaGeracao = 0;
    private Transform jogador;

    private void Start()
    {
        tempoProximaGeracao = TempoEntreGeracoes;
        scriptControlaInterface = GameObject.FindObjectOfType(typeof(ControlaInterface)) as ControlaInterface;
        jogador = GameObject.FindWithTag(Tags.Jogador).transform;
    }

    private void Update()
    {
        if(Time.timeSinceLevelLoad > tempoProximaGeracao)
        {
            Vector3 posicaoDeCriacao = CalcularPosicaoMaisDistanteDoJogador();
            Instantiate(chefePrefab, posicaoDeCriacao, Quaternion.identity);
            scriptControlaInterface.AparecerTextoChefeCriado();
            tempoProximaGeracao = Time.timeSinceLevelLoad + TempoEntreGeracoes;
        }
    }

    Vector3 CalcularPosicaoMaisDistanteDoJogador()
    {
        Vector3 posicaoDeMaiorDistancia = Vector3.zero;
        float maiorDistancia = 0;

        foreach(Transform posicao in PosicoesPossiveisDeGeracao)
        {
            float distanciaEntreOJogador = Vector3.Distance(posicao.position, jogador.position);
            if(distanciaEntreOJogador > maiorDistancia)
            {
                maiorDistancia = distanciaEntreOJogador;
                posicaoDeMaiorDistancia = posicao.position;
            }

        }

        return posicaoDeMaiorDistancia;
    }
}
