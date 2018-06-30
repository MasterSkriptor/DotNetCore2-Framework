namespace EXCSLA.Services.MerchantServices.Paypal
{
    public interface IPayPalService
    {
        string GetPurchaseUrl(int amount, int orderNumber, bool? useIPN = null);
        string GetPurchaseUrl(int amount, bool? useIPN);
    }
}