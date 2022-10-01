using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovimentoJogador : MovimentoPersonagem
{
    public void RotacaoJogador(LayerMask mascaraDoChao)
    {
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

            Rotacionar(posicaoMiraJogador);

            //// pagar nova vis�o do jogador
            //Quaternion novaRotacao = Quaternion.LookRotation(posicaoMiraJogador);

            //// atualizar rota��o do jogador
            //rigidbodyJogador.MoveRotation(novaRotacao);
        }
    }
}
