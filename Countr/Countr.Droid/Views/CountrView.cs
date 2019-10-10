using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;

using MvvmCross.Droid.Support.V7.AppCompat;
using Android.Support.V7.Widget;
using Countr.Core.ViewModels;


namespace Countr.Droid.Views
{
    [Activity(Label = "Add a new counter")]
    public class CountrView : MvxAppCompatActivity<CountrViewModel>
    {
        protected override void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);
            SetContentView(Resource.Layout.countr_view);

            var toolbar = FindViewById<Toolbar>(Resource.Id.toolbar);
            SetSupportActionBar(toolbar);
        }
    }
}