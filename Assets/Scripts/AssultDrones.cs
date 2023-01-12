using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AssultDrones : MonoBehaviour
{
    //Dev Set
    public GameObject enemy;
    public GameObject shield;
    public float sped;
    public Slider enemyHealth;
    public Slider enemyShield;
    public Text powerDisplay;
    public bool Opp;

    //Script Public
    public bool attack = false;
    float power;
    Vector2 ogPosit;
    bool hitEnemy = false;
    bool damageDealt = false;
    bool hitShield = false;

    // Start is called before the first frame update
    void Start()
    {
        ogPosit = transform.position;
        if (Opp)
        {
            sped *= -1;
        }
    }

    // Update is called once per frame
    void Update()
    {
        power = float.Parse(powerDisplay.text);
        if (attack && !hitEnemy && !hitShield)
        {
            transform.position = new Vector2(transform.position.x + sped * Time.deltaTime, transform.position.y);
        }
        if (hitEnemy || hitShield)
        {
            if (!damageDealt)
            {
                if (hitEnemy)
                {
                    //Deal Damage to enemy
                    enemyHealth.value -= power / 100f;
                }
                else if (hitShield)
                {
                    //Deal Damage to shield
                    enemyShield.value -= power / 100f;
                }
                powerDisplay.text = "0";
                damageDealt = true;
            }
            transform.position = new Vector2(transform.position.x - sped * Time.deltaTime, transform.position.y);
            if (Vector2.Distance(transform.position, ogPosit) < 0.5f)
            {
                transform.position = ogPosit;
                attack = false;
                hitEnemy = false;
                hitShield = false;
                damageDealt = false;
            }
        }
    }

    void OnTriggerEnter2D(Collider2D coll)
    {
        // If the Collider2D component is enabled on the collided object
        if (coll.gameObject == enemy)
        {
            hitEnemy = true;
        }
        if(coll.gameObject == shield)
        {
            hitShield = true;
        }
    }
}
