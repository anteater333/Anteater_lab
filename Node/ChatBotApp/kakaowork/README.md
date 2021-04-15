# 필기노트  

영상 강의가 아니였다니...  

이 강좌에서는 "카카오워크 i 오픈빌더" 라는 챗봇 빌더를 통해 챗봇 제작을 수행한다.

서버 지칭하는 용어가 오락가락 하는 느낌인데, 의도한 건지...

## 1. 카카오워크 봇 소개
카카오워크의 메시지 말풍선은 단위 요소인 "블록 조합"을 통해 생성. 간단히 말해서, **말풍선 하나가 여러 개의 블럭**으로 이루어 진다고 생각할 수 있다.  

ex.  
```
Text Block          ---> 제목    ---> 다음 일정에 초대되었습니다.
Devider Block       ---> 구분선  ---> -------------------------
Text Block          ---> 제목    ---> [자유멘토링] ㅁㅁㅁ멘토
Description Block   ---> 설명    ---> 일시    17:00 ~ 19:00
Description Block   ---> 설명    ---> 장소    소마센터
Button Block        ---> 버튼    --->        [자세히보기]
```

커스텀 봇에는 시나리오 타입이라는게 존재.  
 * 알림형 : 단방향 성격의 정보 제공 및 알림 기능 수행
 * 반응형 : 응답, 승인 및 반려, 의견 전송, 투표 등 봇을 통해 특정 기능 수행 가능

봇은 카카오워크 상에서 작동하면서 봇 처리 서버로 API 요청을 보내 응답받는 역할.  

[[고객사 서버]] - [[카카오워크 서버]] - [[채팅방]]

Modal은 일종의 팝업 인풋 창

## 2. 봇 프로세스 및 활용 사례

알림형 커스텀 봇 프로세스  
 1. Bot 생성
 2. Bot 인증 (App Key 전송)
 3. 멤버 조회
 4. 채팅방 생성
 5. 알림형 대화 (조합형 말풍선)  

---

반응형 커스텀 봇 프로세스 (submit_action)  
 1. 메시지 전송 (Button Block: submit_action)
 2. 액션 버튼 클릭
 3. 액션 이벤트 전달 (Callback URL)
 4. 액션 이벤트에 따른 다음 시나리오 진행  

이 경우 고객사 서버는 action_type이 submit_modal인 버튼이 포함된 메시지를 발송해야 한다.  

---

반응형 커스텀 봇 프로세스 (call_modal)  
 1. 메시지 전송 (Button Block: call_modal)
 2. 액션 버튼 클릭
 3. 엑션 이벤트 전달 (Request URL)
 4. Modal 블록 정보 전송
 5. Modal 블록 정보 표시
 6. 정보 입력 & 액션 버튼 클릭
 7. Modal 이벤트 전달 (Callback URL)
 8. Modal 종료  

이번에는 당연히 action_type이 call_modal인 버튼이 메시지에 포함되어야한다.  

## 3. 샘플 봇
### 1. Giphy API를 이용한 짤방 봇
알림형 봇으로, Modal을 통해 알림 시간, 종류, 개수를 설정하면 시간에 맞춰 Giphy로 부터 짤방을 가져와준다.  
시나리오  
 1. 미리 지정해둔 dog, cat과 같은 키워드로 Giphy API 호출 후 결과 값 중 gif url을 파싱해서 서버에 저장해둔다.
 2. 사용자가 키워드, 알림, 개수 등을 지정해 봇에게 요청한다.
 3. 봇이 사용자의 조건에 맞춰서 gif 정보를 가져온다.
 4. 봇이 수집된 gif 정보를 사용자가 지정한 시간에 봇 메시지로 전송한다.  

3, 4번은 거의 동시 같긴 한데...  

---

### 2. 설문 봇
call_modal 방식 반응형 봇.  
시나리오  
 1. 발송기를 개발한다.
    - 설문내용 모달뷰 등록/수정
    - 메시지 발송
 2. 봇(서버)이 설문 메시지를 사용자에게 발송한다.
 3. 사용자가 설문에 대해 응답한다.
 4. 사용자의 응답을 저장하고 엑셀 다운로드를 지원한다.

```
{
	"text": "설문조사 이벤트",
		"blocks": [
			{
				"type": "header",
				"text": "☕ 사내 카페 만족도 조사 🥤",
				"style": "blue"
			},
			{
				"type": "text",
				"text": "어느덧 사내카페가 바뀐지 한달이 되었네요.😉 \n크루들이 카페를 이용하고 계신지 의견을 들어보고자 설문 조사를 진행해봅니다!! \n설문에 참여하면 푸짐한 경품 찬스가있으니 상품 꼭 받아가세요! 🎁",
				"markdown": true
			},
			{
				"type": "button",
				"action_type": "call_modal",
				"value":"number=A-23&cancle",
				"text": "설문 참여하기",
				"style": "default"
			}
		]
}
```

이렇게 json형식으로 설문 메시지를 전달, 마지막 button 블록이 call_modal  

---

### 3. 회의실 체크인 봇
사용자의 회의실 예약 시간에 맞춰 회의실 체크인 메시지를 전송하는 봇. 알림형식 같으면서도 submit_action이 포함되는 반응형 봇이다.  

