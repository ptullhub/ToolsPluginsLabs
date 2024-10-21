using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public bool canBeUsed;
    public bool isInUse;
    // Update is called once per frame
    void Update()
    {
        if (isInUse)
        {
            transform.Translate(Vector3.up * Time.deltaTime * 8f);
        }
        
        if (transform.position.y > 6f)
        {
            canBeUsed = true;
            isInUse = false;
        }
    }
}
