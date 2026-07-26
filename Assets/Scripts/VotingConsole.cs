using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VotingConsole : MonoBehaviour
{
    [Header("References")]
    public NarrativeManager narrative;
    public GameObject choiceUI; // same GameObject you already assigned to NarrativeManager

    [Header("Voting Window")]
    public float votingWindowSeconds = 8f;

    [Header("Physical Buttons")]
    public Renderer[] reportButtonRenderers;
    public Renderer[] ignoreButtonRenderers;
    public Material offMaterial;
    public Material litReportMaterial;
    public Material litIgnoreMaterial;

    [Header("Player Key Bindings")]
    public KeyCode[] reportKeys = { KeyCode.Q, KeyCode.W, KeyCode.E, KeyCode.R };
    public KeyCode[] ignoreKeys = { KeyCode.A, KeyCode.S, KeyCode.D, KeyCode.F };

    public Material idleReportMaterial;
    public Material idleIgnoreMaterial;

    bool votingActive = false;
    bool[] hasVotedReport;
    bool[] hasVotedIgnore;
    int reportVotes = 0;
    int ignoreVotes = 0;
    float timer = 0f;

    void Start()
    {
        hasVotedReport = new bool[reportKeys.Length];
        hasVotedIgnore = new bool[ignoreKeys.Length];
    }

    void Update()
    {
        if (!votingActive && choiceUI.activeSelf)
        {
            StartVotingWindow();
        }

        if (votingActive)
        {
            HandleVotes();
            timer -= Time.deltaTime;
            if (timer <= 0f)
            {
                EndVotingWindow();
            }
        }
    }

    void StartVotingWindow()
    {
        votingActive = true;
        timer = votingWindowSeconds;
        reportVotes = 0;
        ignoreVotes = 0;
        for (int i = 0; i < hasVotedReport.Length; i++) hasVotedReport[i] = false;
        for (int i = 0; i < hasVotedIgnore.Length; i++) hasVotedIgnore[i] = false;

        foreach (var r in reportButtonRenderers) r.material = idleReportMaterial;
        foreach (var r in ignoreButtonRenderers) r.material = idleIgnoreMaterial;
    }

    void HandleVotes()
    {
        Debug.Log("HandleVotes running, votingActive=" + votingActive);

        for (int i = 0; i < reportKeys.Length; i++)
        {
            if (!hasVotedReport[i] && Input.GetKeyDown(reportKeys[i]))
            {
                hasVotedReport[i] = true;
                reportVotes++;
                if (reportButtonRenderers.Length > i)
                    reportButtonRenderers[i].material = litReportMaterial;
            }
        }
        for (int i = 0; i < ignoreKeys.Length; i++)
        {
            if (!hasVotedIgnore[i] && Input.GetKeyDown(ignoreKeys[i]))
            {
                hasVotedIgnore[i] = true;
                ignoreVotes++;
                if (ignoreButtonRenderers.Length > i)
                    ignoreButtonRenderers[i].material = litIgnoreMaterial;
            }
        }
    }

    void EndVotingWindow()
    {
        votingActive = false;
        foreach (var r in reportButtonRenderers) r.material = offMaterial;
        foreach (var r in ignoreButtonRenderers) r.material = offMaterial;

        if (reportVotes == 0 && ignoreVotes == 0)
        {
            StartVotingWindow();
            return;
        }
        if (reportVotes >= ignoreVotes)
            narrative.ChooseCorrect();
        else
            narrative.ChooseWrong();
    }
}