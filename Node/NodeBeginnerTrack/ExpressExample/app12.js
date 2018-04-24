// Express 기본 모듈
const express = require('express'),
 http = require('http'),
 path = require('path');

// Express 미들웨어
const bodyParser = require('body-parser'), 
 static = require('serve-static'),
 expressErrorHandler = require('express-error-handler'),
 cookieParser = require('cookie-parser'),
 expressSession = require('express-session');

const app = express();

app.set('port', process.env.PORT || 3000);

app.use(bodyParser.urlencoded({ extended: false }));

app.use(bodyParser.json());

app.use('/public', static(path.join(__dirname, 'public')));

app.use(cookieParser()); // Most middleware (like cookieParser) is no longer bundled with Express and must be installed separately.
                         // Not express.cookieParser();

app.use(expressSession({
    secret:'my key',
    resave:true,
    saveUninitialized:true
}));

const router = express.Router();

router.route('/process/login').post((req, res) => {
    console.log('/process/login 호출됨.');
    
    const paramId = req.body.id || req.query.id;
    const paramPassword = req.body.password || req.query.password;

    if (req.session.user) {
        console.log('이미 로그인되어 상품 페이지로 이동합니다.');

        res.redirect('/public/product.html');
    } else {
        req.session.user = {
            id: paramId,
            name: '소녀시대',
            authorized: true
        };

        console.log(req.session.id + ' 로그인 성공');
        res.writeHead('200', {'Content-Type':'text/html;charset=utf8'});
        res.write('<h1>로그인 성공</h1>');
        res.write('<div><p>Param id : ' + paramId + '</p></div>');
        res.write('<div><p>Param pwd : ' + paramPassword + '</p></div>');
        res.write("<br><br><a href='/process/product'>상품 페이지로 이동하기</a>");
        res.end();
    }
});

router.route('/process/logout').get((req, res) => {
    console.log('/process/logout 호출됨.');

    if (req.session.user) {
        console.log(req.session.id + ' 로그아웃 합니다.');
        
        req.session.destroy((err) => {
            if (err) {throw err;}

            console.log('세션을 삭제하고 로그아웃되었습니다.');
            res.redirect('/public/login2.html');
        });
    } else {
        console.log('아직 로그인되어 있지 않습니다.');

        res.redirect('/public/login2.html');
    }
})

router.route('/process/users/:id').get((req, res) => {
    console.log('/process/users/:id 처리함.');
    
    const paramId = req.params.id;

    console.log('/process/users와 토큰 %s를 이용해 처리함.', paramId)

    res.writeHead('200', {'Content-Type':'text/html;charset=utf8'});
    res.write('<h1>Express 서버에서 응답한 결과입니다.</h1>');
    res.write('<div><p>Param id : ' + paramId + '</p></div>');
    res.end();
});

router.route('/process/showCookie').get((req, res) => {
    console.log('/process/showCookie 호출됨.');

    res.send(req.cookies);
});

router.route('/process/setUserCookie').get((req, res) => {
    console.log('/process/setUserCookie 호출됨.');

    res.cookie('user', {
        id: 'mike',
        name: '소녀시대',
        authorized: true
    });

    res.redirect('/process/showCookie');
});

router.route('/process/product').get((req, res) => {
    console.log('/process/product 호출됨.');

    if (req.session.user) {
        res.redirect('/public/product.html');
    } else {
        res.redirect('/public/login2.html');
    }
});

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