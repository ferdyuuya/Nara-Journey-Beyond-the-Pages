using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; } // Singleton instance

    public GameObject playerPrefab; // Reference to your player PREFAB (not the instance in the scene)
    public GameObject uiPrefab; // Reference to the UI prefab
    private GameObject playerInstance; // Reference to the active player INSTANCE
    private GameObject uiInstance; // Reference to the active UI INSTANCE


    private void Awake()
    {
        // Singleton pattern implementation
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }

        // Subscribe to the sceneLoaded event
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        // Find the existing player or instantiate a new one
        playerInstance = GameObject.FindGameObjectWithTag("Player");
        uiInstance = GameObject.FindGameObjectWithTag("UITag");
        if (playerInstance == null)
        {
            // Instantiate a new player if not found (e.g., in the first scene)
            playerInstance = Instantiate(playerPrefab);
            uiInstance = Instantiate(uiPrefab);
            playerInstance.tag = "Player";
            uiInstance.tag = "UI";
        }

        SceneManager.MoveGameObjectToScene(playerInstance, scene);
    }

    private void OnDestroy()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;
        Destroy(playerPrefab);
    }
}
