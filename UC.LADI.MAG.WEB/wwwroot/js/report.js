//******************Vẽ báo cáo dạng bảng************************
var Report = (function () {
	//////Vẽ phần trên báo cáo
    function DrawReportHeader(reports,total) {
        var html = '<div class="header_report">';
		html += '<div class="item_1">'
		
		for (let i = 0; i < reports.length; i++) {
			if (reports[i].type == "organization") {
				html += '   <div id="div_' + reports[i].id + '" class="text-center ' + reports[i].css_class + '" style="' + reports[i].css_style + '" >';
				html += '       <span id="span_' + reports[i].id + '">' + reports[i].data + '</span>';
				html += '   </div>';
			}
		}
		for (let i = 0; i < reports.length; i++) {
			if (reports[i].type == "department") {
				html += '   <div id="div_' + reports[i].id + '" class="text-center ' + reports[i].css_class + '" style="' + reports[i].css_style + '" >';
				html += '       <span id="span_' + reports[i].id + '">' + reports[i].data + '</span>';
				html += '   </div>';
				html += '</div>';
			}
		}
		html += '<div class="item_2">'
		for (let i = 0; i < reports.length; i++) {
			if (reports[i].type == "chxh") {
				html += '   <div id="div_' + reports[i].id + '" class="text-center ' + reports[i].css_class + '" style="' + reports[i].css_style + '" >';
				html += '       <span id="span_' + reports[i].id + '">' + reports[i].data + '</span>';
				html += '   </div>';
		
			}
		}
		for (let i = 0; i < reports.length; i++) {
			if (reports[i].type == "dltd") {
				html += '<div id="div_' + reports[i].id + '" class="text-center ' + reports[i].css_class + '" style="' + reports[i].css_style + '" >';
				html += '   <span id="span_' + reports[i].id + '">' + reports[i].data + '</span>';
				html += '</div>';
			}
		}
		html += '</div>';
		html += '<div class="item_3">'
		
		for (let i = 0; i < reports.length; i++) {
			if (reports[i].type == "title") {
				html += '<div id="div_' + reports[i].id + '" class="text-center ' + reports[i].css_class + '" style="' + reports[i].css_style + '" >';
				html += '   <span id="span_' + reports[i].id + '">' + reports[i].data + '</span>';
				html += '</div>';
			}
		}
		for (let i = 0; i < reports.length; i++) {
			if (reports[i].type == "sub_title") {
				html += '<div id="div_' + reports[i].id + '" class="text-center ' + reports[i].css_class + '" style="' + reports[i].css_style + '" >';
				html += '   <span id="span_' + reports[i].id + '">' + reports[i].data + '</span>';
				html += '</div>';
			}
		}
		
		html += '   </div>';
		html += '</div>';
		
		return html;
	}
	//////Vẽ phần dưới báo cáo
	function DrawReportFooter(reports) {
		var html = '<div class="footer_report">';
		html += '    <div class="item_5">'
		for (let i = 0; i < reports.length; i++) {
			if (reports[i].type == "signer_left") {
				html += '   <div id="div_' + reports[i].id + '" class="text-center ' + reports[i].css_class + '" style="' + reports[i].css_style + '" >';
				html += '       <span id="span_' + reports[i].id + '">' + reports[i].data + '</span>';
				html += '   </div>';
			}
		}
		html += '   </div>';
		html += '   <div class="item_6">'
		for (let i = 0; i < reports.length; i++) {
			if (reports[i].type == "signer_inner") {
				html += '   <div id="div_' + reports[i].id + '" class="text-center ' + reports[i].css_class + '" style="' + reports[i].css_style + '" >';
				html += '       <span id="span_' + reports[i].id + '">' + reports[i].data + '</span>';
				html += '   </div>';
			}
		}
		html += '   </div>';
		html += '   <div class="item_7">'
		for (let i = 0; i < reports.length; i++) {
			if (reports[i].type == "signer_right_top") {
				html += '   <div id="div_' + reports[i].id + '" class="text-center ' + reports[i].css_class + '" style="' + reports[i].css_style + '" >';
				html += '       <span id="span_' + reports[i].id + '">' + reports[i].data + ', '+ formatDate() +'</span>';
				html += '   </div>';
			}
		}
		for (let i = 0; i < reports.length; i++) {
			if (reports[i].type == "signer_right_bot") {
		
				html += '   <div id="div_' + reports[i].id + '" class="text-center ' + reports[i].css_class + '" style="' + reports[i].css_style + '" >';
				html += '       <span id="span_' + reports[i].id + '">' + reports[i].data + '</span>';
				html += '   </div>';
			}
		}
		html += '   </div>';
		html += '</div>';
		return html;
	}
	///Vẽ phần thân báo cáo
	function DrawReportTable1(colums, data) {
		var total = data.length;
		var totalBook = 0;
		var totalPrice = 0;
		var html = '';
		///Vẽ table
		html += '<div class="table_report">'
		html += '   <table class="table table-bordered mb-0" style="table-layout: fixed;">'
		html += '       <thead>'
		html += '           <tr class="text-center align-middle fw-bold">'
		for (let i = 0; i < colums.length; i++) {
			html += '               <th style="' + colums[i].css_style + '">' + colums[i].title + '</th>'
		}
		html += '           </tr>'
		html += '       </thead>'
		html += '       <tbody>'
		for (let i = 0; i < data.length; i++) { 
			html += '           <tr id="s_tr_' + i +'" class="text-center align-middle fw-bold">'
			for (let j = 0; j < colums.length; j++) {
				var dataField = colums[j].data_field;
				var cssClass = colums[j].css_class;
				var cssStyle = colums[j].css_style;
				var dataType = colums[j].data_type;
				if(dataField == ''){
					html += '               <td class="fw-normal"></td>'
				}else if(dataField.toLowerCase().includes("stt")){
					html += '               <td class="fw-normal ' + cssClass + '">' + (i + 1) + '</td>'
				}else {
					html += '               <td style="'+ cssStyle +'" class="fw-normal ' + cssClass + '"><div style="display: -webkit-box;-webkit-box-orient: vertical;-webkit-line-clamp: 2;overflow: hidden;text-overflow: ellipsis;white-space: normal;">' + (data[i][dataField] = (data[i][dataField] === null) ?'' : data[i][dataField]) + '</div></td>'
				}
			}
			html += '           </tr>'
		}
		html += '       </tbody>'
		html += '   </table>'
		///Vẽ phần tổng trong báo cáo
		return html;
	}
	
	function DrawButton(buttons) {
		var html = '';
		for (let i = 0; i < buttons.length; i++) {
			var title = buttons[i].title;
			var type = buttons[i].type;
			var id = buttons[i].id;
			var funct = buttons[i].funct;
			var className = buttons[i].className;
			var icon = buttons[i].icon;
			if(type == "button"){
				html += '<button id="' + id +'" type="' + type + '" class="btn ' + className +'"> <i class="' + icon + '"></i>' + title + '</button>'
			}else if(type == "a"){
				html += '<a id="' + id + '" ' + funct + ' class="btn ' + className + '"> <i class="' + icon + '"></i>' + title + '</a>'
			}
		}
		return html;
	}
	function DrawReportTable2(colums, data, type) {

		var html = '';
		html += '<div class="table_report">'
		html += '   <table class="table table-bordered mb-0" style="table-layout: fixed; border-collapse: collapse;">'
		html += '       <thead>'
		html += '           <tr class="text-center align-middle fw-bold">'
		for (let i = 0; i < colums.length; i++) {
			if(colums[i].options == undefined || colums[i].data_field == 'no_header'){
				html += '               <th style="' + colums[i].css_style + '">' + colums[i].title + '</th>'
			}else {
				
				html += '<th style="' + colums[i].css_style + '">'
				html += '   <div class="text-center ' + colums[i].css_class + '" style="padding-top:3px;padding-bottom:3px;" >';
				html += '       <span style="">' + colums[i].title +'</span>';
				html += '   </div>';
				html += '   <div class="text-center ' + colums[i].css_class + '" style="border-top: 1px solid black; margin-left:-3px; margin-right:-3px;display:flex;margin-bottom: -3px;" >';
				for(let j = 0 ; j < colums[i].options.length; j++){
					if(j == colums[i].options.length-1){
						html += '   <div style="display: block; padding: 3px; ' + colums[i].options[j].css_style + '">'
					}else{
						html += '   <div style="display: block; padding: 3px; border-right: 1px solid black;' + colums[i].options[j].css_style + '">'
					}
					html += '       <span>' + colums[i].options[j].title +'</span>';
					html += '   </div>';
				}
				html += '   </div>';
				html += '</th>';
			}
		}
		html += '           </tr>'
		html += '       </thead>'
		html += DrawReportTableBody(colums, data, type)
		html += '   </table>'
		html += '</div>';
		return html;
	}
	///Vẽ phân fthân bảng
	function DrawReportTableBody(colums, data, type) {
		var html = '';
		html += '       <tbody>'
		for (let i = 0; i < data.length; i++) { 
			if(type == 'RegisterSumary' && i%5 !== 0){
				html += '           <tr style="border-bottom: 1px solid black" id="s_tr_' + i +'" class="text-center align-middle fw-bold">'
			}else{
				html += '           <tr id="s_tr_' + i +'" class="text-center align-middle fw-bold">'
			}
			for (let j = 0; j < colums.length; j++) {
				var dataField = colums[j].data_field;
				var cssClass = colums[j].css_class;
				var dataType = colums[j].data_type;
				if((dataType != 'none' || dataType != '')) {
					if('options' in colums[j]){
				
						html += '<td class="fw-normal ' + cssClass + '" style="white-space: normal;">' 
						html += '	<div style="margin:-3px;display:flex;">'
						for(let x = 0; x < colums[j].options.length; x++){
							var dataFieldOption = colums[j].options[x].data_field;
							var cssClassOption = colums[j].options[x].css_class;
							var cssStyleOption = colums[j].options[x].css_style;
							var txt = data[i][dataFieldOption];
							html += '   <div class="'+ cssClassOption +'" style="padding:3px;'+ cssStyleOption +'">'
							if(txt === null || txt === undefined ){
								html += '       <span>&nbsp;</span>';
							}else{
								if(dataField === 'no_header'){
									if(dataFieldOption === 'author'){
										html += '       <span>' + truncateString(txt,18).toUpperCase() +'</span>';
									}else{
										html += '       <span>' + truncateString(txt,23) +'</span>';
									}
								}else{
									html += '       <span>' + txt +'</span>';
								}
							}
							html += '   </div>';
						}
						html += '   </div>';
						html += '</td>'
					}else if(dataField == ''){
						html += '               <td class="fw-normal"></td>'
					}else if(dataField.toLowerCase().includes("stt")){
						html += '               <td class="fw-normal ' + cssClass + '">' + (i + 1) + '</td>'
					}else {
						html += '               <td class="fw-normal ' + cssClass + '">' + (data[i][dataField] = (data[i][dataField]===null)? '':data[i][dataField]) + '</td>'
					}
				}
			}
			html += '           </tr>'
		}
		html += '       </tbody>';
		return html;
	}
	//Vẽ thẻ
	function DrawReportCard(jsonConfig, id, stt) {
		var data = jsonConfig.reports;
		var html = '';
		html += '<tr id="s_tr_' + stt + '" class="text-center align-middle" style="page-break-inside: avoid;">'; // Thêm style cho ngăn tách trang
		html += '    <td>';
		
		if (data.length > 1) {
			html += '<div style="display: flex; page-break-inside: avoid;">'; // Thêm style cho ngăn tách trang
		} else {
			html += '<div style="page-break-inside: avoid;">'; // Thêm style cho ngăn tách trang
		}

		html += '<style>' + jsonConfig.css + '</style>';
		
		for (let j = 0; j < data.length; j++) {
			html += '<div style="' + jsonConfig.style_div0 + '">';
			
			if (data[j].text1.length > 0) {
				for (let i = 0; i < data[j].text1.length; i++) { 
					html += '    <div style="' + data[j].text1[i].style_div1 + '">';
					html += '        <div style="' + data[j].text1[i].style_div2 + '">';
					html += '           <span>' + data[j].text1[i].text + '</span>';
					html += '        </div>';
					html += '    </div>';
				}
			}

			if (data[j].text2.length > 0) {
				for (let i = 0; i < data[j].text2.length; i++) {
					html += '    <div style="' + data[j].text2[i].style_div1 + '">';
					html += '        <div style="' + data[j].text2[i].style_div2 + '">';
					html += '           <span>' + data[j].text2[i].text1 + '</span><span>' + data[j].text2[i].text2 + '</span>';
					html += '        </div>';
					html += '    </div>';
				}
			}

			if (data[j].img.length > 0) {
				for (let i = 0; i < data[j].img.length; i++) {
					html += '    <div style="' + data[j].img[i].style_div1 + '">';
					html += '        <div style="' + data[j].img[i].style_div2 + '">';
					html += '           <img style="' + data[j].img[i].style_img + '" id="' + data[j].img[i].id + id + '" src="' + data[j].img[i].src + '">';
					html += '        </div>';
					html += '    </div>';
				}
			}

			if (data[j].line.length > 0) {
				for (let i = 0; i < data[j].line.length; i++) {
					html += '    <div style="' + data[j].line[i].style_div1 + '">';
					html += '    </div>';
				}
			}
			
			html += '</div>';
		}

		html += '    </div>';
		html += '    </td>';
		html += '</tr>';
		return html;
	}

	
	
	function formatDate() {
		// Lấy ngày hiện tại
		var today = new Date();

		// Lấy ngày, tháng, năm từ đối tượng Date
		var day = today.getDate();
		var month = today.getMonth() + 1; // Tháng bắt đầu từ 0, nên cần cộng thêm 1
		var year = today.getFullYear();

		// Chuyển đổi ngày, tháng thành chuỗi với định dạng hai chữ số
		day = day < 10 ? '0' + day : day;
		month = month < 10 ? '0' + month : month;

		// Định dạng ngày tháng thành chuỗi "ngày DD tháng MM năm YYYY"
		var formattedDate = `ngày ${day} tháng ${month} năm ${year}`;

		return formattedDate;
	}
	
            
	return {
		DrawReportHeader,
		DrawReportFooter,
		DrawReportTable1,
		DrawReportTable2,
		DrawReportCard,
		//DrawReportTable,
		DrawButton
	}
});
//***********************Vẽ báo cạo dạng card, dkcb,....***********************
function BTCReportDKCB(data) {
        var html = '';
        var htmlDiv = '';
        html += '<table style="">';
        html += '<style>@media print { @page { size: auto; margin: 1cm;} body {-webkit-print-color-adjust: exact; print-color-adjust: exact; } #content_print {  display: grid;}}"</style>';
        for (let i = 0; i < data.length; i++) {

            if (i % 6 == 0) {
                html += '   <tr id="" class="text-center align-middle" style="page-break-inside: avoid;">'; // Thêm style cho ngăn tách trang
                html += '      <td style="display:flex;">';
            }
            html += '          <div class="text-center align-middle" style="height: 3.5cm; width: 2.7cm;margin: 2px;border: 2px black solid;">';
            html += '              <div style="border-bottom: 2px black solid;">';
            html += '                  <div style="font-size: 10px;font-weight: bold; ">THƯ VIỆN</div>';
            html += '                  <div style="line-height: 105%;font-size: 10px;font-weight: bold;">TỈNH SƠN LA</div>';
            html += '              </div>';
            html += '              <div style="padding:12px 1px 3px 1px;">';
            html += '                  <div style="line-height: 100%;font-size: 15px;font-weight: bold;">' + data[i].class.substring(0,10) + '</div>';
            html += '                  <div style="font-size: 15px;font-weight: bold;line-height: 100%;">' + data[i].cutter + '</div>';
            html += '                  <div style="margin: 2px 0 2px 0">';
            html += '                      <img style="height: 0.8cm" id="barcode_card_' + data[i].id + '">';
            html += '                  </div>';
            html += '                  <span style=" line-height: 100%;font-size: 20px;font-weight: bold;">' + data[i].id + '</span>';
            html += '              </div>';
            html += '          </div>';
            if (i % 6 == 5 || i == (data.length - 1)) {
                html += '      </td>';
                html += '   </tr>';
            }
        }
        
        html += '</table>';

        $("#content_print").html(html);
        for (let i = 0; i < data.length; i++) {
            JsBarcode("#barcode_card_" + data[i].id, data[i].id, {
                format: "CODE39",
                lineColor: "#000",
                width: 2,
                height: 100,
                margin: 0,
                displayValue: false,
            });
        }
       
}
function ADReportDKCB(data) {
        var html = '';
        var htmlDiv = '';
        html += '<table style="">';
        html += '<style>@media print { @page { size: auto; margin: 1cm;} body {-webkit-print-color-adjust: exact; print-color-adjust: exact; } #content_print {  display: grid;}}"</style>';
        for (let i = 0; i < data.length; i++) {

            if (i % 6 == 0) {
                html += '   <tr id="" class="text-center align-middle" style="page-break-inside: avoid;">'; // Thêm style cho ngăn tách trang
                html += '      <td style="display:flex;">';
            }
            html += '          <div class="text-center align-middle" style="height: 3.5cm; width: 2.7cm;margin: 2px;border: 2px black solid;">';
            html += '              <div style="border-bottom: 2px black solid;">';
            html += '                  <div style="font-size: 10px;font-weight: bold; ">THƯ VIỆN</div>';
            html += '                  <div style="line-height: 105%;font-size: 10px;font-weight: bold;">SỐ</div>';
            html += '              </div>';
            html += '              <div style="padding:12px 1px 3px 1px;">';
            html += '                  <div style="line-height: 100%;font-size: 15px;font-weight: bold;">' + data[i].class.substring(0,10) + '</div>';
            html += '                  <div style="font-size: 15px;font-weight: bold;line-height: 100%;">' + data[i].cutter + '</div>';
            html += '                  <div style="margin: 2px 0 2px 0">';
            html += '                      <img style="height: 0.8cm" id="barcode_card_' + data[i].id + '">';
            html += '                  </div>';
            html += '                  <span style=" line-height: 100%;font-size: 20px;font-weight: bold;">' + data[i].id + '</span>';
            html += '              </div>';
            html += '          </div>';
            if (i % 6 == 5 || i == (data.length - 1)) {
                html += '      </td>';
                html += '   </tr>';
            }
        }
        
        html += '</table>';

        $("#content_print").html(html);
        for (let i = 0; i < data.length; i++) {
            JsBarcode("#barcode_card_" + data[i].id, data[i].id, {
                format: "CODE39",
                lineColor: "#000",
                width: 2,
                height: 100,
                margin: 0,
                displayValue: false,
            });
        }
       
}
//Coupon_Book_A4, A5
function DrawCouponBook() {
    html = '';
    html += '<div class="report_count_book fw-bold">'
    html += '<table class="table table-bordered mb-0" style="table-layout: fixed; border-collapse: collapse;">'
    html += '   <tbody>'

    for (let i = 0; i < 5; i++) {
        if (i % 2 === 0) {
            html += '       <tr>'
            html += '           <td style="padding: 0">'
			html += '               <div style="display: flex;">'
		}
			html += '               <div style="padding:15px;border: 1px dashed #000;">'
            html += '               	<div style="width: 9.5cm;">'
            html += '               	    <div class="item_grid_header">'
            html += '               	        <div class=" text-start" style="width:100%">Số DKCB DVN.037192</div>'
            html += '               	        <div class=" text-start" style="width:100%">Tên sách: Quan niệm của C.Mác -Ph.</div>'
            html += '               	        <div style="display:flex">'
            html += '               	            <div class=" text-start" style="width:70%">Tập:...</div>'
            html += '               	            <div class=" text-start" style="width:30%">Giá:</div>'
            html += '               	        </div>'
            html += '               	    </div>'
            html += '               	    <table class="table" style="table-layout: fixed;">'
			html += '               	        <thead><tr style="height: 9mm;" class="fw-bold"><th>Ngày Mượn</th><th>Số thẻ ĐG</th><th>Chữ ký</th></tr></thead>'
           
            html += '               	        <tbody>'
			for(let j = 0; j < 14; j++){
				html += '               	        	<tr style="height: 9mm; border-bottom: 1px dashed #000;"><td></td><td></td><td></td></tr>'
			}
            html += '               	        </tbody>'
            html += '               	    </table>'
            html += '               	</div>'
			html += '                </div>'
            
        if (i % 2 === 1) {
			html += '                </div>'
            html += '           </td>'
			html += '       </tr>'
		}
    }
    html += '    </tbody>'
    html += '</table>'
    html += '</div>'
    return html;
}

