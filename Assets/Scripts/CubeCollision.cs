using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CubeCollision : MonoBehaviour
{
    Cube cube;
    void Start()
    {
        cube = GetComponent<Cube>();
    }
    private void OnCollisionEnter(Collision collision)
    {
        Cube otherCube = collision.gameObject.GetComponent<Cube>();
        // check if contacted with other cube
        if(otherCube!=null && cube.cubeID>otherCube.cubeID)
        {
            //Check if both cubes have same number
            if(cube.cubeNumber==otherCube.cubeNumber)
            {
                Vector3 contactPoint = collision.contacts[0].point;
                //Check if cubes number less than max number in CubeSpawner
                if(otherCube.cubeNumber<CubeSpawner.Instance.maxCubeNumber)
                {
                    // spawn a new cube as a result
                    Cube newCube = CubeSpawner.Instance.Spawn(cube.cubeNumber * 2, contactPoint + Vector3.up * 1.6f);
                    //push the new Cube up and forward
                    float pushForce = 2.5f;
                    newCube.cubeRigidbody.AddForce(new Vector3(0, .3f, 1f) * pushForce,ForceMode.Impulse);
                    //add some torque
                    float randomValue = Random.Range(-20f, 20f);
                    Vector3 randomDirection = Vector3.one * randomValue;
                    newCube.cubeRigidbody.AddTorque(randomDirection);
                }
                // the explosion should affect surrounder cubes too
                Collider[] surrounderCubes = Physics.OverlapSphere(contactPoint, 2f);
                float explosionForce = 400f;
                float explosionRadius = 1.5f;
                foreach (Collider coll in surrounderCubes)
                {
                    if(coll.attachedRigidbody!=null)
                    {
                        coll.attachedRigidbody.AddExplosionForce(explosionForce, contactPoint, explosionRadius);
                    }
                } 

                // To Do: Explosion FX
                Fx.Instance.PlayCubeExplosionFX(contactPoint, cube.cubeColor);

                // Destroy the two cubes
                CubeSpawner.Instance.DestroyCube(cube);
                CubeSpawner.Instance.DestroyCube(otherCube);
            }

        } 
    }


}
