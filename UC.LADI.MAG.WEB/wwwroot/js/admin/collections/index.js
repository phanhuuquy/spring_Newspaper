class CollectionService extends UcBase {
    constructor(base_url, endPoint, jsonConfigPath) {
        super(base_url, endPoint, jsonConfigPath);
        super.configure({
            formCode: "add_new_collection",
            formName: "Bộ sưu tập",
            hdKey: "#hdKeyId",
            modalCode: "#modalCollection",
            labelCode: "#DetailLabel",
        });
    }

    init() {
        this.drawTable([]);
    }
}

$(document).ready(async function () {
    try {
        UcAjax.initialize();

        const AppState = {
            jsonConfig: null,

        };

        AppState.jsonConfig = await Config().FormSearchList("Admin/Collections/index.json");

        if (AppState.jsonConfig && Array.isArray(AppState.jsonConfig.columns)) {
            new UcFormHelpers().DataTableAjaxClientSide(
                "#collections-table",
                "/api/MagCollection/v2/GetAllCollections",
                AppState.jsonConfig.columns,
                []
            );
        } else {
            console.error("Lỗi: Không tìm thấy cấu hình cột hợp lệ trong JSON.", AppState.jsonConfig);
        }
    } catch (error) {
        console.error("Lỗi khi tải cấu hình bảng:", error);
    }

    const apiEndpoint = "/api/MagCollection/v2";

    $("#action-add").on("click", function () {
        $("#collection-form")[0].reset();
        $("#modalAddLabel").text("Thêm mới Bộ Sưu Tập");
        $("#btn-save").data("action", "create");
    });

    $("#btn-save").on("click", function () {
        const action = $(this).data("action");
        const id = $(this).data("id");
        const formData = {
            name: $("#collection-name").val(),
            order_index: $("#collection-order-index").val(),
            tenant: $("#collection-tenant").val()
        };

        const url = action === "edit"
            ? `${apiEndpoint}/UpdateCollection/${id}`
            : `${apiEndpoint}/CreateCollection`;

        const method = action === "edit" ? "PUT" : "POST";

        UcAjax[method.toLowerCase()](url, formData).then(response => {
            if (response && response.success) {
                alert("Thành công!");
                $("#modal-add").modal("hide");
                $("#collections-table").DataTable().ajax.reload();
            } else {
                alert("Thao tác thất bại: " + (response?.message || "Lỗi không xác định"));
            }
        }).catch(error => console.error("Lỗi khi lưu dữ liệu:", error));
    });

    $("#collections-table").on("click", ".action-edit", function () {
        const id = $(this).data("id");
        UcAjax.get(`${apiEndpoint}/GetCollectionById/${id}`).then(response => {
            if (response && response.success) {
                const collection = response.data;
                $("#collection-name").val(collection.name);
                $("#collection-order-index").val(collection.order_index);
                $("#collection-tenant").val(collection.tenant);
                $("#modalAddLabel").text("Chỉnh sửa Bộ Sưu Tập");
                $("#btn-save").data("action", "edit").data("id", id);
                $("#modal-add").modal("show");
            } else {
                alert("Không tìm thấy bộ sưu tập!");
            }
        }).catch(error => console.error("Lỗi khi tải dữ liệu bộ sưu tập:", error));
    });

    $("#collections-table").on("click", ".action-delete", function () {
        const id = $(this).data("id");
        if (confirm("Bạn có chắc chắn muốn xóa bộ sưu tập này?")) {
            UcAjax.delete(`${apiEndpoint}/DeleteCollection/${id}`).then(response => {
                if (response && response.success) {
                    alert("Xóa thành công!");
                    $("#collections-table").DataTable().ajax.reload();
                } else {
                    alert("Xóa thất bại: " + (response?.message || "Lỗi không xác định"));
                }
            }).catch(error => console.error("Lỗi khi xóa:", error));
        }
    });

    $("#form-refresh").on("click", function () {
        $("#collection-form")[0].reset();
    });
});