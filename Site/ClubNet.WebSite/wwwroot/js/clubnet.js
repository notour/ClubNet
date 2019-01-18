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
//# sourceMappingURL=clubnet.js.map