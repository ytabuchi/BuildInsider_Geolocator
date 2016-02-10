using CoreLocation;
using System;

using UIKit;

namespace X_Geolocator.iOS
{
    public partial class ViewController : UIViewController
    {
        CLLocationManager locMgr = null;

        public ViewController(IntPtr handle) : base(handle)
        {
        }

        public override void ViewDidLoad()
        {
            base.ViewDidLoad();

            locMgr = new CLLocationManager();
            locMgr.RequestWhenInUseAuthorization();

            button.TouchUpInside += (s, _) =>
            {
                locMgr.DesiredAccuracy = 1000;
                locMgr.LocationsUpdated += (object sender, CLLocationsUpdatedEventArgs e) =>
                {
                    var location = e.Locations[e.Locations.Length - 1];
                    LatText.Text = "Latitude: " + location.Coordinate.Latitude.ToString();
                    LonText.Text = "Longitude: " + location.Coordinate.Longitude.ToString();
                };
                locMgr.StartUpdatingLocation();
            };
        }


        public override void DidReceiveMemoryWarning()
        {
            base.DidReceiveMemoryWarning();
            // Release any cached data, images, etc that aren't in use.
        }
    }
}

