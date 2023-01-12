using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class shieldManager : MonoBehaviour
{
    public Slider self;
    public BoxCollider2D selfCol;
    public bool isOpp;
    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        if (self.value < 0)
        {
            Destroy(gameObject);
        }
        else
        {
            selfCol.offset = new Vector2(self.value, 0f);
            if (isOpp)
            {
                selfCol.offset = new Vector2(-self.value, 0f);
            }
            selfCol.size = new Vector2(self.value*2, 0.5f);
        }
    }
}
