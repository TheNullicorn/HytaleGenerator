using Noesis;
using NoesisApp;

namespace HytaleGenerator.NodeEditor.UI;

public class MainWindow : Window
{
    public MainWindow()
    {
        GUI.LoadComponent(this, "UI/MainWindow.xaml");
        DataContext = new ViewModel();
    }
}