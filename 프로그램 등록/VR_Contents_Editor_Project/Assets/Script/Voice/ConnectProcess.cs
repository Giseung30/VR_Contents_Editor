using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Diagnostics;
using UnityEngine.UI;
using System.IO;
using UnityEditor;

public class ConnectProcess : MonoBehaviour
{
    /**
* date 2019.03.22
* author INHO
* desc
* 원하는 음성 텍스트를 입력후, 버튼을 누르면
* 자동적으로 음성합성이 진행되도록 구성.
*/

    [Header("Script")]
    public VoiceController _voiceController; // 음성 타입에 따라 모델이 바뀌도록!!

    Process cmd = new Process();
    public InputField _title;
    public InputField _sentence;
    public GameObject _progressImage;
    public GameObject _voiceButton;

    bool _isMakingVoice; // 음성을 만드는 중인지?
    int _countFiles; // 현재 폴더 파일 갯수 구하기.
    int currentCountFiles; // 실시간 폴더 파일 갯수 구하기. -> 갯수 달라짐으로 음성 만들어 졌는지 판단!
    Text _text; // 텍스트는 무엇인지? ==> Make <-> Cancel 버튼시 Make 버튼 함수 파라미터 값으로 이용하기 위해 저장함.
    int modelType; // 모델 타입 => 어느 모델의 목소리인지?

    string _dirPath;

    void Start()
    {
        _dirPath = Application.persistentDataPath + "/DeepLeaning/samples";

        /* 현재 폴더에 존재하는 파일 갯수를 구한다. */
        _countFiles = getCountFiles();

        /* 외부 프로세서 실행시 보이지 않도록 설정. */
        cmd.StartInfo.CreateNoWindow = true;
        cmd.StartInfo.UseShellExecute = false;

        /* 외부프로세서 (cmd)를 지정하며, 하위 디렉토리를 설정한다. */
        cmd.StartInfo.FileName = @"cmd.exe";
        cmd.StartInfo.WorkingDirectory = Application.persistentDataPath + "/DeepLeaning";
    }

    void Update()
    {
        /* 외부 프로세서가 겹치지 않도록 체크해줌. */
        CheckProcess();
    }

    /* 외부 프로세서를 이용해 동적으로 문장 만들어주는 함수. */
    public void MakingVoice(Text text)
    {
        _text = text;
        string model = null;
        modelType = _voiceController._voiceType;

        /* 모델 타입을 INT 값으로 설정! */
        switch (modelType)
        {
            case 1: // 손석희 타입
                model = "son";
                break;
            case 2: // 윤인호 타입
                model = "yun";
                break;
            case 3: // 유인나 타입
                model = "yuinna";
                break;
        }


        /* 현재 존재하는 파일 갯수를 가져온다. */
        _countFiles = getCountFiles();

        /* InputField에 타이핑 했던 만들 문장을 받아온다. */
        text.text = _sentence.text;

        /* 만들 텍스트를 입력하여 python 코드를 실행. -> 동적으로 생성됨. */
        cmd.StartInfo.Arguments = "/K python synthesizer.py --load_path logs/" + model + " --text \"" + text.text + "\"";
        //cmd.StartInfo.Arguments = "/K dir";

        /* try-catch 문으로 잡아준 이유 : 최초 cmd 만들어 줄 시 해당 if 문이 아닌 오류에 걸리는 현상 발생! */
        try
        {
            /* cmd가 있을시! */
            if (!cmd.HasExited)
            {
                //UnityEngine.Debug.Log("음성 파일 생성중이니 조금만 기다려 주세요!");
            }
            /* cmd가 없을시! (2번째 음성 합성 이후 여기 걸림) */
            else
            {
                cmd.Start();
                _isMakingVoice = true;
            }
        }
        catch (System.Exception e)
        {
            /* 최초 cmd 생성 할때, 해당 구문에 걸린다! */
            cmd.Start();
            _isMakingVoice = true;
        }

        /* 음성합성이 끝나면, 텍스트 내용 초기화! */
        _sentence.text = "";

        /* Cancel <-> Make 버튼이 바뀌어야 한다! */
        ChangeButton();
    }

    public void CheckProcess()
    {

        if (_isMakingVoice)
        {
            /* 실시간으로 파일 갯수를 구한다. => 파일 갯수가 달라지면, 음성 파일이 만들어 졌음을 의미! */
            currentCountFiles = getCountFiles();
            //UnityEngine.Debug.Log("현재 음성 합성파일 생성 중!");
            //UnityEngine.Debug.Log("파일 갯수 : " + currentCountFiles);

            /* 프로그레스 (로딩) 이미지 활성화 */
            _progressImage.SetActive(true);

            /* 음성 합성시 그래프 + 음성파일 ==> 2개가 만들어 진다! */
            if (_countFiles + 2 <= currentCountFiles)
            {
                /* 외부 프로세서(cmd)를 종료시킨다. */
                cmd.CloseMainWindow();

                /* 프로그레스 (로딩) 이미지 비활성화 */
                _progressImage.SetActive(false);
                _isMakingVoice = false;

                /* Cancel <-> Make 버튼이 바뀌어야 한다! */
                ChangeButton();

                /* 해당 생성된 파일들을 디렉터리 정리 및 동적 생성 */
                _voiceController.MoveVoiceFile(_dirPath, _title.text, modelType);

#if UNITY_EDITOR
                /* 동적으로 폴더에 있는 데이터 임포트! -> 유니티 상에서 바로 사용 할 수 있도록
                 * 에디터 모드에만 적용되는 소스를 전처리기 적용 우회시켜 빌드 모드에서도 적용할 수 있도록 설정!
                 */
                ImportAsset.NewImportAsset_Dic(Application.persistentDataPath + "/Resources/Voice");
#endif
            }

        }

    }

    /* 현재 폴더의 파일 갯수를 구하는 함수 */
    public int getCountFiles()
    {
        DirectoryInfo dir = new DirectoryInfo(_dirPath);
        FileInfo[] fileInfos = dir.GetFiles();
        return fileInfos.Length;
    }

    /* 취소 버튼을 눌렀을 시..! */
    public void CancelClick()
    {
        /* Python 프로그램은 cmd창 종료해도 지속되므로, 이름을 찾아가 종료 시켜야 한다! */
        Process[] processes = Process.GetProcessesByName("Python");
        foreach (Process pro in processes) { pro.CloseMainWindow(); }

        /* 외부 프로세서(cmd)를 종료시킨다. */
        cmd.CloseMainWindow();

        /* 프로그레스 (로딩) 이미지 비활성화 */
        _progressImage.SetActive(false);
        _isMakingVoice = false;

        /* Cancel <-> Make 버튼이 바뀌어야 한다! */
        ChangeButton();
    }

    /* Cancel <-> Make 버튼이 바뀌도록 구성. */
    public void ChangeButton()
    {
        /* 어떤 컴포넌트를 수정해야 하는지 받아옴. */
        Button button = this.gameObject.GetComponent<Button>();
        Text text = this.transform.GetChild(0).gameObject.GetComponent<Text>();

        button.onClick.RemoveAllListeners();

        if (_isMakingVoice)
        {
            /* 버튼의 기능(함수)이 Make -> Cancel 로 바뀐다! */
            button.onClick.AddListener(() => this.CancelClick());
            text.text = "Cancel";
        }
        else
        {
            /* 버튼의 기능(함수)이 Cancel -> Make 로 바뀐다! (_text 값은 이전에 저장했었던 오브젝트를 미리 받아옴.) */
            button.onClick.AddListener(() => this.MakingVoice(_text));
            text.text = "Make";
        }

    }
}
