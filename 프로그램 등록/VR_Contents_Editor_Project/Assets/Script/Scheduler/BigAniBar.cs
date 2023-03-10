using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;


public class BigAniBar : MonoBehaviour, IBeginDragHandler, IEndDragHandler, IDragHandler, IPointerClickHandler
{

    private float _currentXPos; // 현재 x좌표 저장하는 변수

    [Header("Animation Info")]
    public GameObject _thisAniBar;//애니메이션 바
    public float _aniBarWidth; //애니메이션 바의 가로 길이
    public GameObject _smallAniBar; // 스몰애니바
    public string _animationName; //애니메이션 이름
    public double _startTime = 0; //시작 시간(초단위)
    public double _finishTime = 10; //끝나는 시간(초단위)
    public double _playTime; //총 플레이 시간(초단위) = 끝나는시간 - 시작시간
    public double _time; //현재 시간
    public TimeController _timeScript; //시간변수가 존재하는 스크립트

    //--------------------------------------------------------------------------------------------------------------
    [Header("Detail Content")]
    public GameObject _detailContent; //설명을 띄워주는 화면
    public Text _detailTime; //화면에서 설명(시간)
    public GameObject _aniBarSubMenu;

    // Use this for initialization
    void Start()
    {
        _thisAniBar = this.gameObject;

        _timeScript = GameObject.Find("Controllers").transform.Find("SchedulerController").GetComponent<TimeController>();
        _time = _timeScript._time;

        _detailContent = GameObject.Find("Canvas/ItemMenuCanvas/SchedulerMenu/ContentDetail") as GameObject;
        _detailTime = _detailContent.transform.GetChild(0).GetComponent<Text>();
        _aniBarSubMenu = GameObject.Find("Canvas/ItemMenuCanvas/AniBarSubMenu") as GameObject;
        /*
        * date   2019.03.04
        * author Skyde47
        * desc
        * 애니메이션 바 생성할때 바 위에 텍스트가 뜨게 만듬
*       */

        InfoUpdate();
    }

    // Update is called once per frame
    void Update()
    {
        InfoUpdate();
    }

    public void InfoUpdate()
    {
        if (this.gameObject.transform.parent.parent.gameObject.activeSelf)
        {
            _aniBarWidth = _thisAniBar.transform.GetComponent<RectTransform>().rect.width;
            _startTime = (_thisAniBar.transform.localPosition.x - _aniBarWidth / 2 - (-894.3f)) * 210 / 1790;
            _finishTime = (_thisAniBar.transform.localPosition.x + _aniBarWidth / 2 - (-894.3f)) * 210 / 1790;
            _playTime = _finishTime - _startTime;
            _time = _timeScript._time;
        }
    }

    public void OnMouseEnter() //마우스를 올리면 애니메이션 시간 표시
    {
        DetailContentUpdate();
        _detailContent.SetActive(true);
        _detailContent.transform.position = new Vector2(this.gameObject.transform.position.x, this.gameObject.transform.position.y + 40);
    }
    public void OnMouseExit() //마우스를 내리면 애니메이션 시간 표시X
    {
        _detailContent.SetActive(false);
    }
    public void OnPointerClick(PointerEventData eventData)
    {
        if (eventData.button == PointerEventData.InputButton.Right)
        {
            Static.STATIC._clickAniBar = this.gameObject;
            _aniBarSubMenu.SetActive(true);
            _aniBarSubMenu.transform.position = new Vector3(this.transform.position.x + 40, this.transform.position.y + 50, this.transform.position.z);
            _aniBarSubMenu.transform.GetChild(0).transform.GetChild(2).GetComponent<AniBarSubMenu>()._bigAniBar = this.gameObject;
            _aniBarSubMenu.transform.GetChild(0).transform.GetChild(2).GetComponent<AniBarSubMenu>()._smallAniBar = _smallAniBar;
        }
    }
    //드래그시작할때
    public void OnBeginDrag(PointerEventData eventData)
    {
        HistoryController.pushAniBarHist(this.gameObject, this.transform.localPosition, this.GetComponent<RectTransform>().rect.width, _smallAniBar.GetComponent<SmallAniBar>()._item._objectNumber, _smallAniBar.GetComponent<SmallAniBar>()._animationName, _smallAniBar.GetComponent<SmallAniBar>()._voice);
        _detailContent.transform.position = new Vector2(this.gameObject.transform.position.x, this.gameObject.transform.position.y + 40);
        DetailContentUpdate();
        _currentXPos = Input.mousePosition.x;   // x축만 이동할 것이기 때문에 x축만 사용
    }
    //드래그중
    public void OnDrag(PointerEventData eventData)
    {
        _detailContent.transform.position = new Vector2(this.gameObject.transform.position.x, this.gameObject.transform.position.y + 40);
        DetailContentUpdate();

        float _moveX = Input.mousePosition.x - _currentXPos;
        _thisAniBar.transform.Translate(new Vector3(_moveX, 0, 0)); // 마우스의 이동만큼 드래그바이동

        _currentXPos = Input.mousePosition.x; // 다음 드래그때 이동 할 거리를 알기 위해 다시 현재 위치를 구한다.
        if ((_thisAniBar.transform.localPosition.x - _aniBarWidth / 2) - 1 < (-894.3f))
        {
            _thisAniBar.transform.localPosition = new Vector3(-893.3f + _aniBarWidth / 2, this.gameObject.transform.localPosition.y, this.gameObject.transform.localPosition.z);
        }
        else if ((_thisAniBar.transform.localPosition.x + _aniBarWidth / 2) > 895.7f)
        {
            _thisAniBar.transform.localPosition = new Vector3(895.7f - _aniBarWidth / 2, this.gameObject.transform.localPosition.y, this.gameObject.transform.localPosition.z);
        }

        _smallAniBar.GetComponent<SmallAniBar>().SmallAniBarPosition();
    }

    //드래그종료
    public void OnEndDrag(PointerEventData eventData)
    {

    }

    public void DetailContentUpdate() //시간 표시 내용 업데이트
    {
        string _timestr;
        _timestr = _startTime.ToString("00.00"); //00.00형식으로 변환해서 저장
        string[] _timearr = _timestr.Split('.'); // '.'을 기준으로 문자열 나눔
        int _timei = System.Convert.ToInt32(_timearr[0]); // 1초이상의 숫자를 int로 변환
        double _m = (_timei % 3600) / 60; //분 계산
        double _s = (_timei % 3600) % 60; //초 계산
        string _startStr = _m.ToString("00") + ":" + _s.ToString("00") + "." + _timearr[1];

        _timestr = _finishTime.ToString("00.00"); //00.00형식으로 변환해서 저장
        _timearr = _timestr.Split('.'); // '.'을 기준으로 문자열 나눔
        _timei = System.Convert.ToInt32(_timearr[0]); // 1초이상의 숫자를 int로 변환
        _m = (_timei % 3600) / 60; //분 계산
        _s = (_timei % 3600) % 60; //초 계산
        string _finishStr = _m.ToString("00") + ":" + _s.ToString("00") + "." + _timearr[1];

        _detailTime.text = _startStr + "~" + _finishStr;
    }


}
