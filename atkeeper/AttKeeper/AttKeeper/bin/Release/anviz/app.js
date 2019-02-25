#!/usr/bin/env node
var path     = require('path')
var optimist = require('optimist')
var bak2xls  = require('./')

var argv = optimist
	.usage('Usage: $0 path-to-input path-to-output [options]')

var input  = argv.argv._[0]
var output = argv.argv._[1]

if (!input || !output) {
	optimist.showHelp()
	process.exit(1)
}

bak2xls(input, output)