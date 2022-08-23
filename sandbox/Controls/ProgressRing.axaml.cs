using System.Reactive.Disposables;
using System.Reactive.Linq;
using Avalonia;
using Avalonia.Controls;

namespace SuessLabs.Avalonia.Controls
{
    /// <summary><see cref="ProgressRing"/> provides a Windows 10 styled circular loading animation.</summary>
    /// <remarks>
    ///   Adapted from the MahApps Metro implementation at
    ///   https://github.com/MahApps/MahApps.Metro/blob/develop/src/MahApps.Metro/Controls/ProgressRing.cs.
    /// </remarks>
    public class ProgressRing : ContentControl
    {
        public static readonly AvaloniaProperty EllipseDiameterProperty =
            AvaloniaProperty.Register<ProgressRing, double>("EllipseDiameter", inherits: true);

        public static readonly AvaloniaProperty EllipseDiameterScaleProperty =
            AvaloniaProperty.Register<ProgressRing, double>("EllipseDiameterScale", inherits: true, defaultValue: 1D);

        public static readonly AvaloniaProperty EllipseOffsetProperty =
            AvaloniaProperty.Register<ProgressRing, Thickness>("EllipseOffset", inherits: true);

        public static readonly AvaloniaProperty IsActiveProperty =
            AvaloniaProperty.Register<ProgressRing, bool>("IsActive", inherits: true, defaultValue: true);

        public static readonly AvaloniaProperty MaxSideLengthProperty =
            AvaloniaProperty.Register<ProgressRing, double>("MaxSideLength", inherits: true);

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
            get => (Thickness)GetValue(EllipseOffsetProperty);
            private set => SetValue(EllipseOffsetProperty, value);
        }

        public bool IsActive
        {
            get => (bool)GetValue(IsActiveProperty);
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
    }
}
