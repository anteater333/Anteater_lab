const Config = require('config');

const axios = require('axios');
const kakaoInstance = axios.create({
    baseURL: 'https://api.kakaowork.com',
    headers: {
        Authorization: `Bearer ${Config.keys.kakaoWork.bot}`
    },
});

// https://docs.kakaoi.ai/kakao_work/webapireference/
// 참고해가며 작성

// 유저 목록 검색 /v1/users.list
exports.getUserList = async () => {
    const res = await kakaoInstance.get('/v1/users.list');
    return res.data.users;
}

// 채팅방 생성 /v1/conversations.open
exports.openConversations = async ({
    userId
}) => {
    const data = {
        user_id: userId,
    };
    const res = await kakaoInstance.post('/v1/conversations.open', data);
    return res.data.conversation
}

// 메시지 전송 /v1/messages.send
exports.sendMessage = async ({
    conversationId,
    text,
    blocks
}) => {
    const data = {
        conversation_id: conversationId,
        text,
        ...blocks && { blocks },
    };

    const res = await kakaoInstance.post('/v1/messages.send', data);
    return res.data.message;
}