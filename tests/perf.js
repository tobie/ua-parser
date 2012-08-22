var uap = require('ua-parser');
var uas = require('./uas').uas;
var i, u, n=100000;

var ts = +new Date();
for(i=0; i<n; i++) {
	u = Math.floor(Math.random()*uas.length);

	uap.parse(uas[u]);
}
var te = +new Date();

console.log("Parsed %d UAs in %d ms @ %s uas/s (%s μs/ua)", n, (te-ts), (n*1000/(te-ts)).toFixed(1), ((te-ts)*1000/n).toFixed(1));
