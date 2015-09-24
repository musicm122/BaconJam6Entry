using Microsoft.Xna.Framework;
using NuclearWinter.UI;

using NUI = NuclearWinter.UI;

namespace BaconGameJam6.GameState
{
    public class GameStatePause : NuclearWinter.GameFlow.GameStateFadeTransition<PlatformerGame>
    {
        private Screen mScreen;

        public bool IsPaused { get; set; }

        public GameStatePause(PlatformerGame game)
            : base(game)
        {
        }

        public override void Start()
        {
            Game.IsMouseVisible = false;

            mScreen = new Screen(Game, Game.UIStyle, Game.GraphicsDevice.Viewport.Width, Game.GraphicsDevice.Viewport.Height);

            Label titleLabel = new Label(mScreen, "Pause");
            titleLabel.Font = mScreen.Style.LargeFont;
            titleLabel.AnchoredRect = NUI.AnchoredRect.CreateCentered(500, 100);

            mScreen.Root.AddChild(titleLabel);

            base.Start();
        }

        public override void Stop()
        {
            base.Stop();
        }

        public override void Draw()
        {
            Game.GraphicsDevice.Clear(Color.CornflowerBlue);
            mScreen.Draw();
        }

        public override void Update(float _fElapsedTime)
        {
            mScreen.IsActive = Game.IsActive;
            mScreen.HandleInput();

            mScreen.Update(_fElapsedTime);
            Game.PauseState.IsPaused = !Game.PauseState.IsPaused;
            if (!IsPaused && (!Game.GameStateMgr.IsSwitching))
            {
                Game.GameStateMgr.SwitchState(Game.PlayState);
            }
        }
    }
}