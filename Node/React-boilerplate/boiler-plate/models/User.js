const mongoose = require('mongoose');

const bcrypt = require('bcrypt');
const saltRounds = 10; // salt 글자수

const jwt = require('jsonwebtoken');

const userSchema = mongoose.Schema({
    name: {
        type: String,
        maxlength: 50,
    },
    email: {
        type: String,
        trim: true, // Space 제거
        unique: true,
    },
    password: {
        type: String,
        minlength: 5,
    },
    lastname: {
        type: String,
        maxlength: 50,
    },
    role: {
        type: Number, // 0 : user, 1 : admin etc...
        default: 0,
    },
    image: String,
    token: {
        type: String,
    },
    tokenExp: {
        type: Number,
    },
});

// save() 메소드 전 hook
userSchema.pre('save', function (next) {
    const user = this; // user schema

    if (user.isModified('password')) {
        // 비밀번호를 암호화 시킨다.
        bcrypt.genSalt(saltRounds, function (err, salt) {
            if (err) return next(err);

            bcrypt.hash(user.password, salt, function (err, hash) {
                if (err) return next(err);

                user.password = hash;
                next();
            });
        });
    } else {
        next();
    }
});

userSchema.methods.comparePassword = function (plainPassword, callback) {
    bcrypt.compare(plainPassword, this.password, function (err, isMatch) {
        if (err) return callback(err);

        callback(null, isMatch);
    });
};

userSchema.methods.generateToken = function (callback) {
    const user = this;

    const token = jwt.sign(user._id.toHexString(), 'secretToken');

    user.token = token;
    user.save((err, user) => {
        if (err) return callback(err);
        callback(null, user);
    });
};

userSchema.statics.findByToken = function (token, callback) {
    const user = this;

    // decode token
    jwt.verify(token, 'secretToken', function (err, decoded) {
        user.findOne({ _id: decoded, token: token }, function (err, user) {
            if (err) return callback(err);

            callback(null, user);
        });
    });
};

const User = mongoose.model('User', userSchema);

module.exports = { User };
