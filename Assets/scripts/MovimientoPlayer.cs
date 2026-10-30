using UnityEngine;
using UnityEngine.InputSystem;


[RequireComponent(typeof(Rigidbody2D))]
public class MovimientoPlayer : MonoBehaviour
{

    public float fuerza = 5f;
    public float fuerzaImpulso = 4f;
    public Rigidbody2D rigidbody2;

    private Vector2 movimiento;
    private Vector3 posicionInicial;
    private bool enSuelo;

    void Awake()
    {
        rigidbody2 = GetComponent<Rigidbody2D>();
        posicionInicial = transform.position;
    }

    public void OnMove(InputValue value)
    {        movimiento = new Vector2(value.Get<Vector2>().x, 0f);
    }

    public void OnJump(InputValue value)
    {
        if (!value.isPressed || !enSuelo)
        {
            return;
        }

        rigidbody2.AddForce(Vector2.up * fuerzaImpulso, ForceMode2D.Impulse);
        enSuelo = false;
    }

    void FixedUpdate()
    {
        rigidbody2.AddForce(movimiento * fuerza);
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.GetContact(0).normal.y > 0.5f)
        {
            enSuelo = true;
        }

        if (collision.gameObject.name == "Meta")
        {
            Debug.Log("Llegaste a la meta.");
        }

        if (collision.gameObject.name == "ZonaDanio")
        {
            Debug.Log("Tocaste una zona de daño.");
            transform.position = posicionInicial;
            rigidbody2.linearVelocity = Vector2.zero;
        }
    }
}
