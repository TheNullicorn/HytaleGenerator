using Noesis;

namespace HytaleGenerator.NodeEditor.UI;

/// <summary>
/// Logic for application ViewModel
/// </summary>
public class ViewModel
{
    public Color TopColor { get; set; }
    public Color BottomColor { get; set; }

    public ViewModel()
    {
        TopColor = Color.FromRgb(17, 102, 157);
        BottomColor = Color.FromRgb(18, 57, 87);
    }
}