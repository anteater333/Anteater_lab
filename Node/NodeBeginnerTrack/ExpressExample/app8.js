// Express 기본 모듈
const express = require('express'),
 http = require('http'),
 path = require('path');

// Express 미들웨어
const bodyParser = require('body-parser'), 
 static = require('serve-static');

const app = express();

app.set('port', process.env.PORT || 3000);

app.use(bodyParser.urlencoded({ extended: false }));

app.use(bodyParser.json());

app.use(static(path.join(__dirname, 'public')));

const router = express.Router();

router.route('/process/login').post((req, res) => {
    console.log('/process/login 처리함.');
    
    const paramId = req.body.id || req.query.id;
    const paramPassword = req.body.password || req.query.password;

    res.writeHead('200', {'Content-Type':'text/html;charset=utf8'});
    res.write('<h1>Express 서버에서 응답한 결과입니다.</h1>');
    res.write('<div><p>Param id : ' + paramId + '</p></div>');
    res.write('<div><p>Param pwd : ' + paramPassword + '</p></div>');
    res.write("<br><br><a href='/login2.html'>로그인 페이지로 돌아가기</a>");
    res.end();
})

app.all('*', (req, res) => {
    res.status(404).send('<h1>404 Not Found Error - 페이지를 찾을 수 없습니다.</h1>');
})

// 라우터 객체 등록
app.use('/', router);

http.createServer(app).listen(app.get('port'), () => {
    console.log('Express 서버가 3000번 포트에서 시작됨.');
});