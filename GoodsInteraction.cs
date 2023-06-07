using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

using System.Text.RegularExpressions;

public class GoodsInteraction : MonoBehaviour
{   //系统UI
    [HideInInspector]
    public GameObject KeyInfo;
    //对话UI
    [HideInInspector]
    public GameObject EkeyInfo;
    //声音
    AudioSource Esound;

    private AudioClip audio;
    private string goodsName;
    public bool dragEnable ;
    private float distance = 5;
    //正则
   
    //角色对物体的熟知情况，默认不知道。
    public bool Knowed = false;
    //拖拽动作

    void OnMouseDrag()
    {
        if (dragEnable) {  
            Vector3 mousePosition = new Vector3(Input.mousePosition.x, Input.mousePosition.y, distance);
        
            Vector3 objectPosition = Camera.main.ScreenToWorldPoint(mousePosition);
        
            transform.position = objectPosition;
            PlayerPrefs.SetInt("dragTest", 1);
        }
       

    }

    //触发触发器范围
    private void OnTriggerEnter(Collider other) 
    {
        
        if (other.name == "Player")
        {
            PlayerPrefs.SetString("EName", this.name);
            
           
            KeyInfo.SetActive(true);
            
        }
        else if(other.name == "Roobot")
        { 
            
        }
        
        
    }
    private void OnTriggerExit(Collider other)
    {
        Debug.Log("离开范围");
        KeyInfo.SetActive(false);
        EkeyInfo.SetActive(false);
    }
   
    // Start is called before the first frame update
    void Awake()
    {   //隐藏UI寻找
        
        GameObject ParentObjectUI = GameObject.Find("UI");
        if (ParentObjectUI != null)
        {
            KeyInfo = ParentObjectUI.transform.Find("SystemUI").gameObject;
            EkeyInfo = ParentObjectUI.transform.Find("EventUI").gameObject;
        }
        else
        {
            Debug.Log("wrong");
        }
        
        Esound = GameObject.Find("EffectsSource").GetComponent<AudioSource>();
        /*EffectSound = transform.Find("EffectsSource").GetComponent<AudioSource>();*/
        
        
    }



    // Update is called once per frame
    void Update()

    {

       

        EClick();

        
        
        
    }

    public void EClick()
    {
        //角色知道这个物体
        if (Knowed)
        {
            if (KeyInfo.activeSelf && Input.GetKeyDown(KeyCode.E))
            {
                
                KeyInfo.SetActive(false);
                EkeyInfo.SetActive(true);
            }
        }
        //角色不知道
        else
        {
            if (KeyInfo.activeSelf && Input.GetKeyDown(KeyCode.E))
            {   
                // 声效触发
                string audioPath = PlayerPrefs.GetString("EName");
                audio = (AudioClip)Resources.Load("Audio/"+audioPath, typeof(AudioClip));
                
                Esound.clip = audio;
                Esound.Play();
                
                KeyInfo.SetActive(false);
                EkeyInfo.SetActive(true);


                PlayerPrefs.SetInt("eEventTest", 1);


            }


        }
    }
}
