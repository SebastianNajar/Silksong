using UnityEngine;
using UnityEngine.UI;

public class UI : MonoBehaviour
{
    //Player Data
    public PlayerData playerData;

    public Image[] masks;
    public Sprite[] maskSprites;

    public void Update()
    {
        int maxHP = playerData.maxHP - 1;
        int currHP = playerData.HP - 1;

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
    }
}
