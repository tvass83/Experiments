using Avalonia;
using Avalonia.Controls.Primitives;

namespace AvaloniaApplication20_SyncPointControl
{
    public class SyncPointControl : TemplatedControl
    {
        public bool IsReference
        {
            get { return GetValue(IsReferenceProperty); }
            set { SetValue(IsReferenceProperty, value); }
        }

        public static readonly StyledProperty<bool> IsReferenceProperty =
            AvaloniaProperty.Register<SyncPointControl, bool>(nameof(IsReference));

        public bool IsSelected
        {
            get { return GetValue(IsSelectedProperty); }
            set { SetValue(IsSelectedProperty, value); }
        }

        public static readonly StyledProperty<bool> IsSelectedProperty =
            AvaloniaProperty.Register<SyncPointControl, bool>(nameof(IsSelected));

        public bool IsHighlighted
        {
            get { return GetValue(IsHighlightedProperty); }
            set { SetValue(IsHighlightedProperty, value); }
        }

        public static readonly StyledProperty<bool> IsHighlightedProperty =
            AvaloniaProperty.Register<SyncPointControl, bool>(nameof(IsHighlighted));
    }
}
