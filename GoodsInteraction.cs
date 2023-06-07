using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

using System.Text.RegularExpressions;

public class GoodsInteraction : MonoBehaviour
{   //ϵͳUI
    [HideInInspector]
    public GameObject KeyInfo;
    //�Ի�UI
    [HideInInspector]
    public GameObject EkeyInfo;
    //����
    AudioSource Esound;

    private AudioClip audio;
    private string goodsName;
    public bool dragEnable ;
    private float distance = 5;
    //����
   
    //��ɫ���������֪�����Ĭ�ϲ�֪����
    public bool Knowed = false;
    //��ק����

    void OnMouseDrag()
    {
        if (dragEnable) {  
            Vector3 mousePosition = new Vector3(Input.mousePosition.x, Input.mousePosition.y, distance);
        
            Vector3 objectPosition = Camera.main.ScreenToWorldPoint(mousePosition);
        
            transform.position = objectPosition;
            PlayerPrefs.SetInt("dragTest", 1);
        }
       

    }

    //������������Χ
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
        Debug.Log("�뿪��Χ");
        KeyInfo.SetActive(false);
        EkeyInfo.SetActive(false);
    }
   
    // Start is called before the first frame update
    void Awake()
    {   //����UIѰ��
        
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
        //��ɫ֪���������
        if (Knowed)
        {
            if (KeyInfo.activeSelf && Input.GetKeyDown(KeyCode.E))
            {
                
                KeyInfo.SetActive(false);
                EkeyInfo.SetActive(true);
            }
        }
        //��ɫ��֪��
        else
        {
            if (KeyInfo.activeSelf && Input.GetKeyDown(KeyCode.E))
            {   
                // ��Ч����
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
