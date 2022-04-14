namespace Footwear.Settings
{
    /// <summary>
    /// Used to configure the email settings before sending the actual email for order
    /// </summary>
    public class MailSettings
    {
        public string Mail { get; set; }
        public string DisplayName { get; set; }
        public string Password { get; set; }
        public string Host { get; set; }
        public int Port { get; set; }
    }
}
