using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;

public class HistoryController : MonoBehaviour
{
    //static Stack<HistoryInfo> stck_hist = new Stack<HistoryInfo>();
    LocateItem locateItem;
    ItemListControl itemListControl;
    ClickedPlaceControl clickedPlaceControl;
    static Stack<int> _stackHistZ = new Stack<int>();// 0 애니바P,W 1 오브젝트P,S
    static Stack<int> _stackHistY = new Stack<int>();
    static Stack<AniBarPosAndWidth> _aniBarHist = new Stack<AniBarPosAndWidth>();
    static Stack<AniBarPosAndWidth> _aniBarHistY = new Stack<AniBarPosAndWidth>();
    static Stack<ObjectPosAndScale> _objectHist = new Stack<ObjectPosAndScale>();
    static Stack<ObjectPosAndScale> _objectHistY = new Stack<ObjectPosAndScale>();
    static Stack<AniBarCreate> _aniBarCreateHist = new Stack<AniBarCreate>();
    static Stack<AniBarCreate> _aniBarCreateHistY = new Stack<AniBarCreate>();
    static Stack<ObjectCreate> _objectCreateHist = new Stack<ObjectCreate>();
    static Stack<ObjectCreate> _objectCreateHistY = new Stack<ObjectCreate>();
    static Stack<ObjectDelete> _objectDeleteHist = new Stack<ObjectDelete>();
    static Stack<ObjectDelete> _objectDeleteHistY = new Stack<ObjectDelete>();
    static Stack<HumanDelete> _humanDeleteHist = new Stack<HumanDelete>();
    static Stack<HumanDelete> _humanDeleteHistY = new Stack<HumanDelete>();
    static Stack<AniBarDelete> _aniBarDeleteHist = new Stack<AniBarDelete>();
    static Stack<AniBarDelete> _aniBarDeleteHistY = new Stack<AniBarDelete>();
    static Stack<AniBarVoiceDelete> _aniBarVoiceDeleteHist = new Stack<AniBarVoiceDelete>();
    static Stack<AniBarVoiceDelete> _aniBarVoiceDeleteHistY = new Stack<AniBarVoiceDelete>();
    static Stack<DressChange> _dressChangeHist = new Stack<DressChange>();
    static Stack<DressChange> _dressChangeHistY = new Stack<DressChange>();
    static Stack<TilingChange> _tilingHist = new Stack<TilingChange>();
    static Stack<TilingChange> _tilingHistY = new Stack<TilingChange>();
    static Stack<TileChange> _tileHist = new Stack<TileChange>();
    static Stack<TileChange> _tileHistY = new Stack<TileChange>();
    static Stack<HandChange> _handHist = new Stack<HandChange>();
    static Stack<HandChange> _handHistY = new Stack<HandChange>();

    private void Awake()
    {
        locateItem = GameObject.Find("ItemController").GetComponent<LocateItem>();
        itemListControl = GameObject.Find("ItemController").GetComponent<ItemListControl>();
        clickedPlaceControl = GameObject.Find("ClickedPlaceCanvas").GetComponent<ClickedPlaceControl>();
    }

    void Update()
    {
        //Debug.Log("_stackHistZ : " + _stackHistZ.Count);
        //Debug.Log("_stackHistY : " + _stackHistY.Count);
        /**
         * 키보드 입력 이벤트
         */
        //if (Input.GetKeyDown(KeyCode.LeftControl) && Input.GetKeyDown(KeyCode.Z))
        if (Input.GetKeyDown(KeyCode.Z))
        {
            popHistoryStack();
        }
        if (Input.GetKeyDown(KeyCode.Y))
        {
            popHistoryStackY();
        }
    }

    /**
     * 이력 저장 Z
     */
    #region
    static public void pushAniBarHist(GameObject go, Vector3 pos, float width, int objectNum, string animationName, bool voice)
    {
        _stackHistZ.Push(0);
        _aniBarHist.Push(new AniBarPosAndWidth(go, pos, width, objectNum, animationName, voice));
    }

    static public void pushObjectHist(GameObject go, Vector3 pos, Vector3 scale, int objectNum, int originNum, Quaternion rot)
    {
        _stackHistZ.Push(1);
        _objectHist.Push(new ObjectPosAndScale(go, pos, scale, objectNum, originNum, rot));
    }

    static public void pushAniBarCreateHist(GameObject go, GameObject go1, string str, int objectNum, int voice)
    {
        _stackHistZ.Push(2);
        _aniBarCreateHist.Push(new AniBarCreate(go, go1, str, objectNum, voice));
    }

    static public void pushObjectCreateHist(GameObject go, int originNum, int objectNum)
    {
        _stackHistZ.Push(3);
        _objectCreateHist.Push(new ObjectCreate(go, originNum, objectNum));
    }

    static public void pushObjectDeleteHist(int objectNum, int originNum, string itemName, Vector3 pos, Vector3 rot, Vector3 scale)
    {
        _stackHistZ.Push(4);
        _objectDeleteHist.Push(new ObjectDelete(objectNum, originNum, itemName, pos, rot, scale));
    }

    static public void pushHumanDeleteHist(int originNum, int objectNum, string humanName, Vector3 pos, Vector3 rot, Vector3 scale, List<AniBarDelete> A, List<AniBarVoiceDelete> AA)
    {
        _stackHistZ.Push(5);
        _humanDeleteHist.Push(new HumanDelete(originNum, objectNum, humanName, pos, rot, scale, A, AA));
    }

    static public void pushAniBarDeleteHist(float barX, float barWidth, string animationName, string parentName, string animationText, int moveOrState, int actionOrFace, int layerNumber,
        float arriveX, float arriveY, float arriveZ, int originNumber, int rotation)
    {
        _stackHistZ.Push(6);
        _aniBarDeleteHist.Push(new AniBarDelete(barX, barWidth, animationName, parentName, animationText, moveOrState, actionOrFace, layerNumber, arriveX, arriveY, arriveZ, originNumber, rotation));
    }

    static public void pushAniBarVoiceDeleteHist(float barX, float barWidth, string voiceName, string parentName, int voiceGender, int originNumber)
    {
        _stackHistZ.Push(7);
        _aniBarVoiceDeleteHist.Push(new AniBarVoiceDelete(barX, barWidth, voiceName, parentName, voiceGender, originNumber));
    }

    static public void pushDressChangeHist(string[] name, string prev, string objectName, int objectNum, int originNum)
    {
        _stackHistZ.Push(8);
        _dressChangeHist.Push(new DressChange(name, prev, objectName, originNum, objectNum));
    }

    static public void pushTilingHist(int ori, int obj, Vector3 scale, int tile)
    {
        _stackHistZ.Push(9);
        _tilingHist.Push(new TilingChange(ori, obj, scale, tile));
    }

    static public void pushTileHist(int ori, int obj, int bul)
    {
        _stackHistZ.Push(10);
        _tileHist.Push(new TileChange(ori, obj, bul));
    }

    static public void pushHandHist(int ori, int obj, bool left, string name, Item hand)
    {
        _stackHistZ.Push(11);
        _handHist.Push(new HandChange(ori, obj, left, name, hand));
    }
    /**
     * 이력 저장 Y
     */

    static public void pushAniBarHistY(GameObject go, Vector3 pos, float width, int objectNum, string animationName, bool voice)
    {
        _stackHistY.Push(0);
        _aniBarHistY.Push(new AniBarPosAndWidth(go, pos, width, objectNum, animationName, voice));
    }

    static public void pushObjectHistY(GameObject go, Vector3 pos, Vector3 scale, int objectNum, int originNum, Quaternion rot)
    {
        _stackHistY.Push(1);
        _objectHistY.Push(new ObjectPosAndScale(go, pos, scale, objectNum, originNum, rot));
    }

    static public void pushAniBarCreateHistY(GameObject go, GameObject go1, string str, int objectNum, int voice)
    {
        _stackHistY.Push(2);
        _aniBarCreateHistY.Push(new AniBarCreate(go, go1, str, objectNum, voice));
    }

    static public void pushObjectCreateHistY(GameObject go, int originNum, int objectNum)
    {
        _stackHistY.Push(3);
        _objectCreateHistY.Push(new ObjectCreate(go, originNum, objectNum));
    }

    static public void pushObjectDeleteHistY(int objectNum, int originNum, string itemName, Vector3 pos, Vector3 rot, Vector3 scale)
    {
        _stackHistY.Push(4);
        _objectDeleteHistY.Push(new ObjectDelete(objectNum, originNum, itemName, pos, rot, scale));
    }

    static public void pushHumanDeleteHistY(int originNum, int objectNum, string humanName, Vector3 pos, Vector3 rot, Vector3 scale, List<AniBarDelete> A, List<AniBarVoiceDelete> AA)
    {
        _stackHistY.Push(5);
        _humanDeleteHistY.Push(new HumanDelete(originNum, objectNum, humanName, pos, rot, scale, A, AA));
    }

    static public void pushAniBarDeleteHistY(float barX, float barWidth, string animationName, string parentName, string animationText, int moveOrState, int actionOrFace, int layerNumber,
        float arriveX, float arriveY, float arriveZ, int originNumber, int rotation)
    {
        _stackHistY.Push(6);
        _aniBarDeleteHistY.Push(new AniBarDelete(barX, barWidth, animationName, parentName, animationText, moveOrState, actionOrFace, layerNumber, arriveX, arriveY, arriveZ, originNumber, rotation));
    }

    static public void pushAniBarVoiceDeleteHistY(float barX, float barWidth, string voiceName, string parentName, int voiceGender, int originNumber)
    {
        _stackHistY.Push(7);
        _aniBarVoiceDeleteHistY.Push(new AniBarVoiceDelete(barX, barWidth, voiceName, parentName, voiceGender, originNumber));
    }

    static public void pushDressChangeHistY(string[] name, string prev, string objectName, int objectNum, int originNum)
    {
        _stackHistY.Push(8);
        _dressChangeHistY.Push(new DressChange(name, prev, objectName, originNum, objectNum));
    }

    static public void pushTilingHistY(int ori, int obj, Vector3 scale, int tile)
    {
        _stackHistY.Push(9);
        _tilingHistY.Push(new TilingChange(ori, obj, scale, tile));
    }

    static public void pushTileHistY(int ori, int obj, int bul)
    {
        _stackHistY.Push(10);
        _tileHistY.Push(new TileChange(ori, obj, bul));
    }

    static public void pushHandHistY(int ori, int obj, bool left, string name, Item hand)
    {
        _stackHistY.Push(11);
        _handHistY.Push(new HandChange(ori, obj, left, name, hand));
    }
    #endregion
    /**
     * 이력 복귀 
     */
    #region
    private void popHistoryStack()
    {
        if (_stackHistZ.Count > 0)
        {
            int _what = _stackHistZ.Pop();
            if (_what == 0) PopAniBarPAW();
            else if (_what == 1) PopObjectPAS();
            else if (_what == 2) PopAniBarCreate();
            else if (_what == 3) PopObjectCreate();
            else if (_what == 4) PopObjectDelete();
            else if (_what == 5) PopHumanDelete();
            else if (_what == 6) PopAniBarDelete();
            else if (_what == 7) PopAniBarVoiceDelete();
            else if (_what == 8) PopDressChange();
            else if (_what == 9) PopTiling();
            else if (_what == 10) PopTile();
            else if (_what == 11) PopHand();
        }
    }

    private void popHistoryStackY()
    {
        if (_stackHistY.Count > 0)
        {
            int _what = _stackHistY.Pop();
            if (_what == 0) PopAniBarPAWY();
            else if (_what == 1) PopObjectPASY();
            else if (_what == 2) PopAniBarCreateY();
            else if (_what == 3) PopObjectCreateY();
            else if (_what == 4) PopObjectDeleteY();
            else if (_what == 5) PopHumanDeleteY();
            else if (_what == 6) PopAniBarDeleteY();
            else if (_what == 7) PopAniBarVoiceDeleteY();
            else if (_what == 8) PopDressChangeY();
            else if (_what == 9) PopTilingY();
            else if (_what == 10) PopTileY();
            else if (_what == 11) PopHandY();
        }
    }
    #endregion
    /*
     * Z
     */
    #region
    private void PopAniBarPAW()
    {
        AniBarPosAndWidth apaw = _aniBarHist.Pop();
        GameObject _go = apaw.getHistoryTarget();
        Vector3 _pos = apaw.getHistoryPosition();
        Vector3 _posY = apaw.getHistoryPosition();
        float _width = apaw.getHistoryWidth();
        float _widthY = apaw.getHistoryWidth();
        int _objectNum = apaw.getObjectNum();
        string _animationName = apaw.getAnimationName();
        bool _voice = apaw.getVoice();

        if (_voice)
        {
            foreach (SmallAniBar A in itemListControl._dataBaseSmallVoice)
            {
                if (A._item._objectNumber == _objectNum && A._animationName == _animationName)
                {
                    _go = A._bigAniBar;
                    _posY = _go.transform.localPosition;
                    _go.transform.localPosition = _pos;
                    A._thisAniBar.transform.localPosition = _pos;
                    _go.GetComponent<RectTransform>().sizeDelta = new Vector2(_width, _go.GetComponent<RectTransform>().rect.height);
                    _widthY = _go.GetComponent<RectTransform>().rect.width;
                    break;
                }
            }
        }
        else
        {
            foreach (SmallAniBar A in itemListControl._dataBaseSmallAnimation)
            {
                if (A._item._objectNumber == _objectNum && A._animationName == _animationName)
                {
                    _go = A._bigAniBar;
                    _posY = _go.transform.localPosition;
                    _go.transform.localPosition = _pos;
                    A._thisAniBar.transform.localPosition = _pos;
                    _go.GetComponent<RectTransform>().sizeDelta = new Vector2(_width, _go.GetComponent<RectTransform>().rect.height);
                    _widthY = _go.GetComponent<RectTransform>().rect.width;
                    break;
                }
            }
        }
        pushAniBarHistY(_go, _posY, _widthY, _objectNum, _animationName, _voice);
    }

    private void PopObjectPAS()
    {
        Debug.Log("alright");
        ObjectPosAndScale apaw = _objectHist.Pop();
        GameObject _go = apaw.getHistoryTarget();
        Vector3 _pos = apaw.getHistoryPosition();
        Vector3 _posY = apaw.getHistoryPosition();
        Vector3 _scale = apaw.getHistoryScale();
        Vector3 _scaleY = apaw.getHistoryScale();
        int _objectNum = apaw.getObjectNum();
        int _originNum = apaw.getOriginNum();
        Quaternion _rot = apaw.getRot();

        if (_originNum >= 2000)
        {
            if (_originNum >= 4000)
            {
                foreach (Item A in itemListControl._dataBaseWall)
                {
                    if (A._objectNumber == _objectNum)
                    {
                        _go = A.item3d.transform.parent.gameObject;
                        break;
                    }
                }
            }
            else
            {
                foreach (Item A in itemListControl._dataBaseHuman)
                {
                    if (A._objectNumber == _objectNum)
                    {
                        _go = A.item3d.transform.parent.gameObject;
                        break;
                    }
                }
            }
        }
        else
        {
            foreach (Item A in itemListControl._dataBaseItem)
            {
                if (A._objectNumber == _objectNum)
                {
                    _go = A.item3d.transform.parent.gameObject;
                    break;
                }
            }
        }
        _posY = _go.transform.localPosition;
        _scaleY = _go.transform.localScale;
        _go.transform.localPosition = _pos;
        _go.transform.localScale = _scale;
        Quaternion _rotY = _go.transform.rotation;
        _go.transform.rotation = _rot;
        pushObjectHistY(_go, _posY, _scaleY, _objectNum, _originNum, _rotY);
        clickedPlaceControl.ResetForHistory();
    }

