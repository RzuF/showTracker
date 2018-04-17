using System;
using Xamarin.Forms;

namespace showTracker.ViewModel.CustomControls
{
    public class ColoredListView : ListView
    {
        public ColoredListView() : base(ListViewCachingStrategy.RecycleElementAndDataTemplate)
        {
            ItemTapped += OnItemTapped;
        }

        public ColoredListView(ListViewCachingStrategy listViewCachingStrategy) : base(listViewCachingStrategy)
        {
            ItemTapped += OnItemTapped;
        }

        public static readonly BindableProperty EvenColorProperty =
            BindableProperty.Create(nameof(EvenColor), typeof(Color), typeof(ColoredListView), Color.Transparent);

        public Color EvenColor
        {
            get => (Color) GetValue(EvenColorProperty);
            set => SetValue(EvenColorProperty, value);
        }

        public static readonly BindableProperty OddColorProperty =
            BindableProperty.Create(nameof(OddColor), typeof(Color), typeof(ColoredListView), Color.Transparent);

        public Color OddColor
        {
            get => (Color) GetValue(OddColorProperty);
            set => SetValue(OddColorProperty, value);
        }

        protected override void SetupContent(Cell content, int index)
        {
            base.SetupContent(content, index);

            if (content is EpisodeContainerItem episodeContainerItem)
            {
                episodeContainerItem.View.BackgroundColor = index % 2 == 0 ? EvenColor : OddColor;
            }
        }
        private void OnItemTapped(object sender, ItemTappedEventArgs itemTappedEventArgs)
        {
            if (itemTappedEventArgs.Item == null)
            {
                return;
            }
            ((ListView)sender).SelectedItem = null;
        }
    }
}