function DrawCouponBookA5(data) {
    html = '';
    html += '<div class="report_count_book fw-bold">'
    html += '<table class="table table-bordered mb-0" style="table-layout: fixed; border-collapse: collapse;">'
    html += '   <tbody>'

    for (let i = 0; i < data.length; i++) {
        if (i % 2 === 0) {
            html += '       <tr>'
            html += '           <td style="padding: 0">'
			html += '               <div style="display: flex;page-break-inside: avoid; font-size:17px;">'
		}
			html += '               <div style="padding:15px;border: 1px dashed #000;">'
            html += '               	<div style="width: 8.5cm;">'
            html += '               	    <div class="item_grid_header">'
            html += '               	        <div class=" text-start" style="width:100%">Số ĐKCB: ' + data[i].id + '</div>'
            html += '               	        <div class=" text-start" style="width:100%">Tên sách: ' + shortenTitle(data[i].title, 30) + '</div>'
            html += '               	        <div style="display:flex">'
            html += '               	            <div class=" text-start" style="width:60%">Tập:...</div>'
            html += '               	            <div class=" text-start" style="width:40%">Giá: ' + data[i].price + '</div>'
            html += '               	        </div>'
            html += '               	    </div>'
            html += '               	    <table class="table" style="table-layout: fixed;">'
			html += '               	        <thead class="text-center"><tr style="height: 7mm;" class="fw-bold"><th>Ngày Mượn</th><th>Số thẻ ĐG</th><th>Chữ ký</th></tr></thead>'
           
            html += '               	        <tbody>'
			for(let j = 0; j < 10; j++){
				html += '               	        	<tr style="height: 8mm; border-bottom: 1px dashed #000;"><td></td><td></td><td></td></tr>'
			}
            html += '               	        </tbody>'
            html += '               	    </table>'
            html += '               	</div>'
			html += '                </div>'
            
        if (i % 2 === 1) {
			html += '                </div>'
            html += '           </td>'
			html += '       </tr>'
		}
    }
    html += '    </tbody>'
    html += '</table>'
    html += '</div>'
    return html;
}