    private void PopAniBarCreate()
    {
        AniBarCreate abc = _aniBarCreateHist.Pop();
        GameObject biggo = abc.GetBigGameObject();
        GameObject smallgo = abc.GetSmallGameObject();
        string animationName = abc.GetAnimationName();
        int objectNum = abc.GetObjectNum();
        int voice = abc.GetVoice();
        float _barX = 0;
        float _barWidth = 0;
        string _parentName = "";
        string _animationText = "";
        int _moveOrState = 0;
        int _actionOrFace = 0;
        int _layerNumber = 0;
        float _arriveX = 0;
        float _arriveY = 0;
        float _arriveZ = 0;
        int _originNum = 0;
        int _rotation = 0;
        string _voiceName = "";
        int _voiceGender = 0;

        Debug.Log(voice);

        if (voice == 0)
        {
            foreach (BigAniBar A in itemListControl._dataBaseBigVoice)
            {
                if (A._smallAniBar.GetComponent<SmallAniBar>()._item._objectNumber == objectNum && A._smallAniBar.GetComponent<SmallAniBar>()._animationName == animationName)
                {
                    biggo = A._thisAniBar;
                    smallgo = A._smallAniBar;
                    itemListControl._dataBaseBigVoice.Remove(A);
                    break;
                }
            }
            foreach (SmallAniBar A in itemListControl._dataBaseSmallVoice)
            {
                if (A._item._objectNumber == objectNum && A._animationName == animationName)
                {
                    _voiceName = A._animationName;
                    _voiceGender = A._voiceGender;
                    _barX = A.transform.localPosition.x;
                    _barWidth = A.GetComponent<RectTransform>().rect.width;
                    if (A._item._originNumber == 2000) _parentName = A.transform.parent.transform.parent.name.Substring(0, 8);
                    if (A._item._originNumber == 2001) _parentName = A.transform.parent.transform.parent.name.Substring(0, 3);
                    if (A._item._originNumber == 2002) _parentName = A.transform.parent.transform.parent.name.Substring(0, 5);
                    itemListControl._dataBaseSmallVoice.Remove(A);
                    itemListControl._voiceDBIndex--;
                    pushAniBarVoiceDeleteHistY(_barX, _barWidth, _voiceName, _parentName, _voiceGender, A._item._originNumber);
                    break;
                }
            }
        }
        else
        {
            foreach (BigAniBar A in itemListControl._dataBaseBigAnimation)
            {
                if (A._smallAniBar.GetComponent<SmallAniBar>()._item._objectNumber == objectNum && A._smallAniBar.GetComponent<SmallAniBar>()._animationName == animationName)
                {
                    biggo = A._thisAniBar;
                    smallgo = A._smallAniBar;
                    itemListControl._dataBaseBigAnimation.Remove(A);
                    itemListControl._actionDBIndex--;
                    break;
                }
            }
            foreach (SmallAniBar A in itemListControl._dataBaseSmallAnimation)
            {
                if (A._item._objectNumber == objectNum && A._animationName == animationName)
                {
                    itemListControl._dataBaseSmallAnimation.Remove(A);
                    _barX = A.transform.localPosition.x;
                    _barWidth = A.GetComponent<RectTransform>().rect.width;
                    if (A._item._originNumber == 2000) _parentName = A.transform.parent.transform.parent.name.Substring(0, 8);
                    if (A._item._originNumber == 2001) _parentName = A.transform.parent.transform.parent.name.Substring(0, 3);
                    if (A._item._originNumber == 2002) _parentName = A.transform.parent.transform.parent.name.Substring(0, 5);
                    _animationText = A._bigAniBar.transform.GetChild(0).GetComponent<Text>().text;
                    _moveOrState = A._moveCheck == true ? 1 : 0;
                    _actionOrFace = A._actionOrFace == true ? 1 : 0;
                    _layerNumber = A._layerNumber;
                    _arriveX = A._arriveLocation.x;
                    _arriveY = A._arriveLocation.y;
                    _arriveZ = A._arriveLocation.z;
                    _originNum = A._item._originNumber;
                    _rotation = A._rotation == true ? 1 : 0;
                    pushAniBarDeleteHistY(_barX, _barWidth, animationName, _parentName, _animationText, _moveOrState, _actionOrFace, _layerNumber, _arriveX, _arriveY, _arriveZ, _originNum, _rotation);
                    break;
                }
            }
        }
        Destroy(biggo);
        Destroy(smallgo);
    }

    private void PopObjectCreate()
    {
        ObjectCreate oc = _objectCreateHist.Pop();
        int originNum = oc.GetOriginNum();
        int objectNum = oc.GetObjectNum();
        GameObject go = oc.GetGameObject();
        GameObject canvas = GameObject.Find("Canvas");
        GameObject big;
        GameObject small;
        /////////////////
        string _itemName = "";
        Vector3 _pos = new Vector3(0, 0, 0);
        Vector3 _rot = new Vector3(0, 0, 0);
        Vector3 _scale = new Vector3(0, 0, 0);
        List<AniBarDelete> abd = new List<AniBarDelete>();
        List<AniBarVoiceDelete> abvd = new List<AniBarVoiceDelete>();
        /////////////////

        if (originNum >= 2000 && originNum < 4000)
        {
            foreach (Item A in itemListControl._dataBaseHuman)
            {
                if (A._objectNumber == objectNum)
                {
                    _itemName = A.itemName;
                    _pos = A.item3d.transform.parent.position;
                    _rot = A.item3d.transform.eulerAngles;
                    _scale = A.item3d.transform.parent.localScale;
                    go = A.item3d.transform.parent.gameObject;
                    itemListControl._dataBaseHuman.Remove(A);
                    Debug.Log("ObjectCreate " + _rot);
                    break;
                }
            }

            big = canvas.transform.GetChild(2).GetChild(4).GetChild(1).GetChild(0).GetChild(0).GetChild(0).gameObject;
            small = canvas.transform.GetChild(2).GetChild(4).GetChild(1).GetChild(0).GetChild(0).GetChild(1).gameObject;

            for (int i = 0; i < big.transform.childCount; i++)
            {
                if (big.transform.GetChild(i).GetComponent<BigSchedulerBar>()._objectNumber == objectNum - 1)
                {
                    foreach (SmallAniBar A in itemListControl._dataBaseSmallAnimation)
                    {
                        if (A._item._objectNumber == objectNum)
                        {
                            itemListControl._dataBaseSmallAnimation.Remove(A);
                            itemListControl._actionDBIndex--;
                        }
                    }
                    foreach (BigAniBar A in itemListControl._dataBaseBigAnimation)
                    {
                        if (A._smallAniBar.GetComponent<SmallAniBar>()._item._objectNumber == objectNum)
                        {
                            itemListControl._dataBaseBigAnimation.Remove(A);
                        }
                    }
                    pushHumanDeleteHistY(originNum, objectNum, _itemName, _pos, _rot, _scale, abd, abvd);
                    Destroy(big.transform.GetChild(i).gameObject);
                    Destroy(small.transform.GetChild(i).gameObject);
                    break;
                }
            }

            foreach (GameObject A in itemListControl._dataBaseBigBar)
            {
                if (A.GetComponent<BigSchedulerBar>()._objectNumber == objectNum - 1)
                {
                    itemListControl._dataBaseBigBar.Remove(A);
                    break;
                }
            }
            foreach (GameObject A in itemListControl._dataBaseSmallbar)
            {
                if (A.GetComponent<SmallSchedulerBar>()._objectNumber == objectNum - 1)
                {
                    itemListControl._dataBaseSmallbar.Remove(A);
                    break;
                }
            }

            Destroy(go.gameObject);
            itemListControl._humanDBIndex--;
        }
        else
        {
            if (originNum >= 4000)
            {
                foreach (Item A in itemListControl._dataBaseWall)
                {
                    if (A._objectNumber == objectNum && A._originNumber == originNum)
                    {
                        _itemName = A.itemName;
                        _pos = A.item3d.transform.parent.position;
                        _rot = A.item3d.transform.parent.eulerAngles;
                        _scale = A.item3d.transform.parent.localScale;
                        go = A.item3d.transform.parent.gameObject;
                        itemListControl._dataBaseWall.Remove(A);
                        pushObjectDeleteHistY(objectNum, originNum, _itemName, _pos, _rot, _scale);
                        itemListControl._wallDBIndex--;
                        break;
                    }
                }
            }
            else
            {
                foreach (Item A in itemListControl._dataBaseItem)
                {
                    if (A._objectNumber == objectNum)
                    {
                        _itemName = A.itemName;
                        _pos = A.item3d.transform.parent.position;
                        _rot = A.item3d.transform.parent.eulerAngles;
                        _scale = A.item3d.transform.parent.localScale;
                        go = A.item3d.transform.parent.gameObject;
                        itemListControl._dataBaseItem.Remove(A);
                        pushObjectDeleteHistY(objectNum, originNum, _itemName, _pos, _rot, _scale);
                        itemListControl._itemDBIndex--;
                        break;
                    }
                }
            }

            Destroy(go);
        }
    }//끝

    private void PopObjectDelete()
    {
        ObjectDelete od = _objectDeleteHist.Pop();

        int objectNum = od.GetObjectNum();
        int originNum = od.GetOriginNum();
        string itemName = od.GetItemName();
        Vector3 pos = od.GetPos();
        Vector3 rot = od.GetRot();
        Vector3 scale = od.GetScale();

        ///////////////////////////

        GameObject _loadObject = Instantiate(itemListControl.GetItem(4, originNum).item3d) as GameObject;
        _loadObject.transform.SetParent(GameObject.Find("InDoor").transform);
        _loadObject = _loadObject.transform.GetChild(0).gameObject;

        _loadObject.layer = LayerMask.NameToLayer("Default");
        _loadObject.AddComponent<ItemObject>();

        /* 빠른 연결을 위한 캐싱 작업 */
        ItemObject _tmp = _loadObject.GetComponent<ItemObject>();

        Item _tmpItem;
        _tmpItem = new Item(itemName, objectNum, originNum, _loadObject);

        /* 추가한 ItemObject에 현재 Item의 정보를 담음 */
        _tmp._thisItem = _tmpItem;
        if (originNum >= 4000) itemListControl.AddWall(_tmpItem);
        else itemListControl.AddDB(_tmpItem);

        _loadObject.AddComponent<Outline>();
        ///* 윤곽선 안보이게 처리! */
        _loadObject.GetComponent<Outline>().enabled = false;

        /* 위치 설정 */
        _loadObject.transform.parent.position = pos;

        /* 회전값 설정 */
        _loadObject.transform.parent.Rotate(rot);

        /* 크기값 설정*/
        _loadObject.transform.parent.localScale = scale;

        pushObjectCreateHistY(_loadObject, originNum, objectNum);

        /////////////////////////////
    }//끝

