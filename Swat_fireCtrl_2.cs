using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Swat_fireCtrl_2 : MonoBehaviour
{
    public GameObject bullet;
    public Transform firePos;
    public GameObject player;
    private bool die=false;

    private float timer=0.0f;
    public static int counting=0;
    public AudioClip clip;
    public AudioSource source;
    void Start()
    {
        counting=0;
        timer=Random.Range(0.0f,2.0f);
    }

    // Update is called once per frame
    void Update()
    {
        if(Control.count==4 && die==false){
            transform.LookAt(player.transform.position);
            timer+=Time.deltaTime;
        }
        if(Control.count==4 && timer>=3.0f){
            if(Control.P_HP>0){
                CreatBullet();         
                timer=0.0f;
            }
        }
        
        
    }
    void OnCollisionEnter(Collision col){
        if(col.gameObject.CompareTag("bullet")){
            this.gameObject.GetComponent<Animator>().SetTrigger("Die");
            StartCoroutine(Die());
            
        }
    }
    void CreatBullet()
    {   
        
            source.clip = clip;
            source.Play();
            bullet.SetActive(true);
            firePos.rotation =  transform.rotation;
            Instantiate(bullet, firePos.position, firePos.rotation);
        

    }
    IEnumerator Die(){
        die=true;
        yield return new WaitForSeconds(1.5f);
        
        Destroy(this.gameObject);
        Swat_fireCtrl_2.counting++;
    }
}
