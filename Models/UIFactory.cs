using toolcad23.Views;

namespace toolcad23.Models
{
    internal class UIFactory
    {
        private static InfoPageView infoPageView;
        internal static InfoPageView GetInfoPageView()
        {
            if (infoPageView == null)
            {
                infoPageView = new InfoPageView();
            }
            return infoPageView;
        }

        private static RetrievePageView retrievePageView;
        internal static RetrievePageView GetRetrievePageView()
        {
            if (retrievePageView == null)
            {
                retrievePageView = new RetrievePageView();
            }
            return retrievePageView;
        }

        private static DeliveryPageView deliveryPageView;
        internal static DeliveryPageView GetDeliveryPageView()
        {
            if (deliveryPageView == null)
            {
                deliveryPageView = new DeliveryPageView();
            }
            return deliveryPageView;
        }
    }
}
