using UnityEngine;

public class ControlaZumbi : MonoBehaviour, IMatavel

{
    public GameObject Jogador, KitMedicoPrefab, ParticulaSangueZumbi;
    [HideInInspector]
    public GeradorZumbis MeuGeradorZumbis;
    public AudioClip SomMorteZumbi;
    private ControlaInterface scriptControlaInterface;
    private MovimentoPersonagem movimentaZumbi;
    private AnimacaoPersonagem animaZumbi;
    private Status statusZumbi;
    private Vector3 posicaoAleatoria;
    private Vector3 direcao;
    private float contadorVagar;
    private float tempoEntrePosicoesAleatorias = 4; // 4 segundos
    private float porcentagemGerarkitMedico = 0.1f;
    

    private void Start()
    {
        Jogador = GameObject.FindWithTag(Tags.Jogador);
        movimentaZumbi = GetComponent<MovimentoPersonagem>();
        animaZumbi = GetComponent<AnimacaoPersonagem>();
        statusZumbi = GetComponent<Status>();

        aleatorizarZumbi();
        scriptControlaInterface = GameObject.FindObjectOfType(typeof(ControlaInterface)) as ControlaInterface;
    }

    private void aleatorizarZumbi()
    {
        // 1 a 27
        int geraTipoZumbi = Random.Range(1, transform.childCount);
        transform.GetChild(geraTipoZumbi).gameObject.SetActive(true);
    }

    private void FixedUpdate()
    {
        // calcular a distancia do zumbi para o jogador
        // transform.position => posição do zumbi
        // Jogador.transform.position => posição do jogador
        float distance = Vector3.Distance(transform.position, Jogador.transform.position);

        movimentaZumbi.Rotacionar(direcao);
        animaZumbi.Movimentar(direcao.magnitude);

        // olhar para jogador
        //Quaternion newRot = Quaternion.LookRotation(direction);
        //rigidbodyZumbi.MoveRotation(newRot);


        if(distance > 15)
        {
            Vagar();
        }
        else if (distance > 2.5)
        {
            // está longe
            // 2.5 => raio do colisor jogador 1 + colisor zumbi 1 = 2

            direcao = Jogador.transform.position - transform.position;

            movimentaZumbi.Movimentar(direcao, statusZumbi.Velocidade);
            // direction.normalized => para 1
            //rigidbodyZumbi
            //.MovePosition(rigidbodyZumbi
            //.position + direction.normalized * Velocity * Time.deltaTime);

            animaZumbi.Atacar(false);
        }
        else
        {
            direcao = Jogador.transform.position - transform.position;
            animaZumbi.Atacar(true);
        }
    }

    void Vagar()
    {
        contadorVagar -= Time.deltaTime;

        if(contadorVagar <= 0)
        {
            posicaoAleatoria = AleatorizarPosicao();
            contadorVagar += tempoEntrePosicoesAleatorias + Random.Range(-1f, 1f);
        }

        bool ficouPertoOSuficiente = Vector3.Distance(transform.position, posicaoAleatoria) <= 0.05;

        if (ficouPertoOSuficiente == false)
        {
            direcao = posicaoAleatoria - transform.position;
            movimentaZumbi.Movimentar(direcao, statusZumbi.Velocidade);
        }
    }

    Vector3 AleatorizarPosicao()
    {
        // insideUnitSphere -> gera posição em uma esfera com raio de 1
        Vector3 posicao = Random.insideUnitSphere * 10;
        posicao += transform.position;
        posicao.y = transform.position.y;

        return posicao;
    }

    // Metodo deve ter o mesmo nome do evento
    // Quando evento acontecer, esse metodo será chamado
    void AtacaJogador()
    {
        int dano = Random.Range(10, 30);
        Jogador.GetComponent<ControlaJogador>().TomarDano(dano);
    }

    public void TomarDano(int dano)
    {
        statusZumbi.Vida -= dano;

        if(statusZumbi.Vida <= 0)
        {
            Morrer();
        }
    }

    public void ParticulaSangue(Vector3 posicao, Quaternion rotacao)
    {
        Instantiate(ParticulaSangueZumbi, posicao, rotacao);
    }
    public void Morrer()
    {
        Destroy(gameObject, 2);
        animaZumbi.Morrer();
        movimentaZumbi.Morrer();
        // desligar script
        this.enabled = false;
        ControlaAudio.instancia.PlayOneShot(SomMorteZumbi);
        VerificarGeracaoKitMedico(porcentagemGerarkitMedico);
        scriptControlaInterface.AjustarQuantidadeDeZumbisMortos();
        MeuGeradorZumbis.DiminuirQuantidadeDeZumbis();
    }

    void VerificarGeracaoKitMedico(float porcentagemGeracao)
    {
        // Random.value -> gera entre 0 e 1
        // Quaternion.identity -> rotação zerada
        if (Random.value <= porcentagemGeracao)
        {
            Instantiate(KitMedicoPrefab, transform.position, Quaternion.identity);
        }
    }
}
