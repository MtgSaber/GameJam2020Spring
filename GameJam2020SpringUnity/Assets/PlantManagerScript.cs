using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlantManagerScript : MonoBehaviour {
    public const float FUTURE_OFFSET = 16.6f;
    
    public GameObject mushroom;
    public GameObject tree;
    public GameObject vine;

    private LinkedList<GameObject> plants;

    // Start is called before the first frame update
    void Start()
    {
        this.plants = new LinkedList<GameObject>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void CreatePlant(int type, Vector2 seedPos) {
        switch (type) {
            case 0:
                Instantiate(this.tree, seedPos + new Vector2(0, FUTURE_OFFSET), Quaternion.identity);
                this.plants.AddLast(this.tree);
                break;
            case 1:
                Instantiate(this.mushroom, seedPos + new Vector2(0, FUTURE_OFFSET), Quaternion.identity);
                this.plants.AddLast(this.mushroom);
                break;
            case 2:
                Instantiate(this.vine, seedPos + new Vector2(0, FUTURE_OFFSET), Quaternion.identity);
                this.vine.transform.transform.Rotate(0.0f, 0.0f, 90.0f);
                this.plants.AddLast(this.vine);
                break;
        }
    }

    public void BiteZaDusto() {
        foreach (var plant in this.plants) Destroy(plant);
    }
}
