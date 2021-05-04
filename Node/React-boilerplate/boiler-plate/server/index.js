const express = require('express');
const app = express();
const port = 5000;

const { User } = require('./models/User');

const bodyParser = require('body-parser');
const cookieParser = require('cookie-parser');

const mongoose = require('mongoose');

const config = require('./config/key');

const { auth } = require('./middleware/auth');

mongoose
    .connect(config.mongoURI, {
        useNewUrlParser: true,
        useUnifiedTopology: true,
        useCreateIndex: true,
        useFindAndModify: false,
    })
    .then(() => console.log('mongoDB connected'))
    .catch((err) => console.log(err));

// application/x-www-form-urlencoded 분석
app.use(bodyParser.urlencoded({ extended: true }));

// application/json 분석
app.use(bodyParser.json());

app.use(cookieParser());

app.get('/', (req, res) => res.send('Hello World!!'));

app.post('/api/users/register', async (req, res) => {
    // 회원 가입에 필요한 정보들을 client에서 가져오면
    // 그것들을 데이터베이스에 넣어준다.

    /**
     * req.body를 읽는 것은 bodyParser의 힘
     */
    const user = new User(req.body);
    try {
        await user.save();
        return res.status(200).json({
            success: true,
        });
    } catch (err) {
        console.log(err)
        return res.json({ success: false, err });
    }
});

app.post('/api/users/login', (req, res) => {
    // 요청된 email을 database에서 있는지 찾는다.
    User.findOne({ email: req.body.email }, (err, user) => {
        if (!user) {
            return res.json({
                loginSuccess: false,
                message: 'No such email',
            });
        }

        // 요청된 email이 database에 있다면 비밀번호가 맞는 비밀번호인지 확인한다.
        user.comparePassword(req.body.password, (err, isMatch) => {
            if (!isMatch) {
                return res.json({
                    loginSuccess: false,
                    message: 'Wrong password.',
                });
            }

            // 비밀번호까지 맞다면 token을 생성한다.  callback 지옥 끔찍
            user.generateToken((err, user) => {
                if (err) return res.status(400).send(err);

                // 토큰을 저장한다. 쿠키 or 로컬스토리지 or Etc
                res.cookie('x_auth', user.token)
                    .status(200)
                    .json({ loginSuccess: true, userId: user._id });
            });
        });
    });
});

app.get('/api/users/auth', auth, (req, res) => {
    // auth 미들웨어를 통과해 왔음 (Authentication === true)
    res.status(200).json({
        _id: req.user._id,
        isAdmin: req.user.role === 0 ? false : true,
        isAuth: true,
        email: req.user.email,
        name: req.user.name,
        lastname: req.user.lastname,
        role: req.user.role,
        image: req.user.image,
    });
});

app.get('/api/users/logout', auth, (req, res) => {
    User.findOneAndUpdate(
        {
            _id: req.user._id,
        },
        {
            token: '',
        },
        (err, user) => {
            if (err) return res.json({ success: false, err });

            return res.status(200).send({
                success: true,
            });
        }
    );
});

app.get('/api/hello', (req, res) => {
    res.send('Hello');
});

app.listen(port, () =>
    console.log(`Web Application Server listening on port ${port}!`)
);
