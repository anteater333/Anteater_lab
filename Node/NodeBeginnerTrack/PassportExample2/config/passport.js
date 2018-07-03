const local_login = require('./passport/local_login');
const local_signup = require('./passport/local_signup');

module.exports = (app, passport) => {
    console.log('config/passport 호출됨.');

    // 세션 저장
    passport.serializeUser((user, done) => {    // 사용자 인증에 성공했을 때 호출
        console.log('serializeUser() 호출됨.');
        console.log(user);

        done(null, user);
    });

    // 세션 복원
    passport.deserializeUser((user, done) => {  // 인증 후 요청이 있을 때마다 호출
        console.log('deserializeUser() 호출됨.');
        console.dir(user);

        done(null, user);
    });

    // 인증 방식 설정
    passport.use('local-login', local_login);
    passport.use('local-signup', local_signup);
    
};