    private void PopHumanDelete()
    {
        HumanDelete hd = _humanDeleteHist.Pop();
        int originNum = hd.GetOriginNum();
        int objectNum = hd.GetObjectNum();
        string humanName = hd.GetHumanName();
        Vector3 pos = hd.GetPos();
        Vector3 rot = hd.GetRot();
        Vector3 scale = hd.GetScale();
        List<AniBarDelete> abd = hd.GetAniBarDelete();
        List<AniBarVoiceDelete> abvd = hd.GetAniBarVoiceDelete();

        /////////////////////////////////////////////////////////////////////

        GameObject _loadObject = Instantiate(itemListControl.GetItem(2, originNum).item3d) as GameObject;
        _loadObject.transform.SetParent(GameObject.Find("InDoor").transform);
        _loadObject = _loadObject.transform.GetChild(0).gameObject;

        _loadObject.layer = LayerMask.NameToLayer("Default");
        _loadObject.AddComponent<ItemObject>();

        /* 빠른 연결을 위한 캐싱 작업 */
        ItemObject _tmp = _loadObject.GetComponent<ItemObject>();

        Item _tmpItem;
        _tmpItem = new Item(humanName, objectNum, originNum, _loadObject);
        /* 추가한 ItemObject에 현재 Item의 정보를 담음 */
        _tmp._thisItem = _tmpItem;
        _tmp._thisItem.itemName = humanName;
        _tmp._humanInitPosition = pos;
        
        HumanItem _tmpHuman = new HumanItem("Idle", objectNum);
        _tmp._thisHuman = _tmpHuman;

        itemListControl.AddHuman(_tmpItem);


        //_loadObject.AddComponent<Outline>();
        ///* 윤곽선 안보이게 처리! */
        //_loadObject.GetComponent<Outline>().enabled = false;

        /* 위치 설정 */
        _loadObject.transform.parent.position = pos;



        /* 회전값 설정 */
        _loadObject.transform.Rotate(rot);

        /* 크기값 설정*/
        _loadObject.transform.parent.localScale = scale;

        /* 스케줄러 생성 파트*/
        //스몰바 생성 및 설정
        GameObject _smallBar = Instantiate(Resources.Load("Prefab/SmallScheduler_Sample")) as GameObject;
        if (originNum == 2001) //클릭했을때의 오브젝트의 오리진 넘버로 구별
        {
            _smallBar.name = "Man" + (objectNum); //스몰바의 이름 설정
            _smallBar.transform.GetChild(0).transform.GetChild(0).transform.GetChild(0).GetComponent<Text>().text = "" + humanName + objectNum; //스몰바의 텍스트설정
        }
        else if (originNum == 2000)
        {
            _smallBar.name = "Daughter" + (objectNum); //스몰바의 이름 설정
            _smallBar.transform.GetChild(0).transform.GetChild(0).transform.GetChild(0).GetComponent<Text>().text = "" + humanName + objectNum; //스몰바의 텍스트설정
        }
        else if (originNum == 2002)
        {
            _smallBar.name = "Woman" + (objectNum); //스몰바의 이름 설정
            _smallBar.transform.GetChild(0).transform.GetChild(0).transform.GetChild(0).GetComponent<Text>().text = "" + humanName + objectNum; //스몰바의 텍스트설정
        }
        //content 설정
        GameObject _smallContent;
        _smallContent = GameObject.Find("Canvas").transform.GetChild(2).transform.GetChild(4).transform.GetChild(1).transform.GetChild(0).transform.GetChild(0).transform.GetChild(1).gameObject;

        _smallBar.transform.SetParent(_smallContent.transform); //small Bar의 부모설정
        _smallBar.transform.localScale = new Vector3(1, 1, 1); //크기 지정
        //small Bar의 정보
        _smallBar.GetComponent<SmallSchedulerBar>()._objectNumber = objectNum - 1; //오브젝트넘버를 small bar에 저장
        _smallBar.GetComponent<SmallSchedulerBar>()._originNumber = originNum; //오리진 정보또한 저장
        _smallBar.GetComponent<SmallSchedulerBar>()._humanNumber = 0;
        _smallBar.GetComponent<SmallSchedulerBar>()._object = _tmpItem;

        //빅바 생성 및 설정
        GameObject _bigBar = Instantiate(Resources.Load("Prefab/BigScheduler_Sample")) as GameObject;
        if (originNum == 2001) //클릭했을때의 오브젝트의 오리진 넘버로 구별
        {
            _bigBar.name = "Man" + (objectNum); //빅바의 이름 설정
            _bigBar.transform.GetChild(0).transform.GetChild(0).GetComponent<Text>().text = "" + humanName + objectNum; //빅바의 텍스트설정
        }
        else if (originNum == 2000)
        {
            _bigBar.name = "Daughter" + (objectNum); //빅바의 이름 설정
            _bigBar.transform.GetChild(0).transform.GetChild(0).GetComponent<Text>().text = "" + humanName + objectNum; //빅바의 텍스트설정
        }
        else
        {
            _bigBar.name = "Woman" + (objectNum); //빅바의 이름 설정
            _bigBar.transform.GetChild(0).transform.GetChild(0).GetComponent<Text>().text = "" + humanName + objectNum; //빅바의 텍스트설정
        }
        //content 설정
        GameObject _bigContent;
        _bigContent = GameObject.Find("Canvas").transform.GetChild(2).transform.GetChild(4).transform.GetChild(1).transform.GetChild(0).transform.GetChild(0).transform.GetChild(0).gameObject;

        _bigBar.transform.SetParent(_bigContent.transform); //big Bar의 부모설정
        _bigBar.transform.localScale = new Vector3(1, 1, 1); //크기 지정
        //big Bar의 정보
        _bigBar.GetComponent<BigSchedulerBar>()._objectNumber = objectNum - 1; //오브젝트넘버를 small bar에 저장
        _bigBar.GetComponent<BigSchedulerBar>()._originNumber = originNum; //오리진 정보또한 저장
        _bigBar.GetComponent<BigSchedulerBar>()._humanNumber = 0; //휴먼넘버를 저장
        //생성된 빅바를 스몰바에 저장
        _smallBar.GetComponent<SmallSchedulerBar>()._bigScheduler = _bigBar;
        //big Bar는 우선 안보이게 처리
        _bigBar.SetActive(false);
        itemListControl._dataBaseBigBar.Add(_bigBar);
        itemListControl._dataBaseSmallbar.Add(_smallBar);

        ////////////////////////////////////////////////////////////////////

        foreach (AniBarDelete A in abd)
        {
            float _barX = A.GetBarX();
            float _barWidth = A.GetBarWidth();
            string _animationName = A.GetAnimationName();
            string _parentName = A.GetParentName();
            string _animationText = A.GetAnimationText();
            int _moveOrState = A.GetMoveOrState();
            int _actionOrFace = A.GetActionOrFace();
            int _layerNumber = A.GetLayerNumber();
            float _arriveX = A.GetArriveX();
            float _arriveY = A.GetArriveY();
            float _arriveZ = A.GetArriveZ();
            int originNumber = A.GetOriginNumber();
            int _rotation = A.GetRotation();
            GameObject _bigAniBarParent = GameObject.Find("Canvas/ItemMenuCanvas/SchedulerMenu/Scroll View/Viewport/Content/BigScheduler"); //빅바스케줄러를 찾아감
            GameObject _bigAniBar = Instantiate(Resources.Load("Prefab/Big")) as GameObject;
            _bigAniBar.transform.GetComponent<BigAniBar>()._animationName = _animationName;
            _bigAniBar.transform.GetChild(0).GetComponent<Text>().text = _animationText;

            Vector3 _arriveLocation = new Vector3(_arriveX, _arriveY, _arriveZ);

            GameObject _smallAniBarParent = GameObject.Find("Canvas/ItemMenuCanvas/SchedulerMenu/Scroll View/Viewport/Content/SmallScheduler"); //스몰스케줄러를 찾아감
            GameObject _smallAniBar = Instantiate(Resources.Load("Prefab/Small")) as GameObject;
            _smallAniBar.transform.GetComponent<SmallAniBar>()._layerNumber = _layerNumber;
            if (_actionOrFace == 0)
            {
                _smallAniBar.transform.GetComponent<SmallAniBar>()._actionOrFace = false;
            }
            else
            {
                _smallAniBar.transform.GetComponent<SmallAniBar>()._actionOrFace = true;
            }
            if (_rotation == 0)
            {
                _smallAniBar.transform.GetComponent<SmallAniBar>()._rotation = false;
            }
            else
            {
                _smallAniBar.transform.GetComponent<SmallAniBar>()._rotation = true;
            }

            Item _item = null;
            Animator _animator = null;
            foreach (Item B in itemListControl._dataBaseHuman)
            {
                if (B._objectNumber == objectNum)
                {
                    _item = B;
                    _animator = B.item3d.GetComponent<Animator>();
                    break;
                }
            }

            _smallAniBar.transform.GetComponent<SmallAniBar>()._item = _item; //사람객체 리스트를 돌면서 확인해야함
            _smallAniBar.transform.GetComponent<SmallAniBar>()._animator = _animator; //사람객체 리스트를 돌면서 확인해야함
            _smallAniBar.transform.GetComponent<SmallAniBar>()._animationName = _animationName;
            if (_moveOrState == 0)
            {
                _smallAniBar.transform.GetComponent<SmallAniBar>()._moveCheck = false;
            }
            else
            {
                _smallAniBar.transform.GetComponent<SmallAniBar>()._moveCheck = true;
                _smallAniBar.transform.GetComponent<SmallAniBar>()._arriveLocation = _arriveLocation;
            }

            //수정해야하는 파트
            int _parentNumber = 0;
            if (_layerNumber == 0 || _layerNumber == 1) //액션
                _parentNumber = 4;
            else if (_layerNumber == 5) //페이스
                _parentNumber = 1;
            else if (_layerNumber == 4 || _layerNumber == 2) // 핸드
                _parentNumber = 2;
            else if (_layerNumber == 3) //레그
                _parentNumber = 3;

            /*스케줄러에 도달했으니 해당 사람의 스케줄바를 찾음*/
            if (originNum == 2001)
            {
                _bigAniBarParent = _bigAniBarParent.transform.Find("Man" + objectNum).transform.GetChild(_parentNumber + 1).gameObject;
                _smallAniBarParent = _smallAniBarParent.transform.Find("Man" + objectNum).transform.GetChild(_parentNumber).gameObject;
            }
            else if (originNum == 2000)
            {
                _bigAniBarParent = _bigAniBarParent.transform.Find("Daughter" + objectNum).transform.GetChild(_parentNumber + 1).gameObject;
                _smallAniBarParent = _smallAniBarParent.transform.Find("Daughter" + objectNum).transform.GetChild(_parentNumber).gameObject;
            }
            else
            {
                _bigAniBarParent = _bigAniBarParent.transform.Find("Woman" + objectNum).transform.GetChild(_parentNumber + 1).gameObject;
                _smallAniBarParent = _smallAniBarParent.transform.Find("Woman" + objectNum).transform.GetChild(_parentNumber).gameObject;
            }

            _bigAniBar.transform.SetParent(_bigAniBarParent.transform, false);
            _bigAniBar.transform.localPosition = new Vector3(_barX, 0, 0); //부모를 지정해둔 뒤 위치를 새로 지정
            _smallAniBar.transform.SetParent(_smallAniBarParent.transform, false);
            _smallAniBar.transform.localPosition = new Vector3(_barX, 0, 0); //부모를 지정해둔 뒤 위치를 새로 지정

            _bigAniBar.transform.GetComponent<BigAniBar>()._smallAniBar = _smallAniBar;
            _smallAniBar.transform.GetComponent<SmallAniBar>()._bigAniBar = _bigAniBar;

            _bigAniBar.transform.GetComponent<RectTransform>().sizeDelta = new Vector2(_barWidth, _bigAniBar.transform.GetComponent<RectTransform>().rect.height);
            _bigAniBar.transform.GetChild(1).transform.localPosition = new Vector3(-_barWidth / 2, 0, 0);
            _bigAniBar.transform.GetChild(2).transform.localPosition = new Vector3(_barWidth / 2, 0, 0);
            _smallAniBar.transform.GetComponent<RectTransform>().sizeDelta = new Vector2(_barWidth, _smallAniBar.transform.GetComponent<RectTransform>().rect.height);

            if (_layerNumber == 0)
            {
                _bigAniBar.GetComponent<Image>().color = new Color(196 / 255.0f, 244 / 255.0f, 254 / 255.0f); //빅애니바의 색상을 변경
                _bigAniBar.transform.GetChild(1).GetComponent<Image>().color = new Color(196 / 255.0f, 244 / 255.0f, 254 / 255.0f); //왼쪽드래그바의 색상 변경
                _bigAniBar.transform.GetChild(2).GetComponent<Image>().color = new Color(196 / 255.0f, 244 / 255.0f, 254 / 255.0f); //오른쪽 드래그바의 색상 변경
                _smallAniBar.GetComponent<Image>().color = new Color(196 / 255.0f, 244 / 255.0f, 254 / 255.0f);
            }
            else if (_layerNumber == 3)
            {
                _bigAniBar.GetComponent<Image>().color = new Color((float)252 / 255, (float)198 / 255, (float)247 / 255); //빅애니바의 색상을 변경
                _bigAniBar.transform.GetChild(1).GetComponent<Image>().color = new Color((float)252 / 255, (float)198 / 255, (float)247 / 255); //왼쪽드래그바의 색상 변경
                _bigAniBar.transform.GetChild(2).GetComponent<Image>().color = new Color((float)252 / 255, (float)198 / 255, (float)247 / 255); //오른쪽 드래그바의 색상 변경
                _smallAniBar.GetComponent<Image>().color = new Color((float)252 / 255, (float)198 / 255, (float)247 / 255);

            }
            else if (_layerNumber == 2)
            {
                _bigAniBar.GetComponent<Image>().color = new Color((float)252 / 255, (float)198 / 255, (float)247 / 255); //빅애니바의 색상을 변경
                _bigAniBar.transform.GetChild(1).GetComponent<Image>().color = new Color((float)252 / 255, (float)198 / 255, (float)247 / 255); //왼쪽드래그바의 색상 변경
                _bigAniBar.transform.GetChild(2).GetComponent<Image>().color = new Color((float)252 / 255, (float)198 / 255, (float)247 / 255); //오른쪽 드래그바의 색상 변경
                _smallAniBar.GetComponent<Image>().color = new Color((float)252 / 255, (float)198 / 255, (float)247 / 255);
            }
            else
            {
                _bigAniBar.GetComponent<Image>().color = new Color((float)212 / 255, (float)238 / 255, (float)204 / 255); //빅애니바의 색상을 변경
                _bigAniBar.transform.GetChild(1).GetComponent<Image>().color = new Color((float)212 / 255, (float)238 / 255, (float)204 / 255); //왼쪽드래그바의 색상 변경
                _bigAniBar.transform.GetChild(2).GetComponent<Image>().color = new Color((float)212 / 255, (float)238 / 255, (float)204 / 255); //오른쪽 드래그바의 색상 변경
                _smallAniBar.GetComponent<Image>().color = new Color((float)212 / 255, (float)238 / 255, (float)204 / 255);
            }

            itemListControl.AddActionDB(_bigAniBar.transform.GetComponent<BigAniBar>(), _smallAniBar.transform.GetComponent<SmallAniBar>());
        }
        foreach (AniBarVoiceDelete AA in abvd)
        {
            float _barX = AA.GetBarX();
            float _barWidth = AA.GetBarWidth();
            string _voiceName = AA.GetVoiceName();
            string _parentName = AA.GetParentName();
            int _voiceGender = AA.GetVoiceGender();
            int originNumber = AA.GetOriginNumber();

            GameObject _bigAniBarParent = GameObject.Find("Canvas/ItemMenuCanvas/SchedulerMenu/Scroll View/Viewport/Content/BigScheduler"); //빅바스케줄러를 찾아감
            GameObject _bigAniBar = Instantiate(Resources.Load("Prefab/VoiceBig")) as GameObject;
            _bigAniBar.transform.GetComponent<BigAniBar>()._animationName = _voiceName;
            _bigAniBar.transform.GetChild(0).GetComponent<Text>().text = _voiceName;

            AudioClip _audioClip;
            if (_voiceGender == 1)
            {
                _audioClip = Resources.Load<AudioClip>("Voice/son/" + _voiceName);
            }
            else
            {
                _audioClip = Resources.Load<AudioClip>("Voice/yuinna/" + _voiceName);
            }

            /*스몰애니바를 생성*/
            GameObject _smallAniBarParent = GameObject.Find("Canvas/ItemMenuCanvas/SchedulerMenu/Scroll View/Viewport/Content/SmallScheduler"); //스몰스케줄러를 찾아감
            GameObject _smallAniBar = Instantiate(Resources.Load("Prefab/Small")) as GameObject;
            _smallAniBar.transform.GetComponent<SmallAniBar>()._voiceGender = _voiceGender;
            _smallAniBar.transform.GetComponent<SmallAniBar>()._voice = true;

            /*찾아야하는 정보*/
            Item _item = null;
            Animator _animator = null;
            foreach (Item B in itemListControl._dataBaseHuman)
            {
                if (B.itemName == _parentName)
                {
                    objectNum = B._objectNumber;
                    _item = B;
                    _animator = B.item3d.GetComponent<Animator>();
                    break;
                }
            }

            _smallAniBar.transform.GetComponent<SmallAniBar>()._item = _item;
            _smallAniBar.transform.GetComponent<SmallAniBar>()._animator = _animator;
            _smallAniBar.transform.GetComponent<SmallAniBar>()._animationName = _voiceName;

            int _parentNumber = 5;

            /*스케줄러에 도달했으니 해당 사람의 스케줄바를 찾음*/
            if (originNum == 2001)
            {
                _bigAniBarParent = _bigAniBarParent.transform.Find("Man" + objectNum).transform.GetChild(_parentNumber + 1).gameObject;
                _smallAniBarParent = _smallAniBarParent.transform.Find("Man" + objectNum).transform.GetChild(_parentNumber).gameObject;
            }
            else if (originNum == 2000)
            {
                _bigAniBarParent = _bigAniBarParent.transform.Find("Daughter" + objectNum).transform.GetChild(_parentNumber + 1).gameObject;
                _smallAniBarParent = _smallAniBarParent.transform.Find("Daughter" + objectNum).transform.GetChild(_parentNumber).gameObject;
            }
            else
            {
                _bigAniBarParent = _bigAniBarParent.transform.Find("Woman" + objectNum).transform.GetChild(_parentNumber + 1).gameObject;
                _smallAniBarParent = _smallAniBarParent.transform.Find("Woman" + objectNum).transform.GetChild(_parentNumber).gameObject;
            }

            /*빅애니바, 스몰애니바의 부모설정*/
            _bigAniBar.transform.SetParent(_bigAniBarParent.transform, false);
            _bigAniBar.transform.localPosition = new Vector3(_barX, 0, 0); //부모를 지정해둔 뒤 위치를 새로 지정
            _smallAniBar.transform.SetParent(_smallAniBarParent.transform, false);
            _smallAniBar.transform.localPosition = new Vector3(_barX, 0, 0); //부모를 지정해둔 뒤 위치를 새로 지정

            _bigAniBar.transform.GetComponent<BigAniBar>()._smallAniBar = _smallAniBar;
            _smallAniBar.transform.GetComponent<SmallAniBar>()._bigAniBar = _bigAniBar;


            _bigAniBar.GetComponent<Image>().color = new Color((float)252 / 255, (float)198 / 255, (float)247 / 255); //빅애니바의 색상을 변경
            _smallAniBar.GetComponent<Image>().color = new Color((float)252 / 255, (float)198 / 255, (float)247 / 255);

            float _aniBarWidth = _bigAniBar.transform.GetComponent<RectTransform>().rect.width;
            float _width = _aniBarWidth * _audioClip.length / 11.73f;
            _bigAniBar.transform.GetComponent<RectTransform>().sizeDelta = new Vector2(_width, _bigAniBar.transform.GetComponent<RectTransform>().rect.height);
            _smallAniBar.transform.GetComponent<RectTransform>().sizeDelta = new Vector2(_width, _smallAniBar.transform.GetComponent<RectTransform>().rect.height);

            _smallAniBar.transform.GetComponent<SmallAniBar>()._audio = _audioClip;

            itemListControl.AddVoiceDB(_bigAniBar.transform.GetComponent<BigAniBar>(), _smallAniBar.transform.GetComponent<SmallAniBar>());

        }

        pushObjectCreateHistY(_loadObject, originNum, objectNum);

    }//끝

