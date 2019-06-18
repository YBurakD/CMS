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
        
    };

    p._ckeditor = function () {
        if ($("#Body").length > 0) {
            CKEDITOR.replace('Body');
        }
    };

    window.trial.AppCategory = new AppCategory;
}(jQuery));