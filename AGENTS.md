## Repo overview

Unity horror game (URP pipeline, `com.unity.inputsystem`, `com.unity.ai.navigation`). Single build scene: `Assets/Scenes/SampleScene.unity`.

## Custom scripts — what they do

All live in `Assets/Scripts/` (no namespace):

- **SC_FPSController.cs** — first-person movement + aim (WASD, shift-run, space-jump, mouse-look). Uses `UnityEngine.InputSystem` directly (`Keyboard.current`, `Mouse.current`), **not** the StarterAssets InputSystem wrapper.
- **pickupLetter.cs** — 8 collectible letter pages. Tracks global state via `public static int pagesCollected`. On pickup: increments counter, updates UI text to `"N/8 pages"`, plays pickup sound + corresponding ambiance layer 1-8, hides interact prompt, deactivates the letter.
- **LetterCollector.cs** — raycast (3 unit range, forward from player transform). Highlights nearby letter objects (`interactable` bool).
- **MonsterAI.cs** — `NavMeshAgent` chases player. Speed and anim.speed scale with `pickupLetter.pagesCollected` (8 tiers from 1.5→4.0 speed, 0.2→1.6 anim).
- **flashlightMovement.cs** — toggles flashlight animator triggers (`walk`/`sprint`) based on player movement + shift.
- **footstepsSounds.cs** — plays/stops `walkSound` / `sprintSound` AudioSource based on movement + shift state.
- **DisableObject.cs** — coroutine that deactivates a GameObject after `activeTime` seconds.
- **`flashlightWalk.anim`** — Animator animation file for flashlight.

## Architecture notes

- Game state is **fully global/static**: `pickupLetter.pagesCollected` is the single source of truth. No event system, no scriptable objects, no SOAs.
- `MonsterAI` reads `pickupLetter.pagesCollected` every frame in `Update()`. No observers, no decoupling.
- Input is hardcoded to `UnityEngine.InputSystem` API (`Keyboard.current`, `Mouse.current`) — not the StarterAssets `PlayerInput`/`StarterAssetsInputs` wrapper (which lives in `Assets/StarterAssets/InputSystem/` but is unused by the custom scripts).
- The scene uses **terrain** with 4 layers: ground, grass, path, rock (`_TerrainAutoUpgrade` folder + `.terrainlayer` files). Two `New Terrain` assets.

## Package boundaries

- `Assets/Scripts/` — custom game logic (edit these)
- `Assets/StarterAssets/` — Unity's third-person controller kit (do not edit; override by adjusting fields on the prefab)
- `Assets/AssetsStore/` — imported store packages: NatureStarterKit2, Rocks and Boulders 2, Abandoned buildings, MarpaStudio, Tom's Terrain Tools
- `Assets/Settings/` — URP assets
- `Assets/Scenes/` — only `SampleScene.unity`

## Unity-specific gotchas

- `pickupLetter.pagesCollected` is `static` — changes survive scene reloads unless explicitly reset.
- Monster `ai.speed` is overwritten every frame by `MonsterAI.Update()` — any external `NavMeshAgent.speed` changes will be immediately overridden.
- `SC_FPSController` guards `Keyboard.current == null || Mouse.current == null` at the top of `Update()` — disables all input if the InputSystem isn't set up.
- The project's `.gitignore` already excludes `Library/`, `Temp/`, `Logs/`, `UserSettings/`, `Build/`, `Builds/`, `*.csproj`, `*.sln`, `.vs/`.

## Agent behavior — visual inspection

- When the task involves inspecting Unity screenshots, scene visuals, or any image output from the editor, **always launch the `visual-reviewer` subagent** (`subagent_type: visual-reviewer`) instead of trying to analyze the image yourself. Do not attempt visual analysis in the main agent.

- Pass the full file path of the image in the task prompt and let the visual-reviewer handle the inspection.

- When comparing images (e.g., before/after lighting changes), include full file paths to each image:
  - `C:\Users\zim\Game making stuff\goofyhorrorgame-ZimTest\Assets\Screenshots\screenshot-lighting-v1.png`
  - `C:\Users\zim\Game making stuff\goofyhorrorgame-ZimTest\Assets\Screenshots\screenshot-lighting-v2.png`
  - Let the visual-reviewer load and compare both files

- The `visual-reviewer` subagent is restricted to **visual inspection only**. It cannot read source files, modify code, or perform editor operations. Use it solely for reviewing screenshots and image output.

- **For horror scenes, the visual-reviewer should prefer darker aesthetics** — darker fog, reduced ambient light, stronger shadows, and minimal background illumination. The reviewer should recommend darker setups that enhance tension, isolation, and mystery over brighter, more visible lighting.

## Editor tooling

- `com.coplaydev.unity-mcp` is installed — use Unity MCP tools for scene/component/prefab operations from the editor.
- VS Code `.vscode/launch.json` includes `vstuc` debugger for Unity attach.
