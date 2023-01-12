using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
/// <summary>
/// I HAVE CHNAGED STUFF
/// </summary>
public class CombatScript : MonoBehaviour
{
    public player2Script player2Script;
    public GameObject playerOneTurn;
    public GameObject playerTwoTurn;
    public OrbSelect swapper;
    //public BallSpawnerScript[] spawners;
    public float waitBeforeContinue;
    //public GameObject playerOneOrbs;
    //public GameObject playerTwoOrbs;
    public Text[] powerDisplays;
    //public Slider playerOneHealth;
    //public Slider playerTwoHealth;
    public bool collecting = false;
    //public GameObject[] toBeDestroyed;
    public List<GameObject> toBeDestroyed = new List<GameObject>();
    List<GameObject> Destroyed = new List<GameObject>();
    public int powerPerOrb;
    public bool justAdded;

    public AssultDrones[] player1Drones;
    public AssultDrones[] player2Drones;

    public bool more = false;
    public float firePower;
    public float earthPower;
    public float waterPower;
    public float lightPower;
    public float darkPower;

    public bool isPlayerOneTurn;
    float value;
    bool attacking = false;


    // Start is called before the first frame update
    void Awake()
    {
        playerOneTurn.SetActive(true);
        isPlayerOneTurn = true;
        InvokeRepeating("DestoryOrbs", 0f, 0.2f);
    }

    // Update is called once per frame
    void Update()
    {
        if (swapper.swappedDone)
        {
            StartCoroutine(gameMaster());
        }
        displayValues();
        if (!collecting)
        {
            firePower = 0;
            earthPower = 0;
            waterPower = 0;
            lightPower = 0;
            darkPower = 0;
        }
    }

    void displayValues()
    {
        if (isPlayerOneTurn)
        {
            powerDisplays[0].text = firePower.ToString();
            powerDisplays[2].text = waterPower.ToString();
            powerDisplays[4].text = earthPower.ToString();
            powerDisplays[6].text = lightPower.ToString();
            powerDisplays[8].text = darkPower.ToString();
        }
        else
        {
            powerDisplays[1].text = firePower.ToString();
            powerDisplays[3].text = waterPower.ToString();
            powerDisplays[5].text = earthPower.ToString();
            powerDisplays[7].text = lightPower.ToString();
            powerDisplays[9].text = darkPower.ToString();
        }
    }
    IEnumerator gameMaster()
    {
        swapper.swappedDone = false;
        attacking = false;
        /*
        //Set Values to zero here
        firePower = 0;
        earthPower = 0;
        waterPower = 0;
        lightPower = 0;
        darkPower = 0;

        playerOneOrbs.SetActive(false);
        playerTwoOrbs.SetActive(false);
        Disable Spawners
        for (int i = 0; i < spawners.Length; i++)
        {
            spawners[i].enabled = false;
        }
        if (isPlayerOneTurn)
        {
            playerOneOrbs.SetActive(true);
        }
        else
        {
            playerTwoOrbs.SetActive(true);
        }
        */

        collecting = true;
        more = true;
        while (more)
        {
            more = false;
            yield return new WaitForSeconds(1.5f);
        }

        //Collect values here and deal damage, reset values turn off display
        if (isPlayerOneTurn)
        {
            if (firePower > 0)
            {
                player1Drones[0].attack = true;
                attacking = true;
            }
            if (waterPower > 0)
            {
                player1Drones[1].attack = true;
                attacking = true;
            }
            if (earthPower > 0)
            {
                player1Drones[2].attack = true;
                attacking = true;
            }
            if (lightPower > 0)
            {
                player1Drones[3].attack = true;
                attacking = true;
            }
            if (darkPower > 0)
            {
                player1Drones[4].attack = true;
                attacking = true;
            }
        }
        else
        {
            if (firePower > 0)
            {
                player2Drones[0].attack = true;
                attacking = true;
            }
            if (waterPower > 0)
            {
                player2Drones[1].attack = true;
                attacking = true;
            }
            if (earthPower > 0)
            {
                player2Drones[2].attack = true;
                attacking = true;
            }
            if (lightPower > 0)
            {
                player2Drones[3].attack = true;
                attacking = true;
            }
            if (darkPower > 0)
            {
                player2Drones[4].attack = true;
                attacking = true;
            }
        }

        if (attacking)
        {
            yield return new WaitForSeconds(3f);
        }

        //Set Values to zero here
        firePower = 0;
        earthPower = 0;
        waterPower = 0;
        lightPower = 0;
        darkPower = 0;

        if (!isPlayerOneTurn)
        {
            playerTwoTurn.SetActive(false);
            playerOneTurn.SetActive(true);
            isPlayerOneTurn = true;
            player2Script.isMyTurnMaster = false;
            player2Script.firstTime = true;
        }
        else
        {
            playerTwoTurn.SetActive(true);
            playerOneTurn.SetActive(false);
            isPlayerOneTurn = false;
            player2Script.isMyTurnMaster = true;
        }
        collecting = false;

        powerDisplays[0].text = firePower.ToString();
        powerDisplays[2].text = waterPower.ToString();
        powerDisplays[4].text = earthPower.ToString();
        powerDisplays[6].text = lightPower.ToString();
        powerDisplays[8].text = darkPower.ToString();
        powerDisplays[1].text = firePower.ToString();
        powerDisplays[3].text = waterPower.ToString();
        powerDisplays[5].text = earthPower.ToString();
        powerDisplays[7].text = lightPower.ToString();
        powerDisplays[9].text = darkPower.ToString();

        /*
        playerOneOrbs.SetActive(false);
        playerTwoOrbs.SetActive(false);
        for (int i = 0; i < spawners.Length; i++)
        {
            spawners[i].enabled = true;
        }
        */
    }

    void DestoryOrbs()
    {
        if (!justAdded)
        {
            if (!swapper.prepped || swapper.swappedDone)
            {
                foreach (var orbb in toBeDestroyed)
                {
                    if (!Destroyed.Contains(orbb))
                    {
                        if (orbb.name == "Water Orb(Clone)") { waterPower += powerPerOrb; }
                        else if (orbb.name == "Fire Orb(Clone)") { firePower += powerPerOrb; }
                        else if (orbb.name == "Earth Orb(Clone)") { earthPower += powerPerOrb; }
                        else if (orbb.name == "Light Orb(Clone)") { lightPower += powerPerOrb; }
                        else if (orbb.name == "Dark Orb(Clone)") { darkPower += powerPerOrb; }
                        //Debug.Log("destroyed");
                        Destroyed.Add(orbb);
                    }
                    Destroy(orbb);
                    //displayValues();
                }
                Destroyed.Clear();
                toBeDestroyed.Clear();
            }
        }
        else
        {
            justAdded = false;
        }
    }
}
