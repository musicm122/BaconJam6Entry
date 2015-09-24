using System;
using System.IO;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using NuclearWinter.UI;

namespace BaconGameJam6.GameState
{
    public class GameStateActivePlay : NuclearWinter.GameFlow.GameStateFadeTransition<PlatformerGame>
    {
        private Screen mScreen;

        // Global content.
        private SpriteFont hudFont;

        private Texture2D winOverlay;
        private Texture2D loseOverlay;
        private Texture2D diedOverlay;

        // Meta-level game state.
        private int levelIndex = -1;

        private Level level;
        private bool wasContinuePressed;

        private GamePadState gamePadState;
        private KeyboardState keyboardState;

        private const int numberOfLevels = 3;

        // When the time remaining is less than the warning time, it blinks on the hud
        private static readonly TimeSpan WarningTime = TimeSpan.FromSeconds(30);

        public GameStateActivePlay(PlatformerGame game)
            : base(game)
        {
        }

        public override void Start()
        {
            // Load fonts
            hudFont = Content.Load<SpriteFont>("Fonts/Hud");

            // Load overlay textures
            winOverlay = Content.Load<Texture2D>("Overlays/you_win");
            loseOverlay = Content.Load<Texture2D>("Overlays/you_lose");
            diedOverlay = Content.Load<Texture2D>("Overlays/you_died");

            //Known issue that you get exceptions if you use Media PLayer while connected to your PC
            //See http://social.msdn.microsoft.com/Forums/en/windowsphone7series/thread/c8a243d2-d360-46b1-96bd-62b1ef268c66
            //Which means its impossible to test this from VS.
            //So we have to catch the exception and throw it away
            //try
            //{
            //    MediaPlayer.IsRepeating = true;
            //    MediaPlayer.Play(Content.Load<Song>("Sounds/Music"));
            //}
            //catch { }

            LoadNextLevel();

            Game.IsMouseVisible = false;

            mScreen = new Screen(Game, Game.UIStyle, Game.GraphicsDevice.Viewport.Width, Game.GraphicsDevice.Viewport.Height);

            base.Start();
        }

        private void DrawHud()
        {
            Rectangle titleSafeArea = Game.GraphicsDevice.Viewport.TitleSafeArea;
            Vector2 hudLocation = new Vector2(titleSafeArea.X, titleSafeArea.Y);
            Vector2 center = new Vector2(titleSafeArea.X + titleSafeArea.Width / 2.0f,
                                         titleSafeArea.Y + titleSafeArea.Height / 2.0f);

            // Draw time remaining. Uses modulo division to cause blinking when the
            // player is running out of time.
            string timeString = "TIME: " + level.TimeRemaining.Minutes.ToString("00") + ":" + level.TimeRemaining.Seconds.ToString("00");
            Color timeColor;
            if (level.TimeRemaining > WarningTime ||
                level.ReachedExit ||
                (int)level.TimeRemaining.TotalSeconds % 2 == 0)
            {
                timeColor = Color.Yellow;
            }
            else
            {
                timeColor = Color.Red;
            }
            DrawShadowedString(hudFont, timeString, hudLocation, timeColor);

            // Draw score
            float timeHeight = hudFont.MeasureString(timeString).Y;
            DrawShadowedString(hudFont, "SCORE: " + level.Score.ToString(), hudLocation + new Vector2(0.0f, timeHeight * 1.2f), Color.Yellow);

            // Determine the status overlay message to show.
            Texture2D status = null;
            if (level.TimeRemaining == TimeSpan.Zero)
            {
                if (level.ReachedExit)
                {
                    status = winOverlay;
                    Game.Exit();
                }
                else
                {
                    status = loseOverlay;
                }
            }
            else if (!level.Player.IsAlive)
            {
                status = diedOverlay;
            }

            if (status != null)
            {
                // Draw status message.
                Vector2 statusSize = new Vector2(status.Width, status.Height);
                Game.SpriteBatch.Draw(status, center - statusSize / 2, Color.White);
            }
        }

        private void DrawShadowedString(SpriteFont font, string value, Vector2 position, Color color)
        {
            Game.SpriteBatch.DrawString(font, value, position + new Vector2(1.0f, 1.0f), Color.Black);
            Game.SpriteBatch.DrawString(font, value, position, color);
        }

        private void HandleInput()
        {
            // get all of our input states
            keyboardState = Keyboard.GetState();
            gamePadState = GamePad.GetState(PlayerIndex.One);

            // Exit the game when back is pressed.
            if (gamePadState.Buttons.Back == ButtonState.Pressed)
                Game.Exit();

            bool continuePressed =
                keyboardState.IsKeyDown(Keys.Enter) ||
                gamePadState.IsButtonDown(Buttons.Start);

            // Perform the appropriate action to advance the game and
            // to get the player back to playing.
            if (!wasContinuePressed && continuePressed)
            {
                if (!level.Player.IsAlive)
                {
                    level.StartNewLife();
                }
                else if (level.TimeRemaining == TimeSpan.Zero)
                {
                    if (level.ReachedExit)
                        LoadNextLevel();
                    else
                        ReloadCurrentLevel();
                }
            }

            wasContinuePressed = continuePressed;
        }

        private void LoadNextLevel()
        {
            // move to the next level
            levelIndex = (levelIndex + 1) % numberOfLevels;

            // Unloads the content for the current level before loading the next one.
            if (level != null)
                level.Dispose();

            // Load the level.
            string levelPath = string.Format("Content/Levels/{0}.txt", levelIndex);
            using (Stream fileStream = TitleContainer.OpenStream(levelPath))
                level = new Level(Game.Services, fileStream, levelIndex);
        }

        private void ReloadCurrentLevel()
        {
            --levelIndex;
            LoadNextLevel();
        }

        public override void Draw()
        {
            Game.SpriteBatch.Begin();
            level.Draw(mScreen.Game.TargetElapsedTime.Seconds, mScreen.Game.SpriteBatch);
            DrawHud();

            Game.SpriteBatch.End();
            mScreen.Draw();
        }

        public override void Update(float _fElapsedTime)
        {
            // Handle polling for our input and handling high-level input
            HandleInput();

            // update our level, passing down the GameTime along with all of our input states

            level.Update(_fElapsedTime, keyboardState, gamePadState, Game.Window.CurrentOrientation);

            mScreen.IsActive = Game.IsActive;
            mScreen.HandleInput();
            mScreen.Update(_fElapsedTime);

            if ((!Game.GameStateMgr.IsSwitching) && Game.InputMgr.KeyboardState.IsKeyDown(Keys.P))
            {
                Game.PauseState.IsPaused = !Game.PauseState.IsPaused;
                if (Game.PauseState.IsPaused)
                {
                    Game.GameStateMgr.SwitchState(Game.PauseState);
                }
            }
        }
    }
}