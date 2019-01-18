module clubnet {
    export class ajaxForm {

        private _url: string;

        private _formId: string;
        private _triggers: HTMLElement[];

        /**
         *  Initialize a new instance of the class ajaxForm
         */
        constructor(public formId: string, public url: string) {
            this._formId = formId;
            this._url = url;
        }

        /**
         * Intialize all the event connection to enabled ajax form preload
         */
        public static initializeFormAjaxPreset(formId: string, url: string): void {
            var ajaxForm = new clubnet.ajaxForm(formId, url);

            let form = $("#" + ajaxForm._formId);

            if (form) {
                ajaxForm._triggers = form.find('input[data-ajax-trigger="true"]').toArray();

                ajaxForm._triggers.forEach((element) => {
                    var self = ajaxForm;
                    element.onchange = function () { self.updateFormAsync() };
                });
            }
        }

        /**
         * Setup the values from the data loaded
         */
        private updateFormValues(data: object) {
            let form = $("#" + this._formId);
            $.each(data, function (propName, value) {

                var input = form.find("input").filter(function () {
                    return $(this).attr('name').toLowerCase() == ("" + propName + "").toLowerCase();
                });

                if (input && input.length > 0 && !input.val()) {
                    input.val(value);
                }
            });
        }

        /** 
 *  Called each time a trigger input value changed
 */
        private updateFormAsync() {
            var missingValue = false;
            var ajaxObject = {};

            this._triggers.forEach((element) => {

                let value = $(element).val();
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
        }
    }
}