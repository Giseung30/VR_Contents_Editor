using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HandSlot : MonoBehaviour
{
    /**
* date 2019.07.11
* author GS
* desc
*  HandItem 슬롯에 동적으로 추가되는 스크립트
*  Slot이나 HumanSlot 스크립트와는 다르게 방안에 배치하는 것이 아니라, 손에 들려지는 것이기 때문에 따로 구분.
*/
    public ClickedItemControl _clickedItemControl;
    public Item _thisItem;
    public bool _isLeft; //왼손이면 true, 오른손이면 false
    private Item _thisHuman;
    private HumanItem _thisHumanItem;
    private GameObject _handObjectClone;
    public Transform _hand; //_leftHand와 _rightHand의 역할 수행
    string _itemName; // 히스토리를 위해 핸드아이템의 이름을 받아옴

    /* 슬롯이 클릭되었을 때 */
    public void OnClickSlot()
    {
        _thisHuman = _clickedItemControl._clickedItem;
        _thisHumanItem = _clickedItemControl._clickedHumanItem;

        Vector3 _pos = new Vector3(1, 0, 0);
        if (_isLeft) //왼손이면
        {
            _hand = _clickedItemControl._clickedItem.item3d.transform.GetChild(0).GetChild(1).GetChild(0).GetChild(2).GetChild(0).GetChild(0).GetChild(1).GetChild(0).GetChild(0).GetChild(0);
        }
        else //오른손이면
        {
            _hand = _clickedItemControl._clickedItem.item3d.transform.GetChild(0).GetChild(1).GetChild(0).GetChild(2).GetChild(0).GetChild(0).GetChild(2).GetChild(0).GetChild(0).GetChild(0);
        }


        if (_isLeft)
        {
            if (_thisHumanItem._leftHandItem != null)
            {
                _itemName = _thisHumanItem._leftHandItem.itemName;
                HistoryController.pushHandHist(_thisHuman._originNumber, _thisHuman._objectNumber, _isLeft, _itemName, _thisHumanItem._leftHandItem);
                if (!_thisHumanItem._leftHandItem.itemName.Equals(_thisItem.itemName))
                {
                    GameObject dObj = _hand.Find(_thisHumanItem._leftHandItem.itemName).gameObject;

                    Destroy(dObj);

                    _thisHumanItem._leftHandItem = _thisItem;
                }
                else
                {
                    GameObject dObj = _hand.Find(_thisHumanItem._leftHandItem.itemName).gameObject;

                    Destroy(dObj);

                    _thisHumanItem._leftHandItem = null;
                    return;
                }
            }
            else
            {
                _itemName = "null";
                HistoryController.pushHandHist(_thisHuman._originNumber, _thisHuman._objectNumber, _isLeft, _itemName, _thisHumanItem._leftHandItem);
                _thisHumanItem._leftHandItem = _thisItem;
            }
            /* 생성 */
            GameObject _handObjectClone = Instantiate(_thisHumanItem._leftHandItem.item3d); //물건 복제
            _handObjectClone.transform.SetParent(_hand); //손을 부모로 설정
            _handObjectClone.name = _thisHumanItem._leftHandItem.itemName; //이름 지정

            
            _handObjectClone.transform.position = _hand.transform.GetChild(2).transform.position; //세번째 손가락 위치로 지정
            
            if (_clickedItemControl._clickedItem._originNumber == 2002) //여자 객체인 경우
            {
                _handObjectClone.transform.GetChild(0).transform.localPosition = _handObjectClone.transform.GetChild(0).GetComponent<HandItem>()._womanLeftPos;
            }
            
            else if (_clickedItemControl._clickedItem._originNumber == 2001)
            {
                _handObjectClone.transform.GetChild(0).transform.localPosition = _handObjectClone.transform.GetChild(0).GetComponent<HandItem>()._manLeftPos;
            }
            
            
            else
            {
                _handObjectClone.transform.GetChild(0).transform.localPosition = _handObjectClone.transform.GetChild(0).GetComponent<HandItem>()._daughterLeftPos;
            }
            
            //_handObjectClone.transform.GetChild(0).transform.localPosition += new Vector3(0, 2, 0) * _handObjectClone.GetComponentInChildren<Collider>().bounds.extents.x; //_handObjectClone 의 위치 미세조정
            _handObjectClone.transform.rotation = _hand.rotation; //회전 각도 지정

        }

        else
        {
            if (_thisHumanItem._rightHandItem != null)
            {
                _itemName = _thisHumanItem._rightHandItem.itemName;
                HistoryController.pushHandHist(_thisHuman._originNumber, _thisHuman._objectNumber, _isLeft, _itemName, _thisHumanItem._rightHandItem);
                if (!_thisHumanItem._rightHandItem.itemName.Equals(_thisItem.itemName))
                {
                    GameObject dObj = _hand.Find(_thisHumanItem._rightHandItem.itemName).gameObject;

                    Destroy(dObj);

                    _thisHumanItem._rightHandItem = _thisItem;
                }
                else
                {
                    GameObject dObj = _hand.Find(_thisHumanItem._rightHandItem.itemName).gameObject;

                    Destroy(dObj);

                    _thisHumanItem._rightHandItem = null;
                    return;
                }
            }
            else
            {
                _itemName = "null";
                HistoryController.pushHandHist(_thisHuman._originNumber, _thisHuman._objectNumber, _isLeft, _itemName, _thisHumanItem._rightHandItem);
                _thisHumanItem._rightHandItem = _thisItem;
            }
            /* 생성 */
            GameObject _handObjectClone = Instantiate(_thisItem.item3d); //물건 복제
            _handObjectClone.transform.SetParent(_hand); //손을 부모로 설정
            _handObjectClone.name = _thisItem.itemName; //이름 지정
            _handObjectClone.transform.position = _hand.transform.GetChild(2).transform.position; //세번째 손가락 위치로 지정

            if (_clickedItemControl._clickedItem._originNumber == 2002) //여자 객체인 경우
            {
                _handObjectClone.transform.GetChild(0).transform.localPosition = _handObjectClone.transform.GetChild(0).GetComponent<HandItem>()._womanRightPos;
            }
            
            else if (_clickedItemControl._clickedItem._originNumber == 2001)
            {
                _handObjectClone.transform.GetChild(0).transform.localPosition = _handObjectClone.transform.GetChild(0).GetComponent<HandItem>()._manRightPos;
            }
            
            
            else
            {
                _handObjectClone.transform.GetChild(0).transform.localPosition = _handObjectClone.transform.GetChild(0).GetComponent<HandItem>()._daughterRightPos;
            }
            
            //_handObjectClone.transform.GetChild(0).transform.localPosition += new Vector3(0, 2, 0) * _handObjectClone.GetComponentInChildren<Collider>().bounds.extents.x; //_handObjectClone 의 위치 미세조정
            _handObjectClone.transform.rotation = _hand.rotation; //회전 각도 지정

        }

    }

}
