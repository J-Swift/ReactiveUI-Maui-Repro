using System.Reactive;
using System.Reactive.Linq;

using ReactiveUI;

namespace ReactiveTest;

public partial class MainPageViewModel : ReactiveObject
{
	// NOTE(jpr): fody not working
	// [Reactive] public int Count { get; private set; }
	private int count;
    public int Count 
    {
        get => count;
        set => this.RaiseAndSetIfChanged(ref count, value);
    }

    public ReactiveCommand<Unit, int> Click { get; }

	public MainPageViewModel()
	{
        Click = ReactiveCommand.CreateFromObservable(doClick);
    }

    IObservable<int> doClick() => Observable.Return(++Count);
}
