using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;
using System;

public class SearchInputField : MonoBehaviour {
    /*
* date 2018.08.16
* author INHO
* desc
* Item Menu에서, Item 또는 Human의 Slot을 검색해 줄 수 있도록
* 지원해주는 Script !
*/

    public InputField _inputField;

    [Header("Script")]
    public CameraMoveAroun _cameraMoveAroundSwi;

    [Header("Content")]
    public GameObject _itemContent;
    public GameObject _parent;

    private int _slotCount;

    private void Start()
    {
        _cameraMoveAroundSwi = Static.STATIC.cameraMoveAroun;
    }

    // Update is called once per frame
    void Update () {

        /* Input Field 에 커서가 깜빡이면, W,A,S,D 를 눌러도 카메라 이동 안하도록 */
        if (_inputField.isFocused)
        {
            _cameraMoveAroundSwi._cameraAroun = false;

            /* 동적으로 생성되는 Slot들의 부모 (묶어주는 GameObject) */
            _parent = _itemContent.transform.Find("Viewport/Item Content").gameObject;

            /* 슬롯의 갯수가 몇 개인지? -> 해당 갯수만큼 for 문 반복조사 */
            _slotCount = _parent.transform.childCount;
        }

	}

    /* 입력받은 Text를 Script에 저장하고 동작시켜주는 Method */
    public void OnSearchSlotName(InputField _inputField)
    {

        /* 현재 켜져 있는 Menu UI에 따라 실시간으로 search 결과를 보여줌. */
        MenuStatus(_inputField);

    }

    /* 해당 Method를 통해 Search 해 줄 Slot 종류가 Human? Item? Place? 를 판별. */
    public void MenuStatus(InputField inputField)
    {

        for (int i = 1; i < _slotCount; i++)
        {
            /* ItemName.Length 보다 Search.Length 가 클 수 있으므로, try-catch 문으로 잡아준다. */
            try
            {
                /* 검색한 Text에 조건이 맞을 시 */
                if (inputField.text.ToLower().Substring(0, inputField.text.Length) == _parent.transform.GetChild(i).name.ToLower().Substring(0, inputField.text.Length))
                {
                    /* Active(true) 설정 */
                    _parent.transform.GetChild(i).gameObject.SetActive(true);
                }
                else
                {
                    /* Active(false) 설정 */
                    _parent.transform.GetChild(i).gameObject.SetActive(false);
                }
            }
            catch (Exception e)
            {
                //_parent.transform.GetChild(i + 1).gameObject.SetActive(false);
            }
        }

    }
}
