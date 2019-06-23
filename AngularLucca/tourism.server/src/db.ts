const JsonDB = require('node-json-db');
const db = new JsonDB('db', true, true);

export const destinations = db.getData('/destinations');
export const activities = db.getData('/activities');
