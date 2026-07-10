using UnityEngine;
using UnityEngine.Video;
using System.Collections;

public class NarrativeManager : MonoBehaviour
{
    public VideoPlayer[] walls;
    public VideoClip welcome, scenarioIntro,
      choiceCorrect, choiceWrong,
      appCTA, outro;
    public GameObject choiceUI;
    public GameObject replayUI;

    enum State
    {
        Welcome, Scenario, AwaitChoice,
        Correct, Wrong, AppCTA, Outro
    }
    State state;

    void Start() => StartCoroutine(RunNarrative());

    IEnumerator RunNarrative()
    {
        // 1. Welcome
        PlayAll(welcome);
        yield return WaitFor(walls[0]);
        // 2. Scenario
        PlayAll(scenarioIntro);
        yield return WaitFor(walls[0]);
        // 3. Show choice UI
        choiceUI.SetActive(true);
        state = State.AwaitChoice;
    }

    public void ChooseCorrect()
    {
        if (state != State.AwaitChoice) return;
        choiceUI.SetActive(false);
        StartCoroutine(CorrectPath());
    }

    public void ChooseWrong()
    {
        if (state != State.AwaitChoice) return;
        choiceUI.SetActive(false);
        StartCoroutine(WrongPath());
    }

    IEnumerator CorrectPath()
    {
        state = State.Correct;
        PlayAll(choiceCorrect);
        yield return WaitFor(walls[0]);
        replayUI.SetActive(true);
    }

    IEnumerator WrongPath()
    {
        state = State.Wrong;
        PlayAll(choiceWrong);
        yield return WaitFor(walls[0]);
        StartCoroutine(AppAndOutro());
    }

    public void Replay()
    {
        replayUI.SetActive(false);
        choiceUI.SetActive(true);
        state = State.AwaitChoice;
    }

    IEnumerator AppAndOutro()
    {
        PlayAll(appCTA);
        yield return WaitFor(walls[0]);
        PlayAll(outro);
        yield return WaitFor(walls[0]);
        // Fade to black — room ends
        StartCoroutine(FadeOut());
    }

    void PlayAll(VideoClip clip)
    {
        foreach (var w in walls)
        {
            w.clip = clip; w.Play();
        }
    }

    IEnumerator WaitFor(VideoPlayer vp)
    {
        yield return new WaitUntil(() =>
          !vp.isPlaying && vp.frame > 0);
    }

    IEnumerator FadeOut()
    {
        float t = 0;
        while (t < 2f)
        {
            t += Time.deltaTime;
            RenderSettings.ambientLight =
              Color.Lerp(Color.black, Color.black, t / 2f);
            yield return null;
        }
    }
}
