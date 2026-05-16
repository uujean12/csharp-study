using AlgorithmDashboard.Models;

namespace AlgorithmDashboard;

public static class Curriculum
{
    // ── 1단계: 정렬 ───────────────────────────────────

    static readonly AlgorithmDef BubbleSort = new("bubble-sort", "버블정렬",
        """
        static void BubbleSort(int[] arr)
        {
            int n = arr.Length;
            for (int i = 0; i < n - 1; i++)
            {
                for (int j = 0; j < n - 1 - i; j++)
                {
                    if (arr[j] > arr[j + 1])
                        (arr[j], arr[j + 1]) = (arr[j + 1], arr[j]);
                }
            }
        }

        // 사용 예시
        int[] arr = { 5, 3, 8, 1, 4 };
        BubbleSort(arr);
        Console.WriteLine(string.Join(", ", arr)); // 1, 3, 4, 5, 8
        """,
        ["arr[j]>arr[j+1]", "arr[j+1]"]);

    static readonly AlgorithmDef SelectionSort = new("selection-sort", "선택정렬",
        """
        static void SelectionSort(int[] arr)
        {
            int n = arr.Length;
            for (int i = 0; i < n - 1; i++)
            {
                int minIdx = i;
                for (int j = i + 1; j < n; j++)
                    if (arr[j] < arr[minIdx]) minIdx = j;
                (arr[i], arr[minIdx]) = (arr[minIdx], arr[i]);
            }
        }
        """,
        ["minidx", "arr[minidx]"]);

    static readonly AlgorithmDef InsertionSort = new("insertion-sort", "삽입정렬",
        """
        static void InsertionSort(int[] arr)
        {
            int n = arr.Length;
            for (int i = 1; i < n; i++)
            {
                int key = arr[i], j = i - 1;
                while (j >= 0 && arr[j] > key)
                    arr[j + 1] = arr[j--];
                arr[j + 1] = key;
            }
        }
        """,
        ["key=arr[i]", "while(j>=0"]);

    static readonly AlgorithmDef MergeSort = new("merge-sort", "합병정렬",
        """
        static void MergeSort(int[] arr, int left, int right)
        {
            if (left >= right) return;
            int mid = (left + right) / 2;
            MergeSort(arr, left, mid);
            MergeSort(arr, mid + 1, right);
            Merge(arr, left, mid, right);
        }

        static void Merge(int[] arr, int left, int mid, int right)
        {
            int[] temp = arr[left..(right + 1)];
            int i = 0, j = mid - left + 1, k = left;
            while (i <= mid - left && j < temp.Length)
                arr[k++] = temp[i] <= temp[j] ? temp[i++] : temp[j++];
            while (i <= mid - left) arr[k++] = temp[i++];
            while (j < temp.Length)  arr[k++] = temp[j++];
        }

        // 사용 예시
        int[] arr = { 5, 3, 8, 1, 4 };
        MergeSort(arr, 0, arr.Length - 1);
        """,
        ["mergesort(", "merge("]);

    static readonly AlgorithmDef QuickSort = new("quick-sort", "퀵정렬",
        """
        static void QuickSort(int[] arr, int left, int right)
        {
            if (left >= right) return;
            int pivot = Partition(arr, left, right);
            QuickSort(arr, left, pivot - 1);
            QuickSort(arr, pivot + 1, right);
        }

        static int Partition(int[] arr, int left, int right)
        {
            int pivot = arr[right], i = left - 1;
            for (int j = left; j < right; j++)
                if (arr[j] <= pivot)
                    (arr[++i], arr[j]) = (arr[j], arr[i]);
            (arr[i + 1], arr[right]) = (arr[right], arr[i + 1]);
            return i + 1;
        }
        """,
        ["partition(", "pivot"]);

