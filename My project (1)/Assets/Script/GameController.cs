using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using models;
using TMPro;
using UnityEngine.UIElements;
using UnityEngine.UI;
using Unity.VisualScripting;
using UnityEngine.SceneManagement;
//INTEGRANTES 

//JoseAndresCriolloEcheverri

public class GameController : MonoBehaviour
{
    #region declaracion de variables
    public string currSubject = "";
    string lineaLeida = "";
    List<PreguntaMultiple> listaPreguntasMultiples;

    List<preguntaFV> listaPreguntasFV;
    List<preguntaFV> listaPFVF;
    List<preguntaFV> listaPFVD;
    List<PreguntaMultiple> listaPMF;
    List<PreguntaMultiple> listaPMD;
    string respuestaPM;
    public bool dificiles = false;
    public bool PMvacio = false;
    public bool PAvacio = false;
    public bool PFVvacio = false;
    public int RC = 0;
    public int Rs = 0;
    public int RI = 0;
    public float score = 0;
    List<int> tiposDisponibles = new List<int> { 1, 2};

    public TextMeshProUGUI textPregunta;
    
    public TextMeshProUGUI textPreguntaFV;
    public TextMeshProUGUI textResp1;
    public TextMeshProUGUI textResp2;
    public TextMeshProUGUI textResp3;
    public TextMeshProUGUI textResp4;
    public TextMeshProUGUI textRC;
    public TextMeshProUGUI textRI;
    public TextMeshProUGUI textScore;
    public GameObject panelUI;
    public GameObject PanelRC;
    public GameObject PanelRI;
    public GameObject PanelLvlup;
    public GameObject PanelDif;
    public GameObject PanelPM;  
    public GameObject PanelPFV;
    public GameObject PanelEND;
    public TimerFill timer;
    public int health = 3;
    // Última pregunta mostrada
    private PreguntaMultiple ultimaPM;
    private preguntaFV ultimaPFV;
    private int tipoUltimaPregunta = 0; // 1 = PM, 2 = FV
    public GameManager gameManager;

    #endregion

    // Start is called before the first frame update
    void Start()
    {
        #region inicializacion
        Debug.Log("currSubject recibido: " + currSubject);
        PanelRC.SetActive(false);
        PanelRI.SetActive(false);
        PanelDif.SetActive(false);
        PanelEND.SetActive(false);
        PanelPFV.SetActive(false);
        PanelLvlup.SetActive(false);
        listaPMF = new List<PreguntaMultiple>();
        listaPMD = new List<PreguntaMultiple>();
        listaPFVF = new List<preguntaFV>();
        listaPFVD = new List<preguntaFV>();
        listaPreguntasMultiples = new List<PreguntaMultiple>();
        listaPreguntasFV = new List<preguntaFV>();
        LecturaPreguntasFV();
        LecturaPreguntasMultiples();
        organizar();
        mostrarPreguntas();
        gameManager = GameObject.FindGameObjectWithTag("manager").GetComponent<GameManager>();
        #endregion
    }

    // Update is called once per frame
    void Update()
    {
       if(health > 3) { health = 3; }

    }
    #region organizarpreguntas
    public void organizar()
    {
        listaPMF.Clear();
        listaPMD.Clear();
        listaPFVF.Clear();
        listaPFVD.Clear();

        for (int i = 0; i < listaPreguntasMultiples.Count; i++)
        {
            if (listaPreguntasMultiples[i].Dificultad == "facil")
            {
                listaPMF.Add(listaPreguntasMultiples[i]);
            }
            else
            {
                listaPMD.Add(listaPreguntasMultiples[i]);
            }
        }

        for (int i = 0; i < listaPreguntasFV.Count; i++)
        {
            if (listaPreguntasFV[i].Dificultad == "facil")
            {
                listaPFVF.Add(listaPreguntasFV[i]);
            }
            else
            {
                listaPFVD.Add(listaPreguntasFV[i]);
            }
        }
    }

    #endregion

    #region tipo de pregunta y pase a dificil
    public void mostrarPreguntas()
    {
        if (tiposDisponibles.Count == 0 && dificiles || health == 0)
        {
            PanelDif.SetActive(true);
            panelUI.SetActive(false);
            textRC.SetText(RC.ToString());
            textRI.SetText(RI.ToString());
            textScore.SetText(score.ToString());
            return;
        }
        if (tiposDisponibles.Count == 0)
        {
            dificiles = true;
            PanelLvlup.SetActive(true);
            PMvacio = false;
            PFVvacio = false;
            tiposDisponibles.Add(1);
            tiposDisponibles.Add(2);
            
            return;
        }
        Debug.Log("Mostrando pregunta...");
        int tipo = tiposDisponibles[UnityEngine.Random.Range(0, tiposDisponibles.Count)];
        Debug.Log("tipo: " + tipo);
        
        switch (tipo)
        {
            case 1:
               
                PanelPFV.SetActive(false);
                MostrarPreguntaMultiple(dificiles ? listaPMD : listaPMF, ref PMvacio, PanelPM);
                panelUI.SetActive(true);
                break;
            case 2:
               
                PanelPM.SetActive(false);
                MostrarPreguntaFV(dificiles ? listaPFVD : listaPFVF, ref PFVvacio, PanelPFV);
                panelUI.SetActive(true);
                break;
        }
       
       
    }
    #endregion

