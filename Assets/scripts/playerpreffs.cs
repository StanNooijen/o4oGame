using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class playerpreffs : MonoBehaviour
{
    public void Clear()
    {
        // Clear all PlayerPrefs
        PlayerPrefs.DeleteAll();
        PlayerPrefs.Save();
        Debug.Log("PlayerPrefs have been cleared.");
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        dont_destroy.instance.SetAudioClip(null, false, 0, null);
    }
}
