module.exports = {
    "title": "까페 설문조사☕",
    "accept": "확인",
    "decline": "취소",
    "value": "cafe_survey_results",
    "blocks": [
      {
        "type": "label",
        "text": "*카페 평점을 알려주세요⭐*",
        "markdown": false
      },
      {
        "type": "select",
        "name": "rating",
        "options": [
          {
            "text": "⭐",
            "value": "1"
          },
          {
            "text": "⭐⭐",
            "value": "2"
          },
          {
            "text": "⭐⭐⭐",
            "value": "3"
          },
          {
            "text": "⭐⭐⭐⭐",
            "value": "4"
          },
          {
            "text": "⭐⭐⭐⭐⭐",
            "value": "5"
          }
        ],
        "required": true,
        "placeholder": "평점을 알려주세요"
      },
      {
        "type": "label",
        "text": "*바라는 점이 있다면 알려주세요!*",
        "markdown": true
      },
      {
        "type": "input",
        "name": "wanted",
        "required": false,
        "placeholder": "ex) 알바생이 너무 못생겼어요😢"
      }
    ]
  }