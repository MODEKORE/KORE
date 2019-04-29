using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System.IO;
using System;

namespace 물리를물리도록
{
    /// <summary>
    /// This is the main type for your game.
    /// </summary>
    public class Game1 : Game
    {
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;

        public Game1()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
        }
        static string[] Settings = File.ReadAllLines("물리를물리도록_기본세팅.txt");
        Texture2D Grid;
        Texture2D Ground;
        Texture2D Ball;
        Texture2D Ball2;
        Vector2 mover;
        Vector2 mover2;
        int Gravity = 0;
        int Gravity2 = 0;
        int Accel = Convert.ToInt32(Settings[0].Substring(0, Settings[0].IndexOf(" ")));
        float SideAccel = Convert.ToSingle(Settings[1].Substring(0, Settings[1].IndexOf(" ")));
        float UpAccel = Convert.ToSingle(Settings[2].Substring(0, Settings[2].IndexOf(" ")));
        int Dormamu = Convert.ToInt32(Settings[3].Substring(0, Settings[3].IndexOf(" ")));
        int Dnum = 0;
        bool ActivateG = true;
        bool SideBoostL = false;
        bool SideBoostR = false;
        bool SideBoostL2 = false;
        bool SideBoostR2 = false;
        bool UpBoost = false;
        bool UpBoost2 = false;

        /// <summary>
        /// Allows the game to perform any initialization it needs to before starting to run.
        /// This is where it can query for any required services and load any non-graphic
        /// related content.  Calling base.Initialize will enumerate through any components
        /// and initialize them as well.
        /// </summary>
        protected override void Initialize()
        {
            // TODO: Add your initialization logic here
            
            base.Initialize();
        }

        /// <summary>
        /// LoadContent will be called once per game and is the place to load
        /// all of your content.
        /// </summary>
        protected override void LoadContent()
        {
            // Create a new SpriteBatch, which can be used to draw textures.
            spriteBatch = new SpriteBatch(GraphicsDevice);

            // TODO: use this.Content to load your game content here
            Grid = Content.Load<Texture2D>("Background/grid");
            Ground = Content.Load<Texture2D>("Background/grass");
            Ball = Content.Load<Texture2D>("Object/ball");
            Ball2 = Content.Load<Texture2D>("Object/ball2");
            mover = new Vector2(30,230);
            mover2 = new Vector2(630, 230);
        }

        /// <summary>
        /// UnloadContent will be called once per game and is the place to unload
        /// game-specific content.
        /// </summary>
        protected override void UnloadContent()
        {
            // TODO: Unload any non ContentManager content here
        }

