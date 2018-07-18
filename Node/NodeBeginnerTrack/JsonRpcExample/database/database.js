const mongoose = require('mongoose');

// database 객체에 db, schema, model 추가
let database = {};

database.init = (app, config) => {
    console.log('database.init() 호출됨.');

    connect(app, config);
};

function connect(app, config) {
    console.log('connect() 호출됨.');

    // 데이터베이스 연결 정보
    const databaseUrl = config.db_url;

    // 데이터베이스 연결
    console.log('데이터베이스 연결을 시도합니다.');
    mongoose.Promise = global.Promise;
    mongoose.connect(databaseUrl);
    database.db = mongoose.connection;  // database.db 주의

    // Events
    database.db.on('error', console.error.bind(console, 'mongoose connection error.'));
    database.db.on('open', () => {
        console.log('데이터베이스에 연결되었습니다. : ' + databaseUrl);
        
        // user 스키마 및 모델 객체 생성
        createSchema(app, config);
    });

    // 연결 끊어졌을 때 5초 후 재연결
    database.db.on('disconnected', () => {
        console.log('연결이 끊어졌습니다. 5초 후 다시 연결합니다.');
        setInterval(connectDB, 5000);
    });
}

// config에 정의한 스키마 및 모델 객체 생성

function createSchema(app, config) {
    let schemaLen = config.db_schemas.length;
    console.log('설정에 정의된 스키마의 수: %d', schemaLen);

    for (let i = 0; i <schemaLen; i++) {
        let curItem = config.db_schemas[i];

        // 모듈 파일에서 모듈 불러온 후 createSchema() 함수 호출하기
        let curSchema = require(curItem.file).createSchema(mongoose); // ex) (user_schema.js).createSchema(mongoose);
        console.log('%s 모듈을 불러들인 후 스키마 정의함.', curItem.file);

        // User 모델 정의
        let curModel = mongoose.model(curItem.collection, curSchema);
        console.log('%s 컬렉션을 위해 모델 정의함.', curItem.collection);

        // database 객체에 속성으로 추가
        database[curItem.schemaName] = curSchema; // dictionary 방식
        database[curItem.modelName] = curModel;
        console.log('스키마 이름 [%s], 모델 이름 [%s]이 database 객체의 속성으로 추가됨.',
                curItem.schemaName, curItem.modelName);
    }

    app.set('database', database);
    global.database = database;
    console.log('database 객체가 app 객체의 속성으로 추가됨.');
}

// database 객체를 module.exports에 할당
module.exports = database;