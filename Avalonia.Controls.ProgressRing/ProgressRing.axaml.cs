using System.Reactive.Disposables;
using System.Reactive.Linq;
using Avalonia;
using Avalonia.Controls;
using Avalonia.Controls.Mixins;

namespace Avalonia.Controls.Ex
{
  /// <summary><see cref="ProgressRing"/> provides a Windows 10 styled circular loading animation.</summary>
  /// <remarks>
  ///   * https://github.com/AvaloniaUI/Documentation/blob/master/docs/authoring-controls/defining-properties.md
  ///   * Adapted from MahApps Metro: https://github.com/MahApps/MahApps.Metro/blob/develop/src/MahApps.Metro/Controls/ProgressRing.cs.
  /// </remarks>
  public class ProgressRing : ContentControl
  {
    public static readonly AvaloniaProperty EllipseDiameterProperty =
        AvaloniaProperty.Register<ProgressRing, double>(nameof(EllipseDiameter), inherits: true);

    public static readonly AvaloniaProperty EllipseDiameterScaleProperty =
        AvaloniaProperty.Register<ProgressRing, double>(nameof(EllipseDiameterScale), inherits: true, defaultValue: 1D);

    public static readonly AvaloniaProperty EllipseOffsetProperty =
        AvaloniaProperty.Register<ProgressRing, Thickness>(nameof(EllipseOffset), inherits: true);

    public static readonly StyledProperty<bool> IsActiveProperty = AvaloniaProperty.Register<ProgressRing, bool>(nameof(IsActive));

    ////public static readonly AvaloniaProperty IsActiveProperty = AvaloniaProperty.RegisterAttached<ProgressRing, bool>(nameof(IsActive), typeof(bool));

    ////public static readonly DirectProperty<ProgressRing, bool> IsActiveProperty = AvaloniaProperty.RegisterDirect<ProgressRing, bool>(
    ////    nameof(IsActive),
    ////    o => o.IsActive,
    ////    (o, v) => o.IsActive = v);

    ////public static readonly AvaloniaProperty IsActiveProperty = AvaloniaProperty.Register<ProgressRing, bool>(nameof(IsActive), defaultValue: true, notifying: OnIsActiveChanged);
    ////private static void OnIsActiveChanged(IAvaloniaObject obj, bool arg2)
    ////{
    ////  ((ProgressRing)obj).Refresh();
    ////}

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

      //// IsActiveProperty.Changed.Subscribe(args => OnIsActiveChanged(args?.Sender, args));
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
      get => (Thickness)GetValue(EllipseOffsetProperty);
      private set => SetValue(EllipseOffsetProperty, value);
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
      EllipseDiameter = (width / 8) * EllipseDiameterScale;
    }

    private void SetEllipseOffset(double width)
    {
      EllipseOffset = new Thickness(0, width / 2, 0, 0);
    }

    private void SetMaxSideLength(double width)
    {
      MaxSideLength = width <= 20 ? 20 : width;
    }

    private void OnIsActiveChanged(IAvaloniaObject? sender, AvaloniaPropertyChangedEventArgs? args)
    ////private void OnIsActiveChanged(IAvaloniaObject? sender, bool? args)
    {
      ProgressRing? progRing = sender as ProgressRing;
      if (progRing != null)
        progRing.Refresh();
    }

    private void Refresh()
    {
    }
  }
}
