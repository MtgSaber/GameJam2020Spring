using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActivateOnPush : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
		if(Input.GetKeyDown(KeyCode.Escape)) {
			Debug.Log("This works!!!!");
			GameObject music = GameObject.Find("Scattered_Assault");
			AudioSource audio = music.GetComponent(typeof(AudioSource)) as AudioSource;
			audio.Stop();
		}
        
    }
}
