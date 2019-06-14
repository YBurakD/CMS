(function ($) {
    "use strict";

    var AppCategory = function () {
        var o = this;
        $(document).ready(function () {
            o.initialize();
        });
    };
    var p = AppCategory.prototype;

    p.initialize = function () {
        this._nestableList();
        this._ckeditor();
    };

    p._nestableList = function () {
        if (!$.isFunction($.fn.nestable)) {
            return;
        }
        if ($('.jsCategories').length > 0) {
            $('.jsCategories').nestable({
                maxDepth: 999999,
                callback: function (target, source) {
                    console.log(target);
                    console.log(source);
                }
            });
        }
    };

    p._ckeditor = function () {
        CKEDITOR.replace('Body');
    }

    window.trial.AppCategory = new AppCategory;
}(jQuery));