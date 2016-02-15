using Plugin.Geolocator;
using Plugin.Geolocator.Abstractions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;

namespace BI_Geolocator
{
    public partial class GetGeoXaml : ContentPage
    {
        Position position;

        public GetGeoXaml()
        {
            InitializeComponent();
        }

        private async void buttonClicked(object sender, EventArgs e)
        {
            IGeolocator locator = CrossGeolocator.Current;
            locator.DesiredAccuracy = 50;

            if (locator.IsGeolocationAvailable)
            {
                if (locator.IsGeolocationEnabled)
                {
                    try
                    {
                        position = await locator.GetPositionAsync(timeoutMilliseconds: 10000);
                        latLabel.Text = "Latitude: " + position.Latitude;
                        lonLabel.Text = "Longitude: " + position.Longitude;

                        System.Diagnostics.Debug.WriteLine("TimeStamp: " + position.Timestamp);
                    }
                    catch (GeolocationException ex)
                    {
                        System.Diagnostics.Debug.WriteLine(ex.InnerException);
                        await DisplayAlert("Error", "Unauthorized", "OK");
                    }
                    catch (Exception ex)
                    {
                        System.Diagnostics.Debug.WriteLine(ex.InnerException);
                    }
                }
                else
                    await DisplayAlert("Error", "Geo sensors is not turned on.\nPlease turn on it.", "OK");

            }
            else
                await DisplayAlert("Error", "This device does not have any geo sensors or enough permission to use it.", "OK");
        }
    }
}
