var authentication_method = "admin";
var sync_type = "order"; //order, receipt
var enable_identification_card = false;

var file_config = {
    reader_avatar: {
        ref_type: "blib_reader_avatar",
        folder_code: "blib_reader_avatar",
    },
    dl_bib_avatar: {
        ref_type: "blib_dl_bib_avatar",
        folder_code: "blib_dl_bib_avatar",
    },
    blib_file_digital: {
        ref_type: "blib_file_digital",
        folder_code: "blib_file_digital",
    },
    ck_finder: {
        ref_type: "ck_finder",
        folder_code: "ck_finder",
    },
};

var perm_module_config = {
    role_menu: {
        class_code: "Menu",
        sid_code: "role",
    },
    group_menu: {
        class_code: "Menu",
        sid_code: "group",
    }
};

var perm_data_config = {
    role_cir_place: {
        class_code: "CirPlace",
        sid_code: "role",
    },
    group_cir_place: {
        class_code: "CirPlace",
        sid_code: "group",
    }
};

//var root_url_web = "http://ucvn.vn/blib/";
var root_url_web = "/";

var base_url_signalr = root_url_web + "signalr/";

var socket_client_key = "ucvn";

var ckfinder_upload_url =
    "http://ucvn.vn:5530/Services/Core/Fms_File/ck-upload?refType=" +
    file_config.ck_finder.ref_type +
    "&folderCode=" +
    file_config.ck_finder.folder_code;

var select2_config = {
    minimumResultsForSearch: -1,
    placeholder: "",
    allowClear: true,
};
var select2_config_search = function (code, multiple = false) {
    return {
        dropdownParent: $(code).parent(),
        placeholder: "",
        multiple: multiple,
        allowClear: true,
    };
};

var message = {
    success: 'Xử lý thành công',
    error: 'Xảy ra sự cố. Hãy kiểm tra lại'
}