var card = new Card({
    form: 'form',
    container: '.card-wrapper',

    formSelectors: {
        numberInput: 'input#CreditcardNumber',
        expiryInput: 'input#expdate',
        cvcInput: 'input#CreditVerificationValue',
        nameInput: 'input#CardHolder'
    },

    masks:{
        cardNumber: 'â€¢'
    },

    messages: {
        validDate: PageLanguage === 'ING' ? 'EXPIRES' : 'EXPIRA',
        monthYear: 'mm/aa'
    },

    placeholders: {
        number: '&bull;&bull;&bull;&bull; &bull;&bull;&bull;&bull; &bull;&bull;&bull;&bull; &bull;&bull;&bull;&bull;',
        cvc: '&bull;&bull;&bull;',
        expiry: '&bull;&bull;/&bull;&bull;',
        name: PageLanguage === 'ING' ? 'Full Name' : 'Nombre Completo'
    }
});
$(document).ready(function ()
{
    $('#form').on('submit',function ()
    {
        var cardValid = $("#CreditcardNumber").is(".jp-card-valid");
        var cardIdentified = $("#CreditcardNumber").is(".identified");
        var expValid = $("#expdate").is(".jp-card-valid");
        var cvcValid = $("#CreditVerificationValue").is(".jp-card-valid");
        var nameValid = true;

        if (!classic) nameValid = $("#CardHolder").is(".jp-card-valid");

        var isValid = cardValid && cardIdentified && expValid && cvcValid && nameValid;

        if (!isValid) {
            $.notify("Datos invalidos");
        }
        else {
            $('.submit').prop('disabled', true);
        }

        return isValid;
    });

    $('#CreditcardNumber').on('change keyup paste', function(){
        var m = 19;
        var ctrl = $('#CreditcardNumber');

        if (ctrl.val().length > m) {
            ctrl.val(ctrl.val().substr(0, m));
        }
    });

    var defaultFormat = /(\d{1,4})/g;

    var cards = [
        {
          type: 'amex',
          pattern: /^3[47]/,
          format: /(\d{1,4})(\d{1,6})?(\d{1,5})?/,
          length: [15],
          cvcLength: [4],
          luhn: true
        }, {
          type: 'dinersclub',
          pattern: /^(36|38|30[0-5])/,
          format: /(\d{1,4})(\d{1,6})?(\d{1,4})?/,
          length: [14],
          cvcLength: [3],
          luhn: true
        }, {
          type: 'discover',
          pattern: /^(6011|65|64[4-9]|622)/,
          format: defaultFormat,
          length: [16],
          cvcLength: [3],
          luhn: true
        }, {
          type: 'jcb',
          pattern: /^35/,
          format: defaultFormat,
          length: [16],
          cvcLength: [3],
          luhn: true
        }, {
          type: 'maestro',
          pattern: /^(5018|5020|5038|6304|6703|6708|6759|676[1-3])/,
          format: defaultFormat,
          length: [12, 13, 14, 15, 16, 17, 18, 19],
          cvcLength: [3],
          luhn: true
        }, {
          type: 'mastercard',
          pattern: /^(5[1-5]|677189)|^(222[1-9]|2[3-6]\d{2}|27[0-1]\d|2720)/,
          format: defaultFormat,
          length: [16],
          cvcLength: [3],
          luhn: true
        }, {
          type: 'unionpay',
          pattern: /^62/,
          format: defaultFormat,
          length: [16, 17, 18, 19],
          cvcLength: [3],
          luhn: false
        }, {
          type: 'visa',
          pattern: /^4/,
          format: defaultFormat,
          length: [13, 16, 19],
          cvcLength: [3],
          luhn: true
        }
    ];

    Payment.setCardArray(cards);

});

// Controller area for display values in enrich.html
$(function () {
    switch (document.getElementById('txtCurrencyCode').innerHTML) {
        case '214':
            document.getElementById('currencyDescription').setAttribute('data-trn-key', 'pesoCurrencyDescription');
            document.getElementById('currencyAbbreviation').innerHTML = 'DOP$ ';
            document.getElementById('currencyAbbreviation2').innerHTML = 'DOP$ ';
            break;
        case '840':
            document.getElementById('currencyDescription').setAttribute('data-trn-key', 'dollarCurrencyDescription');
            document.getElementById('currencyAbbreviation').innerHTML = 'USD$ ';
            document.getElementById('currencyAbbreviation2').innerHTML = 'USD$ ';
            break;
    }

    if (document.getElementById('pageLanguage').innerHTML === 'ING') {
        document.getElementById('submit').setAttribute('value', 'Process');
    } else {
        document.getElementById('submit').setAttribute('value', 'Procesar');
    }
});

// Check Browser Version
function getBrowserInfo() {
    var ua = navigator.userAgent, tem, M = ua.match(/(opera|chrome|safari|firefox|msie|trident(?=\/))\/?\s*(\d+)/i) || [];

    if (/trident/i.test(M[1])) {
        tem = /\brv[ :]+(\d+)/g.exec(ua) || [];
        return {name: 'IE', version: (tem[1] || '')};
    }

    if (M[1] === 'Chrome') {
        tem = ua.match(/\bOPR|Edge\/(\d+)/);
        if (tem != null) {
            return {name: 'Opera', version: tem[1]};
        }
    }

    M = M[2] ? [M[1], M[2]] : [navigator.appName, navigator.appVersion, '-?'];
    if ((tem = ua.match(/version\/(\d+)/i)) != null) {
        M.splice(1, 1, tem[1]);
    }
    return {
        name: M[0],
        version: M[1]
    };
}

function verifyBrowserVersion() {
    var minimumVersionSupported = [];
    minimumVersionSupported['IE'] = 9;
    minimumVersionSupported['MSIE'] = 9;
    minimumVersionSupported['Chrome'] = 70;
    minimumVersionSupported['Firefox'] = 62;
    minimumVersionSupported['Opera'] = 13;
    minimumVersionSupported['Safari'] = 6;

    if (getBrowserInfo().version < minimumVersionSupported[getBrowserInfo().name]) {
        window.location.href = "./unsupported.html";
    }
}