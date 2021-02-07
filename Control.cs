using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;
public class Control : MonoBehaviour
{
   private Behavior behavior; // 캐릭터의 행동 스크립트
   private FireCtrl fireCtrl;
    private Camera mainCamera; // 메인 카메라
    private Vector3 targetPos; // 캐릭터의 이동 타겟 위치
    public GameObject ring;
    public GameObject effect;
    public GameObject[] Object=new GameObject[3];
    public GameObject Door;
    public GameObject Door_2;
    public GameObject sci_door;
    public GameObject sci_door2;
    public GameObject Canvas;
    public GameObject Gameover;
    public GameObject Endning;
    public TextMeshProUGUI hintMessage;
    public static int count=0;
    private bool playOnce=false;
    //패스워드 순서
    public int pw_order;
    //패스워드 텍스트
    public Text[] pw_Message = new Text[4];
    public int[] password = new int[4];
    //터미널 이미지
    public GameObject terminal;
    public GameObject Wires;
    public GameObject picks;
    public static int P_HP=100;
    public bool die=false;
    
    private int money_count = 0;
    //오디오
    public AudioSource[] audioSource = new AudioSource[6];
    public AudioClip[] clips = new AudioClip[7];
    
    void Start()
    {

        P_HP=100;
        count=0;
        behavior = GetComponent<Behavior>();
        mainCamera = GameObject.Find("ARCamera").GetComponent<Camera>();
        //hintM.SetActive(true);
        hintMessage.text = "당신은 무장강도입니다. 반짝이는 포인트 지점으로 가서 기기를 해킹하세요.";


    }
 
    void Update()
    {
        //UI text 출력

        // 마우스 입력을 받았 을 때
        if (Input.GetMouseButtonDown(0))
        {

            audioSource[3].clip = clips[4];
            audioSource[3].Play();
            behavior.start=true;
            // 마우스로 찍은 위치의 좌표 값을 가져온다
            Ray ray = mainCamera.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;
            if (Physics.Raycast(ray, out hit, 10000f) && (hit.collider.CompareTag("Floor") || hit.collider.CompareTag("StagePoint")))
            {
                this.gameObject.GetComponent<Animator>().SetTrigger("Walk");
                targetPos = hit.point;
                effect.transform.position = new Vector3(targetPos.x, targetPos.y+0.05f, targetPos.z);
                if(die==false)
                    StartCoroutine(EffectStart());

                
                
            }


        }
        //audio 컨트롤
        if(die==false){
            if(Control.P_HP<=0){
                CheckLive();
            }
            if(HealthCtrl.time<=0){
                CheckLive();
            }
        // 캐릭터가 움직이고 있다면

        if(behavior.Run(targetPos))
            {
                
                behavior.Turn(targetPos);
            }
            else{
                this.gameObject.GetComponent<Animator>().SetTrigger("Idle");
            }


        if(count==1 && WrieControl.cnt==4 && FireCtrl.isRifleOn==false &&playOnce==false){
            playOnce=true;
            StartCoroutine(StartMessage());
        }

        if(count==1 && FireCtrl.isRifleOn==true&& playOnce==true){
            playOnce=false;
            ring.transform.position=Object[2].transform.position;
            ring.SetActive(true);
            
        }
        
        if(count==2 && Pw_check()==true && playOnce==false){
            playOnce=true;
            audioSource[6].Stop();
            terminal.SetActive(false);
            Object[2].transform.position=new Vector3(-2.77999997f,0.0399999991f,1.49699998f);
            ring.transform.position=Object[2].transform.position;
            ring.SetActive(true);
        }

        if(count==3 && Swat_fireCtrl.counting==5 && playOnce==true){
            playOnce=false;
            hintMessage.text="반짝이는 포인트로 이동하여 다음 스테이지로 넘어가십시오.";
            Object[2].transform.position=new Vector3(0.490999997f,0.0399999991f,1.81900001f);
            ring.transform.position=Object[2].transform.position;
            ring.SetActive(true);
        }
        if(count==4 && Swat_fireCtrl_2.counting==5 && playOnce==false){
            playOnce=true;
            hintMessage.text="반짝이는 포인트로 이동하여 다음 스테이지로 넘어가십시오.";
            Object[2].transform.position=new Vector3(3.44000006f,0.0399999991f,1.96500003f);
            ring.transform.position=Object[2].transform.position;
            ring.SetActive(true);
        }
        // 돈 획득했을 경우
        if(count==5 && money_count == 3 && playOnce==true){
            playOnce=false;
            Object[2].transform.position=new Vector3(4.98400021f,0.0399999991f,1.91299999f);
            ring.transform.position=Object[2].transform.position;
            ring.SetActive(true);
            hintMessage.text="열쇠 마커를 통해 자물쇠와 상호작용을하여 문을 여십시오.";
            
        }
        if(count==6 && PickLock.picks_correct==4 && playOnce==false){
            playOnce=true;
            Door_2.gameObject.GetComponent<Animator>().SetTrigger("Door_open");
            audioSource[4].clip = clips[1];
            audioSource[4].Play();
            Object[2].transform.position=new Vector3(5.79799986f,0.0399999991f,1.91299999f);
            ring.transform.position=Object[2].transform.position;
            ring.SetActive(true);
        }
    }

}



