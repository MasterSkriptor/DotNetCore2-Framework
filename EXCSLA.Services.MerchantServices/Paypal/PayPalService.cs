namespace EXCSLA.Services.MerchantServices.Paypal
{
    public class PayPalService : IPayPalService
    {
        private string _merchantEmail;
        private string _returnUrl;
        private string _cancelUrl;
        private string _currencyCode;
        private int _discount;
        private double _tax;
        private string _paypalUrl;
        

        public PayPalService(PayPalOptions options)
        {
            _merchantEmail = options.MerchantEmail;
            _returnUrl = options.ReturnUrl;
            _cancelUrl = options.CancelUrl;
            _currencyCode = options.CurrencyCode;
            _discount = options.Discount;
            _tax = options.Tax;
            _paypalUrl = options.PayPalUrl;
        }

        public string GetPurchaseUrl(int amount, int orderNumber, bool? useIPN = null)
        {
            return $"{_paypalUrl}?cmd=_xclick&business={_merchantEmail}&return={_returnUrl}&cancel_return={_cancelUrl}&currency_code={_currencyCode}&amount={amount}&item_name={orderNumber}&discount_amount={_discount}&tax={_tax}";
        }

        public string GetPurchaseUrl(int amount, bool? useIPN)
        {
            return $"{_paypalUrl}?cmd=_xclick&business={_merchantEmail}&return={_returnUrl}&cancel_return={_cancelUrl}&currency_code={_currencyCode}&amount={amount}&item_name=0&discount_amount={_discount}&tax={_tax}";
        }
    }
}