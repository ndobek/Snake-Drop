using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MagnetizeParticles : MonoBehaviour
{
    public GameObject magneticSystemObj;
    private IMagnetic magnetizer;
    private ParticleSystem magneticParticleSystem;
    private ParticleSystem.Particle[] magneticParticles;
    public Vector3 CenterOfMagnetization { get { return gameObject.transform.position; } }
    private Vector3 magnetRange;
    public Vector3 MagnetRange { get => magnetRange; set { magnetRange = value; } }
    private void Start()
    {
        magnetizer = magneticSystemObj.GetComponent<IMagnetic>();
        magneticParticleSystem = magneticSystemObj.GetComponent<ParticleSystem>();

    }
    Vector3 GetPath(Vector3 currentPos)
    {
        Vector3 vector = currentPos - CenterOfMagnetization;
        return vector;
    }
    bool CheckRange(Vector3 currentPos)
    {
        if (GetPath(currentPos).magnitude > MagnetRange.magnitude)
        {
            return true;
        }
        else { return false; }
    }
    Vector3 GetDirection(Vector3 currentPos)
    {
        Vector3 result = GetPath(currentPos).normalized;
        return result;
    }
    private void Update()
    {
        magneticParticleSystem.GetParticles(magneticParticles);
        foreach (ParticleSystem.Particle particle in magneticParticles)
        {
            if (CheckRange(particle.position))
            {
                GetDirection(particle.position);
                
            }
        }
    }
}
