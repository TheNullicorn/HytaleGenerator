using HytaleGenerator.NodeEditor.UI;

namespace HytaleGenerator.NodeEditor;

public static class Program
{
    public static void Main()
    {
        var app = new App
        {
            Uri = "/HytaleGenerator.NodeEditor;component/UI/App.xaml"
        };
        app.Run();
    }
}