    #region mostrarpreguntas
    private void MostrarPreguntaMultiple(List<PreguntaMultiple> lista, ref bool vacioFlag, GameObject panel)
    {
        if (lista.Count == 0)
        {
            vacioFlag = true;
            tiposDisponibles.Remove(1);
            panel.SetActive(false);
            Debug.Log("No hay más preguntas múltiples.");
            mostrarPreguntas();
            return;
        }

        panel.SetActive(true);
        int i = UnityEngine.Random.Range(0, lista.Count);

        PreguntaMultiple preguntaSeleccionada = lista[i];
        ultimaPM = preguntaSeleccionada;
        tipoUltimaPregunta = 1;

        textPregunta.text = preguntaSeleccionada.Pregunta;
        textResp1.text = preguntaSeleccionada.Respuesta1;
        textResp2.text = preguntaSeleccionada.Respuesta2;
        textResp3.text = preguntaSeleccionada.Respuesta3;
        textResp4.text = preguntaSeleccionada.Respuesta4;
        respuestaPM = preguntaSeleccionada.RespuestaCorrecta;

        timer.resetFill();
        tiempo(textPregunta.text);

        lista.RemoveAt(i);
        Debug.Log("PM actuales: " + lista.Count);
    }
    private void MostrarPreguntaFV(List<preguntaFV> lista, ref bool vacioFlag, GameObject panel)
    {
        if (lista.Count == 0)
        {
            vacioFlag = true;
            panel.SetActive(false);
            tiposDisponibles.Remove(2);
            Debug.Log("No hay más preguntas falso verdadero.");
            mostrarPreguntas();
            return;
        }

        panel.SetActive(true);
        int i = UnityEngine.Random.Range(0, lista.Count);

        preguntaFV preguntaSeleccionada = lista[i];
        ultimaPFV = preguntaSeleccionada;
        tipoUltimaPregunta = 2;

        textPreguntaFV.text = preguntaSeleccionada.Pregunta;
        respuestaPM = preguntaSeleccionada.Respuesta;

        timer.resetFill();
        tiempo(textPreguntaFV.text);

        lista.RemoveAt(i);
        Debug.Log("PFV actuales: " + lista.Count);
    }

    public void ReintentarPregunta()
    {
        if (tipoUltimaPregunta == 1 && ultimaPM != null)
        {
           
            PanelPFV.SetActive(false);
            PanelPM.SetActive(true);

            textPregunta.text = ultimaPM.Pregunta;
            textResp1.text = ultimaPM.Respuesta1;
            textResp2.text = ultimaPM.Respuesta2;
            textResp3.text = ultimaPM.Respuesta3;
            textResp4.text = ultimaPM.Respuesta4;
            respuestaPM = ultimaPM.RespuestaCorrecta;
            timer.resetFill();
            tiempo(textPregunta.text);
        }
        else if (tipoUltimaPregunta == 2 && ultimaPFV != null)
        {
          
            PanelPM.SetActive(false);
            PanelPFV.SetActive(true);

            textPreguntaFV.text = ultimaPFV.Pregunta;
            respuestaPM = ultimaPFV.Respuesta;
            timer.resetFill();
            tiempo(textPreguntaFV.text);
        }
        else
        {
            Debug.Log("No hay pregunta previa para reintentar.");
        }
    }

    #endregion

    #region comprobacion de respuesta

    public void puntaje() 
    {
        int pts = 0;
        if (dificiles)
        {
             pts = 500;
        }
        else
        {
             pts = 250;
        }
        score += Math.Max(0, timer.max - timer.current) * pts;
    }
    public void comprobarRespuesta1()
    {
        if (textResp1.text.Equals(respuestaPM))
        {
           PanelRC.SetActive(true);
           PanelRI.SetActive(false);
            panelUI.SetActive(false);
            RC += 1;
           Rs += 1;
           puntaje();
        }
        else
        {
            PanelRC.SetActive(false);
            PanelRI.SetActive(true);
            panelUI.SetActive(false);
            RI += 1;
            Rs = 0;
            health -=1;
        }
    }

