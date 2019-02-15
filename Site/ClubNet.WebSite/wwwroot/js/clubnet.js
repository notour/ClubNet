var clubnet;
(function (clubnet) {
    var ajaxForm = /** @class */ (function () {
        /**
         *  Initialize a new instance of the class ajaxForm
         */
        function ajaxForm(formId, url) {
            this.formId = formId;
            this.url = url;
            this._formId = formId;
            this._url = url;
        }
        /**
         * Intialize all the event connection to enabled ajax form preload
         */
        ajaxForm.initializeFormAjaxPreset = function (formId, url) {
            var ajaxForm = new clubnet.ajaxForm(formId, url);
            var form = $("#" + ajaxForm._formId);
            if (form) {
                ajaxForm._triggers = form.find('input[data-ajax-trigger="true"]').toArray();
                ajaxForm._triggers.forEach(function (element) {
                    var self = ajaxForm;
                    element.onchange = function () { self.updateFormAsync(); };
                });
            }
        };
        /**
         * Setup the values from the data loaded
         */
        ajaxForm.prototype.updateFormValues = function (data) {
            var form = $("#" + this._formId);
            $.each(data, function (propName, value) {
                var input = form.find("input").filter(function () {
                    return $(this).attr('name').toLowerCase() == ("" + propName + "").toLowerCase();
                });
                if (input && input.length > 0 && !input.val()) {
                    input.val(value);
                }
            });
        };
        /**
 *  Called each time a trigger input value changed
 */
        ajaxForm.prototype.updateFormAsync = function () {
            var missingValue = false;
            var ajaxObject = {};
            this._triggers.forEach(function (element) {
                var value = $(element).val();
                if (!value || $(element).is(":focus")) {
                    missingValue = true;
                    return;
                }
                ajaxObject[$(element).attr("name")] = $(element).val();
            });
            if (!missingValue) {
                $.ajax({
                    url: this._url,
                    type: "POST",
                    data: ajaxObject,
                    dataType: "json",
                    cache: false,
                    success: this.updateFormValues,
                    error: function (error) {
                        alert("Error : " + JSON.stringify(error));
                    },
                    context: this
                });
            }
        };
        return ajaxForm;
    }());
    clubnet.ajaxForm = ajaxForm;
})(clubnet || (clubnet = {}));
var clubnet;
(function (clubnet) {
    var validationForm = /** @class */ (function () {
        function validationForm() {
        }
        /**
         * Add the bootstrap visual information on success or error
         * @param inputElement second input of the unobtrusive Validation system. input element
         * @param isSuccess define if the validation is a sucess or not
         */
        validationForm.SetFeedback = function (inputElement, isSuccess) {
            var parentContainer = inputElement.parents('div.has-feedback');
            if (isSuccess == true) {
                parentContainer.addClass("has-success");
                parentContainer.removeClass("has-error");
            }
            else {
                parentContainer.removeClass("has-success");
                parentContainer.addClass("has-error");
            }
            var feedbackChild = parentContainer.find('span.form-control-feedback');
            if (feedbackChild.length == 0) {
                parentContainer.append("<span class='glyphicon form-control-feedback' aria-hidden='true'></span>");
            }
            var feedbackChild = parentContainer.find('span.form-control-feedback');
            if (isSuccess == true) {
                feedbackChild.addClass("glyphicon-ok");
                feedbackChild.removeClass("glyphicon-remove");
            }
            else {
                feedbackChild.removeClass("glyphicon-ok");
                feedbackChild.addClass("glyphicon-remove");
            }
        };
        return validationForm;
    }());
    clubnet.validationForm = validationForm;
})(clubnet || (clubnet = {}));
var clubnet;
(function (clubnet) {
    var selectorHelper = /** @class */ (function () {
        function selectorHelper() {
        }
        /**
         * Call to apply a tag selection mode
         */
        selectorHelper.tagSelection = function (select, target, cleanUpMethod) {
            if (cleanUpMethod === void 0) { cleanUpMethod = null; }
            var option = select.options[select.selectedIndex];
            if (option.value == "0" || option.value === undefined)
                return;
            var inputTarget = $(target).attr("data-input-target").toString();
            var choices = $(target).find('input');
            for (var i = 0; i < choices.length; i++)
                if ($(choices[i]).val() == option.value)
                    return;
            var li = document.createElement('li');
            var input = document.createElement('input');
            var text = document.createTextNode(option.firstChild.nodeValue);
            input.type = 'hidden';
            input.name = inputTarget + '[]';
            input.value = option.value;
            li.appendChild(input);
            li.appendChild(text);
            var removeMth = 'this.parentNode.removeChild(this);';
            if (cleanUpMethod != null && cleanUpMethod.length > 0)
                removeMth += cleanUpMethod + "();";
            li.setAttribute('onclick', removeMth);
            $(target).append(li);
        };
        return selectorHelper;
    }());
    clubnet.selectorHelper = selectorHelper;
})(clubnet || (clubnet = {}));
var clubnet;
(function (clubnet) {
    var conditions = /** @class */ (function () {
        function conditions() {
        }
        /**
         *
         */
        conditions.if = function (items, equalityValue, target, classIf, classThen) {
            if ($(items).filter(function (indx, eq) { return $(eq).val() == equalityValue; }).length > 0) {
                $(target).removeClass(classThen);
                $(target).addClass(classIf);
            }
            else {
                $(target).addClass(classThen);
                $(target).removeClass(classIf);
            }
        };
        return conditions;
    }());
    clubnet.conditions = conditions;
})(clubnet || (clubnet = {}));
//# sourceMappingURL=clubnet.js.map