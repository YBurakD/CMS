(function (namespace, $) {
    "use strict";

    var AppForm = function () {
        var o = this;
        $(document).ready(function () {
            o.initialize();
        });

    };
    var p = AppForm.prototype;

    p.initialize = function () {
        // Init events	
        this._initRadioAndCheckbox();
        this._initFloatingLabels();
        this._initInputMask();
        this._initSelect2();
    };

    p._initSelect2 = function () {
        if (!$.isFunction($.fn.select2)) {
            return;
        }
        $(".select2-list").select2({
            "closeOnSelect": false,
            allowClear: true,
            formatNoMatches: function () {
                return itemNotFound;
            }
        });
    };


    p._initInputMask = function () {
        if (!$.isFunction($.fn.inputmask)) {
            return;
        }
        $(".jsPhoneMask").inputmask("0(999) 999 99 99");
    };

    p._initRadioAndCheckbox = function () {
        $('.checkbox-styled input, .radio-styled input').each(function () {
            if ($(this).next('span').length === 0) {
                $(this).after('<span></span>');
            }
        });
    };

    p._initFloatingLabels = function () {
        var o = this;

        $('.floating-label .form-control').on('keyup change', function (e) {
            var input = $(e.currentTarget);

            if ($.trim(input.val()) !== '') {
                input.addClass('dirty').removeClass('static');
            } else {
                input.removeClass('dirty').removeClass('static');
            }
        });

        $('.floating-label .form-control').each(function () {
            var input = $(this);

            if ($.trim(input.val()) !== '') {
                input.addClass('static').addClass('dirty');
            }
        });

        $('.form-horizontal .form-control').each(function () {
            $(this).after('<div class="form-control-line"></div>');
        });
    };

    window.socialplus.AppForm = new AppForm;
}(this.socialplus, jQuery));
