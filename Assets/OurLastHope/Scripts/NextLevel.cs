using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NextLevel : MonoBehaviour
{
    [SerializeField] private string level;
    // Start is called before the first frame update
    public void nextLevel(){
        PlayerPrefs.SetInt(level, 1);
    }
}
