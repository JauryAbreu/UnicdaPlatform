$(function()
{
   var dictionary = {
       "IN_ORDER_TO_PAY":{
           ING: "In Order to Pay to:",
           ESP: "Para Pagar a:"
       },
       "CURRENCY_TYPE":{
           ING: "Currency Type:",
           ESP: "Tipo de Moneda:"
       },
       "TOTAL_AMOUNT":{
           ING: "Total Amount:",
           ESP:"Monto Total:"
       },
       "REFERENCE":{
           ING: "Reference:",
           ESP: "Referencia:"
       },
       "CARDHOLDER_NAME":{
           ING: "Cardholder's Name:",
           ESP: "Nombre del Tarjeta habiente:"
       },
       "CREDIT_CARD_NUMBER":{
           ING: "Card's Number:",
           ESP: "NÃºmero de Tarjeta:"
       },
       "EXPIRATION_DATE":{
           ING: "Expiration Date:",
           ESP: "Fecha de ExpiraciÃ³n:"
       },
       "CREDIT_CARD_VERIFICATION_VALUE":{
           ING: "Card Verification Value (CVV):",
           ESP: "Valor de VerificaciÃ³n (CVV):"
       },
       "dollarCurrencyDescription":{
           ING: "United States Dollars",
           ESP: "DÃ³lares Estadounidenses"
       },
       "pesoCurrencyDescription":{
           ING: "Domincan Pesos",
           ESP: "Pesos Dominicanos"
       }
   };

   if(PageLanguage === 'ING')
   {
       $('body').translate({lang: PageLanguage, t: dictionary});
   }else
       {
           $('body').translate({lang: 'ESP', t: dictionary})
       }
});