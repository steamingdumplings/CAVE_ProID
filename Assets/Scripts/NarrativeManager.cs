using UnityEngine;
using UnityEngine.Video;
using System.Collections;

public class NarrativeManager : MonoBehaviour
{
    [Header("Wall Video Players (one per wall)")]
    public VideoPlayer wallFront;
    public VideoPlayer wallBack;
    public VideoPlayer wallLeft;
    public VideoPlayer wallRight;

    [Header("Welcome Clips (one per wall)")]
    public VideoClip welcome_Front;
    public VideoClip welcome_Back;
    public VideoClip welcome_Left;
    public VideoClip welcome_Right;

    [Header("Scenario Intro Clips")]
    public VideoClip scenario_Front;
    public VideoClip scenario_Back;
    public VideoClip scenario_Left;
    public VideoClip scenario_Right;

    [Header("Correct Choice Clips")]
    public VideoClip correct_Front;
    public VideoClip correct_Back;
    public VideoClip correct_Left;
    public VideoClip correct_Right;

    [Header("Wrong Choice Clips")]
    public VideoClip wrong_Front;
    public VideoClip wrong_Back;
    public VideoClip wrong_Left;
    public VideoClip wrong_Right;

    [Header("App CTA Clips")]
    public VideoClip appCTA_Front;
    public VideoClip appCTA_Back;
    public VideoClip appCTA_Left;
    public VideoClip appCTA_Right;

    [Header("Outro Clips")]
    public VideoClip outro_Front;
    public VideoClip outro_Back;
    public VideoClip outro_Left;
    public VideoClip outro_Right;

    [Header("UI")]
    public GameObject choiceUI;
    public GameObject replayUI;
    public UnityEngine.UI.Image fadeImage;

    [Header("Wall Renderers (for physical fade-to-black)")]
    public Renderer wallFrontRenderer;
    public Renderer wallBackRenderer;
    public Renderer wallLeftRenderer;
    public Renderer wallRightRenderer;

    enum State { Welcome, Scenario, AwaitChoice, Correct, Wrong, AppCTA, Outro }
    State state;

    void Start()
    {
    }

    public void StartNarrative()
    {
        StartCoroutine(RunNarrative());
    }

    // -- Play a different clip on each wall simultaneously
    void PlayAll(VideoClip front, VideoClip back, VideoClip left, VideoClip right)
    {
        Play(wallFront, front);
        Play(wallBack, back);
        Play(wallLeft, left);
        Play(wallRight, right);
    }

    void Play(VideoPlayer vp, VideoClip clip)
    {
        vp.Stop();
        vp.clip = clip;
        vp.Play();
    }

    // -- Wait until the front wall clip finishes (used as master timer)
    IEnumerator WaitForFront()
    {
        yield return new WaitUntil(() =>
            !wallFront.isPlaying && wallFront.frame > 0);
    }

    // -- Main narrative sequence
    IEnumerator RunNarrative()
    {
        // 1. Welcome
        state = State.Welcome;
        PlayAll(welcome_Front, welcome_Back, welcome_Left, welcome_Right);
        yield return WaitForFront();

        // 2. Scenario Intro
        state = State.Scenario;
        PlayAll(scenario_Front, scenario_Back, scenario_Left, scenario_Right);
        yield return WaitForFront();

        // 3. Show choice buttons
        state = State.AwaitChoice;
        choiceUI.SetActive(true);
    }

    // -- Called by Button_Correct
    public void ChooseCorrect()
    {
        if (state != State.AwaitChoice) return;
        choiceUI.SetActive(false);
        StartCoroutine(CorrectPath());
    }

    // -- Called by Button_Wrong
    public void ChooseWrong()
    {
        if (state != State.AwaitChoice) return;
        choiceUI.SetActive(false);
        StartCoroutine(WrongPath());
    }

    IEnumerator CorrectPath()
    {
        state = State.Correct;
        PlayAll(correct_Front, correct_Back, correct_Left, correct_Right);
        yield return WaitForFront();
        replayUI.SetActive(true);
    }

    IEnumerator WrongPath()
    {
        state = State.Wrong;
        PlayAll(wrong_Front, wrong_Back, wrong_Left, wrong_Right);
        yield return WaitForFront();
        yield return StartCoroutine(AppAndOutro());
    }

    // -- Called by Button_Replay
    public void Replay()
    {
        replayUI.SetActive(false);
        choiceUI.SetActive(true);
        state = State.AwaitChoice;
    }

    IEnumerator AppAndOutro()
    {
        state = State.AppCTA;
        PlayAll(appCTA_Front, appCTA_Back, appCTA_Left, appCTA_Right);
        yield return WaitForFront();

        state = State.Outro;
        PlayAll(outro_Front, outro_Back, outro_Left, outro_Right);
        yield return WaitForFront();

        yield return StartCoroutine(FadeToBlack());
    }

    IEnumerator FadeToBlack()
    {
        float t = 0f;
        while (t < 1f)
        {
            t += Time.deltaTime * 0.4f;
            Color c = Color.Lerp(Color.white, Color.black, t);

            wallFrontRenderer.material.color = c;
            wallBackRenderer.material.color = c;
            wallLeftRenderer.material.color = c;
            wallRightRenderer.material.color = c;

            if (fadeImage != null) fadeImage.color = new Color(0, 0, 0, t);
            yield return null;
        }
    }
}
