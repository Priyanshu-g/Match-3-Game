using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class combatSetup : MonoBehaviour
{
    public int[] player1shields;
    public int[] player2shields;

    public Slider[] player1shieldsObjects;
    public Slider[] player2shieldsObjects;

    public SpriteRenderer player1;
    public SpriteRenderer player2; //Likely Enemy

    public Sprite player1Sprite;
    public Sprite player2Sprite;

    public Image backgroundObject;
    public Sprite background;
    // Start is called before the first frame update
    void Start()
    {
        if (player1Sprite != null && player2Sprite != null)
        {
            player1.sprite = player1Sprite;
            player2.sprite = player2Sprite;
        }
        if (background != null)
        {
            backgroundObject.sprite = background;
        }
        for (int i = 0; i < player1shields.Length; i++)
        {
            if (player1shields[i] > 100)
            {
                player1shields[i] = 100;
            }
            if (player2shields[i] > 100)
            {
                player2shields[i] = 100;
            }
            player1shields[i] = Random.Range(0, 100);
            player2shields[i] = Random.Range(0, 100);
            player1shieldsObjects[i].value = player1shields[i] / 100f;
            player2shieldsObjects[i].value = player2shields[i] / 100f;
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
