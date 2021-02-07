using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class WrieControl : MonoBehaviour
{
    public GameObject[] images=new GameObject[8];
    
    public RawImage[] w_long=new RawImage[4];
    
    private Camera mainCamera;
    private string w_name;
    private string c_name;
    private string copy_name;
    private int correct;
    
    public static int cnt=0;
    // Start is called before the first frame update

    void Start()
    {
        cnt=0;
        mainCamera = GameObject.Find("ARCamera").GetComponent<Camera>();    
    }

    // Update is called once per frame
    void Update()
    {
         if (Input.GetMouseButton(0)){
            Ray ray = mainCamera.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;
            if(Physics.Raycast(ray, out hit, 10000f) && hit.collider.CompareTag("Wires")){
                w_name=hit.transform.gameObject.name;
                
                if(WrieControl.cnt==4){
                    StartCoroutine(wait());
                }
                if(w_name==c_name){
                    WrieControl.cnt++;
                    w_long[correct].gameObject.SetActive(true);
                }
                
                
                if(w_name=="Blue"){
                    copy_name=w_name;
                    c_name="Blue_R";
                    correct=0;
                }
                else if(w_name=="Blue_R"){
                    copy_name=w_name;
                    c_name="Blue";
                    correct=0;
                }
                else if(w_name=="Pink"){
                    copy_name=w_name;
                    c_name="Pink_R";
                    correct=1;
                }
                else if(w_name=="Pink_R"){
                    copy_name=w_name;
                    c_name="Pink";
                    correct=1;
                }
                else if(w_name=="Yellow"){
                    copy_name=w_name;
                    c_name="Yellow_R";
                    correct=2;
                }
                else if(w_name =="Yellow_R"){
                    copy_name=w_name;
                    c_name="Yellow";
                    correct=2;
                }
                else if(w_name=="Red"){
                    copy_name=w_name;
                    c_name="Red_R";
                    correct=3;
                }
                else if(w_name=="Red_R"){
                    copy_name=w_name;
                    c_name="Red";
                    correct=3;
                }

            }
         }
    }
    IEnumerator wait(){
        yield return new WaitForSeconds(1.5f);
        this.gameObject.SetActive(false);
        
    }
}
