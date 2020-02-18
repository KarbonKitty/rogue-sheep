using System;
using SFML.Graphics;
using SFML.Window;

namespace RogueSheep
{
    public static class WindowBuilder
    {
        public static RenderWindow CreateWindow(string title, uint width, uint height)
        {
            var videoMode = new VideoMode(
                width,
                height,
                DisplayConsts.BitDepth);

            var contextSettings = new ContextSettings
            {
                DepthBits = DisplayConsts.BitDepth
            };

            var window = new RenderWindow(
                videoMode,
                title,
                Styles.Titlebar | Styles.Close,
                contextSettings);

            window.SetFramerateLimit(60);

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
