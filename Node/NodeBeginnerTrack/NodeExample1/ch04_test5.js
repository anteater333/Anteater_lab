const fs = require('fs');

const data = fs.readFileSync('./package.json', 'utf8'); // Sync Read

console.log(data);