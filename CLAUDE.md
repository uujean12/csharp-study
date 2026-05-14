# CLAUDE.md

This file provides guidance to Claude Code (claude.ai/code) when working with code in this repository.

## 프로젝트 개요

C# 알고리즘 공부 기록을 웹 대시보드로 시각화하는 프로젝트.

- **AlgorithmStudy** — C# 콘솔. 알고리즘 구현 및 실행
- **AlgorithmDashboard** — ASP.NET Core Blazor Web App. 잔디 캘린더 + 알고리즘 블로그 대시보드
- **data/records.json** — 날짜별 공부 기록. 대시보드의 유일한 데이터 소스

## 주요 명령어

```bash
# 전체 빌드
dotnet build CsharpAlgorithmStudy.slnx

# 알고리즘 콘솔 실행
dotnet run --project AlgorithmStudy

# 웹 대시보드 실행
dotnet run --project AlgorithmDashboard

# 특정 프로젝트만 빌드
dotnet build AlgorithmStudy
dotnet build AlgorithmDashboard
```

## 솔루션 구조

```
csharp-algorithm/
├── CsharpAlgorithmStudy.slnx   ← 솔루션 파일 (.NET 10 신규 포맷)
├── AlgorithmStudy/              ← net10.0 콘솔, nullable+implicit usings 활성화
├── AlgorithmDashboard/          ← net10.0 Blazor Web App (Interactive Server rendering)
├── data/
│   └── records.json             ← 공부 기록 (스키마는 아래 참조)
└── CsharpAlgorithm/             ← 초기 스캐폴드 프로젝트 (무시해도 됨)
```

## records.json 스키마

```json
{
  "records": [
    {
      "date": "YYYY-MM-DD",
      "studyMinutes": 60,
      "notes": "자유 메모",
      "algorithms": [
        {
          "id": "kebab-case-id",
          "title": "한글 제목",
          "category": "sorting | search | graph | dp | etc",
          "tags": ["tag1", "tag2"],
          "description": "알고리즘 설명 (마크다운 가능)",
          "complexity": { "time": "O(...)", "space": "O(...)" },
          "sourceFile": "AlgorithmStudy/Category/FileName.cs"
        }
      ]
    }
  ]
}
```

## 아키텍처 핵심

- **데이터 흐름**: `records.json` → Blazor 서비스 레이어가 파싱 → 컴포넌트에 바인딩
- **잔디 캘린더**: `date` 필드 집계 → 날짜별 공부 여부/강도 표시
- **알고리즘 블로그**: `algorithms[].description`을 마크다운으로 렌더링, `sourceFile`로 소스 코드 연결
- Blazor Interactive Server 모드 사용 — JSON 파일을 서버에서 직접 읽을 수 있음

## 컨벤션

- nullable reference types 활성화 (`<Nullable>enable</Nullable>`) — null-forgiving 연산자(`!`) 최소화
- implicit usings 활성화 — `System`, `System.Linq` 등 명시적 `using` 불필요
- 알고리즘 파일은 카테고리별 폴더로 구성: `AlgorithmStudy/Sorting/`, `AlgorithmStudy/Graph/` 등
