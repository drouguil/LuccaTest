export class Destination {

    /**
     * Constructor
     * @param bg Background
     * @param country Country
     * @param description Description
     * @param gallery Pictures of the destination
     * @param id Id
     * @param name Name
     * @param priceRange Range of price to go at this destination
     * @param rating Rating of this destination
     * @param tags Keyworks of the destination
     * @param thumbail Picture
     */

    constructor(
        public bg: string,
        public country: string,
        public description: string,
        public gallery: string[],
        public id: string,
        public name: string,
        public priceRange: string,
        public rating: string,
        public tags: string[],
        public thumbail: string
    ) { }
}
