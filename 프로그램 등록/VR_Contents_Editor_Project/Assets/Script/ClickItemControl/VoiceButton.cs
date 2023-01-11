using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class VoiceButton : MonoBehaviour
{
    [Header("UI")]
    public GameObject _playButton;

    [Header("Image")]
    public Sprite _playImage;
    public Sprite _stopImage;

    [Header("Script")]
    public VoiceController _voiceController;

    public int _voiceType;

    private void Update()
    {
        PlayButtonImageManagement();
    }
    /* PlayButton을 클릭했을 때 */
    public void OnClickPlayButton()
    {
        /* Audio Clip이 실행 중이면 */
        if (this.GetComponent<AudioSource>().isPlaying)
        {
            /* Audio Clip 종료 */
            this.GetComponent<AudioSource>().Stop();
        }
        /* Audio Clip이 실행 중이지 않으면 */
        else
        {
            /* Audio Clip 실행 */
            this.GetComponent<AudioSource>().Play();
        }
    }

    /* PlayButton 이미지 관리자 */
    public void PlayButtonImageManagement()
    {
        /* Audio Clip이 실행 중이면 */
        if (this.GetComponent<AudioSource>().isPlaying)
        {
            /* Play Image로 교체 */
            _playButton.GetComponent<Image>().sprite = _stopImage;
        }
        /* Audio Clip이 실행 중이지 않으면 */
        else
        {
            /* Stop Image로 교체 */
            _playButton.GetComponent<Image>().sprite = _playImage;
        }
    }

    /* VoiceButton을 클릭했을 때 실행되는 함수 */
    public void OnClickVoiceButton()
    {
        /* VoiceCanvas의 VoiceFileName의 Text를 변경 */
        _voiceController._voiceFileName.GetComponent<Text>().text = this.gameObject.name;
        _voiceController._voiceType = _voiceType;
        Static.STATIC._voicename = this.gameObject.name;
        Debug.Log("현재 음성 길이 : " + this.GetComponent<AudioSource>().clip.length + "\n현재 음성 파일 이름 : " + this.gameObject.name);
    }
}