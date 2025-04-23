using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class Health : MonoBehaviour
{
    public int hp;
    public int numHP;

    public Image[] hearts;
    public Sprite full;
    public Sprite empty;
    public GameController gameController;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        hp = gameController.health;
        if (hp > numHP) 
        {
            hp = numHP;
        }
        for (int i = 0; i < hearts.Length; i++) 
        {
            if (i < hp)
            {
                hearts[i].sprite = full;
            }
            else 
            {
                hearts[i].sprite = empty;
            }
            if (i < numHP)
            {
                hearts[i].enabled = true;
            }
            else
            {
                hearts[i].enabled = false;
            }
        }
    }
}
