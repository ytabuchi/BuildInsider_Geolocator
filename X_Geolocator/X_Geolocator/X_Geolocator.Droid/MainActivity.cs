using System;

using Android.App;
using Android.Content;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Android.OS;
using Android.Locations;
using Android.Util;

namespace X_Geolocator.Droid
{
	[Activity (Label = "X_Geolocator.Droid", MainLauncher = true, Icon = "@drawable/icon")]
	public class MainActivity : Activity
	{
        LocationManager locMgr;
        string tag = "MainActivity";
        Button button;
        TextView latText;
        TextView lanText;

		protected override void OnCreate (Bundle bundle)
		{
			base.OnCreate (bundle);

			SetContentView (Resource.Layout.Main);

			button = FindViewById<Button> (Resource.Id.myButton);
            latText = FindViewById<TextView>(Resource.Id.LatText);
            lanText = FindViewById<TextView>(Resource.Id.LanText);

		}

        protected override void OnResume()
        {
            base.OnResume();

            // LocationManagerを初期化
            locMgr = GetSystemService(Context.LocationService) as LocationManager;

            button.Click += (sender, e) =>
            {
                if (locMgr.AllProviders.Contains(LocationManager.NetworkProvider)
                        && locMgr.IsProviderEnabled(LocationManager.NetworkProvider))
                {
                    // Network: Wifi と 3G で位置情報を取得します。電池使用量は少ないですが精度にばらつきがあります。
                    Log.Debug(tag, "Starting location updates with Network");
                    locMgr.RequestLocationUpdates(LocationManager.NetworkProvider, 10000, 10, this);
                }
                else
                {
                    // GetBestProvider で最適な Provider を利用できるようです。(Network があればそれが Best になるようです。)
                    var locationCriteria = new Criteria();
                    locationCriteria.Accuracy = Accuracy.Coarse;
                    locationCriteria.PowerRequirement = Power.Medium;
                    string locationProvider = locMgr.GetBestProvider(locationCriteria, true);

                    Log.Debug(tag, "Starting location updates with " + locationProvider.ToString());
                    locMgr.RequestLocationUpdates(locationProvider, 10000, 10, this);
                }
            };
        }
    }
}


