using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GravityManager : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject[] cylinder=new GameObject[4];
    private bool playOnce=false;
    void Start()
    {
    
    }

    // Update is called once per frame
    void Update()
    {
        if(this.enabled == true && playOnce==false){
            playOnce=true;
            for(int i=0; i<4; i++)
                cylinder[i].gameObject.GetComponent<Rigidbody>().useGravity=true;
        }        
    }
}
