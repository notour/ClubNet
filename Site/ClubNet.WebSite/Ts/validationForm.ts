module clubnet {

    export class validationForm {

        /**
         * Add the bootstrap visual information on success or error
         * @param inputElement second input of the unobtrusive Validation system. input element
         * @param isSuccess define if the validation is a sucess or not
         */
        public static SetFeedback(inputElement, isSuccess: boolean) {

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
        }
    }
}