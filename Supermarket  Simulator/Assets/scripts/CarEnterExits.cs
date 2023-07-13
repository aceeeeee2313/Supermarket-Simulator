using UnityEngine;

public class CarEnterExits : MonoBehaviour
{
    public float exitDistance = 2f;    // The distance from the car where the player should be placed when exiting

    private GameObject player;         // Reference to the player object
    private bool isPlayerInside = false; // Flag to check if the player is inside the car
    private Rigidbody carRigidbody;    // Reference to the car's rigidbody component

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player"); // Assuming the player object has the "Player" tag assigned
        carRigidbody = GetComponent<Rigidbody>(); // Get the car's rigidbody component
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            if (isPlayerInside)
            {
                ExitCar();
            }
            else
            {
                EnterCar();
            }
        }
    }

    void EnterCar()
    {
        // Disable player movement and control
        player.GetComponent<PlayerMovement>().enabled = false;

        // Parent the player to the car
        player.transform.SetParent(transform);

        // Move the player to a specific position within the car (adjust the position based on your car's setup)
        player.transform.localPosition = new Vector3(0f, 0.5f, 0f);

        // Rotate the player to match the car's orientation (adjust the rotation based on your car's setup)
        player.transform.localRotation = Quaternion.identity;

        // Disable car movement and physics
        carRigidbody.isKinematic = true;
        carRigidbody.velocity = Vector3.zero;

        isPlayerInside = true;
    }

    void ExitCar()
    {
        // Remove parent-child relationship between player and car
        player.transform.SetParent(null);

        // Calculate the exit position based on the car's position and orientation
        Vector3 exitPosition = transform.position + transform.right * exitDistance;

        // Move the player to the exit position
        player.transform.position = exitPosition;

        // Enable player movement and control
        player.GetComponent<PlayerMovement>().enabled = true;

        // Enable car movement and physics
        carRigidbody.isKinematic = false;

        isPlayerInside = false;
    }
}
