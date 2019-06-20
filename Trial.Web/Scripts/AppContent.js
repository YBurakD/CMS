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
        this._ckeditor();
        this._ContentUpdateBtnClick();
        this._CategoryUpdateBtnClick();
    };

    p._ckeditor = function () {
        if ($("#Body").length > 0) {
            CKEDITOR.replace('Body');
        }
    };
    p._ContentUpdateBtnClick = function () {
        var buttons = document.getElementsByClassName("jsContentUpdateBtn"); //returns a nodelist
        for (let i = 0; i < buttons.length; i++) {
            buttons[i].addEventListener("click", function () {
                buttonsControl(this, ContentUpdateUrl);
            }, false);
        }

    };
    p._CategoryUpdateBtnClick = function () {
        var buttons = document.getElementsByClassName("jsContentAddBtn"); //returns a nodelist
        for (let i = 0; i < buttons.length; i++) {
            buttons[i].addEventListener("click", function () {
                buttonsControl(this, AddContentToCategory);
            }, false);
        }
    };

    function buttonsControl(button, url) {
            window.location.href = url + "/" + button.getAttribute("data-id");
        }
    window.trial.AppCategory = new AppCategory;
}(jQuery));