const LocalStrategy = require('passport-local').Strategy;

// 회원가입 Strategy

module.exports = new LocalStrategy({
    usernameField : 'email'
    , passwordField : 'password'
    , passReqToCallback : true
}, (req, email, password, done) => {
    // 요청 파라미터 중 name 파라미터 확인
    const paramName = req.body.name || req.query.name;
    console.log('passport의 local-signup 호출됨 : ' + email + ', ' + 'PASSWORD' + ', ' + paramName);

    // User.findOne이 blocking되므로 async 방식으로 변경할 수도 있음
    process.nextTick(() => {
        const database = req.app.get('database');
        database.UserModel.findOne({'email' : email}, (err, user) => {
            // 오류
            if(err) {
                return done(err);
            }

            // 이미 있는 이메일
            if(user) {
                console.log('이미 등록된 계정.');
                return done(null, false, req.flash('signupMessage', '계정이 이미 있습니다.'));
            } else {
                // 유저 정보를 모델 인스턴스 객체에 저장
                const user = new database.UserModel({'email' : email, 'password' : password, 'name' : paramName});
                user.save((err) => {
                    if(err) {throw err;}
                    console.log("사용자 데이터 추가함.");
                    return done(null, user);
                });
            }
        });
    });
})