<h1 align="center">🔥원티드 프리온보딩 프론트엔드 챌린지🔥</h1>

이 문서는 2023년 7월 말 신청한 원티드의 온라인 강의에서 내준 사전 과제에 대해 작성한 문서이다.

# 과제 내용

```markdown
# 원티드 프리온보딩 프론트엔드 챌린지 8월 사전과제

## 리액트 리팩토링 실전가이드: 테스트부터 최적화까지

### 과제 제출 방법

- 사전과제는 테스트와 최적화에 대한 배경지식입니다.
- 아래 질문에 답변을 작성해서 Issue로 올려주시면 됩니다

### 테스트

1. 유닛테스트 vs 통합테스트 vs E2E테스트를 비교하여 설명해주세요
2. 리액트 테스트에 사용되는 도구들을 비교하여 설명해주세요

### 최적화

1. CDN(Content Distributed Network)에 대해 설명해주세요
2. Web Vitals에 대해 설명해주세요
3. Lighthouse에 대해 설명해주세요
```

# 과제 답변

## 테스트

### 1. 유닛테스트 vs 통합테스트 vs E2E테스트를 비교하여 설명해주세요

<p align="center">
  <img width="512" alt="테스트 피라미드" src="https://i.postimg.cc/xTX4h905/testing.png"/>
</p>

#### 유닛 테스트

유닛(Unit, 단위) 테스트는 프로그램을 작은 단위로 쪼개서 각 부분들이 제대로 동작하는지 확인하는 테스트입니다.

<p align="center">
  <img alt="유닛" src="https://i.postimg.cc/2y0Y7Wpr/image.png"/>
  <br/>
  데이터 검증과 관련된 로직이 담긴 모듈과 모듈에 대한 테스트 코드
</p>

유닛 테스트에선 프로그램의 가장 작은 단위, 모듈에 대한 테스트가 수행됩니다. 모듈의 동작(메소드)을 개별적으로 실행해 그 결과가 작성한 테스트 케이스에 부합하는지 확인하는 방식으로 진행합니다. 테스트 유형 중 가장 개별 테스트의 규모가 작지만, 그만큼 테스트 케이스의 수가 많습니다.

#### 통합 테스트

통합(Integration) 테스트는 유닛 테스트보다 더 큰 범위에 대한 테스트로, 단일 모듈을 넘어 둘 이상의 모듈이 통합되었을 때 잘 동작하는지 확인하는 테스트입니다. 통합 테스트를 통해 외부 라이브러리의 동작에 대한 검증 또한 진행할 수 있습니다.

#### E2E 테스트

E2E(End to End) 테스트 혹은 기능(Functional) 테스트는 개발된 프로그램을 사용자 관점에서 처음부터 끝까지 테스트하는 방법을 뜻합니다. 모듈 단위를 넘어 전체 프로그램의 동작의 무결성을 확인하게 됩니다.

대신 실제 사용자의 시나리오를 검증해야 한다는 점에서 위 테스트 유형 중 가장 고비용의 작업이 수행되어야 하며, 테스트 케이스의 수는 그만큼 적어지게 됩니다.

### 2. 리액트 테스트에 사용되는 도구들을 비교하여 설명해주세요

<p align="center">
  <img alt="도구" src="https://i.postimg.cc/52Q1tkHb/image.png" style="border-radius: 32px"/>
</p>

#### Jest

리액트는 자바스크립트 라이브러리로 리액트 프로젝트를 테스팅하기 위해선 별도의 자바스크립트 테스트 라이브러리가 필요합니다. Jest(`jest`)는 대표적인 자바스크립트 테스트 라이브러리로, 테스트 코드를 실행하는 테스트 러너, 테스트 대역(Test Double) 생성 등의 역할을 수행할 수 있습니다.

#### React Testing Library

웹 어플리케이션 테스트의 경우 브라우저에 표시될 컴포넌트들의 상태에 대해 인식할 수 있어야 합니다. React Testing Library(`@testing-library/react`)는 리액트 컴포넌트에 의한 DOM 동작을 테스팅 할 수 있는 라이브러리입니다. 컴포넌트가 화면상에 잘 렌더링 되었는지, UI에 대한 이벤트가 잘 발생하였는지 등을 테스팅 할 수 있습니다.

