using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Tabla : MonoBehaviour
{
   
    public TextMeshProUGUI  leaderboardText;
    public void ShowLeaderboard()
    {
        List<PlayerData> leaderboard = Save.LoadAllPlayers();

        // Ordenar por puntaje descendente
        leaderboard.Sort((a, b) => b.score.CompareTo(a.score));

        // Mostrar en UI
        leaderboardText.text = "";
        for (int i = 0; i < leaderboard.Count; i++)
        {
            leaderboardText.text += $"{i + 1}. {leaderboard[i].name} - {leaderboard[i].score}\n";
        }
    }
    private void Start()
    {
        ShowLeaderboard(); // Mostrar tabla al iniciar si hay datos guardados
    }
}
