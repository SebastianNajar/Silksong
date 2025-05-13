using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public bool reset;

    public static GameManager Instance;

    public PlayerData playerData;
    public PlayerAbilities abilities;

    public HashSet<string> destroyedObjects = new HashSet<string>();

    public Transform respawnPoint;

    //Title Screen
    public bool onTitle;

    //Level 1
    private Animator beadDoorAnimator;
    public AudioClip beadDoorClip;
    private bool opened = false;

    public void Awake()
    {
        //Reset Player Data
        if (reset)
        {
            playerData.HP = 3;
            playerData.score = 0;
            playerData.beads = 0;
        }

        if (Instance != null) { Destroy(gameObject); return; }
        Instance = this;
        DontDestroyOnLoad(gameObject);
    }

    private void Update()
    {
        if (onTitle)
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                onTitle = false;
                SceneManager.LoadScene("Section 1");
            }
        }
    }

    public void StartEvent(string newEvent)
    {
        switch (newEvent)
        {
            case "beadDoor":
                OpenBeadDoor();
                break;
            case "dashCloak":
                abilities.ObtainDash();
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
            SoundManager.instance.PlaySoundClip(beadDoorClip, transform, 5);
            opened = true;
            StartCoroutine(ClearBeads());
        }
    }

    private IEnumerator ClearBeads()
    {
        int numBeads = playerData.beads;

        for(int i = 0; i < numBeads; i++)
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
