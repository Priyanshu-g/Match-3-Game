using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class OrbSelect : MonoBehaviour
{
    public GameObject first;
    public GameObject[] Orbs;
    public Transform orbsParents;
    public GameObject spawners;
    public float sped;
    public CombatScript comMaster;
    public player2Script player2Script;
    public bool swappedDone = false;

    [HideInInspector]
    public GameObject firstOrb;
    [HideInInspector]
    public GameObject secondOrb2P;
    bool firstBool = false;
    Vector2 firstPosi;
    Vector2 secondPosi;
    Vector2 secondCurPosi;
    Vector2 ogPosi;
    RaycastHit2D hit;
    string[] orbsNames;
    int children;
    public bool beginSawp = false;
    public bool prepped = false;
    public SpawnerMaster masterYOGA;
    float ogGrav;

    // Start is called before the first frame update
    void Awake()
    {
        ogGrav = Orbs[0].GetComponent<Rigidbody2D>().gravityScale;
        orbsNames = new string[Orbs.Length];
        ogPosi = first.transform.position;
        for (int i = 0; i<Orbs.Length; i++)
        {
            orbsNames[i] = Orbs[i].name + "(Clone)";
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (!beginSawp)
        {
            //UnFreeze All Orbs (Keeping x-axis freeze)
            for (int i = 0; i < children; ++i)
            {
                children = orbsParents.childCount;
                orbsParents.GetChild(i).gameObject.GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.None;
                orbsParents.GetChild(i).gameObject.GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.FreezePositionX;
                orbsParents.GetChild(i).gameObject.GetComponent<GreenMatchers>().enabled = true;
            }
        }
        if (beginSawp)
        {
            if (!prepped)
            {
                prep();
            }

            //If human turn
            if (comMaster.isPlayerOneTurn)
            {
                firstOrb.transform.position = Vector3.MoveTowards(firstOrb.transform.position, secondPosi, sped * Time.deltaTime);
                hit.collider.gameObject.transform.position = Vector3.MoveTowards(hit.collider.gameObject.transform.position, firstPosi, sped * Time.deltaTime);
                secondCurPosi = hit.collider.gameObject.transform.position;
            }
            else //If bot turn
            {
                firstOrb.transform.position = Vector3.MoveTowards(firstOrb.transform.position, secondPosi, sped * Time.deltaTime);
                secondOrb2P.transform.position = Vector3.MoveTowards(secondOrb2P.transform.position, firstPosi, sped * Time.deltaTime);
                secondCurPosi = secondOrb2P.transform.position;
            }

            if (Vector2.Distance(secondCurPosi, firstPosi) < 0.1f)
            {
                completeSwap();
            }
        }
        else if (Input.GetMouseButtonDown(0) && masterYOGA.spawned[6] == true && !comMaster.collecting && comMaster.isPlayerOneTurn)
        {
            player2Script.isMyTurnSelect = false;
            hit = Physics2D.GetRayIntersection(Camera.main.ScreenPointToRay(Input.mousePosition));
            if (hit)
            {
                if (orbsNames.Contains(hit.collider.gameObject.name))
                {
                    if (!firstBool)
                    {
                        firstOrb = hit.collider.gameObject;
                        first.transform.position = hit.collider.gameObject.transform.position;
                        firstBool = true;
                    }
                    else if (hit.collider.gameObject.transform.position == firstOrb.transform.position)
                    {
                        firstBool = false;
                        first.transform.position = ogPosi;
                    }
                    else if (firstBool)
                    {
                        beginSawp = true;
                    }
                }
            }
        }
        else if (!comMaster.isPlayerOneTurn)
        {
            player2Script.isMyTurnSelect = true;
        }

    }

    void prep()
    {
        //Freeze all orbs
        children = orbsParents.childCount;
        for (int i = 0; i < children; ++i)
        {
            orbsParents.GetChild(i).gameObject.GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.FreezePositionY | RigidbodyConstraints2D.FreezePositionX;
            orbsParents.GetChild(i).gameObject.GetComponent<GreenMatchers>().enabled = false;
        }

        //If human turn
        if (comMaster.isPlayerOneTurn)
        {
            //Prepare For Swap
            firstPosi = firstOrb.transform.position;
            secondPosi = hit.collider.gameObject.transform.position;
            secondCurPosi = hit.collider.gameObject.transform.position;

            //Remove Freeze and collider from swap orbs
            firstOrb.GetComponent<Rigidbody2D>().gravityScale = 0;
            hit.collider.gameObject.GetComponent<Rigidbody2D>().gravityScale = 0;

            firstOrb.GetComponent<BoxCollider2D>().enabled = false;
            hit.collider.gameObject.GetComponent<BoxCollider2D>().enabled = false;
        }
        else //If computer then second orb gets passed
        {
            //Prepare For Swap
            firstPosi = firstOrb.transform.position;
            secondPosi = secondOrb2P.transform.position;
            secondCurPosi = secondOrb2P.transform.position;

            //Remove Freeze and collider from swap orbs
            firstOrb.GetComponent<Rigidbody2D>().gravityScale = 0;
            secondOrb2P.GetComponent<Rigidbody2D>().gravityScale = 0;

            firstOrb.GetComponent<BoxCollider2D>().enabled = false;
            secondOrb2P.GetComponent<BoxCollider2D>().enabled = false;
        }

        spawners.SetActive(false);

        prepped = true;
    }

    void completeSwap()
    {
        //If human turn
        if (comMaster.isPlayerOneTurn)
        {
            //Complete Swap
            hit.collider.gameObject.transform.position = firstPosi;
            firstOrb.transform.position = secondPosi;

            firstOrb.GetComponent<Rigidbody2D>().gravityScale = ogGrav;
            hit.collider.gameObject.GetComponent<Rigidbody2D>().gravityScale = ogGrav;

            firstOrb.GetComponent<BoxCollider2D>().enabled = true;
            hit.collider.gameObject.GetComponent<BoxCollider2D>().enabled = true;
        }
        else
        {
            //Complete Swap
            secondOrb2P.transform.position = firstPosi;
            firstOrb.transform.position = secondPosi;

            firstOrb.GetComponent<Rigidbody2D>().gravityScale = ogGrav;
            secondOrb2P.GetComponent<Rigidbody2D>().gravityScale = ogGrav;

            firstOrb.GetComponent<BoxCollider2D>().enabled = true;
            secondOrb2P.GetComponent<BoxCollider2D>().enabled = true;
        }

        spawners.SetActive(true);

        //UnFreeze All Orbs (Keeping x-axis freeze)
        for (int i = 0; i < children; ++i)
        {
            orbsParents.GetChild(i).gameObject.GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.None;
            orbsParents.GetChild(i).gameObject.GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.FreezePositionX;
            orbsParents.GetChild(i).gameObject.GetComponent<GreenMatchers>().enabled = true;
        }
        first.transform.position = ogPosi;
        firstBool = false;
        beginSawp = false;
        prepped = false;

        swappedDone = true;
    }
}
