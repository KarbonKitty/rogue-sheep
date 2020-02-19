using System;
using SFML.Graphics;
using SFML.Window;

namespace RogueSheep.Display
{
    // TODO: wrap RenderWindow in some RogueSheep class
    public static class WindowBuilder
    {
        private const int BitDepth = 24;
        private const int FrameLimit = 60;

        public static RenderWindow CreateWindow(string title, uint width, uint height)
        {
            var videoMode = new VideoMode(
                width,
                height,
                BitDepth);

            var contextSettings = new ContextSettings
            {
                DepthBits = BitDepth
            };

            var window = new RenderWindow(
                videoMode,
                title,
                Styles.Titlebar | Styles.Close,
                contextSettings);

            window.SetFramerateLimit(FrameLimit);

            //registering event handlers
            window.Closed += Window_Close;

            return window;
        }

        private static void Window_Close(object sender, EventArgs e)
        {
            ((Window)sender).Close();
            Environment.Exit(0);
        }
    }
}
