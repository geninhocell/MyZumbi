using UnityEngine;

public class ControlaCamera : MonoBehaviour

{
    public GameObject Jogador;
    private Vector3 distance;

    // Start is called before the first frame update
    void Start()
    {
        distance = transform.position - Jogador.transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = Jogador.transform.position + distance;
    }
}
