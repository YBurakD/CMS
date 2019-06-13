(function (namespace, $) {
    "use strict";

    var AppValidate = function () {
        var o = this;
        $(document).ready(function () {
            o.initialize();
        });

    };
    var p = AppValidate.prototype;

    p.initialize = function () {
        this._initValidation();
    };

    p._initValidation = function () {

        $(document.body).on("keydown", ".numeric", function (e) {
            return p._checkNumeric(e);
        });

        $(document.body).on("keydown", ".alpha", function (e) {
            return p._isAlphaNumeric(e);
        });

        $(document.body).on("click", "input[type='submit']", function (e) {
            if (!p._validateForm($(this).closest("form"))) {
                e.preventDefault();
            }
        });

        $(document.body).on("change", ".changeValidate", function (e) {
            $.each($(".validateCustom"), function (i, item) {
                var $this = $(item);
                if ($this.hasClass("false")) {
                    $this.addClass("true");
                    $this.removeClass("false");
                } else {
                    $this.addClass("false");
                    $this.removeClass("true");
                }
            });
        });

        $(document.body).on("click", ".addImageValidate", function (e) {
            var $this = $(this);
            $this.prev().addClass("imageValidate");
            $this.prev().removeClass("false");
        });

        $(document.body).on("click", ".imageValidate", function (e) {
            $(".imageValidateMessage").text("");
        });

        $(document.body).on("change", ".imageValidate", function (e) {
            var $this = $(this);
            var btn = $this.next();
            btn.removeClass("btn-success");
            btn.addClass("btn-primary");
            var $files = $this[0].files;
            var imageHeight = $this.attr("data-maxHeight");
            var imageWidth = $this.attr("data-maxWidth");
            $(".imageValidateMessage").text("");
            if (imageHeight !== undefined && imageWidth !== undefined) {
                var iWidth = parseInt(imageWidth);
                var iHeight = parseInt(imageHeight);
                $.each($files, function (i, item) {
                    var img = new Image();
                    img.src = window.URL.createObjectURL(item);
                    img.onload = function () {
                        var width = img.naturalWidth;
                        var height = img.naturalHeight;
                        window.URL.revokeObjectURL(img.src);
                        if (iHeight !== height || iWidth !== width) {
                            var fileArea = $(".jsFileArea").html();
                            $(".jsFileArea").html(fileArea);
                            $(".imageValidateMessage").text($this.attr("data-val-required"));
                        } else {
                            btn.addClass("btn-success");
                            btn.removeClass("btn-primary");
                        }
                    };
                });
            }
        });
    };


    p._validateForm = function (parent) {
        try {
            var valid = true;
            $("span[data-valmsg-for]").text("");
            var inputs = parent.find("input:not(.validateCustom.false,.select2-input)");
            $.each(inputs, function (i, item) {
                var $this = $(this);
                var type = $this.attr("type");
                if ($this.attr("id") !== undefined) {
                    var id = $this.attr("id").replace("_", ".");
                    var errorMessage = parent.find("span[data-valmsg-for='" + id + "']");
                    var val = $this.val();
                    var minLength = $this.attr("data-val-length-min");
                    var maxLength = $this.attr("data-val-length-max");
                    var lengthMessage = $this.attr("data-val-length");
                    var emptyMessage = $this.attr("data-val-required");
                    var emailMessage = $this.attr("data-val-email");
                    if (type === "text" || type === "password" || type === "email" || type === "number") {
                        if (val.length === 0) {
                            errorMessage.text(emptyMessage);
                            valid = false;
                        } else {
                            if (type === "email" && !p._checkMail(val)) {
                                errorMessage.text(emailMessage);
                                valid = false;
                            }
                            if (minLength !== undefined) {
                                if (val.length < minLength) {
                                    errorMessage.text(lengthMessage);
                                    valid = false;
                                }
                            }
                            if (maxLength !== undefined) {
                                if (val.length > maxLength) {
                                    errorMessage.text(lengthMessage);
                                    valid = false;
                                }
                            }
                        }
                    } else if (type === "file") {
                        var fileLength = $this[0].files.length;
                        if (fileLength <= 0) {
                            errorMessage.text(emptyMessage);
                            valid = false;
                        }
                    }
                }
            });

            var select2List = parent.find(".select2-requried");
            $.each(select2List, function (i, item) {
                var $this = $(this);
                var choices = $this.find("ul.select2-choices li.select2-search-choice").length;
                var $validText = $this.find(".field-validation-valid");
                $validText.text("");
                if (!choices) {
                    valid = false;
                    $validText.text($this.attr("data-required-message"));
                }
            });

            var pass = parent.find(".jsPassword:not(.validateCustom.false)");
            var rePass = parent.find(".jsRePassword:not(.validateCustom.false)");
            if (pass.length > 0 && rePass.length > 0) {
                if (pass.val() !== rePass.val()) {
                    $(".jsPassError").text(rePass.attr("datapassworderror"))
                    valid = false;
                }
            }

            var textareas = parent.find("textarea[data-val='true']:not(.validateCustom.false)");
            $.each(textareas, function (i, item) {
                var $this = $(this);
                if ($this.attr("id") !== undefined) {
                    var id = $this.attr("id").replace("_", ".");
                    var errorMessage = parent.find("span[data-valmsg-for='" + id + "']");
                    var val = $this.val();
                    var minLength = $this.attr("data-val-length-min");
                    var maxLength = $this.attr("data-val-length-max");
                    var lengthMessage = $this.attr("data-val-length");
                    var emptyMessage = $this.attr("data-val-required");
                    if (val.length === 0) {
                        errorMessage.text(emptyMessage);
                        valid = false;
                    } else {
                        if (minLength !== undefined) {
                            if (val.length < minLength) {
                                errorMessage.text(lengthMessage);
                                valid = false;
                            }
                        }
                        if (maxLength !== undefined) {
                            if (val.length > maxLength) {
                                errorMessage.text(lengthMessage);
                                valid = false;
                            }
                        }
                    }
                }
            });

            var selectBoxes = parent.find("select.validate");
            $.each(selectBoxes, function (i, item) {
                var $this = $(this);
                if ($this.attr("id") !== undefined) {
                    var id = $this.attr("id").replace("_", ".");
                    var errorMessage = parent.find("span[data-valmsg-for='" + id + "']");
                    var val = $this.val();
                    var emptyMessage = $this.attr("data-val-required");
                    if (val.length === 0 && emptyMessage !== "") {
                        errorMessage.text(emptyMessage);
                        valid = false;
                    }
                }
            });

            return valid;
        } catch (ex) {
            alert(ex);
            return false;
        }
    };

    p._checkMail = function (email) {
        var regex = /^([a-zA-Z0-9_.+-])+\@(([a-zA-Z0-9-])+\.)+([a-zA-Z0-9]{2,4})+$/;
        return regex.test(email);
    };

    p._checkNumeric = function (key) {
        var c = key.keyCode;
        return ((c > 47 && c < 58)) || (c > 95 && c < 106) || (c === 8 || c === 9 || c === 17 || c === 45 || c === 46) || (c > 34 && c < 41) || (key.ctrlKey === true && (c === 67 || c === 86));
    };


    p._isAlphaNumeric = function (key) {
        var c = key.keyCode;
        return (c < 47 || c > 58) && (c < 95 || c > 106);
    };

    window.socialplus.AppValidate = new AppValidate;
}(this.socialplus, jQuery));
