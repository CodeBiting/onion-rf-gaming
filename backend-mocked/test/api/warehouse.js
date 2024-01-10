/**
 * Created by jordi on 02/12/2022.
 *
 * To run the test `mocha test/api/warehouse.js`
 */
const assert = require('assert');
const warehouse = require('../../api/warehouse.js');

const PLACETYPES = [ 
    { "id": 1, "code": "Organization" },
    { "id": 2, "code": "Warehouse" },
    { "id": 3, "code": "User" },
    { "id": 4, "code": "Aisle" },
    { "id": 5, "code": "Shelf" },
    { "id": 6, "code": "Location" },
    { "id": 7, "code": "Container" },
    { "id": 8, "code": "Dock" },
    { "id": 9, "code": "Buffer" },
    { "id": 10, "code": "Fabrication line" },
    { "id": 11, "code": "Truck" },
    { "id": 12, "code": "Picking" }
];
const PLACESUBTYPES = [
    { "id": 1, "code": "Europalet", "x": 800, "y": 145, "z": 1200, "maxWeight": 1500 },
    { "id": 4, "code": "RACK", "x": 900, "y": 2600, "z": 1300, "maxWeight": 1000 },
    { "id": 7, "code": "Virtual", "x": 0, "y": 0, "z": 0, "maxWeight": "NULL" }
];
const COORDINATE_MULTIPLIER = 2;

