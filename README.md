# VR을 이용한 대인 관계 개선 시스템 개발
갈등이 고조되는 상황을 재현하여 서로의 행동을 되돌아보는 계기를 제공하고 **대인 관계를 개선하는 프로그램**을 개발한다.

## 📖 연구 개요
+ 사람 사이의 관계가 점차 각박해지고 현대 사회 풍조에서 인간 간의 갈등과 관계를 개선할 수 있는 방법이 필요하다. 본 연구에서는 대인 관계의 개선에 있어 VR 기술을 적용하여 과거를 회상할 수 있는 시스템 및 이와 관련된 전반적 기술을 연구한다. 기술적으로는 과거 회상용 VR 콘텐츠를 개발할 수 있는 VR 콘텐츠 저작도구를 개발하고, 저작도구를 통해 개발한 VR 콘텐츠를 목적에 맞게 재생할 수 있는 전용 재생기를 구현한다. VR 콘텐츠를 생성하고 이를 이용하는 것에서 끝나면 대인 관계의 개선 여부에 대한 판단이 불가능하기 때문에 개선 정도를 평가할 수 있는 평가 모듈까지 개발하고 이를 연동하여 하나의 **대인 관계 개선 시스템**을 도출한다.

## ⏲ 연구 기간
+ 2019.06.01 ~ 2020.05.31
+ Development of Fence-Mending System using Virtual Reality
+ 참여 연구원수 : 7명

## ⚙ 개발 환경
+ 개발툴 : `Unity 2018.2.0f2`
+ VR Headset : `Samsung HMD Odyssey`, `Samsung Gear VR`
+ VR 플랫폼 : `Windows Mixed Reality`
+ 3D 모델링 애니메이션 제작 : `3ds Max`
+ Database : `SQLlite`, `Json`, `MySQL`
+ Version Control : `Tortoise SVN`
+ 웹 설문지 : `gnuboard5`, `PHP`

## 📋 본인 연구 수행 내용
### 음성 패널 구현
> 스케줄러가 재생될 때 음성을 재생하기 위해서 음성 패널을 구현했다. 음성 패널을 통해 음성을 합성하면 음성 재생 구간을 설정할 수 있게 된다.

<div align="center">
  <img width="75%" height="75%" src="https://user-images.githubusercontent.com/60832219/210264150-971f7e6d-a449-4c96-89fa-79a2b68177ba.png"/>
</div>

+ 음성 합성을 진행하는 데 필요한 것은 음성 대사, 음성 제목, 음성 타입이다. VoiceMadeCanvas는 음성 제목과 음성 대사를 입력받는다. 그리고 Man 타입 또는 Woman 타입 중에 음성을 선택한 뒤 Make 버튼을 누르면 20~30초 뒤 음성 파일(.wav)을 Asset에 저장한다. VoiceCanvas는 VoiceMadeCanvas에서 생성한 음성 파일을 음성 버튼(VoiceButton)으로 나열한다. 음성 버튼은 음성 제목과 재생 버튼을 가지고 있고, 재생 버튼을 누르면 입력된 음성 대사를 재생한다.

<div align="center">
  <img width="75%" height="75%" src="https://user-images.githubusercontent.com/60832219/210264181-cac12788-a2b0-4751-ae93-1de8a2d29a8c.png"/>
</div>

+ 음성 합성 패널의 대략적인 구조도는 이러하다. `VoiceController.cs`는 VoiceMadeCanvas와 VoiceCanvas 객체를 총괄하는 스크립트이다. 사용자가 입력한 제목을 변수 _titleInputField(InputField), 대사를 변수 _voiceInputField(InputField), 음성 타입을 변수 _voiceType(int)에 실시간으로 반영한다. 이때, 음성 타입은 정적 Button 컴포넌트에 연결된 `VoiceType.cs` 스크립트를 통해 할당받는다. 이 정보를 토대로 `ConnectProgress.cs`에서 음성 파일을 생성한다. VoiceCanvas에 있는 VoiceButton 객체는 `VoiceController.cs` 스크립트에서 동적으로 생성한다. 이 객체는 AudioSource 컴포넌트를 포함하고 있어 음성 파일을 저장할 수 있다. 자식 객체인 PlayButton을 클릭하면 저장하고 있던 음성 파일을 실행하여 대사를 출력한다. VoiceButton 객체를 클릭하면 Button 컴포넌트에 적용된 `VoiceButton.cs` 스크립트가 실행된다. 이때, Static.cs 스크립트에 음성 제목을 보내고 `VoiceMaking.cs` 스크립트에 음성 파일을 전달한다. 그러면 스케줄러에서 사용 가능한 데이터 형태가 된다.