//***********************Hàm phụ*******************************
//nhóm các result theo trường
const groupBy = (array, keys) => {
    // Nhóm các đối tượng theo các khóa chỉ định
    const grouped = array.reduce((result, item) => {
        // Tạo khóa từ các giá trị trường chỉ định
        const key = keys.map(k => item[k]).join('-');

        // Khởi tạo nhóm nếu chưa tồn tại
        if (!result[key]) {
            result[key] = {
                keys: {},
                values: []
            };
            keys.forEach(k => result[key].keys[k] = item[k]);
        }

        // Thêm phần tử hiện tại vào nhóm
        result[key].values.push(item);

        return result;
    }, {});

    // Chuyển đổi đối tượng grouped thành mảng các đối tượng kết quả
    return Object.entries(grouped).map(([key, group]) => ({
        keys: group.keys,
        values: group.values
    }));
};
function truncateString(str, num) {
    if (str.length <= num) {
        return str;
    }
    
    // Cắt chuỗi tới số ký tự đã chỉ định
    let truncated = str.slice(0, num);
    
    // Kiểm tra nếu ký tự cuối cùng không phải là dấu cách và không nằm giữa từ
    if (str[num] !== ' ' && str[num - 1] !== ' ') {
        truncated = truncated.slice(0, truncated.lastIndexOf(' '));
    }
    
    // Thêm dấu ...
    return truncated + '...';
}

