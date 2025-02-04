class ItemService extends UcBase {
    constructor(base_url, endPoint, jsonConfigPath) {
        super(base_url, endPoint, jsonConfigPath);
        super.configure({
            formCode: "add_new_item",
            hdKey: "#hdKeyId",
            formName: "Báo",
            modalCode: "#modalItem",
            labelCode: "#DetailLabel",
        });
    }

    init() {
        this.drawTable([]);
    }
}

$(document).ready(function () {
    var itemService = new ItemService(
        base_url_api,
        "MagEvent",
        "Admin/Items/index.json"
    );
    itemService.init();
});
