// Express 기본 모듈
const express = require('express'),
 http = require('http'),
 path = require('path');

// Express 미들웨어
const bodyParser = require('body-parser'), 
 static = require('serve-static'),
 expressErrorHandler = require('express-error-handler'),
 cookieParser = require('cookie-parser'),
 expressSession = require('express-session'),
 multer = require('multer');

const fs = require('fs');
const cors = require('cors'); // CORS (다중 서버 접속)

const app = express();

app.set('port', process.env.PORT || 3000);

app.use(bodyParser.urlencoded({ extended: false }));

app.use(bodyParser.json());

app.use('/public', static(path.join(__dirname, 'public')));
app.use('/uploads', static(path.join(__dirname, 'uploads')));

app.use(cookieParser()); // Most middleware (like cookieParser) is no longer bundled with Express and must be installed separately.
                         // Not express.cookieParser();

app.use(expressSession({
    secret:'my key',
    resave:true,
    saveUninitialized:true
}));

app.use(cors());

// 항상 미들웨어 사용 순서는 body-parser -> multer -> router
// 파일 제한: 10개, 1G
const storage = multer.diskStorage({
    destination: (req, file, callback) => {
        callback(null, 'uploads')
    },
    filename: (req, file, callback) => {
        callback(null, Date.now() + '_' + file.originalname) // 파일명 중복 방지.
    }
});

const uploads = multer({
    storage: storage,
    limits: {
        files: 10,
        fileSize: 1024 * 1024 * 1024
    }
});

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

router.route('/process/photo').post(uploads.array('photo', 1), (req, res) => {
    console.log('/process/photo 호출됨.');

    try {
        const files = req.files;

        console.dir('#===== 업로드된 첫번째 파일 정보 =====#');
        console.dir(req.files[0]);
        console.dir('#=====#');

        let originalname = '',
            filename = '',
            mimetype = '',
            size = 0;

        if (Array.isArray(files)) {
            console.log("배열에 들어있는 파일 갯수 : %d", files.length);

            for (let index = 0; index < files.length; index++) {
                originalname = files[index].originalname;
                filename = files[index].filename;
                mimetype = files[index].mimetype;
                size = files[index].size;
            }
        } else {
            console.log("파일 갯수 : 1 ");

            originalname = files[index].originalname;
            filename = files[index].filename;
            mimetype = files[index].mimetype;
            size = files[index].size;
        }

        console.log('현재 파일 정보 : '+ originalname + ', ' + filename + ', ' + mimetype + ', ' + size);

        res.writeHead('200', {'Content-Type':'text/html;charset=utf8'});
        res.write('<h3>파일 업로드 성공</h3>');
        res.write('<hr/>')
        res.write('<p>원본 파일 이름 : ' + originalname + ' -> 저장 파일명 : ' + filename + '</p>');
        res.write('<p>MIME TYPE : ' + mimetype + '<p>');
        res.write('<p>파일 크기 : ' + size + '</p>');
        res.end();
    } catch (err) {
            console.dir(err.stack);
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