### Build 기능 구현
> 상황 재현을 위해서 필요한 장소를 구성하는 Build 기능을 구현한다. 이 기능으로 벽, 바닥, 가구 등을 자유롭게 배치할 수 있다.

<div align="center">
  <img width="75%" height="75%" src="https://user-images.githubusercontent.com/60832219/210264212-873c23c6-da00-4928-98ed-d92e72fcbd1d.png"/>
</div>

+ 본 기능은 집 구조를 사용자의 편의에 따라 설계할 수 있도록 하는 기능이다. 기본 집 객체인 SimpleHouse에서만 이 기능을 사용할 수 있으므로, 타 집 객체에서는 Build Button이 비활성화된다. Build Button을 클릭할 때 설치할 수 있는 건물이 모두 나타난다.

<div align="center">
  <img width="45%" height="45%" src="https://user-images.githubusercontent.com/60832219/210264224-99c9f763-0f73-432e-a810-64bc31241555.png"/>
</div>

+ 건물 버튼을 클릭하면 `Slot.cs`에 있는 OnClickBuildButton 함수가 실행되어 설치할 건물 객체를 `PlaceController.cs` 스크립트의 LocatedBuilding 변수로 전달한다. LocatedBuilding 변수에 값이 할당되면 LocateBuilding 함수가 실행되어 건물의 위치를 마우스로 조절할 수 있게 된다. 마우스 왼쪽 버튼을 클릭하여 설치를 완료하면 LocatedBuilding 변수를 초기화하면서 `ItemListControl.cs` 스크립트의 DataBastWall(list)에 추가해준다.
+ 위 과정을 통해 설치된 건물 객체는 Scene 상의 Building Panel을 통해 관리된다. Building Panel을 작동시키는 `ClickedPlaceControl.cs`는 `PlaceController.cs` 스크립트의 ClickedPlace를 조작한다. ClickedPlace 변수는 마우스로 건물을 클릭하면 변경되며, `PlaceControl.cs` 스크립트의 ReplaceClickedItem 함수가 담당한다.
+ Building Panel에서는 클릭 된 객체의 이름을 갱신하고, 위치 조절, 크기 조절, 타일의 크기 조절을 담당한다. 도킹 기능은 건물을 설치할 때 모퉁이 근처에 다가가면 자동으로 부착되는 기능으로써 On/Off가 가능하다.

### VR 전용 Canvas 구현
> HMD를 착용하면 Screen Canvas를 볼 수 없으므로, World Canvas를 컨트롤러로 조작할 수 있도록 구현한다.

+ Unity는 오브젝트를 실제로 배치하는 World Space와 UI를 표현하는 Screen Space로 나누어져 있다. HMD를 연결하게 되면 World Space에 있는 Camera의 시점으로 변경되기 때문에 Screen Space의 UI를 조작할 수 없다. 따라서, 가상환경에서 UI를 조작하기 위한 연구를 진행했다.
+ 대부분의 HMD 기기를 인식하는 **SteamVR** 플러그인과 **Window Mixed Reality**를 이용해서 VR을 연동했다. 이렇게 연동함으로써 헤드 트래킹과 컨트롤러 트래킹은 CameraRig 오브젝트가 담당하게 된다. 다음은 Unity에서 Screen Space의 UI를 조작하는 원리에 대해 분석했다. 카메라상의 마우스의 좌표는 **PointerEventData**가 담고, 이 좌표로부터 **GrapicRaycaster**가 발생한다. GrapicRaycaster가 UI와 충돌하면, 충돌한 UI는 **BaseInputModule** 클래스에 있는 함수들로 이벤트를 전달받는다. 이러한 분석 내용을 토대로 Canvas에 이벤트를 전달하는 `VRJoystick.cs` 스크립트를 작성했다. **LineRenderer** 컴포넌트로 **VRPointer**를 제작하여 사용자가 UI를 쉽게 가리키도록 했다.
+ 마지막으로 **Steam VR Input** 플러그인을 통해 컨트롤러의 키를 맵핑하고, CameraRig 오브젝트가 이동할 수 있도록 하여 시나리오를 여러 방면에서 볼 수 있도록 했다. 아래의 그림은 VR Canvas의 구조도를 나타낸 것이다.

