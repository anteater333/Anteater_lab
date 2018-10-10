
module.exports = {
    server_port : 3000,

    // database
    db_url : 'mongodb://localhost:27017/local',
    db_schemas: [ // Array
        {file:'./user_schema', collection:'users5', schemaName:'UserSchema', modelName:'UserModel'}
    ],
    
    // route
    route_info : [
    ]
};