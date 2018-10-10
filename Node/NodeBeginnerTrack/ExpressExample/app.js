// 시작점

const express = require('express'), http = require('http');

const app = express();

// 포트 설정
app.set('port', process.env.PORT || 3000);

// Express 서버 시작
http.createServer(app).listen(app.get('port'), () => {
    console.log('Express 서버를 시작했습니다 : ' + app.get('port'));
})