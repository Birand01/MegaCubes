using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fx : MonoBehaviour
{
    [SerializeField] private ParticleSystem cubeExplosionFX;
    ParticleSystem.MainModule cubeExplosionFXmainModule;
    // Singleton Class
    public static Fx Instance;
    private void Awake()
    {
        if(Instance==null)
        {
            Instance = this;
        }
    }
    void Start()
    {
        cubeExplosionFXmainModule = cubeExplosionFX.main;
    }

    public void PlayCubeExplosionFX(Vector3 position,Color color)
    {
        cubeExplosionFXmainModule.startColor = new ParticleSystem.MinMaxGradient(color);
        cubeExplosionFX.transform.position = position;
        cubeExplosionFX.Play();
    }

   
}