    void  OnCollisionEnter(Collision col){

        if(count==0){
            if(col.gameObject.tag=="StagePoint"){
                //atm앞에 왔을 경우
                
                col.gameObject.SetActive(false);
                Wires.SetActive(true);
                hintMessage.text="각각의 선을 알맞게 이어 붙여 기기를 해킹하세요. ";
                count++;
                    
                //다음 스테이지 전환
                
                
            }
        }
        else if(count==1){
            if(col.gameObject.tag=="StagePoint"){
                col.gameObject.SetActive(false);
                hintMessage.text="보안코드를 입력하세요.";
                ring.SetActive(false);

                audioSource[6].clip = clips[9];
                audioSource[6].volume = 1;
                audioSource[6].loop = true;
                audioSource[6].Play();

                //터미널 실행
                terminal.SetActive(true);

                count++; //직원 컴퓨터
            }
        }
        else if(count==2){
            if(col.gameObject.tag=="StagePoint"){
                col.gameObject.SetActive(false);
                StartCoroutine(StartMessage());
                Door.gameObject.GetComponent<Animator>().SetTrigger("Door_open");

                //문열림 사운드
                audioSource[1].clip = clips[1];
                audioSource[1].Play();
                ring.SetActive(false);
                
                
            }
        }
        else if(count==3){
            if(col.gameObject.tag=="StagePoint"){
                col.gameObject.SetActive(false);
                StartCoroutine(StartMessage());
                sci_door.gameObject.GetComponent<Animator>().SetTrigger("sci_door");
                //금고 문열림사운드
                audioSource[2].clip=clips[2];
                audioSource[2].Play();
                ring.SetActive(false);
 //스테이지 2 문 앞

            }
        }
        else if(count==4){
            if(col.gameObject.tag=="StagePoint"){
                
                col.gameObject.SetActive(false);
                hintMessage.text="문을 열었습니다. \n 모든 돈을 획득 후, 탈출하세요.";
                sci_door2.gameObject.GetComponent<Animator>().SetTrigger("sci_door");
                //금고 문열림사운드
                audioSource[4].clip=clips[2];
                audioSource[4].Play();
                ring.SetActive(false);
                count++;
            }
        }




        else if(count==5){
            if(col.gameObject.tag=="StagePoint"){
                col.gameObject.SetActive(false);


                ring.SetActive(false);
                picks.SetActive(true);
                count++;

            }
            //돈을 획득했을 경우
            else if(col.gameObject.tag =="Money"){
                
                if(money_count == 3)
                {
                    count++;
                }
                else
                {
                    hintMessage.text="돈을 획득했습니다.";
                    col.gameObject.SetActive(false);
                    audioSource[3].clip = clips[6];
                    audioSource[3].volume = 1;
                    audioSource[3].Play();
                    money_count++;
                }
            }
        }
        else if(count==6){
                if(col.gameObject.tag=="StagePoint"){
                    
                    col.gameObject.SetActive(false);
                    hintMessage.text="탈출에 성공하였습니다.";
                    //금고 문열림사운드
                    audioSource[0].clip=clips[11];
                    audioSource[0].Play();
                    ring.SetActive(false);
                    Canvas.SetActive(true);
                    Endning.SetActive(true);
                    this.gameObject.GetComponent<Animator>().SetTrigger("Dance");
                    count++;
                    
                }
            }
    
    }
    IEnumerator StartMessage()
    {
        if(count==1){
            yield return new WaitForSeconds(3.0f);
            hintMessage.text = "기기를 해킹하여 컴퓨터 보안코드를 획득하였습니다. 코드는 \n 4532 입니다.";
            yield return new WaitForSeconds(2.0f);
            hintMessage.text = "총을 꺼내 시민들을 위협한 후, 직원 컴퓨터를 해킹하십시오. \n Virtual Button을 통해 총을 발사 하세요.";
            Object[1].SetActive(true);
        }
        else if(count==2){
            count++; //스테이지 1 문 앞
            hintMessage.text="문을 열었습니다. \nVirtual 버튼을 통해 총을 발사하여 경찰을 제압하세요.";
            yield return new WaitForSeconds(2.0f);
            hintMessage.text="";
        }
        else if(count==3){
            count++; //스테이지 1 문 앞
            hintMessage.text="문을 열었습니다. \nVirtual 버튼을 통해 총을 발사하여 경찰을 제압하세요.";
            yield return new WaitForSeconds(2.0f);
            hintMessage.text="";
        }
    }
    
    IEnumerator EffectStart() {
        effect.SetActive(true);
        yield return new WaitForSeconds(0.1f);
        effect.SetActive(false);
    }
    public bool Pw_check(){
        int sum=password[0]*1000+password[1]*100+password[2]*10+password[3]*1;
        if(sum==4532){
            return true;
        }
        else
            return false;
    }
    //Terminal 버튼
    public void Plus()
    {
        password[pw_order]++;

        if(password[pw_order] >= 10 )
        {
            password[pw_order]=1;
        }

        
        pw_Message[pw_order].text = password[pw_order].ToString();

           
    }

    public void Minus()
    {
        password[pw_order]--;
        if(password[pw_order]<=0)
        {
            password[pw_order]=9;
            
        }
        pw_Message[pw_order].text = password[pw_order].ToString();
    }

    public void pw_order_0()
    {
        pw_order = 0;
    }
    public void pw_order_1()
    {
        pw_order = 1;
    }
    public void pw_order_2()
    {
        pw_order = 2;
    }
    public void pw_order_3()
    {
        pw_order = 3;
    }

    public void Retry()
    {
        SceneManager.LoadScene("Title_Scene");
    }
    public void CheckLive(){
        die=true;
        audioSource[0].Stop();
        audioSource[3].clip=clips[5];
        audioSource[3].volume=1.0f;
        audioSource[3].Play();
        this.gameObject.GetComponent<Animator>().SetTrigger("Die");
        Canvas.SetActive(true);
        Gameover.SetActive(true);
    }
}
