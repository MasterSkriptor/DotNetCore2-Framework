namespace EXCSLA.Services.MerchantServices.Paypal
{
    public class PayPalOptions
    {
        private string _merchantEmail;
        private string _returnUrl;
        private string _cancelUrl;
        private string _currencyCode;
        private int _discount;
        private double _tax;
        private string _paypalUrl;
        

        public string MerchantEmail {get{return _merchantEmail;} set{_merchantEmail = value;}}
        public string ReturnUrl {get{return _returnUrl;} set{_returnUrl = value;}}
        public string CancelUrl {get{return _cancelUrl;} set{_cancelUrl = value;}}
        public string CurrencyCode {get{return _currencyCode;} set{_currencyCode = value;}}
        public int Discount {get{return _discount;} set{_discount = value;}}
        public double Tax {get{return _tax;} set {_tax = value;}}
        public string PayPalUrl {get{return _paypalUrl;} set{_paypalUrl = value;}}
    }
}