function ChangeTotalReport(totals){
	var checkedRows = [];
	var stt = 1;
	$('input[name="checkboxdata"]:checked').each(function () {
		checkedRows.push($(this).attr("id"));
	});
	if (checkedRows.length > 0 && totals != undefined) {
		for (let i = 0; i < checkedRows.length; i++) {
			$('#s_' + checkedRows[i] + ' td:first').text(stt);
			stt++;
		}
		for (let i = 0; i < totals.length; i++) {
			if(totals[i].id == 'totalPrice') {
				var totalPrice = 0;
				for (let j = 0; j < checkedRows.length; j++) {
					var value = parseInt($('#s_' + checkedRows[j] + ' td:nth-child(' + totals[i].value + ')').text().replace(/[^\d]/g, ''));
					console.log($('#s_' + checkedRows[j] + ' td:nth-child(' + totals[i].value + ')').text());
					if (!isNaN(value)) {
						totalPrice += value;
					}
				}
				$('#totalPrice').text(totalPrice.toLocaleString().replace(/,/g, '.'));
				$('#totalPriceTxt').text(numberToWords(totalPrice));
			}
			if (totals[i].id == 'total') {
				 var total = checkedRows.length;
				$('#total').text(total.toLocaleString());
			}
			if (totals[i].id == 'totalBook') {
				var totalBook = 0;
				for (let j = 0; j < checkedRows.length; j++) {
					var value = parseFloat($('#s_' + checkedRows[j] + ' td:nth-child(' + totals[i].value + ')').text().replace(/,/g, '').trim());
					if (!isNaN(value)) {
						totalBook += value;
					}
				}
				$('#totalBook').text(totalBook.toLocaleString());
			}
		}
	}
}

