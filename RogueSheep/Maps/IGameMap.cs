using System.Collections.Generic;
using RogueSheep.Display;

namespace RogueSheep.Maps
{
    public interface IGameMap<T>
    {
        IList<T> Actors { get; }
        Point2i Size { get; }

        bool IsAvailableForMove(Point2i position);
        bool IsTransparent(Point2i position);
        Point2i PositionOf(T actor);
        IPresentable[] GetViewport(Point2i size, Point2i center);
        IPresentable[] GetMap();
        Point2i ClosestFreePosition(Point2i origin);
    }
}
