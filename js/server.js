var express = require('express');
var multer = require('multer');

var app = new express();

app.post('/upload', multer({ dest: './uploads/'}).single('files'), function(req,res){
	res.status(204).end();
});

app.listen(7000, function(req, res){
  console.log("server started at port 7000");
})
