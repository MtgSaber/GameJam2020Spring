using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShaderAutomator : MonoBehaviour
{
	Renderer rend;
	public int count;
    // Start is called before the first frame update
    void Start()
    {
		rend = GetComponent<Renderer>();
		count = 0;
        
    }

    // Update is called once per frame
    void Update()
    {
		count++;
		Material m = rend.material;
        m.SetFloat("_VOffset",count%250/250.0f);
		m.SetFloat("_HOffset",count%1000/1000.0f);

		rend.material = m;
		if (Input.GetKeyDown(KeyCode.K))
        {
	        Debug.Log(m.GetFloat("_VOffset"));
		}
    }
}