    static readonly AlgorithmDef HeapSort = new("heap-sort", "힙정렬",
        """
        static void HeapSort(int[] arr)
        {
            int n = arr.Length;
            for (int i = n / 2 - 1; i >= 0; i--)
                Heapify(arr, n, i);
            for (int i = n - 1; i > 0; i--)
            {
                (arr[0], arr[i]) = (arr[i], arr[0]);
                Heapify(arr, i, 0);
            }
        }

        static void Heapify(int[] arr, int n, int i)
        {
            int largest = i, l = 2 * i + 1, r = 2 * i + 2;
            if (l < n && arr[l] > arr[largest]) largest = l;
            if (r < n && arr[r] > arr[largest]) largest = r;
            if (largest != i)
            {
                (arr[i], arr[largest]) = (arr[largest], arr[i]);
                Heapify(arr, n, largest);
            }
        }
        """,
        ["heapify(", "largest"]);

    // ── 2단계: 탐색 ───────────────────────────────────

    static readonly AlgorithmDef LinearSearch = new("linear-search", "선형탐색",
        """
        static int LinearSearch(int[] arr, int target)
        {
            for (int i = 0; i < arr.Length; i++)
                if (arr[i] == target) return i;
            return -1;
        }

        // 사용 예시
        int[] arr = { 4, 2, 7, 1, 9 };
        int idx = LinearSearch(arr, 7); // 2
        """,
        ["arr[i]==target"]);

    static readonly AlgorithmDef BinarySearch = new("binary-search", "이진탐색",
        """
        static int BinarySearch(int[] arr, int target)
        {
            int left = 0, right = arr.Length - 1;
            while (left <= right)
            {
                int mid = left + (right - left) / 2;
                if (arr[mid] == target) return mid;
                if (arr[mid] < target) left = mid + 1;
                else right = mid - 1;
            }
            return -1;
        }

        // 주의: 배열이 정렬되어 있어야 합니다!
        int[] arr = { 1, 3, 5, 7, 9 };
        int idx = BinarySearch(arr, 7); // 3
        """,
        ["left+(right-left)/2", "arr[mid]"]);

    // ── 3단계: 자료구조 ───────────────────────────────

    static readonly AlgorithmDef StackQueue = new("stack-queue", "스택 / 큐",
        """
        // ── 스택 (LIFO) ──
        var stack = new Stack<int>();
        stack.Push(1);
        stack.Push(2);
        stack.Push(3);
        int top = stack.Pop();   // 3 (마지막에 넣은 것)
        int peek = stack.Peek(); // 2

        // ── 큐 (FIFO) ──
        var queue = new Queue<int>();
        queue.Enqueue(1);
        queue.Enqueue(2);
        queue.Enqueue(3);
        int front = queue.Dequeue(); // 1 (처음에 넣은 것)
        int head  = queue.Peek();    // 2
        """,
        ["stack<", "queue<"]);

    static readonly AlgorithmDef LinkedList = new("linked-list", "링크드리스트",
        """
        class Node
        {
            public int Value;
            public Node? Next;
            public Node(int value) { Value = value; }
        }

        class MyLinkedList
        {
            Node? head;

            public void Add(int value)
            {
                var node = new Node(value);
                if (head == null) { head = node; return; }
                var cur = head;
                while (cur.Next != null) cur = cur.Next;
                cur.Next = node;
            }

            public bool Remove(int value)
            {
                if (head == null) return false;
                if (head.Value == value) { head = head.Next; return true; }
                var cur = head;
                while (cur.Next != null)
                {
                    if (cur.Next.Value == value) { cur.Next = cur.Next.Next; return true; }
                    cur = cur.Next;
                }
                return false;
            }

            public void Print()
            {
                for (var cur = head; cur != null; cur = cur.Next)
                    Console.Write(cur.Value + " ");
            }
        }
        """,
        [".next", "head"]);

