// Express 기본 모듈
const express = require('express'),
 http = require('http'),
 path = require('path');

// Express 미들웨어
const bodyParser = require('body-parser'), 
 static = require('serve-static'),
 expressErrorHandler = require('express-error-handler');

const app = express();

app.set('port', process.env.PORT || 3000);

app.use(bodyParser.urlencoded({ extended: false }));

app.use(bodyParser.json());

app.use('/public', static(path.join(__dirname, 'public')));

const router = express.Router();

router.route('/process/users/:id').get((req, res) => {
    console.log('/process/users/:id 처리함.');
    
    const paramId = req.params.id;

    console.log('/process/users와 토큰 %s를 이용해 처리함.', paramId)

    res.writeHead('200', {'Content-Type':'text/html;charset=utf8'});
    res.write('<h1>Express 서버에서 응답한 결과입니다.</h1>');
    res.write('<div><p>Param id : ' + paramId + '</p></div>');
    res.end();
})

// 라우터 객체 등록
app.use('/', router);

// 에러 처리
const errorHandler = expressErrorHandler({
    static: {
        '404': './public/404.html'
    }
});

app.use(expressErrorHandler.httpError(404));
app.use(errorHandler);

http.createServer(app).listen(app.get('port'), () => {
    console.log('Express 서버가 3000번 포트에서 시작됨.');
});