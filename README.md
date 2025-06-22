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
1. Unity Version:Unity6000+
2. Platforms: Windows, Android
3. Dependencies:
  DOTween (add via Package Manager)
  Sprites for Circle, Square, Squircle, Rhombus (user-provided)
  Shape prefabs and UI elements (user-provided)

#How to Play
Home Screen: Click "Play" to load the gameplay screen with a progress bar.
Controls:
Desktop/Mobile: Click and drag the mouse to move the paddle horizontally.
Gameplay:
Move the paddle to catch falling shapes that match the target shape shown in the UI.
Correct shapes increase your score and attach to the paddle.
Incorrect shapes trigger a game-over state.
Game Over: View your score in the popup and click "Home" to return to the home screen.
