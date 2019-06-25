export class Activity {

    /**
     * Constructor
     * @param description Description
     * @param destinationId Id of the destination with this activity
     * @param id Id
     * @param name Name
     * @param thumbail Image of the activity
     */

    constructor(
        public description: string,
        public destinationId: string,
        public id: string,
        public name: string,
        public thumbail: string
    ) { }
}
