// Monaco Editor가 CDN worker를 cross-origin 없이 실행할 수 있도록 blob URL 사용
const MONACO_CDN = 'https://cdn.jsdelivr.net/npm/monaco-editor@0.47.0/min/vs';

window.MonacoEnvironment = {
    getWorkerUrl: function (workerId, label) {
        return `data:text/javascript;charset=utf-8,${encodeURIComponent(
            `self.MonacoEnvironment = { baseUrl: '${MONACO_CDN}/' };
             importScripts('${MONACO_CDN}/base/worker/workerMain.min.js');`
        )}`;
    }
};

let _editor = null;
let _dotnetRef = null;

window.monacoInterop = {

    init(containerId, value, dotnetRef) {
        _dotnetRef = dotnetRef;

        require.config({ paths: { vs: MONACO_CDN } });

        require(['vs/editor/editor.main'], () => {
            const el = document.getElementById(containerId);
            if (!el) return;

            if (_editor) {
                _editor.dispose();
                _editor = null;
            }

            _editor = monaco.editor.create(el, {
                value: value ?? '',
                language: 'csharp',
                theme: 'vs-dark',
                automaticLayout: true,
                minimap: { enabled: false },
                fontSize: 13,
                lineHeight: 22,
                lineNumbers: 'on',
                scrollBeyondLastLine: false,
                wordWrap: 'off',
                tabSize: 4,
                insertSpaces: true,
                autoIndent: 'full',
                formatOnType: true,
                padding: { top: 10, bottom: 10 },
                renderLineHighlight: 'line',
                cursorBlinking: 'smooth',
                smoothScrolling: true,
                fontFamily: "'SFMono-Regular', Consolas, 'Liberation Mono', Menlo, monospace",
                scrollbar: {
                    verticalScrollbarSize: 6,
                    horizontalScrollbarSize: 6,
                },
            });

            _editor.onDidChangeModelContent(() => {
                const code = _editor.getValue();
                _dotnetRef.invokeMethodAsync('OnCodeChanged', code);
            });
        });
    },

    setValue(value) {
        if (_editor) {
            _editor.setValue(value ?? '');
        }
    },

    getValue() {
        return _editor ? _editor.getValue() : '';
    },

    dispose() {
        if (_editor) {
            _editor.dispose();
            _editor = null;
        }
        _dotnetRef = null;
    }
};
