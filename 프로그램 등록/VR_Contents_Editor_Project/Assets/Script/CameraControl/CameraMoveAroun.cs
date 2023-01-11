using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMoveAroun : MonoBehaviour
{

    /**
   * date 2018.07.12
   * author Lugub
   * desc
   *  키보드 "S" "W" 를 누르면 각각
   *  카메라가 뒤로, 앞으로 움직이고
   *  키보드 "A" "D" 를 누르면 각각
   *  카메라가 왼쪽, 오른쪽으로 회전하고
   *  "Q" "E"를 누르면 각각 위로 아래로 움직인다.
   */

    [Header("Component")]
    public Transform CameraTransform; //카메라 Transform

    [Header("Variable")]
    //public float _moveSpeed = 10f;
    public bool _cameraAroun = true;
    public GameObject _SpriteObject;
    public bool _state; //마우스를 통해 카메라를 움직일 수 있도록 하는 변수
    public float _mouseSensitivity; //카메라 민감도
    public float _mouseRotateSpeed; //카메라 회전 속도
    //float _zoomMinBound;
    //float _zoomMaxBound;


    [Header("CameraView")]
    /* 카메라 시점 변환용 */
    public GameObject _smallSchedulerParent;
    int _cameraViewIndex; // -1 : 전체 시점 , 0 ~ 사람수 : 해당 인원의 시점

    /* 카메라 시점 변환할 인덱스 설정 */
    public int _disableIndex;
    public int _enableIndex = -1;

    //bool cameraMoveswi = false;
    //float cameraMoveSpeed = 50f;
    //Vector3 cameraMovevec;

    [Header("ClickedItemCanvas")]
    public ClickedItemControl _clickedItemControl;

    ///*_defaultPosition은 나중에 카메라 시점변경 할 때 사용. 원래의 자리로 돌아오게 하기 위해서 */
    //private Vector3 _defulatPosition;

    /* 다른 스크립트를 통해 Camera Move를 제어해야 하므로, Static으로 선언. */

    //private Vector3 _originPlace;
    //private Vector3 _zero = new Vector3(0, 0, 0);

    public void Awake()
    {
        CameraTransform = GetComponent<Transform>(); //현재 Transform 담음
        Static.STATIC.cameraMoveAroun = this;
        //_defulatPosition = this.gameObject.transform.position;
        //_originPlace = _zero;
        _mouseSensitivity = 45f;
        //_zoomMaxBound = 179.9f;
        //_zoomMinBound = 0.1f;
        _mouseRotateSpeed = 30f;
    }

    public void Update()
    {
        if (_cameraAroun)
        {
            if (!_state && Input.GetKeyDown(KeyCode.Q))
            {
                _state = true;
            }
            if (_state && Input.GetKeyDown(KeyCode.W))
            {
                _state = false;
            }
            if (_state)
            {
                EditMouseControl();
            }
            /* 특정 키 (현 : Keypad 0)를 누르면 카메라 시점이 변환되도록! */
            if (Input.GetKeyDown(KeyCode.Space)) ChangeCameraNumber();
            if (Input.GetKeyDown(KeyCode.Keypad0)) ChangeCameraNumber(0);
            if (Input.GetKeyDown(KeyCode.Keypad1)) ChangeCameraNumber(1);
            if (Input.GetKeyDown(KeyCode.Keypad2)) ChangeCameraNumber(2);
            if (Input.GetKeyDown(KeyCode.Keypad3)) ChangeCameraNumber(3);
            if (Input.GetKeyDown(KeyCode.Keypad4)) ChangeCameraNumber(4);
            if (Input.GetKeyDown(KeyCode.Keypad5)) ChangeCameraNumber(5);
            if (Input.GetKeyDown(KeyCode.Keypad6)) ChangeCameraNumber(6);
            if (Input.GetKeyDown(KeyCode.Keypad7)) ChangeCameraNumber(7);
            if (Input.GetKeyDown(KeyCode.Keypad8)) ChangeCameraNumber(8);
            if (Input.GetKeyDown(KeyCode.Keypad9)) ChangeCameraNumber(9);
        }
        /* 마우스 클릭을 통해 화면을 빠져나오면, 카메라 이동 할 수 있도록 */
        else { if (Input.GetMouseButtonDown(0)) { _cameraAroun = true; } }

        /* 객체 클릭시 생기는 Sprite 이미지가 카메라 이동시 따라가도록! */
        MoveSprite();

        //if (cameraMoveswi)
        //    CameraMove();

    }

    void MoveSprite()
    {

        if (_clickedItemControl._clickedItem != null && _clickedItemControl._clickedItem.item3d != null)
        {
            /* 해당 객체의 위치를 저장 */
            Vector3 _itemPosition = _clickedItemControl._clickedItem.item3d.transform.position;

            /* 인물 객체인 경우, 스프라이트 이미지가 사람 머리 위에 존재하도록 위치 수정. */
            if (_clickedItemControl._clickedItem._originNumber >= 2000 && _clickedItemControl._clickedItem._originNumber < 3000)
            {
                _itemPosition.y += 15;
            }

            /* 스프라이트 이미지가, 카메라에 따라 이동하도록 Update! */
            _SpriteObject.transform.position = Camera.main.WorldToScreenPoint(_itemPosition);
        }
    }

    ///* 카메라의 줌 인 작업을 할 때 사용*/
    //public void CameraZoomIn(Vector3 _itemCurrentPosition)
    //{
    //    if (_originPlace == _zero)
    //    {
    //        _originPlace = transform.position;
    //    }

    //    _itemCurrentPosition.x += 3f;
    //    _itemCurrentPosition.y += 15f;
    //    _itemCurrentPosition.z -= 25f;

    //    cameraMovevec = _itemCurrentPosition;
    //    cameraMoveswi = true;// Debug.Log("Camera " + transform.position); 
    //}

    ///* 카메라의 줌 아웃 작업을 할 때 사용*/
    //public void CameraZoomOut()
    //{
    //    if (_originPlace != _zero)
    //    {
    //        cameraMovevec = _originPlace;
    //        cameraMoveswi = true;

    //        _originPlace = _zero;
    //    }
    //}

    //public void CameraZoomChange(Vector3 _itemCurrentPosition)
    //{
    //    _itemCurrentPosition.x += 3f;
    //    _itemCurrentPosition.y += 15f;
    //    _itemCurrentPosition.z -= 25f;

    //    cameraMovevec = _itemCurrentPosition;
    //    cameraMoveswi = true;
    //}

    //void CameraMove()
    //{
    //    transform.position = Vector3.MoveTowards(transform.position, cameraMovevec, cameraMoveSpeed++ * Time.deltaTime);

    //    if (transform.position == cameraMovevec)
    //    {
    //        cameraMoveSpeed = 50f;
    //        cameraMoveswi = false;
    //    }
    //}

    public void EditMouseControl()
    {
        float x = Input.GetAxis("Mouse X"); //마우스의 X축 값 담음
        float y = Input.GetAxis("Mouse Y"); //마우스의 Y축 값 담음
        float scroll = Input.GetAxis("Mouse ScrollWheel"); //마우스의 휠 값을 담음

        if (Input.GetMouseButton(0)) //왼쪽 마우스 클릭하면
        {
            CameraTransform.Translate(-x * Time.deltaTime * _mouseSensitivity, -y * Time.deltaTime * _mouseSensitivity, 0f); //이동
        }
        if (Input.GetMouseButton(1))
        {
            CameraTransform.rotation = Quaternion.Euler(CameraTransform.eulerAngles.x - y * Time.deltaTime * _mouseRotateSpeed, CameraTransform.eulerAngles.y + x * Time.deltaTime * _mouseRotateSpeed, 0f);
        }

        CameraTransform.Translate(0f, 0f, scroll * Time.deltaTime * _mouseSensitivity * 10f); 
    }

    /**
   * date 2019.05.06
   * author INHO
   * 특정 버튼을 누름에 따라 카메라 시점이 변환 될 수 있도록!
     * */
    public void ChangeCameraNumber()
    {
        int peopleCnt = _smallSchedulerParent.transform.childCount;
        if (peopleCnt == 0) return; // 0으로 나눌수 없도록 예외처리

        /* -1 : 전체시점 , 0 ~ PeopleCnt : 해당 인원 시점 */
        _disableIndex = (_cameraViewIndex++ % (peopleCnt + 1)) - 1;
        _enableIndex = (_cameraViewIndex % (peopleCnt + 1)) - 1;

        OnEnableCamera(_enableIndex);
        OnDisableCamera(_disableIndex);
    }

    /* 키패드 입력으로, 시점이 변환될 수 있도록 -> 오버로딩 형식 */
    public void ChangeCameraNumber(int keypad)
    {
        if (_smallSchedulerParent.transform.childCount < keypad) return;
        keypad--;

        OnDisableCamera(_enableIndex);
        OnEnableCamera(keypad);

        _enableIndex = keypad;
    }

    /* 어떤 카메라를 Enable 시킬지? */
    public void OnEnableCamera(int index)
    {
        if (index == -1) this.transform.GetChild(0).transform.GetComponent<Camera>().enabled = true;
        else
        {
            GameObject ableObject = _smallSchedulerParent.transform.GetChild(index).GetComponent<SmallSchedulerBar>()._object.item3d;
            ableObject.transform.Find("Camera").GetComponent<Camera>().enabled = true;
        }
    }

    /* 어떤 카메라를 Disable 시킬지? */
    public void OnDisableCamera(int index)
    {
        if (index == -1) this.transform.GetChild(0).transform.GetComponent<Camera>().enabled = false;
        else
        {
            GameObject disableObject = _smallSchedulerParent.transform.GetChild(index).GetComponent<SmallSchedulerBar>()._object.item3d;
            disableObject.transform.Find("Camera").GetComponent<Camera>().enabled = false;
        }
    }
}