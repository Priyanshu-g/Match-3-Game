using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GreenMatchers : MonoBehaviour
{
    RaycastHit2D[] multiHitInfoDown;
    RaycastHit2D[] multiHitInfoUp;
    RaycastHit2D[] multiHitInfoLeft;
    RaycastHit2D[] multiHitInfoRight;
    //RaycastHit2D[] downRay;
    public CombatScript powerLevels;
    public Text valueSet;
    public Text valueSet1;
    //Vector2 curPos;
    //Vector2 lastPos;
    bool justAddedLocal;
    //int notMovingFor = 0;

    // Start is called before the first frame update
    void Awake()
    {
    }


    void FixedUpdate()
    {
        /*
        curPos = transform.position;
        if (Vector2.Distance(curPos, lastPos) < 0.2f)
        {
            notMovingFor += 1;
        }
        else
        {
            notMovingFor = 0;
        }
        lastPos = curPos;
        */
        if (this.enabled && !justAddedLocal)
        {
            Match();
        }
    }

    void Match()
    {
        multiHitInfoDown = (Physics2D.RaycastAll(transform.position, -Vector2.up, Mathf.Infinity));
        System.Array.Sort(multiHitInfoDown, (x, y) => x.distance.CompareTo(y.distance));

        multiHitInfoUp = (Physics2D.RaycastAll(transform.position, Vector2.up, Mathf.Infinity));
        System.Array.Sort(multiHitInfoUp, (x, y) => x.distance.CompareTo(y.distance));

        multiHitInfoLeft = (Physics2D.RaycastAll(transform.position, Vector2.left, Mathf.Infinity));
        System.Array.Sort(multiHitInfoLeft, (x, y) => x.distance.CompareTo(y.distance));

        multiHitInfoRight = (Physics2D.RaycastAll(transform.position, -Vector2.left, Mathf.Infinity));
        System.Array.Sort(multiHitInfoRight, (x, y) => x.distance.CompareTo(y.distance));

        if (multiHitInfoDown.Length != 0 && multiHitInfoUp.Length != 0)
        {
            if (multiHitInfoDown[0].distance < 1f && multiHitInfoUp[0].distance < 1f)
            {
                if (multiHitInfoDown[0].collider.gameObject.name == name && multiHitInfoUp[0].collider.gameObject.name == name)
                {
                    powerLevels.more = true;
                    for (int i = 0; i < multiHitInfoDown.Length; i++)
                    {
                        if (multiHitInfoDown[i].collider.gameObject.name == name)
                        {
                            powerLevels.toBeDestroyed.Add(multiHitInfoDown[i].collider.gameObject);
                            powerLevels.justAdded = true;
                            justAddedLocal = true;
                            //powerLevels.toBeDestroyed[powerLevels.toBeDestroyed.Length] = multiHitInfoDown[i].collider.gameObject;
                        }
                        else
                        {
                            break;
                        }
                    }
                    for (int i = 0; i < multiHitInfoUp.Length; i++)
                    {
                        if (multiHitInfoUp[i].collider.gameObject.name == name)
                        {
                            powerLevels.toBeDestroyed.Add(multiHitInfoUp[i].collider.gameObject);
                            powerLevels.justAdded = true;
                            justAddedLocal = true;
                            //powerLevels.toBeDestroyed[powerLevels.toBeDestroyed.Length] = multiHitInfoUp[i].collider.gameObject;
                        }
                        else
                        {
                            break;
                        }
                    }
                    powerLevels.toBeDestroyed.Add(gameObject);
                    powerLevels.justAdded = true;
                    justAddedLocal = true;
                    //powerLevels.toBeDestroyed[powerLevels.toBeDestroyed.Length] = gameObject;
                }
            }
        }
        if (multiHitInfoLeft.Length != 0 && multiHitInfoRight.Length != 0)
        {
            if (multiHitInfoLeft[0].distance < 1f && multiHitInfoRight[0].distance < 1f)
            {
                if (multiHitInfoLeft[0].collider.gameObject.name == name && multiHitInfoRight[0].collider.gameObject.name == name)
                {
                    powerLevels.more = true;
                    for (int i = 0; i < multiHitInfoLeft.Length; i++)
                    {
                        if (multiHitInfoLeft[i].collider.gameObject.name == name)
                        {
                            powerLevels.toBeDestroyed.Add(multiHitInfoLeft[i].collider.gameObject);
                            powerLevels.justAdded = true;
                            justAddedLocal = true;
                            //powerLevels.toBeDestroyed[powerLevels.toBeDestroyed.Length] = multiHitInfoLeft[i].collider.gameObject;
                        }
                        else
                        {
                            break;
                        }
                    }
                    for (int i = 0; i < multiHitInfoRight.Length; i++)
                    {
                        if (multiHitInfoRight[i].collider.gameObject.name == name)
                        {
                            powerLevels.toBeDestroyed.Add(multiHitInfoRight[i].collider.gameObject);
                            powerLevels.justAdded = true;
                            justAddedLocal = true;
                            //powerLevels.toBeDestroyed[powerLevels.toBeDestroyed.Length] = multiHitInfoRight[i].collider.gameObject;
                        }
                        else
                        {
                            break;
                        }
                    }
                    powerLevels.toBeDestroyed.Add(gameObject);
                    powerLevels.justAdded = true;
                    justAddedLocal = true;
                    //powerLevels.toBeDestroyed[powerLevels.toBeDestroyed.Length] = gameObject;
                }
            }
        }
    }
}
