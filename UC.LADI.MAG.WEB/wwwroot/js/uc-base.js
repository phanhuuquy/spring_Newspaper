class UcBase {
    constructor(base_url, endPoint, jsonConfigPath) {
        this.base_url = base_url;
        this.endPoint = endPoint;
        this.jsonConfigPath = jsonConfigPath;

        this.tableCode = "#tbl_search";
        this.formName = "";
        this.formCode = "";
        this.modalCode = null;
        this.oValue = {};
        this.hdKey = "#hdKey";
        this.inputFileCode = "#input_file";
        this.fileId = null;
        this.refId = null;
        this.refType = null;
        this.folderCode = null;
        this.labelCode = null;
        this.btnSaveCode = "#btn-save";
        this.btnAddCode = "#btn-add";
        this.btnRefreshCode = "#btn-refresh";
        this.btnGoBackCode = "#btn-go-back";
        this.btnCloseCode = "#btn-close";
        this.needToBeUuid = false;
        this.needToBeDMY = [];
        this.needToBeInt = [];
        this.needToBeEmpty = [];
        this.defaultValueIfNone = [];
        this.selectOptions = [];
        this.promiseOptions = [];
        this.enableFixedColumn = true;
        this.jsonConfig = null;
        this.editCodeOfTable = ".action-edit";
        this.deleteCodeOfTable = ".action-delete";
        this.detailCodeOfTable = ".action-detail";
        this.queryInsert = "insert-item";
        this.queryDelete = "delete-item";
        this.queryParamsDelete = {};
        this.useQueryFormatDelete = false;
        this.queryUpdate = "update-item";
        this.queryGetItemById = "get-item-by-id";
        this.queryParamsGetItemById = {};
        this.useQueryFormatGetItemById = false;
        this.queryGetItems = "get-items";
        this.queryParamsGetItems = {};
        this.useQueryFormatGetItems = false;
        this.queryGetList = "get-items";
        this.queryParamsGetList = {};
        this.useQueryFormatGetList = false;
        this.queryOSearch = "search";
    }

    /**
     * @typedef {Object} DefaultValue
     * @property {string} key - Tên thuộc tính. Ví dụ `id`, `code`, ...
     * @property {string} value - Giá trị mặc định của thuộc tính. Ví dụ `0`, ``, `00000000-0000-0000-0000-000000000000`, ...
     */

    /**
     * @typedef {Object} SelectOption
     * @property {string} base_url - Base_url api của select. Ví dụ: `http://localhost:8080/`.
     * @property {string} end_point - End_point của api. Ví dụ: `category/get-items`.
     * @property {string} selectCode - Code của select. Ví dụ: `#sel_category`, `.sel_category`.
     * @property {string} label - Trường thuộc tính làm label của option.
     * @property {string} value - Trường thuộc tính làm value của option.
     * @property {string} orderBy - Sắp xếp label. asc: tăng dần, desc: giảm dần. Mặc định là tăng dần.
     * @property {boolean} [isSelect2] - Sử dụng select2 (mặc định là false).
     * @property {boolean} [searching] - Bật tìm kiếm cho select2 (mặc định là false).
     */

    /**
     * @typedef {Object} PromiseOptions
     * @property {string} base_url - Base_url api của promise. Ví dụ: `http://localhost:8080/`.
     * @property {string} end_point - End_point của api. Ví dụ: `category/get-items`.
     * @property {(data: Object) => void} assignVariable - Hàm trả về `response.data` khi nhận được dữ liệu từ api. Ví dụ: function(data) { this.category = data; }, (data) => { this.category = data; }
     */

    /**
     * @typedef {Object} Config
     * @property {string} tableCode - Code của bảng. Ví dụ: `#tbl_search`. Mặc định là `#tbl_search`.
     * @property {string} formCode - Class của form (ví dụ: `add_new`). Lưu ý: Không cần `.` hay `#` đằng trước formCode.
     * @property {string} formName - Tên của form (sử dụng để làm tên hiển thị trên modal).
     * @property {string} modalCode - Code của modal. Ví dụ `#modalAdd`, `.modalEdit`
     * @property {string} hdKey - Code của trường ẩn, phục vụ Thêm - Sửa (Cần thêm <input id="..." class="..." type="hidden" /> vào form với id tương ứng). Ví dụ: `#hd_key`.
     * @property {string} inputFileCode - Code của input với type="file". Ví dụ: `#input_file`. Mặc định là `#input_file`.
     * @property {string} fileId - fileId của ảnh (id của ảnh trong database). Có thể không cần nếu chưa có id của ảnh.
     * @property {string} refId - refId của ảnh (Thuộc tính này quan hệ 1 - 1 với fileId).
     * @property {string} refType - refType của nơi lưu trữ ảnh.
     * @property {string} folderCode - folderCode của nơi lưu trữ ảnh.
     * @property {string} labelCode - Code của trường label trong modal. Ví dụ: `#modal-label`, ...
     * @property {boolean} needToBeUuid - Cờ xác định liệu trường id của oValue có phải là UUID hay không.
     * @property {string} btnSaveCode - Code của nút lưu. Ví dụ: `#btn-save`, `.btn-save`. Mặc định là `#btn-save`.
     * @property {string} btnAddCode - Code của nút thêm. Ví dụ: `#btn-add`, `.btn-add`. Mặc định là `#btn-add`.
     * @property {string} btnRefreshCode - Code của nút làm mới. Ví dụ: `#btn-refresh`, `.btn-refresh`. Mặc định là `#btn-refresh`.
     * @property {string} btnCloseCode - Code của nút đóng modal. Ví dụ: `#btn-close`, `.btn-close`. Mặc định là `#btn-close`.
     * @property {string} btnGoBackCode - Code của nút quay lại. Ví dụ: `#btn-go-back`, `.btn-go-back`. Mặc định là `#btn-go-back`.
     * @property {string} editCodeOfTable - Code của nút sửa 1 item trong bảng (Nút 3 chấm). Ví dụ: `#action-edit`, `.action-edit`. Mặc đinh là `.action-edit`
     * @property {string} deleteCodeOfTable - Code của nút xóa 1 item trong bảng (Nút 3 chấm). Ví dụ: `#action-delete`, `.action-delete`.
     * @property {string} detailCodeOfTable - Code của nút chi tiết 1 item trong bảng (Nút 3 chấm). Ví dụ: `#action-detail`, `.action-detail`. Mặc định là `.action-detail`.
     * @property {string} queryInsert - Query parameters của api insert. Ví dụ: `insert-item`, `save-item`. Mặc định là `insert-item`.
     * @property {string} queryUpdate - Query parameters của api update. Ví dụ: `update-item`. Mặc định là `update-item`.
     * @property {string} queryDelete - Query parameters của api delete. Ví dụ: `delete-item`. Mặc định là `delete-item`.
     * @property {Object} queryParamsDelete - Các tham số động của query cho API delete. Có thể cấu hình trực tiếp trong deleteItem(). Mặc định là một đối tượng rỗng `{}`. Lưu ý: Lần gọi tiếp theo nếu vẫn là các tham số trước đó thì không cần truyền lại.
     * @property {boolean} useQueryFormatDelete - Xác định xem có sử dụng định dạng query string (ví dụ: `?param=value`) hay không khi gọi API delete. Có thể cấu hình trực tiếp trong deleteItem(). Mặc định là `false`.
     * @property {string} queryGetItems - Query parameters của api get-items. Ví dụ: `get-items`. Mặc định là `get-items`.
     * @property {Object} queryParamsGetItems - Các tham số động của query cho API get-items. Có thể cấu hình trực tiếp trong getItems(). Mặc định là một đối tượng rỗng `{}`. Lưu ý: Lần gọi tiếp theo nếu vẫn là các tham số trước đó thì không cần truyền lại.
     * @property {boolean} useQueryFormatGetItems - Xác định xem có sử dụng định dạng query string (ví dụ: `?param=value`) hay không khi gọi API get-items. Có thể cấu hình trực tiếp trong getItems(). Mặc định là `false`.
     * @property {string} queryGetList - Query parameters của api get-items, nhưng phục vụ vẽ bảng - tableCode cần được cấu hình bằng phương thức configure(). Ví dụ: `get-items`. Mặc định là `get-items`.
     * @property {Object} queryParamsGetList - Các tham số động của query cho API get-items phục vụ vẽ bảng. Có thể cấu hình trực tiếp trong getList(). Mặc định là một đối tượng rỗng `{}`. Lưu ý: Lần gọi tiếp theo nếu vẫn là các tham số trước đó thì không cần truyền lại.
     * @property {boolean} useQueryFormatGetList - Xác định xem có sử dụng định dạng query string (ví dụ: `?param=value`) hay không khi gọi API get-items phục vụ vẽ bảng. Có thể cấu hình trực tiếp trong getList(). Mặc định là `false`.
     * @property {string} queryGetItemById - Query parameters của api get-item-by-id. Ví dụ: `get-item-by-id`. Mặc định là `get-item-by-id`.
     * @property {Object} queryParamsGetItemById - Các tham số động của query cho API get-item-by-id. Có thể cấu hình trực tiếp trong getItemById(). Mặc định là một đối tượng rỗng `{}`. Lưu ý: Lần gọi tiếp theo nếu vẫn là các tham số trước đó thì không cần truyền lại.
     * @property {boolean} useQueryFormatGetItemById - Xác định xem có sử dụng định dạng query string (ví dụ: `?param=value`) hay không khi gọi API get-item-by-id. Có thể cấu hình trực tiếp trong getItemById(). Mặc định là `false`.
     * @property {string} queryOSearch - Query parameters của api tìm kiếm bằng oSearch. Ví dụ: `search`, `search-item`. Mặc định là `search`.
     * @property {Array<string>} needToBeDMY - Danh sách các trường cần định dạng ngày tháng năm (dd/MM/yyyy). Ví dụ: [`created_date`, `published_date`, ...].
     * @property {Array<string>} needToBeInt - Danh sách các trường cần định dạng số nguyên. Ví dụ: [`amount`, `quantity`, ...].
     * @property {Array<string>} needToBeEmpty - Danh sách các trường cần phải gán "". Ví dụ: [`id`, `name`, ...].
     * @property {Array<DefaultValue>} defaultValueIfNone - Danh sách các object chứa các trường thuộc tính được gán giá trị mặc định nếu không có giá trị. Phục vụ cho việc tạo oValue.
     * @property {Array<SelectOption>} selectOptions - Danh sách các object chứa các trường thuộc tính để vẽ Select. Có thể vẽ Select qua Ajax hoặc áp dụng Select2 cho Select đã được tạo sẵn. Sử dụng phương thức onAllSelectsProcessed() để xử lý khi tất cả Select được tạo xong.
     * @property {Array<PromiseOptions>} promiseOptions - Danh sách các object chứa các thuộc tính để thực hiện các promise api. Sử dụng phương thức onPromiseSuccessfully() để xử lý khi tất cả các promise thực thi xong.
     * @property {boolean} enableFixedColumn - Kích hoạt fixed column cho cột action (cuối cùng) của table. Mặc định là _true_.
     */

    /**
     * Đối tượng cấu hình cho form.
     * @param {Config} config
     */

    configure(config) {
        Object.assign(this, config);
        if (config.formName) {
            this.formName = config.formName.toLowerCase();
            this.formCode = config.formCode.replace(".", "");
        }
        this.processSelectOptions();
        this.processPromises();
        this.setupEventHandlers();
    }

    async loadJsonConfig() {
        this.jsonConfig = await Config().FormSearchList(this.jsonConfigPath);
    }

    onPromiseSuccessfully() {}

    processPromises() {
        const promises = this.promiseOptions.map((promiseOption) => {
            return UcAjax.get(
                promiseOption.base_url + promiseOption.end_point
            ).done((response) => {
                promiseOption.assignVariable(response.data);
            });
        });

        Promise.all(promises)
            .then(() => this.onPromiseSuccessfully())
            .catch((error) => Toast().ShowToastError(error));
    }

    /**
     * Trả về response khi `getImageUrl()` thành công. `response.data` chứa url.
     */
    onGetImageUrlSuccessfully(response) {}

    /**
     * Lấy url của ảnh theo `fileId` (id của ảnh trên database). Sử dụng phương thức `onGetImageUrlSuccessfully()` để xử lý khi thành công.
     */
    getImageUrl() {
        UcAjax.get(base_url_core + `Fms_File/view?fileId=${this.fileId}`).done(
            (response) => this.onGetImageUrlSuccessfully(response)
        );
    }

    /**
     * Trả về response khi `updateImage()` thành công.
     */
    onUpdateImageSuccessfully(response) {
        if (response.success) {
            Toast().ShowToastSuccess(response.message);
        } else {
            Toast().ShowToastError(response.message);
        }
    }

    /**
     * Phương thức sẽ cập nhật lại tham chiếu của `fileId` với `refId`.
     * @param {string} fileId - Id của file.
     */
    updateImage(fileId) {
        UcAjax.put(
            base_url_core +
                `Fms_File/update-ref-id?fileId=${fileId}&refId=${this.refId}`
        ).done((response) => {
            this.fileId = fileId;
            this.onUpdateImageSuccessfully(response);
        });
    }

    /**
     * Trả về response khi `uploadImage()` thành công. response.data[0] bao gồm `fileId`, `fileName`, `baseUrl`, `path` và `message`.
     */
    onUploadImageSuccessfully(response) {
        if (response.success) {
            this.updateImage(response.data[0].fileId);
        } else {
            Toast().ShowToastError(response.message);
        }
    }

    /**
     * Phương thức tải ảnh lên server. Cần cấu hình (`fileId`), `inputFileCode`, `refId`, `refType` và `folderCode` bằng phương thức `configure()`.
     */
    uploadImage() {
        var refId = this.refId;
        var fileId = this.fileId;
        var refType = this.refType;
        var folderCode = this.folderCode;
        var imageFile = $(this.inputFileCode).prop("files");

        if (imageFile.length === 0) return;

        var formData = new FormData();
        fileId
            ? formData.append("files", imageFile[0])
            : formData.append(`FileId_${fileId}`, imageFile[0]);

        UcAjax.post(
            base_url_core +
                `Fms_File/upload?refId=${refId}&refType=${refType}&folderCode=${folderCode}`,
            formData
        ).done((response) => this.onUploadImageSuccessfully(response));
    }

    /**
     * Phương thức vẽ bảng theo data truyền vào. `tableCode`, `jsonConfig` cần được cấu hình.
     */
    async drawTable(data) {
        if (!this.jsonConfig) {
            await this.loadJsonConfig(this.jsonConfigPath);
        }

        UcFormHelpers().DataTableAjaxClientSide(
            `${this.tableCode}`,
            this.jsonConfig.columns,
            data || [],
            {
                autoWidth: false,
                rowCallback: this.rowCallback,
            },
            this.enableFixedColumn
        );
    }

    drawSelectWithAjax(response, item) {
        if (response.success) {
            let options = UcFormHelpers().DrawOptionsSelect(
                response.data,
                item.value,
                item.label,
                item.isSelect2 ? undefined : "select"
            );

            const selectElement = $(item.selectCode);
            selectElement.html(options);

            if (item.isSelect2) {
                const select2Config = item.searching
                    ? select2_config_search(item.selectCode)
                    : { minimumResultsForSearch: -1 };

                if (!selectElement.hasClass("select2-hidden-accessible")) {
                    selectElement.select2(select2Config);

                    if (!item.searching) {
                        $(document)
                            .off("click", `${item.selectCode} + span`)
                            .on(
                                "click",
                                `${item.selectCode} + span`,
                                function () {
                                    $(`${item.selectCode}`)
                                        .siblings("span.select2-container")
                                        .find("span.select2-search")
                                        .remove();
                                }
                            );
                    }
                }
            }
        } else {
            Toast().ShowToastError(response.message);
        }
    }

    drawSelectWithoutAjax(item) {
        const selectElement = $(item.selectCode);

        if (item.isSelect2) {
            const select2Config = item.searching
                ? select2_config_search(item.selectCode)
                : { minimumResultsForSearch: Infinity };

            if (!selectElement.hasClass("select2-hidden-accessible")) {
                selectElement.select2(select2Config);
            }
        }
    }

    /**
     * Xử lý khi tất cả select được tạo. Các select này được cấu hình trong `selectOptions` ở phương thức `configure()`.
     */
    onAllSelectsProcessed() {}

    /**
     * Xử lý khi tất cả select được vẽ lại. Các select này cần được cấu hình trước đó trong `selectOptions` ở phương thức `configure()`.
     */
    onAllSelectsProcessedAgain() {}

    /**
     * Vẽ lại các Select đã được cấu hình ở `configure()`. Truyền vào 1 mảng gồm có các selectCode. Ví dụ: [`#select1`, `#select2`, ...]. Mặc định vẽ lại tất cả.
     */
    drawSelectAgain(selectCodes = []) {
        if (selectCodes.length == 0) {
            this.processSelectOptions();
        } else {
            const promises = this.selectOptions
                .filter((item) => selectCodes.includes(item.selectCode))
                .map((item) => {
                    return this.getResolveOfProcessSelect(item);
                });

            Promise.all(promises)
                .then(() => this.onAllSelectsProcessedAgain())
                .catch((error) => console.error(error));
        }
    }

    getResolveOfProcessSelect(item) {
        if (!item.base_url || !item.end_point) {
            this.drawSelectWithoutAjax(item);
            return Promise.resolve();
        }

        let hasStartParamSymbol = item.end_point.includes("?");

        let url =
            item.base_url +
            item.end_point +
            (hasStartParamSymbol ? "&" : "?") +
            `columnsQuery=${item.value}%2C%20${item.label}&orderQuery=${
                item.label
            }%20${item.orderBy || "asc"}`;

        return UcAjax.get(url).done((response) =>
            this.drawSelectWithAjax(response, item)
        );
    }

    processSelectOptions() {
        if (this.selectOptions && this.selectOptions.length > 0) {
            const promises = this.selectOptions.map((item) => {
                return this.getResolveOfProcessSelect(item);
            });

            Promise.all(promises)
                .then(() => this.onAllSelectsProcessed())
                .catch((error) => console.error(error));
        }
    }

    extendData() {
        var oForm = UcFormHelpers().GetFormValues(this.formCode);
        if (!oForm) return;
        var oValue = UcFormHelpers().FormFieldsToObject(oForm);

        var initialOValue = oValue;
        if (this.needToBeUuid) {
            oValue.id = Uuid().Uuidv4();
        } else {
            oValue.id = "";
        }

        if (this.needToBeDMY.length > 0) {
            let charSplit = this.needToBeDMY[needToBeDMY.length - 1]
                ? this.needToBeDMY[this.needToBeDMY.length - 1]
                : "/";
            for (let key in initialOValue) {
                if (this.needToBeDMY.includes(key)) {
                    oValue[key] = new DateUtil().FormatDateToDDMMYYYY(
                        oValue[key],
                        charSplit
                    );
                }
            }
        }

        if (this.needToBeInt.length > 0) {
            for (let key in initialOValue) {
                if (this.needToBeDMY.includes(key)) {
                    oValue[key] = parseInt(oValue[key]);
                }
            }
        }

        if (this.needToBeEmpty.length > 0) {
            for (let key in this.needToBeEmpty) {
                oValue[key] = "";
            }
        }

        if (this.defaultValueIfNone.length > 0) {
            this.defaultValueIfNone.forEach((defaultValue) => {
                if (
                    initialOValue[defaultValue.key] === null ||
                    initialOValue[defaultValue.key] === undefined ||
                    initialOValue[defaultValue.key] === ""
                ) {
                    oValue[defaultValue.key] = defaultValue.value;
                }
            });
        }

        return oValue;
    }

    /**
     * Tạo ra chuỗi query parameters.
     * @param {Object} params - Các tham số cần thiết dưới dạng object { key: value }. Có thể không cần truyền gì nếu không có param.
     * @param {boolean} useQueryFormat - _true_ nếu muốn dạng query string (?param=value), _false_ nếu dạng URL (/param).
     */
    buildUrlParams(params = {}, useQueryFormat = true) {
        if (!params || Object.keys(params).length === 0) return "";

        if (useQueryFormat) {
            // Dùng dạng query string: ?key1=value1&key2=value2
            return (
                "?" +
                Object.entries(params)
                    .map(
                        ([key, value]) =>
                            `${encodeURIComponent(key)}=${encodeURIComponent(
                                value
                            )}`
                    )
                    .join("&")
            );
        } else {
            // Dùng dạng path: /value1/value2
            return (
                "/" +
                Object.values(params)
                    .map((value) => encodeURIComponent(value))
                    .join("/")
            );
        }
    }

    /**
     * Xử lý khi response được trả về từ `getItems()`.
     */
    onGottenItems(response) {}

    /**
     * Lấy danh sách trả về từ API.
     * @param {Object} params - Các tham số cần thiết dưới dạng object { key: value }. Có thể không cần truyền gì nếu không có param.
     * @param {boolean} useQueryFormat - _true_ nếu muốn dạng query string (?param=value), _false_ nếu dạng URL (/param).
     */
    getItems(params = null, useQueryFormat = null) {
        if (params && Object.keys(params).length > 0) {
            this.queryParamsGetItems = params;
        }

        if (useQueryFormat !== null) {
            this.useQueryFormatGetItems = useQueryFormat;
        }

        const queryString = this.buildUrlParams(
            this.queryParamsGetItems,
            this.useQueryFormatGetItems
        );

        const url =
            this.base_url +
            this.endPoint +
            "/" +
            this.queryGetItems +
            queryString;
        UcAjax.get(url).done((response) => {
            this.onGottenItems(response);
        });
    }

    /**
     * Phương thức này được sử dụng để tùy chỉnh nội dung hoặc xử lý logic đặc biệt cho từng hàng. Trả về gồm: row (hàng hiện tại), data (data object của hàng đó) và index (chỉ số của hàng). Lưu ý: Phải sử dụng arrow function đối với hàm này.
     */
    rowCallback = (row, data, index) => {};

    /**
     * Xử lý khi response được trả về từ `getList()`.
     */
    onGottenList(response) {
        if (response.success) {
            this.drawTable(response.data);
        }
    }

    /**
     * Phương thức này lấy danh sách phục vụ vẽ bảng. Nếu bạn muốn lấy danh sách từ API, hãy dùng `getItems()`.
     * @param {Object} params - Các tham số cần thiết dưới dạng object { key: value }. Có thể không cần truyền gì nếu không có param.
     * @param {boolean} useQueryFormat - _true_ nếu muốn dạng query string (?param=value), _false_ nếu dạng URL (/param).
     */
    getList(params = null, useQueryFormat = null) {
        if (params && Object.keys(params).length > 0) {
            this.queryParamsGetList = params;
        }

        if (useQueryFormat !== null) {
            this.useQueryFormatGetList = useQueryFormat;
        }

        const queryString = this.buildUrlParams(
            this.queryParamsGetList,
            this.useQueryFormatGetList
        );

        const url =
            this.base_url +
            this.endPoint +
            "/" +
            this.queryGetList +
            queryString;
        UcAjax.get(url).done(async (response) => {
            if (!this.jsonConfig) {
                await this.loadJsonConfig(this.jsonConfigPath);
            }
            this.onGottenList(response);
        });
    }

    /**
     * Xử lý khi response được trả về từ `insertItem()`.
     */
    onInsertSuccessfully(response) {
        if (response.success) {
            Toast().ShowToastSuccess("Thêm mới thành công!");
            this.jsonConfigPath && this.getList();
        } else {
            Toast().ShowToastError(response.message);
        }
    }

    insertItem(oValue) {
        UcAjax.post(
            this.base_url + this.endPoint + "/" + this.queryInsert,
            oValue
        ).done((response) => this.onInsertSuccessfully(response));
    }

    /**
     * Xử lý khi response được trả về từ `updateItem()`.
     */
    onUpdateSuccessfully(response) {
        if (response.success) {
            Toast().ShowToastSuccess("Cập nhật thành công!");
            this.jsonConfigPath && this.getList();
        } else {
            Toast().ShowToastError(response.message);
        }
    }

    updateItem(oValue) {
        UcAjax.put(
            this.base_url + this.endPoint + "/" + this.queryUpdate,
            oValue
        ).done((response) => {
            this.onUpdateSuccessfully(response);
        });
    }

    /**
     * Xử lý khi response được trả về từ `getItemById()`.
     */
    onGottenItemById(response) {
        if (response.success && response.data) {
            this.hdKey && $(this.hdKey).val(response.data.id);
            UcFormHelpers().SetFormValues(this.formCode, response.data);
        } else {
            Toast().ShowToastError(response.message);
        }
    }

    /**
     * Phương thức lấy item theo id.
     * @param {Object} params - Tham số cần thiết dưới dạng object { key: value }.
     * @param {boolean} useQueryFormat - `true` nếu muốn dạng query string `?param=value`, `false` nếu dạng URL `/param`.
     */
    getItemById(params = null, useQueryFormat = null) {
        if (params && Object.keys(params).length > 0) {
            this.queryParamsGetItemById = params;
        }

        if (useQueryFormat !== null) {
            this.useQueryFormatGetItemById = useQueryFormat;
        }

        const queryString = this.buildUrlParams(
            this.queryParamsGetItemById,
            this.useQueryFormatGetItemById
        );

        const url =
            this.base_url +
            this.endPoint +
            "/" +
            this.queryGetItemById +
            queryString;
        UcAjax.get(url).done((response) => this.onGottenItemById(response));
    }

    /**
     * Xử lý khi response được trả về từ `deleteItem()`.
     */
    onDeleteSuccessfully(response) {
        if (response.success) {
            Toast().ShowToastSuccess("Xóa thành công!");
            this.jsonConfigPath && this.getList();
        } else {
            Toast().ShowToastError(response.message);
        }
    }

    /**
     * Phương thức xóa item theo id.
     * @param {Object} params - Tham số cần thiết dưới dạng object { key: value }.
     * @param {boolean} useQueryFormat - _true_ nếu muốn dạng query string (?param=value), _false_ nếu dạng URL (/param).
     */
    deleteItem(params = null, useQueryFormat = null) {
        if (params && Object.keys(params).length > 0) {
            this.queryParamsDelete = params;
        }

        if (useQueryFormat !== null) {
            this.useQueryFormatDelete = useQueryFormat;
        }

        const queryString = this.buildUrlParams(
            this.queryParamsDelete,
            this.useQueryFormatDelete
        );

        const url =
            this.base_url +
            this.endPoint +
            "/" +
            this.queryDelete +
            queryString;

        UcAjax.delete(url).done((response) =>
            this.onDeleteSuccessfully(response)
        );
    }

    onGetItemsByOSearch(response) {}

    /**
     * Phương thức tìm kiếm dữ liệu theo oSearch. oSearch của form được lấy bằng phương thức UcFormHelpers().GetFormValues(_formCode_).
     */
    searchItems(oSearch) {
        UcAjax.post(
            this.base_url + this.endPoint + "/" + this.queryOSearch,
            oSearch
        ).done(async (response) => {
            if (!this.jsonConfig) {
                await this.loadJsonConfig(this.jsonConfigPath);
            }
            this.onGetItemsByOSearch(response);
        });
    }

    refreshForm() {
        UcFormHelpers().RefreshFormValues(this.formCode);
        $(`.${this.formCode} input`).eq(0).focus();
    }

    /**
     * Lấy oValue của form. Lưu ý: Cần cấu hình code của form bằng formCode của phương thức `configure()`.
     */
    getOValue() {
        var oForm = UcFormHelpers().GetFormValues(this.formCode);
        if (!oForm) return null;
        var oValue = UcFormHelpers().FormFieldsToObject(oForm);
        oValue = this.extendData(oValue);
        return oValue;
    }

    /**
     * Xử lý sự kiện khi nhấn nút lưu. Code của nút lưu cần được cấu hình trong `btnSaveCode` bằng phương thức `configure()`.
     */
    onClickSave() {
        this.oValue = this.getOValue();
        if (!this.oValue) return;
        var id = $(this.hdKey).val();

        if (!id) {
            this.insertItem(this.oValue);
        } else {
            this.oValue.id = id;
            this.updateItem(this.oValue);
        }
    }

    /**
     * Xử lý sự kiện khi nhấn 3 chấm trong bảng -> Nút chỉnh sửa với class được gán cho `editCodeOfTable`.
     */
    onClickEdit(id) {
        this.refreshForm();
        $(this.labelCode).text(`Chỉnh sửa ${this.formName}`);
        $(this.hdKey).val(id);
        this.getItemById({ id: id });
    }

    /**
     * Xử lý sự kiện khi nhấn nút làm mới với code được gán cho `btnRefreshCode`.
     */
    onClickRefresh() {
        this.refreshForm();
        this.formCode && $(`.${this.formCode} input`).eq(0).focus();
    }

    /**
     * Xử lý sự kiện khi nhấn nút thêm với code được gán cho `btnAddCode`.
     */
    onClickAdd() {
        this.refreshForm();
        $(this.hdKey).val("");
        $(this.labelCode).text(`Thêm mới ${this.formName}`);
    }

    /**
     * Xử lý sự kiện khi nhấn 3 chấm trong bảng -> Nút chi tiết với class được gán cho `detailCodeOfTable`.
     */
    onClickDetail(id) {}

    /**
     * Xử lý sự kiện khi nhấn 3 chấm trong bảng -> Nút xóa với class được gán cho `deleteCodeOfTable`.
     */
    onClickDelete(id) {
        this.deleteItem({ id: id });
    }

    /**
     * Xử lý sự kiện khi modal bắt đầu xuất hiện. Code của modal cần được cấu hình ở phương thức `configure()`.
     */
    onShowModal() {}

    /**
     * Xử lý sự kiện khi modal hiển thị xong. Code của modal cần được cấu hình ở phương thức `configure()`.
     */
    onShownModal() {
        this.formCode && $(`.${this.formCode} input`).eq(0).focus();
    }

    /**
     * Xử lý sự kiện khi modal bắt đầu đóng. Code của modal cần được cấu hình ở phương thức `configure()`.
     */
    onHideModal() {}

    /**
     * Xử lý sự kiện khi modal đóng hoàn toàn. Code của modal cần được cấu hình ở phương thức `configure()`.
     */
    onHiddenModal() {}

    /**
     * Xử lý sự kiện khi ấn nút quay lại với code được gán cho `btnGoBackCode`.
     */
    onClickGoBack() {
        window.history.back();
    }

    /**
     * Xử lý sự kiện khi ấn nút đóng modal với code được gán cho `btnCloseCode`.
     */
    onClickCloseModal() {
        $(this.modalCode).modal("hide");
    }

    /**
     * Cuộn tới và focus vào element theo id.
     */
    scrollToControl(id) {
        document.getElementById(`${id}`).scrollIntoView({
            behavior: "smooth",
            block: "start",
        });
        $("#" + id).focus();
    }

    /**
     * Khởi tạo các sự kiện nền.
     */
    setupEventHandlers() {
        const self = this;

        if (
            this.btnAddCode &&
            $(this.btnAddCode).length > 0 &&
            !$._data($(this.btnAddCode)[0], "events")?.click
        ) {
            $(this.btnAddCode).on("click", () => this.onClickAdd());
        }

        if (this.modalCode) {
            const events = {
                "shown.bs.modal": () => this.onShownModal(),
                "show.bs.modal": () => this.onShowModal(),
                "hidden.bs.modal": () => this.onHiddenModal(),
                "hide.bs.modal": () => this.onHideModal(),
            };

            for (const [event, handler] of Object.entries(events)) {
                const isEventAttached = $._data(
                    $(this.modalCode)[0],
                    "events"
                )?.[event]?.some((e) => e.handler === handler);

                if (!isEventAttached) {
                    $(this.modalCode).on(event, handler);
                }
            }
        }

        if (
            this.btnRefreshCode &&
            $(this.btnRefreshCode).length > 0 &&
            !$._data($(this.btnRefreshCode)[0], "events")?.click
        ) {
            $(this.btnRefreshCode).on("click", () => this.onClickRefresh());
        }

        if (
            this.btnSaveCode &&
            $(this.btnSaveCode).length > 0 &&
            !$._data($(this.btnSaveCode)[0], "events")?.click
        ) {
            $(this.btnSaveCode).on("click", () => this.onClickSave());
        }

        if (
            this.btnGoBackCode &&
            $(this.btnGoBackCode).length > 0 &&
            !$._data($(this.btnGoBackCode)[0], "events")?.click
        ) {
            $(this.btnGoBackCode).on("click", () => this.onClickGoBack());
        }

        if (
            this.btnCloseCode &&
            $(this.btnCloseCode).length > 0 &&
            !$._data($(this.btnCloseCode)[0], "events")?.click
        ) {
            $(this.btnCloseCode).on("click", () => this.onClickCloseModal());
        }

        if (
            this.editCodeOfTable &&
            !$._data($(document)[0], "events")?.click?.some(
                (e) =>
                    e.selector ===
                    `${this.tableCode} tbody ${this.editCodeOfTable}`
            )
        ) {
            $(document).on(
                "click",
                `${this.tableCode} tbody ${this.editCodeOfTable}`,
                function () {
                    const id = $(this).data("id");
                    self.onClickEdit(id);
                }
            );
        }

        if (
            this.detailCodeOfTable &&
            !$._data($(document)[0], "events")?.click?.some(
                (e) =>
                    e.selector ===
                    `${this.tableCode} tbody ${this.detailCodeOfTable}`
            )
        ) {
            $(document)
                .off(
                    "click",
                    `${this.tableCode} tbody ${this.detailCodeOfTable}`
                )
                .on(
                    "click",
                    `${this.tableCode} tbody ${this.detailCodeOfTable}`,
                    function () {
                        const id = $(this).data("id");
                        self.onClickDetail(id);
                    }
                )
                .data("event-attached", true);
        }

        if (
            this.deleteCodeOfTable &&
            !$._data($(document)[0], "events")?.click?.some(
                (e) =>
                    e.selector ===
                    `${this.tableCode} tbody ${this.deleteCodeOfTable}`
            )
        ) {
            $(document)
                .off(
                    "click",
                    `${this.tableCode} tbody ${this.deleteCodeOfTable}`
                )
                .on(
                    "click",
                    `${this.tableCode} tbody ${this.deleteCodeOfTable}`,
                    function () {
                        var id = $(this).data("id");
                        Notification().Confirm(
                            `Bạn có muốn xóa ${self.formName} này không?`,
                            function () {
                                self.onClickDelete(id);
                            },
                            null
                        );
                    }
                )
                .data("event-attached", true);
        }
    }
}