const TEST_WAREHOUSE_5x1 = [
    { "id": 1, "code": "Organization", "typeId": 1, "placeTypeCode": "Organization", "x": "NULL", "y": "NULL", "z": "NULL", "subtypeId": "NULL", "allowStock": 0, "parentId": "NULL" },
    { "id": 2, "code": "WH", "typeId": 2, "placeTypeCode": "Warehouse", "x": 0, "y": 0, "z": 0, "subtypeId": "NULL", "allowStock": 1, "parentId": 1 },
    { "id": 3, "code": "A", "typeId": 5, "placeTypeCode": "Shelf", "x": 0, "y": 0, "z": 0, "subtypeId": "NULL", "allowStock": 1, "parentId": 2 },
    { "id": 4, "code": "A 001 000", "typeId": 6, "placeTypeCode": "Location", "x": 1, "y": 0, "z": 1, "subtypeId": 4, "allowStock": 1, "parentId": 3 },
    { "id": 5, "code": "A 002 000", "typeId": 6, "placeTypeCode": "Location", "x": 2, "y": 0, "z": 1, "subtypeId": 4, "allowStock": 1, "parentId": 3 },
    { "id": 6, "code": "A 003 000", "typeId": 6, "placeTypeCode": "Location", "x": 3, "y": 0, "z": 1, "subtypeId": 4, "allowStock": 1, "parentId": 3 },
    { "id": 7, "code": "A 004 000", "typeId": 6, "placeTypeCode": "Location", "x": 4, "y": 0, "z": 1, "subtypeId": 4, "allowStock": 1, "parentId": 3 },
    { "id": 8, "code": "A 005 000", "typeId": 6, "placeTypeCode": "Location", "x": 5, "y": 0, "z": 1, "subtypeId": 4, "allowStock": 1, "parentId": 3 }
];
const TEST_WAREHOUSE_5x1_EXPECTED = [
    { "id": 1, "code": "Organization", "typeId": 1, "placeTypeCode": "Organization", "x": "NULL", "y": "NULL", "z": "NULL", "subtypeId": "NULL", "allowStock": 0, "parentId": "NULL",
      "position": { "x": 0, "y": 0, "z": 0 },
      "rotation": { "x": 0, "y": 0, "z": 0 },
      "scale": { "x": 0, "y": 0, "z": 0 }
    },
    { "id": 2, "code": "WH", "typeId": 2, "placeTypeCode": "Warehouse", "x": 0, "y": 0, "z": 0, "subtypeId": "NULL", "allowStock": 1, "parentId": 1,
      "position": { "x": 0, "y": 0, "z": 0 },
      "rotation": { "x": 0, "y": 0, "z": 0 },
      "scale": { "x": 0, "y": 0, "z": 0 }
    },
    { "id": 3, "code": "A", "typeId": 5, "placeTypeCode": "Shelf", "x": 0, "y": 0, "z": 0, "subtypeId": "NULL", "allowStock": 1, "parentId": 2,
      "position": { "x": 0, "y": 0, "z": 0 },
      "rotation": { "x": 0, "y": 0, "z": 0 },
      "scale": { "x": 0, "y": 0, "z": 0 }
    },
    { "id": 4, "code": "A 001 000", "typeId": 6, "placeTypeCode": "Location", "x": 1, "y": 0, "z": 1, "subtypeId": 4, "allowStock": 1, "parentId": 3,
      "position": { "x": 450, "y": 1300, "z": 650 },
      "rotation": { "x": 0, "y": 0, "z": 0 },
      "scale": { "x": 450, "y": 1300, "z": 650 }
    },
    { "id": 5, "code": "A 002 000", "typeId": 6, "placeTypeCode": "Location", "x": 2, "y": 0, "z": 1, "subtypeId": 4, "allowStock": 1, "parentId": 3,
      "position": { "x": 1350, "y": 1300, "z": 650 },
      "rotation": { "x": 0, "y": 0, "z": 0 },
      "scale": { "x": 450, "y": 1300, "z": 650 }
    },
    { "id": 6, "code": "A 003 000", "typeId": 6, "placeTypeCode": "Location", "x": 3, "y": 0, "z": 1, "subtypeId": 4, "allowStock": 1, "parentId": 3,
      "position": { "x": 2250, "y": 1300, "z": 650 },
      "rotation": { "x": 0, "y": 0, "z": 0 },
      "scale": { "x": 450, "y": 1300, "z": 650 }
    },
    { "id": 7, "code": "A 004 000", "typeId": 6, "placeTypeCode": "Location", "x": 4, "y": 0, "z": 1, "subtypeId": 4, "allowStock": 1, "parentId": 3,
      "position": { "x": 3150, "y": 1300, "z": 650 },
      "rotation": { "x": 0, "y": 0, "z": 0 },
      "scale": { "x": 450, "y": 1300, "z": 650 }
    },
    { "id": 8, "code": "A 005 000", "typeId": 6, "placeTypeCode": "Location", "x": 5, "y": 0, "z": 1, "subtypeId": 4, "allowStock": 1, "parentId": 3,
      "position": { "x": 4050, "y": 1300, "z": 650 },
      "rotation": { "x": 0, "y": 0, "z": 0 },
      "scale": { "x": 450, "y": 1300, "z": 650 }    
    }
];
const TEST_WAREHOUSE_5x1_SHELFS_EXPECTED = [
  { "id": 1, "code": "Organization", "typeId": 1, "placeTypeCode": "Organization", "x": "NULL", "y": "NULL", "z": "NULL", "subtypeId": "NULL", "allowStock": 0, "parentId": "NULL",
    "position": { "x": 0, "y": 0, "z": 0 },
    "rotation": { "x": 0, "y": 0, "z": 0 },
    "scale": { "x": 0, "y": 0, "z": 0 },
    "shelfs": []
  },
  { "id": 2, "code": "WH", "typeId": 2, "placeTypeCode": "Warehouse", "x": 0, "y": 0, "z": 0, "subtypeId": "NULL", "allowStock": 1, "parentId": 1,
    "position": { "x": 0, "y": 0, "z": 0 },
    "rotation": { "x": 0, "y": 0, "z": 0 },
    "scale": { "x": 0, "y": 0, "z": 0 },
    "shelfs": []
  },
  { "id": 3, "code": "A", "typeId": 5, "placeTypeCode": "Shelf", "x": 0, "y": 0, "z": 0, "subtypeId": "NULL", "allowStock": 1, "parentId": 2,
    "position": { "x": 0, "y": 0, "z": 0 },
    "rotation": { "x": 0, "y": 0, "z": 0 },
    "scale": { "x": 0, "y": 0, "z": 0 },
    "shelfs": []
  },
  { "id": 4, "code": "A 001", "typeId": 6, "placeTypeCode": "Location", "x": 1, "y": 0, "z": 1, "subtypeId": 4, "allowStock": 1, "parentId": 3,
    "position": {
      "x": 0.450,
      "y": 1.300,
      "z": 0.650
    },
    "rotation": {
      "x": 0,
      "y": 0,
      "z": 0
    },
    "scale": {
      "x": 0.450,
      "y": 1.300,
      "z": 0.650
    },
    "shelfs": [
      { "y":-1.300 }
    ]
  },
  { "id": 5, "code": "A 002", "typeId": 6, "placeTypeCode": "Location", "x": 2, "y": 0, "z": 1, "subtypeId": 4, "allowStock": 1, "parentId": 3,
    "position": {
      "x": 1.350,
      "y": 1.300,
      "z": 0.650
    },
    "rotation": {
      "x": 0,
      "y": 0,
      "z": 0
    },
    "scale": {
      "x": 0.450,
      "y": 1.300,
      "z": 0.650
    },
    "shelfs": [
      { "y":-1.300 }
    ]
  },
  { "id": 6, "code": "A 003", "typeId": 6, "placeTypeCode": "Location", "x": 3, "y": 0, "z": 1, "subtypeId": 4, "allowStock": 1, "parentId": 3,
    "position": {
      "x": 2.250,
      "y": 1.300,
      "z": 0.650
    },
    "rotation": {
      "x": 0,
      "y": 0,
      "z": 0
    },
    "scale": {
      "x": 0.450,
      "y": 1.300,
      "z": 0.650
    },
    "shelfs": [
      { "y":-1.300 }
    ]
  },
  { "id": 7, "code": "A 004", "typeId": 6, "placeTypeCode": "Location", "x": 4, "y": 0, "z": 1, "subtypeId": 4, "allowStock": 1, "parentId": 3,
    "position": {
      "x": 3.150,
      "y": 1.300,
      "z": 0.650
    },
    "rotation": {
      "x": 0,
      "y": 0,
      "z": 0
    },
    "scale": {
      "x": 0.450,
      "y": 1.300,
      "z": 0.650
    },
    "shelfs": [
      { "y":-1.300 }
    ]
  },
  { "id": 8, "code": "A 005", "typeId": 6, "placeTypeCode": "Location", "x": 5, "y": 0, "z": 1, "subtypeId": 4, "allowStock": 1, "parentId": 3,
    "position": {
      "x": 4.050,
      "y": 1.300,
      "z": 0.650
    },
    "rotation": {
      "x": 0,
      "y": 0,
      "z": 0
    },
    "scale": {
      "x": 0.450,
      "y": 1.300,
      "z": 0.650
    },
    "shelfs": [
      { "y":-1.300 }
    ]
  }
];

