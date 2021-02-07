using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthCtrl : MonoBehaviour
{
    public GameObject Healthbar;
    public Text text;
    private int value;
    public static float time;
    // Start is called before the first frame update
    void Start()
    {
        time=180.0f;
    }

    // Update is called once per frame
    void Update()
    {
        if(time>0){
            if(Control.count<7){
                time-=Time.deltaTime;
            }
            Healthbar.GetComponent<Slider>().value = Control.P_HP;
            if(time<=10){
                text.text = "HP "+ Control.P_HP.ToString()+" / 100" +"<color=#FF0000>" +"  남은시간 :"+time.ToString("N2") +"</color>";
            }
            else
                text.text = "HP "+ Control.P_HP.ToString()+" / 100" + "  남은시간 :"+time.ToString("N2");
            //어떤 액션할지 짜기.
        }
        else{
            time=0;
        }
    }
}
