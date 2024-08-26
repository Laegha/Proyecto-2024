using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    static GameManager instance;
    public static GameManager gm
    {
        get { return instance;}
    }

    void Awake()
    {
        if (instance != null)
        {
            Destroy(this);
            return;
        }

        instance = this;
        DontDestroyOnLoad(gameObject);

        SceneManager.sceneLoaded += GenerateHands;
    }
    
    MinigameManager currMinigameManager;

    [SerializeField] GameObject cursorPrefab;

    public void OnScored(PlayerIdentifier scorer) //Is called by elements on scene
    {
        currMinigameManager.OnScored(scorer);

        List<PointScoreReciever> scoredRecievers = FindObjectsOfType<PointScoreReciever>().ToList();
        foreach (PointScoreReciever reciever in scoredRecievers)
            reciever.OnScored(currMinigameManager);
    }

    public void EndMinigame()
    {
        Debug.Log("Minigame Ended");
        SceneManager.LoadScene("MainMenu");
        currMinigameManager = null;
    }

    public void StartMinigame(string minigameManagerType)
    {
        Type type = Type.GetType(minigameManagerType, false, true);
        currMinigameManager = (MinigameManager) Activator.CreateInstance(type);
    }

    public void UpdateHandsRotation()
    {
        foreach (HandRotation hand in FindObjectsOfType<HandRotation>())
            hand.RotationUpdate();
        
        foreach (CursorMovement cursor in FindObjectsOfType<CursorMovement>())
            cursor.CursorUpdate();

    }

    public void UpdateHandsPosition()
    {
        foreach(HandMovement hand in FindObjectsOfType<HandMovement>())
            hand.MovementUpdate();
        
    }

    public void GenerateHands(Scene scene, LoadSceneMode mode)
    {
        foreach(var controller in ControllersManager.controllersManager.Controllers)
        {
            GameObject hand = Instantiate(currMinigameManager != null ? currMinigameManager.minigameHandPrefab : cursorPrefab, transform.position, Quaternion.identity);
            hand.GetComponent<PlayerIdentifier>().AssignedController = controller.Key;
            if(currMinigameManager == null)
            {
                hand.transform.SetParent(GameObject.Find("Canvas").transform);
                hand.GetComponent<RectTransform>().localPosition = Vector3.zero;
            }
            else
                hand.transform.position = GameObject.Find("HandSpawner").transform.position;

        }
    }
}
