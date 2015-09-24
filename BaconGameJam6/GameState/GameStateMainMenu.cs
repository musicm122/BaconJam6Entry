using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using NuclearWinter.UI;

using NUI = NuclearWinter.UI;

namespace BaconGameJam6.GameState
{
    public class GameStateTitleScreen : NuclearWinter.GameFlow.GameStateFadeTransition<PlatformerGame>
    {
        private Screen mScreen;

        public GameStateTitleScreen(PlatformerGame game)
            : base(game)
        {
        }

        public override void Start()
        {
            Game.IsMouseVisible = false;

            mScreen = new Screen(Game, Game.UIStyle, Game.GraphicsDevice.Viewport.Width, Game.GraphicsDevice.Viewport.Height);

            Label titleLabel = new Label(mScreen, "Slay The Rainbow");
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
            if ((!Game.GameStateMgr.IsSwitching) && Game.InputMgr.KeyboardState.IsKeyDown(Keys.Enter))
            {
                Game.GameStateMgr.SwitchState(Game.PlayState);
            }
        }
    }
}