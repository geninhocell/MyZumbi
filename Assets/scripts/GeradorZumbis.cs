using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GeradorZumbis : MonoBehaviour
{
    public GameObject Zumbi;
    public float TempoGerarZumbi = 1;
    public LayerMask LayerZumbi;
    private float contadorTempo = 0;
    private float distanciaDeGeracao = 3;
    private float distanciaDoJogadorParaGeracao = 20;
    private GameObject jogador;
    private int quantidadeMaximaDeZumbis = 2;
    private int quantidadeDeZumbis;
    private float tempoProximoAumentoDeDificuldade = 30;
    private float contadorDeAumentoDeDificuldade;

    private void Start()
    {
        contadorDeAumentoDeDificuldade = tempoProximoAumentoDeDificuldade;

        jogador = GameObject.FindWithTag(Tags.Jogador);
        for(int i = 0; i < quantidadeMaximaDeZumbis; i++)
        {
            StartCoroutine(GerarNovoZumbi());
        }
    }

    // Update is called once per frame
    void Update()
    {

        bool possoGerarZumbisPelaDistancia = Vector3.Distance(transform.position, jogador.transform.position) > distanciaDoJogadorParaGeracao;

        if (possoGerarZumbisPelaDistancia && quantidadeDeZumbis < quantidadeMaximaDeZumbis)
        {
            contadorTempo += Time.deltaTime;

            if (contadorTempo >= TempoGerarZumbi)
            {
                StartCoroutine(GerarNovoZumbi());
                contadorTempo = 0;
            }
        }

        if(Time.timeSinceLevelLoad > contadorDeAumentoDeDificuldade)
        {
            quantidadeMaximaDeZumbis++;
            contadorDeAumentoDeDificuldade = Time.timeSinceLevelLoad + tempoProximoAumentoDeDificuldade;
        }
    }

    // Criar icone(gizmo)
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(transform.position, distanciaDeGeracao); 
    }

    IEnumerator GerarNovoZumbi()
    {
        Vector3 posicaoDeCriacao = AleatorizarPosicao();
        Collider[] colisores = Physics.OverlapSphere(posicaoDeCriacao, 1, LayerZumbi);

        while(colisores.Length > 0)
        {
            posicaoDeCriacao = AleatorizarPosicao();
            colisores = Physics.OverlapSphere(posicaoDeCriacao, 1, LayerZumbi);
            yield return null;
        }
        ControlaZumbi controlaZumbi = Instantiate(Zumbi, posicaoDeCriacao, transform.rotation)
            .GetComponent<ControlaZumbi>();

        controlaZumbi.MeuGeradorZumbis = this;

        quantidadeDeZumbis++;
    }

    Vector3 AleatorizarPosicao()
    {
        Vector3 posicao = Random.insideUnitSphere * distanciaDeGeracao;
        posicao += transform.position;
        posicao.y = 0;

        return posicao;
    }

    public void DiminuirQuantidadeDeZumbis()
    {
        quantidadeDeZumbis--;
    }
}