        /// <summary>
        /// Allows the game to run logic such as updating the world,
        /// checking for collisions, gathering input, and playing audio.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Update(GameTime gameTime)
        {
            //if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
            //    Exit();
            if(Keyboard.GetState().IsKeyDown(Keys.F5))
            {
                Settings = File.ReadAllLines("KOREPhysics_Settings.txt");
                Accel = Convert.ToInt32(Settings[0].Substring(0, Settings[0].IndexOf(" ")));
                SideAccel = Convert.ToSingle(Settings[1].Substring(0, Settings[1].IndexOf(" ")));
                UpAccel = Convert.ToSingle(Settings[2].Substring(0, Settings[2].IndexOf(" ")));
                Dormamu = Convert.ToInt32(Settings[3].Substring(0, Settings[3].IndexOf(" ")));
            }

            if (Keyboard.GetState().IsKeyDown(Keys.Space)) goto Fin;

            if (Keyboard.GetState().IsKeyDown(Keys.LeftControl)|| Keyboard.GetState().IsKeyDown(Keys.RightControl))
            {
                Dnum += 1;
                if (Dnum < Dormamu)
                {
                    goto Fin;
                }
                else Dnum = 0;
            }


            if (Keyboard.GetState().IsKeyDown(Keys.LeftShift) && Keyboard.GetState().IsKeyDown(Keys.W) && !UpBoost)
            {
                UpBoost = true;
                ActivateG = true;
                Gravity = 0;
                goto Fin;
            }
            if (Keyboard.GetState().IsKeyDown(Keys.RightShift) && Keyboard.GetState().IsKeyDown(Keys.Up) && !UpBoost2)
            {
                UpBoost2 = true;
                ActivateG = true;
                Gravity2 = 0;
                goto Fin;
            }

            if (Keyboard.GetState().IsKeyDown(Keys.LeftShift) && Keyboard.GetState().IsKeyDown(Keys.A) && !SideBoostL)
            {
                SideBoostL = true;
                ActivateG = true;
                goto Fin;
            }
            if (Keyboard.GetState().IsKeyDown(Keys.LeftShift) && Keyboard.GetState().IsKeyDown(Keys.D) && !SideBoostR)
            {
                SideBoostR = true;
                ActivateG = true;
                goto Fin;
            }
            if (Keyboard.GetState().IsKeyDown(Keys.RightShift) && Keyboard.GetState().IsKeyDown(Keys.Left) && !SideBoostL2)
            {
                SideBoostL2 = true;
                ActivateG = true;
                goto Fin;
            }
            if (Keyboard.GetState().IsKeyDown(Keys.RightShift) && Keyboard.GetState().IsKeyDown(Keys.Right) && !SideBoostR2)
            {
                SideBoostR2 = true;
                ActivateG = true;
                goto Fin;
            }

            if (SideBoostL)
            {
                if (mover.Y >= 410)
                {
                    SideBoostL = false;
                }
                else if (mover.X - SideAccel >= 0) mover.X -= SideAccel;
                else mover.X = 0;
            }
            if (SideBoostR)
            {
                if (mover.Y >= 410)
                {
                    SideBoostR = false;
                }
                else if (mover.X + SideAccel < 780) mover.X += SideAccel;
                else mover.X =780;
            }
            if (SideBoostL2)
            {
                if (mover2.Y >= 410)
                {
                    SideBoostL2 = false;
                }
                else if (mover2.X - SideAccel >= 0) mover2.X -= SideAccel;
                else mover2.X = 0;
            }
            if (SideBoostR2)
            {
                if (mover2.Y >= 410)
                {
                    SideBoostR2 = false;
                }
                else if (mover2.X + SideAccel < 780) mover2.X += SideAccel;
                else mover2.X = 780;
            }


            if (Keyboard.GetState().IsKeyDown(Keys.G) && !ActivateG)
            {
                ActivateG = true;
            }
            if (Keyboard.GetState().IsKeyDown(Keys.F) && ActivateG)
            {
                Gravity = 0;
                Gravity2 = 0;
                ActivateG = false;
                SideBoostL = false;
                SideBoostL2 = false;
                SideBoostR = false;
                SideBoostR2 = false;
                UpBoost = false;
                UpBoost2 = false;
            }

            //임시적인 논리코드. 특수 명령 운동중에는 컨트롤러를 블럭시킨다.
            if (!SideBoostL && !SideBoostR &&!UpBoost)
            {
                if (Keyboard.GetState().IsKeyDown(Keys.W))
                {
                    if (mover.Y - 3 >= 0) mover.Y -= 3;
                    else mover.Y = 0;
                }
                if (Keyboard.GetState().IsKeyDown(Keys.S))
                {
                    if (mover.Y + 3 <= 410) mover.Y += 3;
                    else mover.Y = 410;
                }
                if (Keyboard.GetState().IsKeyDown(Keys.A))
                {
                    if (mover.X - 3 >= 0) mover.X -= 3;
                    else mover.X = 0;
                }
                if (Keyboard.GetState().IsKeyDown(Keys.D))
                {
                    if (mover.X < 780) mover.X += 3;
                    else mover.X = 780;
                }
            }
            if (!SideBoostL2 && !SideBoostR2 && !UpBoost2)
            {
                if (Keyboard.GetState().IsKeyDown(Keys.Up))
                {
                    if (mover2.Y - 3 >= 0) mover2.Y -= 3;
                    else mover2.Y = 0;
                }
                if (Keyboard.GetState().IsKeyDown(Keys.Down))
                {
                    if (mover2.Y + 3 <= 410) mover2.Y += 3;
                    else mover2.Y = 410;
                }
                if (Keyboard.GetState().IsKeyDown(Keys.Left))
                {
                    if (mover2.X - 3 >= 0) mover2.X -= 3;
                    else mover2.X = 0;
                }
                if (Keyboard.GetState().IsKeyDown(Keys.Right))
                {
                    if (mover2.X < 780) mover2.X += 3;
                    else mover2.X = 780;
                }
            }



            if (UpBoost)
            {
                if (UpAccel - Gravity > 0)
                {
                    mover.Y = mover.Y - UpAccel + Gravity;
                    Gravity += Accel;
                    goto Skip1G;
                }
                else
                {
                    UpBoost = false;
                    Gravity = 0;
                }
            }

            if (ActivateG &&
                !Keyboard.GetState().IsKeyDown(Keys.W) &&
                !Keyboard.GetState().IsKeyDown(Keys.S))
            {
                Gravity += Accel;
                if (mover.Y + Gravity <= 410) mover.Y += Gravity;
                else
                {
                    mover.Y = 410;
                    Gravity = 0;
                }
            }
            else
            {
                Gravity = 0;
                SideBoostL = false;
                SideBoostR = false;
                UpBoost = false;
            }
            Skip1G:;


            if (UpBoost2)
            {
                if (UpAccel - Gravity2 > 0)
                {
                    mover2.Y = mover2.Y - UpAccel + Gravity2;
                    Gravity2 += Accel;
                    goto Fin; //아래 추가할 일 생기면 Skin2G: 도 만들자!
                }
                else
                {
                    UpBoost2 = false;
                    Gravity2 = 0;
                }
            }
            if (ActivateG &&
                !Keyboard.GetState().IsKeyDown(Keys.Up) &&
                !Keyboard.GetState().IsKeyDown(Keys.Down))
            {
                Gravity2 += Accel;
                if (mover2.Y + Gravity2 <= 410) mover2.Y += Gravity2;
                else
                {
                    Gravity2 = 0;
                    mover2.Y = 410;
                }
            }
            else
            {
                Gravity2 = 0;
                SideBoostL2 = false;
                SideBoostR2 = false;
                UpBoost2 = false;
            }

            // TODO: Add your update logic here

            Fin:;
            base.Update(gameTime);
        }

        /// <summary>
        /// This is called when the game should draw itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            // TODO: Add your drawing code here
            spriteBatch.Begin();
            spriteBatch.Draw(Grid, new Rectangle(0, 0,800,500), Color.White);
            spriteBatch.Draw(Ground, new Rectangle(0, 429, 800, 51), Color.White);
            spriteBatch.Draw(Ball, mover, Color.White); spriteBatch.Draw(Ball2, mover2, Color.White);
            spriteBatch.End();
            //#############################
            base.Draw(gameTime);
        }
    }
}
