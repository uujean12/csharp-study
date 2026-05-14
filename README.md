# C# 알고리즘 공부 대시보드

C#으로 알고리즘을 공부하고, 그 기록을 웹 대시보드로 시각화하는 프로젝트입니다.

## 목표

- 매일 알고리즘 공부 기록을 GitHub 잔디처럼 캘린더로 표시
- 구현한 알고리즘을 블로그 형식으로 정리 (설명 + C# 코드 + 시각화)

## 프로젝트 구조

```
csharp-algorithm/
├── AlgorithmStudy/        # C# 콘솔 — 알고리즘 구현
├── AlgorithmDashboard/    # ASP.NET Core Blazor 웹 대시보드
├── data/
│   └── records.json       # 공부 기록 (날짜별 알고리즘 + 메모)
├── CsharpAlgorithmStudy.slnx
└── README.md
```

## 실행 방법

### 알고리즘 콘솔 실행

```bash
dotnet run --project AlgorithmStudy
```

### 웹 대시보드 실행

```bash
dotnet run --project AlgorithmDashboard
```

브라우저에서 `https://localhost:5001` 접속.

### 전체 빌드

```bash
dotnet build CsharpAlgorithmStudy.slnx
```

## 기록 방식

`data/records.json`에 날짜별로 공부한 알고리즘을 기록합니다.

```json
{
  "date": "2026-05-15",
  "studyMinutes": 60,
  "notes": "오늘 공부한 내용 메모",
  "algorithms": [
    {
      "id": "bubble-sort",
      "title": "버블 정렬",
      "category": "sorting",
      "tags": ["sorting", "beginner"],
      "description": "알고리즘 설명",
      "complexity": { "time": "O(n²)", "space": "O(1)" },
      "sourceFile": "AlgorithmStudy/Sorting/BubbleSort.cs"
    }
  ]
}
```

## 기술 스택

| 역할 | 기술 |
|------|------|
| 알고리즘 구현 | C# 콘솔 (.NET 10) |
| 웹 대시보드 | ASP.NET Core Blazor (.NET 10) |
| 데이터 저장 | JSON 파일 |
