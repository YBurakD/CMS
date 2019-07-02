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
        this._ContentAddToCategoryBtnClick();
        this._ContentCreateBtnClick();
        this._CategorySelectBtnClick();
        this._selectLang();
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
    p._ContentAddToCategoryBtnClick = function () {
        var buttons = document.getElementsByClassName("jsContentAddBtn"); //returns a nodelist
        for (let i = 0; i < buttons.length; i++) {
            buttons[i].addEventListener("click", function () {
                buttonsControl(this, AddContent);
            }, false);
        }
    };
    p._ContentCreateBtnClick = function () {
        var buttons = document.getElementsByClassName("jsContentCreateBtn"); //returns a nodelist
        for (let i = 0; i < buttons.length; i++) {
            buttons[i].addEventListener("click", function () {
                buttonsControl(this, AddContent);
            }, false);
        }
    };
    p._CategorySelectBtnClick = function () {
        var buttons = document.getElementsByClassName("jsContentSelectBtn"); //returns a nodelist
        for (let i = 0; i < buttons.length; i++) {
            buttons[i].addEventListener("click", function () {
                buttonsControl(this, ContentIndexUrl + "/Index")
            }, false);
        }
    };
    p._selectLang = function () {
        var radioButtons = document.getElementsByName("Language");
        for (let i = 0; i < radioButtons.length; i++) {
            radioButtons[i].addEventListener("click", function () {
                radioButtonsControl();
            }, false);
        }
    }
    function radioButtonsControl() {
        var $liItems = $(".dd-item");
        var RadioValue = $("input[name='Language']:checked").val();
        for (let i = 0; i < $liItems.length; i++) {
            if ($liItems[i].dataset.language != RadioValue)
                $liItems[i].style.display = 'none';
            else
                $liItems[i].style.display = '';
        }
    }
    function buttonsControl(button, url) {
        window.location.href = url + "/" + button.getAttribute("data-id");
    }

    window.trial.AppCategory = new AppCategory;
}(jQuery));