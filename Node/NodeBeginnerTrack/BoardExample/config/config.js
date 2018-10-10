
module.exports = {
    server_port : 3000

    // database
    , db_url : 'mongodb://localhost:27017/local'
    , db_schemas: [ // Array
        {file:'./user_schema', collection:'users6', schemaName:'UserSchema', modelName:'UserModel'}
        , {file:'./post_schema', collection:'post', schemaName:'PostSchema', modelName:'PostModel'}
    ]
    
    // route
    , route_info : [
        {file : './post', path : '/process/addpost', method : 'addpost', type : 'post'}
        , {file : './post', path : '/process/showpost/:id', method : 'showpost', type : 'get'}
        , {file : './post', path : '/process/listpost/', method : 'listpost', type : 'post'}
        , {file : './post', path : '/process/listpost/', method : 'listpost', type : 'get'}
    ]

    // facebook API
    , facebook : {
        clientID : '663082074030370'
        , clientSecret : 'a4b44943fd47328c8213e8c0b1a31a09'
        , callbackURL : '/auth/facebook/callback'
    }
};