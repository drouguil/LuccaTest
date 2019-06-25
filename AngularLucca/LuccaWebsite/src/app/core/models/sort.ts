import { SortEnum } from './sort-enum';

export class Sort {

    /**
     * Constructor
     * @param name Name
     * @param value Enum value
     * @param icon Icon
     */

    constructor(
        public name: string,
        public value: SortEnum,
        public icon: string
    ) { }
}
