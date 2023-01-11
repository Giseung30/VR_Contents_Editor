using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using UnityEngine.UI;

public class VoiceController : MonoBehaviour {
    /**
* date 2019.03.26
* author GS,INHO
* desc
*  VoiceCanvas와 VoiceMadeCanvas에 대한 모든 컨트롤을 담담하는 스크립트.
*/

    [Header("UI")]
    public GameObject _voiceFileName;
    public GameObject _voiceMadeCanvas;

    [Header("Dynamic")]
    /* VoiceMadeCanvas의 제목 입력필드 */
    public InputField _titleInputField;
    /* VoiceMadeCanvas의 대사 입력필드 */
    public InputField _voiceInputField;
    /* VoiceMadeCanvas의 VoiceTypeButton이 전달하는 음성 타입 인자 */
    public int _voiceType;

    [Header("Parent Panel")]
    public GameObject _manPanel;
    public GameObject _womanPanel;

    [Header("SampleButton")]
    public GameObject _sampleButton;
    public AudioClip _audioClip;

    GameObject _dynamicPanel = null; //어느 부모 패널에 생성 되는지?
    GameObject _instantiateSample; //어느 버튼에 오디오가 붙는지?
    bool _runAudioConnect; //오디오 연결 중인지?

    string _model;
    string _title;
    string dir_path;

    private void Start()
    {
        dir_path = Static.STATIC.dir_path;
        /* 기본 음성 값 1로 지정 */
        _voiceType = 1;

        string[] extends = { ".wav", ".mp3", ".m4a", "mp4" };

        for (int i = 0; i < extends.Length; i++)
        {
            /* 음성을 불러오자! */
            OnLoadVoice("son", extends[i], _manPanel, _sampleButton, 1);
            //OnLoadVoice(Application.persistentDataPath+"/Resources/Voice/yun", extends[i], _manPanel, _sampleButton,2);
            OnLoadVoice("yuinna", extends[i], _womanPanel, _sampleButton, 3);
        }
    }

    private void Update()
    {
        InitializeInputField();
        ConnectAudio();
    }

    /* InputField 초기화 함수 */
    private void InitializeInputField()
    {
        /* VoiceMadeCanvas가 비활성화 상태이면 */
        if (!_voiceMadeCanvas.activeSelf)
        {
            /* 제목 InputField 초기화 */
            _titleInputField.text = "";
            /* 대사 InputField 초기화 */
            _voiceInputField.text = "";
        }
    }

    void ConnectAudio()
    {
        if (_runAudioConnect)
        {
            while (_audioClip == null)
            {
                /* 오디오 클립 연결 될 때까지 -> .meta 파일을 생성해야 하는 로딩시간 때문에 한번에 못만듬! */
                _audioClip = Resources.Load<AudioClip>("Voice/" + _model + "/" + _title);
            }

            /* 연결 됬으면 끊어준다! */
            _instantiateSample.GetComponent<AudioSource>().clip = _audioClip;
            _instantiateSample.GetComponent<VoiceButton>()._voiceType = _voiceType;
            _runAudioConnect = false;
            _audioClip = null;
        }
    }


    public void MoveVoiceFile(string path, string title, int voiceType)
    {
        DirectoryInfo dir = new DirectoryInfo(path);
        _title = title;

        /* 생성되는 파일을 옮길 경로 및 이름 표기 */
        string loadDir = dir_path + "/Resources/Voice";

        foreach (FileInfo file in dir.GetFiles())
        {
            /* 메타 파일이 생성됬을 때 지우도록! */
            DeleteFile(path, file.Name, ".meta");

            /* 생성된 음성 파일 -> 각 모델 디렉토리 이동 */
            if (file.Extension.ToLower().CompareTo(".wav") == 0)
            {
                switch (voiceType)
                {
                    case 1: //손석희
                        _model = "son";
                        _dynamicPanel = _manPanel;
                        break;
                    case 2: //윤인호
                        _model = "yun";
                        _dynamicPanel = _manPanel;
                        break;
                    case 3: //유인나
                        _model = "yuinna";
                        _dynamicPanel = _womanPanel;
                        break;
                }

                /* 폴더 이름을 바꿔준다. */
                loadDir += "/" + _model + "/" + title + ".wav";

                /* 같은 이름을 가진 파일이 있으면, 덮어 씌우도록 설정. */
                if (ExistSameNameFile(loadDir))
                {
                    file.CopyTo(@loadDir, true);
                    file.Delete();
                    /* 유니티 Slot에도 덮어 씌울 수 있도록 코드 수정 필요 */
                }
                else // 새 파일 생성 시 버튼도 같이 생성!
                {
                    file.MoveTo(loadDir);
                    /* 음성 Slot을 만들어준다. */
                    MakingVoiceSlot(title, ".wav", _dynamicPanel);
                }
            }

            /* 생성된 그래프 파일 -> 삭제 */
            else if (file.Extension.ToLower().CompareTo(".png") == 0)
            {
                file.Delete(); // 파일 삭제
            }

        }//foreach
    }

