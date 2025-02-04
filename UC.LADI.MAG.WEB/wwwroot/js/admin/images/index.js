class ImageService extends UcBase {
    constructor(base_url, endPoint, jsonConfigPath) {
        super(base_url, endPoint, jsonConfigPath);
        super.configure({
            formCode: "add_new_image",
            hdKey: "#hdKeyId",
            formName: "Ảnh",
            modalCode: "#modalImage",
            labelCode: "#DetailLabel",
        });
    }

    init() {
        this.drawTable([]);
    }
}

$(document).ready(function () {
    var imageService = new ImageService(
        base_url_api,
        "MagEvent",
        "Admin/Images/index.json"
    );
    imageService.init();
});
