const LocalStrategy = require('passport-local').Strategy;

// 로그인 Strategy

module.exports = new LocalStrategy({
    usernameField : 'email'
    , passwordField : 'password'
    , passReqToCallback : true
}, (req, email, password, done) => {
    console.log('passport의 local-login 호출됨 : ' + email + ', ' + 'PASSWORD');
    
    const database = req.app.get('database');
    database.UserModel.findOne({'email' : email}, (err, user) => {
        if(err) {return done(err);}

        // 등록된 사용자가 없는 경우
        if(!user) {
            console.log('계정이 일치하지 않음.');
            return done(null, false, req.flash('loginMessage', '등록된 계정이 없습니다.'));
        }

        // 비밀번호를 비교하여 맞지 않는 경우
        const authenticated = user.authenticate(password, user._doc.salt, user._doc.hashed_password);
        if (!authenticated) {
            console.log('비밀번호 일치하지 않음.');
            return done(null, false, req.flash('loginMessage', '비밀번호가 일치하지 않습니다.'));
        }
        
        // 로그인 성공
        console.log('계정과 비밀번호가 일치함.');
        return done(null, user);
    });
});