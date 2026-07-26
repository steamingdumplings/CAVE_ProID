# CyberSafe JARSS — CAVE Prototype

An immersive, walk-in room experience prototype built for youth online-safety education. Instead of watching a video about identity theft, you stand inside the scene — surrounded on all four walls — and make the choice yourself.

This repository contains the Unity project for a CAVE-style installation: a room where four walls each act as a screen, playing synchronized video content so the player feels physically inside the story rather than watching it from outside.

## What this project shows
A short interactive narrative about social media identity theft:
* Welcome — an intro sequence plays across all four walls.
* Scenario — you're placed inside Alex's bedroom as a hacker clones their profile and starts messaging their friends for money, right in front of you.
* Choice — you decide: Report the content, or Ignore it.
* Consequence — the story branches based on your choice:
                Report → the fake account gets taken down, friends are safe.
                Ignore → the scam spreads, friends lose real money, and the damage escalates.
* Outro — the room fades to black, closing on the "stay alert, stay safe" message.

The goal is to make the consequences of reporting vs. ignoring suspicious activity feel immediate and personal, rather than abstract advice.

## What's in this repo
- Assets/_Scenes — the main CAVE scene (CAVE_Main)
- Assets/_Scripts — core logic:
- NarrativeManager.cs — drives the story sequence and branching choice logic
- CameraLook.cs / PlayerMove.cs — first-person look and movement
- OrbitCamera.cs / CameraSwitcher.cs — exterior orbit view before entering the room
- VotingConsole.cs — group voting logic for the choice moment (supports multiple simultaneous "votes," majority decides)
- QuitHandler.cs — lets the build be closed with Escape
- Assets/_Videos — the video clips played across the 4 walls
- Assets/_Materials, Assets/_Prefabs — supporting assets for the room, walls, and voting console

## How to try it (no Unity required)
You don't need Unity installed to run the demo — just the built executable.

1. Go to the **Build_done** page of this repo.
2. Download the full Build folder — ALL OF IT. The files must stay together in the same location, or it won't launch.
3. Double-click the .exe to run it.
4. If Windows shows a "Windows protected your PC" SmartScreen warning, click More info → Run anyway — this appears because the build isn't from a registered publisher, not because anything is wrong.

## Controls
| Action | Input |
|---|---|
| Look around | Mouse (hold left click + drag) |
| Move | Arrow keys (↑ ↓ ← →) |
| Enter the room (from the exterior view)	| Enter |
| Vote: Report the content | Q / W / E / R (one key per participant) |
| Vote: Ignore it |	A / S / D / F (one key per participant) |
| Quit the application | Esc |

The voting console supports up to 4 simultaneous participants — everyone in the room can cast their own vote, and the majority decides which path the story takes.

#### Notes for anyone building on this
* _This is a prototype/pitch demo, not the final physical installation — it runs as a single first-person view on one screen, representing what would eventually be projected across 4 real walls in a physical CAVE room._
* _The voting console keys are intentionally not WASD, since WASD is reserved for movement — using them together would cause movement and voting to trigger simultaneously._
* _Application.Quit() only works in the exported build — pressing Esc inside the Unity Editor's Play mode does nothing, which is expected Unity behavior, not a bug._

Built by Team Ghost JARSS member, Sing Jia.
