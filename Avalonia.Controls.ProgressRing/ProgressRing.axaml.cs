using System.Reactive.Disposables;
using System.Reactive.Linq;
using Avalonia;
using Avalonia.Controls;
using Avalonia.Controls.Mixins;

namespace Avalonia.Controls.ProgressRing
{
  /// <summary><see cref="ProgressRing"/> provides a Windows 10 styled circular loading animation.</summary>
  /// <remarks>
  ///   https://github.com/AvaloniaUI/Documentation/blob/master/docs/authoring-controls/defining-properties.md
  /// </remarks>
  public class ProgressRing : ContentControl
  {
    private Thickness _ellipseOffset;

    public static readonly AvaloniaProperty<double> EllipseDiameterProperty =
        AvaloniaProperty.Register<ProgressRing, double>(nameof(EllipseDiameter), inherits: true);

    public static readonly AvaloniaProperty<double> EllipseDiameterScaleProperty =
        AvaloniaProperty.Register<ProgressRing, double>(nameof(EllipseDiameterScale), inherits: true, defaultValue: 1D);

    public static readonly AvaloniaProperty<Thickness> EllipseOffsetProperty =
        AvaloniaProperty.Register<ProgressRing, Thickness>(nameof(EllipseOffset), inherits: true);

    public static readonly StyledProperty<bool> IsActiveProperty =
      AvaloniaProperty.Register<ProgressRing, bool>(nameof(IsActive));

    public static readonly AvaloniaProperty MaxSideLengthProperty =
        AvaloniaProperty.Register<ProgressRing, double>(nameof(MaxSideLength), inherits: true);

    private readonly CompositeDisposable _disposable = new CompositeDisposable();

    public ProgressRing()
    {
      this.GetObservable(Control.BoundsProperty).ForEachAsync((rect) =>
      {
        SetEllipseDiameter(rect.Width);
        SetEllipseOffset(rect.Width);
        SetMaxSideLength(rect.Width);
      }).DisposeWith(_disposable);
    }

    public double EllipseDiameter
    {
      get => (double)GetValue(EllipseDiameterProperty);
      private set => SetValue(EllipseDiameterProperty, value);
    }

    public double EllipseDiameterScale
    {
      get => (double)GetValue(EllipseDiameterScaleProperty);
      set => SetValue(EllipseDiameterScaleProperty, value);
    }

    public Thickness EllipseOffset
    {
      get => _ellipseOffset;
      private set => SetAndRaise(EllipseOffsetProperty, ref _ellipseOffset, value);
    }

    public bool IsActive
    {
      get => GetValue(IsActiveProperty);
      set => SetValue(IsActiveProperty, value);
    }

    public double MaxSideLength
    {
      get => (double)GetValue(MaxSideLengthProperty);
      private set => SetValue(MaxSideLengthProperty, value);
    }

    private void SetEllipseDiameter(double width)
    {
      // OG: EllipseDiameter = (width / 8) * EllipseDiameterScale;
      EllipseDiameter = (width / 5) * EllipseDiameterScale;
    }

    private void SetEllipseOffset(double width)
    {
      EllipseOffset = new Thickness(0, width / 2, 0, 0);
    }

    private void SetMaxSideLength(double width)
    {
      MaxSideLength = width <= 20 ? 20 : width;
    }
  }
}
