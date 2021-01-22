using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteInEditMode]
public class RoundTransformToPPU : MonoBehaviour
{
    public Transform LinkedTransform;
    public float PPU = 20;
    [Range(-.9f, .9f)]
    public float XOffset = 0;
    [Range(-.9f, .9f)]
    public float YOffset = 0;
    [Range(-.9f, .9f)]
    public float ZOffset = 0;

    private void LateUpdate()
    {
        RoundPosition();
    }

    private void OnEnable()
    {
        if (LinkedTransform == null) LinkedTransform = gameObject.transform;
    }

    private void RoundPosition()
    {
        Vector3 pos = LinkedTransform.position;
        float pixelSize = 1 / PPU;

        float xAdj = (pos.x % pixelSize) - (1 / PPU) * XOffset;
        float yAdj = (pos.y % pixelSize) - (1 / PPU) * YOffset;
        float zAdj = (pos.z % pixelSize) - (1 / PPU) * ZOffset;

        LinkedTransform.position = new Vector3(pos.x - xAdj, pos.y - yAdj, pos.z - zAdj);
    }
}
