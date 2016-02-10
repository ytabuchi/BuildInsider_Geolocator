using Plugin.Geolocator;
using Plugin.Geolocator.Abstractions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Emit;
using System.Text;


using Xamarin.Forms;

namespace XF_Geolocator
{
    public class GetGeoCS : ContentPage
    {
        Button button;
        Label latLabel;
        Label lonLabel;
        Position position;

        public GetGeoCS()
        {
            button = new Button
            {
                Text = "Get Geolocator"
            };
            button.Clicked += async (sender, e) =>
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
                    await DisplayAlert("Error", "This device does not have any geo sensors", "OK");


            };

            latLabel = new Label
            {
                Text = "Latitude: "
            };
            lonLabel = new Label
            {
                Text = "Longitude: "
            };

            Content = new StackLayout
            {
                Padding = new Thickness(10, Device.OnPlatform(20, 0, 0), 10, 10),
                Children = {
                    button,
                    latLabel,
                    lonLabel,
                },
            };
        }
    }
}
