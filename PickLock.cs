using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickLock : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject picks_end;
    private GameObject pickgame;
    public static int picks_correct=0;
    public AudioSource source;
    void Start()
    {
        picks_correct=0;
        pickgame=GameObject.Find("PickGame");
    }

    // Update is called once per frame
    void Update()
    {
        if(picks_correct==4 && pickgame==true){
            pickgame.SetActive(false);

        }
    }
     void OnCollisionEnter(Collision col){
         if(col.gameObject.CompareTag("picks_end")){
             this.gameObject.GetComponent<Rigidbody>().constraints=RigidbodyConstraints.FreezeAll;
             this.gameObject.GetComponent<Rigidbody>().useGravity=false;
             source.Play();
             Destroy(col.gameObject);
             picks_correct++;
         }
     }
}
