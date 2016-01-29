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
        Plugin.Geolocator.Abstractions.Position position;

        public GetGeoCS()
        {
            button = new Button
            {
                Text = "Get Geolocator"
            };
            button.Clicked += async (sender, e) =>
            {
                var locator = CrossGeolocator.Current;
                locator.DesiredAccuracy = 50;

                System.Diagnostics.Debug.WriteLine("Available: " + locator.IsGeolocationAvailable);
                System.Diagnostics.Debug.WriteLine("Enable: " + locator.IsGeolocationEnabled);


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
                Text = "Langitude: "
            };

            Content = new StackLayout
            {
                Padding = 10,
                VerticalOptions = LayoutOptions.Center,
                Children = {
                    button,
                    latLabel,
                    lonLabel,
                },
            };
        }
    }
}