const TEST_WAREHOUSE_5x2 = [
    { "id": 1, "code": "Organization", "typeId": 1, "placeTypeCode": "Organization", "x": "NULL", "y": "NULL", "z": "NULL", "subtypeId": "NULL", "allowStock": 0, "parentId": "NULL" },
    { "id": 2, "code": "WH", "typeId": 2, "placeTypeCode": "Warehouse", "x": 0, "y": 0, "z": 0, "subtypeId": "NULL", "allowStock": 1, "parentId": 1 },
    { "id": 3, "code": "A", "typeId": 5, "placeTypeCode": "Shelf", "x": 0, "y": 0, "z": 0, "subtypeId": "NULL", "allowStock": 1, "parentId": 2 },
    { "id": 4, "code": "A 001 000", "typeId": 6, "placeTypeCode": "Location", "x": 1, "y": 0, "z": 1, "subtypeId": 4, "allowStock": 1, "parentId": 3 },
    { "id": 5, "code": "A 002 000", "typeId": 6, "placeTypeCode": "Location", "x": 2, "y": 0, "z": 1, "subtypeId": 4, "allowStock": 1, "parentId": 3 },
    { "id": 6, "code": "A 003 000", "typeId": 6, "placeTypeCode": "Location", "x": 3, "y": 0, "z": 1, "subtypeId": 4, "allowStock": 1, "parentId": 3 },
    { "id": 7, "code": "A 004 000", "typeId": 6, "placeTypeCode": "Location", "x": 4, "y": 0, "z": 1, "subtypeId": 4, "allowStock": 1, "parentId": 3 },
    { "id": 8, "code": "A 005 000", "typeId": 6, "placeTypeCode": "Location", "x": 5, "y": 0, "z": 1, "subtypeId": 4, "allowStock": 1, "parentId": 3 },
    { "id": 9, "code": "A 001 001", "typeId": 6, "placeTypeCode": "Location", "x": 1, "y": 1, "z": 1, "subtypeId": 4, "allowStock": 1, "parentId": 3 },
    { "id": 10, "code": "A 002 001", "typeId": 6, "placeTypeCode": "Location", "x": 2, "y": 1, "z": 1, "subtypeId": 4, "allowStock": 1, "parentId": 3 },
    { "id": 11, "code": "A 003 001", "typeId": 6, "placeTypeCode": "Location", "x": 3, "y": 1, "z": 1, "subtypeId": 4, "allowStock": 1, "parentId": 3 },
    { "id": 12, "code": "A 004 001", "typeId": 6, "placeTypeCode": "Location", "x": 4, "y": 1, "z": 1, "subtypeId": 4, "allowStock": 1, "parentId": 3 },
    { "id": 13, "code": "A 005 001", "typeId": 6, "placeTypeCode": "Location", "x": 5, "y": 1, "z": 1, "subtypeId": 4, "allowStock": 1, "parentId": 3 }
];
const TEST_WAREHOUSE_5x2_EXPECTED = [
    { "id": 1, "code": "Organization", "typeId": 1, "placeTypeCode": "Organization", "x": "NULL", "y": "NULL", "z": "NULL", "subtypeId": "NULL", "allowStock": 0, "parentId": "NULL",
      "position": { "x": 0, "y": 0, "z": 0 },
      "rotation": { "x": 0, "y": 0, "z": 0 },
      "scale": { "x": 0, "y": 0, "z": 0 }
    },
    { "id": 2, "code": "WH", "typeId": 2, "placeTypeCode": "Warehouse", "x": 0, "y": 0, "z": 0, "subtypeId": "NULL", "allowStock": 1, "parentId": 1,
      "position": { "x": 0, "y": 0, "z": 0 },
      "rotation": { "x": 0, "y": 0, "z": 0 },
      "scale": { "x": 0, "y": 0, "z": 0 }
    },
    { "id": 3, "code": "A", "typeId": 5, "placeTypeCode": "Shelf", "x": 0, "y": 0, "z": 0, "subtypeId": "NULL", "allowStock": 1, "parentId": 2,
      "position": { "x": 0, "y": 0, "z": 0 },
      "rotation": { "x": 0, "y": 0, "z": 0 },
      "scale": { "x": 0, "y": 0, "z": 0 }
    },
    { "id": 4, "code": "A 001 000", "typeId": 6, "placeTypeCode": "Location", "x": 1, "y": 0, "z": 1, "subtypeId": 4, "allowStock": 1, "parentId": 3,
      "position": { "x": 450, "y": 1300, "z": 650 },
      "rotation": { "x": 0, "y": 0, "z": 0 },
      "scale": { "x": 450, "y": 1300, "z": 650 }
    },
    { "id": 5, "code": "A 002 000", "typeId": 6, "placeTypeCode": "Location", "x": 2, "y": 0, "z": 1, "subtypeId": 4, "allowStock": 1, "parentId": 3,
      "position": { "x": 1350, "y": 1300, "z": 650 },
      "rotation": { "x": 0, "y": 0, "z": 0 },
      "scale": { "x": 450, "y": 1300, "z": 650 }
    },
    { "id": 6, "code": "A 003 000", "typeId": 6, "placeTypeCode": "Location", "x": 3, "y": 0, "z": 1, "subtypeId": 4, "allowStock": 1, "parentId": 3,
      "position": { "x": 2250, "y": 1300, "z": 650 },
      "rotation": { "x": 0, "y": 0, "z": 0 },
      "scale": { "x": 450, "y": 1300, "z": 650 }
    },
    { "id": 7, "code": "A 004 000", "typeId": 6, "placeTypeCode": "Location", "x": 4, "y": 0, "z": 1, "subtypeId": 4, "allowStock": 1, "parentId": 3,
      "position": { "x": 3150, "y": 1300, "z": 650 },
      "rotation": { "x": 0, "y": 0, "z": 0 },
      "scale": { "x": 450, "y": 1300, "z": 650 }
    },
    { "id": 8, "code": "A 005 000", "typeId": 6, "placeTypeCode": "Location", "x": 5, "y": 0, "z": 1, "subtypeId": 4, "allowStock": 1, "parentId": 3,
      "position": { "x": 4050, "y": 1300, "z": 650 },
      "rotation": { "x": 0, "y": 0, "z": 0 },
      "scale": { "x": 450, "y": 1300, "z": 650 }    
    },
    { "id": 9, "code": "A 001 001", "typeId": 6, "placeTypeCode": "Location", "x": 1, "y": 1, "z": 1, "subtypeId": 4, "allowStock": 1, "parentId": 3,
      "position": { "x": 450, "y": 3900, "z": 650 },
      "rotation": { "x": 0, "y": 0, "z": 0 },
      "scale": { "x": 450, "y": 1300, "z": 650 }
    },
    { "id": 10, "code": "A 002 001", "typeId": 6, "placeTypeCode": "Location", "x": 2, "y": 1, "z": 1, "subtypeId": 4, "allowStock": 1, "parentId": 3,
      "position": { "x": 1350, "y": 3900, "z": 650 },
      "rotation": { "x": 0, "y": 0, "z": 0 },
      "scale": { "x": 450, "y": 1300, "z": 650 }
    },
    { "id": 11, "code": "A 003 001", "typeId": 6, "placeTypeCode": "Location", "x": 3, "y": 1, "z": 1, "subtypeId": 4, "allowStock": 1, "parentId": 3,
      "position": { "x": 2250, "y": 3900, "z": 650 },
      "rotation": { "x": 0, "y": 0, "z": 0 },
      "scale": { "x": 450, "y": 1300, "z": 650 }
    },
    { "id": 12, "code": "A 004 001", "typeId": 6, "placeTypeCode": "Location", "x": 4, "y": 1, "z": 1, "subtypeId": 4, "allowStock": 1, "parentId": 3,
      "position": { "x": 3150, "y": 3900, "z": 650 },
      "rotation": { "x": 0, "y": 0, "z": 0 },
      "scale": { "x": 450, "y": 1300, "z": 650 }
    },
    { "id": 13, "code": "A 005 001", "typeId": 6, "placeTypeCode": "Location", "x": 5, "y": 1, "z": 1, "subtypeId": 4, "allowStock": 1, "parentId": 3,
      "position": { "x": 4050, "y": 3900, "z": 650 },
      "rotation": { "x": 0, "y": 0, "z": 0 },
      "scale": { "x": 450, "y": 1300, "z": 650 }    
    }
];
const TEST_WAREHOUSE_5x2_SHELFS_EXPECTED = [
  { "id": 1, "code": "Organization", "typeId": 1, "placeTypeCode": "Organization", "x": "NULL", "y": "NULL", "z": "NULL", "subtypeId": "NULL", "allowStock": 0, "parentId": "NULL",
    "position": { "x": 0, "y": 0, "z": 0 },
    "rotation": { "x": 0, "y": 0, "z": 0 },
    "scale": { "x": 0, "y": 0, "z": 0 },
    "shelfs": []
  },
  { "id": 2, "code": "WH", "typeId": 2, "placeTypeCode": "Warehouse", "x": 0, "y": 0, "z": 0, "subtypeId": "NULL", "allowStock": 1, "parentId": 1,
    "position": { "x": 0, "y": 0, "z": 0 },
    "rotation": { "x": 0, "y": 0, "z": 0 },
    "scale": { "x": 0, "y": 0, "z": 0 },
    "shelfs": []
  },
  { "id": 3, "code": "A", "typeId": 5, "placeTypeCode": "Shelf", "x": 0, "y": 0, "z": 0, "subtypeId": "NULL", "allowStock": 1, "parentId": 2,
    "position": { "x": 0, "y": 0, "z": 0 },
    "rotation": { "x": 0, "y": 0, "z": 0 },
    "scale": { "x": 0, "y": 0, "z": 0 },
    "shelfs": []
  },
  { "id": 4, "code": "A 001", "typeId": 6, "placeTypeCode": "Location", "x": 1, "y": 0, "z": 1, "subtypeId": 4, "allowStock": 1, "parentId": 3,
    "position": {
      "x": 0.450,
      "y": 2.600,
      "z": 0.650
    },
    "rotation": {
      "x": 0,
      "y": 0,
      "z": 0
    },
    "scale": {
      "x": 0.450,
      "y": 2.600,
      "z": 0.650
    },
    "shelfs": [
      { "y":-2.600 },
      { "y":0 }
    ]
  },
  { "id": 5, "code": "A 002", "typeId": 6, "placeTypeCode": "Location", "x": 2, "y": 0, "z": 1, "subtypeId": 4, "allowStock": 1, "parentId": 3,
    "position": {
      "x": 1.350,
      "y": 2.600,
      "z": 0.650
    },
    "rotation": {
      "x": 0,
      "y": 0,
      "z": 0
    },
    "scale": {
      "x": 0.450,
      "y": 2.600,
      "z": 0.650
    },
    "shelfs": [
      { "y":-2.600 },
      { "y":0 }
    ]
  },
  { "id": 6, "code": "A 003", "typeId": 6, "placeTypeCode": "Location", "x": 3, "y": 0, "z": 1, "subtypeId": 4, "allowStock": 1, "parentId": 3,
    "position": {
      "x": 2.250,
      "y": 2.600,
      "z": 0.650
    },
    "rotation": {
      "x": 0,
      "y": 0,
      "z": 0
    },
    "scale": {
      "x": 0.450,
      "y": 2.600,
      "z": 0.650
    },
    "shelfs": [
      { "y":-2.600 },
      { "y":0 }
    ]
  },
  { "id": 7, "code": "A 004", "typeId": 6, "placeTypeCode": "Location", "x": 4, "y": 0, "z": 1, "subtypeId": 4, "allowStock": 1, "parentId": 3,
    "position": {
      "x": 3.150,
      "y": 2.600,
      "z": 0.650
    },
    "rotation": {
      "x": 0,
      "y": 0,
      "z": 0
    },
    "scale": {
      "x": 0.450,
      "y": 2.600,
      "z": 0.650
    },
    "shelfs": [
      { "y":-2.600 },
      { "y":0 }
    ]
  },
  { "id": 8, "code": "A 005", "typeId": 6, "placeTypeCode": "Location", "x": 5, "y": 0, "z": 1, "subtypeId": 4, "allowStock": 1, "parentId": 3,
    "position": {
      "x": 4.050,
      "y": 2.600,
      "z": 0.650
    },
    "rotation": {
      "x": 0,
      "y": 0,
      "z": 0
    },
    "scale": {
      "x": 0.450,
      "y": 2.600,
      "z": 0.650
    },
    "shelfs": [
      { "y":-2.600 },,
      { "y":0 }
    ]
  }
];

