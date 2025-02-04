var Config = (function () {
    async function FormSearchList(fileName) {
        return await $.getJSON(root_url_web + "json/config/uc_form_search_list/" + fileName);
    }
    async function Form(fileName) {
        return await $.getJSON(root_url_web + "json/config/uc_form/" + fileName);
    }
    async function Action(fileName) {
        return await $.getJSON(root_url_web + "json/config/uc_action/" + fileName);
    }
	async function FormReportTemplate(fileName) {
        return await $.getJSON(root_url_web + "json/report/" + fileName);
    }
    async function JsonData(fileName) {
        return await $.getJSON(root_url_web + "json/config/data/" + fileName);
    }
    async function HtmlData(fileName) {
        return await $.get(root_url_web + "json/report/" + fileName);
    }
    return { FormSearchList, Form, Action, FormReportTemplate, JsonData,HtmlData};
})
