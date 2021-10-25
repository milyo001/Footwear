namespace Footwear.Data
{
    //The model is used to create Application settings in appsettings.json,injectable with the dependacy injection
    public class ApplicationSettings
    {
        public string JWT_Secret { get; set; }

        public string ApiUrl { get; set; }

    }
}
