using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Button_Script : MonoBehaviour
{
    public GameObject spawnObject;
    public Vector3 spawnLocation = new Vector3(0,0,0);
    public Button yourButton;


    // Start is called before the first frame update
    void Start()
    {
		Button btn = yourButton.GetComponent<Button>();
		btn.onClick.AddListener(OnClick);    
    }
    void OnClick(){
        GameObject obj = GameObject.Instantiate(spawnObject, spawnLocation, new Quaternion());
        obj.GetComponent<Particle_Controller>().randomVelocity = true;
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