<div align="center">
  <img width="75%" height="75%" src="https://user-images.githubusercontent.com/60832219/210264490-72906481-0d8c-418b-94ce-d2924fba5cf6.png"/>
</div>

### Teleport 기능 구현
> 집 객체의 넓은 공간을 수정하는 데에 있어 좀 더 효율적으로 하고자 구현되었다.

<div align="center">
  <img width="30%" height="30%" src="https://user-images.githubusercontent.com/60832219/210264529-eb72549e-fe73-45a2-aa91-b38130fc8077.PNG"/>
</div>

+ 위와 같은 이미지를 에디터 상에서 클릭할 경우 이미지 객체에 내장된 `Teleport.cs` 스크립트의 OnMouseDown 함수가 실행된다. 이때, Camera 객체의 X, Z축 값이 이미지 오브젝트의 X, Z축 값과 같게 변경된다.
+ 이미지 오브젝트는 3D 객체가 아닌 2D 객체이므로, 다른 각도에서 바라보면 종잇장이 공중에 떠 있는 형태가 된다. 따라서, `Teleport.cs` 에서는 Camera 객체를 정면으로 바라볼 수 있도록 LookAt(Transform) 함수가 적용되어 있다. 또한, 스케줄러를 재생하거나 타 객체를 배치하는 경우에는 방해되지 않도록 비활성화하고, Camera 객체와 거리가 가까워지면 점차 이미지 객체가 투명해지도록 구현하였다.

<div align="center">
  <img width="75%" height="75%" src="https://user-images.githubusercontent.com/60832219/210264556-d652b850-0977-4914-bd74-9db6eb951337.png"/>
</div>

### Dimension UI 구현
> Build 기능을 사용할 때에 좌표축을 쉽게 파악할 수 있도록 구현되었다.

+ 좌표축 오브젝트인 DimensionObject를 정사영으로 포착하여 DimensionCanvas를 만들어낸다. 이때, Dimension Canvas는 **RenderTexture**로 구성되어 실시간으로 사용자 카메라의 회전을 반영할 수 있다. 만약, DimensionCanvas를 드래그할 경우 축을 기준으로 한 회전을 할 수 있다. 아래의 그림은 Dimension UI의 대략적인 구조를 나타낸다.

<div align="center">
  <img width="65%" height="65%" src="https://user-images.githubusercontent.com/60832219/210264703-720e6a6c-bb50-405c-a6f0-816a1d708558.png"/>
</div>

### 기타 구현 사항
+ 모든 UI(User Interface) 상호작용 구현(**Coroutine**)
+ **Samsung HMD Odyssey** 연동(**Steam VR**, **Window Mixed Reality**)
+ **Samsung Gear VR** 전용 Build
+ 전용 SDK를 이용해 **HI5 Data Glove** 움직임 구현
+ **3ds Max**로 건물 오브젝트 제작
+ 생성 오브젝트들을 **Database** 파일(SQLite)로 저장

## 🎖 연구 성과
+ VR 콘텐츠 저작도구 개발 완료
+ 전용 VR 콘텐츠 재생기 개발 완료(안드로이드 버전 포함)
+ 특허 등록 1건과 출원 1건 달성
+ 국내 저널 논문 2건 게재
+ 학술발표대회 논문 5건 발표
+ 프로그램 등록 7건
+ 연구학점제 이수 완료

