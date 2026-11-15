using System.Windows;
using System.Windows.Automation.Peers;
using System.Windows.Controls;

namespace ClinicManager.Win.Presentation.Controls;

/// <summary>Sample custom control: a rounded card with a header. Template lives in Themes/Generic.xaml.</summary>
public class CardPanel : ContentControl
{
    static CardPanel()
    {
        DefaultStyleKeyProperty.OverrideMetadata(typeof(CardPanel), new FrameworkPropertyMetadata(typeof(CardPanel)));
    }

    public static readonly DependencyProperty HeaderProperty =
        DependencyProperty.Register(nameof(Header), typeof(string), typeof(CardPanel), new PropertyMetadata(string.Empty));

    public static readonly DependencyProperty CornerRadiusProperty =
        DependencyProperty.Register(nameof(CornerRadius), typeof(CornerRadius), typeof(CardPanel), new PropertyMetadata(new CornerRadius(8)));

    public string Header
    {
        get => (string)GetValue(HeaderProperty);
        set => SetValue(HeaderProperty, value);
    }

    public CornerRadius CornerRadius
    {
        get => (CornerRadius)GetValue(CornerRadiusProperty);
        set => SetValue(CornerRadiusProperty, value);
    }

    // Expose the control to UI Automation so AutomationId is visible to test tools.
    protected override AutomationPeer OnCreateAutomationPeer() => new FrameworkElementAutomationPeer(this);
}
