using BaconGameJam6.GameState;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using NuclearWinter;
using NuclearWinter.UI;

namespace BaconGameJam6
{
    public class PlatformerGame : NuclearGame
    {
        //internal GameStates.GameStateIntro Intro { get; private set; }
        //GameStateMgr<PlatformerGame> GameStateManager;
        internal GameStateTitleScreen MainMenu { get; private set; }

        internal GameStateActivePlay PlayState { get; private set; }

        internal GameStatePause PauseState { get; private set; }

        internal Style UIStyle { get; private set; }

        private GraphicsDeviceManager graphics;
        private SpriteBatch spriteBatch;

        private GamePadState gamePadState;
        private KeyboardState keyboardState;

        public PlatformerGame()
        {
            //graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
        }

        protected override void Initialize()
        {
            base.Initialize();

            //GameStateManager = new NGF.GameStateMgr<PlatformerGame>(this);
            //Components.Add(GameStateManager);
            LoadUIStyle();
            MainMenu = new GameStateTitleScreen(this);
            PlayState = new GameStateActivePlay(this);
            PauseState = new GameStatePause(this);

            GameStateMgr.SwitchState(MainMenu);
        }

        protected override void LoadContent()
        {
            base.LoadContent();
            // Create a new SpriteBatch, which can be used to draw textures.
            spriteBatch = new SpriteBatch(GraphicsDevice);
        }

