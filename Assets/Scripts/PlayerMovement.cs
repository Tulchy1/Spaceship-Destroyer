using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [Header("General Setup Settings")]
    [Tooltip("How fast the ship moves up and down")]
    [SerializeField] float controlSpeed = 25f;
    [SerializeField] float xRange = 5.3f;
    [SerializeField] float yRange = 5f;

    [Header("Laser array")]
    [SerializeField] GameObject[] lasers;

    [Header("Screen position based tuning")]
    [SerializeField] float pitchFactor = -2f;
    [SerializeField] float yawFactor = 2f;

    [Header("Player position based tuning")]
    [SerializeField] float controlFactor = -15f;
    [SerializeField] float rollFactor = -20f;

    float xThrow;
    float yThrow;
    // Update is called once per frame
    void Update()
    {
        ProcessTranslation();
        ProcessRotation();
        ProcessFiring();
        if(Input.GetKey("escape"))
        {
            Application.Quit();
        }
    }

    void ProcessTranslation()
    {
        xThrow = Input.GetAxis("Horizontal");
        yThrow = Input.GetAxis("Vertical");

        float xOffset = xThrow * Time.deltaTime * controlSpeed;
        float xPos = transform.localPosition.x + xOffset;
        float clampedxPos = Mathf.Clamp(xPos, -xRange, xRange);

        float yOffset = yThrow * Time.deltaTime * controlSpeed;
        float yPos = transform.localPosition.y + yOffset;
        float clampedyPos = Mathf.Clamp(yPos, -yRange, yRange);

        transform.localPosition = new Vector3(clampedxPos, clampedyPos, transform.localPosition.z);
    }
    void ProcessRotation()
    {
        float pitchPosition = transform.localPosition.y * pitchFactor;
        float pitchControl = yThrow * controlFactor;

        float pitch = pitchPosition + pitchControl;
        float yaw = transform.localPosition.x * yawFactor;
        float roll = xThrow * rollFactor;

        transform.localRotation = Quaternion.Euler(pitch, yaw, roll);
    }

    void ProcessFiring()
    {
        if(Input.GetButton("Fire1"))
        {
            SetLasersActive(true);
        }
        else
        {
            SetLasersActive(false);
        }
    }

    void SetLasersActive(bool isActive)
    {
        foreach (GameObject laser in lasers)
        {
            var emission = laser.GetComponent<ParticleSystem>().emission;
            emission.enabled = isActive;
        }
    }
}