    static readonly AlgorithmDef Tree = new("tree", "트리 (BST)",
        """
        class TreeNode
        {
            public int Value;
            public TreeNode? Left, Right;
            public TreeNode(int value) { Value = value; }
        }

        class BST
        {
            TreeNode? root;

            public void Insert(int value) => root = Insert(root, value);
            TreeNode Insert(TreeNode? node, int value)
            {
                if (node == null) return new TreeNode(value);
                if (value < node.Value) node.Left  = Insert(node.Left,  value);
                else if (value > node.Value) node.Right = Insert(node.Right, value);
                return node;
            }

            public bool Search(int value) => Search(root, value);
            bool Search(TreeNode? node, int value)
            {
                if (node == null) return false;
                if (node.Value == value) return true;
                return value < node.Value
                    ? Search(node.Left, value)
                    : Search(node.Right, value);
            }

            // 중위순회 (InOrder) → 정렬된 순서 출력
            void InOrder(TreeNode? node)
            {
                if (node == null) return;
                InOrder(node.Left);
                Console.Write(node.Value + " ");
                InOrder(node.Right);
            }
        }
        """,
        ["treenode", "node.left"]);

    static readonly AlgorithmDef HashTable = new("hash-table", "해시테이블",
        """
        // C#의 Dictionary<TKey, TValue>가 해시테이블입니다.
        var table = new Dictionary<string, int>();

        // 삽입
        table["apple"]  = 5;
        table["banana"] = 3;
        table["cherry"] = 8;

        // 조회
        if (table.ContainsKey("apple"))
            Console.WriteLine(table["apple"]); // 5

        // TryGetValue (더 안전한 방법)
        if (table.TryGetValue("banana", out int val))
            Console.WriteLine(val); // 3

        // 삭제
        table.Remove("cherry");

        // 순회
        foreach (var (key, value) in table)
            Console.WriteLine($"{key}: {value}");
        """,
        ["dictionary<", "containskey"]);

    static readonly AlgorithmDef HeapDS = new("heap", "힙 (Min-Heap)",
        """
        class MinHeap
        {
            readonly List<int> heap = new();

            public void Push(int value)
            {
                heap.Add(value);
                int i = heap.Count - 1;
                while (i > 0)
                {
                    int parent = (i - 1) / 2;
                    if (heap[parent] <= heap[i]) break;
                    (heap[parent], heap[i]) = (heap[i], heap[parent]);
                    i = parent;
                }
            }

            public int Pop()
            {
                int min = heap[0];
                heap[0] = heap[^1];
                heap.RemoveAt(heap.Count - 1);
                int i = 0;
                while (true)
                {
                    int l = 2*i+1, r = 2*i+2, s = i;
                    if (l < heap.Count && heap[l] < heap[s]) s = l;
                    if (r < heap.Count && heap[r] < heap[s]) s = r;
                    if (s == i) break;
                    (heap[i], heap[s]) = (heap[s], heap[i]);
                    i = s;
                }
                return min;
            }

            public int Peek() => heap[0];
            public int Count => heap.Count;
        }
        """,
        ["(i-1)/2", "2*i+1"]);

    // ── 4단계: 그래프 ─────────────────────────────────

    static readonly AlgorithmDef DFS = new("dfs", "DFS (깊이 우선 탐색)",
        """
        // 재귀 DFS
        void DFS(int node, bool[] visited, List<int>[] graph)
        {
            visited[node] = true;
            Console.Write(node + " ");
            foreach (int next in graph[node])
                if (!visited[next])
                    DFS(next, visited, graph);
        }

        // 사용 예시
        int n = 5;
        var graph = new List<int>[n];
        for (int i = 0; i < n; i++) graph[i] = new List<int>();
        graph[0].Add(1); graph[0].Add(2);
        graph[1].Add(3); graph[2].Add(4);

        bool[] visited = new bool[n];
        DFS(0, visited, graph); // 0 1 3 2 4
        """,
        ["visited[node]", "dfs("]);

    static readonly AlgorithmDef BFS = new("bfs", "BFS (너비 우선 탐색)",
        """
        void BFS(int start, List<int>[] graph)
        {
            bool[] visited = new bool[graph.Length];
            var queue = new Queue<int>();
            queue.Enqueue(start);
            visited[start] = true;

            while (queue.Count > 0)
            {
                int node = queue.Dequeue();
                Console.Write(node + " ");
                foreach (int next in graph[node])
                {
                    if (!visited[next])
                    {
                        visited[next] = true;
                        queue.Enqueue(next);
                    }
                }
            }
        }
        """,
        ["queue.enqueue(", "queue.dequeue("]);

