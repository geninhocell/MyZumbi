using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class ControlaInterface : MonoBehaviour
{
    private ControlaJogador scripControlaJogador;
    public Slider SliderVidaJogador;
    public GameObject PainelDeGameOver;
    public Text TextoTempoDeSobrevivencia;
    public Text TextoTempoDeSobrevivenciaMaxima;
    public Text TextoQuantidadeDeZumbisMortos;
    private float tempoMaximo;
    private int quantidadeDeZumbisMortos;

    void Start()
    {
        scripControlaJogador = GameObject.FindWithTag(Tags.Jogador)
            .GetComponent<ControlaJogador>();

        SliderVidaJogador.maxValue = scripControlaJogador.statusJogador.VidaInicial;
        AtualizarSliderVidaJogador();
        Time.timeScale = 1;

        tempoMaximo = PlayerPrefs.GetFloat(Tags.TempoMaximo);
    }

    public void AtualizarSliderVidaJogador()
    {
        SliderVidaJogador.value = scripControlaJogador.statusJogador.Vida;
    }

    public void GameOver()
    {
        PainelDeGameOver.SetActive(true);
        // pausar jogo
        Time.timeScale = 0;

        //Time.time -> Tempo total de jogo
        //Time.timeSinceLevelLoad -> desde que reiniciou
        int minutos = (int)Time.timeSinceLevelLoad / 60;
        int segundos = (int)Time.timeSinceLevelLoad % 60;
        TextoTempoDeSobrevivencia.text = "Você sobreviveu por " + minutos + "min e " + segundos + "seg";

        AjustarTempoMaximo(minutos, segundos);
    }

    void AjustarTempoMaximo(int minutos, int segundos)
    {
        if(Time.timeSinceLevelLoad > tempoMaximo)
        {
            tempoMaximo = Time.timeSinceLevelLoad;
            TextoTempoDeSobrevivenciaMaxima.text = 
                string.Format("Seu melhor tempo é {0}min e {1}seg", minutos, segundos);

            PlayerPrefs.SetFloat(Tags.TempoMaximo, tempoMaximo);
        }
        if(TextoTempoDeSobrevivenciaMaxima.text == "")
        {
            minutos = (int)tempoMaximo / 60;
            segundos = (int)tempoMaximo % 60;

            TextoTempoDeSobrevivenciaMaxima.text =
                string.Format("Seu melhor tempo é {0}min e {1}seg", minutos, segundos);
        }
    }

    public void AjustarQuantidadeDeZumbisMortos()
    {
        quantidadeDeZumbisMortos++;
        TextoQuantidadeDeZumbisMortos.text = string.Format("x {0}", quantidadeDeZumbisMortos);
    }

    public void Reiniciar()
    {
        SceneManager.LoadScene(Tags.SampleScene);
    }
}
