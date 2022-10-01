using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ControlaArma : MonoBehaviour
{
    public GameObject Bala;
    public GameObject CanoDaArma;
    public AudioClip SomDoTiro;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        // Fire1 mouse 0, botão erquerdo do mause
        if (Input.GetButtonDown(Tags.Fire1))
        {
            // criar bala
            Instantiate(Bala, CanoDaArma.transform.position, CanoDaArma.transform.rotation);
            ControlaAudio.instancia.PlayOneShot(SomDoTiro);
        }
    }
}
