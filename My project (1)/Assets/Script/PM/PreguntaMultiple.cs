using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace models
{
    public class PreguntaMultiple
    {
        private string pregunta;
        private string respuesta1;
        private string respuesta2;
        private string respuesta3;
        private string respuesta4;
        private string respuestaCorrecta;

        private string dificultad;

        public PreguntaMultiple()
        {
        }

        public PreguntaMultiple(string pregunta, string respuesta1, string respuesta2, string respuesta3, string respuesta4, string respuestaCorrecta,  string dificultad)
        {
            this.pregunta = pregunta;
            this.respuesta1 = respuesta1;
            this.respuesta2 = respuesta2;
            this.respuesta3 = respuesta3;
            this.respuesta4 = respuesta4;
            this.respuestaCorrecta = respuestaCorrecta;
            this.dificultad = dificultad;
        }

        public string Pregunta { get => pregunta; set => pregunta = value; }
        public string Respuesta1 { get => respuesta1; set => respuesta1 = value; }
        public string Respuesta2 { get => respuesta2; set => respuesta2 = value; }
        public string Respuesta3 { get => respuesta3; set => respuesta3 = value; }
        public string Respuesta4 { get => respuesta4; set => respuesta4 = value; }
        public string RespuestaCorrecta { get => respuestaCorrecta; set => respuestaCorrecta = value; }
        public string Dificultad { get => dificultad; set => dificultad = value; }
    }
}