    static readonly AlgorithmDef Dijkstra = new("dijkstra", "다익스트라",
        """
        int[] Dijkstra(int start, int n, List<(int to, int w)>[] graph)
        {
            int[] dist = Enumerable.Repeat(int.MaxValue, n).ToArray();
            dist[start] = 0;
            var pq = new PriorityQueue<int, int>();
            pq.Enqueue(start, 0);

            while (pq.Count > 0)
            {
                pq.TryDequeue(out int u, out int d);
                if (d > dist[u]) continue;  // 오래된 항목 스킵
                foreach (var (v, w) in graph[u])
                {
                    if (dist[u] + w < dist[v])
                    {
                        dist[v] = dist[u] + w;
                        pq.Enqueue(v, dist[v]);
                    }
                }
            }
            return dist;
        }
        """,
        ["dist[", "priorityqueue<"]);

    // ── 5단계: DP ─────────────────────────────────────

    static readonly AlgorithmDef Fibonacci = new("fibonacci", "피보나치 수열 (DP)",
        """
        // 탑다운 (메모이제이션)
        int FibMemo(int n, int[] memo)
        {
            if (n <= 1) return n;
            if (memo[n] != -1) return memo[n];
            return memo[n] = FibMemo(n-1, memo) + FibMemo(n-2, memo);
        }

        // 보텀업 (타뷸레이션)
        int Fibonacci(int n)
        {
            if (n <= 1) return n;
            int[] dp = new int[n + 1];
            dp[0] = 0; dp[1] = 1;
            for (int i = 2; i <= n; i++)
                dp[i] = dp[i-1] + dp[i-2];
            return dp[n];
        }

        Console.WriteLine(Fibonacci(10)); // 55
        """,
        ["dp[i]=dp[i-1]+dp[i-2]"]);

    static readonly AlgorithmDef Knapsack = new("knapsack", "배낭 문제 (0/1 Knapsack)",
        """
        int Knapsack(int[] weights, int[] values, int capacity)
        {
            int n = weights.Length;
            int[,] dp = new int[n + 1, capacity + 1];

            for (int i = 1; i <= n; i++)
            {
                for (int w = 0; w <= capacity; w++)
                {
                    dp[i, w] = dp[i-1, w]; // 넣지 않는 경우
                    if (weights[i-1] <= w)  // 넣는 경우
                        dp[i, w] = Math.Max(dp[i, w],
                            dp[i-1, w - weights[i-1]] + values[i-1]);
                }
            }
            return dp[n, capacity];
        }

        // 무게 [2,3,4], 가치 [3,4,5], 용량 5
        Console.WriteLine(Knapsack(new[]{2,3,4}, new[]{3,4,5}, 5)); // 7
        """,
        ["dp[i,w]", "weights[i-1]"]);

    static readonly AlgorithmDef LCS = new("lcs", "LCS (최장 공통 부분수열)",
        """
        int LCS(string a, string b)
        {
            int m = a.Length, n = b.Length;
            int[,] dp = new int[m + 1, n + 1];

            for (int i = 1; i <= m; i++)
            {
                for (int j = 1; j <= n; j++)
                {
                    if (a[i-1] == b[j-1])
                        dp[i, j] = dp[i-1, j-1] + 1;
                    else
                        dp[i, j] = Math.Max(dp[i-1, j], dp[i, j-1]);
                }
            }
            return dp[m, n];
        }

        Console.WriteLine(LCS("ABCBDAB", "BDCAB")); // 4 (BCAB)
        """,
        ["dp[i,j]", "a[i-1]==b[j-1]"]);

