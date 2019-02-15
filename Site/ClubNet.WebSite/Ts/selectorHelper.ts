module clubnet {
    export class selectorHelper {

        /**
         * Call to apply a tag selection mode
         */
        public static tagSelection(select: HTMLSelectElement, target: any, cleanUpMethod: string = null): void {
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
        }
    }
}