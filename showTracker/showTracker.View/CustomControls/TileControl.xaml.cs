using System;
using System.Windows.Input;
using CommonServiceLocator;
using showTracker.BusinessLayer.Interfaces;
using showTracker.Model.Enum;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace showTracker.ViewModel.CustomControls
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class TileControl : Grid
	{
	    public LayoutOptions TileOption => LayoutOptions.FillAndExpand;
		public TileControl ()
		{
		    _stLogger = ServiceLocator.Current.GetInstance<ISTLogger>();
            _tileIconStrategyResolver = ServiceLocator.Current.GetInstance<ITileIconStrategyResolver>();
		    InitializeComponent ();
		}

	    public static readonly BindableProperty TileClickedProperty =
	        BindableProperty.Create(nameof(TileClicked), typeof(ICommand), typeof(TileControl));

	    public ICommand TileClicked
	    {
	        get => (ICommand) GetValue(TileClickedProperty);
	        set => SetValue(TileClickedProperty, value);
	    }

	    public static readonly BindableProperty TileIconEnumProperty =
	        BindableProperty.Create(nameof(TileIconEnum), typeof(TileIconEnum), typeof(TileControl), TileIconEnum.Unknown,
	            propertyChanged: (bindable, value, newValue) =>
	            {
                    ((TileControl)bindable).ChangeTile(newValue as TileIconEnum? ?? TileIconEnum.Unknown);
	            });

	    public TileIconEnum TileIconEnum
	    {
	        get => (TileIconEnum) GetValue(TileIconEnumProperty);
	        set => SetValue(TileIconEnumProperty, value);
	    }

	    public string TileIconResourceId { get; private set; } = "";
	    public string TileLabel { get; private set; } = "";
         
	    public virtual void OnTileClick(object sender, EventArgs e)
	    {
	        TileClicked?.Execute(null);
	    }

	    public void ChangeTile(TileIconEnum tileIconEnum)
	    {
	        var currentStrategy = _tileIconStrategyResolver.Resolve(tileIconEnum);

	        TileIconResourceId = currentStrategy?.ResourceId;
	        TileLabel = currentStrategy?.Label;

	        _stLogger.Log($"Tile: {currentStrategy?.Label}/{currentStrategy?.ResourceId}");
        }

	    private readonly ITileIconStrategyResolver _tileIconStrategyResolver;
	    private readonly ISTLogger _stLogger;
	}
}