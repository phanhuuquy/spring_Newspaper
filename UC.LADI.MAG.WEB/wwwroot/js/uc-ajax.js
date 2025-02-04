/**
 * Chứa các phương thức API tĩnh. Có thể bổ trợ cho UcBase.
 */
class UcAjax {
    static initialize() {
        $.ajaxSetup({
            headers: {
            },
            beforeSend: () => openLoader(),
            error: (jqXHR, textStatus, errorThrown) =>
                UcAjax.handleAjaxError(jqXHR, textStatus, errorThrown),
            complete: () => closeLoader(),
        });
    }
    /**
     * Gọi API bằng phương thức GET.
     * @param {string} url - Đường dẫn API.
     * @returns {Promise<any>} Đối tượng jQuery.ajax để sử dụng tiếp tục.
     */
    static get(url) {
        return $.ajax({
            url,
            type: "GET",
        });
    }

    /**
     * Gọi API bằng phương thức POST.
     * @param {string} url - Đường dẫn API.
     * @param {object} data - Dữ liệu gửi lên server.
     * @returns {Promise<any>} Đối tượng jQuery.ajax để sử dụng tiếp tục.
     */
    static post(url, data) {
        const isFormData = data instanceof FormData;

        return $.ajax({
            url: url,
            type: "POST",
            contentType: isFormData ? false : "application/json",
            processData: !isFormData,
            dataType: "json",
            data: isFormData ? data : data ? JSON.stringify(data) : undefin ,
        });
    }

    /**
     * Gọi API bằng phương thức PUT.
     * @param {string} url - Đường dẫn API.
     * @param {object} data - Dữ liệu cập nhật gửi lên server.
     * @returns {Promise<any>} Đối tượng jQuery.ajax để sử dụng tiếp tục.
     */
    static put(url, data) {
        return $.ajax({
            url,
            type: "PUT",
            data: data ? JSON.stringify(data) : null,
            dataType: "json",
            contentType: "application/json",
        });
    }

    /**
     * Gọi API bằng phương thức DELETE.
     * @param {string} url - Đường dẫn API.
     * @returns {Promise<any>} Đối tượng jQuery.ajax để sử dụng tiếp tục.
     */
    static delete(url) {
        return $.ajax({
            url,
            type: "DELETE",
        });
    }

    /**
     * Xử lý lỗi toàn cục cho các yêu cầu AJAX.
     * @param {jqXHR} jqXHR - Đối tượng jQuery XMLHttpRequest.
     * @param {string} textStatus - Trạng thái lỗi.
     * @param {string} errorThrown - Mô tả lỗi.
     */
    static handleAjaxError(jqXHR, textStatus, errorThrown) {
        let errorMessage = "Đã xảy ra lỗi bất ngờ. Vui lòng thử lại sau.";

        if (jqXHR.responseText) {
            try {
                let error = JSON.parse(jqXHR.responseText);
                errorMessage = error.message || "Đã xảy ra lỗi trên máy chủ.";
            } catch (e) {
                errorMessage = jqXHR.responseText;
                console.log("Lỗi phân tích JSON:", e);
            }
        } else if (textStatus === "timeout") {
            errorMessage =
                "Yêu cầu đã hết thời gian chờ. Vui lòng kiểm tra kết nối internet và thử lại.";
        } else if (textStatus === "abort") {
            errorMessage = "Yêu cầu đã bị hủy. Vui lòng thử lại.";
        } else if (jqXHR.status === 0) {
            errorMessage =
                "Lỗi kết nối mạng. Vui lòng kiểm tra kết nối internet của bạn.";
        } else if (jqXHR.status === 404) {
            errorMessage = "Không tìm thấy tài nguyên được yêu cầu (404).";
        } else if (jqXHR.status === 500) {
            errorMessage = "Lỗi máy chủ (500). Vui lòng thử lại sau.";
        } else if (jqXHR.status === 502) {
            errorMessage =
                "Lỗi gateway (502). Máy chủ đang gặp sự cố. Vui lòng thử lại sau.";
        }

        Toast().ShowToastError(errorMessage);
        console.log("Phản hồi:", jqXHR.responseText);
        console.log("Trạng thái:", textStatus);
        console.log("Lỗi:", errorThrown);
        console.log("Mã trạng thái:", jqXHR.status);
    }
}
