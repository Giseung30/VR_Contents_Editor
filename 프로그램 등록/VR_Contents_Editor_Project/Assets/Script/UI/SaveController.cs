using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.UI;

public class SaveController : MonoBehaviour
{

    public GameObject _samepleButton;

    HiddenMenuControl _hiddenMenuControl;

    /*
     * GameDBController 에서 playedDic과 keyDic에 값을 넘겨줌
     */
    public Dictionary<string, int> playedDic = new Dictionary<string, int>();
    public Dictionary<string, int> keyDic = new Dictionary<string, int>();
    public Dictionary<string, string> timeDic = new Dictionary<string, string>();
    public List<string> fileName = new List<string>();

    /**
     * @date : 2018.04.01
     * @author : Won,Junseok
     * @desc : VR DB 초기화
     *         사용하기 전에 반드시 실행 필요   
     *  
     **/
    /**
    * @date : 2019.11.11
    * @author : Day
    * @desc : SavePanel에 DataBase에 저장된 DB파일들에 대한
    *       버튼을 만들어주면서 저장된 값을 불러와주는 함수
    *       HiddenMenuControl에서 TotalTable에 대한 작업을 우선적으로 진행
    *       그 과정에서 얻어온 값들로 진행.
    **/
    IEnumerator Start()
    {
        Debug.Log("SaveController.cs 시작~");
        _hiddenMenuControl = GameObject.Find("HiddenMenu").GetComponent<HiddenMenuControl>();
        /*
         * HiddenMenu에서 TotalTable 설정(?)을 마침.
         */
        yield return StartCoroutine(_hiddenMenuControl.Hidden_Start());

        foreach (string file in fileName)
        {
            //같은 파일 이름으로 key 값이 설정 되있어야함.
            if (playedDic.ContainsKey(file) && keyDic.ContainsKey(file))
            {
                GameObject tmp = Instantiate(_samepleButton) as GameObject;

                tmp.name = file;    //실행 횟수를 추가하기 위해 GameObject를 검색해야함.
                tmp.transform.GetChild(0).GetComponent<Text>().text = "<color=#0000ff>" + keyDic[file] + ". </color>" + file + " : <color=#ff0000>" + playedDic[file] + "</color>";
                tmp.transform.GetChild(2).GetComponent<Text>().text = timeDic[file];
                tmp.transform.SetParent(this.gameObject.transform);
                tmp.transform.localScale = new Vector3(0.9f, 0.9f, 1);
            }
        }

    }

}
