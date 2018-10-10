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

// socket.io 모듈
const socketio = require('socket.io');

// CORS
const cors = require('cors');

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

// cors를 미들웨어로 등록
app.use(cors());

/*************************************************************************/
// Passport 사용 설정 (미들웨어)
app.use(passport.initialize());
app.use(passport.session());
app.use(flash());

const configPassport = require('./config/passport');
configPassport(app, passport);
/*************************************************************************/

/*************************************************************************/
// 뷰 엔진 설정
const viewEngine = 'ejs';
app.set('views', __dirname + '/views');
app.set('view engine', viewEngine);
console.log('뷰 엔진이 ' + viewEngine + '로 설정되었습니다.');
/*************************************************************************/

/*************************************************************************/
// 패스포트 관련 함수 라우팅
const userPassport = require('./routes/user_passport');
userPassport(app, passport);
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
const server = http.createServer(app).listen(app.get('port'), () => {
    console.log('======= 서버가 시작되었습니다. 포트 : ' + app.get('port') + ' =======');

    // 데이터베이스 연결
    database.init(app, config);
});

// socket.io 서버 시작
const io = socketio.listen(server);
console.log('socket.io 요청을 받아들일 준비가 되었습니다.');

// 로그인 아이디 매핑(로그인 아이디 -> 소켓 아이디)
let login_ids = {};

// connection 이벤트 발생 시
io.sockets.on('connection', (socket) => {
    console.log('connetion info : ', socket.request.connection._peername);

    // 소켓 객체에 클라이언트 Host, Port 정보 속성으로 추가
    socket.remoteAddress = socket.request.connection._peername.address;
    socket.remotePort = socket.request.connection._peername.port;

    // 'message' 이벤트
    socket.on('message', (message) => {
        console.log('message 이벤트를 받았습니다.');
        console.dir(message);

        if(message.recepient == 'ALL') { // 전체전송
            console.dir('모든 클라이언트에게 message 이벤트를 전송합니다.');
            io.sockets.emit('message', message);
        } else {
            // command 속성으로 채팅 유형 구분
            if(message.command == 'chat') {
                // 일대일 채팅
                if(login_ids[message.recepient]) {
                    io.sockets.connected[login_ids[message.recepient]].emit('message', message);
                    // 응답 메시지 전송
                    sendResponse(socket, 'message', '200', '메시지를 전송했습니다.');
                } else { // 사용자 없음
                    sendResponse(socket, 'login', '404', '상대방의 로그인 ID를 찾을 수 없습니다.');
                }
            } else if(message.command == 'groupchat') {
                if(message.room == null) {
                    sendResponse(socket, 'message', '200', '입장한 방이 없습니다.');
                } else if(io.sockets.adapter.rooms[message.room]) {
                    // 방에 들어 있는 모든 사용자에게 메시지 전달
                    io.sockets.in(message.room).emit('message', message);
                    sendResponse(socket, 'message', '200', '방 [' + message.room + '] 의 모든 사용자들에게 메시지를 전송했습니다.');
                } else {
                    sendResponse(socket, 'message', '200', '방이 존재하지 않습니다.');
                }
            }
        }
    });

    // 'login' 이벤트
    socket.on('login', (login) => {
        console.log('login 이벤트를 받았습니다.');
        console.dir(login);

        // 기존 클라이언트 ID가 없으면 클라이언트 ID를 앱에 추가
        console.log('접속한 소켓의 ID : ' + socket.id);
        login_ids[login.id] = socket.id;
        socket.login_id = login.id;

        console.log('접속한 클라이언트 ID 개수 : %d', Object.keys(login_ids).length);

        // 응답 메시지 전송
        sendResponse(socket, 'login', '200', '로그인되었습니다.');
    });

    // 'logout' 이벤트
    socket.on('logout', (logout) => {
        console.log('logout 이벤트를 받았습니다.');
        console.dir(logout);

        console.log('로그아웃한 ID : ' + socket.id);
        delete login_ids[logout.id];

        console.log('접속한 클라이언트 ID 개수 : %d', Object.keys(login_ids).length);

        // 응답 메시지 전송
        sendResponse(socket, 'login', '200', '로그아웃되었습니다.');
    });

    // 'room' 이벤트
    socket.on('room', (room) => {
        let output = { };
        console.log('room 이벤트를 받았습니다.');
        console.dir(room);

        if(room.command == 'create') {
            if(io.sockets.adapter.rooms[room.roomId]) { // 방이 이미 만들어져 있는 경우
                console.log('방이 이미 만들어져 있습니다.');

            } else {
                console.log('방을 새로 만듭니다.');
                
                socket.join(room.roomId);

                const curRoom = io.sockets.adapter.rooms[room.roomId];
                curRoom.id = room.roomId;
                curRoom.name = room.roomName;
                curRoom.owner = room.roomOwner;
            }
        } else if(room.command == 'update') {
            const curRoom = io.sockets.adapter.rooms[room.roomId];
            curRoom.id = room.roomId;
            curRoom.name = room.roomName;
            curRoom.owner = room.roomOwner;
        } else if(room.command == 'delete') {
            socket.leave(room.roomId);

            if(io.sockets.adapter.rooms[room.roomId]) { // 방이 만들어져 있는 경우
                delete io.sockets.adapter.rooms[room.roomId];
            } else {    // 방이 만들어져 있지 않은 경우
                console.log('방이 만들어져 있지 않습니다.');
            }
        } else if(room.command == 'join') {
            socket.join(room.roomId);

            output.room = room.roomId;

            // response
            sendResponse(socket, 'room', '200', '방에 입장하였습니다.');
        } else if(room.command == 'leave') {
            socket.leave(room.roomId);

            output.room = null;

            // response
            sendResponse(socket, 'room', '200', '방에서 나갔습니다.');
        }

        const roomList = getRoomList();

        output.command = 'list';
        output.rooms = roomList;

        console.log('클라이언트로 보낼 데이터 : ' + JSON.stringify(output));

        socket.emit('room', output);
    });
});

function sendResponse(socket, command, code, message) {
    const statusObj = {
        command : command
        , code : code
        , message : message
    };
    socket.emit('response', statusObj);
}

function getRoomList() {
    console.dir(io.sockets.adapter.rooms);

    let roomList = [ ];

    Object.keys(io.sockets.adapter.rooms).forEach((roomId) => {
        console.log('current room id : ' + roomId);
        const outRoom = io.sockets.adapter.rooms[roomId];

        // find default room using all attributes
        let foundDefault = false;
        let index = 0;
        Object.keys(outRoom.sockets).forEach((key) => {
            console.log('#' + index + ' : ' + key + ', ' + outRoom.sockets[key]);

            if(roomId == key) { // default room
                foundDefault = true;
                console.log('this is default room.');
            }
            index++;
        });

        if(!foundDefault) {
            roomList.push(outRoom);
        }
    });

    console.log('[ROOM LIST]');
    console.dir(roomList);

    return roomList;
}