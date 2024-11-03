using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoNotDestroy : MonoBehaviour
{
   private void Awake()
   {
      GameObject[] audioobj = GameObject.FindGameObjectsWithTag("Audio");
      if (audioobj.Length > 1)
      {
         Destroy(this.gameObject);
      }
      DontDestroyOnLoad(this.gameObject);
   }
}
