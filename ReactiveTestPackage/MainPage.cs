namespace ReactiveTest;

public partial class MainPage : ContentPage
{
    private int count;

    private Label LblCount { get; }
    private Button BtnClick { get; }

    public MainPage()
    {
        LblCount = new()
        {
            Text = $"Current count: {count}",
        };
        BtnClick = new()
        {
            Text = "Click me",
        };
        BtnClick.Clicked += OnClick;

        Content = new StackLayout
        {
            Spacing = 16,
            Margin = new Thickness(16),
            Children = {
                LblCount,
                BtnClick,
            },
        };
    }

    private void OnClick(object sender, EventArgs e)
    {
        LblCount.Text = $"Current count: {++count}";
    }
}