        public void LoadUIStyle()
        {
            // UI Style
            UIStyle = new Style();

            UIStyle.SmallFont = new UIFont(Content.Load<SpriteFont>("Fonts/SmallFont"), 14, 0);
            UIStyle.MediumFont = new UIFont(Content.Load<SpriteFont>("Fonts/MediumFont"), 18, -2);
            UIStyle.LargeFont = new UIFont(Content.Load<SpriteFont>("Fonts/LargeFont"), 24, 0);
            UIStyle.ExtraLargeFont = new UIFont(Content.Load<SpriteFont>("Fonts/LargeFont"), 24, 0);

            UIStyle.SpinningWheel = Content.Load<Texture2D>("Sprites/UI/SpinningWheel");

            UIStyle.DefaultTextColor = new Color(224, 224, 224);
            UIStyle.DefaultButtonHeight = 60;

            UIStyle.ButtonFrame = Content.Load<Texture2D>("Sprites/UI/ButtonFrame");
            UIStyle.ButtonDownFrame = Content.Load<Texture2D>("Sprites/UI/ButtonFrameDown");
            UIStyle.ButtonHoverOverlay = Content.Load<Texture2D>("Sprites/UI/ButtonHover");
            UIStyle.ButtonFocusOverlay = Content.Load<Texture2D>("Sprites/UI/ButtonFocus");
            UIStyle.ButtonDownOverlay = Content.Load<Texture2D>("Sprites/UI/ButtonPress");

            UIStyle.TooltipFrame = Content.Load<Texture2D>("Sprites/UI/TooltipFrame");

            UIStyle.ButtonCornerSize = 20;
            UIStyle.ButtonVerticalPadding = 10;
            UIStyle.ButtonHorizontalPadding = 15;

            UIStyle.RadioButtonCornerSize = UIStyle.ButtonCornerSize;
            UIStyle.RadioButtonFrameOffset = 7;
            UIStyle.ButtonFrameLeft = Content.Load<Texture2D>("Sprites/UI/ButtonFrameLeft");
            UIStyle.ButtonDownFrameLeft = Content.Load<Texture2D>("Sprites/UI/ButtonFrameLeftDown");

            UIStyle.ButtonFrameMiddle = Content.Load<Texture2D>("Sprites/UI/ButtonFrameMiddle");
            UIStyle.ButtonDownFrameMiddle = Content.Load<Texture2D>("Sprites/UI/ButtonFrameMiddleDown");

            UIStyle.ButtonFrameRight = Content.Load<Texture2D>("Sprites/UI/ButtonFrameRight");
            UIStyle.ButtonDownFrameRight = Content.Load<Texture2D>("Sprites/UI/ButtonFrameRightDown");

            UIStyle.EditBoxFrame = Content.Load<Texture2D>("Sprites/UI/EditBoxFrame");
            UIStyle.EditBoxCornerSize = 20;

            UIStyle.Panel = Content.Load<Texture2D>("Sprites/UI/Panel01");
            UIStyle.PanelCornerSize = 15;

            UIStyle.NotebookStyle.TabCornerSize = 15;
            UIStyle.NotebookStyle.Tab = Content.Load<Texture2D>("Sprites/UI/Tab");
            UIStyle.NotebookStyle.TabFocus = Content.Load<Texture2D>("Sprites/UI/ButtonFocus");
            UIStyle.NotebookStyle.ActiveTab = Content.Load<Texture2D>("Sprites/UI/ActiveTab");
            UIStyle.NotebookStyle.ActiveTabFocus = Content.Load<Texture2D>("Sprites/UI/ActiveTabFocused");
            UIStyle.NotebookStyle.TabClose = Content.Load<Texture2D>("Sprites/UI/TabClose");
            UIStyle.NotebookStyle.TabCloseHover = Content.Load<Texture2D>("Sprites/UI/TabCloseHover");
            UIStyle.NotebookStyle.TabCloseDown = Content.Load<Texture2D>("Sprites/UI/TabCloseDown");
            UIStyle.NotebookStyle.UnreadTabMarker = Content.Load<Texture2D>("Sprites/UI/UnreadTabMarker");

            UIStyle.ListViewStyle.ListViewFrame = Content.Load<Texture2D>("Sprites/UI/ListFrame");
            UIStyle.ListViewStyle.ListViewFrameCornerSize = 10;
            UIStyle.ListRowInsertMarker = Content.Load<Texture2D>("Sprites/UI/ListRowInsertMarker");

            UIStyle.ListViewStyle.CellFrame = Content.Load<Texture2D>("Sprites/UI/ListRowFrame");
            UIStyle.ListViewStyle.CellCornerSize = 10;
            UIStyle.ListViewStyle.SelectedCellFrame = Content.Load<Texture2D>("Sprites/UI/ListRowFrameSelected");
            UIStyle.ListViewStyle.CellFocusOverlay = Content.Load<Texture2D>("Sprites/UI/ListRowFrameFocused");
            UIStyle.ListViewStyle.CellHoverOverlay = Content.Load<Texture2D>("Sprites/UI/ListRowFrameHover");
            UIStyle.ListViewStyle.ColumnHeaderFrame = Content.Load<Texture2D>("Sprites/UI/ButtonFrame"); // FIXME

            UIStyle.PopupFrame = Content.Load<Texture2D>("Sprites/UI/PopupFrame");
            UIStyle.PopupFrameCornerSize = 30;

            UIStyle.CheckBoxFrameHover = Content.Load<Texture2D>("Sprites/UI/CheckBoxFrameHover");
            UIStyle.CheckBoxChecked = Content.Load<Texture2D>("Sprites/UI/Checked");
            UIStyle.CheckBoxUnchecked = Content.Load<Texture2D>("Sprites/UI/Unchecked");

            UIStyle.SliderFrame = Content.Load<Texture2D>("Sprites/UI/ListFrame");

            UIStyle.VerticalScrollbar = Content.Load<Texture2D>("Sprites/UI/VerticalScrollbar");
            UIStyle.VerticalScrollbarCornerSize = 5;

            UIStyle.DropDownBoxEntryHoverOverlay = Content.Load<Texture2D>("Sprites/UI/ListRowFrameFocused");
            UIStyle.DropDownArrow = Content.Load<Texture2D>("Sprites/UI/DropDownArrow");

            UIStyle.SplitterFrame = Content.Load<Texture2D>("Sprites/UI/SplitterFrame");
            UIStyle.SplitterDragHandle = Content.Load<Texture2D>("Sprites/UI/SplitterDragHandle");
            UIStyle.SplitterCollapseArrow = Content.Load<Texture2D>("Sprites/UI/SplitterCollapseArrow");

            UIStyle.ProgressBarFrame = Content.Load<Texture2D>("Sprites/UI/EditBoxFrame");
            UIStyle.ProgressBarFrameCornerSize = 15;
            UIStyle.ProgressBar = Content.Load<Texture2D>("Sprites/UI/ProgressBar");
            UIStyle.ProgressBarCornerSize = 15;

            UIStyle.TextAreaFrame = Content.Load<Texture2D>("Sprites/UI/ListFrame");
            UIStyle.TextAreaFrameCornerSize = 15;
            UIStyle.TextAreaGutterFrame = Content.Load<Texture2D>("Sprites/UI/TextAreaGutterFrame");
            UIStyle.TextAreaGutterCornerSize = 15;

            //EnsureProperPresentationParams();
        }

        protected override void Update(GameTime gameTime)
        {
            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            base.Draw(gameTime);
        }
    }
}