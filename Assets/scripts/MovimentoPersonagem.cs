using UnityEngine;

public class MovimentoPersonagem : MonoBehaviour
{
    private Rigidbody meuRigidbody;

    private void Awake()
    {
        meuRigidbody = GetComponent<Rigidbody>();
    }
    public void Movimentar(Vector3 direction, float velocity)
    {
        meuRigidbody
            .MovePosition(meuRigidbody
            .position + direction.normalized * velocity * Time.deltaTime);
    }

    public void Rotacionar(Vector3 direction)
    {
        if(Vector3.zero == direction) return;

        Quaternion newRot = Quaternion.LookRotation(direction);
        meuRigidbody.MoveRotation(newRot);
    }

    public void Morrer()
    {
        // None -> desabilita todos
        meuRigidbody.constraints = RigidbodyConstraints.None;
        meuRigidbody.velocity = Vector3.zero;
        meuRigidbody.isKinematic = false; // religar gravidade
        GetComponent<Collider>().enabled = false;
    }
}