시나리오  
 1. 회의실 예약 페이지 개발 (혹은 이미 존재)
 2. 봇(서버)이 예약 시간에 맞춰 체크인 메시지 발송
 3. 사용자가 버튼으로 체크인 여부 응답(체크인/예약취소)
 4. 사용자의 응답에 따라 체크인 확인/회의실 예약 취소

```
{
	"text": "Push alarm message",
		"blocks": [
			{
				"type": "image_link",
				"url": "https://t1.kakaocdn.net/kakaowork/resources/block-kit/imagelink/image2@3x.jpg"
			},
			{
				"type": "text",
				"text": "10분 후 회의실 예약이 있습니다. \n체크인 해주세요. \n\n 회의실 8층 A-23 \n 시간 11:00~12:00 ",
				"markdown": true
			},
			{
				"type": "action",
				"elements": [
					{ "type": "button",
					 "text": "예약취소",
					 "style": "primary",
					 "action_type": "submit_action",
					 "action_name": "cancle",
					 "value": "number=A-23"
					},
					{
						"type": "button",
						"text": "체크인",
						"style": "danger",
						"action_type": "submit_action",
						"action_name": "checkIn",
						"value": "number=A-23"
					}
				]
			}
		]
}
```

이미지 까지 포함된 멋진 메시지를 만들어서 보낼 수 있다.  
마지막 action 블록에 elements 속성을 줘서 Yes/No 버튼을 만든 부분 주목.  

## 4. 예제 프로젝트 실습

이제부터 본론에 들어간다고 볼 수 있다. 앞서 소개한 세 가지 예제 중 설문조사 봇을 만들어 보도록 한다.  

다음과 같은 시나리오를 따른다.  
 1. 설문 조사 메세지 전송
 2. 유저가 설문 작성
 3. 설문 완료 알림 전송

좀 더 풀어서 쓰면,  
 1. 유저에게 설문 전송
 2. request url을 통해 액션 이벤트를 받아 모달 뷰 정보를 반환
 3. callback url을 통해 사용자의 응답을 받아 설문 완료 메세지를 전송

시나리오에서 사용되는 메세지 유형은 총 두가지 (당연히 메세지 두 개 보내니까...)  
 * call_modal이 포함된 반응형 Modal (call_modal block이 포함된 말풍선과 call_modal로 불러와지게 될 Modal로 구성)  
 * 설문 후 완료 메세지를 보내기 위한 알림형 메세지  

하나하나 분석해보자.  

 1. 유저에게 설문 전송  

알림을 보낼 유저 ID 탐색 -> 해당 유저 ID를 통해 채팅방 생성 -> 생성된 채팅방에 알림 전송  

 2. request url을 통해 액션 이벤트를 받아 모달 뷰 정보를 반환  

유저가 설문조사 버튼을 클릭하면 카카오워크 서버가 request url에 등록한 url로 어떤 모달 뷰를 띄울 것인지 물어본다.  
모달의 형태는 json으로 만들어 response로 전달해줘야 한다.  
```
                        modal?
KakaoWork           ===Request==>         3rd Party

 Server             <==Response==          Server
                      modal.json
```

 3. callback url을 통해 사용자의 응답을 받아 설문 완료 메세지를 전송

유저가 설문을 완료하면 카카오서버가 응답 결과를 callback url로 보내 응답을 준다. POST 방식인듯. 여기서 서버측의 메소드가 request body를 읽고 설문 결과 취합 및 응답 메세지를 보낼 수 있는듯 하다.  

```
                     body: survey
KakaoWork           ===Request==>         3rd Party

 Server             <==Response==          Server
                     message.json
```

ajax는 ajax 라이브러리계의 스테디 셀러 axios를 사용한다.  
사실 나도 axios밖에 안써봐서 다른게 있는지는 모르겠지만  
config라는 라이브러리도 있다. 이런게 있는줄 알았으면 진작에 써올껄.  

```
const Config = require('config');

console.log(Config.keys.kakaoWork.bot); // [APP KEY] 출력
```
config 모듈 기억해두자.  

근데 어차피 실습하려면 구름IDE 환경에서 작업해야 하긴 함. 구름IDE가 생각보다 훨씬 대단한 환경이기 때문에... github pages같은 기능도 지원하는 것 같다. 일종의 임시 도메인  

메시지 블록/모달은 [카카오워크 Block Kit Builder](https://www.kakaowork.com/block-kit-builder)를 통해 손쉽게 만들 수 있다. 역시 이런건 WYSIWYG이 필수.  

이 강의를 통해 배우는 부분은 카카오워크 봇 만들기의 빙산의 일각에 불과하다. 예를 들어 button 블록의 action_type의 경우, 배운 것은 call_modal, submit_action 둘이지만, open_external_app, open_inapp_browser, open_system_browser 속성도 존재한다. 각 기능들은 뭐 이름에서 직관적으로 이해 가능.  

call_modal은 아무튼 value라는 속성이 추가로 필요하고, value 속성은 출력할 modal을 의미하는듯?  

---

## 5. 마무으리

아무튼 유용한 학습이었다. API도 잘 정리돼있고 멋들어진 빌더까지 있으니 개발이 크게 어렵진 않을듯. 중요한건 아이디어를 얼마나 기똥차게 만드느냐겠지만...  

https://docs.kakaoi.ai/kakao_work/  
https://www.kakaowork.com/block-kit-builder  
