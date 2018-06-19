const login = (req, res) => {
	console.log('user 모듈 안에 있는 login 호출됨.');

    // 로그인 처리
    let paramId = req.param('id');
    let paramPassword = req.param('password');

    console.log('요청 파라미터 : ' + paramId + ', ' + 'PASSWORD');

    // 데이터베이스 객체 참조
    let database = req.app.get('database');

    if (database.db) {
        authUser(database, paramId, paramPassword, (err, docs) => {
            if (err) {
                console.error('사용자 로그인 중 에러 발생 : ' + err.stack);
                    
                res.writeHead('200', {'Content-Type':'text/html;charset=utf8'});
                res.write('<h2>사용자 로그인 중 에러 발생</h2>');
                res.write('<p>' + err.stack + '</p>');
                res.end();
                
                return;
            }

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
};

const adduser = (req, res) => {
	console.log('user 모듈 안에 있는 adduser 호출됨.');

    let paramId = req.body.id || req.query.id;
    let paramPassword = req.body.password || req.query.password;
    let paramName = req.body.name || req.query.name;

    console.log('요청 파라미터 : ' + paramId + ', ' + 'PASSWORD' + ', ' + paramName);

    // 데이터베이스 객체 참조
    let database = req.app.get('database');

    if (database.db) {
        addUser(database, paramId, paramPassword, paramName, (err, result) => {
            if (err) {
                console.error('사용자 추가 중 에러 발생 : ' + err.stack);
                
                res.writeHead('200', {'Content-Type':'text/html;charset=utf8'});
				res.write('<h2>사용자 추가 중 에러 발생</h2>');
                res.write('<p>' + err.stack + '</p>');
				res.end();
                
                return;
            }

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
};

const listuser = (req, res) => {
	console.log('user 모듈 안에 있는 listuser 호출됨.');

    // 데이터베이스 객체 참조
	let database = req.app.get('database');
    
    // 데이터베이스 객체가 초기화된 경우, 몯레 객체의 findAll 메소드 호출
    if(database.db) {
        // 1. 모든 사용자 검색
        database.UserModel.findAll((err, results) => {
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
};

// 사용자 인증
const authUser = (database, id, password, callback) => {
    console.log('authUser 호출됨.');

    // 아이디와 비밀번호를 사용해 검색
    database.UserModel.findById(id, (err, results) => {
        if(err) {
            callback(err, null);
            return;
        }

        console.log('아이디 [%s]로 사용자 검색 결과', id);
        console.dir(results);

        if(results.length > 0) {
            console.log('아이디 [%s]가 일치하는 사용자 찾음.', id);

            let user = new database.UserModel({id: id});
            let authenticated = user.authenticate(password, results[0]._doc.salt, results[0]._doc.hashed_password);
            
            if(authenticated) {
                console.log('비밀번호 일치함');
                callback(null, results);
            } else {
                console.log("비밀번호 일치하지 않음");
                callback(null, null);
            }
        } else {
            console.log("일치하는 사용자를 찾지 못함.");
            callback(null, null);
        }
    });
};

// 사용자 추가.. 어디가 수정된지는 모르겠음.
const addUser = (database, id, password, name, callback) => {
    console.log('addUser 호출됨.');

    // UserModel의 인스턴스 생성
    let user = new database.UserModel({"id" : id, "password" : password, "name" : name});

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

module.exports.login = login;
module.exports.adduser = adduser;
module.exports.listuser = listuser;