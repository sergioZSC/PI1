
using UnityEngine;
using UnityEngine.UI;
using System;

// Observador
public class GameManager : MonoBehaviour
{
    public GameObject paddle1;
    public GameObject paddle2;
    public GameObject ball;
    public Text tituloUI;
    public Text mensajeUI;
    public Text modalidadUI;
    public Text score1UI;
    public Text score2UI;

    private bool mRunning = false;
    private BallMovementManager mBallMovementManager;
    private MovementManager mMovementManager1;
    private MovementManager mMovementManager2;

    private Modos modo = Modos.MULTIJUGADOR;
    private int mScoreJugador1 = 0;
    private int mScoreJugador2 = 0;

    private void Start()
    {
        mMovementManager1 = paddle1.GetComponent<MovementManager>();
        mMovementManager2 = paddle2.GetComponent<MovementManager>();

        mBallMovementManager = ball.GetComponent<BallMovementManager>();
        mBallMovementManager.AddGoalScoredDelegate(OnGoalScoredDelegate);

    }

    private void Update()
    {
        if (!mRunning && Input.GetKeyDown(KeyCode.LeftControl)&& modo==Modos.MULTIJUGADOR)
        {
            modo = Modos.UNJUGADOR;
            modalidadUI.text = "Un Jugador";

        }else if (!mRunning && Input.GetKeyDown(KeyCode.LeftControl) && modo == Modos.UNJUGADOR)
        {
            modo = Modos.MULTIJUGADOR;
            modalidadUI.text = "Multijugador";
        }

        if (!mRunning && Input.GetKeyDown(KeyCode.Space))
        {
            mMovementManager1.addBallModo(ball,modo);
            mMovementManager2.addBallModo(ball,modo);
            StartGame();
            Debug.Log(modo);
        }
    }

    private void StartGame()
    {
        ball.SetActive(true);
        mBallMovementManager.StartGame();
        tituloUI.gameObject.SetActive(false);
        mensajeUI.gameObject.SetActive(false);
        score1UI.gameObject.SetActive(false);
        score2UI.gameObject.SetActive(false);
        modalidadUI.gameObject.SetActive(false);
        mRunning = true;
    }

    public void OnGoalScoredDelegate(object sender, EventArgs e)
    {
        Debug.Log("Goal");

        // Actualizar los score
        GoalScoredData data = e as GoalScoredData;
        if (data.jugador == "Paddle1")
        {
            mScoreJugador1++;
        }else
        {
            mScoreJugador2++;
        }


        tituloUI.text = "GOL!";
        mensajeUI.text = "Presione Espacio para continuar...\n Presione Ctrl para cambiar de modalidad...";
        score1UI.text = mScoreJugador1.ToString();
        score2UI.text = mScoreJugador2.ToString();
        tituloUI.gameObject.SetActive(true);
        mensajeUI.gameObject.SetActive(true);
        score1UI.gameObject.SetActive(true);
        score2UI.gameObject.SetActive(true);
        modalidadUI.gameObject.SetActive(true);
        mRunning = false;
    }
}
