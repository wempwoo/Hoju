using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BattleScene
{
    public class BarGauge : Entity
    {
        public float value;

        private SpriteRenderer spriteRenderer;
        private Texture2D texture;
        private Color[] pixels;

        void Start()
        {
            this.transform.localScale = new Vector2(0.6f, 0.1f);
            this.spriteRenderer = this.GetComponent<SpriteRenderer>();

            int w = 1;
            int h = 1;
            this.pixels = new Color[w];
            this.texture = new Texture2D(this.pixels.Length, h);
            //for (int i = 0; i < this.pixels.Length; i++) this.pixels[i] = t;
            //for (int i = 5; i < this.pixels.Length; i++) this.pixels[i] = f;
            //this.texture.SetPixels(this.pixels);
            this.texture.SetPixel(0, 0, t);
            //this.texture.SetPixel(2, 2, t);
            //this.texture.SetPixel(3, 3, t);
            this.texture.Apply();
            this.spriteRenderer.sprite = Sprite.Create(this.texture, new Rect(0, 0, w, h), Vector2.zero);

        }

        private static readonly Color t = new Color(0.4f, 0.9f, 0.5f);
        private static readonly Color f = new Color(0.9f, 0.5f, 0.5f);

        void Update()
        {
            //for (int i = 0; i < this.pixels.Length; i++) this.pixels[i] = t;
            //for (int i = 5; i < this.pixels.Length; i++) this.pixels[i] = f;
            //this.texture.SetPixels(this.pixels);
            //this.texture.Apply();
            //this.spriteRenderer.sprite = Sprite.Create(this.texture, new Rect(0, 0, 10, 1), Vector2.zero);
        }
    }
}