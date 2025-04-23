using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace models
{
    public class preguntaFV
    {
        private string pregunta;
        private string respuesta;
        
        private string dificultad;

        public preguntaFV()
        {
        }

        public preguntaFV(string pregunta, string respuesta, string dificultad)
        {
            this.pregunta = pregunta;
            this.respuesta = respuesta;
       
            this.dificultad = dificultad;
        }
        public string Pregunta { get => pregunta; set => pregunta = value; }
        public string Respuesta { get => respuesta; set => respuesta = value; }
     
        public string Dificultad { get => dificultad; set => dificultad = value; }


    }
}