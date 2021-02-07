using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
public class TimeCheck : MonoBehaviour
{
    void Update(){

            StartCoroutine(time());       

    }
    IEnumerator time(){
        if(Control.count==7){

            yield return new WaitForSeconds(16.5f);
            this.GetComponent<TextMeshProUGUI>().text="클리어 시간 : "+ (180.0f-HealthCtrl.time).ToString("N2")+"초";
        

        }
    }
}