function ChangeTotalReportV2(totals){
	var checkedRows = [];
	var stt = 1;
	$('input[name="checkboxdata"]:checked').each(function () {
		checkedRows.push($(this).attr("id"));
	});
	if (checkedRows.length > 0 && totals != undefined) {
		for (let i = 0; i < checkedRows.length; i++) {
			$('#s_' + checkedRows[i] + ' td:first').text(stt);
			stt++;
		}
		for (let i = 0; i < totals.length; i++) {
			if(totals[i].id == 'totalPrice') {
				var totalPrice = 0;
				for (let j = 0; j < checkedRows.length; j++) {
					var value = parseFloat($('#s_' + checkedRows[j] + ' td:nth-child(' + totals[i].value + ')').text().replace(/,/g, '').trim());
					if (!isNaN(value)) {
						totalPrice += value;
					}
				}
				$('#totalPrice').text(totalPrice.toLocaleString());
				$('#totalPriceTxt').text(numberToWords(totalPrice));
			}
			if (totals[i].id == 'total') {
				 var total = checkedRows.length;
				$('#total').text(total);
			}
			if (totals[i].id == 'totalBook') {
				var totalBook = 0;
				for (let j = 0; j < checkedRows.length; j++) {
					var value = parseFloat($('#s_' + checkedRows[j] + ' td:nth-child(' + totals[i].value + ')').text().replace(/,/g, '').trim());
					if (!isNaN(value)) {
						totalBook += value;
					}
				}
				$('#totalBook').text(totalBook);
			}
		}
	}
}

	
function ClientCheckAll(chkAllId, Groupname) {
    var checkboxes = document.getElementsByName(Groupname);
    var isCheckAll = document.getElementById(chkAllId).checked;
    var ids = [];
    for (var i = 0; i < checkboxes.length; i++) {
        checkboxes[i].checked = isCheckAll;
    }
    DisplayRowInTable();
}

