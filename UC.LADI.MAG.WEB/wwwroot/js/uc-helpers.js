var UcHelpers = function () {
    function GetFrequencyTextByCode(code) {
        var rs = '';
        if (code[0] == 'D') {
            rs = 'Ngày';
        }
        else if (code[0] == 'W') {
            rs = 'Tuần';
        }
        else if (code[0] == 'M' || code[0] == 'N') {
            rs = 'Tháng';
        }
        else if (code[0] == 'Y') {
            rs = 'Năm';
        }
        return rs;
    }
    function GetLangueResource(url) {
        $.ajax({
            type: "GET",
            contentType: "application/json",
            dataType: "json",
            url: url,
            headers: { "Authorization": "Bearer " + new UcHelpers().GetAccessToken() },
            success: function (res) {
                localStorage.setItem('UcLang', JSON.stringify(res));
            },
            error: function () {

            }
        });
    }
    function GetUserInfo() {
        return localStorage.getItem('UcUserInfo');
    }
    function GetAccessToken() {
        let userInfo = JSON.parse(localStorage.getItem('UcUserInfo'));
        if (userInfo) {
            return userInfo.accessToken;
        }
        return '';
    }
    function GetUserId() {
        let userInfo = JSON.parse(localStorage.getItem('UcUserInfo'));
        if (userInfo) {
            return userInfo.userId;
        }
        return '';
    }
    function GetUserName() {
        let userInfo = JSON.parse(localStorage.getItem('UcUserInfo'));
        if (userInfo) {
            return userInfo.userName;
        }
        return '';
    }
    function SetUserInfo(data) {
        localStorage.setItem('UcUserInfo', JSON.stringify(data));
    }
    function SetLang(data) {
        localStorage.setItem('UcLang', data);
    }
    function TruncateString(str, num) {
        if (str) {
            if (str.length > num) {
                return str.slice(0, num) + "...";
            } else {
                return str;
            }
        }
        else {
            return str;
        }
    }
    function NumberToMoney(num) {
        return parseInt(num).toLocaleString('vi', { style: 'currency', currency: 'VND' });
    }
    function NumberToRoman(num) {
        const romanNumerals = [
            { value: 1000, numeral: 'M' },
            { value: 900, numeral: 'CM' },
            { value: 500, numeral: 'D' },
            { value: 400, numeral: 'CD' },
            { value: 100, numeral: 'C' },
            { value: 90, numeral: 'XC' },
            { value: 50, numeral: 'L' },
            { value: 40, numeral: 'XL' },
            { value: 10, numeral: 'X' },
            { value: 9, numeral: 'IX' },
            { value: 5, numeral: 'V' },
            { value: 4, numeral: 'IV' },
            { value: 1, numeral: 'I' }
        ];

        let result = '';
        for (let i = 0; i < romanNumerals.length; i++) {
            while (num >= romanNumerals[i].value) {
                result += romanNumerals[i].numeral;
                num -= romanNumerals[i].value;
            }
        }
        return result;
    }
    function Label(code) {
        if (!localStorage.getItem('UcLang')) {
            return code;
        }

        let label = JSON.parse(localStorage.getItem('UcLang'))['LAppController'][code];
        if (!label) {
            return code;
        }
        return label;
    }
    return {
        GetFrequencyTextByCode,
        GetLangueResource,
        GetUserInfo,
        SetUserInfo,
        SetLang,
        GetUserId,
        GetUserName,
        GetAccessToken,
        TruncateString,
        NumberToMoney,
        NumberToRoman,
        Label
    };
}