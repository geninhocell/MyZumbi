using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimacaoPersonagem : MonoBehaviour
{
    private Animator meuAnimator;

    private void Awake()
    {
        meuAnimator = GetComponent<Animator>();
    }
    public void Atacar(bool atacando)
    {
        meuAnimator.SetBool(Tags.Atacando, atacando);
    }

    public void Movimentar(float valorDeMovimento)
    {
        meuAnimator.SetFloat(Tags.Movendo, valorDeMovimento);
    }

    public void Morrer()
    {
        meuAnimator.SetTrigger(Tags.Morrer);
    }
}