    static readonly AlgorithmDef LIS = new("lis", "LIS (최장 증가 부분수열)",
        """
        int LIS(int[] arr)
        {
            int n = arr.Length;
            int[] dp = new int[n];
            Array.Fill(dp, 1);

            for (int i = 1; i < n; i++)
                for (int j = 0; j < i; j++)
                    if (arr[j] < arr[i])
                        dp[i] = Math.Max(dp[i], dp[j] + 1);

            return dp.Max();
        }

        int[] arr = { 3, 1, 4, 1, 5, 9, 2, 6 };
        Console.WriteLine(LIS(arr)); // 4 (1,4,5,9)
        """,
        ["dp[i]=", "arr[j]<arr[i]"]);

    // ── 6단계: 그리디 ─────────────────────────────────

    static readonly AlgorithmDef CoinChange = new("coin-change", "거스름돈 (그리디)",
        """
        int[] CoinChange(int[] coins, int amount)
        {
            // 큰 동전부터 사용 (정렬 후 내림차순)
            Array.Sort(coins);
            Array.Reverse(coins);
            var result = new List<int>();

            foreach (int coin in coins)
            {
                while (amount >= coin)
                {
                    result.Add(coin);
                    amount -= coin;
                }
            }
            return result.ToArray();
        }

        // 동전: 500, 100, 50, 10 / 거스름돈: 1260원
        var coins = new[] { 500, 100, 50, 10 };
        var change = CoinChange(coins, 1260);
        Console.WriteLine(string.Join(", ", change));
        // 500, 500, 100, 100, 50, 10
        """,
        ["amount-=", "coin"]);

    static readonly AlgorithmDef ActivitySelection = new("activity-selection", "활동 선택 (그리디)",
        """
        int ActivitySelection(int[] start, int[] end)
        {
            int n = start.Length;
            // 종료 시간 기준 정렬
            int[] idx = Enumerable.Range(0, n).OrderBy(i => end[i]).ToArray();

            int count = 1, lastEnd = end[idx[0]];
            for (int i = 1; i < n; i++)
            {
                if (start[idx[i]] >= lastEnd)
                {
                    count++;
                    lastEnd = end[idx[i]];
                }
            }
            return count;
        }

        int[] s = { 1, 3, 0, 5, 8, 5 };
        int[] e = { 2, 4, 6, 7, 9, 9 };
        Console.WriteLine(ActivitySelection(s, e)); // 4
        """,
        ["end[", "start["]);

    // ── 7단계: 분할정복 ───────────────────────────────

    static readonly AlgorithmDef BinarySearchApplied = new("binary-search-applied", "이진탐색 응용 (Lower Bound)",
        """
        // target 이상인 첫 번째 인덱스 찾기
        int LowerBound(int[] arr, int target)
        {
            int left = 0, right = arr.Length;
            while (left < right)
            {
                int mid = left + (right - left) / 2;
                if (arr[mid] < target) left = mid + 1;
                else right = mid;
            }
            return left;
        }

        // target 초과인 첫 번째 인덱스 찾기
        int UpperBound(int[] arr, int target)
        {
            int left = 0, right = arr.Length;
            while (left < right)
            {
                int mid = left + (right - left) / 2;
                if (arr[mid] <= target) left = mid + 1;
                else right = mid;
            }
            return left;
        }

        int[] arr = { 1, 2, 2, 3, 3, 4, 5 };
        Console.WriteLine(LowerBound(arr, 3)); // 3
        Console.WriteLine(UpperBound(arr, 3)); // 5
        """,
        ["left<right", "arr[mid]<target"]);

    static readonly AlgorithmDef MergeSortApplied = new("merge-sort-applied", "합병정렬 응용 (역순 쌍 세기)",
        """
        long CountInversions(int[] arr)
        {
            if (arr.Length <= 1) return 0;
            int mid = arr.Length / 2;
            var left  = arr[..mid];
            var right = arr[mid..];
            long count = CountInversions(left) + CountInversions(right);

            int i = 0, j = 0, k = 0;
            while (i < left.Length && j < right.Length)
            {
                if (left[i] <= right[j]) arr[k++] = left[i++];
                else
                {
                    arr[k++] = right[j++];
                    count += left.Length - i; // 역순 쌍 추가
                }
            }
            while (i < left.Length)  arr[k++] = left[i++];
            while (j < right.Length) arr[k++] = right[j++];
            return count;
        }

        int[] arr = { 5, 3, 2, 4, 1 };
        Console.WriteLine(CountInversions(arr)); // 9
        """,
        ["countinversions(", "left.length-i"]);

