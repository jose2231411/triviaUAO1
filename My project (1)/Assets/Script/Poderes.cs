using System.Collections;
using System.Collections.Generic;

using UnityEngine;
using UnityEngine.UI;
using static UnityEngine.EventSystems.EventTrigger;

public class Poderes : MonoBehaviour
{
    public GameController Controller;
    private int RS;
    public Button change;
    public Button retry;
    public Button pause;
    private bool pauseUsed = false;

    private bool changeUsed = false;
    private bool retryUsed = false;

    // Start is called before the first frame update
    void Start()
    {
        change.interactable = false;
        retry.interactable = false;
        pause.interactable = false;

    }

    void Update()
    {
        RS = Controller.Rs;

        // Recargar individualmente
        if (RS >= 3 && changeUsed)
        {
            changeUsed = false;
            Debug.Log("Poder 'change' recargado.");
        }

        if (RS >= 5 && retryUsed)
        {
            retryUsed = false;
            Debug.Log("Poder 'retry' recargado.");
        }

        if (RS >= 4 && pauseUsed)
        {
            pauseUsed = false;
            Debug.Log("Poder 'pause' recargado.");
        }

        if (RS >= 6)
        {
            Controller.health += 1;
            Controller.Rs = 0;
        }

        // Habilitar botones si no están usados y cumple la racha
        change.interactable = RS >= 3 && !changeUsed;
        retry.interactable = RS >= 5 && !retryUsed;
        pause.interactable = RS >= 4 && !pauseUsed;
    }

    public void changeQuestion()
    {
        Controller.mostrarPreguntas();
        changeUsed = true;
        change.interactable = false;

        Debug.Log("Poder 'change' usado.");
    }

    public void retryQuestion()
    {
        Controller.ReintentarPregunta();
        retryUsed = true;
        retry.interactable = false;

        Debug.Log("Poder 'retry' usado.");
    }
    public void PausarTimer()
    {
        Controller.timer.enabled = false; // Pausar
        pauseUsed = true;
        pause.interactable = false;

        Debug.Log("Poder 'pause' usado. Temporizador pausado por 5 segundos.");

        StartCoroutine(ReanudarTimerLuegoDe(5f)); // Reanudar después de 5 segundos
    }

    IEnumerator ReanudarTimerLuegoDe(float segundos)
    {
        yield return new WaitForSeconds(segundos);
        Controller.timer.enabled = true;
        Debug.Log("Temporizador reanudado automáticamente.");
    }

}

