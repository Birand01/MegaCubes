using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] private float moveSpeed;
    [SerializeField] private float pushForce;
    [SerializeField] private float cubeMaxPosX;
    [Space]
    [SerializeField] private TouchSlider touchSlider;
    private Cube mainCube;
    private bool canMove;
    private bool isPointerDown;
    private Vector3 cubePos;
    void Start()
    {
        SpawnCube();
        canMove = true;
        //Listen to slider events
        touchSlider.OnPointerDownEvent += OnPointerDown;
        touchSlider.OnPointerDragEvent += OnPointerDrag;
        touchSlider.OnPointerUpEvent += OnPointerUp;

    }
   

    private void SpawnCube()
    {
        mainCube = CubeSpawner.Instance.SpawnRandom();
        mainCube.isMainCube = true;

        //reset Cubepos variable
        cubePos = mainCube.transform.position;
    }
    void Update()
    {
        if(isPointerDown)
        {
            mainCube.transform.position = Vector3.Lerp(mainCube.transform.position, cubePos, moveSpeed * Time.deltaTime);
        }
    }
    private void OnPointerDown()
    {
        isPointerDown = true;
    }
    private void OnPointerDrag(float xMovement)
    {
        if(isPointerDown)
        {
            cubePos =mainCube.transform.position;
            cubePos.x = xMovement * cubeMaxPosX;
        }
    }
    private void OnPointerUp()
    {
        if (isPointerDown && canMove)
        {
            isPointerDown = false;
            canMove = false;
            // Push The cube
            mainCube.cubeRigidbody.AddForce(Vector3.forward * pushForce, ForceMode.Impulse);
            //Spawn a newCube after .3 seconds
            Invoke("SpawnNewCube", .3f);
        }
       

       
    }
    private void SpawnNewCube()
    {
        mainCube.isMainCube = false;
        canMove = true;
        SpawnCube();
    }
    private void OnDestroy()
    {
        //Remove Listeners
        touchSlider.OnPointerDownEvent -= OnPointerDown;
        touchSlider.OnPointerDragEvent -= OnPointerDrag;
        touchSlider.OnPointerUpEvent -= OnPointerUp;
    }
}
