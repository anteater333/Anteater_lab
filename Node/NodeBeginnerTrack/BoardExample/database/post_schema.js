const utils = require('../utils/utils');

let SchemaObj = {};

SchemaObj.createSchema = (mongoose) => {
    // 글 스키마 정의
    const PostSchema = mongoose.Schema({
        title : {type : String, trim : true, 'default' : ''} // 글 제목
        , contents : {type : String, trim : true, 'default' : ''} // 글 내용
        , writer : {type : mongoose.Schema.ObjectId, ref : 'users'} // 글쓴이
        , tags : {type : [ ], 'defaults' : ''} // 태그
        , created_at : {type : Date, index : {unique : false}, 'default' : Date.now} // 작성일
        , updated_at : {type : Date, index : {unique : false}, 'default' : Date.now} // 수정일
        , comments : [{ // 댓글
            contents : {type : String, trim : true, 'default' : ''} // 댓글 내용
            , writer : {type : mongoose.Schema.ObjectId, ref : 'users'} // 댓글 작성자
            , created_at : {type : Date, 'default' : Date.now} // 댓글 작성일
        }]
    });

    // 필수 속성의 'required' validation
    PostSchema.path('title').required(true, '글 제목을 입력하셔야 합니다.');
    PostSchema.path('contents').required(true, '글 내용을 입력하셔야 합니다.');

    PostSchema.methods = {
        // 글 저장
        savePost : function(callback) {
            const self = this;

            this.validate(function(err) {
                if(err) return callback(err);

                self.save(callback);
            });
        }

        // 댓글 추가
        , addComment : function(user, comment, callback) {
            this.comment.push({
                contents : comment.comments
                , writer : user._id
            });
            this.save(callback);
        }

        // 댓글 삭제
        , removeComment : function(id, callback) {
            const index = utils.indexOf(this.comments, {id : id});

            if(~index) {
                this.comments.splice(index, 1);
            } else {
                return callback('ID [' + id + ']를 가진 댓글 객체를 찾을 수 없습니다.');
            }
            this.save(callback);
        }
    }

    PostSchema.statics = {
        // ID로 글 찾기
    }
}

