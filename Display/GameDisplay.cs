using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using SFML.Graphics;
using SFML.System;

namespace RogueSheep
{
    public class GameDisplay
    {
        private readonly Tilemap tilemap;
        private readonly GameTile[] tiles;
        private readonly int width;
        private readonly int fontWidth = 10;
        private readonly int fontHeight = 16;

        public GameDisplay(Vector2i size, int fontWidth, int fontHeight)
        {
            this.fontWidth = fontWidth;
            this.fontHeight = fontHeight;
            tilemap = CreateTilemap(fontWidth, fontHeight);
            tiles = new GameTile[size.X * size.Y];
            width = size.X;
            Clear();
        }

        public void Draw(GameTile tile, Vector2i position)
        {
            var i = VectorToIndex(position);
            this.tiles[i] = tile;
        }

        public void Draw(IList<GameTile> tiles, Vector2i position)
        {
            var i = VectorToIndex(position);
            foreach (var tile in tiles)
            {
                this.tiles[i++] = tile;
            }
        }

        public void Draw(IList<GameTile> tiles, Vector2i position, int lineLength)
        {
            var i = VectorToIndex(position);
            var column = 0;
            foreach (var tile in tiles)
            {
                this.tiles[i++] = tile;
                column++;
                if (column == lineLength)
                {
                    i -= column;
                    i += width;
                    column = 0;
                }
            }
        }

        public void Draw(string data, Vector2i position, Color? foreground = null, Color? background = null)
        {
            var fg = foreground ?? Color.White;
            var bg = background ?? Color.Black;

            Draw(data.Select(c => new GameTile((SpriteEnum)c, fg, bg)).ToList(), position);
        }

        public void DrawToWindow(RenderWindow window)
        {
            for (var i = 0; i < tiles.Length; i++)
            {
                var s = new Sprite(tilemap[tiles[i].Glyph])
                {
                    Position = IndexToVector(i),
                    Color = tiles[i].Foreground
                };
                window.Draw(s);
            }
        }

        public void Clear()
        {
            for (var i = 0; i < tiles.Length; i++)
            {
                tiles[i] = new GameTile(SpriteEnum.Empty);
            }
        }

        private int VectorToIndex(Vector2i vector)
        {
            return vector.Y * width + vector.X;
        }

        private Vector2f IndexToVector(int index)
        {
            return new Vector2f((index % width) * fontWidth, (index / width) * fontHeight);
        }

        private static Tilemap CreateTilemap(int fontWidth, int fontHeight)
        {
            var assembly = typeof(GameDisplay).Assembly;
            var resourceName = $"RogueSheep.data.font{fontWidth}x{fontHeight}.png";
            try
            {
                Stream font = assembly.GetManifestResourceStream(resourceName);
                return new Tilemap(font, fontWidth, fontHeight, DisplayConsts.FONT_COUNT);
            }
            catch
            {
                // add actual error logging?
                throw;
            }
        }
    }
}
