using System;
using System.Collections.Generic;
using Xamarin.Forms;

namespace Olis
{
    public partial class Header : StackLayout
    {
        //public static readonly BindableProperty RButtonClickedProperty =
        //    BindableProperty.Create("RButtonClicked", typeof(TapGestureRecognizer),
        //        typeof(Header), default(TapGestureRecognizer));
        //public TapGestureRecognizer RButtonClicked
        //{
        //    get
        //    {
        //        return (TapGestureRecognizer)GetValue(RButtonClickedProperty);
        //    }

        //    set
        //    {
        //        SetValue(RButtonClickedProperty, value);
        //    }
        //}

        //public static readonly BindableProperty LButtonClickedProperty =
        //    BindableProperty.Create("LButtonClicked", typeof(TapGestureRecognizer),
        //        typeof(Header), default(TapGestureRecognizer));
        //public TapGestureRecognizer LButtonClicked
        //{
        //    get
        //    {
        //        return (TapGestureRecognizer)GetValue(LButtonClickedProperty);
        //    }

        //    set
        //    {
        //        SetValue(LButtonClickedProperty, value);
        //    }
        //}
        public Header()
        {
            InitializeComponent();
        }

        public void SetRButtonClicked(EventHandler handler)
        {
            var tapGestureRecognizer = new TapGestureRecognizer();
            tapGestureRecognizer.Tapped += handler;
            tapGestureRecognizer.NumberOfTapsRequired = 1;
            RButton.GestureRecognizers.Add(tapGestureRecognizer);
        }

        public void SetLButtonClicked(EventHandler handler)
        {
            var tapGestureRecognizer = new TapGestureRecognizer();
            tapGestureRecognizer.Tapped += handler;
            tapGestureRecognizer.NumberOfTapsRequired = 1;
            LButton.GestureRecognizers.Add(tapGestureRecognizer);
        }

        async void GoBack(System.Object sender, System.EventArgs e)
        {
            await Navigation.PopModalAsync();
        }
    }
}

