using Noesis;

namespace HytaleGenerator.NodeEditor.UI;

public class Node : UserControl
{
    public Node()
    {
        GUI.LoadComponent(this, "/HytaleGenerator.NodeEditor;component/UI/Node.xaml");
    }
}