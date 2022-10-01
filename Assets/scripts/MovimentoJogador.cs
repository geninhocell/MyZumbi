using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovimentoJogador : MovimentoPersonagem
{
    public void RotacaoJogador(LayerMask mascaraDoChao)
    {
        // apartir da camera principal, criar raio para posição do mouse
        Ray raio = Camera.main.ScreenPointToRay(Input.mousePosition);

        // desenhar raio na tela
        Debug.DrawRay(raio.origin, raio.direction * 100, Color.red);

        // posição de impacto
        RaycastHit impacto;

        // Gerar o raio
        // impacto sera atribuido
        // 100 distancia
        // mascaraDoChao pegar somente chão
        if (Physics.Raycast(raio, out impacto, 100, mascaraDoChao))
        {
            // guardar posição ao ponto de impacto
            // transform.position, posição do jogador
            Vector3 posicaoMiraJogador = impacto.point - transform.position;

            // cancelar y para jogador não olha para o chão
            posicaoMiraJogador.y = transform.position.y;

            Rotacionar(posicaoMiraJogador);

            //// pagar nova visão do jogador
            //Quaternion novaRotacao = Quaternion.LookRotation(posicaoMiraJogador);

            //// atualizar rotação do jogador
            //rigidbodyJogador.MoveRotation(novaRotacao);
        }
    }
}
