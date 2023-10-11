var express = require('express');
var router = express.Router();

const placetypes = require('../data/placetypes.json');
const placesubtypes = require('../data/placesubtypes.json');
const warehouse = require('../data/warehouse.json');
const users = require('../data/users.json');

/* GET home page. */
router.get('/', function(req, res, next) {
  res.render('index', { title: 'Express' });
});

router.get('/login', function(req, res, next) {
    let status = 200;

    res.status(status).json({
        status: status,
        message: 'Login successful'
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

module.exports = router;

