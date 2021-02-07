using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulltetCtrl : MonoBehaviour
{
    public float speed= 1000.0f;
    



    void Start()
    {
        GetComponent<Rigidbody>().AddForce(transform.forward*speed);
    }

    void  OnCollisionEnter(Collision col){
            if(col.gameObject.CompareTag("Player")){
                Control.P_HP-=10;
                Debug.Log(Control.P_HP);
                if(Control.P_HP<=0){
                    Control.P_HP=0;
                    Debug.Log("GameOver");
                }
            }

            Destroy(this.gameObject);
            
    }
}
