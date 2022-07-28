(function (window, undefined) {
    'use strict';

    /*
    NOTE:
    ------
    PLACE HERE YOUR OWN JAVASCRIPT CODE IF NEEDED
    WE WILL RELEASE FUTURE UPDATES SO IN ORDER TO NOT OVERWRITE YOUR JAVASCRIPT CODE PLEASE CONSIDER WRITING YOUR SCRIPT HERE.  */

    (function ($) {
        $.fn.ajaxislem = function (Url, Data) {
            var result;
            $.ajax({
                type: 'POST',
                url: Url,
                data: Data,
                cache: false,
                async: false,
                global: false,
                success: function (data, textStatus, jqXHR) {
                    result = data;
                },
                error: function (xhr, textStatus, errorThrown) {
                    var Data = { Durum: false, Mesaj: xhr.responseText, Id: 0, Html: "" };
                    result = Data;
                },
                fail: function () {
                    var Data = { Durum: false, Mesaj: xhr.responseText, Id: 0, Html: "" };
                    result = Data;
                }
            });
            return result;
        }
    })(jQuery);

})(window);

function Alert(message, type) {
    $.bootstrapGrowl(message, {
        type: type ? "success" : "danger",
        ele: 'body', // which element to append to
        offset: { from: 'top', amount: 20 }, // 'top', or 'bottom'
        align: 'right', // ('left', 'right', or 'center')
        width: 250, // (integer, or 'auto')
        delay: 4000, // Time while the message will be displayed. It's not equivalent to the *demo* timeOut!
        allow_dismiss: true, // If true then will display a cross to close the popup.
        stackup_spacing: 10 // spacing between consecutively stacked growls.
    });
}

function convertToJavaScriptDate(value) {
    var date = new Date(value),
        yr = date.getFullYear(),
        month = (date.getMonth() + 1) < 10 ? '0' + (date.getMonth() + 1) : (date.getMonth() + 1),
        day = date.getDate() < 10 ? '0' + date.getDate() : date.getDate(),
        newDate = day + '/' + month + '/' + yr;
    return newDate;
}

$(".student-remove").on('click', function () {
    var id = $(this).data('student-id');
    var result = $.fn.ajaxislem("/Student/Delete", { id: id });
    if (result.success) {
        $(this).parents('tr').fadeOut(300, function () { $(this).remove(); });
    }
    Alert(result.message, result.success);
})

$(".student-detail").on('click', function () {
    var id = $(this).data('student-id');
    var result = $.fn.ajaxislem("/Student/Detail", { id: id });
    if (result.success) {
        var modal = $("#detail-modal");
        $(modal).find('.student-no').html(result.data.studentNo);
        $(modal).find('.student-fullname').html(result.data.firstName + ' ' + result.data.lastName);
        $(modal).find('.student-birthdate').html(convertToJavaScriptDate(result.data.birthDate));
        $(modal).modal('show');
    } else {
        Alert(result.message, result.success);
    }
})


$(".student-course").on('click', function () {
    var id = $(this).data('student-id');
    var result = $.fn.ajaxislem("/Student/GetCourses", { id: id });
    if (result.success) {
        var html = '';
        var modal = $("#courses-modal");
        $(result.data).each(function (index, item) {
            var isCheck = item.selected ? "checked" : "";
            var formGroup = '<div class="form-group"> <input ' + isCheck + ' type="checkbox" class="form-control" data-student="' + id + '" data-id="' + item.courseCode + '"/> ' + item.courseName + ' </div>';
            html += formGroup;
        })
        $(modal).find('.modal-body').html(html);
        $(modal).modal('show');
    } else {
        Alert(result.message, result.success);
    }
})

$('body').on('change', 'input[type="checkbox"]', function () {
    var studentId = $(this).data('student');
    var courseId = $(this).data('id');
    var result = $.fn.ajaxislem("/Student/AddCourse", { studentId: studentId, code: courseId });
    Alert(result.message, result.success);
});

$(".course-remove").on('click', function () {
    var id = $(this).data('course-id');
    var result = $.fn.ajaxislem("/Course/Delete", { id: id });
    if (result.success) {
        $(this).parents('tr').fadeOut(300, function () { $(this).remove(); });
    }
    Alert(result.message, result.success);
})
