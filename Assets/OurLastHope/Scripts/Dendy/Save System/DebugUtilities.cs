using UnityEngine;

public class DebugUtilities : MonoBehaviour
{
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.R) && Input.GetKey(KeyCode.LeftShift))
        {
            ResetSaveData();
        }
    }

    void ResetSaveData()
    {
        PlayerPrefs.DeleteAll();
        Debug.Log("Save data has been reset.");
    }
}
