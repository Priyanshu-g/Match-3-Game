using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallSpawnerScript : MonoBehaviour
{

    public Rigidbody2D[] Orbs;
    public BoxCollider2D board;
    public Transform orbsParent;
    //public SpawnerMaster master;
    public int leftIndex;
    public CombatScript comScript;
    RaycastHit2D[] rayDown;


    public int mostRecent = -1;
    public bool spawnCom = false;
    int xcount;
    Vector2 positionss;
    // Start is called before the first frame update
    void Awake()
    {
        InvokeRepeating("SpawnOpenSpace", 0.1f, 0.4f);
    }

    // Update is called once per frame
    void Update()
    {
    }

    void SpawnOpenSpace()
    {
        if (this.enabled)
        {
            positionss = new Vector2(transform.position.x, transform.position.y+1);
            rayDown = (Physics2D.RaycastAll(positionss, -Vector2.up, 8f));
            //Debug.DrawRay(positionss, -Vector2.up * 8f, Color.green, 10f);

            if (rayDown.Length<5)
            {
                xcount = Random.Range(0, Orbs.Length);
                while (xcount == mostRecent)
                {
                    xcount = Random.Range(0, Orbs.Length);
                }
                Rigidbody2D clone;
                clone = Instantiate(Orbs[xcount], transform.position, transform.rotation, orbsParent);
                spawnCom = true;
                mostRecent = xcount;
                comScript.more = true;
            }
        }
    }
}
