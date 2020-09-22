using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class MenuSystem : MonoBehaviour {

   public void PlayAgian()
    {
         SceneManager.LoadScene("Level"); 
    }
}
