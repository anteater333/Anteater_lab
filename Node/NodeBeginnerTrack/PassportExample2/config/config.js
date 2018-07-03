
module.exports = {
    server_port : 3000

    // database
    , db_url : 'mongodb://localhost:27017/local'
    , db_schemas: [ // Array
        {file:'./user_schema', collection:'users6', schemaName:'UserSchema', modelName:'UserModel'}
    ]
    
    // route
    , route_info : [
    ]

    // facebook API
    , facebook : {
        clientID : '663082074030370'
        , clientSecret : 'a4b44943fd47328c8213e8c0b1a31a09'
        , callbackURL : '/auth/facebook/callback'
    }
};