using System.Reactive.Disposables;
using ReactiveUI;
using ReactiveUI.Maui;

namespace ReactiveTest;

public partial class MainPage : ReactiveContentPage<MainPageViewModel>
{
    private Label LblCount { get; }
    private Button BtnClick { get; }

    public MainPage()
    {
        ViewModel = new();

        LblCount = new();
        BtnClick = new()
        {
            Text = "Click me",
        };

        Content = new StackLayout
        {
            Spacing = 16,
            Margin = new Thickness(16),
            Children = {
                LblCount,
                BtnClick,
            },
        };

        this.WhenActivated(disposables =>
        {
            this.OneWayBind(ViewModel, vm => vm.Count, v => v.LblCount.Text, txt => $"Current count: {txt}")
                .DisposeWith(disposables);

            this.BindCommand(ViewModel, vm => vm.Click, v => v.BtnClick)
                .DisposeWith(disposables);
        });
    }
}

