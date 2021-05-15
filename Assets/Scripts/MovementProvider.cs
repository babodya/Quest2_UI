using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR;
using UnityEngine.XR.Interaction.Toolkit;

public class MovementProvider : MonoBehaviour
{
    public float speed = 1.0f;
    public float gravityMultiplier = 1.0f;
    public List<XRController> controllers = null;
    private CharacterController characterController = null;
    private GameObject head = null;

    bool isStarted;

    private void Awake()
    {
        characterController = GetComponent<CharacterController>();
        head = GetComponent<XRRig>().cameraGameObject;
    }
    // Start is called before the first frame update
    void Start()
    {
        PositionController();
    }

    // Update is called once per frame
    void Update()
    {
        PositionController();
        //CheckForInput();
        Move();
        //ApplyGravity();
    }

    void PositionController()
    {
        float headHeight = Mathf.Clamp(head.transform.localPosition.y, 1, 2);
        characterController.height = headHeight;

        Vector4 newCenter = Vector3.zero;
        newCenter.x = head.transform.localPosition.x;
        newCenter.z = head.transform.localPosition.z;

        characterController.center = newCenter;
    }

    void CheckForInput()
    {
        foreach(XRController controller in controllers)
        {
            if (controller.enableInputActions)
            {
                CheckForMovement(controller.inputDevice);
            }
        }
    }

    void CheckForMovement(InputDevice device)
    {
        if(device.TryGetFeatureValue(CommonUsages.primary2DAxis, out Vector2 position))
        {
            StartMove(position);
        }
    }

    void StartMove(Vector2 position)
    {
        Vector3 direction = new Vector3(position.x, 0, position.y);
        Vector3 headRotation = new Vector3(0, head.transform.eulerAngles.y, 0);

        direction = Quaternion.Euler(headRotation) * direction;

        Vector3 movement = direction * speed;
        characterController.Move(movement * Time.deltaTime);
    }

    void ApplyGravity()
    {
        Vector3 gravity = new Vector3(0, Physics.gravity.y * gravityMultiplier, 0);
        gravity.y *= Time.deltaTime;

        characterController.Move(gravity * Time.deltaTime);
    }

    public void StartMove()
    {
        isStarted = true;
    }

    private void Move()
    {
        if(!isStarted)
        {
            return;
        }
        Vector3 direction = new Vector3(0, 0, 1.0f);
        Vector3 movement = direction * speed;
        characterController.Move(movement * Time.deltaTime);
    }
}
