using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public Transform playerTransform;
    public Vector3 offset;
    [Range(1, 10)]
    public float smoothFactor;
    public Vector3 minValues, maxValues;

    private GameManager gameManager;
    void Start()
    {
        gameManager = FindObjectOfType<GameManager>(); // Find the GameManager
        if (gameManager != null)
        {
            playerTransform = gameManager.playerPrefab.transform; // Assuming GameManager has a 'player' reference
        }
        else
        {
            Debug.LogError("GameManager not found!");
        }
    }

    private void FixedUpdate()
    {
        Follow();
    }

    void Follow() //For FollowCharachter
    {
        //Define minimum and maximum xyz position

        Vector3 playerPosition = playerTransform.position + offset;
        //Verify playerPosition is out of the bounds limits
        //Limit it to the min and max values
        Vector3 boundPosition = new Vector3(
            Mathf.Clamp(playerPosition.x, minValues.x, maxValues.x),
            Mathf.Clamp(playerPosition.y, minValues.y, maxValues.y),
            Mathf.Clamp(playerPosition.z, minValues.z, maxValues.z));

        Vector3 smoothPosition = Vector3.Lerp(transform.position, boundPosition, smoothFactor*Time.fixedDeltaTime);
        transform.position = smoothPosition;
    }
}