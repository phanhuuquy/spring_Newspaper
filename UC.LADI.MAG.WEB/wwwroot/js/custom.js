function openLoader() {
	$(document.body).addClass("loader-modal-open");
	$("#loaderModal").css("display", "flex");
}

function closeLoader() {
	$(document.body).removeClass("loader-modal-open");
	setTimeout(() => $("#loaderModal").hide(), 300);
}
$(document).ready(function () {
	var $wrapper = $('.main-wrapper');
	var $pageWrapper = $('.page-wrapper');
	var $slimScrolls = $('.slimscroll');

	$.fn.dataTable.moment('DD/MM/YYYY');

	var Sidemenu = function () {
		this.$menuItem = $('#sidebar-menu a');
	};

	function initJS() {
		var $this = Sidemenu;

		$('#sidebar-menu a').on('click', function (e) {
			if ($(this).parent().hasClass('submenu')) {
				e.preventDefault();
			}
			if (!$(this).hasClass('subdrop')) {
				$('ul', $(this).parents('ul:first')).hide(350);
				$('a', $(this).parents('ul:first')).removeClass('subdrop');
				$(this).next('ul').show(350);
				$(this).addClass('subdrop');
			} else if ($(this).hasClass('subdrop')) {
				$(this).removeClass('subdrop');
				$(this).next('ul').hide(350);
			}
		});

		let lis = $('#sidebar-menu ul li.submenu a.active').parents('li');
		$(lis.get().reverse()).each(function (index) {
			if (!$(this).children('a:first').hasClass("active")) {
				$(this).children('a:first').addClass('active').trigger('click');
			}
		});
		

		$(document).on('click', '#toggle_btn', function () {
			if ($('body').hasClass('mini-sidebar')) {
				$('body').removeClass('mini-sidebar');
				$('.subdrop + ul').show();
			} else {
				$('body').addClass('mini-sidebar');
				$('.subdrop + ul').hide();
			}
			return false;
		});
		$(document).on('mouseover', function (e) {
			e.stopPropagation();
			if ($('body').hasClass('mini-sidebar') && $('#toggle_btn').is(':visible')) {
				var targ = $(e.target).closest('.sidebar').length;
				if (targ) {
					$('body').addClass('expand-menu');
					$('.subdrop + ul').show();
				} else {
					$('body').removeClass('expand-menu');
					$('.subdrop + ul').hide();
				}
				return false;
			}
		});

		$('body').append('<div class="sidebar-overlay"></div>');
		$(document).on('click', '#mobile_btn', function () {
			$wrapper.toggleClass('slide-nav');
			$('.sidebar-overlay').toggleClass('opened');
			$('html').addClass('menu-opened');
			$('#task_window').removeClass('opened');
			return false;
		});

		$(".sidebar-overlay").on("click", function () {
			$('html').removeClass('menu-opened');
			$(this).removeClass('opened');
			$wrapper.removeClass('slide-nav');
			$('.sidebar-overlay').removeClass('opened');
			$('#task_window').removeClass('opened');
		});

		if ($slimScrolls.length > 0) {
			$slimScrolls.slimScroll({
				height: 'auto',
				width: '100%',
				position: 'right',
				size: '7px',
				color: '#ccc',
				wheelStep: 10,
				touchScrollStep: 100
			});
			var wHeight = $(window).height() - 60;
			$slimScrolls.height(wHeight);
			$('.sidebar .slimScrollDiv').height(wHeight);
			$(window).resize(function () {
				var rHeight = $(window).height() - 60;
				$slimScrolls.height(rHeight);
				$('.sidebar .slimScrollDiv').height(rHeight);
			});
		}

		var specificChildSubLinks = ["/Perm", "/Sid", "/Class"];

		$('a[href="/Perm"], a[href="/Sid"], a[href="/Class"]').on('click', function() {
			sessionStorage.setItem('clickedSpecificLink', $(this).attr('href'));
		})

		var savedScrollTop = sessionStorage.getItem('sidebarScrollTop');
		if (savedScrollTop) {
			var clickedSpecificLink = sessionStorage.getItem('clickedSpecificLink');
			if (specificChildSubLinks.includes(clickedSpecificLink)) {
				setTimeout(() => {
					$slimScrolls.slimScroll({ scrollTo: savedScrollTop + 'px' });
				}, 400);
			} else {
				$slimScrolls.slimScroll({ scrollTo: savedScrollTop + 'px' });
			}
		}

		$slimScrolls.on('scroll', function () {
			var currentScroll = $(this).scrollTop();
			sessionStorage.setItem('sidebarScrollTop', currentScroll);
		});

		$.extend($.fn.dataTable.defaults, {
			language: {
				"sEmptyTable": "Không có dữ liệu trong bảng",
				"sInfo": "Hiển thị từ _START_ đến _END_ của _TOTAL_ bản ghi",
				"sInfoEmpty": "Hiển thị từ 0 đến 0 của 0 bản ghi",
				"sInfoFiltered": "(Đã lọc từ tổng _MAX_ bản ghi)",
				"sInfoPostFix": "",
				"sInfoThousands": ",",
				"sLengthMenu": "Hiển thị _MENU_ bản ghi",
				"sLoadingRecords": "...",
				"sProcessing": "...",
				"sSearch": "Tìm kiếm:",
				"sZeroRecords": "Không tìm thấy kết quả",
				"oPaginate": {
					"sFirst": "<i class=\"fa fa-angle-double-left\"></i>",
					"sLast": "<i class=\"fa fa-angle-double-left\"></i>",
					"sNext": "<i class=\" fa fa-angle-right\"></i>",
					"sPrevious": "<i class=\"fa fa-angle-left\"></i>"
				},
				"oAria": {
					"sSortAscending": ": activate to sort column ascending",
					"sSortDescending": ": activate to sort column descending"
				}
			}
		});
		//
		if ($('.floating').length > 0) {
			$('.floating').on('focus blur', function (e) {
				$(this).parents('.form-focus').toggleClass('focused', (e.type === 'focus' || this.value.length > 0));
			}).trigger('blur');
		}

		//$.ajaxSetup({
		//	error: function (jqXHR, textStatus, errorThrown) {
		//		if (jqXHR.status === 401) {
		//			window.location.href = locationVal + "/Error/Error401";
		//		}
		//	}
		//});
	}
	
	initJS();
});