## ✔ 연구 후기
+ 대학교 재학 시절에 연구실 소속 연구원으로써 지낼 수 있었던 것은 **행운이자 기회**였다고 생각한다. 어느 대학 수업보다도 **실무적**이었기에, 지금도 좋은 개발 습관들이 남아있다.
+ 연구실 프로젝트는 교수님들과 몇 년 단위로 수행하는 대규모 프로젝트였기에 여러 연구원이 개발하면서 고도화되었다. 이전 연도에 개발이 어느 정도 진행된 상태였는데, 남이 작성한 코드를 읽고 이해하는 것만 거의 몇 주가 걸렸다. 지금껏 본인이 작성했던 코드는 더욱 남들이 이해하기 어려울 것이란 생각에 부끄럽기도 했다. 이것이 내가 책임감을 가지고 개발에 임하게 된 계기였다. 이때부터 **프로그래밍 명명법**을 따르고, **주석**을 꼼꼼히 작성하며, 당일 개발 내용을 **기록**하는 습관을 들였다.
+ 많은 연구원이 함께하다 보니 소통이 매우 중요했는데, 회의를 매주 2회 진행하면서 **개발 내용을 공유**하고 **목표를 확인**했다. 동시에 많은 인원이 프로젝트를 변경했을 때 문제가 발생하지 않도록, **버전 관리 시스템**을 최초로 사용해보았다. 반면에, VR 기술을 다뤄본 적이 없어서 생긴 난관 때문에 팀원들과 밤도 많이 지새웠는데, 썩 나쁘지 않은 경험이었다.
+ 프로그램을 개발할 때는 **독립적인 모듈**로 구성되어야 한다는 것을 크게 깨달은 적이 있다. 다른 팀원들이 작성한 함수에 계속 추가하며 개발하다 보니, 이해하기 어려울뿐더러 간단한 수정 사항도 고쳐야 할 스크립트가 기하급수적으로 늘어나게 되었다.
+ 연구 내용에 대한 논문 작성 후, **학술 발표 대회**를 여럿 다니게 되었다. 처음에는 비교적 엄중하고 공적인 발표라고 생각해서, 청중이 어려운 내용보다는 이해하기 쉬운 발표를 하는 것에 초점을 두었다. 하지만, 배경지식이 있는 청중들이 오는 자리였기 때문에, 발표 후에 질문을 많이 받았던 기억이 난다.
+ 연구실에서는 필요한 자원을 구매할 수 있도록 해주었는데, 덕분에 비싼 장비들을 구매해서 새로운 개발을 해볼 수 있었다. 그 중 하나가 데이터 글러브인데, 손가락 마디를 모두 인식해서 어려움없이 오브젝트를 집는 행동을 할 수 있었다. 또한, Unite Seoul 같은 개발자 컨퍼런스를 참가할 수 있게 교통 비용을 지원해줘서 많이 배울 수 있었다.

<div align="center">
  <table border="0">
  <tr>
    <td align="center">
      <img width="1000em" height="250em" src="https://user-images.githubusercontent.com/60832219/210274772-b29871dc-67e8-4788-a476-db8dba42ad2f.png"/>
    </td>
    <td align="center">
      <img width="1000em" height="250em" src="https://user-images.githubusercontent.com/60832219/210274777-6b3ca679-e695-4fa8-b2e8-fdf29e843765.PNG"/>
    </td>
  </tr>
  <tr>
    <td align="center">
      <img width="1000em" height="250em" src="https://user-images.githubusercontent.com/60832219/210274784-5138a133-1b32-4d30-abe7-21feed06dba7.PNG"/>
    </td>
    <td align="center">
      <img width="1000em" height="250em" src="https://user-images.githubusercontent.com/60832219/210274806-a0cae6db-131f-4b27-a859-5af98305ee2c.png"/>
    </td>
  </tr>
  <tr>
    <td align="center">
      <img width="1000em" height="250em" src="https://user-images.githubusercontent.com/60832219/210274811-a6d36278-b495-4255-999d-8f9152b6fd8d.png"/>
    </td>
    <td align="center">
      <img width="1000em" height="250em" src="https://user-images.githubusercontent.com/60832219/210274814-1d990fda-7cda-461a-9975-31506529aa6f.png"/>
    </td>
  </tr>
  </table>
</div>