function DisplayRowInTable() {
    var checkedRows = [];
    $('input[name="checkboxdata"]:checked').each(function () {
        checkedRows.push($(this).attr("id"));
    });

    if (checkedRows.length === 0) {
        $('#content_print tbody tr').show();
    } else {
        $('#content_print tbody tr').hide();
        // Hiển thị các dòng tương ứng với các checkbox được chọn
        checkedRows.forEach(function (row) {
            $('#content_print tbody #s_' + row).show();
        });
    }
}

function numberToWords(num) {
    const units = ['', 'nghìn', 'triệu', 'tỷ', 'nghìn tỷ'];
    const unitWords = ['không', 'một', 'hai', 'ba', 'bốn', 'năm', 'sáu', 'bảy', 'tám', 'chín'];

    const convertBelowThousand = (n) => {
        let str = '';
        const hundreds = Math.floor(n / 100);
        const tens = Math.floor((n % 100) / 10);
        const ones = n % 10;

        if (hundreds > 0) {
            str += unitWords[hundreds] + ' trăm ';
        }
        if (tens > 1) {
            str += unitWords[tens] + ' mươi ';
            if (ones > 0) {
                str += unitWords[ones];
            }
        } else if (tens === 1) {
            str += 'mười ';
            if (ones > 0) {
                str += unitWords[ones];
            }
        } else if (ones > 0) {
            str += unitWords[ones];
        }
        
        return str.trim();
    };

    if (num === 0) return 'không';

    let words = '';
    let unitIndex = 0;

    while (num > 0) {
        let part = num % 1000;
        if (part > 0) {
            words = convertBelowThousand(part) + ' ' + units[unitIndex] + ' ' + words;
        }
        num = Math.floor(num / 1000);
        unitIndex++;
    }

    return words.trim();
}