    private void PopAniBarDelete()
    {
        AniBarDelete A = _aniBarDeleteHist.Pop();
        float _barX = A.GetBarX();
        float _barWidth = A.GetBarWidth();
        string _animationName = A.GetAnimationName();
        string _parentName = A.GetParentName();
        string _animationText = A.GetAnimationText();
        int _moveOrState = A.GetMoveOrState();
        int _actionOrFace = A.GetActionOrFace();
        int _layerNumber = A.GetLayerNumber();
        float _arriveX = A.GetArriveX();
        float _arriveY = A.GetArriveY();
        float _arriveZ = A.GetArriveZ();
        int originNum = A.GetOriginNumber();
        int _rotation = A.GetRotation();
        GameObject _bigAniBarParent = GameObject.Find("Canvas/ItemMenuCanvas/SchedulerMenu/Scroll View/Viewport/Content/BigScheduler"); //빅바스케줄러를 찾아감
        GameObject _bigAniBar = Instantiate(Resources.Load("Prefab/Big")) as GameObject;
        _bigAniBar.transform.GetComponent<BigAniBar>()._animationName = _animationName;
        _bigAniBar.transform.GetChild(0).GetComponent<Text>().text = _animationText;

        Vector3 _arriveLocation = new Vector3(_arriveX, _arriveY, _arriveZ);

        GameObject _smallAniBarParent = GameObject.Find("Canvas/ItemMenuCanvas/SchedulerMenu/Scroll View/Viewport/Content/SmallScheduler"); //스몰스케줄러를 찾아감
        GameObject _smallAniBar = Instantiate(Resources.Load("Prefab/Small")) as GameObject;
        _smallAniBar.transform.GetComponent<SmallAniBar>()._layerNumber = _layerNumber;
        if (_actionOrFace == 0)
        {
            _smallAniBar.transform.GetComponent<SmallAniBar>()._actionOrFace = false;
        }
        else
        {
            _smallAniBar.transform.GetComponent<SmallAniBar>()._actionOrFace = true;
        }
        if (_rotation == 0)
        {
            _smallAniBar.transform.GetComponent<SmallAniBar>()._rotation = false;
        }
        else
        {
            _smallAniBar.transform.GetComponent<SmallAniBar>()._rotation = true;
        }

        int objectNum = 0;
        Item _item = null;
        Animator _animator = null;
        foreach (Item B in itemListControl._dataBaseHuman)
        {
            Debug.Log(B.itemName);
            Debug.Log(_parentName);
            if (B.itemName == _parentName)
            {
                objectNum = B._objectNumber;
                _item = B;
                _animator = B.item3d.GetComponent<Animator>();
                break;
            }
        }

        _smallAniBar.transform.GetComponent<SmallAniBar>()._item = _item; //사람객체 리스트를 돌면서 확인해야함
        _smallAniBar.transform.GetComponent<SmallAniBar>()._animator = _animator; //사람객체 리스트를 돌면서 확인해야함
        _smallAniBar.transform.GetComponent<SmallAniBar>()._animationName = _animationName;
        if (_moveOrState == 0)
        {
            _smallAniBar.transform.GetComponent<SmallAniBar>()._moveCheck = false;
        }
        else
        {
            _smallAniBar.transform.GetComponent<SmallAniBar>()._moveCheck = true;
            _smallAniBar.transform.GetComponent<SmallAniBar>()._arriveLocation = _arriveLocation;
        }

        //수정해야하는 파트
        int _parentNumber = 0;
        if (_layerNumber == 0 || _layerNumber == 1) //액션
            _parentNumber = 4;
        else if (_layerNumber == 5) //페이스
            _parentNumber = 1;
        else if (_layerNumber == 4 || _layerNumber == 2) // 핸드
            _parentNumber = 2;
        else if (_layerNumber == 3) //레그
            _parentNumber = 3;
        /*스케줄러에 도달했으니 해당 사람의 스케줄바를 찾음*/
        if (originNum == 2001)
        {
            _bigAniBarParent = _bigAniBarParent.transform.Find("Man" + objectNum).transform.GetChild(_parentNumber + 1).gameObject;
            _smallAniBarParent = _smallAniBarParent.transform.Find("Man" + objectNum).transform.GetChild(_parentNumber).gameObject;
        }
        else if (originNum == 2000)
        {
            _bigAniBarParent = _bigAniBarParent.transform.Find("Daughter" + objectNum).transform.GetChild(_parentNumber + 1).gameObject;
            _smallAniBarParent = _smallAniBarParent.transform.Find("Daughter" + objectNum).transform.GetChild(_parentNumber).gameObject;
        }
        else
        {
            _bigAniBarParent = _bigAniBarParent.transform.Find("Woman" + objectNum).transform.GetChild(_parentNumber + 1).gameObject;
            _smallAniBarParent = _smallAniBarParent.transform.Find("Woman" + objectNum).transform.GetChild(_parentNumber).gameObject;
        }

        _bigAniBar.transform.SetParent(_bigAniBarParent.transform, false);
        _bigAniBar.transform.localPosition = new Vector3(_barX, 0, 0); //부모를 지정해둔 뒤 위치를 새로 지정
        _smallAniBar.transform.SetParent(_smallAniBarParent.transform, false);
        _smallAniBar.transform.localPosition = new Vector3(_barX, 0, 0); //부모를 지정해둔 뒤 위치를 새로 지정

        _bigAniBar.transform.GetComponent<BigAniBar>()._smallAniBar = _smallAniBar;
        _smallAniBar.transform.GetComponent<SmallAniBar>()._bigAniBar = _bigAniBar;

        _bigAniBar.transform.GetComponent<RectTransform>().sizeDelta = new Vector2(_barWidth, _bigAniBar.transform.GetComponent<RectTransform>().rect.height);
        _bigAniBar.transform.GetChild(1).transform.localPosition = new Vector3(-_barWidth / 2, 0, 0);
        _bigAniBar.transform.GetChild(2).transform.localPosition = new Vector3(_barWidth / 2, 0, 0);
        _smallAniBar.transform.GetComponent<RectTransform>().sizeDelta = new Vector2(_barWidth, _smallAniBar.transform.GetComponent<RectTransform>().rect.height);

        if (_layerNumber == 0)
        {
            _bigAniBar.GetComponent<Image>().color = new Color(196 / 255.0f, 244 / 255.0f, 254 / 255.0f); //빅애니바의 색상을 변경
            _bigAniBar.transform.GetChild(1).GetComponent<Image>().color = new Color(196 / 255.0f, 244 / 255.0f, 254 / 255.0f); //왼쪽드래그바의 색상 변경
            _bigAniBar.transform.GetChild(2).GetComponent<Image>().color = new Color(196 / 255.0f, 244 / 255.0f, 254 / 255.0f); //오른쪽 드래그바의 색상 변경
            _smallAniBar.GetComponent<Image>().color = new Color(196 / 255.0f, 244 / 255.0f, 254 / 255.0f);
        }
        else if (_layerNumber == 3)
        {
            _bigAniBar.GetComponent<Image>().color = new Color((float)252 / 255, (float)198 / 255, (float)247 / 255); //빅애니바의 색상을 변경
            _bigAniBar.transform.GetChild(1).GetComponent<Image>().color = new Color((float)252 / 255, (float)198 / 255, (float)247 / 255); //왼쪽드래그바의 색상 변경
            _bigAniBar.transform.GetChild(2).GetComponent<Image>().color = new Color((float)252 / 255, (float)198 / 255, (float)247 / 255); //오른쪽 드래그바의 색상 변경
            _smallAniBar.GetComponent<Image>().color = new Color((float)252 / 255, (float)198 / 255, (float)247 / 255);

        }
        else if (_layerNumber == 2)
        {
            _bigAniBar.GetComponent<Image>().color = new Color((float)252 / 255, (float)198 / 255, (float)247 / 255); //빅애니바의 색상을 변경
            _bigAniBar.transform.GetChild(1).GetComponent<Image>().color = new Color((float)252 / 255, (float)198 / 255, (float)247 / 255); //왼쪽드래그바의 색상 변경
            _bigAniBar.transform.GetChild(2).GetComponent<Image>().color = new Color((float)252 / 255, (float)198 / 255, (float)247 / 255); //오른쪽 드래그바의 색상 변경
            _smallAniBar.GetComponent<Image>().color = new Color((float)252 / 255, (float)198 / 255, (float)247 / 255);
        }
        else
        {
            _bigAniBar.GetComponent<Image>().color = new Color((float)212 / 255, (float)238 / 255, (float)204 / 255); //빅애니바의 색상을 변경
            _bigAniBar.transform.GetChild(1).GetComponent<Image>().color = new Color((float)212 / 255, (float)238 / 255, (float)204 / 255); //왼쪽드래그바의 색상 변경
            _bigAniBar.transform.GetChild(2).GetComponent<Image>().color = new Color((float)212 / 255, (float)238 / 255, (float)204 / 255); //오른쪽 드래그바의 색상 변경
            _smallAniBar.GetComponent<Image>().color = new Color((float)212 / 255, (float)238 / 255, (float)204 / 255);
        }

        itemListControl.AddActionDB(_bigAniBar.transform.GetComponent<BigAniBar>(), _smallAniBar.transform.GetComponent<SmallAniBar>());
        pushAniBarCreateHistY(_bigAniBar, _smallAniBar, _animationName, objectNum, _smallAniBar.GetComponent<SmallAniBar>()._voice == true ? 0 : 1);
    }//끝

    private void PopAniBarVoiceDelete()
    {
        AniBarVoiceDelete A = _aniBarVoiceDeleteHist.Pop();
        float _barX = A.GetBarX();
        float _barWidth = A.GetBarWidth();
        string _voiceName = A.GetVoiceName();
        string _parentName = A.GetParentName();
        int _voiceGender = A.GetVoiceGender();
        int originNum = A.GetOriginNumber();

        GameObject _bigAniBarParent = GameObject.Find("Canvas/ItemMenuCanvas/SchedulerMenu/Scroll View/Viewport/Content/BigScheduler"); //빅바스케줄러를 찾아감
        GameObject _bigAniBar = Instantiate(Resources.Load("Prefab/VoiceBig")) as GameObject;
        _bigAniBar.transform.GetComponent<BigAniBar>()._animationName = _voiceName;
        _bigAniBar.transform.GetChild(0).GetComponent<Text>().text = _voiceName;

        AudioClip _audioClip;
        if (_voiceGender == 1)
        {
            _audioClip = Resources.Load<AudioClip>("Voice/son/" + _voiceName);
        }
        else
        {
            _audioClip = Resources.Load<AudioClip>("Voice/yuinna/" + _voiceName);
        }

        /*스몰애니바를 생성*/
        GameObject _smallAniBarParent = GameObject.Find("Canvas/ItemMenuCanvas/SchedulerMenu/Scroll View/Viewport/Content/SmallScheduler"); //스몰스케줄러를 찾아감
        GameObject _smallAniBar = Instantiate(Resources.Load("Prefab/Small")) as GameObject;
        _smallAniBar.transform.GetComponent<SmallAniBar>()._voiceGender = _voiceGender;
        _smallAniBar.transform.GetComponent<SmallAniBar>()._voice = true;

        /*찾아야하는 정보*/
        int objectNum = 0;
        Item _item = null;
        Animator _animator = null;
        foreach (Item B in itemListControl._dataBaseHuman)
        {
            if (B.itemName == _parentName)
            {
                objectNum = B._objectNumber;
                _item = B;
                _animator = B.item3d.GetComponent<Animator>();
                break;
            }
        }

        _smallAniBar.transform.GetComponent<SmallAniBar>()._item = _item;
        _smallAniBar.transform.GetComponent<SmallAniBar>()._animator = _animator;
        _smallAniBar.transform.GetComponent<SmallAniBar>()._animationName = _voiceName;

        int _parentNumber = 5;

        /*스케줄러에 도달했으니 해당 사람의 스케줄바를 찾음*/
        if (originNum == 2001)
        {
            _bigAniBarParent = _bigAniBarParent.transform.Find("Man" + objectNum).transform.GetChild(_parentNumber + 1).gameObject;
            _smallAniBarParent = _smallAniBarParent.transform.Find("Man" + objectNum).transform.GetChild(_parentNumber).gameObject;
        }
        else if (originNum == 2000)
        {
            _bigAniBarParent = _bigAniBarParent.transform.Find("Daughter" + objectNum).transform.GetChild(_parentNumber + 1).gameObject;
            _smallAniBarParent = _smallAniBarParent.transform.Find("Daughter" + objectNum).transform.GetChild(_parentNumber).gameObject;
        }
        else
        {
            _bigAniBarParent = _bigAniBarParent.transform.Find("Woman" + objectNum).transform.GetChild(_parentNumber + 1).gameObject;
            _smallAniBarParent = _smallAniBarParent.transform.Find("Woman" + objectNum).transform.GetChild(_parentNumber).gameObject;
        }

        /*빅애니바, 스몰애니바의 부모설정*/
        _bigAniBar.transform.SetParent(_bigAniBarParent.transform, false);
        _bigAniBar.transform.localPosition = new Vector3(_barX, 0, 0); //부모를 지정해둔 뒤 위치를 새로 지정
        _smallAniBar.transform.SetParent(_smallAniBarParent.transform, false);
        _smallAniBar.transform.localPosition = new Vector3(_barX, 0, 0); //부모를 지정해둔 뒤 위치를 새로 지정

        _bigAniBar.transform.GetComponent<BigAniBar>()._smallAniBar = _smallAniBar;
        _smallAniBar.transform.GetComponent<SmallAniBar>()._bigAniBar = _bigAniBar;


        _bigAniBar.GetComponent<Image>().color = new Color((float)252 / 255, (float)198 / 255, (float)247 / 255); //빅애니바의 색상을 변경
        _smallAniBar.GetComponent<Image>().color = new Color((float)252 / 255, (float)198 / 255, (float)247 / 255);

        float _aniBarWidth = _bigAniBar.transform.GetComponent<RectTransform>().rect.width;
        float _width = _aniBarWidth * _audioClip.length / 11.73f;
        _bigAniBar.transform.GetComponent<RectTransform>().sizeDelta = new Vector2(_width, _bigAniBar.transform.GetComponent<RectTransform>().rect.height);
        _smallAniBar.transform.GetComponent<RectTransform>().sizeDelta = new Vector2(_width, _smallAniBar.transform.GetComponent<RectTransform>().rect.height);

        _smallAniBar.transform.GetComponent<SmallAniBar>()._audio = _audioClip;

        itemListControl.AddVoiceDB(_bigAniBar.transform.GetComponent<BigAniBar>(), _smallAniBar.transform.GetComponent<SmallAniBar>());
        pushAniBarCreateHistY(_bigAniBar, _smallAniBar, _voiceName, objectNum, _smallAniBar.GetComponent<SmallAniBar>()._voice == true ? 0 : 1);

    }//끝

    private void PopDressChange()
    {
        DressChange A = _dressChangeHist.Pop();
        string[] _name = A.GetName();
        string prev = A.GetPrev();
        string objectName = A.GetObjectName();
        int objectNum = A.GetObjectNum();
        int originNum = A.GetOriginNum();
        HumanItem _clickedHumanItem;

        GameObject _newCloth = null;
        string _prev = "null";

        foreach (Item B in itemListControl._dataBaseHuman)
        {
            if (B._objectNumber == objectNum && B._originNumber == originNum)
            {
                _newCloth = B.item3d;
            }
        }
        _clickedHumanItem = _newCloth.GetComponent<ItemObject>()._thisHuman;
        if (_name[0] == "shirt")
        {
            if (_clickedHumanItem._shirt != null)
            {
                _prev = _clickedHumanItem._shirt.name;
                _clickedHumanItem._shirt.SetActive(false);
            }
            if (prev.CompareTo("null") == 0) _clickedHumanItem._shirt = null;
            else if (prev.CompareTo("null") != 0)
            {
                _newCloth = _newCloth.transform.Find(_name[0] + "/" + prev).gameObject;
                _clickedHumanItem._shirt = _newCloth;
            }
            pushDressChangeHistY(_name, _prev, objectName, objectNum, originNum);
        }
        else if (_name[0] == "pant")
        {
            if (_clickedHumanItem._pant != null)
            {
                _prev = _clickedHumanItem._pant.name;
                _clickedHumanItem._pant.SetActive(false);
            }
            if (prev.CompareTo("null") == 0) _clickedHumanItem._pant = null;
            else if (prev.CompareTo("null") != 0)
            {
                _newCloth = _newCloth.transform.Find(_name[0] + "/" + prev).gameObject;
                _clickedHumanItem._pant = _newCloth;
            }
            pushDressChangeHistY(_name, _prev, objectName, objectNum, originNum);
        }
        if (prev.CompareTo("null") != 0) _newCloth.SetActive(true);
    }

    private void PopTiling()
    {
        TilingChange _ti = _tilingHist.Pop();
        int _originNum = _ti.GetOriginNum();
        int _objectNum = _ti.GetObjectNum();
        int _tileOriginNum = _ti.GetTileOriginNum();
        Vector3 _scale = _ti.GetScale();
        Vector3 _scaleY;
        GameObject _go = null;

        Debug.Log(_objectNum);
        Debug.Log(_originNum);

        foreach (Item B in itemListControl._dataBaseWall)
        {
            if (B._originNumber == _originNum && B._objectNumber == _objectNum)
            {
                _go = B.item3d;
                break;
            }
        }
        MeshRenderer _mesh = _go.GetComponent<MeshRenderer>();

        if (_tileOriginNum == -1)
        {
            _scaleY = _mesh.material.mainTextureScale;
            _mesh.material.mainTextureScale = _scale;
            pushTilingHistY(_originNum, _objectNum, _scaleY, -1);
        }
        else
        {
            Sprite tmp = Instantiate(itemListControl.GetImages(3, _tileOriginNum));
            if (tmp != null) _mesh.material.mainTexture = tmp.texture;
            else _mesh.material.mainTexture = null;
            _go.GetComponent<ItemObject>()._placeNumber = _tileOriginNum;
            pushTilingHistY(_originNum, _objectNum, _scale, -1);
        }
        clickedPlaceControl.ResetForHistory();
    }

    private void PopTile()
    {
        TileChange A = _tileHist.Pop();
        int _originNumber = A.GetOriginNum();
        int _objectNumber = A.GetObjectNum();
        int _buildingNumber = A.GetBuildingOriginNum();
        GameObject _target = null;

        foreach (Item B in itemListControl._dataBaseWall)
        {
            if (B._originNumber == _originNumber && B._objectNumber == _objectNumber)
            {
                _target = B.item3d;
            }
        }
        Debug.Log(_target);

        /* 클릭된 Place 객체의 MeshRenderer 컴포넌트를 담음 */
        MeshRenderer _clickedMeshRenderer = _target.GetComponent<MeshRenderer>();

        /* Slot의 OriginNumber에 해당하는 Sprite 정보를 가져옴 */
        Sprite _tmp = itemListControl.GetImages(3, _buildingNumber);

        /* 존재하지 않는 Sprite이면 */
        if (_tmp == null)
        {
            /* 건물의 OriginNumber를 저장 */
            int _buildingOriginNumber = _target.GetComponent<ItemObject>()._thisItem._originNumber;

            /* 건물들의 3DObject가 들어있는 디렉터리 경로 지정 */
            System.IO.DirectoryInfo _dir = new System.IO.DirectoryInfo("Assets/Resources/Item/Wall/Build");

            /* 디렉터리 탐색 */
            foreach (System.IO.FileInfo _file in _dir.GetFiles())
            {
                /* 파일의 OriginNumber와 일치하면 */
                if (_file.Name.Split('_')[0] == _buildingOriginNumber.ToString() && _file.Extension.ToLower() == ".prefab")
                {
                    /* Resource를 통해 건물 정보 저장 */
                    GameObject _building = Resources.Load<GameObject>("Item/Wall/Build/" + _file.Name.Substring(0, _file.Name.Length - 7));

                    /* 머터리얼을 복제해서 클릭된 건물의 머터리얼로 지정 */
                    _clickedMeshRenderer.material = Instantiate(_building.GetComponent<Transform>().GetChild(0).GetComponent<MeshRenderer>().sharedMaterial);

                    break; //반복문 빠져나옴
                }
            }
        }

        /* 존재하는 Sprite이면 */
        else
        {
            /* Sprite 복제 */
            Sprite _tmpClone = Instantiate(_tmp);

            /* Texture로 적용 */
            _clickedMeshRenderer.material.mainTexture = _tmpClone.texture;
        }

        /* 클릭된 건물의 ItemObject에 PlaceNumber 지정 */
        Debug.Log("YCheck");
        pushTileHistY(_originNumber, _objectNumber, _target.GetComponent<ItemObject>()._placeNumber);
        _target.GetComponent<ItemObject>()._placeNumber = _buildingNumber;
    }

