using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

using Vuforia;
public class FireCtrl : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject bullet;
    public Transform firePos;
    public GameObject V_button;
    private bool isPressed;
     private Behavior behavior;
    public GameObject player_transform;

    //라이플 오브젝트
    public GameObject rifle;
    public static bool isRifleOn;

    //오디오
    public AudioSource source;
    public AudioClip clip;
     public AudioClip clip2;
     
     public bool Crowd =false;
    void Start()
    {
        isRifleOn=false;
        VirtualButtonBehaviour vb_behaviour;
        vb_behaviour = V_button.GetComponent<VirtualButtonBehaviour>();
        vb_behaviour.RegisterOnButtonPressed(onButtonPressed);
        isRifleOn = false;
    }


    // Update is called once per frame
    void Update()
    {
        if(Control.count==5){
            isRifleOn=false;
        }
        if(isRifleOn == true && isPressed == true)
        {
            Fire();
            isPressed = false;
        }
        if(Crowd == true)
        {
          source.clip = clip2;
          source.Play();
          Crowd = false;
        }

    }
    public void onButtonPressed(VirtualButtonBehaviour vb)
    {
        //ispressed true로 반환
        isPressed = true;
        
    }
    void Fire()
    {
        source.clip = clip;
        source.Play();
        CreatBullet();
        
    }

    void CreatBullet()
    {
        bullet.SetActive(true);
        firePos.rotation =  player_transform.transform.rotation;
        Instantiate(bullet, firePos.position, firePos.rotation);
    }

    //버튼 눌렀을 때 라이플 활성화
    public void RifleCreateButton()
    {
        Crowd = true;
        isRifleOn = true;
        rifle.SetActive(true);
    }

}
