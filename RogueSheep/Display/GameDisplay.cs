using System.Collections.Generic;
using System.IO;
using System.Linq;
using SFML.Graphics;

namespace RogueSheep
{
    public class GameDisplay
    {
        public Point2i Offset { get; }
        public Point2i Size { get; }
        public Point2i SizePx { get; }
        private readonly Tilemap tilemap;
        private readonly GameTile[] tiles;
        private readonly int fontWidth;
        private readonly int fontHeight;

        public GameDisplay(Point2i size, Point2i offset, int fontWidth, int fontHeight)
        {
            this.fontWidth = fontWidth;
            this.fontHeight = fontHeight;
            tilemap = new TilemapFactory().CreateTilemapFromResource(fontWidth, fontHeight, 16, 16);
            tiles = new GameTile[size.X * size.Y];
            Size = size;
            SizePx = (size.X * fontWidth, size.Y * fontHeight);
            Offset = offset;
            Clear();
        }

        public void Draw(GameTile tile, Point2i position)
        {
            var i = VectorToIndex(position);
            this.tiles[i] = tile;
        }

        public void Draw(IList<GameTile> tiles, Point2i position)
        {
            var i = VectorToIndex(position);
            foreach (var tile in tiles)
            {
                this.tiles[i++] = tile;
            }
        }

        public void Draw(IList<GameTile> tiles, Point2i position, int lineLength)
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
                    i += Size.X;
                    column = 0;
                }
            }
        }

        public void Draw(string data, Point2i position, Color? foreground = null, Color? background = null)
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

        private int VectorToIndex(Point2i vector)
        {
            return (vector.Y * Size.X) + vector.X;
        }

        private Point2f IndexToVector(int index)
        {
            return new Point2f((index % Size.X * fontWidth) + Offset.X, (index / Size.X * fontHeight) + Offset.Y);
        }
    }
}
