

namespace Footwear_Tests.Mocks
{
    public static class MockData
    {
        // A test Stripe API Key used for payments
        private const string _testStripeApiKey = "sk_test_51JvSm1EzlmwAD2nGZhL8vfUSiDteGqAYl0iKaPDbix9v9rZcfzjcOm9Kh0GgUMsXSNurIyW6T6br9dbuAlWkO74e008QjJw2YC";
        private const string _clientUrl = "https://footwear.com";

        public static string TestClientUrl { get => _clientUrl; }
        public static string TestStripeApiKey { get => _testStripeApiKey; }
    }
}
