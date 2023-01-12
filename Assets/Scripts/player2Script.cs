using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class player2Script : MonoBehaviour
{
    public OrbSelect swapper;

    public bool isMyTurnSelect = false;
    public bool isMyTurnMaster = false;
    public GameObject first;

    public bool firstTime = true;

    RaycastHit2D[] column;
    GameObject[] initalColumn = new GameObject[5];
    GameObject[][] columnArray = new GameObject[7][];

    GameObject[] initalRow = new GameObject[7];
    GameObject[][] rowArray = new GameObject[5][];

    GameObject[][] chosenArray;

    GameObject selectedFirstOrb;
    GameObject selectedSecondOrb;
    bool firstOrbChosen = false;
    bool secondOrbChosen = false;
    int chosenColumn;
    int[] chosenIndexs = new int[3];


    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        if (isMyTurnSelect && isMyTurnMaster && firstTime)
        {
            StartCoroutine(playTurn());
        }
    }

    IEnumerator playTurn()
    {
        firstTime = false;
        yield return new WaitForSeconds(2f);
        if (Random.value > 0.5)
        {
            makeColumnSelection();
        }
        else
        {
            makeRowSelection();
        }
        yield return new WaitForSeconds(2f);
        swapper.firstOrb = selectedFirstOrb;
        swapper.secondOrb2P = selectedSecondOrb;
        swapper.beginSawp = true;
    }

    GameObject[][] createColumnArrays()
    {
        for (int i = 0; i < 7; i++)
        {
            column = (Physics2D.LinecastAll(new Vector2(-4.5f + 1.5f * i, 6f), new Vector2(-4.5f + 1.5f * i, -0.5f)));
            System.Array.Sort(column, (x, y) => x.distance.CompareTo(y.distance));

            //Debug.DrawLine(new Vector2(-4.5f + 1.5f * i, 6f), new Vector2(-4.5f + 1.5f * i, -0.5f), Color.green, 10f);

            for (int j = 0; j < column.Length; j++)
            {
                initalColumn[j] = column[j].collider.gameObject;
            }
            columnArray[i] = (GameObject[])initalColumn.Clone();
        }
        return columnArray;
    }

    GameObject[][] createRowArrays()
    {
        createColumnArrays();

        for (int i = 0; i < 5; i++)
        {
            for (int j = 0; j < 7; j++)
            {
                initalRow[j] = columnArray[j][i];
            }
            rowArray[i] = (GameObject[])initalRow.Clone();
        }
        return rowArray;
    }

    void makeColumnSelection()
    {
        chosenArray = createColumnArrays();
        for (int i = 0; i < 7; i++)//Go through each column
        {
            for (int j = 2; j < 5; j++)//Start at third index and looking for similar gap
            {
                if (chosenArray[i][j].name == chosenArray[i][j - 2].name)
                {
                    selectedFirstOrb = chosenArray[i][j-1];
                    first.transform.position = selectedFirstOrb.transform.position;
                    firstOrbChosen = true;
                    chosenColumn = i;
                    chosenIndexs[0] = j;
                    chosenIndexs[1] = j - 1;
                    chosenIndexs[2] = j - 2;
                    break;
                }
            }
            if (firstOrbChosen)
            {
                break;
            }
        }

        if (!firstOrbChosen)
        {
            makeRowSelection();
            return;
        }
        else
        {
            for (int i = 0; i < 7; i++)//Go through each column
            {
                for (int j = 2; j < 5; j++)//Start at third index and looking for similar gap
                {
                    if(i == chosenColumn && chosenIndexs.Contains(j))
                    {
                        continue;
                    }

                    if (chosenArray[chosenColumn][chosenIndexs[0]].name == chosenArray[i][j].name)
                    {
                        selectedSecondOrb = chosenArray[i][j];
                        secondOrbChosen = true;
                        break;
                    }
                }
                if (secondOrbChosen)
                {
                    break;
                }
            }
            if (!secondOrbChosen)
            {
                makeRowSelection();
                return;
            }
        }
        firstOrbChosen = false;
        secondOrbChosen = false;
    }

    void makeRowSelection()
    {
        chosenArray = createRowArrays();
        for (int i = 0; i < 5; i++)//Go through each row
        {
            for (int j = 2; j < 7; j++)//Start at third index and looking for similar gap
            {
                if (chosenArray[i][j].name == chosenArray[i][j - 2].name)
                {
                    selectedFirstOrb = chosenArray[i][j - 1];
                    first.transform.position = selectedFirstOrb.transform.position;
                    firstOrbChosen = true;
                    chosenColumn = i;
                    chosenIndexs[0] = j;
                    chosenIndexs[1] = j - 1;
                    chosenIndexs[2] = j - 2;
                    break;
                }
            }
            if (firstOrbChosen)
            {
                break;
            }
        }

        if (!firstOrbChosen)
        {
            makeColumnSelection();
            return;
        }
        else
        {
            for (int i = 0; i < 5; i++)//Go through each column
            {
                for (int j = 2; j < 7; j++)//Start at third index and looking for similar gap
                {
                    if (i == chosenColumn && chosenIndexs.Contains(j))
                    {
                        continue;
                    }

                    if (chosenArray[chosenColumn][chosenIndexs[0]].name == chosenArray[i][j].name)
                    {
                        selectedSecondOrb = chosenArray[i][j];
                        secondOrbChosen = true;
                        break;
                    }
                }
                if (secondOrbChosen)
                {
                    break;
                }
            }
            if (!secondOrbChosen)
            {
                makeColumnSelection();
                return;
            }
        }
        firstOrbChosen = false;
        secondOrbChosen = false;
    }
}