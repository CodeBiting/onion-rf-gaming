var express = require('express');
var router = express.Router();

const placetypes = require('../data/placetypes.json');
const placesubtypes = require('../data/placesubtypes.json');
const warehouse = require('../data/warehouse.json');
const warehouse5x2 = require('../data/warehouse-5x2.json');
const warehouse5x2s = require('../data/warehouse-5x2-shelfs.json');
const warehouseraul = require('../data/warehouse-raul.json');
const users = require('../data/users.json');
const userimages = require('../data/userimages.json');

/* GET home page. */
router.get('/', function(req, res, next) {
  res.render('index', { title: 'Express' });
});

/*
router.get('/login', function(req, res, next) {
    let status = 200;

    res.status(status).json({
        status: status,
        message: 'Login successful'
    });
});
*/

/**
 * Function to login a user and return a user object
 * The request body should contain the following:
 * - user
 * - password
 * The returned user object if the login suceeded should contain the following:
 * - id
 * - name
 * - email
 * - level
 * - image
 */
router.post('/login', function(req, res, next) {
    let status = 200;

    res.status(status).json({
        status: status,
        message: 'Login successful',
        user: {
            id: 1,
            name: 'John Doe',
            email: 'johndoe@email.com',
            level: 2,
            image: userimages[0].image
        } 
    });
});

router.get('/warehouse', function(req, res, next) {
    let status = 200;

    res.status(status).json({
        status: status,
        message: 'Login successful',
        warehouse
    });
});

router.get('/placetypes', function(req, res, next) {
    let status = 200;

    res.status(status).json({
        status: status,
        message: 'Login successful',
        placetypes
    });
});

router.get('/placesubtypes', function(req, res, next) {
    let status = 200;

    res.status(status).json({
        status: status,
        message: 'Login successful',
        placesubtypes
    });
});

router.get('/users', function(req, res, next) {
    let status = 200;

    res.status(status).json({
        status: status,
        message: 'Login successful',
        users
    });
});

router.get('/warehouse5x2', function(req, res, next) {
    let status = 200;

    res.status(status).json({
        status: status,
        message: 'Login successful',
        warehouse5x2
    });
});

router.get('/warehouse5x2s', function(req, res, next) {
    let status = 200;

    res.status(status).json({
        status: status,
        message: 'Login successful',
        warehouse5x2s
    });
});
router.get('/warehouseraul', function(req, res, next) {
    let status = 200;

    res.status(status).json({
        status: status,
        message: 'Login successful',
        warehouseraul
    });
});

router.get('/warehouse4unity', function(req, res, next) {
    let status = 200;

    let warehouse4unity = [];

    res.status(status).json({
        status: status,
        message: 'Login successful',
        warehouse: warehouse4unity
    });
});

module.exports = router;

