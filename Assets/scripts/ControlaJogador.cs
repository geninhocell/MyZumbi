using UnityEngine;
using UnityEngine.SceneManagement;

public class ControlaJogador : MonoBehaviour
{
    // Start is called before the first frame update
    //void Start()
    //{

    //}

    public float Velocity = 10;
    public LayerMask mascaraDoChao;
    public GameObject TextoGameOver;
    public bool Vivo = true;
    private Vector3 direction;
    private Rigidbody rigidbodyJogador;
    private Animator animatorJogador;
    public int Vida = 100;

    private void Start()
    {
        Time.timeScale = 1;
        rigidbodyJogador = GetComponent<Rigidbody>();
        animatorJogador = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        float axisX = Input.GetAxis("Horizontal"); // A D e <- ->
        float axisZ = Input.GetAxis("Vertical");

        direction = new Vector3(axisX, 0, axisZ);

        // movimenta indefinidamente
        //transform.Translate(Vector3.forward);
        //transform.Translate(direction * Velocity * Time.deltaTime);

        if(direction != Vector3.zero)
        {
            animatorJogador.SetBool("Moving", true);
        }
        else
        {
            animatorJogador.SetBool("Moving", false);
        }

        if(Vivo == false)
        {
            if (Input.GetButtonDown("Fire1"))
            {
                SceneManager.LoadScene("SampleScene");
            }
        }
    }

    void FixedUpdate()
    {
        rigidbodyJogador
            .MovePosition(
                rigidbodyJogador.position + direction * Velocity * Time.deltaTime
            );

        // apartir da camera principal, criar raio para posi��o do mouse
        Ray raio = Camera.main.ScreenPointToRay(Input.mousePosition);

        // desenhar raio na tela
        Debug.DrawRay(raio.origin, raio.direction * 100, Color.red);

        // posi��o de impacto
        RaycastHit impacto;

        // Gerar o raio
        // impacto sera atribuido
        // 100 distancia
        // mascaraDoChao pegar somente ch�o
        if (Physics.Raycast(raio, out impacto, 100, mascaraDoChao))
        {
            // guardar posi��o ao ponto de impacto
            // transform.position, posi��o do jogador
            Vector3 posicaoMiraJogador = impacto.point - transform.position;

            // cancelar y para jogador n�o olha para o ch�o
            posicaoMiraJogador.y = transform.position.y;

            // pagar nova vis�o do jogador
            Quaternion novaRotacao = Quaternion.LookRotation(posicaoMiraJogador);

            // atualizar rota��o do jogador
            rigidbodyJogador.MoveRotation(novaRotacao);
        }
    }
}
