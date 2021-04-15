const express = require('express');
const router = express.Router();

// 미리 정의해둔 services
const libKakaoWork = require('../libs/kakaoWork')

// 보기에 복잡해서 분리해놓은 message block 코드
const surveyBlocks = require('../blocks/caffe_survey');
const caffe_survey = require('../blocks/caffe_survey');
const cafe_survey_modal = require('../blocks/cafe_survey_modal');
const cafe_survey_result = require('../blocks/cafe_survey_result');

// controllers
// 설문조사 발송
router.get('/', async (req, res, next) => {
  // 1. 유저 목록 검색 => users
  const users = await libKakaoWork.getUserList();

  // 2. 검색된 모든 유저에게 각각 채팅방 생성 => conversations
  const conversations = await Promise.all(
    users.map((user) => libKakaoWork.openConversations({ userId: user.id}))
  );

  // 생성된 채팅방에 메세지 전송
  const messages = await Promise.all([
    conversations.map((conversation) => libKakaoWork.sendMessage({
      conversationId: conversation.id,
      text: '설문조사 이벤트',
      blocks: caffe_survey,
    }))
  ]);

  // 응답
  res.json({
    users,
    conversations,
    messages
  })
});

// 설문조사 modal 발송
router.post('/request', async (req, res, next) => {
  const { message, value } = req.body;

  switch (value) {
    case 'cafe_survey':
      // cafe_survey_modal 반환
      return res.json({view: cafe_survey_modal})
      break;  // ?
    default:
  }

  res.json({});
});

// 설문조사 완료 후 처리
// 시나리오에 따르면 req.body.value === cafe_survey_results
router.post('/callback', async (req, res, next) => {
  const { message, actions, action_time, value } = req.body;

  switch (value) {
    case 'cafe_survey_results':
      await libKakaoWork.sendMessage({
        conversationId: message.conversation_id,
        text: '설문조사에 응해주셔서 감사합니다.',
        blocks: cafe_survey_result(actions.rating, actions.wanted, action_time)
      })
      break;
    default:
  }

  res.json({ reesult: true })
})

module.exports = router;