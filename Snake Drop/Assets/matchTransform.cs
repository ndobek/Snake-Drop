using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class matchTransform : MonoBehaviour
{
    public Transform match;
    public bool matchLocationX;
    public bool matchLocationY;
    public bool matchLocationZ;

    // Update is called once per frame
    void LateUpdate()
    {
        Transform t = gameObject.transform;

        float posX = matchLocationX? match.position.x : t.position.x;
        float posY = matchLocationY? match.position.y : t.position.y;
        float posZ = matchLocationZ? match.position.z : t.position.z;

        t.position = new Vector3(posX, posY, posZ);
    }
}
