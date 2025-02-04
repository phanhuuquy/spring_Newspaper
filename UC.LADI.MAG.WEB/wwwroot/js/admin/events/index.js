class EventService extends UcBase {
    constructor(base_url, endPoint, jsonConfigPath) {
        super(base_url, endPoint, jsonConfigPath);
        super.configure({
            formCode: "add_new_event",
            hdKey: "#hdKeyId",
            formName: "Sự kiện",
            modalCode: "#modalEvent",
            labelCode: "#DetailLabel",
        });
    }

    init() {
        this.drawTable([]);
    }
}

$(document).ready(function () {
    var eventService = new EventService(
        base_url_api,
        "MagEvent",
        "Admin/Events/index.json"
    );
    eventService.init();
});