## 최적화

### 1. CDN(Content Distributed Network)에 대해 설명해주세요

<p align="center">
  <img width="512" alt="CDN" src="https://i.postimg.cc/BvG4hCVB/cdn.png"/>
</p>

CDN은 네트워크로부터 물리적으로 떨어져 있는 사용자에게 콘텐츠를 더 빠르게 제공하기 위해 제안된 기술입니다. 마치 현실의 물류창고와 같이 서비스에 사용될 리소스를 미리 원격지 서버에 캐시해 사용자가 빠르게 서비스의 콘텐츠를 얻을 수 있도록 만들어줍니다.

속도 측면의 장점과 더불어 큰 리소스를 처리할 때 발생하는 병목현상과 서버에 가해지는 부하를 줄여 서비스 운영에 도움을 주는 기술입니다.

### 2. Web Vitals에 대해 설명해주세요

<p align="center">
  <img alt="webVitals" src="https://i.postimg.cc/2ykq8HVz/webvitals.png"/>
</p>

Web Vitals는 웹에서 사용자 경험 품질을 측정할 수 있도록 구글에서 제안한 정량적 지표입니다. 위와 같은 지표들이 존재하지만 구글은 현대 웹에서 가장 주요한 세 가지 지표를 별도로 Core Web Vitals로 소개하고 있습니다.

<p align="center">
  <img alt="coreWebVitals" src="https://i.postimg.cc/jSxrgGBq/core-Web-Vitals.png"/>
</p>

#### Largest Contentful Paint

LCP는 페이지가 처음으로 로드를 시작한 시점을 기준으로 뷰포트 내에 있는 가장 큰 이미지/텍스트 블록이 렌더링된 시간을 의미합니다. 웹 페이지의 **로딩**에 대한 수준을 나타내며, LCP가 2.5초 이내에 발생해야 준수한 웹 페이지로 판단됩니다.

#### First Input Delay

FID는 사용자가 페이지에서 처음으로 클릭 등으로 상호 작용할 때부터 그 응답으로 이벤트 핸들러의 처리가 시작되기까지의 시간을 의미합니다. FID는 웹 페이지의 **상호 작용**에 대한 수준을 나타냅니다. 응답 지연을 100ms 이하로 발생시켜야 준수한 웹 페이지로 판단됩니다.

#### Cumulative Layout Shift

CLS는 웹 페이지의 **시각적 안정성**에 대한 수준을 나타내는 지표입니다. 시각적 안정성이란 현재 웹 페이지에서 발생한 레이아웃의 변경이 사용자에게 불편함을 유발하지 않아야 함을 의미합니다. CLS는 레이아웃 이동 점수(layout shift score)라는 기준을 통해 계산되며, CLS 점수가 0.1 이하여야 준수한 웹 페이지로 판단됩니다.

### 3. Lighthouse에 대해 설명해주세요

<p align="center">
  <img alt="lighthouse" src="https://i.postimg.cc/6pW2CVx7/lighthouse.png"/>
</p>

[Chrome 확장 프로그램](https://chrome.google.com/webstore/detail/lighthouse/blipmdconlkpinefehnmjammfjpmpbjk?hl=ko)  
[Firefox Add-on](https://addons.mozilla.org/ko/firefox/addon/google-lighthouse/)

Lighthouse는 구글에서 만든 웹 페이지의 성능 측정 도구입니다.

<p align="center">
  <img alt="lighthouse-sample" src="https://i.postimg.cc/qvHsC6jS/image.png"/>
</p>

<p align="center"><a href="https://googlechrome.github.io/lighthouse/viewer/?psiurl=https%3A%2F%2Fblog.anteater-lab.link%2F&strategy=mobile&category=performance&category=accessibility&category=best-practices&category=seo&category=pwa&utm_source=lh-chrome-ext" target="_blank">개인 블로그 활용 리포트 샘플</a></p>

Lighthouse를 사용하면 Web Vitals에 기반해 웹 페이지에 대한 리포트를 생성할 수 있습니다. 전체적인 점수와 개선 사항에 대한 인사이트를 간편하게 생성해 주는 도구입니다.
