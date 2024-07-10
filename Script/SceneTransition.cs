using UnityEngine.SceneManagement;
using UnityEngine;

public class SceneTransition : MonoBehaviour
{
    // Public Fields
    public string nextScene;    // Name of the scene to load
    public Transform spawnPoint;  // Reference to the spawn point in the new scene
    public string previousScene;  // Name of the scene where the player came from
    public GameObject playerPrefab; // Prefab of the player character (only if you want to instantiate the player)

    // Private Fields
    private GameObject playerInstance; // Reference to the current player instance

    // Unity Events
    private void Awake()
    {
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    private void OnDestroy()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }

    // Public Methods
    public void TriggerSceneChange()
    {
        if (string.IsNullOrEmpty(nextScene))
        {
            Debug.LogError("Scene name not set for scene transition!");
            return; // Exit early if the scene name is invalid
        }

        // If player exists, keep it across scenes
        playerInstance = GameManager.Instance.playerPrefab;
        if (playerInstance != null)
        {
            DontDestroyOnLoad(playerInstance);
        }

        SceneManager.LoadScene(nextScene);
    }

    // Private Methods
    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        if (scene.name != nextScene)
        {
            return; // Only handle the specific scene we transitioned to
        }

        playerInstance = GameObject.FindGameObjectWithTag("Player");
        if (playerInstance == null && playerPrefab != null)
        {
            playerInstance = Instantiate(playerPrefab, spawnPoint.position, spawnPoint.rotation);
            playerInstance.tag = "Player"; // Ensure the tag is set if instantiated
        }

        Transform targetSpawn = spawnPoint; // Default to the assigned spawnPoint
        if (!string.IsNullOrEmpty(previousScene))
        {
            Transform potentialSpawn = transform.Find(previousScene + "Spawn");
            if (potentialSpawn != null)
            {
                targetSpawn = potentialSpawn;
            }
        }

        if (playerInstance != null && targetSpawn != null)
        {
            playerInstance.transform.position = targetSpawn.position;
            SceneManager.MoveGameObjectToScene(playerInstance, scene); // Move to the correct scene if instantiated
        }
        else
        {
            Debug.LogError("Player or Spawn Point not found in the new scene!");
        }

        GameManager.Instance.playerPrefab = playerInstance;

        if (scene.name == "" && playerInstance != null)
        {
            Destroy(playerInstance);
            playerInstance = null; // Reset referensi playerInstance
        }
    }
}