using UnityEngine;

public class ControlaJogador : MonoBehaviour, IMatavel, ICuravel
{
    // Start is called before the first frame update
    //void Start()
    //{

    //}

    public LayerMask mascaraDoChao;
    public GameObject TextoGameOver;
    public ControlaInterface ScriptControlaInterface;
    public AudioClip SomDeDano;
    public Status statusJogador;
    private Vector3 direction;
    private MovimentoJogador movimentaJogador;
    private AnimacaoPersonagem animaJogador;

    private void Start()
    {
        
        movimentaJogador = GetComponent<MovimentoJogador>();
        animaJogador = GetComponent<AnimacaoPersonagem>();
        statusJogador = GetComponent<Status>();
    }

    // Update is called once per frame
    void Update()
    {
        float axisX = Input.GetAxis(Tags.Horizontal); // A D e <- ->
        float axisZ = Input.GetAxis(Tags.Vertical);

        direction = new Vector3(axisX, 0, axisZ);

        // movimenta indefinidamente
        //transform.Translate(Vector3.forward);
        //transform.Translate(direction * Velocity * Time.deltaTime);

        animaJogador.Movimentar(direction.magnitude);       
    }

    void FixedUpdate()
    {
        movimentaJogador.Movimentar(direction, statusJogador.Velocidade);

        movimentaJogador.RotacaoJogador(mascaraDoChao);
    }

    public void TomarDano(int dano)
    {
        statusJogador.Vida -= dano;

        ScriptControlaInterface.AtualizarSliderVidaJogador();

        ControlaAudio.instancia.PlayOneShot(SomDeDano);

        if (statusJogador.Vida <= 0)
        {
            Morrer();
        }
    }

    public void Morrer()
    {
        //TextoGameOver.SetActive(true);
        ScriptControlaInterface.GameOver();
    }

    public void CurarVida(int quantidadeDeCura)
    {
        statusJogador.Vida += quantidadeDeCura;

        if(statusJogador.Vida > statusJogador.VidaInicial)
        {
            statusJogador.Vida = statusJogador.VidaInicial;
        }

        ScriptControlaInterface.AtualizarSliderVidaJogador();
    }
}
