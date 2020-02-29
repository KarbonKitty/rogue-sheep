using System;
using SFML.Graphics;

namespace RogueSheep
{
    public struct RogueColor : IEquatable<RogueColor>
    {
        public byte R;
        public byte G;
        public byte B;
        public byte A;

        public RogueColor(byte red, byte green, byte blue) : this(red, green, blue, 255) {}

        public RogueColor(byte red, byte green, byte blue, byte alpha)
        {
            R = red;
            G = green;
            B = blue;
            A = alpha;
        }

        public static implicit operator Color(RogueColor c) => new Color(c.R, c.G, c.B, c.A);
        public static implicit operator RogueColor(Color c) => new RogueColor(c.R, c.G, c.B, c.A);

        public static implicit operator RogueColor((byte r, byte g, byte b) t) => new RogueColor(t.r, t.g, t.b);
        public static implicit operator RogueColor((byte r, byte g, byte b, byte a) t) => new RogueColor(t.r, t.g, t.b, t.a);

        public bool Equals(RogueColor other) =>
            this.R == other.R &&
            this.G == other.G &&
            this.B == other.B &&
            this.A == other.A;
    }
}
