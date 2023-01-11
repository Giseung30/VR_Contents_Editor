using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SmallAniBar : MonoBehaviour
{

    [Header("Animation Info")]
    public int _layerNumber; //레이어 번호
    public bool _actionOrFace; //true면 단일행동 false면 페이스
    public Item _item; //해당 표정, 단일 행동을 하는 객체의본체
    public Animator _animator; //애니메이션 담당 하는 컴포넌트
    public string _animationName; //애니메이션 이름

    [Header("Animation Play Info")]
    public GameObject _thisAniBar; //현재 애니메이션 바
    public float _aniBarWidth; //애니메이션 바의 가로 길이
    public TimeController _timeScript; //시간변수가 존재하는 스크립트
    public double _time; //현재 시간
    public double _startTime = 0; //시작 시간(초단위)
    public double _finishTime = 10; //끝나는 시간(초단위)
    public double _playTime; //총 플레이 시간(초단위) = 끝나는시간 - 시작시간
    public bool _moveCheck; //이동하는 애니메이션인가
    public bool _rotation;
    public Vector3 _startLocation; //시작 위치
    public Vector3 _arriveLocation; // 도착 위치 //바생성시 값 정해짐
    public bool _playSw = false; //애니메이션 실행 여부
    private float _currentXPos; // 현재 x좌표 저장하는 변수
    public bool _voice = false;//보이스 인지 아닌지
    public AudioClip _audio;
    public int _voiceGender;

    public GameObject _bigAniBar;
    public RectTransform _animationRectTransform;
    public bool _once = true;
    public bool _once2 = true;

    [Header("Face Info")]
    public SkinnedMeshRenderer _skin;
    public float[] _face = new float[4];

    public GameObject _animationBar;
    public Transform _toe; //앉거나 일어서는 애니메이션 실행 시 주축이 되는 발가락 객체
    public Vector3 _initialToePosition; //최초의 발가락 위치
    public bool _checkToe = false; //애니메이션을 벗어났을 때 주축이 중심이 되는지 확인
    public bool _checkHead = false;

    void Start()
    {
        _animationRectTransform = _bigAniBar.GetComponent<RectTransform>();

        _thisAniBar = this.gameObject;
        //_thisAniBar.transform.localPosition = new Vector3(0, 0, 0); //부모를 지정해둔 뒤 위치를 새로 지정
        _timeScript = GameObject.Find("Controllers").transform.Find("SchedulerController").GetComponent<TimeController>();
        _time = _timeScript._time;

        InfoUpdate();

        _toe = _item.item3d.transform.GetChild(0).GetChild(1).GetChild(0).GetChild(0).GetChild(0).GetChild(0).GetChild(0);
    }

    void SmallAniBarWidth()
    {
        this.gameObject.transform.GetComponent<RectTransform>().sizeDelta = new Vector2(_animationRectTransform.rect.width, this.gameObject.transform.GetComponent<RectTransform>().rect.height);
        this.gameObject.transform.position = new Vector2(_bigAniBar.transform.position.x, this.gameObject.transform.position.y);
    }

    void InfoUpdate()
    {
        _aniBarWidth = _thisAniBar.transform.GetComponent<RectTransform>().rect.width;
        _time = _timeScript._time;
        //_bigAniBar.transform.GetComponent<BigAniBar>().InfoUpdate();
        _startTime = (_thisAniBar.transform.localPosition.x - _aniBarWidth / 2 - (-894.3f)) * 210 / 1790;
        _finishTime = (_thisAniBar.transform.localPosition.x + _aniBarWidth / 2 - (-894.3f)) * 210 / 1790;
        _playTime = _finishTime - _startTime;

    }

    public void SmallAniBarPosition()
    {
        _thisAniBar.transform.position = new Vector3(_bigAniBar.transform.position.x, _thisAniBar.transform.position.y, _bigAniBar.transform.position.z);
    }

    // Update is called once per frame
    void Update()
    {
        InfoUpdate();
        float _width = _bigAniBar.transform.GetComponent<RectTransform>().rect.width;
        this.gameObject.transform.GetComponent<RectTransform>().sizeDelta = new Vector2(_width, this.transform.GetComponent<RectTransform>().rect.height);
        if (_bigAniBar.active) this.transform.position = new Vector3(_bigAniBar.transform.position.x, this.transform.position.y, this.transform.position.z);
        if (_timeScript._repeatCal) _once = true;
        if (!_timeScript._sw&&!_voice) _animator.speed = 0;
        else if(_timeScript._sw && !_voice) _animator.speed = 1;
        if (!_timeScript._sw && !_timeScript._drag && !_voice) _animator.speed = 0;
        if (!_timeScript._sw && _timeScript._drag && !_voice) _animator.speed = 1;
        if (_timeScript._sw == false)
        {
            Debug.Log("_timeScript._sw : " + _timeScript._sw);
            _item.item3d.GetComponent<AudioSource>().Pause();
        }
        else
        {
            Debug.Log("_timeScript._sw : " + _timeScript._sw);
            _item.item3d.GetComponent<AudioSource>().UnPause();
        }
        if (Static.STATIC._repeat && _once && _moveCheck)//구간반복 위치 체크
        {
            Debug.Log("Start Circulate...");
            if (_startTime > (Static.STATIC._repeatleft.transform.localPosition.x - _timeScript._minLine.transform.localPosition.x) * 210 / 1790f)
            {
                Debug.Log("Circulate Case 1");
                int i = 0, type = 0;
                foreach (GameObject go in Static.STATIC._repeatHuman)
                {
                    if (go.gameObject == _item.item3d.transform.parent.gameObject)
                    {
                        if (((Vector3)Static.STATIC._repeatAniBar[i]).x > this.transform.position.x)
                        {
                            Debug.Log("Result Case 1");
                            Static.STATIC._repeatAniBar[i] = this.transform.position;
                            Static.STATIC._repeatHumanPos[i] = _startLocation;
                            Static.STATIC._repeatHumanRotate[i] = _item.item3d.transform.GetComponent<ItemObject>()._humanInitRotation;
                            Static.STATIC._repeatState[i] = 1;
                        }
                        type++;
                        break;
                    }
                    i++;
                }
                if (type == 0)
                {
                    Debug.Log("Result Case 2");
                    Static.STATIC._repeatHuman.Add(_item.item3d.transform.parent.gameObject);
                    Static.STATIC._repeatAniBar.Add(this.transform.position);
                    Static.STATIC._repeatHumanPos.Add(_startLocation);
                    Static.STATIC._repeatHumanRotate.Add(_item.item3d.transform.GetComponent<ItemObject>()._humanInitRotation);
                    Static.STATIC._repeatState.Add(1);
                }
            }
            else if (_arriveLocation != new Vector3(0, 0, 0) && _finishTime < (Static.STATIC._repeatleft.transform.localPosition.x - _timeScript._minLine.transform.localPosition.x) * 210 / 1790f)
            {
                Debug.Log("Circulate Case 2");
                int i = 0, type = 0;
                foreach (GameObject go in Static.STATIC._repeatHuman)
                {
                    if (go.gameObject == _item.item3d.transform.parent.gameObject)
                    {
                        Debug.Log("Result Case 3");
                        if (((Vector3)Static.STATIC._repeatAniBar[i]).x < Static.STATIC._repeatleft.transform.position.x
                                && ((Vector3)Static.STATIC._repeatAniBar[i]).x < this.transform.position.x)
                        {
                            Static.STATIC._repeatAniBar[i] = this.transform.position;
                            Static.STATIC._repeatHumanPos[i] = _arriveLocation;
                            Vector3 v = new Vector3(_arriveLocation.x - _startLocation.x + _arriveLocation.x, _arriveLocation.y, _arriveLocation.z - _startLocation.z + _arriveLocation.z);
                            Static.STATIC._repeatHumanRotate[i] = v;
                            Static.STATIC._repeatState[i] = 0;
                        }
                        type++;
                        break;
                    }
                    i++;
                }
                if (type == 0)
                {
                    Debug.Log("Result Case 4");
                    Debug.Log(_item.item3d.transform.parent.gameObject);
                    Static.STATIC._repeatHuman.Add(_item.item3d.transform.parent.gameObject);
                    Static.STATIC._repeatAniBar.Add(this.transform.position);
                    Static.STATIC._repeatHumanPos.Add(_arriveLocation);
                    Vector3 v = new Vector3(_arriveLocation.x - _startLocation.x + _arriveLocation.x, _arriveLocation.y, _arriveLocation.z - _startLocation.z + _arriveLocation.z);
                    Static.STATIC._repeatHumanRotate.Add(v);
                    Static.STATIC._repeatState.Add(0);
                }
            }
            _once = false;
            Debug.Log("Success Circulating...!");
        }
        if (_actionOrFace) //행동 액션
        {
            if (_playSw) //실행중
            {
                if (_startTime >= _time || _finishTime <= _time) //중지
                {
                    _playSw = false;
                    _animator.SetBool(_animationName, false);
                    _animator.SetLayerWeight(_layerNumber, 0);
                }

                if (_moveCheck||_checkHead)
                    Character_Move_By_Timer();
                _animator.SetLayerWeight(_layerNumber, 1);

                if (_animationName == "SitDown" || _animationName == "StandUP" || _animationName=="Sit") //앉기 혹은 일어서기 애니메이션
                {
                    if (!_checkToe) //처음이면
                    {
                        _checkToe = true; //검사할 것
                        _initialToePosition = _toe.position;
                        //_initialToePosition = new Vector3(_toe.position.x, 1f, _toe.position.z); //발가락 위치를 담음
                    }
                    /* 지속적으로 위치 변경 */
                    Vector3 _item3dPosition = _item.item3d.transform.position;
                    _item.item3d.transform.position = new Vector3(_item3dPosition.x + (_initialToePosition.x - _toe.position.x), _item3dPosition.y + (_initialToePosition.y - _toe.position.y), _item3dPosition.z + (_initialToePosition.z - _toe.position.z));
                }
            }
            else //실행중이 아님
            {
                /* 실행중이 아닐때 시작위치 재설정 */
                if (_startTime - _time < 0.02 && _startTime - _time > 0)
                {
                    _startLocation = _item.item3d.transform.position;
                }
                if (_startTime - _time > 0)
                {
                    _startLocation = _item.item3d.transform.position;
                }
                if (_startTime < _time && _finishTime > _time) //실행해야함
                {
                    _item.item3d.GetComponent<Animator>().applyRootMotion = false;
                    _playSw = true;
                    //if (!_checkHead)
                    //{
                        _animator.SetBool(_animationName, true);
                        _animator.SetLayerWeight(_layerNumber, 1);
                    //}
                    //애니메이션을 시행할때 해당 방향을 쳐다보게 만듬
                    //if (_moveCheck) _item.item3d.transform.LookAt(new Vector3(_arriveLocation.x + _startLocation.x, _item.item3d.transform.localRotation.y + _startLocation.y, _arriveLocation.z + _startLocation.z));
                    if (_animationName == "Look")
                    {
                        Vector3 _forward = _item.item3d.transform.forward; //현재 객체가 바라보고 있는 방향
                        Vector3 _dir = _arriveLocation - _item.item3d.transform.parent.transform.position; //객체가 바라봐야할 방향
                        _dir = _dir.normalized;
                        float _acos = (_forward.x * _dir.x + _forward.z * _dir.z) / (Mathf.Sqrt(_forward.x * _forward.x + _forward.z * _forward.z) * Mathf.Sqrt(_dir.x * _dir.x + _dir.z * _dir.z));
                        _acos = _acos / 2;
                        Vector3 _left = _item.item3d.transform.right * -1;
                        float _inner = _left.x * _dir.x + _left.z * _dir.z;
                        float _angle = (_inner / (Mathf.Sqrt(_left.x * _left.x + _left.z * _left.z) * Mathf.Sqrt(_dir.x * _dir.x + _dir.z * _dir.z)));

                        Debug.Log("_acos : " + _acos + ",dir : " + _dir);

                        if (_angle < 0)
                        {  //left와 dir 사이의 각이 90를 넘어가면 오른쪽 방향
                            float _dif = 0.5f - _acos;
                            _acos = 0.5f + _dif;
                        }
                        _animator.SetFloat("Blend", _acos);

                    }
                }

                if (_checkToe) //발가락 검사
                {
                    if (_item.item3d.transform.position.y > _initialToePosition.y - 0.05f && _item.item3d.transform.position.y < _initialToePosition.y + 0.05f) //객체가 정상적으로 돌아오면
                    {
                        _checkToe = false; //검사 해제
                    }
                    else //객체가 정상 위치로 돌아오지 않으면
                    {
                        /* 지속적으로 위치 변경 */
                        Vector3 _item3dPosition = _item.item3d.transform.position;
                        _item.item3d.transform.position = new Vector3(_item3dPosition.x + (_initialToePosition.x - _toe.position.x), _item3dPosition.y + (_initialToePosition.y - _toe.position.y), _item3dPosition.z + (_initialToePosition.z - _toe.position.z));
                    }
                }
            }
        }
        else
        {
            if (_voice)//보이스
            {
                if (_playSw)
                {
                    if (_startTime >= _time || _finishTime <= _time)
                    {
                        Debug.Log("voiceEnd");
                        _playSw = false;
                        _item.item3d.GetComponent<AudioSource>().Stop();
                        _item.item3d.GetComponent<AudioSource>().clip = null;
                    }
                }
                else//보이스 실행 X
                {
                    if (_startTime < _time && _finishTime > _time)
                    {
                        _playSw = true;
                        _item.item3d.GetComponent<AudioSource>().clip = _audio;
                        Debug.Log("voiceStart");
                        _item.item3d.GetComponent<AudioSource>().Play();
                    }
                }
            }
            else // 표정
            {
                if (_playSw) //애니메이션이 실행중임
                {
                    if (_startTime >= _time || _finishTime <= _time)
                    {
                        _playSw = false; //실행시간이 아님 고로 종료
                        FaceInit();
                    }
                }
                else //애니메이션이 실행중이지 않음
                {
                    if (_startTime < _time && _finishTime > _time)
                    {
                        _playSw = true;
                        FaceMove();
                    }
                }
            }
        }
        //현재 애니메이션 실행 중이 아닌 경우 layerWeight를 0으로 만듬.
        if (!_voice&&_finishTime <= _time && _animator.GetCurrentAnimatorStateInfo(_layerNumber).IsName("Idle"))
        {
            if (_layerNumber != 0) _animator.SetLayerWeight(_layerNumber, 0);
        }
        
    }

    void Character_Move_By_Timer()
    {
        if (!_rotation)
        {
            float val = (float)((_time - _startTime) / (_playTime));

            if (_moveCheck)
            {
                _item.item3d.transform.parent.transform.position =
                    new Vector3(
                        (val * _arriveLocation.x + (1 - val) * _startLocation.x)
                        , (val * _arriveLocation.y + (1 - val) * _startLocation.y)
                        , (val * _arriveLocation.z + (1 - val) * _startLocation.z));
                if (_time > _startTime && _time < _finishTime)
                {
                    if (_once2)
                    {
                        _item.item3d.transform.LookAt(new Vector3(_arriveLocation.x, _arriveLocation.y, _arriveLocation.z));
                        if (_timeScript._sw || _timeScript._drag) _once2 = false;
                    }
                }
                else _once2 = true;
            }
            if(_checkHead)
            {
                Debug.Log("head Move");
                GameObject _head = _item.item3d.transform.GetChild(0).GetChild(1).GetChild(0).GetChild(2).GetChild(0).GetChild(0).GetChild(0).GetChild(0).gameObject;
                Debug.Log(_head.name);
                Debug.Log(_head.transform.rotation);
                Vector3 dir = _arriveLocation - _item.item3d.transform.position;

                _head.transform.rotation = Quaternion.Lerp(_head.transform.rotation, Quaternion.LookRotation(dir), val * Time.deltaTime);
            }
        }
    }

    void FaceInit() //얼굴 표정 초기화
    {
        for (int i = 0; i < 4; i++)
        {
            _item.item3d.transform.Find("Body").GetComponent<SkinnedMeshRenderer>().SetBlendShapeWeight(i, 0);
        }

    }

    void FaceMove() //얼굴 표정 만들기
    {
        for (int i = 0; i < 4; i++)
        {
            _item.item3d.transform.Find("Body").GetComponent<SkinnedMeshRenderer>().SetBlendShapeWeight(i, _face[i]);
        }
    }
}