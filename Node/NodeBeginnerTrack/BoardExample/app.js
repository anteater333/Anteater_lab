// Express 기본 모듈
const express = require('express')
    , http = require('http')
    , path = require('path');

// Express 미들웨어
const bodyParser = require('body-parser')
    , cookieParser = require('cookie-parser')
    , static = require('serve-static');

// Error Handler 모듈
const expressErrorHandler = require('express-error-handler');

// Session 미들웨어
const expressSession = require('express-session');

// mongoose 모듈
const mongoose = require('mongoose');

// config 모듈
const config = require('./config/config');

// database 모듈
const database = require('./database/database');

// route 모듈
const route_loader = require('./routes/route_loader');

// = = = = = PASSPORT = = = = = //
const passport = require('passport');
const flash = require('connect-flash');

// Express 객체
const app = express();

/*************************************************************************/
// 기본 속성 설정
console.log('config.server_port : %d', config.server_port);
app.set('port', config.server_port);

// body-parser를 사용해 application/x-www-form-urlencoded 파싱
// body-parser는 POST request 데이터를 추출할 수 있게 해준다.
app.use(bodyParser.urlencoded({ extended: false }));

// body-parser를 사용해 application/json 파싱
app.use(bodyParser.json());

// public 폴더를 static으로 오픈
app.use('/public', static(path.join(__dirname, 'public')));

// cookie-parser 설정
app.use(cookieParser());

// 세션 설정
app.use(expressSession({
    secret:'my key',
    resave:true,
    saveUninitialized:true
}));

/*************************************************************************/
// Passport 사용 설정 (미들웨어)
app.use(passport.initialize());
app.use(passport.session());
app.use(flash());

const configPassport = require('./config/passport');
configPassport(app, passport);

// Passport 관련 함수 라우팅
const userPassport = require('./routes/user_passport');
userPassport(app, passport);
/*************************************************************************/

/*************************************************************************/
// 뷰 엔진 설정
const viewEngine = 'ejs';
app.set('views', __dirname + '/views');
app.set('view engine', viewEngine);
console.log('뷰 엔진이 ' + viewEngine + '로 설정되었습니다.');
/*************************************************************************/

/*************************************************************************/
// 라우팅
const router = express.Router();
route_loader.init(app, router, config);
/*************************************************************************/

// 404 Not Found
const errorHandler = expressErrorHandler({
    static: {
        '404': './public/404.html'
    }
});

app.use(expressErrorHandler.httpError(404));
app.use(errorHandler);

// 프로세스 종료 시 데이터베이스 연결 해제
process.on('SIGTERM', () => {
    console.log('프로세스가 종료됩니다.');
    app.close();
});

app.on('close', () => {
    console.log('Express 서버 객체가 종료됩니다.');
    if (database.db) {
        database.db.close();
    }
});

// 서버 시작
http.createServer(app).listen(app.get('port'), () => {
    console.log('======= 서버가 시작되었습니다. 포트 : ' + app.get('port') + ' =======');

    // 데이터베이스 연결
    database.init(app, config);
});