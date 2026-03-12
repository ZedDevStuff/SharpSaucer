using SharpSaucer;
using System;

namespace SharpSaucerExample
{
    internal class Program
    {
        [STAThread]
        static int Main(string[] args)
        {
            using var app = new SaucerApplication("sharp-saucer-example");
            return app.Run(
                onRun: app =>
                {
                    Console.WriteLine($"Using Saucer version: {app.Version}");
                    var screen = app.Screens[0];
                    Console.WriteLine($"Primary screen name: {screen.Name}");
                    Console.WriteLine($"Primary screen size: {screen.Size}");
                    Console.WriteLine($"Primary screen Position: {screen.Position}");

                    var window = new SaucerWindow(app)
                    {
                        Size = (900, 600),
                        MinSize = (400, 300),
                        Title = "SharpSaucer – Hello World!"
                    };
                    //var webview = new SaucerWebView(window)
                    //{
                    //    DevTools = true,
                    //    ContextMenu = true
                    //};
                    //webview.SetUrl(new Url("https://saucer.app/"));
                    window.Show();
                    var title = window.Title;
                    Console.WriteLine(title);
                },
                onFinish: app =>
                {
                    Console.WriteLine("Application is exiting...");
                }
            );
        }
    }
}