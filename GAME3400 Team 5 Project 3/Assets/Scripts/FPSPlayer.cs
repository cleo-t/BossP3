using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

[RequireComponent(typeof(CharacterController))]
public class FPSPlayer : MonoBehaviour
{
    public bool crouching
    {
        get
        {
            return this.isCrouching;
        }
        private set
        {
            this.isCrouching = value;
        }
    }

    [SerializeField]
    public float moveSpeed;
    [SerializeField]
    public float crouchSpeedCoeff = 0.125f;
    [SerializeField]
    public float turnSpeed;
    [SerializeField]
    public float crouchHeightCoeff = 0.5f;

    [SerializeField]
    public float range = 100f;

    public GameObject gun;
    public Camera fpsCam;
    public Image crosshair;

    private bool isCrouching;
    private bool hasGun = false;
    private float initialYScale;

    private CharacterController cc;
    

    void Start()
    {
        this.cc = this.GetComponent<CharacterController>();
        this.isCrouching = false;
        this.initialYScale = this.transform.localScale.y;
        /*
        if (gun == null) {
            this.gun = gameObject.transform.GetChild(1).gameObject;
        }
        */
        this.gun.SetActive(false);
        this.crosshair.enabled = false;
        
    }

    private void Update()
    {
        float turnInput = this.GetTurnInput();
        this.Turn(turnInput);
        this.isCrouching = this.GetCrouchInput();

        if (Input.GetButtonDown("Fire1") && hasGun) {
            Shoot();
        }
    }

    private bool GetCrouchInput()
    {
        return Input.GetButton("Crouch");
    }

    void FixedUpdate()
    {
        this.SetCrouching(this.isCrouching);
        Vector3 moveInput = this.GetMoveInput();
        this.Move(moveInput);
    }

    private Vector3 GetMoveInput()
    {
        float horInput = Input.GetAxis("Horizontal");
        float verInput = Input.GetAxis("Vertical");
        Vector3 inputVec = new Vector3(horInput, 0, verInput);
        if (inputVec.magnitude > 1)
        {
            inputVec.Normalize();
        }

        return inputVec;
    }

    private float GetTurnInput()
    {
        return Input.GetAxis("Mouse X");
    }
    
    private void Turn(float turnInput)
    {
        this.transform.rotation *= Quaternion.AngleAxis(turnInput * this.turnSpeed * Time.deltaTime, Vector3.up);
    }
    
    private void Move(Vector3 moveInput)
    {
        Vector3 movement = (this.transform.right * moveInput.x
            + this.transform.forward * moveInput.z) * (this.moveSpeed * (this.isCrouching ? this.crouchSpeedCoeff : 1));
        this.cc.SimpleMove(movement * Time.fixedDeltaTime);
    }

    private void SetCrouching(bool crouching)
    {
        this.cc.enabled = false;
        this.transform.localScale = new Vector3(
            this.transform.localScale.x,
            crouching ? this.initialYScale * this.crouchHeightCoeff : this.initialYScale,
            this.transform.localScale.z);
        this.cc.enabled = true;
    }

    void OnTriggerEnter(Collider other) {
        if (other.transform.name == "FloorShotgun") {
            hasGun = true;
            gun.SetActive(true);
            crosshair.enabled = true;
            Destroy(other.gameObject);
            return;
        }

    }

    private void Shoot() 
    {
        RaycastHit target;
        if (Physics.Raycast(fpsCam.transform.position, fpsCam.transform.forward, out target, range)) {

            SpookyStuff ghost = target.transform.GetComponent<SpookyStuff>();

            if (ghost != null) {
                ghost.TakeDamage();
            }
        }
    }
}
