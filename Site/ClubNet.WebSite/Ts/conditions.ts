module clubnet {
    export class conditions {

        /**
         * 
         */
        public static if(items, equalityValue, target, classIf: string, classThen: string): void
        {
            if ($(items).filter(function (indx, eq) { return $(eq).val() == equalityValue }).length > 0) {
                $(target).removeClass(classThen);
                $(target).addClass(classIf);
            }
            else {
                $(target).addClass(classThen);
                $(target).removeClass(classIf);
            }
        }
    }
}