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

// Express 객체
const app = express();

// 기본 속성 설정
app.set('port', process.env.PORT || 3000);

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
// 라우팅

// 라우터 객체 참조
const router = express.Router();

// 샤용자 리스트 함수
router.route('/process/listuser').post((req, res) => {
    console.log('/process/listuser 호출됨.');

    // 데이터베이스 객체가 초기화된 경우, 몯레 객체의 findAll 메소드 호출
    if(database) {
        // 1. 모든 사용자 검색
        UserModel.findAll((err, results) => {
            if (err) { // 에러
                console.log('사용자 리스트 조회 중 오류 발생 : ' + err.stack);

                res.writeHead('200', {'Content-Type' : 'text/html;charset=utf8'});
                res.write('<h2>사용자 리스트 조회 중 오류 발생</h2>');
                res.write('<p>' + err.stack + '</p>');
                res.end();
                
                return;
            }

            if (results) { // 결과 객체가 있으면
                console.dir(results);

                res.writeHead('200', {'Content-Type' : 'text/html;charset=utf8'});
                res.write('<h2>사용자 리스트</h2>');
                res.write('<div><ul>');

                for (let i = 0; i < results.length; i++) {
                    let curId = results[i]._doc.id;
                    let curName = results[i]._doc.name;
                    res.write('    <li>#' + i + ' : ' + curId + ', ' + curName + '</li>');
                }

                res.write('</ul></div>');
                res.end();
            } else { // 결과 객체가 없으면
                res.writeHead('200', {'Content-Type' : 'text/html;charset=utf8'});
                res.write('<h2>사용자 리스트 조회 실패</h2>');
                res.end();
            }
        });
    } else { // 데이터베이스 객체가 초기화되지 않았을 때 실패 응답 전송
        res.writeHead('200', {'Content-Type' : 'text/html;charset=utf8'});
        res.write('<h2>데이터베이스 연결 실패</h2>');
        res.end();
    }
});

// 로그인 라우팅 함수 - 데이터베이스의 정보와 비교
router.route('/process/login').post((req, res) => {
    console.log('/process/login 호출됨.');
    
    // 로그인 처리
    let paramId = req.param('id');
    let paramPassword = req.param('password');

    if (database) {
        authUser(database, paramId, paramPassword, (err, docs) => {
            if (err) { throw err; }

            if (docs) {
                console.dir(docs);
                let username = docs[0].name;
                res.writeHead('200', {'Content-Type' : 'text/html;charset=utf8'});
                res.write('<h1>로그인 성공</h1>');
                res.write('<div><p>사용자 아이디 : ' + paramId + '</p></div>');
                res.write('<div><p>사용자 이름 : ' + username + '</p></div>');
                res.write("<br><br><a href='/public/login.html'>다시 로그인하기</a>");
                res.end();
            } else {
                res.writeHead('200', {'Content-Type' : 'text/html;charset=utf8'});
                res.write('<h1>로그인 실패</h1>');
                res.write('<div><p>아이디와 비밀번호를 다시 확인하십시오.</p></div>');
                res.write("<br><br><a href='/public/login.html'>다시 로그인하기</a>");
                res.end();
            }
        });
    } else {
        res.writeHead('200', {'Content-Type' : 'text/html;charset=utf8'});
        res.write('<h2>데이터베이스 연결 실패</h2>');
        res.write('<div><p>데이터베이스에 연결하지 못했습니다.</p></div>');
        res.end();
    }
});

// 사용자 추가 라우팅 함수 - 클라이언트에서 보내온 데이터를 이용해 데이터베이스에 추가
router.route('/process/adduser').post((req, res) => {
    console.log('/process/adduser 호출됨.');

    let paramId = req.body.id || req.query.id;
    let paramPassword = req.body.password || req.query.password;
    let paramName = req.body.name || req.query.name;

    console.log('요청 파라미터 : ' + paramId + ', ' + paramPassword + ', ' + paramName);

    if (database) {
        addUser(database, paramId, paramPassword, paramName, (err, result) => {
            if (err) { throw err; }

            // 결과 객체에 추가된 데이터가 있으면 성공 응답 전송
            if (result /*&& result.insertedCount > 0*/) {
                console.dir(result);

                res.writeHead('200', {'Content-Type':'text/html;charset=utf8'});
                res.write('<h2>사용자 추가 성공</h2>');
                res.end();
            } else {
                res.writeHead('200', {'Content-Type':'text/html;charset=utf8'});
                res.write('<h2>사용자 추가 실패</h2>');
                res.end();
            }
        });
    } else {
        res.writeHead('200', {'Content-Type' : 'text/html;charset=utf8'});
        res.write('<h2>데이터베이스 연결 실패</h2>');
        res.write('<div><p>데이터베이스에 연결하지 못했습니다.</p></div>');
        res.end();
    }
})

