using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GeradorChefe : MonoBehaviour
{
    public GameObject chefePrefab;
    public float TempoEntreGeracoes = 60;
    private float tempoProximaGeracao = 0;

    private void Start()
    {
        tempoProximaGeracao = TempoEntreGeracoes;
    }

    private void Update()
    {
        if(Time.timeSinceLevelLoad > tempoProximaGeracao)
        {
            Instantiate(chefePrefab, transform.position, Quaternion.identity);
            tempoProximaGeracao = Time.timeSinceLevelLoad + TempoEntreGeracoes;
        }
    }
}
