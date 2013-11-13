using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NuclearWinter.UI;
using NuclearWinter;
using NUI = NuclearWinter.UI;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework;


namespace BaconGameJam6.GameState
{
    public class GameStateMainMenu : NuclearWinter.GameFlow.GameStateFadeTransition<PlatformerGame>
    {
        Screen mScreen;

        public GameStateMainMenu(PlatformerGame game)
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
