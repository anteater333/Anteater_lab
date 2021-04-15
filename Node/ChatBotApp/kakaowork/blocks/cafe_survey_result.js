module.exports = (rating, wanted, time) => {
    
    return [
    {
    "type": "header",
    "text": "설문에 응해주셔서 감사합니다!",
    "style": "yellow"
    },
    {
    "type": "text",
    "text": "이모지는 PC 어플리케이션에서 몬생기게 나와서 안쓰려구여",
    "markdown": true
    },
    {
    "type": "text",
    "text": "*답변 내용*",
    "markdown": true
    },
    {
    "type": "description",
    "term": "평점",
    "content": {
        "type": "text",
        "text": rating,
        "markdown": false
    },
    "accent": true
    },
    {
    "type": "description",
    "term": "바라는 점",
    "content": {
        "type": "text",
        "text": wanted,
        "markdown": false
    },
    "accent": true
    },
    {
    "type": "description",
    "term": "시간",
    "content": {
        "type": "text",
        "text": time,
        "markdown": false
    },
    "accent": true
    }
]
}