    /* 음성이 만들어질때마다 Slot을 만들어 주기 */
    public void MakingVoiceSlot(string name, string extension, GameObject parentPanel)
    {
        /* 샘플 버튼 생성 */
        _instantiateSample = Instantiate(_sampleButton) as GameObject;
        /* 보통 샘플버튼은 비활성화 되어있는 경우가 많으므로 */
        _instantiateSample.SetActive(true);
        /* 샘플 버튼의 이름 설정 */
        _instantiateSample.name = name;
        /* 샘플 버튼의 텍스트 설정 */
        _instantiateSample.transform.GetChild(0).GetComponent<Text>().text = name;
        /* 샘플 버튼의 부모 설정 */
        _instantiateSample.transform.SetParent(parentPanel.transform);
        /* 샘플 버튼의 스케일 설정 */
        _instantiateSample.transform.localScale = new Vector3(1, 1, 1);

        _runAudioConnect = true;
    }

    /* 음성 Load할때마다 Slot을 만들어 주기 */
    void LoadingVoiceSlot(string path, string name, GameObject parentPanel,int voiceType)
    {
        path = "Voice/" + path;

        /* 샘플 버튼 생성 */
        _instantiateSample = Instantiate(_sampleButton) as GameObject;
        /* 보통 샘플버튼은 비활성화 되어있는 경우가 많으므로 */
        _instantiateSample.SetActive(true);
        /* 샘플 버튼의 이름 설정 */
        _instantiateSample.name = name;
        /* 샘플 버튼의 텍스트 설정 */
        _instantiateSample.transform.GetChild(0).GetComponent<Text>().text = name;
        /* 샘플 버튼의 부모 설정 */
        _instantiateSample.transform.SetParent(parentPanel.transform);
        /* 샘플 버튼의 스케일 설정 */
        _instantiateSample.transform.localScale = new Vector3(1, 1, 1);

        _instantiateSample.GetComponent<AudioSource>().clip = Resources.Load<AudioClip>(path + "/" + name);
        _instantiateSample.GetComponent<VoiceButton>()._voiceType = voiceType;
    }

    /* 해당 경로에 디렉터리가 같은 파일(=> 이름이 같은 파일)이 존재하는지? */
    bool ExistSameNameFile(string path)
    {
        FileInfo check = new FileInfo(path);
        if (check.Exists) return true;
        else return false;
    }

    /* 메타파일을 지우기 위해 만든 메서드 */
    void DeleteFile(string path, string name,string extension)
    {
        /* 지울 메타파일을 경로를 통해 들고온다 */
        FileInfo file = new FileInfo(path + "/" + name + extension);
        if (file.Exists) file.Delete();
    }

    /* LoadVoice 는 불러올 시 목소리도 연동되야 하므로, 따로 메서드 만듦.  */
    void OnLoadVoice(string path, string extension, GameObject parentObject, GameObject _sampleButton,int voiceType)
    {
        DirectoryInfo dir = new DirectoryInfo(dir_path + "/Resources/Voice/" + path);

        foreach (FileInfo file in dir.GetFiles())
        {
            string name = file.Name;
            name = name.Substring(0, name.Length - 4); // 확장자를 지워준다.
            if (file.Extension.ToLower().CompareTo(extension) == 0)
            {
                /* 음성 Slot을 만들어준다. */
                LoadingVoiceSlot(path, name, parentObject,voiceType);
            }
        }
    }
    
    /* 선택된 버튼(음성)을 삭제 버튼을 클릭! */
    public void OnClickDelete()
    {
        /* 해당 음성을 찾아가 삭제한다! */
        string deleteName = _voiceFileName.GetComponent<Text>().text;
        GameObject deleteObject = null;
        GameObject parentObject = null;

        if (deleteName == "") return;

        switch (_voiceType)
        {
            case 1: //손석희
                _model = "son";
                parentObject = _manPanel;
                break;
            case 2: //윤인호
                _model = "yun";
                parentObject = _manPanel;
                break;
            case 3: //유인나
                _model = "yuinna";
                parentObject = _womanPanel;
                break;
        }

        /* 파일 삭제 */
        DeleteFile("Assets/Resources/Voice/" + _model, deleteName, ".meta");
        DeleteFile("Assets/Resources/Voice/" + _model, deleteName, ".wav");

        /* 유니티 상에서 버튼 삭제 */
        deleteObject = parentObject.transform.Find(deleteName).gameObject;
        Destroy(deleteObject);

        /* 선택된 text 값은 초기화 */
        _voiceFileName.GetComponent<Text>().text = "";

    }
}
