using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class SwitchController : MonoBehaviour, IEndDragHandler
{
    public static SwitchController instance;

    [SerializeField] int maxSwitches = 4;
    int currentSwitche;
    Vector3 switchPos;
    [SerializeField] Vector3 switchStep;
    [SerializeField] RectTransform switchesRect;

    [SerializeField] float tweenTime;
    [SerializeField] LeanTweenType TweenType;
    float dragThreshold;

    public List<AudioClip> audioClips = new List<AudioClip>();
    public List<GameObject> switchGameObjects = new List<GameObject>();
    public List<Sprite> unlockedSprites = new List<Sprite>();
    public List<bool> switchLocked = new List<bool>();
    public bool AudioSelection = false;
    public AudioSource audioSource;

    private int Score;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            SceneManager.sceneLoaded += OnSceneLoaded;
        }

        Score = PlayerPrefs.GetInt("Highscore", 0);
        currentSwitche = PlayerPrefs.GetInt("CurrentSwitche_" + AudioSelection, 1); // Load the saved switch index for each switch
        switchPos = switchesRect.localPosition + (currentSwitche - 1) * switchStep; // Set the initial position
        dragThreshold = Screen.width / 15;

        audioSource = GetComponent<AudioSource>();
        if (audioSource == null)
        {
            audioSource = gameObject.AddComponent<AudioSource>();
        }

        for (int i = 0; i < maxSwitches; i++)
        {
            switchLocked.Add(PlayerPrefs.GetInt("SwitchLocked_" + i, 0) == 0);
        }

        UpdateImagesBasedOnScore(Score);
        MoveSwitches(); // Move to the saved switch position

        // Play the audio clip if the current switch is not the first one
        if (currentSwitche > 1 && currentSwitche <= maxSwitches + 1 && !switchLocked[currentSwitche - 2])
        {
            AudioClip selectedClip = audioClips[currentSwitche - 2];
            dont_destroy.instance.SetAudioClip(selectedClip, AudioSelection, currentSwitche, switchLocked);
        }
    }

    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        if (scene.buildIndex == 0)
        {
            Debug.Log("SwitchController: OnSceneLoaded: Main Menu");
            Debug.Log("audioclips count: " + audioClips.Count);
            PlayerPrefs.SetInt("NumberOfSounds", audioClips.Count);
            PlayerPrefs.Save();
            Debug.Log("Number of sounds: " + PlayerPrefs.GetInt("NumberOfSounds"));
        }
    }

    private void OnDestroy()
    {
        if (instance == this)
        {
            SceneManager.sceneLoaded -= OnSceneLoaded;
        }
    }

    public void Next()
    {
        if (currentSwitche < maxSwitches + 1)
        {
            currentSwitche++;
            switchPos += switchStep;
            MoveSwitches();
            PlayerPrefs.SetInt("CurrentSwitche_" + AudioSelection, currentSwitche); // Save the current switch index for each switch
        }
    }

    public void Previous()
    {
        if (currentSwitche > 1)
        {
            currentSwitche--;
            switchPos -= switchStep;
            MoveSwitches();
            PlayerPrefs.SetInt("CurrentSwitche_" + AudioSelection, currentSwitche); // Save the current switch index for each switch
        }
    }

    void MoveSwitches()
    {
        switchesRect.LeanMoveLocal(switchPos, tweenTime).setEase(LeanTweenType.easeOutBack);

        AudioClip selectedClip = null;

        if (currentSwitche == 1)
        {
            StopCurrentSound();
            dont_destroy.instance.SetAudioClip(null, true, currentSwitche, switchLocked);
        }
        else if (currentSwitche > 1 && currentSwitche <= maxSwitches + 1)
        {
            if (!switchLocked[currentSwitche - 2])
            {
                selectedClip = audioClips[currentSwitche - 2];
            }
        }

        dont_destroy.instance.SetAudioClip(selectedClip, AudioSelection, currentSwitche, switchLocked);
    }


    public void StopCurrentSound()
    {
        dont_destroy.instance.StopSound(AudioSelection);
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        if (Mathf.Abs(eventData.position.x - eventData.pressPosition.x) > dragThreshold)
        {
            if (eventData.position.x > eventData.pressPosition.x)
            {
                Previous();
            }
            else
            {
                Next();
            }
        }
    }

    void UpdateImagesBasedOnScore(int score)
    {
        int requiredScore = 20;
        for (int i = 1; i < switchGameObjects.Count; i++)
        {
            requiredScore += 50;
            if (score >= requiredScore && i < unlockedSprites.Count)
            {
                Debug.Log("SwitchController: UpdateImagesBasedOnScore: Unlocking switch " + i);
                Image imageComponent = switchGameObjects[i].GetComponent<Image>();
                if (imageComponent != null)
                {
                    imageComponent.sprite = unlockedSprites[i];
                    imageComponent.preserveAspect = false;
                    switchLocked[i] = false;
                    PlayerPrefs.SetInt("SwitchLocked_" + i, 0); // Save the unlocked state

                    Transform switchTransform = switchGameObjects[i].transform.Find("Text");
                    if (switchTransform != null)
                    {
                        switchTransform.gameObject.SetActive(true);
                    }
                }
            }
        }
    }
}