// 라우터 객체 등록
app.use('/', router);
/*************************************************************************/

// 404 Not Found
const errorHandler = expressErrorHandler({
    static: {
        '404': './public/404.html'
    }
});

app.use(expressErrorHandler.httpError(404));
app.use(errorHandler);


/*************************************************************************/
// Database (Mongoose 사용)

/*
// 몽고디비 모듈 사용
const MongoClient = require('mongodb').MongoClient;
*/

// 데이터베이스 객체를 위한 변수
let database;

// 데이터베이스 스키마 객체를 위한 변수
let UserSchema;

// 데이터베이스 모델 객체를 위한 변수
let UserModel;

// 데이터베이스에 연결
function connectDB() {
    // 데이터베이스 연결 정보
    const databaseUrl = 'mongodb://localhost:27017/local';

    // 데이터베이스 연결
    console.log('데이터베이스 연결을 시도합니다.');
    mongoose.Promise = global.Promise;
    mongoose.connect(databaseUrl);
    database = mongoose.connection;

    // Events
    database.on('error', console.error.bind(console, 'mongoose connection error.'));
    database.on('open', () => {
        console.log('데이터베이스에 연결되었습니다. : ' + databaseUrl);
        
        // 스키마 정의
        UserSchema = mongoose.Schema({
            id: {type: String, required: true, unique: true},
            name: {type: String, index: 'hashed'},
            password: {type: String, required: true},
            age: {type: Number, 'default': -1},
            created_at: {type: Date, index: {unique: false}, 'default': Date.now},
            updated_at: {type: Date, index: {unique: false}, 'default': Date.now}
        });

        // 스키마에 static 메소드 추가
        UserSchema.static('findById', function(id, callback) {   // 주의! Arrow function 사용 시 this를 인식하지 못함.
            return this.find({id: id}, callback);
        });

        UserSchema.static('findAll', function(callback) {
            return this.find({ }, callback);
        });
        console.log('UserSchema 정의.');

        // UserModel 정의
        UserModel = mongoose.model("users2", UserSchema);
        console.log('UserModel 정의.');
    });

    // 연결 끊어졌을 때 5초 후 재연결
    database.on('disconnected', () => {
        console.log('연결이 끊어졌습니다. 5초 후 다시 연결합니다.');
        setInterval(connectDB, 5000);
    });
}

// 사용자 인증
const authUser = (database, id, password, callback) => {
    console.log('authUser 호출됨.');

    // 아이디와 비밀번호를 사용해 검색
    UserModel.findById(id, (err, results) => {
        if(err) {
            callback(err, null);
            return;
        }

        console.log('아이디 [%s]로 사용자 검색 결과', id);
        console.dir(results);

        if(results.length > 0) {
            console.log('아이디 [%s]가 일치하는 사용자 찾음.', id);

            if(results[0]._doc.password === password) {
                console.log('비밀번호 일치함');
                callback(null, results);
            } else {
                
            }
        } else {
            console.log("일치하는 사용자를 찾지 못함.");
            callback(null, null);
        }
    });
};

// 사용자 추가
const addUser = (databse, id, password, name, callback) => {
    console.log('addUser 호출됨 : ' + id + ', ' + password + ', ' + name);

    // UserModel의 인스턴스 생성
    let user = new UserModel({"id" : id, "password" : password, "name" : name});

    // save() 사용
    user.save((err) => {
        if(err) {
            callback(err, null);
            return;
        }

        console.log("사용자 데이터 추가함.");
        callback(null, user);
    });
};
/*************************************************************************/




// 서버 시작
http.createServer(app).listen(app.get('port'), () => {
    console.log('서버가 시작되었습니다. 포트 : ' + app.get('port'));

    // 데이터베이스 연결
    connectDB();
});