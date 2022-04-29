using UnityEngine;

public enum TipoJugador
{
    JUGADOR1, JUGADOR2
}
public enum Modos { UNJUGADOR, MULTIJUGADOR }

public class MovementManager : MonoBehaviour
{
    public float speed;
    public TipoJugador tipo;
    private Modos modo = Modos.MULTIJUGADOR;
    private GameObject ball;

    public void addBallModo(GameObject mball, Modos mModo)
    {
        ball = mball;
        modo = mModo;
    }
    private void Update()
    {
        float movement;
        if (tipo == TipoJugador.JUGADOR1)
        {
            // Leer el input del usuario
            movement = Input.GetAxis("Vertical");
            aceleracion(movement);
        }
        else
        {
            if (modo == Modos.MULTIJUGADOR) 
            {
                movement = Input.GetAxis("Mouse Y");
                aceleracion(movement);
            }
            else
            {
                movement = ball.GetComponent<Transform>().position.y-1;
                GetComponent<Transform>().position = new Vector3(
                    GetComponent<Transform>().position.x,
                    Mathf.Clamp(movement,-7f,7f),
                    0f);
            }
            
        }
        
        
    }
    private void aceleracion(float movement)
    {
        Vector3 actualPos = GetComponent<Transform>().position;
        GetComponent<Transform>().position = new Vector3(
                actualPos.x,
                Mathf.Clamp(actualPos.y + (speed * movement * Time.deltaTime), -7f, 7f),
                actualPos.z
        );
    }
}