// TODO: test shelfs with several depths (z)

// TODO: test shelf with irregular locations (several placesubtypes)

// TODO: test several shelfs


describe('Warehouse API', function () {
    describe('transformWarehouse4unity()', function () {
        it('should transform a shelf with 5 columns and 1 rows successfully', function () {
            const wtransformed = warehouse.transformWarehouse4unity(TEST_WAREHOUSE_5x1, 
                PLACETYPES, 
                PLACESUBTYPES, 
                COORDINATE_MULTIPLIER);

            //console.log(JSON.stringify(wtransformed));

            assert.deepEqual(TEST_WAREHOUSE_5x1_EXPECTED, wtransformed);
        });

        it('should transform a shelf with 5 columns and 2 rows successfully', function () {
            const wtransformed = warehouse.transformWarehouse4unity(TEST_WAREHOUSE_5x2, 
                PLACETYPES, 
                PLACESUBTYPES, 
                COORDINATE_MULTIPLIER);

            console.log(JSON.stringify(wtransformed));

            assert.deepEqual(TEST_WAREHOUSE_5x2_EXPECTED, wtransformed);
        });
    });

    describe('extractShelfsFromWarehouse()', function () {
        it('should extract a shelf with 5 columns and 1 rows successfully', function () {
            const shelfs = warehouse.extractShelfsFromWarehouse(TEST_WAREHOUSE_5x1, PLACETYPES, PLACESUBTYPES);

            assert.deepEqual(TEST_WAREHOUSE_5x1_SHELFS_EXPECTED, shelfs);
        });

        it('should extract a shelf with 5 columns and 2 rows successfully', function () {
            const shelfs = warehouse.extractShelfsFromWarehouse(TEST_WAREHOUSE_5x2, PLACETYPES, PLACESUBTYPES);

            assert.deepEqual(TEST_WAREHOUSE_5x2_SHELFS_EXPECTED, shelfs);
        });
    });
});