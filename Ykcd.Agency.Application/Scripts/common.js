$(document).ready(function () {
    $.validator.setDefaults({
        highlight: function (element) {
            $(element).closest('.form-group').addClass('has-error');
        },
        unhighlight: function (element) {
            $(element).closest('.form-group').removeClass('has-error');
        },
        errorElement: 'span',
        errorClass: 'help-block',
        errorPlacement: function (error, element) {
            if (element.parent('.input-group').length) {
                error.insertAfter(element.parent());
            } else {
                error.insertAfter(element);
            }
        }

    });
    $.validator.messages.required = '';

    $('form:first').validate();

    $('.select2').select2();

    $('.datepicker').datepicker({ language: "vi", format: 'dd/mm/yyyy', autoclose: true, todayHighlight: true });

    $(".datepicker").click(function () {
        $(this).datepicker('show');
    });

    $("[id*='chkItemId']").click(function () {
        AddRemoveValueToList(this);
    });

    $("[data-target='.my-modal-lg']").click(function () {
        if ($(this).attr("data-link") !== '') {
            ShowPopUp($(this).attr("data-link"));
        }
    });

    //Fix lỗi validate ngày tháng trên chrome
    jQuery.validator.methods.date = function (value, element) {
        var isChrome = /Chrome/.test(navigator.userAgent) && /Google Inc/.test(navigator.vendor);
        if (isChrome) {
            var d = new Date();
            return this.optional(element) || !/Invalid|NaN/.test(new Date(d.toLocaleDateString(value)));
        } else {
            return this.optional(element) || !/Invalid|NaN/.test(new Date(value));
        }
    };

    var groups = {};
    $("select option[data-category]").each(function () {
        groups[$.trim($(this).attr("data-category"))] = true;
    });
    $.each(groups, function (c) {
        $("select option[data-category='" + c + "']").wrapAll('<optgroup label="' + c + '">');
    });

    $(document).on('show.bs.modal', '.modal', function (event) {
        var zIndex = 1040 + (10 * $('.modal:visible').length);
        $(this).css('z-index', zIndex);
        setTimeout(function () {
            $('.modal-backdrop').not('.modal-stack').css('z-index', zIndex - 1).addClass('modal-stack');
        }, 0);
    });
});

$("form:first").validate();

function validateForm() {
    $('form:first').validate({
        meta: "validate"
    });
    return $('form:first').valid();
}

function validatePopUpForm() {
    $('#popup').validate({ meta: "validate" });
    return $('#popup').valid();
}

function AddRemoveValueToList(checkbox) {
    var selectedIds = $("[id='selectedIds']").val();
    var newValueList = "";

    if (checkbox.checked) {
        if (!CheckValueIsExistInList(checkbox.value)) {
            if (selectedIds == null || selectedIds === "") {
                newValueList = checkbox.value;
            } else {
                newValueList = selectedIds + "," + checkbox.value;
            }
        }
    } else {
        var arr = selectedIds.split(",");

        for (var i = 0; i < arr.length; i++) {
            if (arr[i] !== checkbox.value) {
                newValueList = selectedIds + "," + checkbox.value;
            }
        }
    }

    $("[id='selectedIds']").val(newValueList);
};

function CheckValueIsExistInList(value) {
    var selectedIds = $("[id='selectedIds']").val();

    if (selectedIds != null) {
        var arr = selectedIds.split(",");

        for (var i = 0; i < arr.length; i++) {
            if (arr[i] === value) {
                return true;
            }
        }
    }

    return false;
}

function ShowDeleteConfirm() {
    var selectIds = $("[id='selectedIds']").val();
    if (!selectIds) {
        alert("Yêu cầu chọn ít nhất 1 dòng để thực hiện xóa!");
        return false;
    } else {
        if (confirm("Bạn có muốn xóa những dòng được chọn?")) {
            return true;
        }
    }
    return false;
}

function ShowConfirmMessage(message) {
    if (confirm(message)) {
        return true;
    }
    return false;
}

function ShowPopUp(url) {
    $(".modal-body-content").empty();

    $.post(url, '', function (data) {
        $(".my-modal-body-content").prepend(data);
        $('.datepicker').datepicker({ language: "vi", format: 'dd/mm/yyyy', autoclose: true });

        $(".datepicker").click(function () {
            $(this).datepicker('show');
        });
        $('.select2').select2();

        var groups = {};
        $("select option[data-category]").each(function () {
            groups[$.trim($(this).attr("data-category"))] = true;
        });
        $.each(groups, function (c) {
            $("select option[data-category='" + c + "']").wrapAll('<optgroup label="' + c + '">');
        });
    });
}

function CheckHaveFile() {
    if (validateForm()) {
        var validFields = $('input[type="file"]').map(function () {
            if ($(this).val() !== "")
                return $(this);
        }).get();

        if (validFields.length) {
            return true;
        }
        else {
            $('#thong-bao-chua-chon-file').modal('show');
            return false;
        }
    }
    return false;
}