using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework;

namespace GameProject
{
    public sealed class Input
    {
        Input() {
        }
        private static readonly object padlock = new object();
        private static Input instance = null;
        public static Input Instance {
            get {
                lock (padlock) {
                    if(instance == null){
                        instance = new Input();
                    }
                    return instance;
                }
            }
        }


        private KeyboardState kState;
        private MouseState mState;
        private Matrix scaleMatrix;
        private Camera camera;

        public void Update() {
            kState = Keyboard.GetState();
            mState = Mouse.GetState();
        }

        public KeyboardState getKState() {
            return kState;
        }

        public MouseState getMState() {
            return mState;
        }

        public Vector2 getGlobalMousePos()
        {
            Vector2 mousePos = Vector2.Transform(new Vector2(mState.X, mState.Y), Matrix.Invert(camera.getTransform()));
            return mousePos;
        }

        public Vector2 getStaticMousePos() {
            Vector2 mousePos = Vector2.Transform(new Vector2(mState.X, mState.Y), Matrix.Invert(scaleMatrix));
            return mousePos;
        }

        public void setScaleMatrix(Matrix scaleMatrix) {
            this.scaleMatrix = scaleMatrix;
        }

        public void setCamera(Camera camera) {
            this.camera = camera;
        }

        public Camera getCamera() {
            return camera;
        }
    }
}
