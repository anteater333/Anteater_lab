const express = require('express'), http = require('http');

const app = express();

app.use((req, res, next) => {
    console.log('첫 번째 미들웨어에서 요청을 처리함.');

    res.redirect('https://namu.wiki/');
});

http.createServer(app).listen(3000, () => {
    console.log('Express 서버가 3000번 포트에서 시작됨.');
});