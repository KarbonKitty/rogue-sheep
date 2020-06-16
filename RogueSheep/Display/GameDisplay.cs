using System.Collections.Generic;
using System.Linq;

using SFML.Graphics;

namespace RogueSheep.Display
{
    public class GameDisplay
    {
        public Point2i Offset { get; }
        public Point2i Size { get; }
        public Point2i SizePx { get; }
        private readonly ITilemap tilemap;
        private readonly GameTile[] tiles;
        private readonly int fontWidth;
        private readonly int fontHeight;

        public GameDisplay(Point2i size, Point2i offset, ITilemapConfiguration tilemapConfiguration)
        {
            fontWidth = tilemapConfiguration.SpriteWidth;
            fontHeight = tilemapConfiguration.SpriteHeight;
            tilemap = new TilemapFactory().CreateTilemap(tilemapConfiguration);
            tiles = new GameTile[size.X * size.Y];
            Size = size;
            SizePx = (size.X * fontWidth, size.Y * fontHeight);
            Offset = offset;
            Clear();
        }

        public void Draw(CP437Glyph glyph, Point2i position, RogueColor? foreground = null, RogueColor? background = null)
            => Draw(new GameTile(glyph, foreground ?? RogueColor.White, background ?? RogueColor.Transparent), position);

        public void Draw(IPresentable presentable, Point2i position) => Draw(presentable.Presentation, position);

        public void Draw(IList<IPresentable> presentables, Point2i position) => Draw(presentables.Select(p => p.Presentation).ToList(), position);

        public void Draw(IList<IPresentable> presentables, Point2i position, int lineLength)
        {
            var i = VectorToIndex(position);
            var column = 0;
            foreach (var presentable in presentables)
            {
                this.tiles[i++] = presentable.Presentation;
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

            Draw(data.Select(c => new GameTile((CP437Glyph)c, fg, bg)).ToList(), position);
        }

        public void DrawToWindow(RenderWindow window)
        {
            for (var i = 0; i < tiles.Length; i++)
            {
                var b = new Sprite(BackgroundTile.Tile)
                {
                    Position = IndexToVector(i),
                    Color = tiles[i].Background
                };
                window.Draw(b);
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
                tiles[i] = new GameTile(CP437Glyph.Empty);
            }
        }

        public void Draw(GameTile tile, Point2i position)
        {
            var i = VectorToIndex(position);
            tiles[i] = tile;
        }

        public void Draw(IList<GameTile> gameTiles, Point2i position)
        {
            var i = VectorToIndex(position);
            foreach (var gameTile in gameTiles)
            {
                tiles[i++] = gameTile;
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