    private void PopHand()
    {
        HandChange hg = _handHist.Pop();
        int _originNum = hg.GetOriginNum();
        int _objectNum = hg.GetObjectNum();
        bool _isLeft = hg.GetIsLeft();
        string _objectName = hg.GetObjectName();
        Item _thisItem = hg.GetItem();
        Item _thisHuman = null;
        HumanItem _thisHumanItem = null;
        GameObject _handObjectClone = null;
        Transform _hand;
        string _itemName = "null";

        Debug.Log("HandY " + _objectName);
        if (_thisItem != null) Debug.Log("HandY " + _thisItem.itemName);

        foreach (Item A in itemListControl._dataBaseHuman)
        {
            if (A._originNumber == _originNum && A._objectNumber == _objectNum)
            {
                _thisHuman = A;
                _thisHumanItem = A.item3d.GetComponent<ItemObject>()._thisHuman;
                break;
            }
        }

        Vector3 _pos = new Vector3(1, 0, 0);
        if (_isLeft) //왼손이면
        {
            /* 왼손을 변수로 저장 
            if (_clickedItemControl._clickedItem._originNumber == 2002) //여자 객체인 경우 본 계층이 다르기 때문에 별도로 지정
            {
                _hand = _clickedItemControl._clickedItem.item3d.transform.GetChild(0).GetChild(1).GetChild(0).GetChild(2).GetChild(0).GetChild(0).GetChild(0).GetChild(0).GetChild(0).GetChild(0).GetChild(0);
            }
            else
            {
            */
            _hand = _thisHuman.item3d.transform.GetChild(0).GetChild(1).GetChild(0).GetChild(2).GetChild(0).GetChild(0).GetChild(1).GetChild(0).GetChild(0).GetChild(0);
            //}

        }
        else //오른손이면
        {
            /* 오른손을 변수로 저장하고 
            if (_clickedItemControl._clickedItem._originNumber == 2002) //여자 객체인 경우
            {
                _hand = _clickedItemControl._clickedItem.item3d.transform.GetChild(0).GetChild(1).GetChild(0).GetChild(2).GetChild(0).GetChild(0).GetChild(0).GetChild(2).GetChild(0).GetChild(0).GetChild(0);
            }
            else
            {
            */
            _hand = _thisHuman.item3d.transform.GetChild(0).GetChild(1).GetChild(0).GetChild(2).GetChild(0).GetChild(0).GetChild(2).GetChild(0).GetChild(0).GetChild(0);
            //}
        }


        if (_isLeft)
        {
            if (_thisHumanItem._leftHandItem != null)
            {
                _itemName = _thisHumanItem._leftHandItem.itemName;
                HistoryController.pushHandHistY(_thisHuman._originNumber, _thisHuman._objectNumber, _isLeft, _itemName, _thisHumanItem._leftHandItem);
                if (!_objectName.Equals("null") && !_thisHumanItem._leftHandItem.itemName.Equals(_thisItem.itemName))
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
                HistoryController.pushHandHistY(_thisHuman._originNumber, _thisHuman._objectNumber, _isLeft, _itemName, _thisHumanItem._leftHandItem);
                _thisHumanItem._leftHandItem = _thisItem;
            }
            /* 생성 */
            _handObjectClone = Instantiate(_thisHumanItem._leftHandItem.item3d); //물건 복제
            _handObjectClone.transform.SetParent(_hand); //손을 부모로 설정
            _handObjectClone.name = _thisHumanItem._leftHandItem.itemName; //이름 지정

            /*
            if (_thisHuman._originNumber == 2002) //여자 객체인 경우 남자, 딸 객체와 본이 다르기 때문에 별도로 지정해주어야한다.
            {
                _handObjectClone.transform.position = _hand.transform.GetChild(2).GetChild(0).transform.position; //세번째 손가락의 위치가 다름
            }
            else
            {
            */
            _handObjectClone.transform.position = _hand.transform.GetChild(2).transform.position; //세번째 손가락 위치로 지정

            if (_thisHuman._originNumber == 2002) //여자 객체인 경우
            {
                _handObjectClone.transform.GetChild(0).transform.localPosition = _handObjectClone.transform.GetChild(0).GetComponent<HandItem>()._womanLeftPos;
            }

            else if (_thisHuman._originNumber == 2001)
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
                HistoryController.pushHandHistY(_thisHuman._originNumber, _thisHuman._objectNumber, _isLeft, _itemName, _thisHumanItem._rightHandItem);
                if (!_objectName.Equals("null") && !_thisHumanItem._rightHandItem.itemName.Equals(_thisItem.itemName))
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
                HistoryController.pushHandHistY(_thisHuman._originNumber, _thisHuman._objectNumber, _isLeft, _itemName, _thisHumanItem._rightHandItem);
                _thisHumanItem._rightHandItem = _thisItem;
            }
            /* 생성 */
            _handObjectClone = Instantiate(_thisItem.item3d); //물건 복제
            _handObjectClone.transform.SetParent(_hand); //손을 부모로 설정
            _handObjectClone.name = _thisItem.itemName; //이름 지정
            _handObjectClone.transform.position = _hand.transform.GetChild(2).transform.position; //세번째 손가락 위치로 지정

            if (_thisHuman._originNumber == 2002) //여자 객체인 경우
            {
                _handObjectClone.transform.GetChild(0).transform.localPosition = _handObjectClone.transform.GetChild(0).GetComponent<HandItem>()._womanRightPos;
            }

            else if (_thisHuman._originNumber == 2001)
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
    #endregion
    /*
     * Y
     */
    #region
    private void PopAniBarPAWY()
    {
        AniBarPosAndWidth apaw = _aniBarHistY.Pop();
        GameObject _go = apaw.getHistoryTarget();
        Vector3 _pos = apaw.getHistoryPosition();
        Vector3 _posY = apaw.getHistoryPosition();
        float _width = apaw.getHistoryWidth();
        float _widthY = apaw.getHistoryWidth();
        int _objectNum = apaw.getObjectNum();
        string _animationName = apaw.getAnimationName();
        bool _voice = apaw.getVoice();

        if (_voice)
        {
            foreach (SmallAniBar A in itemListControl._dataBaseSmallVoice)
            {
                if (A._item._objectNumber == _objectNum && A._animationName == _animationName)
                {
                    _go = A._bigAniBar;
                    _posY = _go.transform.localPosition;
                    _go.transform.localPosition = _pos;
                    A._thisAniBar.transform.localPosition = _pos;
                    _go.GetComponent<RectTransform>().sizeDelta = new Vector2(_width, _go.GetComponent<RectTransform>().rect.height);
                    _widthY = _go.GetComponent<RectTransform>().rect.width;
                    break;
                }
            }
        }
        else
        {
            foreach (SmallAniBar A in itemListControl._dataBaseSmallAnimation)
            {
                if (A._item._objectNumber == _objectNum && A._animationName == _animationName)
                {
                    _go = A._bigAniBar;
                    _posY = _go.transform.localPosition;
                    _go.transform.localPosition = _pos;
                    A._thisAniBar.transform.localPosition = _pos;
                    _go.GetComponent<RectTransform>().sizeDelta = new Vector2(_width, _go.GetComponent<RectTransform>().rect.height);
                    _widthY = _go.GetComponent<RectTransform>().rect.width;
                    break;
                }
            }
        }
        pushAniBarHist(_go, _posY, _widthY, _objectNum, _animationName, _voice);
    }

    private void PopObjectPASY()
    {
        ObjectPosAndScale apaw = _objectHistY.Pop();
        GameObject _go = apaw.getHistoryTarget();
        Vector3 _pos = apaw.getHistoryPosition();
        Vector3 _posY = apaw.getHistoryPosition();
        Vector3 _scale = apaw.getHistoryScale();
        Vector3 _scaleY = apaw.getHistoryScale();
        int _objectNum = apaw.getObjectNum();
        int _originNum = apaw.getOriginNum();
        Quaternion _rot = apaw.getRot();

        if (_originNum >= 2000)
        {
            if (_originNum >= 4000)
            {
                foreach (Item A in itemListControl._dataBaseWall)
                {
                    if (A._objectNumber == _objectNum)
                    {
                        _go = A.item3d.transform.parent.gameObject;
                        break;
                    }
                }
            }
            else
            {
                foreach (Item A in itemListControl._dataBaseHuman)
                {
                    if (A._objectNumber == _objectNum)
                    {
                        _go = A.item3d.transform.parent.gameObject;
                        break;
                    }
                }
            }
        }
        else
        {
            foreach (Item A in itemListControl._dataBaseItem)
            {
                if (A._objectNumber == _objectNum)
                {
                    _go = A.item3d.transform.parent.gameObject;
                    break;
                }
            }
        }
        _posY = _go.transform.localPosition;
        _scaleY = _go.transform.localScale;
        _go.transform.localPosition = _pos;
        _go.transform.localScale = _scale;
        Quaternion _rotZ = _go.transform.rotation;
        _go.transform.rotation = _rot;
        pushObjectHist(_go, _posY, _scaleY, _objectNum, _originNum, _rotZ);
        clickedPlaceControl.ResetForHistory();
    }

    private void PopAniBarCreateY()
    {
        AniBarCreate abc = _aniBarCreateHistY.Pop();
        GameObject biggo = abc.GetBigGameObject();
        GameObject smallgo = abc.GetSmallGameObject();
        string animationName = abc.GetAnimationName();
        int objectNum = abc.GetObjectNum();
        int voice = abc.GetVoice();
        float _barX = 0;
        float _barWidth = 0;
        string _parentName = "";
        string _animationText = "";
        int _moveOrState = 0;
        int _actionOrFace = 0;
        int _layerNumber = 0;
        float _arriveX = 0;
        float _arriveY = 0;
        float _arriveZ = 0;
        int _originNum = 0;
        int _rotation = 0;
        string _voiceName = "";
        int _voiceGender = 0;



        if (voice == 0)
        {
            foreach (BigAniBar A in itemListControl._dataBaseBigVoice)
            {
                Debug.Log("a = " + A);
                if (A._smallAniBar.GetComponent<SmallAniBar>()._item._objectNumber == objectNum && A._smallAniBar.GetComponent<SmallAniBar>()._animationName == animationName)
                {
                    biggo = A._thisAniBar;
                    smallgo = A._smallAniBar;
                    itemListControl._dataBaseBigVoice.Remove(A);
                    break;
                }
            }
            foreach (SmallAniBar A in itemListControl._dataBaseSmallVoice)
            {
                if (A._item._objectNumber == objectNum && A._animationName == animationName)
                {
                    _voiceName = A._animationName;
                    _voiceGender = A._voiceGender;
                    _barX = A.transform.localPosition.x;
                    _barWidth = A.GetComponent<RectTransform>().rect.width;
                    if (A._item._originNumber == 2000) _parentName = A.transform.parent.transform.parent.name.Substring(0, 8);
                    if (A._item._originNumber == 2001) _parentName = A.transform.parent.transform.parent.name.Substring(0, 3);
                    if (A._item._originNumber == 2002) _parentName = A.transform.parent.transform.parent.name.Substring(0, 5);
                    itemListControl._dataBaseSmallVoice.Remove(A);
                    itemListControl._voiceDBIndex--;
                    pushAniBarVoiceDeleteHist(_barX, _barWidth, _voiceName, _parentName, _voiceGender, A._item._originNumber);
                    break;
                }
            }
        }
        else
        {
            foreach (BigAniBar A in itemListControl._dataBaseBigAnimation)
            {
                Debug.Log("a = " + A);
                if (A._smallAniBar.GetComponent<SmallAniBar>()._item._objectNumber == objectNum && A._smallAniBar.GetComponent<SmallAniBar>()._animationName == animationName)
                {
                    biggo = A._thisAniBar;
                    smallgo = A._smallAniBar;
                    itemListControl._dataBaseBigAnimation.Remove(A);
                    itemListControl._actionDBIndex--;
                    break;
                }
            }
            foreach (SmallAniBar A in itemListControl._dataBaseSmallAnimation)
            {
                if (A._item._objectNumber == objectNum && A._animationName == animationName)
                {
                    itemListControl._dataBaseSmallAnimation.Remove(A);
                    _barX = A.transform.localPosition.x;
                    _barWidth = A.GetComponent<RectTransform>().rect.width;
                    if (A._item._originNumber == 2000) _parentName = A.transform.parent.transform.parent.name.Substring(0, 8);
                    if (A._item._originNumber == 2001) _parentName = A.transform.parent.transform.parent.name.Substring(0, 3);
                    if (A._item._originNumber == 2002) _parentName = A.transform.parent.transform.parent.name.Substring(0, 5);
                    _animationText = A._bigAniBar.transform.GetChild(0).GetComponent<Text>().text;
                    _moveOrState = A._moveCheck == true ? 1 : 0;
                    _actionOrFace = A._actionOrFace == true ? 1 : 0;
                    _layerNumber = A._layerNumber;
                    _arriveX = A._arriveLocation.x;
                    _arriveY = A._arriveLocation.y;
                    _arriveZ = A._arriveLocation.z;
                    _originNum = A._item._originNumber;
                    _rotation = A._rotation == true ? 1 : 0;
                    pushAniBarDeleteHist(_barX, _barWidth, animationName, _parentName, _animationText, _moveOrState, _actionOrFace, _layerNumber, _arriveX, _arriveY, _arriveZ, _originNum, _rotation);
                    break;
                }
            }
        }
        Destroy(biggo);
        Destroy(smallgo);
    }

    private void PopObjectCreateY()
    {
        ObjectCreate oc = _objectCreateHistY.Pop();
        int originNum = oc.GetOriginNum();
        int objectNum = oc.GetObjectNum();
        GameObject go = oc.GetGameObject();
        GameObject canvas = GameObject.Find("Canvas");
        GameObject big;
        GameObject small;
        /////////////////
        string _itemName = "";
        Vector3 _pos = new Vector3(0, 0, 0);
        Vector3 _rot = new Vector3(0, 0, 0);
        Vector3 _scale = new Vector3(0, 0, 0);
        List<AniBarDelete> abd = new List<AniBarDelete>();
        List<AniBarVoiceDelete> abvd = new List<AniBarVoiceDelete>();
        /////////////////

        Debug.Log(objectNum);

        if (originNum >= 2000 && originNum < 4000)
        {
            foreach (Item A in itemListControl._dataBaseHuman)
            {
                if (A._objectNumber == objectNum)
                {
                    _itemName = A.itemName;
                    _pos = A.item3d.transform.parent.position;
                    _rot = A.item3d.transform.parent.eulerAngles;
                    _scale = A.item3d.transform.parent.localScale;
                    go = A.item3d.transform.parent.gameObject;
                    itemListControl._dataBaseHuman.Remove(A);
                    Debug.Log(A);
                    break;
                }
            }

            big = canvas.transform.GetChild(2).GetChild(4).GetChild(1).GetChild(0).GetChild(0).GetChild(0).gameObject;
            small = canvas.transform.GetChild(2).GetChild(4).GetChild(1).GetChild(0).GetChild(0).GetChild(1).gameObject;

            for (int i = 0; i < big.transform.childCount; i++)
            {
                if (big.transform.GetChild(i).GetComponent<BigSchedulerBar>()._objectNumber == objectNum - 1)
                {
                    foreach (SmallAniBar A in itemListControl._dataBaseSmallAnimation)
                    {
                        if (A._item._objectNumber == objectNum)
                        {

                            itemListControl._dataBaseSmallAnimation.Remove(A);
                            itemListControl._actionDBIndex--;
                        }
                    }
                    foreach (GameObject A in itemListControl._dataBaseSmallbar)
                    {
                        if (A.GetComponent<SmallSchedulerBar>()._objectNumber == objectNum - 1)
                        {
                            itemListControl._dataBaseSmallbar.Remove(A);
                            break;
                        }
                    }
                    pushHumanDeleteHist(originNum, objectNum, _itemName, _pos, _rot, _scale, abd, abvd);
                    Destroy(big.transform.GetChild(i).gameObject);
                    Destroy(small.transform.GetChild(i).gameObject);
                    break;
                }
            }

            foreach (GameObject A in itemListControl._dataBaseBigBar)
            {
                if (A.GetComponent<BigSchedulerBar>()._objectNumber == objectNum - 1)
                {
                    itemListControl._dataBaseBigBar.Remove(A);
                    break;
                }
            }
            foreach (GameObject A in itemListControl._dataBaseSmallbar)
            {
                if (A.GetComponent<SmallSchedulerBar>()._objectNumber == objectNum - 1)
                {
                    itemListControl._dataBaseSmallbar.Remove(A);
                    break;
                }
            }
            Destroy(go.gameObject);
            itemListControl._humanDBIndex--;
        }
        else
        {
            if (originNum >= 4000)
            {
                foreach (Item A in itemListControl._dataBaseWall)
                {
                    if (A._objectNumber == objectNum && A._originNumber == originNum)
                    {
                        _itemName = A.itemName;
                        _pos = A.item3d.transform.parent.position;
                        _rot = A.item3d.transform.parent.eulerAngles;
                        _scale = A.item3d.transform.parent.localScale;
                        go = A.item3d.transform.parent.gameObject;
                        itemListControl._dataBaseWall.Remove(A);
                        pushObjectDeleteHist(objectNum, originNum, _itemName, _pos, _rot, _scale);
                        itemListControl._wallDBIndex--;
                        break;
                    }
                }
            }
            else
            {
                foreach (Item A in itemListControl._dataBaseItem)
                {
                    if (A._objectNumber == objectNum)
                    {
                        _itemName = A.itemName;
                        _pos = A.item3d.transform.parent.position;
                        _rot = A.item3d.transform.parent.eulerAngles;
                        _scale = A.item3d.transform.parent.localScale;
                        go = A.item3d.transform.parent.gameObject;
                        itemListControl._dataBaseItem.Remove(A);
                        pushObjectDeleteHist(objectNum, originNum, _itemName, _pos, _rot, _scale);
                        itemListControl._itemDBIndex--;
                        break;
                    }
                }
            }

            Destroy(go);
        }
    }//끝

    private void PopObjectDeleteY()
    {
        ObjectDelete od = _objectDeleteHistY.Pop();

        int objectNum = od.GetObjectNum();
        int originNum = od.GetOriginNum();
        string itemName = od.GetItemName();
        Vector3 pos = od.GetPos();
        Vector3 rot = od.GetRot();
        Vector3 scale = od.GetScale();

        ///////////////////////////
        GameObject _loadObject = null;
        if (originNum >= 4000) _loadObject = Instantiate(itemListControl.GetItem(4, originNum).item3d) as GameObject;
        else _loadObject = Instantiate(itemListControl.GetItem(1, originNum).item3d) as GameObject;

        _loadObject.transform.SetParent(GameObject.Find("InDoor").transform);
        _loadObject = _loadObject.transform.GetChild(0).gameObject;

        _loadObject.layer = LayerMask.NameToLayer("Default");
        _loadObject.AddComponent<ItemObject>();

        /* 빠른 연결을 위한 캐싱 작업 */
        ItemObject _tmp = _loadObject.GetComponent<ItemObject>();

        Item _tmpItem;
        _tmpItem = new Item(itemName, objectNum, originNum, _loadObject);

        /* 추가한 ItemObject에 현재 Item의 정보를 담음 */
        _tmp._thisItem = _tmpItem;
        if (originNum >= 4000) itemListControl.AddWall(_tmpItem);
        else itemListControl.AddDB(_tmpItem);

        _loadObject.AddComponent<Outline>();
        ///* 윤곽선 안보이게 처리! */
        _loadObject.GetComponent<Outline>().enabled = false;

        /* 위치 설정 */
        _loadObject.transform.parent.position = pos;

        /* 회전값 설정 */
        _loadObject.transform.parent.Rotate(rot);

        /* 크기값 설정*/
        _loadObject.transform.parent.localScale = scale;

        pushObjectCreateHist(_loadObject, originNum, objectNum);

        /////////////////////////////
    }//끝

    private void PopHumanDeleteY()
    {
        HumanDelete hd = _humanDeleteHistY.Pop();
        int originNum = hd.GetOriginNum();
        int objectNum = hd.GetObjectNum();
        string humanName = hd.GetHumanName();
        Vector3 pos = hd.GetPos();
        Vector3 rot = hd.GetRot();
        Vector3 scale = hd.GetScale();
        List<AniBarDelete> abd = hd.GetAniBarDelete();
        List<AniBarVoiceDelete> abvd = hd.GetAniBarVoiceDelete();

        Debug.Log("HumanDeleteY " + rot);

        /////////////////////////////////////////////////////////////////////

        GameObject _loadObject = Instantiate(itemListControl.GetItem(2, originNum).item3d) as GameObject;
        _loadObject.transform.SetParent(GameObject.Find("InDoor").transform);
        _loadObject = _loadObject.transform.GetChild(0).gameObject;

        _loadObject.layer = LayerMask.NameToLayer("Default");
        _loadObject.AddComponent<ItemObject>();

        /* 빠른 연결을 위한 캐싱 작업 */
        ItemObject _tmp = _loadObject.GetComponent<ItemObject>();

        Item _tmpItem;
        _tmpItem = new Item(humanName, objectNum, originNum, _loadObject);
        /* 추가한 ItemObject에 현재 Item의 정보를 담음 */
        _tmp._thisItem = _tmpItem;
        _tmp._thisItem.itemName = humanName;
        _tmp._humanInitPosition = pos;

        HumanItem _tmpHuman = new HumanItem("Idle", objectNum);
        _tmp._thisHuman = _tmpHuman;

        itemListControl.AddHuman(_tmpItem);


        //_loadObject.AddComponent<Outline>();
        ///* 윤곽선 안보이게 처리! */
        //_loadObject.GetComponent<Outline>().enabled = false;

        /* 위치 설정 */
        _loadObject.transform.parent.position = pos;



        /* 회전값 설정 */
        _loadObject.transform.Rotate(rot);

        /* 크기값 설정*/
        _loadObject.transform.parent.localScale = scale;

        /* 스케줄러 생성 파트*/
        //스몰바 생성 및 설정
        GameObject _smallBar = Instantiate(Resources.Load("Prefab/SmallScheduler_Sample")) as GameObject;
        if (originNum == 2001) //클릭했을때의 오브젝트의 오리진 넘버로 구별
        {
            _smallBar.name = "Man" + (objectNum); //스몰바의 이름 설정
            _smallBar.transform.GetChild(0).transform.GetChild(0).transform.GetChild(0).GetComponent<Text>().text = "" + humanName + objectNum; //스몰바의 텍스트설정
        }
        else if (originNum == 2000)
        {
            _smallBar.name = "Daughter" + (objectNum); //스몰바의 이름 설정
            _smallBar.transform.GetChild(0).transform.GetChild(0).transform.GetChild(0).GetComponent<Text>().text = "" + humanName + objectNum; //스몰바의 텍스트설정
        }
        else if (originNum == 2002)
        {
            _smallBar.name = "Woman" + (objectNum); //스몰바의 이름 설정
            _smallBar.transform.GetChild(0).transform.GetChild(0).transform.GetChild(0).GetComponent<Text>().text = "" + humanName + objectNum; //스몰바의 텍스트설정
        }
        //content 설정
        GameObject _smallContent;
        _smallContent = GameObject.Find("Canvas").transform.GetChild(2).transform.GetChild(4).transform.GetChild(1).transform.GetChild(0).transform.GetChild(0).transform.GetChild(1).gameObject;

        _smallBar.transform.SetParent(_smallContent.transform); //small Bar의 부모설정
        _smallBar.transform.localScale = new Vector3(1, 1, 1); //크기 지정
        //small Bar의 정보
        _smallBar.GetComponent<SmallSchedulerBar>()._objectNumber = objectNum - 1; //오브젝트넘버를 small bar에 저장
        _smallBar.GetComponent<SmallSchedulerBar>()._originNumber = originNum; //오리진 정보또한 저장
        _smallBar.GetComponent<SmallSchedulerBar>()._humanNumber = 0;
        _smallBar.GetComponent<SmallSchedulerBar>()._object = _tmpItem;

        //빅바 생성 및 설정
        GameObject _bigBar = Instantiate(Resources.Load("Prefab/BigScheduler_Sample")) as GameObject;
        if (originNum == 2001) //클릭했을때의 오브젝트의 오리진 넘버로 구별
        {
            _bigBar.name = "Man" + (objectNum); //빅바의 이름 설정
            _bigBar.transform.GetChild(0).transform.GetChild(0).GetComponent<Text>().text = "" + humanName + objectNum; //빅바의 텍스트설정
        }
        else if (originNum == 2000)
        {
            _bigBar.name = "Daughter" + (objectNum); //빅바의 이름 설정
            _bigBar.transform.GetChild(0).transform.GetChild(0).GetComponent<Text>().text = "" + humanName + objectNum; //빅바의 텍스트설정
        }
        else
        {
            _bigBar.name = "Woman" + (objectNum); //빅바의 이름 설정
            _bigBar.transform.GetChild(0).transform.GetChild(0).GetComponent<Text>().text = "" + humanName + objectNum; //빅바의 텍스트설정
        }
        //content 설정
        GameObject _bigContent;
        _bigContent = GameObject.Find("Canvas").transform.GetChild(2).transform.GetChild(4).transform.GetChild(1).transform.GetChild(0).transform.GetChild(0).transform.GetChild(0).gameObject;

        _bigBar.transform.SetParent(_bigContent.transform); //big Bar의 부모설정
        _bigBar.transform.localScale = new Vector3(1, 1, 1); //크기 지정
        //big Bar의 정보
        _bigBar.GetComponent<BigSchedulerBar>()._objectNumber = objectNum - 1; //오브젝트넘버를 small bar에 저장
        _bigBar.GetComponent<BigSchedulerBar>()._originNumber = originNum; //오리진 정보또한 저장
        _bigBar.GetComponent<BigSchedulerBar>()._humanNumber = 0; //휴먼넘버를 저장
        //생성된 빅바를 스몰바에 저장
        _smallBar.GetComponent<SmallSchedulerBar>()._bigScheduler = _bigBar;
        //big Bar는 우선 안보이게 처리
        _bigBar.SetActive(false);
        itemListControl._dataBaseBigBar.Add(_bigBar);
        itemListControl._dataBaseSmallbar.Add(_smallBar);

        ////////////////////////////////////////////////////////////////////

        foreach (AniBarDelete A in abd)
        {
            float _barX = A.GetBarX();
            float _barWidth = A.GetBarWidth();
            string _animationName = A.GetAnimationName();
            string _parentName = A.GetParentName();
            string _animationText = A.GetAnimationText();
            int _moveOrState = A.GetMoveOrState();
            int _actionOrFace = A.GetActionOrFace();
            int _layerNumber = A.GetLayerNumber();
            float _arriveX = A.GetArriveX();
            float _arriveY = A.GetArriveY();
            float _arriveZ = A.GetArriveZ();
            int originNumber = A.GetOriginNumber();
            int _rotation = A.GetRotation();
            GameObject _bigAniBarParent = GameObject.Find("Canvas/ItemMenuCanvas/SchedulerMenu/Scroll View/Viewport/Content/BigScheduler"); //빅바스케줄러를 찾아감
            GameObject _bigAniBar = Instantiate(Resources.Load("Prefab/Big")) as GameObject;
            _bigAniBar.transform.GetComponent<BigAniBar>()._animationName = _animationName;
            _bigAniBar.transform.GetChild(0).GetComponent<Text>().text = _animationText;

            Vector3 _arriveLocation = new Vector3(_arriveX, _arriveY, _arriveZ);

            GameObject _smallAniBarParent = GameObject.Find("Canvas/ItemMenuCanvas/SchedulerMenu/Scroll View/Viewport/Content/SmallScheduler"); //스몰스케줄러를 찾아감
            GameObject _smallAniBar = Instantiate(Resources.Load("Prefab/Small")) as GameObject;
            _smallAniBar.transform.GetComponent<SmallAniBar>()._layerNumber = _layerNumber;
            if (_actionOrFace == 0)
            {
                _smallAniBar.transform.GetComponent<SmallAniBar>()._actionOrFace = false;
            }
            else
            {
                _smallAniBar.transform.GetComponent<SmallAniBar>()._actionOrFace = true;
            }
            if (_rotation == 0)
            {
                _smallAniBar.transform.GetComponent<SmallAniBar>()._rotation = false;
            }
            else
            {
                _smallAniBar.transform.GetComponent<SmallAniBar>()._rotation = true;
            }

            Item _item = null;
            Animator _animator = null;
            foreach (Item B in itemListControl._dataBaseHuman)
            {
                if (B._objectNumber == objectNum)
                {
                    _item = B;
                    _animator = B.item3d.GetComponent<Animator>();
                    break;
                }
            }

            _smallAniBar.transform.GetComponent<SmallAniBar>()._item = _item; //사람객체 리스트를 돌면서 확인해야함
            _smallAniBar.transform.GetComponent<SmallAniBar>()._animator = _animator; //사람객체 리스트를 돌면서 확인해야함
            _smallAniBar.transform.GetComponent<SmallAniBar>()._animationName = _animationName;
            if (_moveOrState == 0)
            {
                _smallAniBar.transform.GetComponent<SmallAniBar>()._moveCheck = false;
            }
            else
            {
                _smallAniBar.transform.GetComponent<SmallAniBar>()._moveCheck = true;
                _smallAniBar.transform.GetComponent<SmallAniBar>()._arriveLocation = _arriveLocation;
            }

            //수정해야하는 파트
            int _parentNumber = 0;
            if (_layerNumber == 0 || _layerNumber == 1) //액션
                _parentNumber = 4;
            else if (_layerNumber == 5) //페이스
                _parentNumber = 1;
            else if (_layerNumber == 4 || _layerNumber == 2) // 핸드
                _parentNumber = 2;
            else if (_layerNumber == 3) //레그
                _parentNumber = 3;

            /*스케줄러에 도달했으니 해당 사람의 스케줄바를 찾음*/
            if (originNum == 2001)
            {
                _bigAniBarParent = _bigAniBarParent.transform.Find("Man" + objectNum).transform.GetChild(_parentNumber + 1).gameObject;
                _smallAniBarParent = _smallAniBarParent.transform.Find("Man" + objectNum).transform.GetChild(_parentNumber).gameObject;
            }
            else if (originNum == 2000)
            {
                _bigAniBarParent = _bigAniBarParent.transform.Find("Daughter" + objectNum).transform.GetChild(_parentNumber + 1).gameObject;
                _smallAniBarParent = _smallAniBarParent.transform.Find("Daughter" + objectNum).transform.GetChild(_parentNumber).gameObject;
            }
            else
            {
                _bigAniBarParent = _bigAniBarParent.transform.Find("Woman" + objectNum).transform.GetChild(_parentNumber + 1).gameObject;
                _smallAniBarParent = _smallAniBarParent.transform.Find("Woman" + objectNum).transform.GetChild(_parentNumber).gameObject;
            }

            _bigAniBar.transform.SetParent(_bigAniBarParent.transform, false);
            _bigAniBar.transform.localPosition = new Vector3(_barX, 0, 0); //부모를 지정해둔 뒤 위치를 새로 지정
            _smallAniBar.transform.SetParent(_smallAniBarParent.transform, false);
            _smallAniBar.transform.localPosition = new Vector3(_barX, 0, 0); //부모를 지정해둔 뒤 위치를 새로 지정

            _bigAniBar.transform.GetComponent<BigAniBar>()._smallAniBar = _smallAniBar;
            _smallAniBar.transform.GetComponent<SmallAniBar>()._bigAniBar = _bigAniBar;

            _bigAniBar.transform.GetComponent<RectTransform>().sizeDelta = new Vector2(_barWidth, _bigAniBar.transform.GetComponent<RectTransform>().rect.height);
            _bigAniBar.transform.GetChild(1).transform.localPosition = new Vector3(-_barWidth / 2, 0, 0);
            _bigAniBar.transform.GetChild(2).transform.localPosition = new Vector3(_barWidth / 2, 0, 0);
            _smallAniBar.transform.GetComponent<RectTransform>().sizeDelta = new Vector2(_barWidth, _smallAniBar.transform.GetComponent<RectTransform>().rect.height);

            if (_layerNumber == 0)
            {
                _bigAniBar.GetComponent<Image>().color = new Color(196 / 255.0f, 244 / 255.0f, 254 / 255.0f); //빅애니바의 색상을 변경
                _bigAniBar.transform.GetChild(1).GetComponent<Image>().color = new Color(196 / 255.0f, 244 / 255.0f, 254 / 255.0f); //왼쪽드래그바의 색상 변경
                _bigAniBar.transform.GetChild(2).GetComponent<Image>().color = new Color(196 / 255.0f, 244 / 255.0f, 254 / 255.0f); //오른쪽 드래그바의 색상 변경
                _smallAniBar.GetComponent<Image>().color = new Color(196 / 255.0f, 244 / 255.0f, 254 / 255.0f);
            }
            else if (_layerNumber == 3)
            {
                _bigAniBar.GetComponent<Image>().color = new Color((float)252 / 255, (float)198 / 255, (float)247 / 255); //빅애니바의 색상을 변경
                _bigAniBar.transform.GetChild(1).GetComponent<Image>().color = new Color((float)252 / 255, (float)198 / 255, (float)247 / 255); //왼쪽드래그바의 색상 변경
                _bigAniBar.transform.GetChild(2).GetComponent<Image>().color = new Color((float)252 / 255, (float)198 / 255, (float)247 / 255); //오른쪽 드래그바의 색상 변경
                _smallAniBar.GetComponent<Image>().color = new Color((float)252 / 255, (float)198 / 255, (float)247 / 255);

            }
            else if (_layerNumber == 2)
            {
                _bigAniBar.GetComponent<Image>().color = new Color((float)252 / 255, (float)198 / 255, (float)247 / 255); //빅애니바의 색상을 변경
                _bigAniBar.transform.GetChild(1).GetComponent<Image>().color = new Color((float)252 / 255, (float)198 / 255, (float)247 / 255); //왼쪽드래그바의 색상 변경
                _bigAniBar.transform.GetChild(2).GetComponent<Image>().color = new Color((float)252 / 255, (float)198 / 255, (float)247 / 255); //오른쪽 드래그바의 색상 변경
                _smallAniBar.GetComponent<Image>().color = new Color((float)252 / 255, (float)198 / 255, (float)247 / 255);
            }
            else
            {
                _bigAniBar.GetComponent<Image>().color = new Color((float)212 / 255, (float)238 / 255, (float)204 / 255); //빅애니바의 색상을 변경
                _bigAniBar.transform.GetChild(1).GetComponent<Image>().color = new Color((float)212 / 255, (float)238 / 255, (float)204 / 255); //왼쪽드래그바의 색상 변경
                _bigAniBar.transform.GetChild(2).GetComponent<Image>().color = new Color((float)212 / 255, (float)238 / 255, (float)204 / 255); //오른쪽 드래그바의 색상 변경
                _smallAniBar.GetComponent<Image>().color = new Color((float)212 / 255, (float)238 / 255, (float)204 / 255);
            }

            itemListControl.AddActionDB(_bigAniBar.transform.GetComponent<BigAniBar>(), _smallAniBar.transform.GetComponent<SmallAniBar>());
        }
        foreach (AniBarVoiceDelete AA in abvd)
        {
            float _barX = AA.GetBarX();
            float _barWidth = AA.GetBarWidth();
            string _voiceName = AA.GetVoiceName();
            string _parentName = AA.GetParentName();
            int _voiceGender = AA.GetVoiceGender();
            int originNumber = AA.GetOriginNumber();

            GameObject _bigAniBarParent = GameObject.Find("Canvas/ItemMenuCanvas/SchedulerMenu/Scroll View/Viewport/Content/BigScheduler"); //빅바스케줄러를 찾아감
            GameObject _bigAniBar = Instantiate(Resources.Load("Prefab/VoiceBig")) as GameObject;
            _bigAniBar.transform.GetComponent<BigAniBar>()._animationName = _voiceName;
            _bigAniBar.transform.GetChild(0).GetComponent<Text>().text = _voiceName;

            AudioClip _audioClip;
            if (_voiceGender == 1)
            {
                _audioClip = Resources.Load<AudioClip>("Voice/son/" + _voiceName);
            }
            else
            {
                _audioClip = Resources.Load<AudioClip>("Voice/yuinna/" + _voiceName);
            }

            /*스몰애니바를 생성*/
            GameObject _smallAniBarParent = GameObject.Find("Canvas/ItemMenuCanvas/SchedulerMenu/Scroll View/Viewport/Content/SmallScheduler"); //스몰스케줄러를 찾아감
            GameObject _smallAniBar = Instantiate(Resources.Load("Prefab/Small")) as GameObject;
            _smallAniBar.transform.GetComponent<SmallAniBar>()._voiceGender = _voiceGender;
            _smallAniBar.transform.GetComponent<SmallAniBar>()._voice = true;

            /*찾아야하는 정보*/
            Item _item = null;
            Animator _animator = null;
            foreach (Item B in itemListControl._dataBaseHuman)
            {
                if (B.itemName == _parentName)
                {
                    objectNum = B._objectNumber;
                    _item = B;
                    _animator = B.item3d.GetComponent<Animator>();
                    break;
                }
            }

            _smallAniBar.transform.GetComponent<SmallAniBar>()._item = _item;
            _smallAniBar.transform.GetComponent<SmallAniBar>()._animator = _animator;
            _smallAniBar.transform.GetComponent<SmallAniBar>()._animationName = _voiceName;

            int _parentNumber = 5;

            /*스케줄러에 도달했으니 해당 사람의 스케줄바를 찾음*/
            if (originNum == 2001)
            {
                _bigAniBarParent = _bigAniBarParent.transform.Find("Man" + objectNum).transform.GetChild(_parentNumber + 1).gameObject;
                _smallAniBarParent = _smallAniBarParent.transform.Find("Man" + objectNum).transform.GetChild(_parentNumber).gameObject;
            }
            else if (originNum == 2000)
            {
                _bigAniBarParent = _bigAniBarParent.transform.Find("Daughter" + objectNum).transform.GetChild(_parentNumber + 1).gameObject;
                _smallAniBarParent = _smallAniBarParent.transform.Find("Daughter" + objectNum).transform.GetChild(_parentNumber).gameObject;
            }
            else
            {
                _bigAniBarParent = _bigAniBarParent.transform.Find("Woman" + objectNum).transform.GetChild(_parentNumber + 1).gameObject;
                _smallAniBarParent = _smallAniBarParent.transform.Find("Woman" + objectNum).transform.GetChild(_parentNumber).gameObject;
            }

            /*빅애니바, 스몰애니바의 부모설정*/
            _bigAniBar.transform.SetParent(_bigAniBarParent.transform, false);
            _bigAniBar.transform.localPosition = new Vector3(_barX, 0, 0); //부모를 지정해둔 뒤 위치를 새로 지정
            _smallAniBar.transform.SetParent(_smallAniBarParent.transform, false);
            _smallAniBar.transform.localPosition = new Vector3(_barX, 0, 0); //부모를 지정해둔 뒤 위치를 새로 지정

            _bigAniBar.transform.GetComponent<BigAniBar>()._smallAniBar = _smallAniBar;
            _smallAniBar.transform.GetComponent<SmallAniBar>()._bigAniBar = _bigAniBar;


            _bigAniBar.GetComponent<Image>().color = new Color((float)252 / 255, (float)198 / 255, (float)247 / 255); //빅애니바의 색상을 변경
            _smallAniBar.GetComponent<Image>().color = new Color((float)252 / 255, (float)198 / 255, (float)247 / 255);

            float _aniBarWidth = _bigAniBar.transform.GetComponent<RectTransform>().rect.width;
            float _width = _aniBarWidth * _audioClip.length / 11.73f;
            _bigAniBar.transform.GetComponent<RectTransform>().sizeDelta = new Vector2(_width, _bigAniBar.transform.GetComponent<RectTransform>().rect.height);
            _smallAniBar.transform.GetComponent<RectTransform>().sizeDelta = new Vector2(_width, _smallAniBar.transform.GetComponent<RectTransform>().rect.height);

            _smallAniBar.transform.GetComponent<SmallAniBar>()._audio = _audioClip;

            itemListControl.AddVoiceDB(_bigAniBar.transform.GetComponent<BigAniBar>(), _smallAniBar.transform.GetComponent<SmallAniBar>());

        }

        pushObjectCreateHist(_loadObject, originNum, objectNum);

    }//끝

    private void PopAniBarDeleteY()
    {
        AniBarDelete A = _aniBarDeleteHistY.Pop();
        float _barX = A.GetBarX();
        float _barWidth = A.GetBarWidth();
        string _animationName = A.GetAnimationName();
        string _parentName = A.GetParentName();
        string _animationText = A.GetAnimationText();
        int _moveOrState = A.GetMoveOrState();
        int _actionOrFace = A.GetActionOrFace();
        int _layerNumber = A.GetLayerNumber();
        float _arriveX = A.GetArriveX();
        float _arriveY = A.GetArriveY();
        float _arriveZ = A.GetArriveZ();
        int originNum = A.GetOriginNumber();
        int _rotation = A.GetRotation();
        GameObject _bigAniBarParent = GameObject.Find("Canvas/ItemMenuCanvas/SchedulerMenu/Scroll View/Viewport/Content/BigScheduler"); //빅바스케줄러를 찾아감
        GameObject _bigAniBar = Instantiate(Resources.Load("Prefab/Big")) as GameObject;
        _bigAniBar.transform.GetComponent<BigAniBar>()._animationName = _animationName;
        _bigAniBar.transform.GetChild(0).GetComponent<Text>().text = _animationText;

        Vector3 _arriveLocation = new Vector3(_arriveX, _arriveY, _arriveZ);

        GameObject _smallAniBarParent = GameObject.Find("Canvas/ItemMenuCanvas/SchedulerMenu/Scroll View/Viewport/Content/SmallScheduler"); //스몰스케줄러를 찾아감
        GameObject _smallAniBar = Instantiate(Resources.Load("Prefab/Small")) as GameObject;
        _smallAniBar.transform.GetComponent<SmallAniBar>()._layerNumber = _layerNumber;
        if (_actionOrFace == 0)
        {
            _smallAniBar.transform.GetComponent<SmallAniBar>()._actionOrFace = false;
        }
        else
        {
            _smallAniBar.transform.GetComponent<SmallAniBar>()._actionOrFace = true;
        }
        if (_rotation == 0)
        {
            _smallAniBar.transform.GetComponent<SmallAniBar>()._rotation = false;
        }
        else
        {
            _smallAniBar.transform.GetComponent<SmallAniBar>()._rotation = true;
        }

        int objectNum = 0;
        Item _item = null;
        Animator _animator = null;
        foreach (Item B in itemListControl._dataBaseHuman)
        {
            if (B.itemName == _parentName)
            {
                objectNum = B._objectNumber;
                _item = B;
                _animator = B.item3d.GetComponent<Animator>();
                break;
            }
        }

        _smallAniBar.transform.GetComponent<SmallAniBar>()._item = _item; //사람객체 리스트를 돌면서 확인해야함
        _smallAniBar.transform.GetComponent<SmallAniBar>()._animator = _animator; //사람객체 리스트를 돌면서 확인해야함
        _smallAniBar.transform.GetComponent<SmallAniBar>()._animationName = _animationName;
        if (_moveOrState == 0)
        {
            _smallAniBar.transform.GetComponent<SmallAniBar>()._moveCheck = false;
        }
        else
        {
            _smallAniBar.transform.GetComponent<SmallAniBar>()._moveCheck = true;
            _smallAniBar.transform.GetComponent<SmallAniBar>()._arriveLocation = _arriveLocation;
        }

        //수정해야하는 파트
        int _parentNumber = 0;
        if (_layerNumber == 0 || _layerNumber == 1) //액션
            _parentNumber = 4;
        else if (_layerNumber == 5) //페이스
            _parentNumber = 1;
        else if (_layerNumber == 4 || _layerNumber == 2) // 핸드
            _parentNumber = 2;
        else if (_layerNumber == 3) //레그
            _parentNumber = 3;

        /*스케줄러에 도달했으니 해당 사람의 스케줄바를 찾음*/
        if (originNum == 2001)
        {
            _bigAniBarParent = _bigAniBarParent.transform.Find("Man" + objectNum).transform.GetChild(_parentNumber + 1).gameObject;
            _smallAniBarParent = _smallAniBarParent.transform.Find("Man" + objectNum).transform.GetChild(_parentNumber).gameObject;
        }
        else if (originNum == 2000)
        {
            _bigAniBarParent = _bigAniBarParent.transform.Find("Daughter" + objectNum).transform.GetChild(_parentNumber + 1).gameObject;
            _smallAniBarParent = _smallAniBarParent.transform.Find("Daughter" + objectNum).transform.GetChild(_parentNumber).gameObject;
        }
        else
        {
            _bigAniBarParent = _bigAniBarParent.transform.Find("Woman" + objectNum).transform.GetChild(_parentNumber + 1).gameObject;
            _smallAniBarParent = _smallAniBarParent.transform.Find("Woman" + objectNum).transform.GetChild(_parentNumber).gameObject;
        }

        _bigAniBar.transform.SetParent(_bigAniBarParent.transform, false);
        _bigAniBar.transform.localPosition = new Vector3(_barX, 0, 0); //부모를 지정해둔 뒤 위치를 새로 지정
        _smallAniBar.transform.SetParent(_smallAniBarParent.transform, false);
        _smallAniBar.transform.localPosition = new Vector3(_barX, 0, 0); //부모를 지정해둔 뒤 위치를 새로 지정

        _bigAniBar.transform.GetComponent<BigAniBar>()._smallAniBar = _smallAniBar;
        _smallAniBar.transform.GetComponent<SmallAniBar>()._bigAniBar = _bigAniBar;

        _bigAniBar.transform.GetComponent<RectTransform>().sizeDelta = new Vector2(_barWidth, _bigAniBar.transform.GetComponent<RectTransform>().rect.height);
        _bigAniBar.transform.GetChild(1).transform.localPosition = new Vector3(-_barWidth / 2, 0, 0);
        _bigAniBar.transform.GetChild(2).transform.localPosition = new Vector3(_barWidth / 2, 0, 0);
        _smallAniBar.transform.GetComponent<RectTransform>().sizeDelta = new Vector2(_barWidth, _smallAniBar.transform.GetComponent<RectTransform>().rect.height);

        if (_layerNumber == 0)
        {
            _bigAniBar.GetComponent<Image>().color = new Color(196 / 255.0f, 244 / 255.0f, 254 / 255.0f); //빅애니바의 색상을 변경
            _bigAniBar.transform.GetChild(1).GetComponent<Image>().color = new Color(196 / 255.0f, 244 / 255.0f, 254 / 255.0f); //왼쪽드래그바의 색상 변경
            _bigAniBar.transform.GetChild(2).GetComponent<Image>().color = new Color(196 / 255.0f, 244 / 255.0f, 254 / 255.0f); //오른쪽 드래그바의 색상 변경
            _smallAniBar.GetComponent<Image>().color = new Color(196 / 255.0f, 244 / 255.0f, 254 / 255.0f);
        }
        else if (_layerNumber == 3)
        {
            _bigAniBar.GetComponent<Image>().color = new Color((float)252 / 255, (float)198 / 255, (float)247 / 255); //빅애니바의 색상을 변경
            _bigAniBar.transform.GetChild(1).GetComponent<Image>().color = new Color((float)252 / 255, (float)198 / 255, (float)247 / 255); //왼쪽드래그바의 색상 변경
            _bigAniBar.transform.GetChild(2).GetComponent<Image>().color = new Color((float)252 / 255, (float)198 / 255, (float)247 / 255); //오른쪽 드래그바의 색상 변경
            _smallAniBar.GetComponent<Image>().color = new Color((float)252 / 255, (float)198 / 255, (float)247 / 255);

        }
        else if (_layerNumber == 2)
        {
            _bigAniBar.GetComponent<Image>().color = new Color((float)252 / 255, (float)198 / 255, (float)247 / 255); //빅애니바의 색상을 변경
            _bigAniBar.transform.GetChild(1).GetComponent<Image>().color = new Color((float)252 / 255, (float)198 / 255, (float)247 / 255); //왼쪽드래그바의 색상 변경
            _bigAniBar.transform.GetChild(2).GetComponent<Image>().color = new Color((float)252 / 255, (float)198 / 255, (float)247 / 255); //오른쪽 드래그바의 색상 변경
            _smallAniBar.GetComponent<Image>().color = new Color((float)252 / 255, (float)198 / 255, (float)247 / 255);
        }
        else
        {
            _bigAniBar.GetComponent<Image>().color = new Color((float)212 / 255, (float)238 / 255, (float)204 / 255); //빅애니바의 색상을 변경
            _bigAniBar.transform.GetChild(1).GetComponent<Image>().color = new Color((float)212 / 255, (float)238 / 255, (float)204 / 255); //왼쪽드래그바의 색상 변경
            _bigAniBar.transform.GetChild(2).GetComponent<Image>().color = new Color((float)212 / 255, (float)238 / 255, (float)204 / 255); //오른쪽 드래그바의 색상 변경
            _smallAniBar.GetComponent<Image>().color = new Color((float)212 / 255, (float)238 / 255, (float)204 / 255);
        }

        itemListControl.AddActionDB(_bigAniBar.transform.GetComponent<BigAniBar>(), _smallAniBar.transform.GetComponent<SmallAniBar>());
        pushAniBarCreateHist(_bigAniBar, _smallAniBar, _animationName, objectNum, _smallAniBar.GetComponent<SmallAniBar>()._voice == true ? 0 : 1);
    }//끝

    private void PopAniBarVoiceDeleteY()
    {
        AniBarVoiceDelete A = _aniBarVoiceDeleteHistY.Pop();
        float _barX = A.GetBarX();
        float _barWidth = A.GetBarWidth();
        string _voiceName = A.GetVoiceName();
        string _parentName = A.GetParentName();
        int _voiceGender = A.GetVoiceGender();
        int originNum = A.GetOriginNumber();

        Debug.Log(_barX);
        Debug.Log(_barWidth);
        Debug.Log(_voiceName);
        Debug.Log(_parentName);
        Debug.Log(_voiceGender);
        Debug.Log(originNum);

        GameObject _bigAniBarParent = GameObject.Find("Canvas/ItemMenuCanvas/SchedulerMenu/Scroll View/Viewport/Content/BigScheduler"); //빅바스케줄러를 찾아감
        GameObject _bigAniBar = Instantiate(Resources.Load("Prefab/VoiceBig")) as GameObject;
        _bigAniBar.transform.GetComponent<BigAniBar>()._animationName = _voiceName;
        _bigAniBar.transform.GetChild(0).GetComponent<Text>().text = _voiceName;

        AudioClip _audioClip;
        if (_voiceGender == 1)
        {
            _audioClip = Resources.Load<AudioClip>("Voice/son/" + _voiceName);
        }
        else
        {
            _audioClip = Resources.Load<AudioClip>("Voice/yuinna/" + _voiceName);
        }

        /*스몰애니바를 생성*/
        GameObject _smallAniBarParent = GameObject.Find("Canvas/ItemMenuCanvas/SchedulerMenu/Scroll View/Viewport/Content/SmallScheduler"); //스몰스케줄러를 찾아감
        GameObject _smallAniBar = Instantiate(Resources.Load("Prefab/Small")) as GameObject;
        _smallAniBar.transform.GetComponent<SmallAniBar>()._voiceGender = _voiceGender;
        _smallAniBar.transform.GetComponent<SmallAniBar>()._voice = true;

        /*찾아야하는 정보*/
        int objectNum = 0;
        Item _item = null;
        Animator _animator = null;
        foreach (Item B in itemListControl._dataBaseHuman)
        {
            if (B.itemName == _parentName)
            {
                objectNum = B._objectNumber;
                _item = B;
                _animator = B.item3d.GetComponent<Animator>();
                break;
            }
        }

        _smallAniBar.transform.GetComponent<SmallAniBar>()._item = _item;
        _smallAniBar.transform.GetComponent<SmallAniBar>()._animator = _animator;
        _smallAniBar.transform.GetComponent<SmallAniBar>()._animationName = _voiceName;

        int _parentNumber = 5;

        /*스케줄러에 도달했으니 해당 사람의 스케줄바를 찾음*/
        if (originNum == 2001)
        {
            _bigAniBarParent = _bigAniBarParent.transform.Find("Man" + objectNum).transform.GetChild(_parentNumber + 1).gameObject;
            _smallAniBarParent = _smallAniBarParent.transform.Find("Man" + objectNum).transform.GetChild(_parentNumber).gameObject;
        }
        else if (originNum == 2000)
        {
            _bigAniBarParent = _bigAniBarParent.transform.Find("Daughter" + objectNum).transform.GetChild(_parentNumber + 1).gameObject;
            _smallAniBarParent = _smallAniBarParent.transform.Find("Daughter" + objectNum).transform.GetChild(_parentNumber).gameObject;
        }
        else
        {
            _bigAniBarParent = _bigAniBarParent.transform.Find("Woman" + objectNum).transform.GetChild(_parentNumber + 1).gameObject;
            _smallAniBarParent = _smallAniBarParent.transform.Find("Woman" + objectNum).transform.GetChild(_parentNumber).gameObject;
        }

        /*빅애니바, 스몰애니바의 부모설정*/
        _bigAniBar.transform.SetParent(_bigAniBarParent.transform, false);
        _bigAniBar.transform.localPosition = new Vector3(_barX, 0, 0); //부모를 지정해둔 뒤 위치를 새로 지정
        _smallAniBar.transform.SetParent(_smallAniBarParent.transform, false);
        _smallAniBar.transform.localPosition = new Vector3(_barX, 0, 0); //부모를 지정해둔 뒤 위치를 새로 지정

        _bigAniBar.transform.GetComponent<BigAniBar>()._smallAniBar = _smallAniBar;
        _smallAniBar.transform.GetComponent<SmallAniBar>()._bigAniBar = _bigAniBar;


        _bigAniBar.GetComponent<Image>().color = new Color((float)252 / 255, (float)198 / 255, (float)247 / 255); //빅애니바의 색상을 변경
        _smallAniBar.GetComponent<Image>().color = new Color((float)252 / 255, (float)198 / 255, (float)247 / 255);

        float _aniBarWidth = _bigAniBar.transform.GetComponent<RectTransform>().rect.width;
        float _width = _aniBarWidth * _audioClip.length / 11.73f;
        _bigAniBar.transform.GetComponent<RectTransform>().sizeDelta = new Vector2(_width, _bigAniBar.transform.GetComponent<RectTransform>().rect.height);
        _smallAniBar.transform.GetComponent<RectTransform>().sizeDelta = new Vector2(_width, _smallAniBar.transform.GetComponent<RectTransform>().rect.height);

        _smallAniBar.transform.GetComponent<SmallAniBar>()._audio = _audioClip;

        itemListControl.AddVoiceDB(_bigAniBar.transform.GetComponent<BigAniBar>(), _smallAniBar.transform.GetComponent<SmallAniBar>());
        pushAniBarCreateHist(_bigAniBar, _smallAniBar, _voiceName, objectNum, _smallAniBar.GetComponent<SmallAniBar>()._voice == true ? 0 : 1);

    }//끝

    private void PopDressChangeY()
    {
        DressChange A = _dressChangeHistY.Pop();
        string[] _name = A.GetName();
        string prev = A.GetPrev();
        string objectName = A.GetObjectName();
        int objectNum = A.GetObjectNum();
        int originNum = A.GetOriginNum();
        HumanItem _clickedHumanItem;

        GameObject _newCloth = null;
        string _prev = "null";

        foreach (Item B in itemListControl._dataBaseHuman)
        {
            if (B._objectNumber == objectNum && B._originNumber == originNum)
            {
                _newCloth = B.item3d;
            }
        }
        _clickedHumanItem = _newCloth.GetComponent<ItemObject>()._thisHuman;
        if (_name[0] == "shirt")
        {
            if (_clickedHumanItem._shirt != null)
            {
                _prev = _clickedHumanItem._shirt.name;
                _clickedHumanItem._shirt.SetActive(false);
            }
            if (prev.CompareTo("null") == 0) _clickedHumanItem._shirt = null;
            else if (prev.CompareTo("null") != 0)
            {
                _newCloth = _newCloth.transform.Find(_name[0] + "/" + prev).gameObject;
                _clickedHumanItem._shirt = _newCloth;
            }
            pushDressChangeHist(_name, _prev, objectName, objectNum, originNum);
        }
        else if (_name[0] == "pant")
        {
            if (_clickedHumanItem._pant != null)
            {
                _prev = _clickedHumanItem._pant.name;
                _clickedHumanItem._pant.SetActive(false);
            }
            if (prev.CompareTo("null") == 0) _clickedHumanItem._pant = null;
            else if (prev.CompareTo("null") != 0)
            {
                _newCloth = _newCloth.transform.Find(_name[0] + "/" + prev).gameObject;
                _clickedHumanItem._pant = _newCloth;
            }
            pushDressChangeHist(_name, _prev, objectName, objectNum, originNum);
        }
        if (prev.CompareTo("null") != 0) _newCloth.SetActive(true);
    }
    private void PopTilingY()
    {
        TilingChange _ti = _tilingHistY.Pop();
        int _originNum = _ti.GetOriginNum();
        int _objectNum = _ti.GetObjectNum();
        int _tileOriginNum = _ti.GetTileOriginNum();
        Vector3 _scale = _ti.GetScale();
        Vector3 _scaleZ;
        GameObject _go = null;

        foreach (Item B in itemListControl._dataBaseWall)
        {
            if (B._originNumber == _originNum && B._objectNumber == _objectNum)
            {
                _go = B.item3d;
                break;
            }
        }
        MeshRenderer _mesh = _go.GetComponent<MeshRenderer>();

        if (_tileOriginNum == -1)
        {
            _scaleZ = _mesh.material.mainTextureScale;
            _mesh.material.mainTextureScale = _scale;
            pushTilingHistY(_originNum, _objectNum, _scaleZ, -1);
        }
        else
        {
            Sprite tmp = Instantiate(itemListControl.GetImages(3, _tileOriginNum));
            if (tmp != null) _mesh.material.mainTexture = tmp.texture;
            else _mesh.material.mainTexture = null;
            _go.GetComponent<ItemObject>()._placeNumber = _tileOriginNum;
            pushTilingHistY(_originNum, _objectNum, _scale, -1);
        }
        clickedPlaceControl.ResetForHistory();
    }

    private void PopTileY()
    {
        TileChange A = _tileHistY.Pop();
        int _originNumber = A.GetOriginNum();
        int _objectNumber = A.GetObjectNum();
        int _buildingNumber = A.GetBuildingOriginNum();
        GameObject _target = null;

        foreach (Item B in itemListControl._dataBaseWall)
        {
            if (B._originNumber == _originNumber && B._objectNumber == _objectNumber)
            {
                _target = B.item3d;
            }
        }
        Debug.Log(_target);

        /* 클릭된 Place 객체의 MeshRenderer 컴포넌트를 담음 */
        MeshRenderer _clickedMeshRenderer = _target.GetComponent<MeshRenderer>();

        /* Slot의 OriginNumber에 해당하는 Sprite 정보를 가져옴 */
        Sprite _tmp = itemListControl.GetImages(3, _buildingNumber);

        /* 존재하지 않는 Sprite이면 */
        if (_tmp == null)
        {
            /* 건물의 OriginNumber를 저장 */
            int _buildingOriginNumber = _target.GetComponent<ItemObject>()._thisItem._originNumber;

            /* 건물들의 3DObject가 들어있는 디렉터리 경로 지정 */
            System.IO.DirectoryInfo _dir = new System.IO.DirectoryInfo("Assets/Resources/Item/Wall/Build");

            /* 디렉터리 탐색 */
            foreach (System.IO.FileInfo _file in _dir.GetFiles())
            {
                /* 파일의 OriginNumber와 일치하면 */
                if (_file.Name.Split('_')[0] == _buildingOriginNumber.ToString() && _file.Extension.ToLower() == ".prefab")
                {
                    /* Resource를 통해 건물 정보 저장 */
                    GameObject _building = Resources.Load<GameObject>("Item/Wall/Build/" + _file.Name.Substring(0, _file.Name.Length - 7));

                    /* 머터리얼을 복제해서 클릭된 건물의 머터리얼로 지정 */
                    _clickedMeshRenderer.material = Instantiate(_building.GetComponent<Transform>().GetChild(0).GetComponent<MeshRenderer>().sharedMaterial);

                    break; //반복문 빠져나옴
                }
            }
        }

        /* 존재하는 Sprite이면 */
        else
        {
            /* Sprite 복제 */
            Sprite _tmpClone = Instantiate(_tmp);

            /* Texture로 적용 */
            _clickedMeshRenderer.material.mainTexture = _tmpClone.texture;
        }

        /* 클릭된 건물의 ItemObject에 PlaceNumber 지정 */
        pushTileHist(_originNumber, _objectNumber, _target.GetComponent<ItemObject>()._placeNumber);
        _target.GetComponent<ItemObject>()._placeNumber = _buildingNumber;
    }

    private void PopHandY()
    {
        HandChange hg = _handHistY.Pop();
        int _originNum = hg.GetOriginNum();
        int _objectNum = hg.GetObjectNum();
        bool _isLeft = hg.GetIsLeft();
        string _objectName = hg.GetObjectName();
        Item _thisItem = hg.GetItem();
        Item _thisHuman = null;
        HumanItem _thisHumanItem = null;
        GameObject _handObjectClone = null;
        Transform _hand;
        string _itemName = "null";

        Debug.Log("HandY " + _objectName);
        if (_thisItem != null) Debug.Log("HandY " + _thisItem.itemName);

        foreach (Item A in itemListControl._dataBaseHuman)
        {
            if (A._originNumber == _originNum && A._objectNumber == _objectNum)
            {
                _thisHuman = A;
                _thisHumanItem = A.item3d.GetComponent<ItemObject>()._thisHuman;
                break;
            }
        }

        Debug.Log(_thisHuman.item3d.GetComponent<ItemObject>()._thisItem.itemName);
        Debug.Log(_thisHuman.item3d.GetComponent<ItemObject>()._thisHuman._humanNumber);
        Debug.Log(_thisHumanItem);

        Vector3 _pos = new Vector3(1, 0, 0);
        if (_isLeft) //왼손이면
        {
            /* 왼손을 변수로 저장 
            if (_clickedItemControl._clickedItem._originNumber == 2002) //여자 객체인 경우 본 계층이 다르기 때문에 별도로 지정
            {
                _hand = _clickedItemControl._clickedItem.item3d.transform.GetChild(0).GetChild(1).GetChild(0).GetChild(2).GetChild(0).GetChild(0).GetChild(0).GetChild(0).GetChild(0).GetChild(0).GetChild(0);
            }
            else
            {
            */
            _hand = _thisHuman.item3d.transform.GetChild(0).GetChild(1).GetChild(0).GetChild(2).GetChild(0).GetChild(0).GetChild(1).GetChild(0).GetChild(0).GetChild(0);
            //}

        }
        else //오른손이면
        {
            /* 오른손을 변수로 저장하고 
            if (_clickedItemControl._clickedItem._originNumber == 2002) //여자 객체인 경우
            {
                _hand = _clickedItemControl._clickedItem.item3d.transform.GetChild(0).GetChild(1).GetChild(0).GetChild(2).GetChild(0).GetChild(0).GetChild(0).GetChild(2).GetChild(0).GetChild(0).GetChild(0);
            }
            else
            {
            */
            _hand = _thisHuman.item3d.transform.GetChild(0).GetChild(1).GetChild(0).GetChild(2).GetChild(0).GetChild(0).GetChild(2).GetChild(0).GetChild(0).GetChild(0);
            //}
        }


        if (_isLeft)
        {
            if (_thisHumanItem._leftHandItem != null)
            {
                _itemName = _thisHumanItem._leftHandItem.itemName;
                HistoryController.pushHandHist(_thisHuman._originNumber, _thisHuman._objectNumber, _isLeft, _itemName, _thisHumanItem._leftHandItem);
                if (!_objectName.Equals("null") && !_thisHumanItem._leftHandItem.itemName.Equals(_thisItem.itemName))
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
            _handObjectClone = Instantiate(_thisHumanItem._leftHandItem.item3d); //물건 복제
            _handObjectClone.transform.SetParent(_hand); //손을 부모로 설정
            _handObjectClone.name = _thisHumanItem._leftHandItem.itemName; //이름 지정

            /*
            if (_thisHuman._originNumber == 2002) //여자 객체인 경우 남자, 딸 객체와 본이 다르기 때문에 별도로 지정해주어야한다.
            {
                _handObjectClone.transform.position = _hand.transform.GetChild(2).GetChild(0).transform.position; //세번째 손가락의 위치가 다름
            }
            else
            {
            */
            _handObjectClone.transform.position = _hand.transform.GetChild(2).transform.position; //세번째 손가락 위치로 지정
            //}
            _handObjectClone.transform.GetChild(0).transform.localPosition += new Vector3(0, 2, 0) * _handObjectClone.GetComponentInChildren<Collider>().bounds.extents.x; //_handObjectClone 의 위치 미세조정
            _handObjectClone.transform.rotation = _hand.rotation; //회전 각도 지정

        }

        else
        {
            if (_thisHumanItem._rightHandItem != null)
            {
                _itemName = _thisHumanItem._rightHandItem.itemName;
                HistoryController.pushHandHist(_thisHuman._originNumber, _thisHuman._objectNumber, _isLeft, _itemName, _thisHumanItem._rightHandItem);
                if (!_objectName.Equals("null") && !_thisHumanItem._rightHandItem.itemName.Equals(_thisItem.itemName))
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
            _handObjectClone = Instantiate(_thisItem.item3d); //물건 복제
            _handObjectClone.transform.SetParent(_hand); //손을 부모로 설정
            _handObjectClone.name = _thisItem.itemName; //이름 지정
            if (_thisHuman._originNumber == 2002) //여자 객체인 경우 남자, 딸 객체와 본이 다르기 때문에 별도로 지정해주어야한다.
            {
                _handObjectClone.transform.position = _hand.transform.GetChild(2).GetChild(0).transform.position; //세번째 손가락의 위치가 다름
            }
            else
            {
                _handObjectClone.transform.position = _hand.transform.GetChild(2).transform.position; //세번째 손가락 위치로 지정
            }
            _handObjectClone.transform.GetChild(0).transform.localPosition += new Vector3(0, 2, 0) * _handObjectClone.GetComponentInChildren<Collider>().bounds.extents.x; //_handObjectClone 의 위치 미세조정
            _handObjectClone.transform.rotation = _hand.rotation; //회전 각도 지정

        }

    }
    #endregion  Y
}
