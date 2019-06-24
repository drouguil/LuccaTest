import { config } from 'dotenv';
config(); // load .env file

import * as express from 'express';
import * as bodyParser from 'body-parser';
import * as cors from "cors";

import { registerDestinationRoutes } from './destination';
import { registerActivityRoutes } from './activity';

const app: any = express();

const host = process.env.HOST || 'localhost',
	port = process.env.PORT || 3000;

app.use(bodyParser.urlencoded({ extended: true }));
app.use(bodyParser.json());

app.use(cors);

// allow cors
/*app.use(function(req, res, next) {
	res.header("Access-Control-Allow-Origin", "*");
	res.header("Access-Control-Allow-Headers", "Origin, X-Requested-With, Content-Type, Accept");
	next();
});*/

registerDestinationRoutes(app);
registerActivityRoutes(app);

app.listen(port, host, function() {
	console.log(`lucca-tourism RESTful API server started on: http://${host}:${port}`);
});

export default app;