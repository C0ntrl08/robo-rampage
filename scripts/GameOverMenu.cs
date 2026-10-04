using Godot;
using System;

namespace RoboRampage.scripts
{
    public partial class GameOverMenu : Control
    {
        public void GameOver()
        {
            GetTree().Paused = true;
            Visible = true;
            Input.MouseMode = Input.MouseModeEnum.Visible;
        }

        public void OnRestartButtonPressed()
        {
            GetTree().Paused = false;
            GetTree().ReloadCurrentScene();
        }

        public void OnQuitButtonPressed()
        {
            GetTree().Quit();
        }
    }
}
