using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Netcode;
using UnityEngine.InputSystem;

public class PlayerController : NetworkBehaviour
{
    [SerializeField]
    private Transform playerTransform;
    [SerializeField] 
    private Camera cam;

    [SerializeField] 
    private InputAction movement = new InputAction();

    [SerializeField] 
    private LayerMask layerMaskWalkable = new LayerMask();

    [SerializeField] 
    private Vector3 movePosition;

    private Vector3 spawnPosition = new Vector3(0.8142774f, 1f, -7.049958f);

    [SerializeField] private float speed; // character

    private float rotationSpeed = 10f;

    [SerializeField] 
    private Rigidbody rb;

    [SerializeField]
    private Character character;

    [SerializeField] 
    private NetworkVariable<Vector3> networkTransformPosition = new NetworkVariable<Vector3>();


    private void Start(){
         
        playerTransform = GetComponent<Transform>();
        cam = GetComponentInChildren<Camera>();
        rb = GetComponent<Rigidbody>();
        character = GetComponent<Character>();
        speed = character.Speed.Value;
        transform.position = spawnPosition;
                
        
    }

    public override void OnNetworkSpawn()
    {
        if(IsServer){
            playerTransform = GetComponent<Transform>();
            cam = GetComponentInChildren<Camera>();
            rb = GetComponent<Rigidbody>();
            character = GetComponent<Character>();
            speed = character.Speed.Value;
            transform.position = spawnPosition;
        }
    }

    public void MoveCharacter(){

        if(Input.GetKey(KeyCode.Mouse0)){

            Ray ray = cam.ScreenPointToRay(Mouse.current.position.ReadValue());

            RaycastHit hit;

            //Debug.Log("Raycast hit.");

            if(Physics.Raycast(ray, out hit, 1000, layerMaskWalkable)){
                
                //Debug.Log("Moving character to: " + hit.point);

                movePosition = hit.point;

                // [1] https://www.youtube.com/watch?v=zZDiC0aOXDY&t=1239s
                Vector3 destination = Vector3.MoveTowards(playerTransform.position, movePosition, character.Speed.Value * Time.deltaTime);
                Vector3 direction = (movePosition - playerTransform.position).normalized;
                
                if(Vector3.Distance(transform.position, hit.point) > 0.2f){
                    rb.velocity = direction * character.Speed.Value;
                    transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(direction), rotationSpeed * Time.deltaTime);
                
                }
                
            }
        }
        else{
            rb.velocity = Vector3.zero;
        }

    }

    private void Update(){

        if(IsOwner && IsLocalPlayer)
        {
            MoveCharacter();
        }
        
    }
    private void OnEnable(){
        movement.Enable();
    }

    private void OnDisable(){
        movement.Disable();
    }     
    
}