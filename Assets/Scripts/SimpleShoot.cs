using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR;
using UnityEngine.XR.Interaction.Toolkit;

public class SimpleShoot : MonoBehaviour
{

    public GameObject bulletPrefab;
    public GameObject castinPrefab;
    public GameObject muzzleFlashPrefab;
    public Transform barrelLocation;
    public Transform casingExitLocation;

    public float shotPower = 100f;

    public bool isGrab = false;

    public AudioClip fireClip;
    AudioSource fireAudio;

    public HandState currentGrab;
    // Start is called before the first frame update
    void Start()
    {
        if(barrelLocation == null)
        {
            barrelLocation = transform;
        }

        fireAudio = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        //if (Input.GetButtonDown("Fire"))
        //{
        //    GetComponent<Animator>().SetTrigger("Fire");
        //}
    }

    public void grabGun()
    {
        isGrab = true;
    }

    public void dropGun()
    {
        isGrab = false;
    }

    public void Shoot()
    {
        if (isGrab == true)
        {
            GameObject tempFlash;
            Instantiate(bulletPrefab, barrelLocation.position, barrelLocation.rotation).GetComponent<Rigidbody>().AddForce(barrelLocation.forward * shotPower);
            tempFlash = Instantiate(muzzleFlashPrefab, barrelLocation.position, barrelLocation.rotation);

            fireAudio.PlayOneShot(fireClip);
        }
    }
    
    public void SetGraspState(HandState state)
    {
        currentGrab = state;
    }

    public void SetGraspNONE()
    {
        if (!GetComponent<XRGrabInteractable>().isSelected)
        {
            currentGrab = HandState.NONE;
        }
    }

    public void SetGraspLEFT()
    {
        currentGrab = HandState.LEFT;
    }

    public void SetGraspRIGHT()
    {
        currentGrab = HandState.RIGHT;
    }
}
