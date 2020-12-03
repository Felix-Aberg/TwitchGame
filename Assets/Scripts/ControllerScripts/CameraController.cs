using System.Collections;
using System.Collections.Generic;
using System.Net.Sockets;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    public float moveSpeed;
    public float boostSpeedModifier;
    public float mouseSensitivityX;
    public float mouseSensitivityY;
    public float mouseClampY;

    public float rotateSpeed;
    public float turnSpeed;
    public float AFKThreshold;

    float lastInput;
    bool isAFK;
    int useAFKCamera = 2;

    Vector3 velocity; //This is local to the players rotation
    Vector3 eulerRotation;

    // Start is called before the first frame update
    void Start()
    {
        eulerRotation = transform.rotation.eulerAngles;
        useAFKCamera = PlayerPrefs.GetInt("AFKCameraMode", 2);
    }

    // Update is called once per frame
    void Update()
    {

        //TODO: redo this
        //0 = afkCamera off, 1 = afkCamera on, 2 = afkCamera always
        if (Input.GetKeyDown(KeyCode.Alpha1) || Input.GetKeyDown(KeyCode.Keypad1))
        {
            useAFKCamera = 1;
            PlayerPrefs.SetInt("AFKCameraMode", 1);
        }

        if (Input.GetKeyDown(KeyCode.Alpha2) || Input.GetKeyDown(KeyCode.Keypad2))
        {
            useAFKCamera = 2;
            PlayerPrefs.SetInt("AFKCameraMode", 2);
        }

        if (Input.GetKeyDown(KeyCode.Alpha3) || Input.GetKeyDown(KeyCode.Keypad3))
        {
            useAFKCamera = 3;
            PlayerPrefs.SetInt("AFKCameraMode", 3);
        }

        isAFK = CheckAFK();
        if ((isAFK && useAFKCamera == 2) || useAFKCamera == 3) 
            ExecuteAutoCamera();

        Move();

        if (useAFKCamera != 3)
            Rotate();       
    }

    void Move()
    {
        //Basic input camera controls
        //Input button names are in all caps, see "VirtualKeys" enum for button names

        velocity.x = Input.GetAxisRaw("HORIZONTAL");
        velocity.y = Input.GetAxisRaw("STACKED");
        velocity.z = Input.GetAxisRaw("VERTICAL");

        if (useAFKCamera == 3)
            velocity.x = 0;

        if (Input.GetButton("BOOST"))
        {
            velocity *= boostSpeedModifier;
        }

        velocity *= moveSpeed * Time.unscaledDeltaTime;
        transform.Translate(velocity, Space.Self);
    }

    void Rotate()
    {
        if (Input.GetButton("FIRE2"))
        {
            Cursor.visible = false;
            Cursor.lockState = CursorLockMode.Locked;

            eulerRotation.y += Input.GetAxisRaw("MOUSEX") * mouseSensitivityX;
            eulerRotation.x += -Input.GetAxisRaw("MOUSEY") * mouseSensitivityY;

            if (eulerRotation.x > mouseClampY)
            {
                eulerRotation.x = mouseClampY;
            }
            else if (eulerRotation.x < -mouseClampY)
            {
                eulerRotation.x = -mouseClampY;
            }
            //*/
            //deltaRotation = transform.rotation.eulerAngles;
            transform.rotation = Quaternion.Euler(eulerRotation);
        }
        else
        {
            Cursor.visible = true;
            Cursor.lockState = CursorLockMode.None;
        }
    }

    bool CheckAFK()
    {
        if (Input.anyKey)
        {
            lastInput = 0;
        }
        else
        {
            lastInput += Time.unscaledDeltaTime;
        }

        if (lastInput > AFKThreshold)
            return true;
        return false;
    }

    void ExecuteAutoCamera()
    {
        //Look toward pivot
        Vector3 lookTowards = Quaternion.LookRotation(transform.parent.position - transform.position).eulerAngles;

        lookTowards.x = Mathf.LerpAngle(transform.rotation.eulerAngles.x, lookTowards.x, turnSpeed * Time.unscaledDeltaTime);
        lookTowards.y = Mathf.LerpAngle(transform.rotation.eulerAngles.y, lookTowards.y, turnSpeed * Time.unscaledDeltaTime);
        transform.rotation = Quaternion.Euler(lookTowards);

        //Rotate around pivot
        transform.parent.Rotate(0f, rotateSpeed * Time.unscaledDeltaTime, 0f);

        //Apply to euler
        eulerRotation = transform.rotation.eulerAngles;
    }

    private void OnDestroy()
    {
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;
    }
}
