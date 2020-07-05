using System.Collections.Generic;
using RogueSheep.Display;

namespace RogueSheep.Maps
{
    public interface IGameMap<TObject, TActor>
    {
        IList<TActor> Actors { get; }
        Point2i Size { get; }
        GameGrid<bool> TransparencyGrid { get; }

        TObject GetActualObject(Point2i location);
        TObject GetObjectFromMemory(Point2i location);

        bool IsAvailableForMove(Point2i position);
        bool IsTransparent(Point2i position);
        Point2i PositionOf(TActor actor);
        GameTile[] GetViewport(Point2i size, Point2i center);
        GameTile[] GetMaskedViewport(Point2i size, Point2i center, GameGrid<bool> visibilityMap);
        IPresentable[] GetMap();
        Point2i ClosestFreePosition(Point2i origin);
    }
}