    public void comprobarRespuesta2()
    {
        if (textResp2.text.Equals(respuestaPM))
        {
            PanelRC.SetActive(true);
            PanelRI.SetActive(false);
            panelUI.SetActive(false);
            RC += 1;
            Rs += 1;
            puntaje();
        }
        else
        {
            PanelRC.SetActive(false);
            PanelRI.SetActive(true);
            panelUI.SetActive(false);
            RI += 1;
            Rs = 0;
            health -= 1;
        }
    }
    public void comprobarRespuesta3()
    {
        if (textResp3.text.Equals(respuestaPM))
        {
            PanelRC.SetActive(true);
            PanelRI.SetActive(false);
            panelUI.SetActive(false);
            RC += 1;
            Rs += 1;
            puntaje();
        }
        else
        {
            PanelRC.SetActive(false);
            PanelRI.SetActive(true);
            panelUI.SetActive(false);
            RI += 1;
            Rs = 0;
            health -= 1;
        }
    }
    public void comprobarRespuesta4()
    {
        if (textResp4.text.Equals(respuestaPM))
        {
            PanelRC.SetActive(true);
            PanelRI.SetActive(false);
            panelUI.SetActive(false);
            RC += 1;
            Rs += 1;
            puntaje();
        }
        else
        {
            PanelRC.SetActive(false);
            PanelRI.SetActive(true);
            panelUI.SetActive(false);
            RI += 1;
            Rs = 0;
            health -= 1;
        }
    }

    public void comprobarRespuestaFV(bool escogido) 
    {
        if (escogido == bool.Parse(respuestaPM))
        {
            PanelRC.SetActive(true);
            PanelRI.SetActive(false);
            panelUI.SetActive(false);
            RC += 1;
            Rs += 1;
            puntaje();
        }
        else 
        {
            PanelRC.SetActive(false);
            PanelRI.SetActive(true);
            panelUI.SetActive(false);
            RI += 1;
            Rs = 0;
            health -= 1;
        }

    }

    #endregion

    #region Lectura archivos
    public void LecturaPreguntasMultiples()
    {
        try
        {
            string filename = "Assets/Files/" + currSubject + "PM.txt";
            Debug.Log(filename);
            StreamReader sr1 = new StreamReader(filename);
            while ((lineaLeida = sr1.ReadLine()) != null)
            {


                string[] lineaPartida = lineaLeida.Split("-");


                if (lineaPartida.Length < 7)
                {
                    Debug.LogWarning("Línea inválida (menos de 7 campos): " + lineaLeida);
                    continue;
                }

                string pregunta = lineaPartida[0];
                string respuesta1 = lineaPartida[1];
                string respuesta2 = lineaPartida[2];
                string respuesta3 = lineaPartida[3];
                string respuesta4 = lineaPartida[4];
                string respuestaCorrecta= lineaPartida[5];
                string difucltad = lineaPartida[6];

                if (lineaPartida.Length < 7)
{
    Debug.LogWarning("Línea inválida (menos de 7 campos): " + lineaLeida);
    continue;
}



                PreguntaMultiple objPM=new PreguntaMultiple(pregunta, respuesta1, respuesta2, respuesta3,
                    respuesta4, respuestaCorrecta, difucltad);

                listaPreguntasMultiples.Add(objPM);

            }
            Debug.Log("PreguntasM: " + listaPreguntasMultiples.Count);
        }
        catch(Exception e) 
        { 
            Debug.Log("ERROR!!!!! "+e.ToString());
        }
        finally
        { Debug.Log("Executing finally block."); }
    }
 
    public void LecturaPreguntasFV()
    {
        try
        {
            string filename = "Assets/Files/" + currSubject + "PFV.txt";
            StreamReader sr1 = new StreamReader(filename);
            while ((lineaLeida = sr1.ReadLine()) != null)
            {
                string[] lineaPartida = lineaLeida.Split("-");
                string pregunta = lineaPartida[0];
                string respuesta = lineaPartida[1];
                string dificultad = lineaPartida[2];


                preguntaFV objPA = new preguntaFV(pregunta, respuesta, dificultad);

                listaPreguntasFV.Add(objPA);

            }
            Debug.Log("PreguntasFV:  " + listaPreguntasFV.Count);
        }
        catch (Exception e)
        {
            Debug.Log("ERROR!!!!! " + e.ToString());
        }
        finally
        { Debug.Log("Executing finally block."); }
    }

    #endregion

    public void tiempo(string pregunta)
    {
        double time = 0;
        for(int i = 0; i < pregunta.Length; i++) 
        {
            time += 0.5;
        }
        timer.max = (int) time;
    }
    public void save()
    {
        gameManager.getScore();
        gameManager.saveData();
    }
}