    // ── 8단계: 문자열 ─────────────────────────────────

    static readonly AlgorithmDef KMP = new("kmp", "KMP 문자열 탐색",
        """
        int[] BuildLPS(string pattern)
        {
            int m = pattern.Length;
            int[] lps = new int[m];
            int len = 0, i = 1;
            while (i < m)
            {
                if (pattern[i] == pattern[len]) lps[i++] = ++len;
                else if (len != 0) len = lps[len - 1];
                else lps[i++] = 0;
            }
            return lps;
        }

        List<int> KMPSearch(string text, string pattern)
        {
            int[] lps = BuildLPS(pattern);
            var result = new List<int>();
            int i = 0, j = 0;
            while (i < text.Length)
            {
                if (text[i] == pattern[j]) { i++; j++; }
                if (j == pattern.Length)
                {
                    result.Add(i - j);
                    j = lps[j - 1];
                }
                else if (i < text.Length && text[i] != pattern[j])
                {
                    if (j != 0) j = lps[j - 1];
                    else i++;
                }
            }
            return result;
        }

        var positions = KMPSearch("AABAACAADAABAABA", "AABA");
        Console.WriteLine(string.Join(", ", positions)); // 0, 9, 12
        """,
        ["lps[", "pattern[j]"]);

    static readonly AlgorithmDef Trie = new("trie", "트라이 (Trie)",
        """
        class TrieNode
        {
            public Dictionary<char, TrieNode> Children = new();
            public bool IsEnd;
        }

        class Trie
        {
            readonly TrieNode root = new();

            public void Insert(string word)
            {
                var node = root;
                foreach (char c in word)
                {
                    if (!node.Children.ContainsKey(c))
                        node.Children[c] = new TrieNode();
                    node = node.Children[c];
                }
                node.IsEnd = true;
            }

            public bool Search(string word)
            {
                var node = root;
                foreach (char c in word)
                {
                    if (!node.Children.ContainsKey(c)) return false;
                    node = node.Children[c];
                }
                return node.IsEnd;
            }

            public bool StartsWith(string prefix)
            {
                var node = root;
                foreach (char c in prefix)
                {
                    if (!node.Children.ContainsKey(c)) return false;
                    node = node.Children[c];
                }
                return true;
            }
        }

        var trie = new Trie();
        trie.Insert("apple");
        trie.Insert("app");
        Console.WriteLine(trie.Search("app"));      // True
        Console.WriteLine(trie.StartsWith("appl")); // True
        Console.WriteLine(trie.Search("appl"));     // False
        """,
        ["children[", "isend"]);

    public static readonly List<Stage> Stages =
    [
        new("1단계: 정렬", [BubbleSort, SelectionSort, InsertionSort, MergeSort, QuickSort, HeapSort]),
        new("2단계: 탐색", [LinearSearch, BinarySearch]),
        new("3단계: 자료구조", [StackQueue, LinkedList, Tree, HashTable, HeapDS]),
        new("4단계: 그래프", [DFS, BFS, Dijkstra]),
        new("5단계: DP", [Fibonacci, Knapsack, LCS, LIS]),
        new("6단계: 그리디", [CoinChange, ActivitySelection]),
        new("7단계: 분할정복", [BinarySearchApplied, MergeSortApplied]),
        new("8단계: 문자열", [KMP, Trie]),
    ];

    public static AlgorithmDef? Find(string id) =>
        Stages.SelectMany(s => s.Algorithms).FirstOrDefault(a => a.Id == id);
}
