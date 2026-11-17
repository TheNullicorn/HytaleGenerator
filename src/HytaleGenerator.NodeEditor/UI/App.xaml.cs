using NoesisApp;

namespace HytaleGenerator.NodeEditor.UI;

public class App : Application
{
    protected override Display CreateDisplay() => new XDisplay();

    protected override RenderContext CreateRenderContext() => new RenderContextGLX();
}