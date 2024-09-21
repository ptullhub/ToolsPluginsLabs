using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine;


public class ParticleManager : MonoBehaviour
{
    public static ParticleManager instance;

    [SerializeField] private ParticleSystem explosion;
    // Start is called before the first frame update
    void Start()
    {
        if (instance == null)
        {
            instance = this;
        }
    }

    public void Explode(Transform position)
    {
        ParticleSystem explosionObject = Instantiate(explosion, position.position, Quaternion.identity);
        explosionObject.Play();
    }
}
