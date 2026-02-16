using System;
using SharpSaucer;

namespace SharpSaucerExample
{
    internal class Program
    {
        [STAThread]
        static int Main(string[] args)
        {
            using var app = new Application("sharp-saucer-example");
            return app.Run(
                onRun: app =>
                {
                    var window = new Window(app)
                    {
                        Size = (900, 600),
                        MinSize = (400, 300),
                        Title = "SharpSaucer – Hello World!"
                    };
                    var webview = new Webview(window)
                    {
                        DevTools = true,
                        ContextMenu = true
                    };
                    webview.SetUrl(new Url("https://saucer.app/"));
                    window.Show();
                },
                onFinish: app =>
                {
                    Console.WriteLine("Application is exiting...");
                }
            );
        }
    }
}