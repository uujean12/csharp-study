# 🌱 C# 알고리즘 공부 대시보드

C#으로 알고리즘을 공부하고, 그 기록을 웹 대시보드로 시각화하는 프로젝트입니다.

## 화면 구성

| 영역 | 기능 |
|------|------|
| **상단 헤더** | 전체 진행률 표시 (완료 N / 26 개) |
| **잔디 캘린더** | GitHub 스타일 52주 × 7일 기여 그래프 |
| **왼쪽 패널** | 8단계 알고리즘 커리큘럼 아코디언 목록 |
| **가운데 패널** | 개념 작성 + Monaco 코드 에디터 + 채점 |
| **오른쪽 패널** | Claude AI 질문창 |

## 커리큘럼 (26개 알고리즘)

| 단계 | 주제 | 알고리즘 |
|------|------|----------|
| 1단계 | 정렬 | 버블, 선택, 삽입, 합병, 퀵, 힙 정렬 |
| 2단계 | 탐색 | 선형탐색, 이진탐색 |
| 3단계 | 자료구조 | 스택/큐, 링크드리스트, 트리(BST), 해시테이블, 힙 |
| 4단계 | 그래프 | DFS, BFS, 다익스트라 |
| 5단계 | DP | 피보나치, 배낭문제, LCS, LIS |
| 6단계 | 그리디 | 거스름돈, 활동 선택 |
| 7단계 | 분할정복 | 이진탐색 응용(Lower Bound), 합병정렬 응용 |
| 8단계 | 문자열 | KMP, 트라이 |

## 주요 기능

### 잔디 캘린더
- 최근 52주 공부 기록을 GitHub 스타일 격자로 표시
- 하루에 완료한 알고리즘 수에 따라 색상 강도 변화
- 마우스 오버 시 해당 날짜의 완료 알고리즘 목록 툴팁

### 코드 에디터 (Monaco Editor)
- VS Code와 동일한 Monaco Editor 적용
- C# 문법 강조 (Syntax Highlighting), 줄 번호, 자동 들여쓰기
- vs-dark 테마

### 키워드 기반 채점
- 제출 시 알고리즘별 핵심 키워드 포함 여부로 정답 판별
- 정답 시 잔디 캘린더에 자동 기록
- 모범 답안 보기 기능

### AI 질문창
- Anthropic Claude API 연동
- 현재 학습 중인 알고리즘 컨텍스트를 자동으로 포함하여 질문
- 대화 히스토리 유지

## 기술 스택

| 역할 | 기술 |
|------|------|
| 웹 프레임워크 | ASP.NET Core Blazor Web App (.NET 10, Interactive Server) |
| 코드 에디터 | Monaco Editor 0.47 (CDN + JS Interop) |
| AI | Anthropic Claude API (`claude-sonnet-4-6`) |
| 데이터 저장 | JSON 파일 (`data/progress.json`, `data/records.json`) |

## 프로젝트 구조

```
csharp-algorithm/
├── AlgorithmDashboard/          # Blazor 웹 대시보드
│   ├── Components/
│   │   ├── Pages/Home.razor     # 메인 페이지 (3열 레이아웃 전체)
│   │   └── ContributionStrip.razor
│   ├── Curriculum.cs            # 26개 알고리즘 정의 + 모범 답안
│   ├── Models/AlgorithmModels.cs
│   ├── Services/
│   │   ├── ProgressService.cs   # JSON 읽기/쓰기
│   │   └── ClaudeService.cs     # Claude API 호출
│   └── wwwroot/js/
│       └── monaco-interop.js    # Monaco Editor JS interop
├── data/
│   ├── progress.json            # 알고리즘별 개념/코드/완료 여부
│   └── records.json             # 날짜별 완료 알고리즘 (잔디 데이터)
└── CsharpAlgorithmStudy.slnx
```

## 실행 방법

### 사전 준비 — Claude API 키 설정

`AlgorithmDashboard/appsettings.json`에 API 키를 입력합니다.

```json
{
  "Claude": {
    "ApiKey": "sk-ant-...",
    "Model": "claude-sonnet-4-6"
  }
}
```

### 웹 대시보드 실행

```bash
dotnet run --project AlgorithmDashboard
```

브라우저에서 `http://localhost:5186` 접속.

### 빌드

```bash
dotnet build CsharpAlgorithmStudy.slnx
```
