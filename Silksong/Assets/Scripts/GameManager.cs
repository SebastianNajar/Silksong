using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    public PlayerData playerData;
    public HashSet<string> destroyedObjects = new HashSet<string>();

    //Level 1
    public GameObject beadDoor;
    private Animator beadDoorAnimator;
    private bool opened = false;

    public void Awake()
    {
        //Reset Player Data
        playerData.HP = 3;
        playerData.score = 0;
        playerData.beads = 0;

        if (Instance != null) { Destroy(gameObject); return; }
        Instance = this;
        DontDestroyOnLoad(gameObject);
    }

    public void StartEvent(string newEvent)
    {
        switch (newEvent)
        {
            case "beadDoor":
                OpenBeadDoor();
                break;
        }
    }

    public void OpenBeadDoor()
    {
        if (!opened)
        {
            beadDoorAnimator = GameObject.Find("Bead Door").GetComponent<Animator>();
        }

        if(playerData.beads > 3 && !opened)
        {
            beadDoorAnimator.SetBool("open", true);
            opened = true;
            StartCoroutine(ClearBeads());
        }
    }

    private IEnumerator ClearBeads()
    {
        for(int i = 0; i < playerData.beads; i++)
        {
            yield return new WaitForSeconds(0.5f);
            playerData.beads--;
        }
    }

    public void RegisterDestroyedObject(string id)
    {
        destroyedObjects.Add(id);
    }

    public bool IsObjectDestroyed(string id)
    {
        return destroyedObjects.Contains(id);
    }
}
