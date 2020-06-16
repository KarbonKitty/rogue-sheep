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

        public RogueColor(byte red, byte green, byte blue) : this(red, green, blue, 255) { }

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

        public static readonly RogueColor AliceBlue = new RogueColor(240, 248, 255);
        public static readonly RogueColor AntiqueWhite = new RogueColor(250, 235, 215);
        public static readonly RogueColor Aqua = new RogueColor(0, 255, 255);
        public static readonly RogueColor Aquamarine = new RogueColor(127, 255, 212);
        public static readonly RogueColor Azure = new RogueColor(240, 255, 255);
        public static readonly RogueColor Beige = new RogueColor(245, 245, 220);
        public static readonly RogueColor Bisque = new RogueColor(255, 228, 196);
        public static readonly RogueColor Black = new RogueColor(0, 0, 0);
        public static readonly RogueColor BlanchedAlmond = new RogueColor(255, 235, 205);
        public static readonly RogueColor Blue = new RogueColor(0, 0, 255);
        public static readonly RogueColor BlueViolet = new RogueColor(138, 43, 226);
        public static readonly RogueColor Brown = new RogueColor(165, 42, 42);
        public static readonly RogueColor Burlywood = new RogueColor(222, 184, 135);
        public static readonly RogueColor CadetBlue = new RogueColor(95, 158, 160);
        public static readonly RogueColor Chartreuse = new RogueColor(127, 255, 0);
        public static readonly RogueColor Chocolate = new RogueColor(210, 105, 30);
        public static readonly RogueColor Coral = new RogueColor(255, 127, 80);
        public static readonly RogueColor CornflowerBlue = new RogueColor(100, 149, 237);
        public static readonly RogueColor Cornsilk = new RogueColor(255, 248, 220);
        public static readonly RogueColor Crimson = new RogueColor(220, 20, 60);
        public static readonly RogueColor Cyan = new RogueColor(0, 255, 255);
        public static readonly RogueColor DarkBlue = new RogueColor(0, 0, 139);
        public static readonly RogueColor DarkCyan = new RogueColor(0, 139, 139);
        public static readonly RogueColor DarkGoldenrod = new RogueColor(184, 134, 11);
        public static readonly RogueColor DarkGray = new RogueColor(169, 169, 169);
        public static readonly RogueColor DarkGreen = new RogueColor(0, 100, 0);
        public static readonly RogueColor DarkGrey = new RogueColor(169, 169, 169);
        public static readonly RogueColor DarkKhaki = new RogueColor(189, 183, 107);
        public static readonly RogueColor DarkMagenta = new RogueColor(139, 0, 139);
        public static readonly RogueColor DarkOliveGreen = new RogueColor(85, 107, 47);
        public static readonly RogueColor DarkOrange = new RogueColor(255, 140, 0);
        public static readonly RogueColor DarkOrchid = new RogueColor(153, 50, 204);
        public static readonly RogueColor DarkRed = new RogueColor(139, 0, 0);
        public static readonly RogueColor DarkSalmon = new RogueColor(233, 150, 122);
        public static readonly RogueColor DarkSeaGreen = new RogueColor(143, 188, 143);
        public static readonly RogueColor DarkSlateBlue = new RogueColor(72, 61, 139);
        public static readonly RogueColor DarkSlateGray = new RogueColor(47, 79, 79);
        public static readonly RogueColor DarkSlateGrey = new RogueColor(47, 79, 79);
        public static readonly RogueColor DarkTurquoise = new RogueColor(0, 206, 209);
        public static readonly RogueColor DarkViolet = new RogueColor(148, 0, 211);
        public static readonly RogueColor DeepPink = new RogueColor(255, 20, 147);
        public static readonly RogueColor DeepSkyBlue = new RogueColor(0, 191, 255);
        public static readonly RogueColor DimGray = new RogueColor(105, 105, 105);
        public static readonly RogueColor DimGrey = new RogueColor(105, 105, 105);
        public static readonly RogueColor DodgerBlue = new RogueColor(30, 144, 255);
        public static readonly RogueColor Firebrick = new RogueColor(178, 34, 34);
        public static readonly RogueColor FloralWhite = new RogueColor(255, 250, 240);
        public static readonly RogueColor ForestGreen = new RogueColor(34, 139, 34);
        public static readonly RogueColor Fuchsia = new RogueColor(255, 0, 255);
        public static readonly RogueColor Gainsboro = new RogueColor(220, 220, 220);
        public static readonly RogueColor GhostWhite = new RogueColor(248, 248, 255);
        public static readonly RogueColor Gold = new RogueColor(255, 215, 0);
        public static readonly RogueColor Goldenrod = new RogueColor(218, 165, 32);
        public static readonly RogueColor Gray = new RogueColor(128, 128, 128);
        public static readonly RogueColor Green = new RogueColor(0, 128, 0);
        public static readonly RogueColor GreenYellow = new RogueColor(173, 255, 47);
        public static readonly RogueColor Grey = new RogueColor(128, 128, 128);
        public static readonly RogueColor Honeydew = new RogueColor(240, 255, 240);
        public static readonly RogueColor HotPink = new RogueColor(255, 105, 180);
        public static readonly RogueColor IndianRed = new RogueColor(205, 92, 92);
        public static readonly RogueColor Indigo = new RogueColor(75, 0, 130);
        public static readonly RogueColor Ivory = new RogueColor(255, 255, 240);
        public static readonly RogueColor Khaki = new RogueColor(240, 230, 140);
        public static readonly RogueColor Lavender = new RogueColor(230, 230, 250);
        public static readonly RogueColor LavenderBlush = new RogueColor(255, 240, 245);
        public static readonly RogueColor LawnGreen = new RogueColor(124, 252, 0);
        public static readonly RogueColor LemonChiffon = new RogueColor(255, 250, 205);
        public static readonly RogueColor LightBlue = new RogueColor(173, 216, 230);
        public static readonly RogueColor LightCoral = new RogueColor(240, 128, 128);
        public static readonly RogueColor LightCyan = new RogueColor(224, 255, 255);
        public static readonly RogueColor LightGoldenrodYellow = new RogueColor(250, 250, 210);
        public static readonly RogueColor LightGray = new RogueColor(211, 211, 211);
        public static readonly RogueColor LightGreen = new RogueColor(144, 238, 144);
        public static readonly RogueColor LightGrey = new RogueColor(211, 211, 211);
        public static readonly RogueColor LightPink = new RogueColor(255, 182, 193);
        public static readonly RogueColor LightSalmon = new RogueColor(255, 160, 122);
        public static readonly RogueColor LightSeaGreen = new RogueColor(32, 178, 170);
        public static readonly RogueColor LightSkyBlue = new RogueColor(135, 206, 250);
        public static readonly RogueColor LightSlateGray = new RogueColor(119, 136, 153);
        public static readonly RogueColor LightSlateGrey = new RogueColor(119, 136, 153);
        public static readonly RogueColor LightSteelBlue = new RogueColor(176, 196, 222);
        public static readonly RogueColor LightYellow = new RogueColor(255, 255, 224);
        public static readonly RogueColor Lime = new RogueColor(0, 255, 0);
        public static readonly RogueColor LimeGreen = new RogueColor(50, 205, 50);
        public static readonly RogueColor Linen = new RogueColor(250, 240, 230);
        public static readonly RogueColor Magenta = new RogueColor(255, 0, 255);
        public static readonly RogueColor Maroon = new RogueColor(128, 0, 0);
        public static readonly RogueColor MediumAquamarine = new RogueColor(102, 205, 170);
        public static readonly RogueColor MediumBlue = new RogueColor(0, 0, 205);
        public static readonly RogueColor MediumOrchid = new RogueColor(186, 85, 211);
        public static readonly RogueColor MediumPurple = new RogueColor(147, 112, 219);
        public static readonly RogueColor MediumSeaGreen = new RogueColor(60, 179, 113);
        public static readonly RogueColor MediumSlateBlue = new RogueColor(123, 104, 238);
        public static readonly RogueColor MediumSpringGreen = new RogueColor(0, 250, 154);
        public static readonly RogueColor MediumTurquoise = new RogueColor(72, 209, 204);
        public static readonly RogueColor MediumVioletRed = new RogueColor(199, 21, 133);
        public static readonly RogueColor MidnightBlue = new RogueColor(25, 25, 112);
        public static readonly RogueColor MintCream = new RogueColor(245, 255, 250);
        public static readonly RogueColor MistyRose = new RogueColor(255, 228, 225);
        public static readonly RogueColor Moccasin = new RogueColor(255, 228, 181);
        public static readonly RogueColor NavajoWhite = new RogueColor(255, 222, 173);
        public static readonly RogueColor Navy = new RogueColor(0, 0, 128);
        public static readonly RogueColor OldLace = new RogueColor(253, 245, 230);
        public static readonly RogueColor Olive = new RogueColor(128, 128, 0);
        public static readonly RogueColor OliveDrab = new RogueColor(107, 142, 35);
        public static readonly RogueColor Orange = new RogueColor(255, 165, 0);
        public static readonly RogueColor OrangeRed = new RogueColor(255, 69, 0);
        public static readonly RogueColor Orchid = new RogueColor(218, 112, 214);
        public static readonly RogueColor PaleGoldenrod = new RogueColor(238, 232, 170);
        public static readonly RogueColor PaleGreen = new RogueColor(152, 251, 152);
        public static readonly RogueColor PaleTurquoise = new RogueColor(175, 238, 238);
        public static readonly RogueColor PaleVioletRed = new RogueColor(219, 112, 147);
        public static readonly RogueColor PapayaWhip = new RogueColor(255, 239, 213);
        public static readonly RogueColor PeachPuff = new RogueColor(255, 218, 185);
        public static readonly RogueColor Peru = new RogueColor(205, 133, 63);
        public static readonly RogueColor Pink = new RogueColor(255, 192, 203);
        public static readonly RogueColor Plum = new RogueColor(221, 160, 221);
        public static readonly RogueColor PowderBlue = new RogueColor(176, 224, 230);
        public static readonly RogueColor Purple = new RogueColor(128, 0, 128);
        public static readonly RogueColor RebeccaPurple = new RogueColor(102, 51, 153);
        public static readonly RogueColor Red = new RogueColor(255, 0, 0);
        public static readonly RogueColor RosyBrown = new RogueColor(188, 143, 143);
        public static readonly RogueColor RoyalBlue = new RogueColor(65, 105, 225);
        public static readonly RogueColor SaddleBrown = new RogueColor(139, 69, 19);
        public static readonly RogueColor Salmon = new RogueColor(250, 128, 114);
        public static readonly RogueColor SandyBrown = new RogueColor(244, 164, 96);
        public static readonly RogueColor SeaGreen = new RogueColor(46, 139, 87);
        public static readonly RogueColor Seashell = new RogueColor(255, 245, 238);
        public static readonly RogueColor Sienna = new RogueColor(160, 82, 45);
        public static readonly RogueColor Silver = new RogueColor(192, 192, 192);
        public static readonly RogueColor SkyBlue = new RogueColor(135, 206, 235);
        public static readonly RogueColor SlateBlue = new RogueColor(106, 90, 205);
        public static readonly RogueColor SlateGray = new RogueColor(112, 128, 144);
        public static readonly RogueColor SlateGrey = new RogueColor(112, 128, 144);
        public static readonly RogueColor Snow = new RogueColor(255, 250, 250);
        public static readonly RogueColor SpringGreen = new RogueColor(0, 255, 127);
        public static readonly RogueColor SteelBlue = new RogueColor(70, 130, 180);
        public static readonly RogueColor Tan = new RogueColor(210, 180, 140);
        public static readonly RogueColor Teal = new RogueColor(0, 128, 128);
        public static readonly RogueColor Thistle = new RogueColor(216, 191, 216);
        public static readonly RogueColor Tomato = new RogueColor(255, 99, 71);
        public static readonly RogueColor Turquoise = new RogueColor(64, 224, 208);
        public static readonly RogueColor Violet = new RogueColor(238, 130, 238);
        public static readonly RogueColor Wheat = new RogueColor(245, 222, 179);
        public static readonly RogueColor White = new RogueColor(255, 255, 255);
        public static readonly RogueColor WhiteSmoke = new RogueColor(245, 245, 245);
        public static readonly RogueColor Yellow = new RogueColor(255, 255, 0);
        public static readonly RogueColor YellowGreen = new RogueColor(154, 205, 50);
    }
}
