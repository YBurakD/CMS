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
        this._btnClick();
        this._selectLang();
    };

    p._nestableList = function () {
        if (!$.isFunction($.fn.nestable)) {
            return;
        }
        if ($('.jsCategories').length > 0) {
            $('.jsCategories').nestable({
                maxDepth: 999999,
                callback: function (target, source) {
                    var model = {};
                    var parent = source.parent().closest("li");
                    var sourceId = source.data("id");
                    var parentId = parent.data("id");

                    // Sıralama
                    var idList = [];
                    var categories = $(".jsCategories li[data-id]");
                    $.each(categories, function (i, item) {
                        var $this = $(this);
                        var id = $this.data("id");
                        idList.push(id);
                    });
                    model.SourceId = sourceId;
                    model.ParentId = parentId;
                    model.SortList = idList;

                    $.ajax({
                        type: "POST",
                        url: sortUrl,
                        dataType: "json",
                        data: model,
                        success: function (response) {
                            console.log(response);
                        }
                    });

                }
            });
        }
        $('.dd a').on('mousedown', function (event) { event.preventDefault(); return false; });
    };

    p._ckeditor = function () {
        if ($("#Body").length > 0) {
            CKEDITOR.replace('Body');
        }
    };
    p._btnClick = function () {
        var buttons = document.getElementsByClassName("jsUpdateBtn"); //returns a nodelist
        for (let i = 0; i < buttons.length; i++) {
            buttons[i].addEventListener("click", function () {
                buttonsControl(this, i);
            }, false);
        }

        function buttonsControl(button, i) {
            window.location.href = updateUrl + "/" + button.getAttribute("data-id");
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
    window.trial.AppCategory = new AppCategory;
}(jQuery));