function truncate15Words(text, maxWords) {
	if(text != null && text != undefined){
		const words = text.split(' '); // Tách chuỗi thành mảng các từ
		if (words.length > maxWords) {
			return words.slice(0, maxWords).join(' ') + '...'; // Lấy 15 từ đầu và thêm "..."
		}
		return text; // Trả về chuỗi gốc nếu chuỗi không dài hơn 15 từ
	}else return text = '';
}

function removeVietnameseTones(str) {
    str = str.replace(/à|á|ạ|ả|ã|â|ầ|ấ|ậ|ẩ|ẫ|ă|ằ|ắ|ặ|ẳ|ẵ/g, "a");
    str = str.replace(/è|é|ẹ|ẻ|ẽ|ê|ề|ế|ệ|ể|ễ/g, "e");
    str = str.replace(/ì|í|ị|ỉ|ĩ/g, "i");
    str = str.replace(/ò|ó|ọ|ỏ|õ|ô|ồ|ố|ộ|ổ|ỗ|ơ|ờ|ớ|ợ|ở|ỡ/g, "o");
    str = str.replace(/ù|ú|ụ|ủ|ũ|ư|ừ|ứ|ự|ử|ữ/g, "u");
    str = str.replace(/ỳ|ý|ỵ|ỷ|ỹ/g, "y");
    str = str.replace(/đ/g, "d");
    str = str.replace(/À|Á|Ạ|Ả|Ã|Â|Ầ|Ấ|Ậ|Ẩ|Ẫ|Ă|Ằ|Ắ|Ặ|Ẳ|Ẵ/g, "A");
    str = str.replace(/È|É|Ẹ|Ẻ|Ẽ|Ê|Ề|Ế|Ệ|Ể|Ễ/g, "E");
    str = str.replace(/Ì|Í|Ị|Ỉ|Ĩ/g, "I");
    str = str.replace(/Ò|Ó|Ọ|Ỏ|Õ|Ô|Ồ|Ố|Ộ|Ổ|Ỗ|Ơ|Ờ|Ớ|Ợ|Ở|Ỡ/g, "O");
    str = str.replace(/Ù|Ú|Ụ|Ủ|Ũ|Ư|Ừ|Ứ|Ự|Ử|Ữ/g, "U");
    str = str.replace(/Ỳ|Ý|Ỵ|Ỷ|Ỹ/g, "Y");
    str = str.replace(/Đ/g, "D");
    str = str.replace(/\u0300|\u0301|\u0303|\u0309|\u0323/g, ""); // Huyền, sắc, ngã, hỏi, nặng
    str = str.replace(/\u02C6|\u0306|\u031B/g, ""); // Â, Ê, Ă, Ơ, Ư
    return str;
}
function shortenTitle(title, maxLength = 28) {
    if (title.length <= maxLength) {
        return title; // Nếu chuỗi ngắn hơn hoặc bằng maxLength, giữ nguyên
    }

    // Cắt chuỗi tới maxLength
    let truncated = title.slice(0, maxLength);

    // Kiểm tra nếu ký tự cuối cùng của truncated là một phần của từ
    if (title[maxLength] && title[maxLength] !== ' ' && truncated[maxLength - 1] !== ' ') {
        // Tìm vị trí khoảng trắng cuối cùng trong truncated
        let lastSpaceIndex = truncated.lastIndexOf(' ');
        if (lastSpaceIndex !== -1) {
            truncated = truncated.slice(0, lastSpaceIndex); // Cắt tới khoảng trắng cuối cùng
        }
    }

    return truncated;
}
            