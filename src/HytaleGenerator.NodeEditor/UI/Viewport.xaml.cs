using HytaleGenerator.Math;
using Noesis;

namespace HytaleGenerator.NodeEditor.UI;

public class Viewport : UserControl
{
    private const double ZoomSpeed = 0.1f;

    private readonly Canvas _bodyElement;
    private readonly Rectangle _selectionElement;

    private DVector2 _pan;
    private double _zoom = 1.0;

    private bool _panning;
    private bool _selecting;

    private DVector2? _mousePos;
    private DVector2 _selectionCornerA;
    private DVector2 _selectionCornerB;

    public Viewport()
    {
        GUI.LoadComponent(this, "/HytaleGenerator.NodeEditor;component/UI/Viewport.xaml");

        _bodyElement = (Canvas)FindName("Viewport.Body");
        _bodyElement.RenderTransform = new MatrixTransform();

        _selectionElement = (Rectangle)FindName("Viewport.Selection");

        MouseMove += OnMouseMove;
        MouseUp += OnMouseUp;
        MouseDown += OnMouseDown;
        MouseWheel += OnMouseWheel;
    }

    private void OnMouseMove(object sender, MouseEventArgs args)
    {
        var position = PointToVec(args.GetPosition(this));

        if (_selecting)
        {
            _selectionCornerB = PointToVec(args.GetPosition(_bodyElement));
            UpdateSelectionTransform();
        }

        if (_panning)
        {
            var delta = position - _mousePos;
            if (delta.HasValue)
            {
                PanRelatively(delta.Value);
            }
        }

        _mousePos = position;
    }

    private void OnMouseDown(object sender, MouseButtonEventArgs args)
    {
        switch (args.ChangedButton)
        {
            case MouseButton.Left when !_panning:
                _selecting = true;
                _selectionCornerA = _selectionCornerB = PointToVec(args.GetPosition(relativeTo: _bodyElement));
                _selectionElement.Visibility = Visibility.Visible;
                UpdateSelectionTransform();
                break;

            case MouseButton.Right:
                _panning = true;
                break;
        }

        if ((_selecting || _panning) && !IsMouseCaptured)
        {
            CaptureMouse();
        }
    }

    private void OnMouseUp(object sender, MouseButtonEventArgs args)
    {
        var position = PointToVec(args.GetPosition(this));

        switch (args.ChangedButton)
        {
            case MouseButton.Left:
                _selecting = false;
                _selectionElement.Visibility = Visibility.Hidden;
                // TODO: Select all nodes within the rectangle.
                break;

            case MouseButton.Right:
                _panning = false;
                break;
        }

        if (!_selecting && !_panning && IsMouseCaptured)
        {
            ReleaseMouseCapture();
        }
    }

    /// <summary>
    /// Handles scrolling of the mouse wheel, which is used to zoom in and out of the viewport.
    /// </summary>
    private void OnMouseWheel(object sender, MouseWheelEventArgs args)
    {
        var position = args.GetPosition(_bodyElement);
        var direction = int.Sign(args.Delta);
        var scaleFactor = 1.0 + direction * ZoomSpeed;
        ZoomRelativelyAtPoint(new DVector2(position.X, position.Y), scaleFactor);
    }

    private void PanRelatively(DVector2 delta)
    {
        _pan += delta;
        UpdateBodyTransform();
    }

    private void ZoomRelativelyAtPoint(DVector2 referencePoint, double scaleFactor)
    {
        const double minZoom = 0.02;
        
        if (double.IsNaN(_zoom))
        {
            _zoom = 1.0;
            UpdateBodyTransform();
            return;
        }

        // Prevent zooming out too far.
        // FIXME: At around 0.02 all child elements of this control get removed. Figure out why this is.
        if (scaleFactor < 1.0 && _zoom <= minZoom)
        {
            return;
        }

        _pan += _zoom * (referencePoint - referencePoint * scaleFactor);
        _zoom = double.Max(_zoom * scaleFactor, minZoom);

        UpdateBodyTransform();
    }

    private void UpdateBodyTransform()
    {
        var transform = _bodyElement.RenderTransform as MatrixTransform ?? new MatrixTransform();
        transform.Matrix = new Matrix((float)_zoom, 0, 0, (float)_zoom, (float)_pan.X, (float)_pan.Y);
        UpdateSelectionTransform();
    }

    private void UpdateSelectionTransform()
    {
        var localA = PointToVec(_bodyElement.TranslatePoint(new Point((float)_selectionCornerA.X, (float)_selectionCornerA.Y), this));
        var localB = PointToVec(_bodyElement.TranslatePoint(new Point((float)_selectionCornerB.X, (float)_selectionCornerB.Y), this));
        var localMin = DVector2.Min(localA, localB);
        var localSize = DVector2.Abs(localA - localB);

        Canvas.SetLeft(_selectionElement, (float)localMin.X);
        Canvas.SetTop(_selectionElement, (float)localMin.Y);
        _selectionElement.Width = (float)localSize.X;
        _selectionElement.Height = (float)localSize.Y;
    }

    private static DVector2 PointToVec(Point point) => new(point.X, point.Y);
}