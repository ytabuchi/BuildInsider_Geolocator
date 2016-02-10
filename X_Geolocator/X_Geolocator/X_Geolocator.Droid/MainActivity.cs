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
	public class MainActivity : Activity, ILocationListener
    {
        LocationManager locMgr;
        string tag = "MainActivity";
        Button button;
        TextView latText;
        TextView lonText;

		protected override void OnCreate (Bundle bundle)
		{
			base.OnCreate (bundle);

			SetContentView (Resource.Layout.Main);

			button = FindViewById<Button> (Resource.Id.Button);
            latText = FindViewById<TextView>(Resource.Id.LatText);
            lonText = FindViewById<TextView>(Resource.Id.LonText);

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
                    // Network: Wifiと3Gで位置情報を取得します。電池使用量は少ないですが精度にばらつきがあります。
                    Log.Debug(tag, "Starting location updates with Network");
                    locMgr.RequestLocationUpdates(LocationManager.NetworkProvider, 10000, 10, this);
                }
                else
                {
                    // GetBestProviderで最適なProviderを利用できるようです。(NetworkがあればそれがBestProviderになるようです。)
                    var locationCriteria = new Criteria();
                    locationCriteria.Accuracy = Accuracy.Coarse;
                    locationCriteria.PowerRequirement = Power.Medium;
                    string locationProvider = locMgr.GetBestProvider(locationCriteria, true);

                    Log.Debug(tag, "Starting location updates with " + locationProvider.ToString());
                    locMgr.RequestLocationUpdates(locationProvider, 10000, 10, this);
                }
            };
        }

        public void OnLocationChanged(Android.Locations.Location location)
        {
            Log.Debug(tag, "Location changed");
            latText.Text = "Latitude: " + location.Latitude.ToString();
            lonText.Text = "Longitude: " + location.Longitude.ToString();
        }

        public void OnProviderDisabled(string provider)
        {
            // Android 側で手動で位置情報モードを変更すると発火します。
            Log.Debug(tag, provider + " provider disabled");
        }

        public void OnProviderEnabled(string provider)
        {
            Log.Debug(tag, provider + " enabled by user");
        }

        public void OnStatusChanged(string provider, [GeneratedEnum] Availability status, Bundle extras)
        {
            Log.Debug(tag, provider + " availability has changed to " + status.ToString());
        }

        protected override void OnPause()
        {
            base.OnPause();
            Log.Debug(tag, "OnPause, stop location manager update");
            locMgr.RemoveUpdates(this);
        }
    }
}


