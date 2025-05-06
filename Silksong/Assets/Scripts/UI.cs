using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UI : MonoBehaviour
{
    //Player Data
    public PlayerData playerData;

    //Health
    public Image[] masks;
    public Sprite[] maskSprites;

    //Score
    public TextMeshProUGUI scoreText;

    //Beads
    public TextMeshProUGUI beadsText;

    public void Update()
    {
        int currHP = playerData.HP - 1;
        int score = playerData.score;
        int beads = playerData.beads;

        //Set Health
        for(int i = 0; i < masks.Length; i++)
        {
            if(currHP < i)
            {
                masks[i].sprite = maskSprites[1];
            } else
            {
                masks[i].sprite = maskSprites[0];
            }
        }

        //Set Score
        scoreText.text = $"{score:D6}";

        //Set Beads
        beadsText.text = $"x{beads:D2}";
    }

}
