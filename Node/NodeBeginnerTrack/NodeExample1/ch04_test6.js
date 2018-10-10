const fs = require('fs');

const data = fs.readFile('./package.json', 'utf8', (err, data) => {
    console.log(data);
}); // Async Read

console.log('프로젝트 폴더 안의 package.json 파일을 읽도록 요청했습니다.');