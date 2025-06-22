# ShapeCatcher
A 2D Unity game where players control a paddle to catch falling shapes (Circle, Square, Squircle, Rhombus) matching a target shape. Features smooth scene transitions, object pooling, and a clean UI for an engaging experience.

#Features
1. Shape-catching gameplay with four distinct shapes: Circle, Square, Squircle, Rhombus
2. Responsive paddle movement via mouse input
3. Object pooling for efficient shape spawning
4. Dynamic UI displaying score and target shape
5. Game-over popup with score display and home button
6. Asynchronous scene loading with progress bar
7. Button animations using DOTween for visual feedback
8. Persistent UI management across scenes
9. Lightweight design optimized for performance


#Requirements
1. Unity Version: Unity 6000.0+
2. Platforms: Windows, Android
- Dependencies:
3. Add DOTween Package
4. prites for Circle, Square, Squircle, Rhombus (user-provided)
5. Shape prefabs and UI elements (user-provided)

#How to Play
1. Home Screen: Click "Play" to load the gameplay screen with a progress bar.
- Controls:
2. Desktop: Click and drag the mouse to move the paddle horizontally.
- Gameplay:
3. Move the paddle to catch falling shapes that match the target shape shown in the UI.
4. Correct shapes increase your score and attach to the paddle.
5. Incorrect shapes trigger a game-over state.
6. Game Over: View your score in the popup and click "Home" to return to the home screen.


#Project Structure
- Scripts:
1. Player.cs: Paddle movement, collision detection, score updates, game-over logic
2. SceneTransitionManager.cs: Async scene loading with progress bar
3. ShapeGenrator.cs: (Assumed) Spawns shapes and sets target shape
4. Shape.cs: Abstract base class for shapes
5. Circle.cs, Square.cs, Squircle.cs, Rhombus.cs: Shape implementations
6. ShapePoolSO.cs: ScriptableObject for shape pool configuration
7. ShapePooler.cs: Manages object pooling for shapes
8. UIManager.cs: UI management for score, target shape, and game-over popup
9. GameOverUIPopUp.cs: Game-over popup with animations


#Scenes:
1. ShapeCatchHomeScreen: Home screen with play button
2. ShapeCatchGamePlayScreen: Gameplay scene

#Assets:
1. Shape prefabs (Circle, Square, Squircle, Rhombus)
2. ScriptableObjects for shape pools

#Short Note on Game Feel and Optimization
-Game Feel
1. Controls: Player.cs ensures smooth paddle movement with Mathf.Clamp for screen bounds and Vector3.Lerp for responsive mouse tracking (_speed). A distance check (0.5f) balances precision and fluidity.
2. Feedback: Visual feedback via DOTween animations in GameOverUIPopUp.cs (button scaling with DOScale) enhances interactivity. Score updates in UIManager.cs (UpdateScoreText) provide immediate reward.
3. Visuals: Clean UI (_scoreText, _targetImage in UIManager.cs) and distinct shape sprites (Circle, Square, Squircle, Rhombus) create a cohesive aesthetic.

-Optimization
1. Async Loading: SceneTransitionManager.cs and GameOverUIPopUp.cs use SceneManager.LoadSceneAsync with a progress bar (_progressBar, _progressText) for seamless transitions, critical for mobile.
2. Object Pooling: ShapePooler.cs and ShapePoolSO.cs implement object pooling for shapes, reducing instantiation costs. Pools are pre-warmed (InitialSize) and capped (maxSize) for efficiency.
3. Efficiency: Singletons (UIManager.cs, SceneTransitionManager.cs) with DontDestroyOnLoad (UI only) reduce reinstantiation. ShapePoolSO ScriptableObjects decouple configuration for scalability.
