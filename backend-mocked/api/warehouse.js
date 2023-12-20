/**
 * Module to manage the tenant and its users
 */

const PLACE_TYPE_SHELF=5;

module.exports = {
    /**
     * Function that gets all warehouse places and adds attributes to be painted with unity.
     * Adds position (x, y, z), rotation (x, y, z) and scale (x, y, z) to each place.
     * Assumptions:
     * - the position is the center of the object
     * - the rotation is set to 0,0,0
     * - the scale is the size of the object in milimeters 
     * 
     * | y
     * |   / z
     * |  /
     * | /
     * |/__________ x
     * 
     * @param {*} warehouse : list of places with an initial point (x, y, z) like (1, 0, 0).The point indicates order, not absolute position.
     *                        For example, if the first place is (1, 0, 0), the second place is (2, 0, 0), this tells us that the second place is
     *                        at the right of the first place. If the third place is (1, 1, 0), this tells us that the third place is up the first place.
     * @param {*} placetypes : list of types of places (organization, buffer, dock, ...)
     * @param {*} placesubtypes : list of subtypes that defines the width, height and depth of the place in milimeters
     * @param {*} coordinateMultiplier : multiplies the place coordinate to convert it to milimeters
     */
    transformWarehouse4unity: function (warehouse, placetypes, placesubtypes, coordinateMultiplier) {
        // Step 1: Initialize position, rotation and scale of each place
        warehouse.forEach(place => {
            place.position = {
                x: 0,
                y: 0,
                z: 0
            };
            place.rotation = {
                x: 0,
                y: 0,
                z: 0
            };
            let placesubtype = placesubtypes.find(placesubtype => placesubtype.id === place.subtypeId);
            if (placesubtype === undefined) {
                // Organization can have a subtypeId = null
                placesubtype = {
                    x: 0,
                    y: 0,
                    z: 0
                }
            }
            place.scale = {
                x: placesubtype.x / 2,
                y: placesubtype.y / 2,
                z: placesubtype.z / 2
            }
        });

        // Step 2: Calculate width, height and depth of each place in milimeters
        // 2.1: Get all the places with "Shelf" type
        // 2.2: Foreach shelf place, get all the locations that are in the shelf
        // 2.3: Foreach location, recalculate the place pint from the last place and
        //      set the width, height and depth of the location
        let shelfPlaces = warehouse.filter(place => place.typeId === PLACE_TYPE_SHELF);

        shelfPlaces.forEach(shelfPlace => {
            let placesInShelf = warehouse.filter(place => place.parentId === shelfPlace.id);
            // Sort places by x, y, z
            placesInShelf = placesInShelf.sort((a, b) => {
                if (a.x > b.x) return 1;
                if (a.x < b.x) return -1;
                if (a.y > b.y) return 1;
                if (a.y < b.y) return -1;
                if (a.z > b.z) return 1;
                if (a.z < b.z) return -1;
                return 0;
            });
            
            // Foreach location, calculate the place piosition
            placesInShelf.forEach(place => {
                place.position = getPositionCoordinates(shelfPlace, placesInShelf, place);
                //console.log(`place.position ${place.position.x},${place.position.y},${place.position.z}`);
            });
        });

        return warehouse;
    },

    /**
     * Function that gets all warehouse places and extracts column racks from it.
     * Assumptions:
     * - the position is the center of the object in unity coordinates (each unity is 1 meter)
     * - the rotation is set to 0,0,0
     * - the scale is the size of the object in meters (each unity is 1 meter)
     * 
     * | y
     * |   / z
     * |  /
     * | /
     * |/__________ x
     * 
     * @param {*} warehouse : list of places with an initial point (x, y, z) like (1, 0, 0).The point indicates order, not absolute position.
     *                        For example, if the first place is (1, 0, 0), the second place is (2, 0, 0), this tells us that the second place is
     *                        at the right of the first place. If the third place is (1, 1, 0), this tells us that the third place is up the first place.
     * @param {*} placetypes : list of types of places (organization, buffer, dock, ...)
     * @param {*} placesubtypes : list of subtypes that defines the width, height and depth of the place in milimeters
     */
    extractShelfsFromWarehouse: function(warehouse, placetypes, placesubtypes) {
        return [];
    }
};

/**
 * Function that calculates the start position (newPlace.position) of the 
 * newPlace based on the already calculated places of the shelf
 * The calculation goes from left to rigth and from bottom to up
 * Assumptions: 
 * - shelfPlaces must be ordered on x, y, z
 * - shelfPlaces must have the scale object set
 * @param {*} shelf 
 * @param {*} shelfPlaces 
 * @param {*} place 
 */
function getPositionCoordinates(shelf, shelfPlaces, place) {
    let newPosition = { x: 0, y: 0, z: 0 };

    //console.log(`Place coordinates ${place.x},${place.y},${place.z}`);

    // Find the x coordinate: substract 1 to newPlace x and find the place with this x
    let foundPlaceX = shelfPlaces.find(p => p.x === place.x - 1 && p.y === place.y && p.z === place.z);
    //console.log(foundPlaceX);
    newPosition.x = (foundPlaceX && foundPlaceX.position ? foundPlaceX.position.x + foundPlaceX.scale.x + place.scale.x : shelf.x + place.scale.x);

    // Find the y coordinate: substract 1 to newPlace y and find the place with this y
    let foundPlaceY = shelfPlaces.find(p => p.y === place.y - 1 && p.x === place.x && p.z === place.z);
    //console.log(foundPlaceY);
    newPosition.y = (foundPlaceY && foundPlaceY.position ? foundPlaceY.position.y + foundPlaceY.scale.y + place.scale.y : shelf.y + place.scale.y);

    // Find the z coordinate: substract 1 to newPlace z and find the place with this z
    let foundPlaceZ = shelfPlaces.find(p => p.z === place.z - 1 && p.x === place.x && p.y === place.y);
    //console.log(foundPlaceZ);
    newPosition.z = (foundPlaceZ && foundPlaceZ.position ? foundPlaceZ.position.z + foundPlaceZ.scale.z + place.scale.z : shelf.z + place.scale.z);

    //console.log(`newPosition = ${JSON.stringify(newPosition)}`);
    return newPosition;
}